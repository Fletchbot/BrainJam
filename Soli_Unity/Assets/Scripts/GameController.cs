using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniOSC;

namespace SoliGameController
{
    public class GameController : MonoBehaviour
    {
        GestureController gest_c;
        TrainingStates ts;

        [Header("Wekinator Run Dispatcher")]
        public GameObject WekMeditateDTW_Run, WekFocusDTW_Run, WekEmotionDTW_Run, wekEmotionSVM_Run;
        public GameObject MuseMonitor;
        [Header("Mode")]
        public bool isWekRun, f_trainSW, m_trainSW;
        public int HeadsetOn;
        [Header("Meters")]
        public GameObject MeditateMeter, FocusMeter, EmotionMeter;
        public float prevT_Focus, currT_Focus, mean_Focus, prevT_Meditate, currT_Meditate, mean_Meditate;
        private Vector3 focusGamePos, meditateGamePos, emotionsGamePos;
        private Vector2 middleAnchorMin, middleAnchorMax, topRightAnchorMin, topRightAnchorMax;
        [Header("GameStates")]
        public bool NoGesture, Meditate, Focus, Happy, Sad, Unsure;
        public bool MeditationTested, HappinessTested, SadnessTested, FocusTested;
        private bool M_sw, H_sw, S_sw, U_sw, F_sw;
        [Header("HeldStates")]
        public bool mindStateTimeOut, heldTimeout, noGHeld_Reached, mHeld_Reached, hHeld_Reached, sHeld_Reached, uHeld_Reached;
        public float  m_HeldScore, h_HeldScore, s_HeldScore, u_HeldScore, noG_HeldScore;
        private float m_Held, h_Held, s_Held, noG_Held, u_Held, HeldPercentage;
        [Header("Timer Section")]
        public float noGestureCountdown, heldCountdown, gestureThresholdTimer;
        public int state;
        public float sixtysecCounter, thirtysecCounter, tensecCounter, fivesecCounter, foursecCounter, threesecCounter, twosecCounter, secCounter;

        // Use this for initialization
        public void OnEnable()
        {
            ts = this.GetComponent<TrainingStates>();
            gest_c = this.GetComponent<GestureController>();

            focusGamePos = new Vector3(-60, -60, 0);
            meditateGamePos = new Vector3(-240, -60, 0);
            emotionsGamePos = new Vector3(-150, -60, 0);

            middleAnchorMin = new Vector2(0.5f, 0.5f);
            middleAnchorMax = new Vector2(0.5f, 0.5f);

            topRightAnchorMin = new Vector2(1f, 1f);
            topRightAnchorMax = new Vector2(1f, 1f);

            sixtysecCounter = 60.0f;
            thirtysecCounter = 30.0f;
            tensecCounter = 10.0f;
            fivesecCounter = 5.0f;
            foursecCounter = 4.0f;
            threesecCounter = 3.0f;
            twosecCounter = 2.0f;
            secCounter = 1.0f;

            HeadsetOn = MuseMonitor.GetComponent<UniOSCMuseMonitor>().touchingforehead;

            ResetValues();
            MetersOnEnable();

            NoG_Enable();

            if (HeadsetOn == 1)
            {
                isWekRun = true;
            }

        }
        // Update is called once per frame
        public void Update()
        {
            HeadsetOn = MuseMonitor.GetComponent<UniOSCMuseMonitor>().touchingforehead;

            //Training
            if (state >= 0 && HeadsetOn == 1)
            {
                ts.MeditateTraining();
                ts.EmotionsTraining();
                ts.FocusTraining();
            }

            if(HeadsetOn == 1)
            {
                if (!isWekRun) isWekRun = true;
                //Game
                WekRun();
                GestureStates();

                //Meters/HoldGesture
                MetersUpdate();
                HeldState();
            } 
            else if (HeadsetOn == 0 && isWekRun)
            {
                ResetGame();
            }

        }

        public void ResetGame()
        {
            ResetValues();
            MetersOnEnable();

            MeditateMeter.GetComponent<ActivateObjects>().SetDeactive(true);
            EmotionMeter.GetComponent<ActivateObjects>().SetDeactive(true);
            FocusMeter.GetComponent<ActivateObjects>().SetDeactive(true);

            NoG_Enable();

            if (isWekRun)
            {
                isWekRun = false;
                WekRun();
            }

        }

        public void GestureStates() //state 0 = narratorMTraining, state 1 = meditatetraining complete, state 2 = NarratoremoTraining state 3 = Happy/Sad training complete, state 4 = NarratorfTraining state 5 = focustraining complete
        {

            if (state == 1 && gest_c.isMeditate && !MeditationTested || state == -1 && gest_c.isMeditate && !M_sw && !heldTimeout)
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
            }
                       
            //Happy
             if (state == 2 && !HappinessTested && gest_c.isHappy && !heldTimeout && ts.happy|| state == -1 && gest_c.isHappy && !H_sw && !heldTimeout)
            {
                if (!HappinessTested)
                {
                    noGestureCountdown = sixtysecCounter;
                    HappinessTested = true;
                    if (SadnessTested) state++;
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
              if(!gest_c.isMeditate)  mHeld_Reached = false;
                sHeld_Reached = false;
                uHeld_Reached = false;
            }
            //Sad
            if (state == 3 && !SadnessTested && gest_c.isSad && !heldTimeout && !heldTimeout || state == -1 && gest_c.isSad && !S_sw && !heldTimeout)
            {
                if (!SadnessTested)
                {
                    noGestureCountdown = sixtysecCounter;
                    SadnessTested = true;
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
                if (!gest_c.isMeditate) mHeld_Reached = false;
                hHeld_Reached = false;
                uHeld_Reached = false;
            }

            //Unsure
            if (state == -1 && gest_c.isUnsure && !U_sw && !heldTimeout)
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
                if (!gest_c.isMeditate) mHeld_Reached = false;
                hHeld_Reached = false;
                sHeld_Reached = false;
            }


            //No Gesture TimeOut
            if (mindStateTimeOut && state == -1) // no state change within 30sec/test60sec goto no gesture(lava erupting)
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
            if (state == 0 && gestureThresholdTimer <= 0.0f && !m_trainSW)
            {
                currT_Meditate = gest_c.wek_mFloat;
                mean_Meditate = (prevT_Meditate + currT_Meditate) / 2;

                if (mean_Meditate >= 9.01f)
                {
                    gest_c.mTarget = 8.0f;
                    gest_c.mOut = 9.0f;
                }
                else if (mean_Meditate >= 8.01f && mean_Meditate <= 9.0f)
                {
                    gest_c.mTarget = 7.5f;
                    gest_c.mOut = 9.0f;
                }
                else if (mean_Meditate >= 7.01f && mean_Meditate <= 8.0f)
                {
                    gest_c.mTarget = 6.5f;
                    gest_c.mOut = 8.5f;
                }
                else if (mean_Meditate >= 6.01f && mean_Meditate <= 7.0f)
                {
                    gest_c.mTarget = 6.0f;
                    gest_c.mOut = 7.5f;
                }
                else if (mean_Meditate >= 5.0f && mean_Meditate <= 6.0f)
                {
                    gest_c.mTarget = 5.5f;
                    gest_c.mOut = 7.0f;
                }

                MeditateMeter.GetComponent<PieMeter>().MinValuec1 = gest_c.mOut+2.0f;
                MeditateMeter.GetComponent<PieMeter>().MinValuec2 = gest_c.mOut+2.0f;
                MeditateMeter.GetComponent<PieMeter>().MaxValuec1 = gest_c.mTarget-2.0f;
                MeditateMeter.GetComponent<PieMeter>().MaxValuec2 = gest_c.mTarget-2.0f;

                gestureThresholdTimer = threesecCounter;
                m_trainSW = true;
            }
            else if (state == 0 && !m_trainSW)
            {
                if (gestureThresholdTimer == threesecCounter)
                {
                    prevT_Meditate = gest_c.wek_mFloat;
                }

                gestureThresholdTimer -= Time.deltaTime;
            }
            //Focus calibrate difficulty
            if (state == 4 && gestureThresholdTimer <= 0.0f && !f_trainSW)
            {
                currT_Focus = gest_c.wek_fFloat;
                mean_Focus = (prevT_Focus + currT_Focus) / 2;

                if (mean_Focus >= 7.51f)
                {
                    gest_c.fTarget = 8.0f;
                    gest_c.fOut = 8.5f;
                }
                else if (mean_Focus >= 6.51f && mean_Focus <= 7.5f)
                {
                    gest_c.fTarget = 7.0f;
                    gest_c.fOut = 7.5f;
                }
                else if (mean_Focus >= 5.51f && mean_Focus <= 6.5f)
                {
                    gest_c.fTarget = 6.0f;
                    gest_c.fOut = 6.5f;
                }
                else if (mean_Focus >= 4.51f && mean_Focus <= 5.5f)
                {
                    gest_c.fTarget = 5.0f;
                    gest_c.fOut = 5.5f;
                }
                else if (mean_Focus >= 3.5f && mean_Focus <= 4.5f)
                {
                    gest_c.fTarget = 4.0f;
                    gest_c.fOut = 4.5f;
                }
                FocusMeter.GetComponent<PieMeter>().MinValuec1 = gest_c.fOut;
                FocusMeter.GetComponent<PieMeter>().MinValuec2 = gest_c.fOut;
                FocusMeter.GetComponent<PieMeter>().MaxValuec1 = gest_c.fTarget;
                FocusMeter.GetComponent<PieMeter>().MaxValuec2 = gest_c.fTarget;

                gestureThresholdTimer = threesecCounter;
                f_trainSW = true;
            }
            else if (state == 4)
            {
                if (gestureThresholdTimer == threesecCounter)
                {
                    prevT_Focus = gest_c.wek_fFloat;
                    gestureThresholdTimer = secCounter;
                }
                gestureThresholdTimer -= Time.deltaTime;
            }
        }

        private void MetersUpdate()
        {
            MeditateMeter.GetComponent<PieMeter>().Valuec1 = gest_c.wek_mFloat;
            MeditateMeter.GetComponent<PieMeter>().Valuec2 = gest_c.wek_mFloat;

            FocusMeter.GetComponent<PieMeter>().Valuec1 = gest_c.wek_fFloat;
            FocusMeter.GetComponent<PieMeter>().Valuec2 = gest_c.wek_fFloat;

            //Find a way to have unsure...
            EmotionMeter.GetComponent<PieMeter>().Valuec1 = gest_c.h_guiVal;
            EmotionMeter.GetComponent<PieMeter>().Valuec2 = gest_c.s_guiVal;
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
                        mindStateTimeOut = false;
                    }

                    if (h_Held <= 0.0f)
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
                    if (NoGesture && !gest_c.isMeditate && !gest_c.isHappy && !gest_c.isSad)
                    {
                        noG_Held -= Time.deltaTime;
                        noG_HeldScore += Time.deltaTime;
                    }
                    else if (Meditate && gest_c.isMeditate)
                    {
                        m_Held -= Time.deltaTime;
                        m_HeldScore += Time.deltaTime;
                    }

                    if (Happy && gest_c.isHappy)
                    {
                        h_Held -= Time.deltaTime;
                        h_HeldScore += Time.deltaTime;
                    }
                    else if (Sad && gest_c.isSad)
                    {
                        s_Held -= Time.deltaTime;
                        s_HeldScore += Time.deltaTime;
                    }
                    else if (Unsure && gest_c.isUnsure)
                    {
                        u_Held -= Time.deltaTime;
                        u_HeldScore += Time.deltaTime;
                    }
                }

                heldCountdown -= Time.deltaTime;
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
            if (!gest_c.isMeditate) Meditate = false;
            Happy = true;
            Sad = false;
            Unsure = false;
        }
        public void S_Enable()
        {
            NoGesture = false;
            if (!gest_c.isMeditate) Meditate = false;
            Happy = false;
            Sad = true;
            Unsure = false;
        }
        public void U_Enable()
        {
            NoGesture = false;
            if (!gest_c.isMeditate) Meditate = false;
            Happy = false;
            Sad = false;
            Unsure = true;
        }

        public void TrainingMeterManager()
        {
            if(state == 0) //meditatemeter to middle & scale up
            {
                MeditateMeter.GetComponent<ActivateObjects>().SetActive(true);

                MeditateMeter.transform.localScale = new Vector3 (2,2,2);
                MeditateMeter.GetComponent<RectTransform>().anchorMin = new Vector2(middleAnchorMin.x, middleAnchorMin.y);
                MeditateMeter.GetComponent<RectTransform>().anchorMax = new Vector2(middleAnchorMax.x, middleAnchorMax.y);
                MeditateMeter.GetComponent<RectTransform>().localPosition = new Vector3 (0, 0, 0);
            }
            else if (state == 2)
            {
                EmotionMeter.GetComponent<ActivateObjects>().SetActive(true);
                //place meditatemeter to game pos & scale down
                MeditateMeter.transform.localScale = new Vector3(1, 1, 1);
                MeditateMeter.GetComponent<RectTransform>().localPosition = new Vector3(meditateGamePos.x, meditateGamePos.y, 0);
                MeditateMeter.GetComponent<RectTransform>().anchorMin = new Vector2(topRightAnchorMin.x, topRightAnchorMin.y);
                MeditateMeter.GetComponent<RectTransform>().anchorMax = new Vector2(topRightAnchorMax.x, topRightAnchorMax.y);


                //place emotionmeter to middle & scale up
                EmotionMeter.transform.localScale = new Vector3(2, 2, 2);
                EmotionMeter.GetComponent<RectTransform>().anchorMin = new Vector2(middleAnchorMin.x, middleAnchorMin.y);
                EmotionMeter.GetComponent<RectTransform>().anchorMax = new Vector2(middleAnchorMax.x, middleAnchorMax.y);
                EmotionMeter.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
            }
            else if (state == 4)
            {
                FocusMeter.GetComponent<ActivateObjects>().SetActive(true);
                //place emotionmeter to game pos & scale down
                EmotionMeter.transform.localScale = new Vector3(1, 1, 1);
                EmotionMeter.GetComponent<RectTransform>().localPosition = new Vector3(emotionsGamePos.x, emotionsGamePos.y, 0);
                EmotionMeter.GetComponent<RectTransform>().anchorMin = new Vector2(topRightAnchorMin.x, topRightAnchorMin.y);
                EmotionMeter.GetComponent<RectTransform>().anchorMax = new Vector2(topRightAnchorMax.x, topRightAnchorMax.y);


                //place focusmeter to middle & scale up
                FocusMeter.transform.localScale = new Vector3(2, 2, 2);
                FocusMeter.GetComponent<RectTransform>().anchorMin = new Vector2(middleAnchorMin.x, middleAnchorMin.y);
                FocusMeter.GetComponent<RectTransform>().anchorMax = new Vector2(middleAnchorMax.x, middleAnchorMax.y);
                FocusMeter.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
            }
            else if (state == 5)
            {
                //place focusmeter to game pos & scale down
                FocusMeter.transform.localScale = new Vector3(1, 1, 1);
                FocusMeter.GetComponent<RectTransform>().localPosition = new Vector3(focusGamePos.x, focusGamePos.y, 0);
                FocusMeter.GetComponent<RectTransform>().anchorMin = new Vector2(topRightAnchorMin.x, topRightAnchorMin.y);
                FocusMeter.GetComponent<RectTransform>().anchorMax = new Vector2(topRightAnchorMax.x, topRightAnchorMax.y);
            }
        }

        private void ResetValues()
        {
            heldTimeout = false;
            noGHeld_Reached = false;
            mHeld_Reached = false;
            hHeld_Reached = false;
            sHeld_Reached = false;
            uHeld_Reached = false;

            heldCountdown = threesecCounter;
            HeldPercentage = 1.7f;
            noG_Held = HeldPercentage;
            m_Held = HeldPercentage;
            h_Held = HeldPercentage;
            s_Held = HeldPercentage;
            u_Held = HeldPercentage;
            m_HeldScore = 0;
            noG_HeldScore = 0;
            u_HeldScore = 0;
            s_HeldScore = 0;
            h_HeldScore = 0;

            gestureThresholdTimer = threesecCounter;

            f_trainSW = false;
            m_trainSW = false;

            MeditationTested = false;
            HappinessTested = false;
            SadnessTested = false;
            FocusTested = false;

            mindStateTimeOut = false;

            state = 0;
    }
        private void MetersOnEnable()
        {
            EmotionMeter.GetComponent<PieMeter>().MinValuec1 = 0.0f;
            EmotionMeter.GetComponent<PieMeter>().MinValuec2 = 0.0f;
            EmotionMeter.GetComponent<PieMeter>().MaxValuec1 = 2.5f;
            EmotionMeter.GetComponent<PieMeter>().MaxValuec2 = 2.5f;

            MeditateMeter.GetComponent<PieMeter>().MinValuec1 = gest_c.mOut+2.0f;
            MeditateMeter.GetComponent<PieMeter>().MinValuec2 = gest_c.mOut+2.0f;
            MeditateMeter.GetComponent<PieMeter>().MaxValuec1 = gest_c.mTarget-2.0f;
            MeditateMeter.GetComponent<PieMeter>().MaxValuec2 = gest_c.mTarget-2.0f;

            FocusMeter.GetComponent<PieMeter>().MinValuec1 = gest_c.fOut;
            FocusMeter.GetComponent<PieMeter>().MinValuec2 = gest_c.fOut;
            FocusMeter.GetComponent<PieMeter>().MaxValuec1 = gest_c.fTarget;
            FocusMeter.GetComponent<PieMeter>().MaxValuec2 = gest_c.fTarget;
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
