using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniOSC;

public class GestureController : MonoBehaviour
{
    [Header("Mode")]
    public bool MuseSolo, MuseMulti, Standalone;
    public bool isWekRun;

    [Header("Wekinator Receiver")]
    public GameObject WekOSC_SoloReceiver, WekOSC_MultiReceiver;

    public float meditateFloat, focusFloat, emotions;
   
    public bool isMeditate, isFocus;
    public bool isUnsure, isHappy, isSad; 
    public float unsureFloat, happyFloat, sadFloat;
    private bool G_sw, M_sw, H_sw, S_sw;

    [Header("Wekinator Run Dispatcher")]
    public GameObject WekMeditateDTW_Run, WekFocusDTW_Run, WekEmotionSVM_Run;

    [Header("Game Gestures")]
    public bool StartTest, IntroTest, EndTest, StartGame, MeditationTested, HappinessTested, SadnessTested;

    public bool NoGesture, Mediate, Focus, Happy, Sad, mindStateTimeOut, onenessTimeout;

    public float M_Oneness, H_Oneness, S_Oneness, noG_Oneness, OnenessPercentage, M_OnenessScore, H_OnenessScore, S_OnenessScore, noG_OnenessScore;


    [Header("Timer Section")]
    public float standaloneCountdown, standaloneCounter;
    public float noGestureCountdown, onenessCountdown, meditateCountdown, focusCountdown;
    public float unsureCountdown, happyCountdown, sadCountdown;

    public float sixtysecCounter, tensecCounter, fivesecCounter, threesecCounter, secCounter;
    public float speed = 1;
    public int state;

    [Header("Game Section")]
    public bool p1Destroyed, p2Destroyed, pPoint, sCombo, sdCombo, cStack; 
    public int p1combo, p2combo, superCombo, superduperCombo, comboStackTotal;
    public int p1Total, p2Total, superTotal, superduperTotal;
    public float projectileCountdown, comboCountdown;

    private System.Random randomizer;

    // Use this for initialization
    public void OnEnable()
    {
        IntroTest = true;
        StartTest = true;
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
            tensecCounter = 10.0f;
            fivesecCounter = 5.0f;
            threesecCounter = 2.0f;
            secCounter = 1.0f;

            meditateCountdown = fivesecCounter;
            focusCountdown = secCounter;

            unsureCountdown = secCounter;
            happyCountdown = threesecCounter;
            sadCountdown = threesecCounter;

            projectileCountdown = threesecCounter;
            comboCountdown = tensecCounter;

            OnenessPercentage = 3.5f;          
        }     
    }
    // Update is called once per frame
    public void Update()
    {
        if (MuseSolo || MuseMulti)
        {
            UpdateMuseHeadset();

            GestureStates();
            MeditateStates();
            EmotionStates();
            FocusStates();

           if(StartGame) ScoreBoard();

            if (MuseSolo)
            {
                WekMeditateDTW_Run.GetComponent<WekEventDispatcherButton>().ButtonClick(isWekRun);
                WekFocusDTW_Run.GetComponent<WekEventDispatcherButton>().ButtonClick(isWekRun);
                WekEmotionSVM_Run.GetComponent<WekEventDispatcherButton>().ButtonClick(isWekRun);
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
            meditateFloat = WekOSC_SoloReceiver.GetComponent<UniOSCWekOutputReceiver>().meditateFloat;
            focusFloat = WekOSC_SoloReceiver.GetComponent<UniOSCWekOutputReceiver>().focusFloat;

            emotions = WekOSC_SoloReceiver.GetComponent<UniOSCWekOutputReceiver>().emotions;
            unsureFloat = WekOSC_SoloReceiver.GetComponent<UniOSCWekOutputReceiver>().unsureFloat;
            happyFloat = WekOSC_SoloReceiver.GetComponent<UniOSCWekOutputReceiver>().happyFloat;
            sadFloat = WekOSC_SoloReceiver.GetComponent<UniOSCWekOutputReceiver>().sadFloat;
        }
        else if (MuseMulti)
        {
            meditateFloat = WekOSC_MultiReceiver.GetComponent<UniOSCWekOutputReceiver>().meditateFloat;
            focusFloat = WekOSC_MultiReceiver.GetComponent<UniOSCWekOutputReceiver>().focusFloat;
            emotions = WekOSC_MultiReceiver.GetComponent<UniOSCWekOutputReceiver>().emotions;
            unsureFloat = WekOSC_MultiReceiver.GetComponent<UniOSCWekOutputReceiver>().unsureFloat;
            happyFloat = WekOSC_MultiReceiver.GetComponent<UniOSCWekOutputReceiver>().happyFloat;
            sadFloat = WekOSC_MultiReceiver.GetComponent<UniOSCWekOutputReceiver>().sadFloat;
        }
    }
    void InvokeIntro()
    {
        IntroTest = false;
    }
    public void GestureStates()
    {
        if (StartTest && !isMeditate) //Intro Lava Erupting
        {
            NoG_Enable();

            if (StartTest) StartTest = false;
            Debug.Log("IntroState");
        }
        else if (IntroTest && isMeditate && !MeditationTested && !onenessTimeout || isMeditate && !M_sw && !onenessTimeout) //Intro into meditation Sunset
        {
            if (IntroTest) Invoke("InvokeIntro", 10.0f);
            if(MeditationTested) MeditationTested = true;

            M_Enable();

            onenessCountdown = fivesecCounter;
            noGestureCountdown = sixtysecCounter;
            mindStateTimeOut = true;
            onenessTimeout = true;
            Debug.Log("MeditateState");

            M_sw = true;
            H_sw = false;
            S_sw = false;
        }
        else if (!IntroTest && !HappinessTested && isHappy && !onenessTimeout || isHappy && HappinessTested && !H_sw && !onenessTimeout) // meditation or sad into Happy Clear Day
        {
            if (!HappinessTested) HappinessTested = true;

            H_Enable();

            onenessCountdown = fivesecCounter;
            noGestureCountdown = sixtysecCounter;
            mindStateTimeOut = true;
            onenessTimeout = true;
            Debug.Log("HappyState");

            M_sw = false;
            H_sw = true;
            S_sw = false;
        }
        else if (!IntroTest && !SadnessTested && isSad && !onenessTimeout|| isSad && SadnessTested && !S_sw && !onenessTimeout) // meditation or happy into Sad Winter night
        {
            if (!SadnessTested) SadnessTested = true;
            S_Enable();

            onenessCountdown = fivesecCounter;
            noGestureCountdown = sixtysecCounter;
            mindStateTimeOut = true;
            onenessTimeout = true;
            Debug.Log("SadState");

            M_sw = false;
            H_sw = false;
            S_sw = true;
        }

        if (HappinessTested && SadnessTested && !StartGame && !NoGesture && !EndTest) // after all tests complete start game
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
            Debug.Log("FocusState");
        }

        if (onenessTimeout) // oneness int = 10 secs in one state
        {
            onenessCountdown -= Time.deltaTime;

            if (onenessCountdown <= 0)
            {
                onenessCountdown = fivesecCounter;

                if (noG_Oneness <= 0 || M_Oneness <= 0 || H_Oneness <= 0 || S_Oneness <= 0)
                {
                //    DisableAllGestures();
                    noG_Oneness = OnenessPercentage;
                    M_Oneness = OnenessPercentage;
                    H_Oneness = OnenessPercentage;
                    S_Oneness = OnenessPercentage;
                }
                else
                {
                    onenessTimeout = false;
                }
   

            }
            else
            {
                if (NoGesture && !isMeditate && !isHappy && !isSad)
                {
                    noG_Oneness -= Time.deltaTime;
                    noG_OnenessScore += Time.deltaTime;
                }
                else if (Mediate && isMeditate)
                {
                    M_Oneness -= Time.deltaTime;
                    M_OnenessScore += Time.deltaTime;
                }
                else if (Happy && isHappy)
                {
                    H_Oneness -= Time.deltaTime;
                    H_OnenessScore += Time.deltaTime;
                }
                else if (Sad && isSad)
                {
                    S_Oneness -= Time.deltaTime;
                    S_OnenessScore += Time.deltaTime;
                }
            }
        }

        if (mindStateTimeOut) // no state change within 1min goto no gesture(lava erupting)
        {
            noGestureCountdown -= Time.deltaTime;

            if (noGestureCountdown <= 0)
            {
                NoG_Enable();

                onenessCountdown = tensecCounter;
                onenessTimeout = true;

                mindStateTimeOut = false;
            }
        }
    }

    public void MeditateStates()
    {
        if (meditateFloat <= 5.7f)
        {
            if (meditateCountdown <= 0.0f)
            {
                isMeditate = true;
                meditateCountdown = fivesecCounter;
                Debug.Log("isMeditate");
            }
            else
            {
                meditateCountdown -= Time.deltaTime;
            }
        }
        else
        {
            isMeditate = false;
            meditateCountdown = fivesecCounter;
        }
    }

    public void FocusStates()
    {
        if (focusFloat <= 3.5f)
        {
            if (focusCountdown <= 0.0f)
            {
                isFocus = true;
                focusCountdown = secCounter;
                Debug.Log("isFocus");
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

                happyCountdown = threesecCounter;
                sadCountdown = threesecCounter;
            }
        }
        else if (emotions == 2 && !isHappy)
        {
            if (happyCountdown <= 0)
            {
                isHappy = true;
                isSad = false;
                isUnsure = false;
                happyCountdown = threesecCounter;
                Debug.Log("isHappy");
            }
            else
            {
                happyCountdown -= Time.deltaTime;

                unsureCountdown = secCounter;
                sadCountdown = threesecCounter;
            }

        }
        else if (emotions == 3 && !isSad)
        {
            if (sadCountdown <= 0)
            {
                isHappy = false;
                isSad = true;
                isUnsure = false;
                sadCountdown = threesecCounter;
                Debug.Log("isSad");
            }
            else
            {
                sadCountdown -= Time.deltaTime;

                happyCountdown = threesecCounter;
                unsureCountdown = secCounter;
            }
        }
    }

    private void NoG_Enable()
    {
        NoGesture = true;
        Mediate = false;
        Happy = false;
        Sad = false;
    }
    private void M_Enable()
    {
        NoGesture = false;
        Mediate = true;
        Happy = false;
        Sad = false;
    }
    private void H_Enable()
    {
        NoGesture = false;
        Mediate = false;
        Happy = true;
        Sad = false;
    }
    private void S_Enable()
    {
        NoGesture = false;
        Mediate = false;
        Happy = false;
        Sad = true;
    }

    private void DisableAllGestures()
    {
        NoGesture = false;
        Mediate = false;
        Happy = false;
        Sad = false;
    }

    public void projectileDestroyed(string p)
    {
        if(p == "Projectile1")
        {
            p1Destroyed = true;
            p1Total++;
            p1combo++;
            p1Destroyed = false;
        }
        else if (p == "Projectile2")
        {
            p2Destroyed = true;
            p2Total++;
            p2combo++;
        }
        else
        {
            p1Destroyed = false;
            p2Destroyed = false;
        }
    }
    void ScoreBoard()
    {
        projectileCountdown -= Time.deltaTime;
        comboCountdown -= Time.deltaTime;

        if (projectileCountdown <= 0.0f)
        {
            if (p1combo >= 2 && p2combo >= 2)
            {
                pPoint = false;
                sCombo = false;
                sdCombo = true;

                superduperCombo++;
                superduperTotal++;
            }
            else if (p1combo >= 1 && p2combo >= 1)
            {
                pPoint = false;
                sCombo = true;
                sdCombo = false;

                superCombo++;
                superTotal++;
            }
            else if (p1combo >= 1 && p2combo == 0 || p1combo == 0 && p2combo >= 1)
            {
                pPoint = true;
                sCombo = false;
                sdCombo = false;
            }

            p1combo = 0;
            p2combo = 0;
            projectileCountdown = threesecCounter;
        }

        if(comboCountdown <= 0.0f)
        {
            if(superCombo >= 2 || superduperCombo >= 2)
            {
                cStack = true;
            }
            else
            {
                cStack = false;
            }
            superduperCombo = 0;
            superCombo = 0;
            comboCountdown = tensecCounter;
        }

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
            standaloneCounter -= Time.deltaTime * speed;
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
                DisableAllGestures();
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
                DisableAllGestures();
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
