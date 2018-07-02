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

        public bool NoGesture, Mediate, Happy, Sad, Instr1, Instr2;

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
            if (NoGesture)
            {
                CurrMonth = 1;
                CurrDay = 1;
                if(CurrHour > 8.7f && CurrHour < 8.9f)
                {
                    speed = 0;
                } else
                {
                    speed = 16*3.5f;
                }
            }
            else if (Mediate)
            {
                CurrMonth = 5;
                CurrDay = 5;
                if (CurrHour > 19.9f && CurrHour < 20.1f)
                {
                    speed = 0;
                }
                else
                {
                    speed = 16*3.5f;
                }
            }
            else if (Happy)
            {
                CurrMonth = 6;
                CurrDay = 20;

                if (CurrHour > 13.4f && CurrHour < 13.6f)
                {
                    speed = 0;
                }
                else
                {
                    speed = 16*3.5f;
                }
            }
            else if (Sad)
            {
                CurrMonth = 10;
                CurrDay = 10;

                if (CurrHour > 21.4f && CurrHour < 21.6f)
                {
                    speed = 0;
                }
                else
                {
                    speed = 16*3.5f;
                }
            }
            else if (Instr1)
            {
                CurrMonth = 8;
                CurrDay = 8;
                if (CurrHour > 9.3f && CurrHour < 9.5f)
                {
                    speed = 0;
                }
                else
                {
                    speed = 16*3.5f;
                }
            }
            else if (Instr2)
            {
                CurrMonth = 2;
                CurrDay = 8;
                if (CurrHour > 9.7f && CurrHour < 9.8f)
                {
                    speed = 0;
                }
                else
                {
                    speed = 16*3.5f;
                }

            }
            else
            {
                speed = 1;
            }
        }
    }
}