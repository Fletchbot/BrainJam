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
            skyManager.Current_Time = CurrHour;

            StateSwitch();

        }

        void StateSwitch()
        {
            if (NoGesture)
            {
                CurrMonth = 1;
                CurrDay = 1;
                CurrHour = 8.8f;
            }
            else if (Mediate)
            {
                CurrMonth = 5;
                CurrDay = 5;
                CurrHour = 20;
            }
            else if (Happy)
            {
                CurrMonth = 6;
                CurrDay = 20;
                CurrHour = 13.0f;
            }
            else if (Sad)
            {
                CurrMonth = 10;
                CurrDay = 10;
                CurrHour = 21.5f;
            }
            else if (Instr1)
            {
                CurrMonth = 8;
                CurrDay = 8;
                CurrHour = 9.0f;
            }
            else if (Instr2)
            {
                CurrMonth = 2;
                CurrDay = 8;
                CurrHour = 9.5f;

            }
        }
    }
}