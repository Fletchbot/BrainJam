using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniOSC;

namespace SoliGameController
{
    public class GameController : MonoBehaviour
    {
        GestureController gestureController;
        AudioPlaytestManager au;

        [Header("Wekinator Run Dispatcher")]
        public GameObject WekMeditateDTW_Run, WekFocusDTW_Run, WekEmotionDTW_Run, wekEmotionSVM_Run;
        [Header("Mode")]
        public bool Muse, Standalone;
        private bool isWekRun;
        [Header("Meters")]
        public GameObject MeditateMeter, FocusMeter, EmotionMeter;
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
        private float sixtysecCounter, thirtysecCounter, tensecCounter, fivesecCounter, foursecCounter, threesecCounter, twosecCounter, secCounter;
        private System.Random randomizer;

        // Use this for initialization
        public void OnEnable()
        {
            au = this.GetComponent<AudioPlaytestManager>();
            randomizer = new System.Random();

            sixtysecCounter = 60.0f;
            thirtysecCounter = 30.0f;
            tensecCounter = 10.0f;
            fivesecCounter = 5.0f;
            foursecCounter = 4.0f;
            threesecCounter = 3.0f;
            twosecCounter = 2.0f;
            secCounter = 1.0f;

            gestureThresholdTimer = tensecCounter;

            gestureController = this.GetComponent<GestureController>();

            EmotionMeter.GetComponent<PieMeter>().MinValuec1 = 0.0f;
            EmotionMeter.GetComponent<PieMeter>().MinValuec2 = 0.0f;
            EmotionMeter.GetComponent<PieMeter>().MaxValuec1 = 2.5f;
            EmotionMeter.GetComponent<PieMeter>().MaxValuec2 = 2.5f;

            heldCountdown = threesecCounter;
            HeldPercentage = 1.7f;
            noG_Held = HeldPercentage;
            m_Held = HeldPercentage;
            h_Held = HeldPercentage;
            s_Held = HeldPercentage;
            u_Held = HeldPercentage;

            focusSkipCountdown = threesecCounter;

            state = 0;

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
                WekMeditateDTW_Run.GetComponent<WekEventDispatcherButton>().ButtonClick(isWekRun);
                WekFocusDTW_Run.GetComponent<WekEventDispatcherButton>().ButtonClick(isWekRun);
                WekEmotionDTW_Run.GetComponent<WekEventDispatcherButton>().ButtonClick(isWekRun);
                wekEmotionSVM_Run.GetComponent<WekEventDispatcherButton>().ButtonClick(isWekRun);
            }
            TrainingStates();
            GestureStates();

            MetersUpdate();
            GestureDifficultyUpdate();
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

        public void TrainingStates()
        {           
            //Intro
            if (state == 0) //INTRO (Lava Erupting)
            {
                NoG_Enable();

                if(au.N_Intro == 2 && focusSkipCountdown <= 0.0f)
                {
                    state++;
                    focusSkipCountdown = threesecCounter;
                    Debug.Log("Intro");
                }
                else if (au.N_Intro == 2 && gestureController.isFocus)
                {
                    focusSkipCountdown -= Time.deltaTime;
                }
            }
            else if (state == 1) //MEDITATE TRAINING INTRO
            {
                if (au.N_Intro == 3 && focusSkipCountdown <= 0.0f)
                {
                    state++;
                    au.N_Intro = 4;
                    focusSkipCountdown = threesecCounter;
                    Debug.Log("MeditateIntroSkip");
                }
                else if (au.N_Intro == 3 && gestureController.isFocus)
                {
                    focusSkipCountdown -= Time.deltaTime;
                }
                else if (au.N_Intro == 4)
                {
                    state++;
                    Debug.Log("MeditateIntro");
                }
            }
            else if (state == 3) //EMOTION TRAINING INTRO
            {
                if (au.N_Intro == 5 && focusSkipCountdown <= 0.0f)
                {
                    state++;
                    au.N_Intro = 6;
                    focusSkipCountdown = threesecCounter;
                    Debug.Log("EmotionIntroSkip");
                }
                else if (au.N_Intro == 5 && gestureController.isFocus)
                {
                    focusSkipCountdown -= Time.deltaTime;
                }
                else if (au.N_Intro == 6)
                {
                    state++;
                    Debug.Log("EmotionIntro");
                }
            }
            else if (state == 5) //FOCUS TRAINING INTRO
            {
                if (au.N_Intro == 8)
                {
                    state++;
                    Debug.Log("FocusIntro");
                }

            }
        }
        public void GestureStates()
        {
            if (state == 2 && gestureController.isMeditate && !MeditationTested || state == -1 && gestureController.isMeditate && !M_sw && !heldTimeout)
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
             if (state == 4 && !HappinessTested && gestureController.isHappy && !heldTimeout || state == -1 && gestureController.isHappy && !H_sw && !heldTimeout)
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
            if (state == 4 && !SadnessTested && gestureController.isSad && !heldTimeout || state == -1 && gestureController.isSad && !S_sw && !heldTimeout)
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

            if (state == 6 && gestureController.isFocus && !FocusTested || state == -1 && gestureController.isFocus && !F_sw)
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

        public void GestureDifficultyUpdate()
        {
            MeditateMeter.GetComponent<PieMeter>().MinValuec1 = gestureController.mOut;
            MeditateMeter.GetComponent<PieMeter>().MinValuec2 = gestureController.mOut;
            MeditateMeter.GetComponent<PieMeter>().MaxValuec1 = gestureController.mTarget;
            MeditateMeter.GetComponent<PieMeter>().MaxValuec2 = gestureController.mTarget;

            FocusMeter.GetComponent<PieMeter>().MinValuec1 = gestureController.fOut;
            FocusMeter.GetComponent<PieMeter>().MinValuec2 = gestureController.fOut;
            FocusMeter.GetComponent<PieMeter>().MaxValuec1 = gestureController.fTarget;
            FocusMeter.GetComponent<PieMeter>().MaxValuec2 = gestureController.fTarget;

            //Focus calibrate difficulty
            if (state == 0 && gestureThresholdTimer <= 0.0f)
            {
                if (gestureController.wek_fFloat >= 10.5f)
                {
                    gestureController.fTarget = 10.0f;
                    gestureController.fOut = 11.0f;
                }
                else if (gestureController.wek_fFloat >= 9.5f && gestureController.wek_fFloat <= 10.4f)
                {
                    gestureController.fTarget = 9.0f;
                    gestureController.fOut = 10.0f;
                }
                else if (gestureController.wek_fFloat >= 7.5f && gestureController.wek_fFloat <= 9.4f)
                {
                    gestureController.fTarget = 7.0f;
                    gestureController.fOut = 8.0f;
                }
                else if (gestureController.wek_fFloat >= 5.5f && gestureController.wek_fFloat <= 7.4f)
                {
                    gestureController.fTarget = 5.0f;
                    gestureController.fOut = 6.0f;
                }
                else if (gestureController.wek_fFloat >= 4.0f && gestureController.wek_fFloat <= 5.4f)
                {
                    gestureController.fTarget = 3.5f;
                    gestureController.fOut = 4.5f;
                }

                gestureThresholdTimer = tensecCounter;
            }
            else if (state == 0)
            {
                gestureThresholdTimer -= Time.deltaTime;
            }

            // Meditate calibrate difficulty
            if (state == 1 && gestureThresholdTimer <= 0.0f)
            {
                if (gestureController.wek_mFloat >= 9.1f)
                {
                    gestureController.mTarget = 8.0f;
                    gestureController.mOut = 9.0f;
                }
                else if (gestureController.wek_mFloat >= 8.1f && gestureController.wek_mFloat <= 9.0f)
                {
                    gestureController.mTarget = 7.5f;
                    gestureController.mOut = 9.0f;
                }
                else if (gestureController.wek_mFloat >= 7.1f && gestureController.wek_mFloat <= 8.0f)
                {
                    gestureController.mTarget = 6.5f;
                    gestureController.mOut = 8.5f;
                }
                else if (gestureController.wek_mFloat >= 6.1f && gestureController.wek_mFloat <= 7.0f)
                {
                    gestureController.mTarget = 5.5f;
                    gestureController.mOut = 7.5f;
                }
                else if (gestureController.wek_mFloat >= 5.0f && gestureController.wek_mFloat <= 6.0f)
                {
                    gestureController.mTarget = 4.5f;
                    gestureController.mOut = 7.0f;
                }

                gestureThresholdTimer = tensecCounter;
            }
            else if (state == 1)
            {
                gestureThresholdTimer -= Time.deltaTime;
            }
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

        public void StateChangerButton(bool statechange)
        {
            if (statechange)
            {
                state++;
            }
        }
    }
}
