using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniOSC;

public class GestureController : MonoBehaviour
{
    [Header("Mode")]
    public bool MuseSolo, MuseMulti, Standalone;
    private bool isWekRun;

    [Header("Wekinator Receiver")]
    public GameObject WekOSC_SoloReceiver, WekOSC_MultiReceiver;
    [Header("Wekinator Run Dispatcher")]
    public GameObject WekMeditateDTW_Run, WekFocusDTW_Run, WekEmotionDTW_Run, wekEmotionSVM_Run;
    [Header("Meters")]
    public GameObject MeditateMeter, FocusMeter, EmotionMeter;

    public float wek_mFloat, wek_fFloat, wek_hFloat, wek_sFloat, wek_mood, wek_facialExpression;
    public bool isMeditate, isFocus, isHappy, isSad, isUnsure;
    private bool M_sw, H_sw, S_sw, U_sw;

    [Header("Game Gestures")]
    public bool MeditateTest, EmotionTest, EndTest, StartGame, MeditationTested, HappinessTested, SadnessTested;
    public bool NoGesture, Meditate, Focus, Happy, Sad, Unsure, mindStateTimeOut, heldTimeout;
    public bool noGHeld_Reached, mHeld_Reached, hHeld_Reached, sHeld_Reached, uHeld_Reached;
    public float m_HeldScore, h_HeldScore, s_HeldScore, u_HeldScore, noG_HeldScore;
    private float m_Held, h_Held, s_Held, noG_Held, u_Held, HeldPercentage;   
    private bool intro_sw, mTest_sw;
    [Header("Game Settings")]
    public float mTarget, fTarget, eTarget;
    [Header("Timer Section")]
    public float standaloneCountdown, standaloneCounter;
    public float noGestureCountdown, heldCountdown, meditateCountdown, focusCountdown;
    public float unsureCountdown, happyCountdown, sadCountdown;
    private float sixtysecCounter, thirtysecCounter, tensecCounter, fivesecCounter, foursecCounter, threesecCounter, twosecCounter, secCounter;
    public int state;

    private System.Random randomizer;

    // Use this for initialization
    public void OnEnable()
    {
        randomizer = new System.Random();

        if (Standalone)
        {
            NoG_Enable(); //VolcanoErupt
            state = -1;
            standaloneCountdown = 45.0f;
            standaloneCounter = standaloneCountdown;
        }
        else
        {
            isWekRun = true;

            sixtysecCounter = 60.0f;
            thirtysecCounter = 30.0f;
            tensecCounter = 10.0f;
            fivesecCounter = 5.0f;
            foursecCounter = 4.0f;
            threesecCounter = 3.0f;
            twosecCounter = 2.0f;
            secCounter = 1.0f;

            meditateCountdown = threesecCounter;
            focusCountdown = secCounter;

            unsureCountdown = secCounter;
            happyCountdown = twosecCounter;
            sadCountdown = twosecCounter;

            heldCountdown = foursecCounter;
            HeldPercentage = 2.2f;
            noG_Held = HeldPercentage;
            m_Held = HeldPercentage;
            h_Held = HeldPercentage;
            s_Held = HeldPercentage;
            u_Held = HeldPercentage;

            mTarget = 5.0f;
            fTarget = 2.5f;
            eTarget = 3.0f;

            MeditateMeter.GetComponent<SimpleBars>().MinValue = 20.0f;
            MeditateMeter.GetComponent<SimpleBars>().MaxValue = mTarget;

            FocusMeter.GetComponent<SimpleBars>().MinValue = 10.0f;
            FocusMeter.GetComponent<SimpleBars>().MaxValue = fTarget;

            EmotionMeter.GetComponent<EmotionMeter>().MinValuec1 = 13.0f;
            EmotionMeter.GetComponent<EmotionMeter>().MinValuec2 = 13.0f;
            EmotionMeter.GetComponent<EmotionMeter>().MaxValuec1 = eTarget;
            EmotionMeter.GetComponent<EmotionMeter>().MaxValuec2 = eTarget;

        }
    }
    // Update is called once per frame
    public void Update()
    {
        if (MuseSolo || MuseMulti)
        {
            UpdateMuseHeadset();

            MeditateStates();
            EmotionStates();
            FocusStates();

            GestureStates();
            HeldState();

            MetersUpdate();

            if (MuseSolo)
            {
                WekMeditateDTW_Run.GetComponent<WekEventDispatcherButton>().ButtonClick(isWekRun);
                WekFocusDTW_Run.GetComponent<WekEventDispatcherButton>().ButtonClick(isWekRun);
                WekEmotionDTW_Run.GetComponent<WekEventDispatcherButton>().ButtonClick(isWekRun);
                wekEmotionSVM_Run.GetComponent<WekEventDispatcherButton>().ButtonClick(isWekRun);
       
            }
            else if (MuseMulti)
            {

            }
        }
        else if (Standalone)
        {
            StandaloneEnable();
        }

    }

    public void MuseSoloMode(bool solo)
    {
        if (solo)
        {
            MuseSolo = true;
            MuseMulti = false;
            Standalone = false;
        }
        else if (!solo)
        {
            MuseSolo = false;
        }
    }
    public void MuseMultiMode(bool multi)
    {
        if (multi)
        {
            MuseSolo = false;
            MuseMulti = true;
            Standalone = false;
        }
        else if (!multi)
        {
            MuseMulti = false;
        }

    }

    public void UpdateMuseHeadset()
    {
        if (MuseSolo)
        {
            wek_mFloat = WekOSC_SoloReceiver.GetComponent<UniOSCWekOutputReceiver>().meditateFloat;
            wek_fFloat = WekOSC_SoloReceiver.GetComponent<UniOSCWekOutputReceiver>().focusFloat;
            wek_hFloat = WekOSC_SoloReceiver.GetComponent<UniOSCWekOutputReceiver>().happyFloat;
            wek_sFloat = WekOSC_SoloReceiver.GetComponent<UniOSCWekOutputReceiver>().sadFloat;
            wek_mood = WekOSC_SoloReceiver.GetComponent<UniOSCWekOutputReceiver>().mood;
            wek_facialExpression = WekOSC_SoloReceiver.GetComponent<UniOSCWekOutputReceiver>().facialExpression;
        }
        else if (MuseMulti)
        {
            wek_mFloat = WekOSC_MultiReceiver.GetComponent<UniOSCWekOutputReceiver>().meditateFloat;
            wek_fFloat = WekOSC_MultiReceiver.GetComponent<UniOSCWekOutputReceiver>().focusFloat;
            wek_hFloat = WekOSC_MultiReceiver.GetComponent<UniOSCWekOutputReceiver>().happyFloat;
            wek_sFloat = WekOSC_MultiReceiver.GetComponent<UniOSCWekOutputReceiver>().sadFloat;
            wek_mood = WekOSC_MultiReceiver.GetComponent<UniOSCWekOutputReceiver>().mood;
            wek_facialExpression = WekOSC_MultiReceiver.GetComponent<UniOSCWekOutputReceiver>().facialExpression;
        }
    }
    private void InvokeMeditateTest()
    {
        MeditateTest = true;
    }
    private void InvokeEmotionTest()
    {
        EmotionTest = true;
    }

    public void GestureStates()
    {
        //Intro
        if (!intro_sw && !MeditateTest && !isMeditate) //Intro Lava Erupting
        {
            NoG_Enable();

            Invoke("InvokeMeditateTest", 10.0f);
            intro_sw = true;
            Debug.Log("IntroState");
        }
        //Meditate
        if (StartGame && isMeditate && !M_sw && !heldTimeout || MeditateTest && isMeditate && !MeditationTested)
        {
            if (!mTest_sw) Invoke("InvokeEmotionTest", 10.0f);
            if (!MeditationTested)
            {
                noGestureCountdown = sixtysecCounter;
                MeditationTested = true;
                Debug.Log("MeditateTest");
            }
            else
            {
                noGestureCountdown = thirtysecCounter;
            }

            M_Enable();

           // heldCountdown = foursecCounter;
            m_Held = HeldPercentage;
           // mindStateTimeOut = true;
          //  heldTimeout = true;

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
        if (StartGame && isHappy && !H_sw && !heldTimeout || EmotionTest && !HappinessTested && isHappy && !heldTimeout)
        {
            if (!HappinessTested)
            {
                noGestureCountdown = sixtysecCounter;
                HappinessTested = true;
                Debug.Log("HappyTest");
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
        if (StartGame && isSad && !S_sw && !heldTimeout || EmotionTest && !SadnessTested && isSad && !heldTimeout)
        {
            if (!SadnessTested)
            {
                noGestureCountdown = sixtysecCounter;
                SadnessTested = true;
            }
            else
            {
                noGestureCountdown = thirtysecCounter;
            }

            S_Enable();

            heldCountdown = foursecCounter;
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
        if (StartGame && isUnsure && !U_sw && !heldTimeout)
        {                  
            U_Enable();

            heldCountdown = foursecCounter;
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
        //No Gesture TimeOut
        if (mindStateTimeOut) // no state change within 30sec/test60sec goto no gesture(lava erupting)
        {
            noGestureCountdown -= Time.deltaTime;

            if (noGestureCountdown <= 0)
            {
                NoG_Enable();

                heldCountdown = foursecCounter;
                noG_Held = HeldPercentage;
                heldTimeout = true;

                mindStateTimeOut = false;
            }
        }
        //All Tests completed trigger StartGame
        if (HappinessTested && SadnessTested && !StartGame && !EndTest) // after all tests complete start game
        {
            StartGame = true;
            EndTest = true;
        }
        else if (!EndTest)
        {
            StartGame = false;
        }

        if (isFocus)
        {
          //  Debug.Log("Focused");
        }
    }

    public void HeldState()
    {
        if (heldTimeout) //  5 secs in one state
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
                    
                    if(u_Held <= 0.0f)
                    {
                        uHeld_Reached = true;
                        u_Held = HeldPercentage;
                    }
                    if(h_Held <= 0.0f)
                    {
                        hHeld_Reached = true;
                        h_Held = HeldPercentage;
                    }
                    if(s_Held <= 0.0f)
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

                heldCountdown = foursecCounter;
            }
            else
            {
                if (NoGesture && !isMeditate && !isHappy && !isSad)
                {
                    noG_Held -= Time.deltaTime;
                    noG_HeldScore += Time.deltaTime;
                }
                else if (Meditate && isMeditate)
                {
                    m_Held -= Time.deltaTime;
                    m_HeldScore += Time.deltaTime;
                }

                if (Happy && isHappy)
                {
                    h_Held -= Time.deltaTime;
                    h_HeldScore += Time.deltaTime;
                }

                if (Sad && isSad)
                {
                    s_Held -= Time.deltaTime;
                    s_HeldScore += Time.deltaTime;
                }

                if (Unsure && isUnsure)
                {
                    u_Held -= Time.deltaTime;
                    u_HeldScore += Time.deltaTime;
                }
            }

            heldCountdown -= Time.deltaTime;
        }
    }

    public void MeditateStates()
    {
        if (wek_mFloat <= mTarget)
        {
            if (meditateCountdown <= 0.0f)
            {
                isMeditate = true;
                meditateCountdown = threesecCounter;
            }
            else
            {
                meditateCountdown -= Time.deltaTime;
            }
        }
        else if (wek_mFloat >= (mTarget + 2.0f))
        {
            if (meditateCountdown <= 1.0f)
            {
                isMeditate = false;
                meditateCountdown = threesecCounter;
            }
            else
            {
                meditateCountdown -= Time.deltaTime;
            }

        }
    }

    public void FocusStates()
    {
        if (wek_fFloat <= fTarget)
        {
            if (focusCountdown <= 0.0f)
            {
                isFocus = true;
                focusCountdown = secCounter;
            }
            else
            {
                focusCountdown -= Time.deltaTime;
            }
        }
        else
        {
            isFocus = false;
            focusCountdown = secCounter;
        }
    }

    public void EmotionStates()
    {
        //HAPPY
        if (wek_hFloat <= eTarget && wek_sFloat >= eTarget)
        {
            if (happyCountdown <= 0.0f)
            {
                isHappy = true;
                happyCountdown = twosecCounter;
            }
            else
            {
                happyCountdown -= Time.deltaTime;
            }
        }
        else if (wek_hFloat >= (eTarget + 1.0f))
        {
            happyCountdown = twosecCounter;
            isHappy = false;
        }

        //SAD
        if (wek_sFloat <= eTarget && wek_hFloat >= eTarget)
        {
            if (sadCountdown <= 0.0f)
            {
                isSad = true;
                sadCountdown = twosecCounter;
            }
            else
            {
                sadCountdown -= Time.deltaTime;
            }
        }
        else if (wek_sFloat >= (eTarget + 1.0f))
        {
            sadCountdown = twosecCounter;
            isSad = false;
        }

        //UNSURE
        if (!isHappy && !isSad && !isUnsure || wek_hFloat >= (eTarget+0.1f) && wek_sFloat >= (eTarget + 0.1f) && wek_hFloat <= (eTarget + 0.6f) && wek_sFloat <= (eTarget + 0.6f))
        {
            isUnsure = true;
        } else if (isHappy && isUnsure || isSad && isUnsure)
        {
            isUnsure = false;
        }
    }

    /*   public void SVMEmotionStates()
       {
           if (emotions == 1 && !isUnsure)
           {
               if (unsureCountdown <= 0)
               {
                   isHappy = false;
                   isSad = false;
                   isUnsure = true;
                   unsureCountdown = secCounter;
                   Debug.Log("isUnsure");
               }
               else
               {
                   unsureCountdown -= Time.deltaTime;

                   happyCountdown = twosecCounter;
                   sadCountdown = twosecCounter;
               }
           }
           else if (emotions == 2 && !isHappy && !isMeditate)
           {
               if (happyCountdown <= 0)
               {
                   isHappy = true;
                   isSad = false;
                   isUnsure = false;
                   happyCountdown = twosecCounter;
                   Debug.Log("isHappy");
               }
               else
               {
                   happyCountdown -= Time.deltaTime;

                   unsureCountdown = secCounter;
                   sadCountdown = twosecCounter;
               }

           }
           else if (emotions == 3 && !isSad && !isMeditate)
           {
               if (sadCountdown <= 0)
               {
                   isHappy = false;
                   isSad = true;
                   isUnsure = false;
                   sadCountdown = twosecCounter;
                   Debug.Log("isSad");
               }
               else
               {
                   sadCountdown -= Time.deltaTime;

                   happyCountdown = twosecCounter;
                   unsureCountdown = secCounter;
               }
           }
       }
   */

    private void NoG_Enable()
    {
        NoGesture = true;
        Meditate = false;
        Happy = false;
        Sad = false;
        Unsure = false;
    }
    private void M_Enable()
    {
        NoGesture = false;
        Meditate = true;
    }
    private void H_Enable()
    {
        NoGesture = false;
       if(!isMeditate) Meditate = false;
        Happy = true;
        Sad = false;
        Unsure = false;
    }
    private void S_Enable()
    {
        NoGesture = false;
       if(!isMeditate) Meditate = false;
        Happy = false;
        Sad = true;
        Unsure = false;
    }
    private void U_Enable()
    {
        NoGesture = false;
       if(!Meditate) Meditate = false;
        Happy = false;
        Sad = false;
        Unsure = true;
    }

    private void MetersUpdate()
    {
        MeditateMeter.GetComponent<SimpleBars>().Value = wek_mFloat;
        FocusMeter.GetComponent<SimpleBars>().Value = wek_fFloat;

        //Find a way to have unsure...
        EmotionMeter.GetComponent<EmotionMeter>().Valuec1 = wek_hFloat;
        EmotionMeter.GetComponent<EmotionMeter>().Valuec2 = wek_sFloat;        
    }

    public void StandaloneMode(bool standalone)
    {
        if (standalone)
        {
            MuseSolo = false;
            MuseMulti = false;
            Standalone = true;
        }
        else if (!standalone)
        {
            Standalone = false;
        }
    }
    void StandaloneEnable()
    {
        if (Standalone && state == -1)
        {
            state = 0;
        }
        else if (state > -1)
        {
            standaloneCounter -= Time.deltaTime;
            if (standaloneCounter <= 0)
            {
                standaloneCounter = 0;
            }
        }

        if (state == 0)
        {
            if (standaloneCounter <= 0)
            {
                M_Enable();
                state++;
                standaloneCounter = standaloneCountdown;
            }

        }
        else if (state == 1)
        {
            if (standaloneCounter <= 0)
            {
                H_Enable();
                state++;
                standaloneCounter = standaloneCountdown;
            }
        }
        else if (state == 2)
        {
            if (standaloneCounter <= 0)
            {
                S_Enable();
                state++;
                standaloneCounter = standaloneCountdown;
            }
        }
        else if (state == 3)
        {
            if (standaloneCounter <= 0)
            {
                U_Enable();
                state++;
                standaloneCounter = standaloneCountdown;
            }
        }
        else if (state >= 4)
        {
            if (standaloneCounter <= 0)
            {
                RandomGesture();
                state++;
                standaloneCounter = standaloneCountdown;
            }

        }
    }
    private void RandomGesture()
    {

        int funcToChoose = randomizer.Next(7);

        switch (funcToChoose)
        {
            case 0:
                U_Enable();
                break;
            case 1:
                M_Enable();
                break;
            case 2:
                H_Enable();
                break;
            case 3:
                S_Enable();
                break;
            case 4:
                NoG_Enable();
                break;
        }

    }
}
