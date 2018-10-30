using UnityEngine;
using System.Collections;
//using Artngame.SKYMASTER;

namespace Artngame.SKYMASTER
{
    public class CalenderManager : MonoBehaviour
    {

        SkyMasterManager skyManager;
        WeatherManager wm;
        public GameObject scoreManager;

        public int CurrMonth;
        public int CurrDay;
        public float CurrHour;
        public float speed;
        public float speedUp, acc, maxAcc, highAcc, lowAcc;
        private float normSpeed;

        private bool NoGesture, Meditate, Happy, Sad, Unsure;
        private bool G_sw, M_sw, H_sw, S_sw, U_sw;
        private bool noGHeldOff, noGHeld_Rsw, mHeldOff, mHeld_Rsw, hHeldOff, hHeld_Rsw, sHeldOff, sHeld_Rsw;
        private bool noGHeld_Reached, mHeld_Reached, hHeld_Reached, sHeld_Reached, uHeld_Reached;
        private float h_ScorePercent, s_ScorePercent, u_ScorePercent, noG_ScorePercent, percentMax, percentMin;
        private int state;
 
        // Use this for initialization
        void Start()
        {
            skyManager = this.GetComponent<SkyMasterManager>();
            wm = this.GetComponent<WeatherManager>();
            normSpeed = 1.0f;
            maxAcc = 64.0f;
            acc = normSpeed;
            highAcc = 6.0f;
            lowAcc = 3.0f;
            percentMax = 30.0f;
            percentMin = 15.0f;

        }

        // Update is called once per frame
        void Update()
        {
            state = wm.state;

            NoGesture = wm.NoGesture;
            Meditate = wm.Meditate;
            Happy = wm.Happy;
            Sad = wm.Sad;
            Unsure = wm.Unsure;

            noGHeld_Reached = wm.noGHeld_Reached;
            mHeld_Reached = wm.mHeld_Reached;
            hHeld_Reached = wm.hHeld_Reached;
            sHeld_Reached = wm.sHeld_Reached;
            uHeld_Reached = wm.uHeld_Reached;

            h_ScorePercent = scoreManager.GetComponent<ScoreManager>().h_ScorePercent;
            s_ScorePercent = scoreManager.GetComponent<ScoreManager>().s_ScorePercent;
            u_ScorePercent = scoreManager.GetComponent<ScoreManager>().u_ScorePercent;
            noG_ScorePercent = scoreManager.GetComponent<ScoreManager>().noG_ScorePercent;
            
            skyManager.Current_Month = CurrMonth;
            skyManager.Current_Day = CurrDay;
            CurrHour = skyManager.Current_Time;
            skyManager.SPEED = speed;

            HeldReachedSW();
          if(state == -1) SpeedUpController();
            StateSwitch();
        }

        void HeldReachedSW()
        {
            if (noGHeld_Reached && !noGHeldOff)
            {
                noGHeld_Rsw = true;

                noGHeldOff = true;
            }
            if (mHeld_Reached && !mHeldOff)
            {
                mHeld_Rsw = true;

                mHeldOff = true;
            }
            if (hHeld_Reached && !hHeldOff && !Meditate)
            {
                hHeld_Rsw = true;

                hHeldOff = true;
            }
            if (sHeld_Reached && !sHeldOff && !Meditate)
            {
                sHeld_Rsw = true;

                sHeldOff = true;
            }

        }

        void SpeedUpController()
        {
            if(acc >= normSpeed && acc <= maxAcc)
            {
                if (NoGesture && noGHeld_Rsw)
                {
                    if(noG_ScorePercent >= percentMax)
                    {
                        acc = acc + highAcc;
                    }
                    else if (noG_ScorePercent >= percentMin && noG_ScorePercent <= (percentMax - 0.1f))
                    {
                        acc = acc + lowAcc;
                    }
                    else
                    {
                        acc = normSpeed * highAcc;
                    }
                }

                if (Happy && mHeld_Rsw || Happy && hHeld_Rsw)
                {
                    if (h_ScorePercent >= percentMax)
                    {
                        acc = acc + highAcc;
                    }
                    else if (h_ScorePercent >= percentMin && h_ScorePercent <= (percentMax - 0.1f))
                    {
                        acc = acc + lowAcc;
                    }
                    else
                    {
                        acc = normSpeed * highAcc;
                    }
                }

                if (Sad && mHeld_Rsw || Sad && sHeld_Rsw)
                {
                    if (s_ScorePercent >= percentMax)
                    {
                        acc = acc + maxAcc;
                    }
                    else if (s_ScorePercent >= percentMin && s_ScorePercent <= (percentMax - 0.1f))
                    {
                        acc = acc + lowAcc;
                    }
                    else
                    {
                        acc = normSpeed * highAcc;
                    }
                }

                if (Unsure && mHeld_Rsw)
                {
                    if (u_ScorePercent >= percentMax)
                    {
                        acc = acc + highAcc;
                    }
                    else if (u_ScorePercent >= percentMin && u_ScorePercent <= (percentMax - 0.1f))
                    {
                        acc = acc + lowAcc;
                    }
                    else
                    {
                        acc = normSpeed * highAcc;
                    }
                }               
            }

            speedUp = normSpeed * acc;
        }

        void StateSwitch()
        {
            if(state >= 0) speedUp = 16 * 5.0f;

            if (NoGesture) //Volcano Erupt Night
            {
                CurrMonth = 10;
                CurrDay = 10;

                if (state == 0 && CurrHour >= 23.75f && CurrHour <= 23.9f || state == -1 && noGHeld_Rsw && CurrHour >= 23.75f && CurrHour <= 23.9f)
                {
                    speed = 0.0f;
                    noGHeld_Rsw = false;
                }
                else if (state == 0 && !G_sw || state == -1 && noGHeld_Rsw && G_sw)
                {
                    speed = speedUp;

                    if (state == -1)
                    {
                        G_sw = false;
                    }
                    else if (state >= 0)
                    {
                        G_sw = true;
                    }
                }
                else if (state == -1 && !G_sw && !noGHeld_Rsw)
                {
                    speed = normSpeed * highAcc;
                    G_sw = true;
                }

                M_sw = false;
                H_sw = false;
                S_sw = false;
            }
            else if (Meditate && state == -1|| Meditate && state == 2 && wm.n_Intro <= 4 && !wm.Sad) //Sunset
            {
                CurrMonth = 5;
                CurrDay = 5;

                if (state ==2 && CurrHour <= 20.2f && CurrHour >= 20.0f || state == -1 && mHeld_Rsw && CurrHour <= 20.2f && CurrHour >= 20.0f)
                {
                    speed = 0.0f;
                    mHeld_Rsw = false;
                }
                else if (state ==2 && !M_sw || state == -1 && mHeld_Rsw && M_sw)
                {
                    speed = speedUp;

                    if (state == -1)
                    {
                        M_sw = false;
                    }
                    else if (state == 2)
                    {
                        M_sw = true;
                    }                   
                }
                else if (state == -1 && !M_sw && !mHeld_Rsw)
                {
                    speed = normSpeed * highAcc;
                    M_sw = true;
                }

                G_sw = false;
                H_sw = false;
                S_sw = false;
            }
            else if (Happy && !Meditate || Happy && state == 2 && wm.HappinessTested) //Midday Sun
            {
                CurrMonth = 6;
                CurrDay = 20;

                if (state == 2 && CurrHour > 13.4f && CurrHour < 13.6f || state == -1 && hHeld_Rsw && CurrHour > 13.4f && CurrHour < 13.6f)
                {
                    speed = 0.0f;
                    hHeld_Rsw = false;
                }
                else if (state == 2 && !H_sw || state == -1 && hHeld_Rsw && H_sw)
                {
                    speed = speedUp;

                    if (state == -1)
                    {
                        H_sw = false;
                    } 
                    else if (state == 2)
                    {
                        H_sw = true;
                    }
                }
                else if (state == -1 && !H_sw && !hHeld_Rsw)
                {
                    speed = normSpeed * highAcc;
                    H_sw = true;
                }

                G_sw = false;
                M_sw = false;
                S_sw = false;
            }
            else if (Sad && !Meditate || Sad && state >= 3) //Early Morning
            {
                CurrMonth = 1;
                CurrDay = 1;

                if (state >= 3 && CurrHour > 8.95f && CurrHour < 9.0f || state == -1 && sHeld_Rsw && CurrHour > 8.95f && CurrHour < 9.0f)
                {
                    speed = 0.0f;
                    sHeld_Rsw = false;
                }
                else if (state >= 3 && !S_sw || state == -1 && sHeld_Rsw && S_sw)
                {
                    speed = speedUp;

                    if (state == -1)
                    {
                        S_sw = false;
                    }
                    else if (state >= 3)
                    {
                        S_sw = true;
                    }
                }
                else if (state == -1 && !S_sw && !sHeld_Rsw)
                {
                    speed = normSpeed * highAcc;
                    S_sw = true;
                }

                G_sw = false;
                M_sw = false;
                H_sw = false;
            }
            else
            {
                speed = normSpeed * highAcc;
            }
        }
    }
   
}