using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniOSC;

namespace SoliGameController
{
    public class GameController : MonoBehaviour
    {
        MeditateState ms;
        FocusState fs;
        EmotionState es;

        //   [Header("Wekinator Run Dispatcher")]
        public GameObject HDGrp, HS0, HS1, HS2, HS3;
        public GameObject WekMeditateDTW_Run, WekFocusDTW_Run, WekEmotionDTW_Run, wekEmotionSVM_Run;
        public GameObject MuseMonitor;
        public GameObject HomeButton;
        //       [Header("Wekinator")]
        public GameObject WekOSC_Receiver;
        public float wek_mFloat, wek_fFloat, wek_hFloat, wek_sFloat, wek_mood, wek_facialExpression;
        public bool wek_isM, wek_isF;
        //   [Header("Mode")]
        public bool isRunning, homebtn, HDGrpOn;
        public int HeadsetOn;
        public float batt, hs0, hs1, hs2, hs3;
        public Color HS_ActiveCol, HS_DeactiveCol, HS_LerpedCol;
   //     private bool HS_Col_SW, HS_Trans_SW, HS_Lerp_SW, HS_DeactiveCol_SW;
        //    [Header("GameStates")]
        public bool NoGesture, Meditate, Happy, Sad, Unsure;
        public bool MeditationTested, HappinessTested, SadnessTested, FocusTested;
        private bool M_sw, H_sw, S_sw, U_sw, F_sw;
   //     [Header("HeldStates")]
        public bool mindStateTimeOut, heldTimeout, noGHeld_Reached, mHeld_Reached, hHeld_Reached, sHeld_Reached, uHeld_Reached;
        public float  m_HeldScore, h_HeldScore, s_HeldScore, u_HeldScore, noG_HeldScore;
        private float m_Held, h_Held, s_Held, noG_Held, u_Held, HeldPercentage;
   //     [Header("Timer Section")]
        public float noGestureCountdown, heldCountdown, runCountdown;
        public int state;
        public float noGSixtyCounter, noGCounter, heldCounter;

        // Use this for initialization
        private void ResetValues()
        {
            heldTimeout = false;
            noGHeld_Reached = false;
            mHeld_Reached = false;
            hHeld_Reached = false;
            sHeld_Reached = false;
            uHeld_Reached = false;

            heldCountdown = heldCounter;
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

            MeditationTested = false;
            HappinessTested = false;
            SadnessTested = false;
            FocusTested = false;

            mindStateTimeOut = false;

            state = 0;
        }

        public void OnEnable()
        {
            ms = this.GetComponent<MeditateState>();
            fs = this.GetComponent<FocusState>();
            es = this.GetComponent<EmotionState>();

            HS_ActiveCol = new Color32(80, 160, 80, 255);
            HS_DeactiveCol = new Color32(80, 160, 80, 0);

            noGSixtyCounter = 60.0f;
            noGCounter = 10.0f;
            heldCounter = 3.0f;
            runCountdown = 3.0f;

            HeadsetOn = MuseMonitor.GetComponent<UniOSCMuseMonitor>().touchingforehead;

            ResetValues();

            NoG_Enable();
        }
        // Update is called once per frame
        public void HeadsetIndicators()
        {
            HS_LerpedCol = Color.Lerp(HS_DeactiveCol, HS_ActiveCol, Mathf.PingPong(Time.time, 0.3f));

            if (!HDGrpOn)
            {
                HDGrp.GetComponent<ActivateObjects>().SetActive(true);
                HDGrpOn = true;
            }

            if (hs0 == 1)
            {
                HS0.GetComponent<MuseHSIMeter>().fillersemiC1.color = HS_ActiveCol;
                HS0.GetComponent<MuseHSIMeter>().fillersemiC2.color = HS_ActiveCol;
            }
            else if (hs0 == 2)
            {
                HS0.GetComponent<MuseHSIMeter>().fillersemiC1.color = HS_LerpedCol;
                HS0.GetComponent<MuseHSIMeter>().fillersemiC2.color = HS_LerpedCol;
            }
            else if (hs0 == 4)
            {
                HS0.GetComponent<MuseHSIMeter>().fillersemiC1.color = HS_DeactiveCol;
                HS0.GetComponent<MuseHSIMeter>().fillersemiC2.color = HS_DeactiveCol;
            }

            if (hs1 == 1)
            {
                HS1.GetComponent<MuseHSIMeter>().fillersemiC1.color = HS_ActiveCol;
                HS1.GetComponent<MuseHSIMeter>().fillersemiC2.color = HS_ActiveCol;
            }
            else if (hs1 == 2)
            {
                HS1.GetComponent<MuseHSIMeter>().fillersemiC1.color = HS_LerpedCol;
                HS1.GetComponent<MuseHSIMeter>().fillersemiC2.color = HS_LerpedCol;
            }
            else if (hs1 == 4)
            {
                HS1.GetComponent<MuseHSIMeter>().fillersemiC1.color = HS_DeactiveCol;
                HS1.GetComponent<MuseHSIMeter>().fillersemiC2.color = HS_DeactiveCol;
            }

            if (hs2 == 1)
            {
                HS2.GetComponent<MuseHSIMeter>().fillersemiC1.color = HS_ActiveCol;
                HS2.GetComponent<MuseHSIMeter>().fillersemiC2.color = HS_ActiveCol;
            }
            else if (hs2 == 2)
            {
                HS2.GetComponent<MuseHSIMeter>().fillersemiC1.color = HS_LerpedCol;
                HS2.GetComponent<MuseHSIMeter>().fillersemiC2.color = HS_LerpedCol;
            }
            else if (hs2 == 4)
            {
                HS2.GetComponent<MuseHSIMeter>().fillersemiC1.color = HS_DeactiveCol;
                HS2.GetComponent<MuseHSIMeter>().fillersemiC2.color = HS_DeactiveCol;
            }

            if (hs3 == 1)
            {
                HS3.GetComponent<MuseHSIMeter>().fillersemiC1.color = HS_ActiveCol;
                HS3.GetComponent<MuseHSIMeter>().fillersemiC2.color = HS_ActiveCol;
            }
            else if (hs3 == 2)
            {
                HS3.GetComponent<MuseHSIMeter>().fillersemiC1.color = HS_LerpedCol;
                HS3.GetComponent<MuseHSIMeter>().fillersemiC2.color = HS_LerpedCol;
            }
            else if (hs3 == 4)
            {
                HS3.GetComponent<MuseHSIMeter>().fillersemiC1.color = HS_DeactiveCol;
                HS3.GetComponent<MuseHSIMeter>().fillersemiC2.color = HS_DeactiveCol;
            }
        }
        public void UpdateMuseHeadset()
        {
            wek_mFloat = WekOSC_Receiver.GetComponent<UniOSCWekOutputReceiver>().meditateFloat;
            wek_isM = WekOSC_Receiver.GetComponent<UniOSCWekOutputReceiver>().isMeditate;

            wek_fFloat = WekOSC_Receiver.GetComponent<UniOSCWekOutputReceiver>().focusFloat;
            wek_isF = WekOSC_Receiver.GetComponent<UniOSCWekOutputReceiver>().isFocus;

            wek_hFloat = WekOSC_Receiver.GetComponent<UniOSCWekOutputReceiver>().happyFloat;
            wek_sFloat = WekOSC_Receiver.GetComponent<UniOSCWekOutputReceiver>().sadFloat;
            wek_mood = WekOSC_Receiver.GetComponent<UniOSCWekOutputReceiver>().mood;
            wek_facialExpression = WekOSC_Receiver.GetComponent<UniOSCWekOutputReceiver>().facialExpression;
        }
        public void Update()
        {
            HeadsetOn = MuseMonitor.GetComponent<UniOSCMuseMonitor>().touchingforehead;
            batt = MuseMonitor.GetComponent<UniOSCMuseMonitor>().batt;
            hs0 = MuseMonitor.GetComponent<UniOSCMuseMonitor>().hs0;
            hs1 = MuseMonitor.GetComponent<UniOSCMuseMonitor>().hs1;
            hs2 = MuseMonitor.GetComponent<UniOSCMuseMonitor>().hs2;
            hs3 = MuseMonitor.GetComponent<UniOSCMuseMonitor>().hs3;

            if (HeadsetOn == 1 && hs0 == 1 && hs1 == 1 && hs2 == 1 && hs3 == 1 && !isRunning)
            {
                if (runCountdown <= 0.0f)
                {
                    HDGrpOn = false;
                    HDGrp.GetComponent<ActivateObjects>().SetDeactive(true);
                    runCountdown = 3.0f;
                    isRunning = true;
                }
                else if (runCountdown >= 0.0f)
                {
                    runCountdown -= Time.deltaTime;
                }

            }
            else if (HeadsetOn == 1 && hs0 >= 2 && !isRunning || HeadsetOn == 1 && hs1 >= 2 && !isRunning || HeadsetOn == 1 && hs2 >= 2 && !isRunning || HeadsetOn == 1 && hs3 >= 2 && !isRunning)
            {
                HeadsetIndicators();
            }
            else if (HeadsetOn == 0 && isRunning)
            {
                ResetValues();
                NoG_Enable();
                isRunning = false;
                WekRun();

            }

            if (isRunning)
            {
                UpdateMuseHeadset();
                WekRun();
                GestureStates();
                HeldState();
            } 

            if (Input.anyKey && !homebtn)
            {
                homebtn = true;
                HomeButton.GetComponent<ActivateObjects>().SetActive(homebtn);
            }
            else if (!Input.anyKey && homebtn && HeadsetOn == 1)
            {
                homebtn = false;
                HomeButton.GetComponent<ActivateObjects>().SetDeactive(true);
            }

        }

        public void GestureStates() //state 0 = narratorMTraining, state 1 = meditatetraining complete, state 2 = NarratoremoTraining state 3 = Happy/Sad training complete, state 4 = NarratorfTraining state 5 = focustraining complete
        {
            if (state == 1 && ms.isMeditate && !MeditationTested || state == -1 && ms.isMeditate && !M_sw && !heldTimeout)
            {
                if (!MeditationTested)
                {
                    noGestureCountdown = noGSixtyCounter;
                    MeditationTested = true;
                    state++;
                    Debug.Log("MeditatePassed");
                }
                else
                {
                     noGestureCountdown =  noGCounter;
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
             if (state == 2 && !HappinessTested && es.isHappy && !heldTimeout && es.happy|| state == -1 && es.isHappy && !H_sw && !heldTimeout)
            {
                if (!HappinessTested)
                {
                    noGestureCountdown = noGSixtyCounter;
                    HappinessTested = true;

                    Debug.Log("HappyPassed");
                }

                H_Enable();

                heldCountdown = heldCounter;
                h_Held = HeldPercentage;
                mindStateTimeOut = true;
                heldTimeout = true;

                Debug.Log("HappyState");

                M_sw = false;
                H_sw = true;
                S_sw = false;
                U_sw = false;

                noGHeld_Reached = false;
              if(!ms.isMeditate)  mHeld_Reached = false;
                sHeld_Reached = false;
                uHeld_Reached = false;
            }
            //Sad
            if (state == 3 && !SadnessTested && es.isSad && !heldTimeout && !heldTimeout || state == -1 && es.isSad && !S_sw && !heldTimeout)
            {
                if (!SadnessTested)
                {
                    noGestureCountdown = noGSixtyCounter;
                    SadnessTested = true;
                    if (HappinessTested) state++;
                    Debug.Log("SadPassed");
                }

                S_Enable();

                heldCountdown = heldCounter;
                s_Held = HeldPercentage;
                mindStateTimeOut = true;
                heldTimeout = true;

                Debug.Log("SadState");

                M_sw = false;
                H_sw = false;
                S_sw = true;
                U_sw = false;

                noGHeld_Reached = false;
                if (!ms.isMeditate) mHeld_Reached = false;
                hHeld_Reached = false;
                uHeld_Reached = false;
            }

            //Unsure
            if (state == -1 && es.isUnsure && !U_sw && !heldTimeout)
            {
                U_Enable();

                heldCountdown = heldCounter;
                u_Held = HeldPercentage;
                mindStateTimeOut = true;
                heldTimeout = true;

                Debug.Log("UnsureState");

                M_sw = false;
                H_sw = false;
                S_sw = false;
                U_sw = true;

                noGHeld_Reached = false;
                if (!ms.isMeditate) mHeld_Reached = false;
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

                    heldCountdown = heldCounter;
                    noG_Held = HeldPercentage;
                    heldTimeout = true;

                    mindStateTimeOut = false;
                }
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
                        mindStateTimeOut = false;
                    }

                    if (h_Held <= 0.0f)
                    {
                        hHeld_Reached = true;
                        h_Held = HeldPercentage;
                       // mindStateTimeOut = false;
                    }
                    else if (s_Held <= 0.0f)
                    {
                        sHeld_Reached = true;
                        s_Held = HeldPercentage;
                      //  mindStateTimeOut = false;
                    }
                    else if (u_Held <= 0.0f)
                    {
                        uHeld_Reached = true;
                        u_Held = HeldPercentage;
                    }
                    else
                    {
                     //   if (NoGesture)
                     //   {
                     //       NoGesture = false;
                     //   }
                        heldTimeout = false;
                    }

                    heldCountdown = heldCounter;
                }
                else
                {
                    if (NoGesture && !ms.isMeditate)
                    {
                        noG_Held -= Time.deltaTime;
                        noG_HeldScore += Time.deltaTime;
                    }
                    else if (Meditate && ms.isMeditate)
                    {
                        m_Held -= Time.deltaTime;
                        m_HeldScore += Time.deltaTime;
                    }

                    if (Happy && es.isHappy)
                    {
                        h_Held -= Time.deltaTime;
                        h_HeldScore += Time.deltaTime;
                    }
                    else if (Sad && es.isSad)
                    {
                        s_Held -= Time.deltaTime;
                        s_HeldScore += Time.deltaTime;
                    }
                    else if (Unsure && es.isUnsure)
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
           // NoGesture = false;
            if (!ms.isMeditate) Meditate = false;
            Happy = true;
            Sad = false;
            Unsure = false;
        }
        public void S_Enable()
        {
          //  NoGesture = false;
            if (!ms.isMeditate) Meditate = false;
            Happy = false;
            Sad = true;
            Unsure = false;
        }
        public void U_Enable()
        {
         //   NoGesture = false;
            if (!ms.isMeditate) Meditate = false;
            Happy = false;
            Sad = false;
            Unsure = true;
        }

        private void WekRun()
        {
            WekMeditateDTW_Run.GetComponent<WekEventDispatcherButton>().ButtonClick(isRunning);
            WekFocusDTW_Run.GetComponent<WekEventDispatcherButton>().ButtonClick(isRunning);
            WekEmotionDTW_Run.GetComponent<WekEventDispatcherButton>().ButtonClick(isRunning);
            wekEmotionSVM_Run.GetComponent<WekEventDispatcherButton>().ButtonClick(isRunning);
        }

    }
}
