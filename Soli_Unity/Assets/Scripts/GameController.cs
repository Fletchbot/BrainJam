using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniOSC;

namespace SoliGameController
{
    public class GameController : MonoBehaviour
    {
        GestureController gestureController;
        TrainingStates ts;

        [Header("Wekinator Run Dispatcher")]
        public GameObject WekMeditateDTW_Run, WekFocusDTW_Run, WekEmotionDTW_Run, wekEmotionSVM_Run;
        [Header("Mode")]
        public bool Muse, Standalone;
        private bool isWekRun;
        [Header("Meters")]
        public GameObject MeditateMeter, FocusMeter, EmotionMeter;
        public float prevT_Focus, currT_Focus, mean_Focus, prevT_Meditate, currT_Meditate, mean_Meditate;
        private Vector3 focusGamePos, meditateGamePos, emotionsGamePos, TrainingPos;
        [Header("GameStates")]
        public bool NoGesture, Meditate, Focus, Happy, Sad, Unsure;
        public bool MeditationTested, HappinessTested, SadnessTested, FocusTested;
        private bool M_sw, H_sw, S_sw, U_sw, F_sw;
        [Header("HeldStates")]
        public bool mindStateTimeOut, heldTimeout, noGHeld_Reached, mHeld_Reached, hHeld_Reached, sHeld_Reached, uHeld_Reached;
        public float  m_HeldScore, h_HeldScore, s_HeldScore, u_HeldScore, noG_HeldScore;
        private float m_Held, h_Held, s_Held, noG_Held, u_Held, HeldPercentage;
        [Header("Timer Section")]
        public float noGestureCountdown, heldCountdown, gestureThresholdTimer, standaloneCountdown, standaloneCounter, focusSkipCountdown;
        public int state;
        public float sixtysecCounter, thirtysecCounter, tensecCounter, fivesecCounter, foursecCounter, threesecCounter, twosecCounter, secCounter;

        // Use this for initialization
        public void OnEnable()
        {
            ts = this.GetComponent<TrainingStates>();
            gestureController = this.GetComponent<GestureController>();

            counterOnEnable();
            MetersOnEnable();

            state = 0;

            TrainingPos = new Vector3(-500,-350,0);
            focusGamePos = new Vector3(-60,-60,0);
            meditateGamePos = new Vector3(-240,-60,0);
            emotionsGamePos = new Vector3(-150,-60,0);

            if (Standalone)
            {
                NoG_Enable(); //VolcanoErupt
                standaloneCountdown = 45.0f;
                standaloneCounter = standaloneCountdown;
            }
            else
            {
                isWekRun = true;
            }
        }
        // Update is called once per frame
        public void Update()
        {
            if (Standalone)
            {

            }
            else if (Muse)
            {
                WekRun();
            }
            //Training
            if (state >= 0)
            {
                ts.MeditateTraining();
                ts.EmotionsTraining();
                ts.FocusTraining();
                GestureDifficultyUpdate();
            }
            //Game
            GestureStates();
            //Meters/HoldGesture
            MetersUpdate();
            HeldState();
        }

        public void MuseMode(bool muse)
        {
            if (muse)
            {
                Muse = true;
                Standalone = false;
            }
            else if (!muse)
            {
                Muse = false;
            }
        }

        public void GestureStates() //state 0 = narratorMTraining, state 1 = meditatetraining complete, state 2 = NarratoremoTraining state 3 = Happy/Sad training complete, state 4 = NarratorfTraining state 5 = focustraining complete
        {
            if (state == 1 && gestureController.isMeditate && !MeditationTested || state == -1 && gestureController.isMeditate && !M_sw && !heldTimeout)
            {
                if (!MeditationTested)
                {
                    noGestureCountdown = sixtysecCounter;
                    MeditationTested = true;
                    state++;
                    Debug.Log("MeditatePassed");
                }
                else
                {
                    noGestureCountdown = thirtysecCounter;
                }

                M_Enable();
                m_Held = HeldPercentage;

                Debug.Log("MeditateState");

                M_sw = true;
                H_sw = false;
                S_sw = false;
                U_sw = false;

                noGHeld_Reached = false;
                hHeld_Reached = false;
                sHeld_Reached = false;
                uHeld_Reached = false;
            }
                       
            //Happy
             if (state == 3 && !HappinessTested && gestureController.isHappy && !heldTimeout || state == -1 && gestureController.isHappy && !H_sw && !heldTimeout)
            {
                if (!HappinessTested)
                {
                    noGestureCountdown = sixtysecCounter;
                    HappinessTested = true;
                    if(SadnessTested) state++;
                    Debug.Log("HappyPassed");
                }
                else
                {
                    noGestureCountdown = thirtysecCounter;
                }

                H_Enable();

                heldCountdown = foursecCounter;
                h_Held = HeldPercentage;
                mindStateTimeOut = true;
                heldTimeout = true;

                Debug.Log("HappyState");

                M_sw = false;
                H_sw = true;
                S_sw = false;
                U_sw = false;

                noGHeld_Reached = false;
                mHeld_Reached = false;
                sHeld_Reached = false;
                uHeld_Reached = false;
            }
            //Sad
            if (state == 3 && !SadnessTested && gestureController.isSad && !heldTimeout || state == -1 && gestureController.isSad && !S_sw && !heldTimeout)
            {
                if (!SadnessTested)
                {
                    noGestureCountdown = sixtysecCounter;
                    SadnessTested = true;
                    if(HappinessTested) state++;
                    Debug.Log("SadPassed");
                }
                else
                {
                    noGestureCountdown = thirtysecCounter;
                }

                S_Enable();

                heldCountdown = threesecCounter;
                s_Held = HeldPercentage;
                mindStateTimeOut = true;
                heldTimeout = true;

                Debug.Log("SadState");

                M_sw = false;
                H_sw = false;
                S_sw = true;
                U_sw = false;

                noGHeld_Reached = false;
                mHeld_Reached = false;
                hHeld_Reached = false;
                uHeld_Reached = false;
            }

            //Unsure
            if (state == -1 && gestureController.isUnsure && !U_sw && !heldTimeout)
            {
                U_Enable();

                heldCountdown = threesecCounter;
                u_Held = HeldPercentage;
                mindStateTimeOut = true;
                heldTimeout = true;

                Debug.Log("UnsureState");

                M_sw = false;
                H_sw = false;
                S_sw = false;
                U_sw = true;

                noGHeld_Reached = false;
                mHeld_Reached = false;
                hHeld_Reached = false;
                sHeld_Reached = false;
            }

            if (state == 5 && gestureController.isFocus && !FocusTested || state == -1 && gestureController.isFocus && !F_sw)
            {
                if (!FocusTested && gestureController.wek_fFloat >= gestureController.fOut)
                {
                    FocusTested = true;
                    state = -1;
                    Debug.Log("FocusPassed");
                }

                Focus = true;
                F_sw = true;
            }
            else
            {
                F_sw = false;
                Focus = false;
            }

            //No Gesture TimeOut
            if (mindStateTimeOut) // no state change within 30sec/test60sec goto no gesture(lava erupting)
            {
                noGestureCountdown -= Time.deltaTime;

                if (noGestureCountdown <= 0)
                {
                    NoG_Enable();

                    heldCountdown = threesecCounter;
                    noG_Held = HeldPercentage;
                    heldTimeout = true;

                    mindStateTimeOut = false;
                }
            }
        }

        public void GestureDifficultyUpdate()
        {
            // Meditate calibrate difficulty
            if (state == 0 && gestureThresholdTimer <= 0.0f && !ts.m_trainSW)
            {
                currT_Meditate = gestureController.wek_mFloat;
                mean_Meditate = (prevT_Meditate + currT_Meditate) / 2;

                if (mean_Meditate >= 9.1f)
                {
                    gestureController.mTarget = 8.0f;
                    gestureController.mOut = 9.0f;
                }
                else if (mean_Meditate >= 8.1f && mean_Meditate <= 9.0f)
                {
                    gestureController.mTarget = 7.5f;
                    gestureController.mOut = 9.0f;
                }
                else if (mean_Meditate >= 7.1f && mean_Meditate <= 8.0f)
                {
                    gestureController.mTarget = 6.5f;
                    gestureController.mOut = 8.5f;
                }
                else if (mean_Meditate >= 6.1f && mean_Meditate <= 7.0f)
                {
                    gestureController.mTarget = 5.5f;
                    gestureController.mOut = 7.5f;
                }
                else if (mean_Meditate >= 5.0f && mean_Meditate <= 6.0f)
                {
                    gestureController.mTarget = 4.5f;
                    gestureController.mOut = 7.0f;
                }

                MeditateMeter.GetComponent<PieMeter>().MinValuec1 = gestureController.mOut;
                MeditateMeter.GetComponent<PieMeter>().MinValuec2 = gestureController.mOut;
                MeditateMeter.GetComponent<PieMeter>().MaxValuec1 = gestureController.mTarget;
                MeditateMeter.GetComponent<PieMeter>().MaxValuec2 = gestureController.mTarget;

                gestureThresholdTimer = tensecCounter;
                ts.m_trainSW = true;
            }
            else if (state == 0)
            {
                if (gestureThresholdTimer == 10.0f)
                {
                    prevT_Meditate = gestureController.wek_mFloat;
                }

                gestureThresholdTimer -= Time.deltaTime;
            }
            //Focus calibrate difficulty
            if (state == 3 && gestureThresholdTimer <= 0.0f && !ts.f_trainSW)
            {
                currT_Focus = gestureController.wek_fFloat;
                mean_Focus = (prevT_Focus + currT_Focus) / 2;

                if (mean_Focus >= 10.5f)
                {
                    gestureController.fTarget = 9.0f;
                    gestureController.fOut = 10.0f;
                }
                else if (mean_Focus >= 9.5f && mean_Focus <= 10.4f)
                {
                    gestureController.fTarget = 8.0f;
                    gestureController.fOut = 9.0f;
                }
                else if (mean_Focus >= 7.5f && mean_Focus <= 9.4f)
                {
                    gestureController.fTarget = 4.5f;
                    gestureController.fOut = 5.5f;
                }
                else if (mean_Focus >= 5.5f && mean_Focus <= 7.4f)
                {
                    gestureController.fTarget = 3.5f;
                    gestureController.fOut = 4.5f;
                }
                else if (mean_Focus >= 4.0f && mean_Focus <= 5.4f)
                {
                    gestureController.fTarget = 2.5f;
                    gestureController.fOut = 3.5f;
                }
                FocusMeter.GetComponent<PieMeter>().MinValuec1 = gestureController.fOut;
                FocusMeter.GetComponent<PieMeter>().MinValuec2 = gestureController.fOut;
                FocusMeter.GetComponent<PieMeter>().MaxValuec1 = gestureController.fTarget;
                FocusMeter.GetComponent<PieMeter>().MaxValuec2 = gestureController.fTarget;

                gestureThresholdTimer = tensecCounter;
                ts.f_trainSW = true;
            }
            else if (state == 3)
            {
                if (gestureThresholdTimer == 10.0f)
                {
                    prevT_Focus = gestureController.wek_fFloat;
                }
                gestureThresholdTimer -= Time.deltaTime;
            }
        }

        private void MetersUpdate()
        {
            MeditateMeter.GetComponent<PieMeter>().Valuec1 = gestureController.wek_mFloat;
            MeditateMeter.GetComponent<PieMeter>().Valuec2 = gestureController.wek_mFloat;

            FocusMeter.GetComponent<PieMeter>().Valuec1 = gestureController.wek_fFloat;
            FocusMeter.GetComponent<PieMeter>().Valuec2 = gestureController.wek_fFloat;

            //Find a way to have unsure...
            EmotionMeter.GetComponent<PieMeter>().Valuec1 = gestureController.h_guiVal;
            EmotionMeter.GetComponent<PieMeter>().Valuec2 = gestureController.s_guiVal;
        }

        public void HeldState()
        {
            if (heldTimeout) //  3 secs in one state
            {
                if (heldCountdown <= 0.0f)
                {
                    if (noG_Held <= 0.0f)
                    {
                        noGHeld_Reached = true;
                        noG_Held = HeldPercentage;
                    }
                    else if (m_Held <= 0.0f)
                    {
                        mHeld_Reached = true;
                        m_Held = HeldPercentage;

                        if (u_Held <= 0.0f)
                        {
                            uHeld_Reached = true;
                            u_Held = HeldPercentage;
                        }
                        if (h_Held <= 0.0f)
                        {
                            hHeld_Reached = true;
                            h_Held = HeldPercentage;
                        }
                        if (s_Held <= 0.0f)
                        {
                            sHeld_Reached = true;
                            s_Held = HeldPercentage;
                        }
                        mindStateTimeOut = false;
                    }
                    else if (h_Held <= 0.0f)
                    {
                        hHeld_Reached = true;
                        h_Held = HeldPercentage;
                        mindStateTimeOut = false;
                    }
                    else if (s_Held <= 0.0f)
                    {
                        sHeld_Reached = true;
                        s_Held = HeldPercentage;
                        mindStateTimeOut = false;
                    }
                    else if (u_Held <= 0.0f)
                    {
                        uHeld_Reached = true;
                        u_Held = HeldPercentage;
                    }
                    else
                    {
                        if (NoGesture)
                        {
                            NoGesture = false;
                        }
                        heldTimeout = false;
                    }

                    heldCountdown = threesecCounter;
                }
                else
                {
                    if (NoGesture && !gestureController.isMeditate && !gestureController.isHappy && !gestureController.isSad)
                    {
                        noG_Held -= Time.deltaTime;
                        noG_HeldScore += Time.deltaTime;
                    }
                    else if (Meditate && gestureController.isMeditate)
                    {
                        m_Held -= Time.deltaTime;
                        m_HeldScore += Time.deltaTime;
                    }

                    if (Happy && gestureController.isHappy)
                    {
                        h_Held -= Time.deltaTime;
                        h_HeldScore += Time.deltaTime;
                    }

                    if (Sad && gestureController.isSad)
                    {
                        s_Held -= Time.deltaTime;
                        s_HeldScore += Time.deltaTime;
                    }

                    if (Unsure && gestureController.isUnsure)
                    {
                        u_Held -= Time.deltaTime;
                        u_HeldScore += Time.deltaTime;
                    }
                }

                heldCountdown -= Time.deltaTime;
            }
        }

        public void StandaloneMode(bool standalone)
        {
            if (standalone)
            {
                Muse = false;
                Standalone = true;
            }
            else if (!standalone)
            {
                Standalone = false;
            }
        }

        public void NoG_Enable()
        {
            NoGesture = true;
            Meditate = false;
            Happy = false;
            Sad = false;
            Unsure = false;
        }
        public void M_Enable()
        {
            NoGesture = false;
            Meditate = true;
        }
        public void H_Enable()
        {
            NoGesture = false;
            if (!gestureController.isMeditate) Meditate = false;
            Happy = true;
            Sad = false;
            Unsure = false;
        }
        public void S_Enable()
        {
            NoGesture = false;
            if (!gestureController.isMeditate) Meditate = false;
            Happy = false;
            Sad = true;
            Unsure = false;
        }
        public void U_Enable()
        {
            NoGesture = false;
            if (!gestureController.isMeditate) Meditate = false;
            Happy = false;
            Sad = false;
            Unsure = true;
        }

        public void TrainingMeterManager()
        {
            if(state == 0) //meditatemeter to middle & scale up
            {
                MeditateMeter.transform.localScale = new Vector3 (2,2,2);
                MeditateMeter.transform.position = new Vector3 (TrainingPos.x, TrainingPos.y, TrainingPos.z);
            }
            else if (state == 2)
            {
                //place meditatemeter to game pos & scale down
                MeditateMeter.transform.localScale = new Vector3(1, 1, 1);
                MeditateMeter.transform.position = new Vector3(meditateGamePos.x, meditateGamePos.y, meditateGamePos.z);

                //place emotionmeter to middle & scale up
                EmotionMeter.transform.localScale = new Vector3(2, 2, 2);
                EmotionMeter.transform.position = new Vector3(TrainingPos.x, TrainingPos.y, TrainingPos.z);
            }
            else if (state == 4)
            {
                //place emotionmeter to game pos & scale down
                EmotionMeter.transform.localScale = new Vector3(1, 1, 1);
                EmotionMeter.transform.position = new Vector3(emotionsGamePos.x, emotionsGamePos.y, emotionsGamePos.z);

                //place focusmeter to middle & scale up
                FocusMeter.transform.localScale = new Vector3(2, 2, 2);
                FocusMeter.transform.position = new Vector3(TrainingPos.x, TrainingPos.y, TrainingPos.z);
            }
            else if (state == 5)
            {
                //place focusmeter to game pos & scale down
                FocusMeter.transform.localScale = new Vector3(1, 1, 1);
                FocusMeter.transform.position = new Vector3(focusGamePos.x, focusGamePos.y, focusGamePos.z);
            }
        }

        private void counterOnEnable()
        {
            sixtysecCounter = 60.0f;
            thirtysecCounter = 30.0f;
            tensecCounter = 10.0f;
            fivesecCounter = 5.0f;
            foursecCounter = 4.0f;
            threesecCounter = 3.0f;
            twosecCounter = 2.0f;
            secCounter = 1.0f;

            heldCountdown = threesecCounter;
            HeldPercentage = 1.7f;
            noG_Held = HeldPercentage;
            m_Held = HeldPercentage;
            h_Held = HeldPercentage;
            s_Held = HeldPercentage;
            u_Held = HeldPercentage;
        }
        private void MetersOnEnable()
        {
            EmotionMeter.GetComponent<PieMeter>().MinValuec1 = 0.0f;
            EmotionMeter.GetComponent<PieMeter>().MinValuec2 = 0.0f;
            EmotionMeter.GetComponent<PieMeter>().MaxValuec1 = 2.5f;
            EmotionMeter.GetComponent<PieMeter>().MaxValuec2 = 2.5f;

            MeditateMeter.GetComponent<PieMeter>().MinValuec1 = gestureController.mOut;
            MeditateMeter.GetComponent<PieMeter>().MinValuec2 = gestureController.mOut;
            MeditateMeter.GetComponent<PieMeter>().MaxValuec1 = gestureController.mTarget;
            MeditateMeter.GetComponent<PieMeter>().MaxValuec2 = gestureController.mTarget;

            FocusMeter.GetComponent<PieMeter>().MinValuec1 = gestureController.fOut;
            FocusMeter.GetComponent<PieMeter>().MinValuec2 = gestureController.fOut;
            FocusMeter.GetComponent<PieMeter>().MaxValuec1 = gestureController.fTarget;
            FocusMeter.GetComponent<PieMeter>().MaxValuec2 = gestureController.fTarget;
        }
        private void WekRun()
        {
            WekMeditateDTW_Run.GetComponent<WekEventDispatcherButton>().ButtonClick(isWekRun);
            WekFocusDTW_Run.GetComponent<WekEventDispatcherButton>().ButtonClick(isWekRun);
            WekEmotionDTW_Run.GetComponent<WekEventDispatcherButton>().ButtonClick(isWekRun);
            wekEmotionSVM_Run.GetComponent<WekEventDispatcherButton>().ButtonClick(isWekRun);
        }

    }
}
