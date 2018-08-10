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
        public float speedUp, acc, maxAcc;
        private float normSpeed;
        private bool StartGame;
        private bool NoGesture, Meditate, Happy, Sad, Unsure; 
        private bool G_sw, M_sw, H_sw, S_sw, U_sw, noGHeld_sw, mHeld_sw, hHeld_sw, sHeld_sw, uHeld_sw;
        private bool noGHeld_Reached, mHeld_Reached, hHeld_Reached, sHeld_Reached, uHeld_Reached;

        // Use this for initialization
        void Start()
        {
            skyManager = this.GetComponent<SkyMasterManager>();
            weatherManager = this.GetComponent<WeatherManager>();
            normSpeed = 1.0f;
            maxAcc = 64.0f;
        }

        // Update is called once per frame
        void Update()
        {
            StartGame = weatherManager.StartGame;

            NoGesture = weatherManager.NoGesture;
            Meditate = weatherManager.Meditate;
            Happy = weatherManager.Happy;
            Sad = weatherManager.Sad;
            Unsure = weatherManager.Unsure;

            noGHeld_Reached = weatherManager.noGHeld_Reached;
            mHeld_Reached = weatherManager.mHeld_Reached;
            hHeld_Reached = weatherManager.hHeld_Reached;
            sHeld_Reached = weatherManager.sHeld_Reached;
            uHeld_Reached = weatherManager.uHeld_Reached;
            
            skyManager.Current_Month = CurrMonth;
            skyManager.Current_Day = CurrDay;
            CurrHour = skyManager.Current_Time;
            skyManager.SPEED = speed;

            SpeedUpController();
        //    StateSwitch();
        }

        void SpeedUpController()
        {
            if(speedUp <= normSpeed && speedUp >= maxAcc)
            {

            }
            speedUp = normSpeed * acc;
        }

 /*       void StateSwitch()
        {
         //   float speedup = 16 * 4.0f;

            if (noGHeld_Reached)
            {
                noGHeld_sw = true;
            }
            if (M_Reached)
            {
                m_Rsw = true;
            }
            if (H_Reached)
            {
                h_Rsw = true;
            }
            if (S_Reached)
            {
                s_Rsw = true;
            }

            if (NoGesture) //Volcano Erupt
            {
                CurrMonth = 10;
                CurrDay = 10;

                if (!StartGame && CurrHour >= 23.75f && CurrHour <= 23.9f || StartGame && noGHeld_sw && CurrHour >= 23.75f && CurrHour <= 23.9f)
                {
                    speed = 0.0f;
                    G_sw = true;
                    noGHeld_sw = false;
                }
                else if (!StartGame && !G_sw || StartGame && noGHeld_sw)
                {
                    speed = speedup;
                    G_sw = true;
                }
                else if (StartGame && !G_sw && !noG_Rsw)
                {
                    speed = 1.0f;
                    G_sw = true;
                }

                M_sw = false;
                H_sw = false;
                S_sw = false;
            }

            if (Meditate) //Sunset
            {
                CurrMonth = 5;
                CurrDay = 5;

                if (!StartGame && CurrHour <= 20.2f && CurrHour >= 20.0f || StartGame && m_Rsw && CurrHour <= 20.2f && CurrHour >= 20.0f)
                {
                    speed = 0.0f;
                    M_sw = true;
                    m_Rsw = false;
                }
                else if (!StartGame && !M_sw || StartGame && m_Rsw)
                {
                    speed = speedup;
                    M_sw = true;
                }
                else if (StartGame && !M_sw && !m_Rsw)
                {
                    speed = 1.0f;
                    M_sw = true;
                }

                G_sw = false;
                H_sw = false;
                S_sw = false;
            }

            if (Happy) //Midday Sun
            {
                CurrMonth = 6;
                CurrDay = 20;

                if (!StartGame && CurrHour > 13.4f && CurrHour < 13.6f || StartGame && h_Rsw && CurrHour > 13.4f && CurrHour < 13.6f)
                {
                    speed = 0.0f;
                    H_sw = true;
                    h_Rsw = false;
                }
                else if (!StartGame && !H_sw || StartGame && h_Rsw)
                {
                    speed = speedup;
                    H_sw = true;
                }
                else if (StartGame && !H_sw && !h_Rsw)
                {
                    speed = 1.0f;
                    H_sw = true;
                }

                G_sw = false;
                M_sw = false;
                S_sw = false;
            }

            if (Sad) //Winter Night
            {
                CurrMonth = 1;
                CurrDay = 1;

                if (!StartGame && CurrHour > 8.95f && CurrHour < 9.0f || StartGame && s_Rsw && CurrHour > 8.95f && CurrHour < 9.0f)
                {
                    speed = 0.0f;
                    S_sw = true;
                    s_Rsw = false;
                }
                else if (!StartGame && !S_sw || StartGame && s_Rsw)
                {
                    speed = speedup;
                    S_sw = true;
                }
                else if (StartGame && !S_sw && !s_Rsw)
                {
                    speed = 1.0f;
                    S_sw = true;
                }

                G_sw = false;
                M_sw = false;
                H_sw = false;
            }
        }*/
    }
   
}