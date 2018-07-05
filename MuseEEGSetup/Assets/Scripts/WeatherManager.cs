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

        public bool isLava;
        public float shaderOffset;

        public bool NoGesture, Mediate, Happy, Sad, Instr1, Instr2; 
        // Use this for initialization
        void Start()
        {
            skyManager = this.GetComponent<SkyMasterManager>();

        }

        public string currentWeather;//for debug purposes

        public float cloudDensityChangeSpeed = 1;

        int weatherChoice = -1;
        

        // Update is called once per frame
        void Update()
        {
            NoGesture = gestureController.GetComponent<GestureController>().NoGesture;
            Mediate = gestureController.GetComponent<GestureController>().Mediate;
            Happy = gestureController.GetComponent<GestureController>().Happy;
            Sad = gestureController.GetComponent<GestureController>().Sad;
            Instr1 = gestureController.GetComponent<GestureController>().Instr1;
            Instr2 = gestureController.GetComponent<GestureController>().Instr2;

            StateSwitch();
            snow_lava_SW();
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

                    if (weatherChoice == 0)
                    {
                        terrainControl.fogDensity = Mathf.Lerp(terrainControl.fogDensity, 0.1f, Speed1 * Time.deltaTime); //for light rain, light snow
                        terrainControl.AddFogHeightOffset = Mathf.Lerp(terrainControl.AddFogHeightOffset, 700, Speed1 * Time.deltaTime);
                    }
                    if (weatherChoice == 1 || weatherChoice == 5)
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

        }

        void StateSwitch()
        {
            if (NoGesture)
            {
                isLava = false;
                weatherChoice = 6;
            }
            else if (Mediate)
            {
                weatherChoice = 0;
            }
            else if (Happy)
            {
                weatherChoice = 0;

            }
            else if (Sad)
            {
                isLava = true;
                weatherChoice = 4;
            }
            else if (Instr1)
            {
                weatherChoice = 1;
            }
            else if (Instr2)
            {
                weatherChoice = 1;
            }
        }

        void snow_lava_SW()
        {
            float rate = 0.006f;
            if (NoGesture == true && isLava == false)
            {
                if(shaderOffset >=-1.0f && shaderOffset <= 1.8f)
                {
                    shaderOffset = shaderOffset + rate;
                }
                SnowLavaMat.SetFloat("_LightIntensity", 1.0f);
                SnowLavaMat.SetFloat("_isLava", 0);
                SnowLavaMat.SetFloat("Snow_Cover_offset", shaderOffset);

            }
            else if (Sad == true && isLava == true)
            {
                if (shaderOffset >= -1.0f && shaderOffset <= 1.8f)
                {
                    shaderOffset = shaderOffset + rate;
                }
                SnowLavaMat.SetFloat("_LightIntensity", 4.0f);
                SnowLavaMat.SetFloat("_isLava", 1);
                SnowLavaMat.SetFloat("Snow_Cover_offset", shaderOffset);

            } else if (Happy || Mediate || Instr1 || Instr2)
            {
                if (shaderOffset >= 0.0f)
                {
                    shaderOffset = shaderOffset - rate;
                } else if (shaderOffset <= 0.0f)
                {
                    SnowLavaMat.SetFloat("_isLava", 0);
                }
                SnowLavaMat.SetFloat("_LightIntensity", 1.0f);
                SnowLavaMat.SetFloat("Snow_Cover_offset", shaderOffset);
            }
        }
    }
}
