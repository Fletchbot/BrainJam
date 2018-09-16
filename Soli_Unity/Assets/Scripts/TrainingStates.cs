using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SoliGameController
{
    public class TrainingStates : MonoBehaviour
    {
        GameController gc;
        GestureController gestureController;
        AudioPlaytestManager au;

        public float gestureCountdown, f_sustainRestDiff;
        public int focusNotesPlayed;
        public bool gestureOn, h_On, s_On, train_sw, happy, sad, reset, m_On, f_On;

        // Use this for initialization
        public void OnEnable()
        {
            gc = this.GetComponent<GameController>();
            gestureController = this.GetComponent<GestureController>();
            au = this.GetComponent<AudioPlaytestManager>();

            resetValues();
        }

        public void Update()
        {
            if(gc.HeadsetOn == 0 && !reset)
            {
                resetValues();
                reset = true;
            }
            else if (gc.HeadsetOn == 1 && reset)
            {
                reset = false;
            }

           /* if (gc.state == 4 && gc.f_trainSW)
            {
               FocusTrainResetDiff();
            }*/
        }

        private void invokeGestureDifficultyUpdate()
        {
            gc.GestureDifficultyUpdate();
        }

        public void MeditateTraining() //state 0 & 1
        {
            //Intro
            if (gc.state == 0) //INTRO (Lava Erupting)
            {
                if (!train_sw) //Enable eruption, place and scale meditatemeter in middle of screen
                {
                    gc.NoG_Enable();
                    gc.TrainingMeterManager();
                    train_sw = true;
                }

                if(au.N_Intro <= 2)
                {
                    if (gestureController.isMeditate && !m_On)
                    {
                        m_On = true;
                    }
                }
                //after Narrator and meditate lasts 2 secs go to emotions training
                if (au.N_Intro == 2 && gestureCountdown <= 0.0f)
                {
                    train_sw = false;
                    gestureOn = false;
                    gestureCountdown = gc.twosecCounter;
                    gc.state++;
                }
                //after Narrator and ismeditate countdown for 2 secs with text
                else if (au.N_Intro == 2 && gestureController.isMeditate)
                {
                    if (!gestureOn)
                    {
                        gestureOn = true;
                    }

                    gestureCountdown -= Time.deltaTime;
                }
                else
                {
                    gestureOn = false;
                    gestureCountdown = gc.twosecCounter;
                }

                if (au.N_Intro == 2 && !m_On)
                {
                    Invoke("invokeGestureDifficultyUpdate", 2.0f);
                }
            }
        }
        public void EmotionsTraining() //state 2 & 3 
        {
            if (gc.state == 2) //EMOTION TRAINING INTRO
            {
                if (!train_sw) // place/scale meditate to gamepos and place and scale emotionsmeter in middle of screen
                {
                    gc.TrainingMeterManager();
                    train_sw = true;
                }

                // after Narrator and emotions lasts 2 secs go to focus training
                if (au.N_Intro == 4 && gestureCountdown <= 0.0f || au.N_Intro == 6 && gestureCountdown <= 0.0f)
                {
                    if (h_On) happy = true;
                    if (s_On) sad = true;
                    if (happy && sad)
                    {
                        train_sw = false;
                        gc.state++;
                    }
                    gestureCountdown = gc.twosecCounter;
                }
                // after Narrator and ishappy/issad countdown for 2 secs with text
                else if (au.N_Intro == 4 && gestureController.isHappy || au.N_Intro == 6 && gestureController.isSad)
                {
                    if (gestureController.isHappy && !h_On && !happy)
                    {
                        h_On = true;
                    }
                    else if (gestureController.isSad && !s_On && happy)
                    {
                        s_On = true;
                    }
                    gestureCountdown -= Time.deltaTime;
                }
                else
                {
                    h_On = false;
                    s_On = false;
                    gestureCountdown = gc.twosecCounter;
                }
            }
        }
        public void FocusTraining() //state 4 & 5
        {
            if (gc.state == 4) //FOCUS TRAINING INTRO
            {
                if (!train_sw) // place/scale emotions to gamepos and place and scale focusmeter in middle of screen
                {
                    gc.TrainingMeterManager();
                    train_sw = true;
                }

                if (au.N_Intro >= 7)
                {
                    if (gestureController.isFocus && !f_On)
                    {
                        f_On = true;
                    }
                }
                // after Narrator and focus plays 3 notes go to game
                if (au.N_Intro == 8 && focusNotesPlayed == 3)
                {
                    train_sw = false;
                    gestureOn = false;
                    gc.state++;
                }
                // after Narrator and isfocus plays note with text
                if (au.N_Intro == 8 && gestureController.isFocus)
                {
                    if (!gestureOn)
                    {
                        focusNotesPlayed++;
                        gestureOn = true;
                    }
                }
                // if !isfocus deactivate timertext
                else if (au.N_Intro == 8 && !gestureController.isFocus)
                {
                    if (gestureOn)
                    {
                        gestureOn = false;
                    }
                }

                if (au.N_Intro == 8 && !f_On)
                {
                    Invoke("invokeGestureDifficultyUpdate", 1.0f);
                }
            }
            else if (gc.state == 5)
            {
                if (!train_sw) // place/scale focus to gamepos
                {
                    gc.TrainingMeterManager();
                    train_sw = true;
                }

                if(au.N_Intro == 9)
                {
                    gc.state = -1;
                }
            }
        }

   /*     public void FocusTrainResetDiff()
        {
            if (gestureController.isFocus)
            {
                if (f_sustainRestDiff <= 0.0f)
                {
                    gestureController.fTarget -= 0.5f;
                    gestureController.fOut -= 0.5f;
                    f_sustainRestDiff = 7.0f;
                }
                else
                {
                    f_sustainRestDiff -= Time.deltaTime;
                }
            }
        }*/

        public void resetValues()
        {
            gestureCountdown = gc.twosecCounter;

            focusNotesPlayed = 0;
            f_sustainRestDiff = 7.0f;

            train_sw = false;
            gestureOn = false;
            h_On = false;
            s_On = false;
            happy = false;
            sad = false;
        }
    }
}
