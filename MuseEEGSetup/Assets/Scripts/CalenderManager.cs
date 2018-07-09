using UnityEngine;
using System.Collections;
//using Artngame.SKYMASTER;

namespace Artngame.SKYMASTER
{
    public class CalenderManager : MonoBehaviour
    {

        SkyMasterManager skyManager;
        WeatherManager weatherManager;

        public int CurrMonth;
        public int CurrDay;
        public float CurrHour;
        public float speed;

        private bool NoGesture, Mediate, Happy, Sad, Instr1, Instr2;
        private bool G_sw, M_sw, H_sw, S_sw, I1_sw, I2_sw;

        // Use this for initialization
        void Start()
        {
            skyManager = this.GetComponent<SkyMasterManager>();
            weatherManager = this.GetComponent<WeatherManager>();

        }

        // Update is called once per frame
        void Update()
        {
            NoGesture = weatherManager.NoGesture;
            Mediate = weatherManager.Mediate;
            Happy = weatherManager.Happy;
            Sad = weatherManager.Sad;
            Instr1 = weatherManager.Instr1;
            Instr2 = weatherManager.Instr2;

            skyManager.Current_Month = CurrMonth;
            skyManager.Current_Day = CurrDay;
            CurrHour = skyManager.Current_Time;
            skyManager.SPEED = speed;

            StateSwitch();

        }

        void StateSwitch()
        {
            float speedup = 16 * 4.0f;

            if (NoGesture) //Volcano Erupt
            {
                CurrMonth = 10;
                CurrDay = 10;

               if (!G_sw && CurrHour >= 23.75f && CurrHour <= 23.9f)
                {
                    speed = 0.0f;
                    G_sw = true;
                }
                else if (!G_sw)
                {
                    speed = speedup;
                }

                M_sw = false;
                H_sw = false;
                S_sw = false;
                Instr1 = false;
                Instr2 = false;
            }
            else if (Mediate) //Sunset
            {
                CurrMonth = 5;
                CurrDay = 5;

                if (!M_sw && CurrHour <= 20.2f && CurrHour >= 20.0f)
                {
                    speed = 0.5f;
                    M_sw = true;
                }
                else if (!M_sw) 
                {
                    speed = speedup;
                }

                G_sw = false;
                H_sw = false;
                S_sw = false;
                Instr1 = false;
                Instr2 = false;
            }
            else if (Happy) //Midday Sun
            {
                CurrMonth = 6;
                CurrDay = 20;

                if (!H_sw && CurrHour > 13.4f && CurrHour < 13.6f)
                {
                    speed = 1.0f;
                    H_sw = true;
                }
                else if (!H_sw)
                {
                    speed = speedup;
                }

                G_sw = false;
                M_sw = false;
                S_sw = false;
                Instr1 = false;
                Instr2 = false;
            }
            else if (Sad) //Winter Night
            {
                CurrMonth = 1;
                CurrDay = 1;
                if (!S_sw && CurrHour > 8.95f && CurrHour < 9.0f)
                {
                    speed = 0.0f;
                    S_sw = true;
                }
                else if (!S_sw)
                {
                    speed = speedup;
                }

                G_sw = false;
                M_sw = false;
                H_sw = false;
                Instr1 = false;
                Instr2 = false;
            }
            else if (Instr1) //Rain morning
            {
                CurrMonth = 8;
                CurrDay = 8;
                if (!I1_sw && CurrHour > 9.3f && CurrHour < 9.5f)
                {
                    speed = 1.0f;
                    Instr1 = true;
                }
                else if (!I1_sw)
                {
                    speed = speedup;
                }

                G_sw = false;
                M_sw = false;
                H_sw = false;
                S_sw = false;
                Instr2 = false;
            }
            else if (Instr2) //Raining Afternoon
            {
                CurrMonth = 2;
                CurrDay = 8;
                if (!I2_sw && CurrHour > 17.7f && CurrHour < 17.8f)
                {
                    speed = 1.0f;
                    Instr2 = true;
                }
                else if (!I2_sw)
                {
                    speed = speedup;
                }

                G_sw = false;
                M_sw = false;
                H_sw = false;
                S_sw = false;
                Instr1 = false;
            }
            else
            {
                speed = 1;
            }
        }
    }
}