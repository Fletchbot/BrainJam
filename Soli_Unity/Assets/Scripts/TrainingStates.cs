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

        [Header("Timer Section")]
        public GameObject timerText;
        Text text;
        public float gestureThresholdTimer, gestureCountdown;
        public int focusNotesPlayed;
        public bool gestureOn, h_On, s_On, train_sw, f_trainSW, m_trainSW;

        // Use this for initialization
        public void OnEnable()
        {
            gc = this.GetComponent<GameController>();
            gestureController = this.GetComponent<GestureController>();
            au = this.GetComponent<AudioPlaytestManager>();

            gestureThresholdTimer = gc.tensecCounter;
            gestureCountdown = gc.threesecCounter;
            text = timerText.GetComponent<Text>();

            focusNotesPlayed = 0;
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

                if (au.N_Intro == 1 && gestureCountdown <= 0.0f || au.N_Intro == 2 && gestureCountdown <= 0.0f) // During/after Narrator and meditate lasts 3 secs go to emotions training
                {
                    train_sw = false;
                    gestureOn = false;
                    timerText.GetComponent<ActivateObjects>().SetDeactive(true);
                    gc.state++;
                    gestureCountdown = gc.threesecCounter;
                }
                else if (au.N_Intro == 1 && gestureController.isMeditate || au.N_Intro == 2 && gestureController.isMeditate) // During/after Narrator and ismeditate countdown for 3 secs with text
                {
                    if (!gestureOn)
                    {
                        timerText.GetComponent<ActivateObjects>().SetActive(true);
                        gestureOn = true;
                    }

                    string seconds = (gestureCountdown % 60).ToString("0");
                    text.text = seconds;
                    gestureCountdown -= Time.deltaTime;
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

                    // During/after Narrator and emotions lasts 3 secs go to focus training
                    if (au.N_Intro == 3 && gestureCountdown <= 0.0f && h_On && s_On || au.N_Intro == 4 && gestureCountdown <= 0.0f && h_On && s_On) 
                    {
                        train_sw = false;
                        timerText.GetComponent<ActivateObjects>().SetDeactive(true);
                        gc.state++;
                        gestureCountdown = gc.threesecCounter;
                    }
                    // During/after Narrator and ismeditate countdown for 3 secs with text
                    else if (au.N_Intro == 3 && gestureController.isHappy || au.N_Intro == 3 && gestureController.isSad || au.N_Intro == 4 && gestureController.isHappy || au.N_Intro == 4 && gestureController.isSad) 
                    {
                        if (gestureController.isHappy && !h_On)
                        {
                            timerText.GetComponent<ActivateObjects>().SetActive(true);
                            h_On = true;
                        }
                        else if (gestureController.isSad && !s_On)
                        {
                            timerText.GetComponent<ActivateObjects>().SetActive(true);
                            s_On = true;
                        }

                        string seconds = (gestureCountdown % 60).ToString("0");
                        text.text = seconds;
                        gestureCountdown -= Time.deltaTime;
                    }
                    else
                    {
                        gestureCountdown = gc.threesecCounter;
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

                // During/after Narrator and focus plays 3 notes go to game
                if (au.N_Intro == 5 && focusNotesPlayed == 3 || au.N_Intro == 6 && focusNotesPlayed == 3) 
                {
                    train_sw = false;
                    gestureOn = false;
                    timerText.GetComponent<ActivateObjects>().SetDeactive(true);
                    gc.state++;
                }
                // During/after Narrator and isfocus plays note with text
                else if (au.N_Intro == 1 && gestureController.isFocus || au.N_Intro == 2 && gestureController.isFocus)
                {
                    if (!gestureOn)
                    {
                        timerText.GetComponent<ActivateObjects>().SetActive(true);
                        focusNotesPlayed++;
                        gestureOn = true;
                    }

                    string notesplayed = focusNotesPlayed.ToString("0");
                    text.text = notesplayed;
                }
                else if (au.N_Intro == 1 && !gestureController.isFocus || au.N_Intro == 2 && !gestureController.isFocus)
                {
                    if (gestureOn)
                    {
                        gestureOn = false;
                        timerText.GetComponent<ActivateObjects>().SetDeactive(true);
                    }
                }
            }
            else if (gc.state == 5)
            {
                if (!train_sw) // place/scale focus to gamepos
                {
                    gc.TrainingMeterManager();
                    train_sw = true;
                }
            }
        }
    }
}
