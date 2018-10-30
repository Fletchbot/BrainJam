using UnityEngine;
using System.Collections;
//using Artngame.SKYMASTER;
using SoliGameController;

namespace Artngame.SKYMASTER
{
    public class WeatherManager : MonoBehaviour
    {
        SkyMasterManager skyManager;
        WaterHandlerSM waterManager;
        public Material SnowLavaMat;

        public GameController gc;
        public AudioPlaytestManager au;

        public bool affectFog = false;
        public bool affectFogParams = false;
        public string currentWeather;//for debug purposes
        public float cloudDensityChangeSpeed = 1;
        int weatherChoice = -1;

        public float shaderOffset;

        public bool turnonLava, turnonSnow;
        public float growLavaSnow, onStartGrow;
        public float meltLavaSnow;

        public bool NoGesture, Meditate, Happy, Sad, Unsure;       
        public bool noGHeld_Reached, mHeld_Reached, hHeld_Reached, sHeld_Reached, uHeld_Reached;
        public bool MeditationTested, HappinessTested, SadnessTested, FocusTested;

        public int state, n_Intro;

        // Use this for initialization
        void Start()
        {
            skyManager = this.GetComponent<SkyMasterManager>();
            onStartGrow = 1.8f;
            growLavaSnow = 0.008f;
            meltLavaSnow = 0.004f;
     
        }       
        // Update is called once per frame
        void Update()
        {
            state = gc.state;
            n_Intro = au.N_Intro;

            NoGesture = gc.NoGesture;
            Meditate = gc.Meditate;

            Happy = gc.Happy;
            Sad = gc.Sad;
            Unsure = gc.Unsure;
            
            noGHeld_Reached = gc.noGHeld_Reached;
            mHeld_Reached = gc.mHeld_Reached;
            hHeld_Reached = gc.hHeld_Reached;
            sHeld_Reached = gc.sHeld_Reached;
            uHeld_Reached = gc.uHeld_Reached;

            MeditationTested = gc.MeditationTested;
            HappinessTested = gc.HappinessTested;
            SadnessTested = gc.SadnessTested;
            FocusTested = gc.FocusTested;

            SkymasterWeather();
            StateSwitch();
            snow_lava_SW();
        }

        void StateSwitch()
        {
            //No Gesture Timeout
            if (NoGesture && state == -1 && !noGHeld_Reached) //Storm
            {
                weatherChoice = 3;
            }
            else if (NoGesture && state == -1 && noGHeld_Reached || NoGesture && state == 0) // Heavy Storm
            {
                weatherChoice = 4;
            }

            //Meditate Happy
            if(Meditate && state <=2||Happy && state == 2 ||Meditate && Happy && state == -1 || Happy && !Meditate && state == -1) //Sunny
            {
                weatherChoice = 0;
            }

            //Sad
            if (Sad && state >= 3 && SadnessTested) //Heavy Snow Storm
            {
                weatherChoice = 2;
            }
            else if (Sad && state == -1 && !sHeld_Reached) //Rain (both !meditate/meditate)
            {
              //  weatherChoice = 1;
            }
            else if (Sad && state == -1 && sHeld_Reached) //Heavy Rain (both !meditate/meditate)
            {
                weatherChoice = 2;
            }

            //Unsure
            if (Unsure && state == -1 && !uHeld_Reached) //Cloudy (both !meditate/meditate)
            {
                weatherChoice = 7;
            }
            else if (Unsure && state == -1 && uHeld_Reached) //Heavy Cloud (both !meditate/meditate)
            {
                weatherChoice = 8;
            }

        }

        void snow_lava_SW()
        {
            
            //Eruption No Gesture State LAVA
            if (NoGesture && noGHeld_Reached && state == -1 || NoGesture && state == 0)
            {
                turnonSnow = false;

                if (shaderOffset >= -1.0f && shaderOffset <= 1.8f)
                {
                    if (state >= 0)
                    {
                        shaderOffset = onStartGrow;
                    }
                    else
                    {
                        shaderOffset = shaderOffset + growLavaSnow;
                    }
                }

                SnowLavaMat.SetFloat("_LightIntensity", 4.0f);
                SnowLavaMat.SetFloat("_isLava", 1);
                SnowLavaMat.SetFloat("Snow_Cover_offset", shaderOffset);
            }

            //SNOW 
     /*       if (Sad && state == 2 && n_Intro == 4) ///Add Snow when sad  + higher level states start game 
            {
                turnonLava = false;

                if (shaderOffset >= -1.0f && shaderOffset <= 1.8f)
                {
                    shaderOffset = shaderOffset + growLavaSnow;
                }
                SnowLavaMat.SetFloat("_LightIntensity", 1.0f);
                SnowLavaMat.SetFloat("_isLava", 0);
                SnowLavaMat.SetFloat("Snow_Cover_offset", shaderOffset);
            }*/
            //MELT LAVA/SNOW
            if (Meditate && state == -1 || Happy && state == -1 || Sad && state == -1 || Unsure && !NoGesture || Meditate && state == 2 || Happy && state >= 3)
            {
                turnonLava = false;
                turnonSnow = false;

                if (shaderOffset >= 0.0f)
                {
                    shaderOffset = shaderOffset - meltLavaSnow;
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
                skyManager.WeatherSeverity = 1;
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
                skyManager.WeatherSeverity = 9;
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
