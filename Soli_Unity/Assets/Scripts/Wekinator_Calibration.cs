using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Wekinator_Calibration : MonoBehaviour
{
    public GameObject playtestButtonText;

    public GameObject Meditate_Rec, Focus_Rec, dtwEmotions_Rec, svmEmotions_Rec, svmEmotions_class;
    public GameObject StateManager;
    public GameObject[] Icons;
    public AudioSource[] CalAudio;

    public bool RunCalibration, isPaused, resume, statechange;
    public bool recOn, stopRec;

    //DTW && SVM REC
    public float waitEpoch, fullEpoch, halfEpoch, counter, svmClass;
    public int gesture, epochStart;
    
    // Calibration SEQ
    private int state, sceneLoader;
    private Color MedCol, EmoCol, focusCol, recCol, finCol, waitCol;
    private bool _M, _E, _F, _ME, _MF, _FE, _All;
    private bool mRec, eRec, fRec, colorRec, mFin, eFin, fFin;
    private Vector3 Middle, Left, Right;
    private bool playtestButtonTxt;

    private bool mDTW, fDTW, eDTW, eSVM;

    public void Start()
    {
        waitEpoch = 1.0f;
        halfEpoch = 15.0f;
        fullEpoch = 30.0f;
        counter = waitEpoch;
        sceneLoader = 2;
    }

    public void Update()
    {
        mDTW = StateManager.GetComponent<CalibrationStateManager>().mDTW;
        fDTW = StateManager.GetComponent<CalibrationStateManager>().fDTW;
        eDTW = StateManager.GetComponent<CalibrationStateManager>().eDTW;
        eSVM = StateManager.GetComponent<CalibrationStateManager>().eSVM;

        RunCalibration = StateManager.GetComponent<CalibrationStateManager>().runCalibration;

        if (RunCalibration)
        {
            PlaybackUpdate();
        }

        TriggerEpoch();

        deactivateMindStateIcons();
        changeIconColor();

        isCompleted();
    }

    public void PlaybackUpdate()
    {
        state = StateManager.GetComponent<CalibrationStateManager>().state;
        statechange = StateManager.GetComponent<CalibrationStateManager>().statechange;

        if (state == -1) //main window
        {
            if (statechange)
            {
                stopAllAudio();
                CalAudio[1].GetComponent<AudioSource>().Play();
                epochStart = 0;
            }

            MenuIconsSW(); 
        }
        else if (state == 0) //Narrator Intro
        {
            if (statechange)
            {
                CalAudio[2].GetComponent<AudioSource>().Play();
                epochStart = 0;

                NarratorIconsSW();
            }

            if (isPaused == true && resume == false)
            {
                CalAudio[2].GetComponent<AudioSource>().Pause();
                pauseIconSW();
            }
            else if (resume == true && isPaused == true)
            {
                CalAudio[2].GetComponent<AudioSource>().Play();
                resumeIconSW();
            }

        }
        else if (state == 1) //Narrator Meditate closed
        {
            if (statechange)
            {
                if (_M || _ME || _MF)
                {
                    CalAudio[14].GetComponent<AudioSource>().Play();
                    epochStart = 0;

                    NarratorIconsSW();
                }
                else if (_All)
                {
                    CalAudio[3].GetComponent<AudioSource>().Play();
                    epochStart = 0;
                }
            }

            if (isPaused == true && resume == false)
            {
                if (_M || _ME || _MF)
                {
                    CalAudio[14].GetComponent<AudioSource>().Pause();
                    pauseIconSW();
                }
                else if (_All)
                {
                    CalAudio[3].GetComponent<AudioSource>().Pause();
                    pauseIconSW();
                }
            }
            else if (resume == true && isPaused == true)
            {
                if (_M || _ME || _MF)
                {
                    CalAudio[14].GetComponent<AudioSource>().Play();
                    resumeIconSW();
                }
                else if (_All)
                {
                    CalAudio[3].GetComponent<AudioSource>().Play();
                    resumeIconSW();
                }
            }
        }
        else if (state == 3) //Narrator meditate open
        {
            if (statechange)
            {
                CalAudio[1].GetComponent<AudioSource>().Play();
                CalAudio[4].GetComponent<AudioSource>().Play();

                epochStart = 0;

                NarratorIconsSW();
            }

            if (isPaused == true && resume == false)
            {
                CalAudio[4].GetComponent<AudioSource>().Pause();
                pauseIconSW();
            }
            else if (resume == true && isPaused == true)
            {
                CalAudio[4].GetComponent<AudioSource>().Play();
                resumeIconSW();
            }
        }
        else if (state == 2 || state == 4)//meditation eyes closed/open g1 port 6448
        {
            if (statechange)
            {
                counter = waitEpoch;
                svmClass = 1.0f;
                gesture = 1;
                mRec = true;
                epochStart = 1;

                CalibrateIconsSW();

                CalAudio[0].GetComponent<AudioSource>().Play();
            }

            if (stopRec == true)
            {
                deactivateRecIcons();
                activateBackIcon();
                mRec = false;
                epochStart = 0;
            }
        }
        else if (state == 5) //Narrator Focus closed
        { 
            if (statechange)
            {
                if (_MF || _All)
                {
                    CalAudio[1].GetComponent<AudioSource>().Play();
                    CalAudio[16].GetComponent<AudioSource>().Play();
                    epochStart = 0;

                    NarratorIconsSW();

                }
                else if (_F || _FE)
                {
                    CalAudio[1].GetComponent<AudioSource>().Play();
                    CalAudio[16].GetComponent<AudioSource>().Play();
                    epochStart = 0;

                    NarratorIconsSW();

                }
            }

            if (isPaused == true && resume == false)
            {
                if (_MF || _All)
                {
                    CalAudio[16].GetComponent<AudioSource>().Pause();
                    pauseIconSW();
                }
                else if (_F || _FE)
                {
                    CalAudio[16].GetComponent<AudioSource>().Pause();
                    pauseIconSW();
                }

            }
            else if (resume == true && isPaused == true)
            {
                if (_MF || _All)
                {
                    CalAudio[16].GetComponent<AudioSource>().Play();
                    resumeIconSW();
                }
                else if (_F || _FE)
                {
                    CalAudio[16].GetComponent<AudioSource>().Play();
                    resumeIconSW();
                }
            }
        }
        else if (state == 6) //focus wek
        {
            if (statechange)
            {
                counter = waitEpoch;
                svmClass = 1.0f;
                gesture = 1;
                fRec = true;
                epochStart = 1;

                CalAudio[0].GetComponent<AudioSource>().Play();

                CalibrateIconsSW();
            }

            if (stopRec == true)
            {
                deactivateRecIcons();
                activateBackIcon();
                fRec = false;
                epochStart = 0;
            }
        }
        else if (state == 7) //Narrator Emotions Happy
        {
            if (statechange)
            {
                if (_E || _FE)
                {
                    CalAudio[1].GetComponent<AudioSource>().Play();
                    CalAudio[15].GetComponent<AudioSource>().Play();
                    epochStart = 0;

                    NarratorIconsSW();
                } else if (_All || _ME)
                {
                    CalAudio[1].GetComponent<AudioSource>().Play();
                    CalAudio[5].GetComponent<AudioSource>().Play();
                    epochStart = 0;

                    NarratorIconsSW();
                }
            }

            if (isPaused == true && resume == false)
            {
                if (_E || _FE)
                {
                    CalAudio[15].GetComponent<AudioSource>().Pause();
                    pauseIconSW();
                }
                else if (_All || _ME)
                {
                    CalAudio[5].GetComponent<AudioSource>().Pause();
                    pauseIconSW();
                }                  
            }
            else if (resume == true && isPaused == true)
            {
                if (_E || _FE)
                {
                    CalAudio[15].GetComponent<AudioSource>().Play();
                    resumeIconSW();
                }
                else if (_All || _ME)
                {
                    CalAudio[5].GetComponent<AudioSource>().Play();
                    resumeIconSW();
                }
            }
        }
        else if (state == 9) //Narrator Emotions Sad closed
        {
            if (statechange)
            {
                CalAudio[1].GetComponent<AudioSource>().Play();
                CalAudio[7].GetComponent<AudioSource>().Play();

                AudioHelmCalibrationManager.main.DroneDisable();
                AudioHelmCalibrationManager.main.BassDisable();
                epochStart = 0;

                NarratorIconsSW();
            }

            if (isPaused == true && resume == false)
            {
                CalAudio[7].GetComponent<AudioSource>().Pause();
                pauseIconSW();
            }
            else if (resume == true && isPaused == true)
            {
                CalAudio[7].GetComponent<AudioSource>().Play();
                resumeIconSW();
            }
        }
        else if (state == 8 || state == 10) //Happy/Sad Closed  WEK
        {
            if (statechange)
            {
                if(state == 8) //Happy rec
                {
                    AudioHelmCalibrationManager.main.chords[0] = true;
                    AudioHelmCalibrationManager.main.chords[1] = false;
                    AudioHelmCalibrationManager.main.chords[2] = false;
                    counter = waitEpoch;
                    svmClass = 2.0f;
                    gesture = 1;
                }
                else if (state == 10) // Sad rec
                {
                    AudioHelmCalibrationManager.main.chords[0] = false;
                    AudioHelmCalibrationManager.main.chords[1] = true;
                    AudioHelmCalibrationManager.main.chords[2] = false;
                    counter = waitEpoch;
                    svmClass = 3.0f;
                    gesture = 2;
                }

                eRec = true;
                epochStart = 1;
                CalAudio[0].GetComponent<AudioSource>().Play();

                AudioHelmCalibrationManager.main.DroneEnable();
                AudioHelmCalibrationManager.main.BassEnable();

                CalibrateIconsSW();
            }

            if (stopRec == true)
            {
                deactivateRecIcons();
                activateBackIcon();
                eRec = false;
                epochStart = 0;
                AudioHelmCalibrationManager.main.DroneDisable();
                AudioHelmCalibrationManager.main.BassDisable();
            }
        }
        else if (state == 11) //Narrator End (MENU)
        {
            if (statechange)
            {
                AudioHelmCalibrationManager.main.DroneDisable();
                AudioHelmCalibrationManager.main.BassDisable();

                CalAudio[13].GetComponent<AudioSource>().Play();
                CalAudio[1].GetComponent<AudioSource>().Play();

                epochStart = 0;
                MenuIconsSW();
            }
        }
        else if (state == 13) // skip menu
        {
            deactivateSkip();
            stopAllAudio();

            if (isPaused == true && resume == false)
            {
                pauseIconSW();
            }
            else if (resume == true && isPaused == true)
            {
                resumeIconSW();
            }
        }
    }

    public void TriggerEpoch()
    {
        if (state == 2 && epochStart == 1) // Meditation closed
        {
            EpochRecorder();
            colorRec = true;
        }
        else if (state == 4 && epochStart == 1) // Meditation open
        {
            EpochRecorder();
            colorRec = true;
        }
        else if (state == 6 && epochStart == 1) // Focus closed
        {
            EpochRecorder();
            colorRec = true;
        }
        else if (state == 8 && epochStart == 1) //Happy closed
        {
            EpochRecorder();
            colorRec = true;
        }
        else if (state == 10 && epochStart == 1) //Sad open
        {
            EpochRecorder();
            colorRec = true;
        }
        else if (epochStart == 0)
        {
            recOn = false;
            colorRec = false;

            Meditate_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
            Focus_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);          
            dtwEmotions_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
            svmEmotions_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);

            epochStart = -1;
        }
    }
   
    public void EpochRecorder() 
    {
        if (counter == waitEpoch || counter == fullEpoch || counter == halfEpoch)
        {
            recOn = true;

            if (mDTW)
            {
                Meditate_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().Gesture(gesture);
                Meditate_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
            }

            if (fDTW)
            {
                Focus_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().Gesture(gesture);
                Focus_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
            }

            if (eDTW)
            {
                dtwEmotions_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().Gesture(gesture);
                dtwEmotions_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
            }

            if (eSVM)
            {
                svmEmotions_class.GetComponent<UniOSC.WekEventDispatcherButton>().svm_Class(svmClass);
                svmEmotions_class.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
                svmEmotions_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
            }

            counter -= Time.deltaTime;
        }
        else if (counter <= 0)
        {
            recOn = false;

            if (mDTW)
            {
                Meditate_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
                counter = waitEpoch;
            }
            if (fDTW)
            {
                Focus_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
                counter = waitEpoch;
            }

            if (eDTW)
            {
                dtwEmotions_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
                counter = waitEpoch;
            }
            if (eSVM)
            {
                svmEmotions_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
            }
        }
        else
        {
            counter -= Time.deltaTime;
        }

    }

    private void MenuIconsSW()
    {
        activateRunIcon();
        activateMindStateIcons();

        deactivateSkip();
        deactivateNarratorIcon();
        deactivateBackIcon();
        deactivateRecIcons();
    }
    private void NarratorIconsSW()
    {
        resumeIconSW();
        activateNarratorIcon();
        activateSkip();

        deactivateRunIcon();
        deactivateBackIcon();
        deactivateRecIcons();
        deactivateRunIcon();
    }
    private void CalibrateIconsSW()
    {
        activateRecIcons();

        deactivateSkip();
        deactivateNarratorIcon();
        deactivateRunIcon();
    }

    public void Play(bool play)
    {
        if (play)
        {
            resume = true;
        }
    }
    public void Paused(bool paused)
    {
        if (paused)
        {
            isPaused = true;
        }
    }
    public void Stop(bool stop)
    {
        if (stop)
        {
            stopRec = true;
            Invoke("Switch", 0.1f);
        }
    }
    public void Cancel(bool cancel)
    {
        if (cancel)
        {
            MenuIconsSW();

            resume = false;
            state = -1;
            counter = 0;

            recOn = false;

            Meditate_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
            Focus_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
            dtwEmotions_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
            svmEmotions_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);

            stopAllAudio();
        } 
    }
    private void Switch()
    {
        if (stopRec) stopRec = false;
    }

    public void playtestButtonClick()
    {
        if (mFin || eFin || fFin)
        {
            Icons[14].GetComponent<LoadSceneOnClick>().LoadByIndex(sceneLoader);
        }
        else
        {
            playtestButtonText.GetComponent<ActivateObjects>().SetActive(true);
            playtestButtonTxt = true;
        }
    }
    public void deactivatePlaytestButtonText()
    {
        if (playtestButtonTxt)
        {
            playtestButtonText.GetComponent<ActivateObjects>().SetDeactive(true);
            playtestButtonTxt = false;
        }
    }

    private void activateNarratorIcon()
    {
        Icons[0].GetComponent<ActivateObjects>().SetActive(true);
    }
    private void deactivateNarratorIcon()
    {
        Icons[0].GetComponent<ActivateObjects>().SetDeactive(true);
    }

    private void resumeIconSW()
    {
        Icons[1].GetComponent<ActivateObjects>().SetDeactive(true);
        Icons[2].GetComponent<ActivateObjects>().SetActive(true);

        isPaused = false;
        resume = false;
    }
    private void pauseIconSW()
    {
        Icons[2].GetComponent<ActivateObjects>().SetDeactive(true);
        Icons[1].GetComponent<ActivateObjects>().SetActive(true);
    }

    private void activateRecIcons()
    {
        Icons[3].GetComponent<ActivateObjects>().SetActive(true);
        Icons[4].GetComponent<ActivateObjects>().SetActive(true);

        Icons[2].GetComponent<ActivateObjects>().SetDeactive(true);
    }
    private void deactivateRecIcons()
    {
        Icons[3].GetComponent<ActivateObjects>().SetDeactive(true);
        Icons[4].GetComponent<ActivateObjects>().SetDeactive(true);
    }

    private void activateBackIcon()
    {
        Icons[5].GetComponent<ActivateObjects>().SetActive(true);
    }
    private void deactivateBackIcon()
    {
        Icons[5].GetComponent<ActivateObjects>().SetDeactive(true);
    }

    private void activateRunIcon()
    {
        Icons[10].GetComponent<ActivateObjects>().SetActive(true);
    }
    private void deactivateRunIcon()
    {
        Icons[10].GetComponent<ActivateObjects>().SetDeactive(true);
    }

    private void activateSkip()
    {
        Icons[13].GetComponent<ActivateObjects>().SetActive(true);
    }
    private void deactivateSkip()
    {
        Icons[13].GetComponent<ActivateObjects>().SetDeactive(true);
    }

    private void activateMindStateIcons()
    {
        Middle = new Vector3(0, -280, 0);
        Left = new Vector3(-200, -240, 0);
        Right = new Vector3(200, -240, 0);

        Icons[7].GetComponent<RectTransform>().localPosition = Left;
        Icons[9].GetComponent<RectTransform>().localPosition = Middle;
        Icons[8].GetComponent<RectTransform>().localPosition = Right;

        Icons[7].GetComponent<ActivateObjects>().SetActive(true);
        Icons[8].GetComponent<ActivateObjects>().SetActive(true);
        Icons[9].GetComponent<ActivateObjects>().SetActive(true);
        Icons[12].GetComponent<ActivateObjects>().SetActive(true);
        Icons[11].GetComponent<ActivateObjects>().SetDeactive(true);
    }
    private void deactivateMindStateIcons()
    {
        _M = StateManager.GetComponent<CalibrationStateManager>().Meditate;
        _E = StateManager.GetComponent<CalibrationStateManager>().Emotion;
        _F = StateManager.GetComponent<CalibrationStateManager>().Focus;
        _ME = StateManager.GetComponent<CalibrationStateManager>().MeditateEmo;
        _MF = StateManager.GetComponent<CalibrationStateManager>().MeditateFocus;
        _FE = StateManager.GetComponent<CalibrationStateManager>().FocusEmo;
        _All = StateManager.GetComponent<CalibrationStateManager>().AllSelected;

        if (RunCalibration)
        {
            Middle = new Vector3(0, -280, 0);
            Left = new Vector3(-80, -280, 0);
            Right = new Vector3(80, -280, 0);

            if (_M)
            {
                Icons[7].GetComponent<RectTransform>().localPosition = Middle;
                Icons[8].GetComponent<ActivateObjects>().SetDeactive(true);
                Icons[9].GetComponent<ActivateObjects>().SetDeactive(true);
                Icons[12].GetComponent<ActivateObjects>().SetDeactive(true);
                Icons[11].GetComponent<ActivateObjects>().SetActive(true);
            }
            else if (_MF)
            {
                Icons[7].GetComponent<RectTransform>().localPosition = Left;
                Icons[9].GetComponent<RectTransform>().localPosition = Right;
                Icons[8].GetComponent<ActivateObjects>().SetDeactive(true);
                Icons[12].GetComponent<ActivateObjects>().SetDeactive(true);
                Icons[11].GetComponent<ActivateObjects>().SetActive(true);
            }
            else if (_ME)
            {
                Icons[7].GetComponent<RectTransform>().localPosition = Left;
                Icons[8].GetComponent<RectTransform>().localPosition = Right;
                Icons[9].GetComponent<ActivateObjects>().SetDeactive(true);
                Icons[12].GetComponent<ActivateObjects>().SetDeactive(true);
                Icons[11].GetComponent<ActivateObjects>().SetActive(true);
            }
            else if (_E)
            {
                Icons[8].GetComponent<RectTransform>().localPosition = Middle;
                Icons[7].GetComponent<ActivateObjects>().SetDeactive(true);
                Icons[9].GetComponent<ActivateObjects>().SetDeactive(true);
                Icons[12].GetComponent<ActivateObjects>().SetDeactive(true);
                Icons[11].GetComponent<ActivateObjects>().SetActive(true);
            }
            else if (_FE)
            {
                Icons[9].GetComponent<RectTransform>().localPosition = Left;
                Icons[8].GetComponent<RectTransform>().localPosition = Right;
                Icons[7].GetComponent<ActivateObjects>().SetDeactive(true);
                Icons[12].GetComponent<ActivateObjects>().SetDeactive(true);
                Icons[11].GetComponent<ActivateObjects>().SetActive(true);
            }
            else if (_F)
            {
                Icons[9].GetComponent<RectTransform>().localPosition = Middle;
                Icons[7].GetComponent<ActivateObjects>().SetDeactive(true);
                Icons[8].GetComponent<ActivateObjects>().SetDeactive(true);
                Icons[12].GetComponent<ActivateObjects>().SetDeactive(true);
                Icons[11].GetComponent<ActivateObjects>().SetActive(true);
            }
            else if (_All)
            {
                Icons[12].GetComponent<ActivateObjects>().SetDeactive(true);
                Icons[11].GetComponent<ActivateObjects>().SetActive(true);
            }
        }
    }

    public void changeIconColor()
    {
        mFin = StateManager.GetComponent<CalibrationStateManager>().mFin;
        fFin = StateManager.GetComponent<CalibrationStateManager>().fFin;
        eFin = StateManager.GetComponent<CalibrationStateManager>().eFin;

        recCol = StateManager.GetComponent<CalibrationStateManager>().recColor;
        finCol = StateManager.GetComponent<CalibrationStateManager>().finishColor;
        waitCol = new Color32(255, 255, 255, 255);

        if (!colorRec || state == -1)
        {
            if (!mFin)
            {
                MedCol = StateManager.GetComponent<CalibrationStateManager>().MCol;
            }
            else if (mFin && statechange)
            {
                if (mFin && fFin)
                {
                    MedCol = finCol;
                    focusCol = finCol;
                }
                else
                {
                    MedCol = finCol;
                }
            }

            if (!fFin)
            {
                focusCol = StateManager.GetComponent<CalibrationStateManager>().FCol;
            }
            else if (fFin && statechange)
            {
                focusCol = finCol;
            }

            if (!eFin)
            {
                EmoCol = StateManager.GetComponent<CalibrationStateManager>().ECol;
            }
            else if (eFin && statechange)
            {
                EmoCol = finCol;
            }
        }
        else if (colorRec)
        {
            if (mRec && !mFin)
            {
                MedCol = recCol;
            }
            if (fRec && !fFin)
            {
                focusCol = recCol;
            } 
            if (eRec && !eFin)
            {
                EmoCol = recCol;
            }
        }
        if (RunCalibration)
        {
            if (_MF && !mFin)
            {
                focusCol = waitCol;
            }
            if (_ME && !mFin)
            {
                EmoCol = waitCol;
            }
            if (_FE && !fFin)
            {
                EmoCol = waitCol;
            }

            if (_All && !mFin)
            {
                focusCol = waitCol;
                EmoCol = waitCol;
            }
            else if (_All && mFin && !fFin)
            {
                EmoCol = waitCol;
            }
        }

        Icons[7].GetComponent<Image>().color = MedCol;
        Icons[8].GetComponent<Image>().color = EmoCol;
        Icons[9].GetComponent<Image>().color = focusCol;
    }

    public void stopAllAudio()
    {
        for (int i = 0; i < CalAudio.Length; i++)
        {
            CalAudio[i].GetComponent<AudioSource>().Stop();
        }
        AudioHelmCalibrationManager.main.DroneDisable();
        AudioHelmCalibrationManager.main.BassDisable();
    }

    public void isCompleted()
    {
        if(mFin || eFin || fFin)
        {
            Icons[15].GetComponent<Image>().color = Color.green;     
        }
        else
        {
            Icons[15].GetComponent<Image>().color = new Color32(219,39,32,255);
        }
    }
    //NOT BEING USED
 /*   public void DeleteExamples()
    {
        deleteExamples = true;
        DTWSolo_Delete.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(deleteExamples);
        DTWMulti_Delete.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(deleteExamples);
        SVMSolo_Delete.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(deleteExamples);
        deleteExamples = false;
    }*/
}


