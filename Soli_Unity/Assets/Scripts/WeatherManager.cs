using UnityEngine;
using System.Collections;
//using Artngame.SKYMASTER;

namespace Artngame.SKYMASTER
{
    public class WeatherManager : MonoBehaviour
    {
        SkyMasterManager skyManager;
        WaterHandlerSM waterManager;
        public Material SnowLavaMat;

        public GameObject gestureController;

        public bool affectFog = false;
        public bool affectFogParams = false;
        public string currentWeather;//for debug purposes
        public float cloudDensityChangeSpeed = 1;
        int weatherChoice = -1;

        public float shaderOffset;

        public bool StartGame;
        public bool turnonLava, turnonSnow;

        public bool NoGesture, Meditate, Happy, Sad, Unsure;       
        public bool noGHeld_Reached, mHeld_Reached, hHeld_Reached, sHeld_Reached, uHeld_Reached;

        // Use this for initialization
        void Start()
        {
            skyManager = this.GetComponent<SkyMasterManager>();
        }       
        // Update is called once per frame
        void Update()
        {
            StartGame = gestureController.GetComponent<GestureController>().StartGame;

            NoGesture = gestureController.GetComponent<GestureController>().NoGesture;
            Meditate = gestureController.GetComponent<GestureController>().Meditate;

            Happy = gestureController.GetComponent<GestureController>().Happy;
            Sad = gestureController.GetComponent<GestureController>().Sad;
            Unsure = gestureController.GetComponent<GestureController>().Unsure;
            
            noGHeld_Reached = gestureController.GetComponent<GestureController>().noGHeld_Reached;
            mHeld_Reached = gestureController.GetComponent<GestureController>().mHeld_Reached;
            hHeld_Reached = gestureController.GetComponent<GestureController>().hHeld_Reached;
            sHeld_Reached = gestureController.GetComponent<GestureController>().sHeld_Reached;
            uHeld_Reached = gestureController.GetComponent<GestureController>().uHeld_Reached;

            SkymasterWeather();
            StateSwitch();
            snow_lava_SW();
        }

        void StateSwitch()
        {
            //No Gesture Timeout
            if (NoGesture && StartGame && !noGHeld_Reached) //Storm
            {
                weatherChoice = 3;
            }
            else if (NoGesture && StartGame && noGHeld_Reached || NoGesture && !StartGame) // Heavy Storm
            {
                weatherChoice = 4;
            }

            //Meditate or Happy
            if(Meditate || Happy && !Meditate) //Sunny
            {
                weatherChoice = 0;
            }

            //Sad
            if (Sad && !StartGame) //Heavy Snow Storm
            {
                weatherChoice = 6;
            }
            else if (Sad && StartGame && !sHeld_Reached) //Rain
            {
                weatherChoice = 1;
            }
            else if (Sad && StartGame && sHeld_Reached) //Heavy Rain
            {
                weatherChoice = 2;
            }

            //Unsure
            if (Unsure && !uHeld_Reached) //Cloudy
            {
                weatherChoice = 7;
            }
            else if (Unsure && uHeld_Reached) //Heavy Cloud
            {
                weatherChoice = 8;
            }

        }

        void snow_lava_SW()
        {
            float rate = 0.1f;

            //LAVA
            if (NoGesture && noGHeld_Reached && StartGame || NoGesture && !StartGame)
            {
                turnonSnow = false;

                if (shaderOffset >= -1.0f && shaderOffset <= 1.8f)
                {
                    shaderOffset = shaderOffset + rate;
                }

                SnowLavaMat.SetFloat("_LightIntensity", 4.0f);
                SnowLavaMat.SetFloat("_isLava", 1);
                SnowLavaMat.SetFloat("Snow_Cover_offset", shaderOffset);
            }

            //SNOW
            if (Sad && !StartGame) ///Add Snow when sad  + higher level states start game 
            {
                turnonLava = false;

                if (shaderOffset >= -1.0f && shaderOffset <= 1.8f)
                {
                    shaderOffset = shaderOffset + rate;
                }
                SnowLavaMat.SetFloat("_LightIntensity", 1.0f);
                SnowLavaMat.SetFloat("_isLava", 0);
                SnowLavaMat.SetFloat("Snow_Cover_offset", shaderOffset);
            }

            //MELT LAVA/SNOW
            if (Happy || Meditate || Unsure || Sad && StartGame)
            {
                turnonLava = false;
                turnonSnow = false;

                if (shaderOffset >= 0.0f)
                {
                    shaderOffset = shaderOffset - rate;
                }
                else if (shaderOffset <= 0.0f)
                {
                    SnowLavaMat.SetFloat("_isLava", 0);
                }

                SnowLavaMat.SetFloat("_LightIntensity", 1.0f);
                SnowLavaMat.SetFloat("Snow_Cover_offset", shaderOffset);
            }
        }

        void SkymasterWeather()
        {
            //NOTE removed +0.1 modifier in shader volume clouds script (v3.4.8) for slower changes in cloud coloration transitions

            float Speed1 = 0.1f;


            if (affectFog)
            {

                SeasonalTerrainSKYMASTER terrainControl = skyManager.Terrain_controller;
                if (terrainControl == null)
                {
                    terrainControl = skyManager.Mesh_Terrain_controller;
                }

                if (terrainControl != null)
                {

                    terrainControl.UseFogCurves = true;
                    terrainControl.SkyFogOn = true;
                    terrainControl.HeightFogOn = true;

                    if (affectFogParams)
                    {
                        terrainControl.fogGradientDistance = 0; //v3.4.9
                        terrainControl.VFogDistance = 12;
                    }

                    if (weatherChoice == 0 || weatherChoice == 7)
                    {
                        terrainControl.fogDensity = Mathf.Lerp(terrainControl.fogDensity, 0.1f, Speed1 * Time.deltaTime); //for light rain, light snow
                        terrainControl.AddFogHeightOffset = Mathf.Lerp(terrainControl.AddFogHeightOffset, 700, Speed1 * Time.deltaTime);
                    }
                    if (weatherChoice == 1 || weatherChoice == 5 || weatherChoice == 8)
                    {
                        terrainControl.fogDensity = Mathf.Lerp(terrainControl.fogDensity, 0.5f, Speed1 * Time.deltaTime); //for  rain,  snow
                        terrainControl.AddFogHeightOffset = Mathf.Lerp(terrainControl.AddFogHeightOffset, 700, Speed1 * Time.deltaTime);
                    }
                    if (weatherChoice == 2 || weatherChoice == 3 || weatherChoice == 4 || weatherChoice == 6)
                    {
                        terrainControl.fogDensity = Mathf.Lerp(terrainControl.fogDensity, 2, Speed1 * Time.deltaTime); //for heavy weather
                        terrainControl.AddFogHeightOffset = Mathf.Lerp(terrainControl.AddFogHeightOffset, 700, Speed1 * Time.deltaTime);
                    }
                }
            }

            float speed = 0.05f * cloudDensityChangeSpeed * Time.deltaTime;
            if (weatherChoice == 0)
            {
                skyManager.currentWeatherName = SkyMasterManager.Volume_Weather_types.Cloudy;
                if (skyManager.VolShaderCloudsH != null)
                {
                    skyManager.VolShaderCloudsH.ClearDayCoverage = Mathf.Lerp(skyManager.VolShaderCloudsH.ClearDayCoverage, -0.85f, speed); // -0.55f;
                }
                skyManager.WeatherSeverity = 0;
                currentWeather = "Sunny";
            }
            if (weatherChoice == 1)
            {
                skyManager.currentWeatherName = SkyMasterManager.Volume_Weather_types.Rain;
                if (skyManager.VolShaderCloudsH != null)
                {
                    skyManager.VolShaderCloudsH.ClearDayCoverage = Mathf.Lerp(skyManager.VolShaderCloudsH.ClearDayCoverage, -0.25f, speed * 2); // -0.25f;
                }
                skyManager.WeatherSeverity = 4;
                currentWeather = "Rain";
            }
            if (weatherChoice == 2)
            {
                skyManager.currentWeatherName = SkyMasterManager.Volume_Weather_types.Rain;
                if (skyManager.VolShaderCloudsH != null)
                {
                    skyManager.VolShaderCloudsH.ClearDayCoverage = Mathf.Lerp(skyManager.VolShaderCloudsH.ClearDayCoverage, -0.20f, speed * 4.5f); //-0.20f;
                }
                skyManager.WeatherSeverity = 10;
                currentWeather = "Heavy Rain";
            }
            if (weatherChoice == 3)
            {
                skyManager.currentWeatherName = SkyMasterManager.Volume_Weather_types.HeavyStorm;
                if (skyManager.VolShaderCloudsH != null)
                {
                    skyManager.VolShaderCloudsH.ClearDayCoverage = Mathf.Lerp(skyManager.VolShaderCloudsH.ClearDayCoverage, -0.15f, speed * 5); // -0.15f;
                    skyManager.VolShaderCloudsH.StormCoverage = Mathf.Lerp(skyManager.VolShaderCloudsH.ClearDayCoverage, -0.17f, speed * 5); // -0.17f;
                }
                skyManager.WeatherSeverity = 4;
                currentWeather = "Storm";
            }
            if (weatherChoice == 4)
            {
                skyManager.currentWeatherName = SkyMasterManager.Volume_Weather_types.HeavyStorm;
                if (skyManager.VolShaderCloudsH != null)
                {
                    skyManager.VolShaderCloudsH.ClearDayCoverage = Mathf.Lerp(skyManager.VolShaderCloudsH.ClearDayCoverage, -0.10f, speed * 5); // -0.10f;
                    skyManager.VolShaderCloudsH.StormCoverage = Mathf.Lerp(skyManager.VolShaderCloudsH.ClearDayCoverage, -0.10f, speed * 5); // -0.10f;
                }
                skyManager.WeatherSeverity = 10;
                currentWeather = "Heavy Storm";
            }
            if (weatherChoice == 5)
            {
                skyManager.currentWeatherName = SkyMasterManager.Volume_Weather_types.SnowStorm;
                if (skyManager.VolShaderCloudsH != null)
                {
                    skyManager.VolShaderCloudsH.ClearDayCoverage = Mathf.Lerp(skyManager.VolShaderCloudsH.ClearDayCoverage, -0.25f, speed * 2); // -0.10f;
                    skyManager.VolShaderCloudsH.StormCoverage = Mathf.Lerp(skyManager.VolShaderCloudsH.ClearDayCoverage, -0.25f, speed * 2); // -0.10f;
                }
                skyManager.WeatherSeverity = 2;
                currentWeather = "Snow Storm";
            }
            if (weatherChoice == 6)
            {
                skyManager.currentWeatherName = SkyMasterManager.Volume_Weather_types.SnowStorm;
                if (skyManager.VolShaderCloudsH != null)
                {
                    skyManager.VolShaderCloudsH.ClearDayCoverage = Mathf.Lerp(skyManager.VolShaderCloudsH.ClearDayCoverage, -0.15f, speed); // -0.10f;
                    skyManager.VolShaderCloudsH.StormCoverage = Mathf.Lerp(skyManager.VolShaderCloudsH.ClearDayCoverage, -0.15f, speed); // -0.10f;
                }
                skyManager.WeatherSeverity = 10;
                currentWeather = "Heavy Snow Storm";
            }
            if (weatherChoice == 7)
            {
                skyManager.currentWeatherName = SkyMasterManager.Volume_Weather_types.FlatClouds;
                if (skyManager.VolShaderCloudsH != null)
                {
                    skyManager.VolShaderCloudsH.ClearDayCoverage = Mathf.Lerp(skyManager.VolShaderCloudsH.ClearDayCoverage, -0.45f, speed); // -0.55f;
                }
                skyManager.WeatherSeverity = 4;
                currentWeather = "Light Cloud";
            }
            if (weatherChoice == 8)
            {
                skyManager.currentWeatherName = SkyMasterManager.Volume_Weather_types.Cloudy;
                if (skyManager.VolShaderCloudsH != null)
                {
                    skyManager.VolShaderCloudsH.ClearDayCoverage = Mathf.Lerp(skyManager.VolShaderCloudsH.ClearDayCoverage, -0.25f, speed * 2); // -0.55f;
                }
                skyManager.WeatherSeverity = 6;
                currentWeather = "Heavy Cloud";
            }

        }


    }
}
