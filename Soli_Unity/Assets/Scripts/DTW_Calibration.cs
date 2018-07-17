using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class DTW_Calibration : MonoBehaviour
{
    public GameObject playtestButtonText;

    public GameObject DTW_Rec1, DTW_Rec2, DTW_Delete1, DTW_Delete2;
    public GameObject StateManager;
    public GameObject[] Icons;
    public AudioSource[] CalAudio;

    public bool RunCalibration, isPaused, resume, statechange;
    public bool recOn, stopRec, deleteExamples;

    //DTW REC
    public float waitEpoch, counter;
    public int gesture, epochStart;

    // Calibration SEQ
    private int state, sceneLoader;
    private Color MedCol, EmoCol, AuCol, recCol, finCol, waitCol;
    private bool _M, _E, _A, _ME, _MA, _EA, _All;
    private bool mRec, eRec, aRec, colorRec, mFin, eFin, aFin;
    private Vector3 Middle, Left, Right;
    private bool playtestButtonTxt;

    public void Start()
    {
        waitEpoch = 1.5f;
        counter = waitEpoch;
        sceneLoader = 3;
    }

    public void Update()
    {
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
        else if (state == 1) //Narrator Meditation1
        {
            if (statechange)
            {
                if(_M || _ME || _MA)
                {
                    CalAudio[14].GetComponent<AudioSource>().Play();
                    epochStart = 0;

                    NarratorIconsSW();
                } else if (_All)
                {
                    CalAudio[3].GetComponent<AudioSource>().Play();
                    epochStart = 0;
                }
            }

            if (isPaused == true && resume == false)
            {
                if (_M || _ME || _MA)
                {
                    CalAudio[14].GetComponent<AudioSource>().Pause();
                    pauseIconSW();
                } else if (_All) 
                {
                    CalAudio[3].GetComponent<AudioSource>().Pause();
                    pauseIconSW();
                }
            }
            else if (resume == true && isPaused == true)
            {
                if (_M || _ME || _MA)
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
        else if (state == 2)//breath meditation eyes closed g1
        {
            if (statechange)
            {
                counter = waitEpoch;
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
        else if (state == 3) //Narrator Meditation2
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
        else if (state == 4)//breath meditation eyes open g2
        {
            if (statechange)
            {
                counter = waitEpoch;
                gesture = 2;
                mRec = true;
                epochStart = 1;

                CalAudio[0].GetComponent<AudioSource>().Play();

                CalibrateIconsSW();
            }

            if (stopRec == true)
            {
                deactivateRecIcons();
                activateBackIcon();
                mRec = false;
                epochStart = 0;
            }
        }
        else if (state == 5) //Narrator Emotion1
        {
            if (statechange)
            {
                if (_E || _EA)
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
                if (_E || _EA)
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
                if (_E || _EA)
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
        else if (state == 6) //Happy eyes closed g3
        {
            if (statechange)
            {
                counter = waitEpoch;
                gesture = 3;
                eRec = true;
                epochStart = 1;

                CalAudio[0].GetComponent<AudioSource>().Play();

                CalibrateIconsSW();
            }

            if (stopRec == true)
            {
                deactivateRecIcons();
                activateBackIcon();
                eRec = false;
                epochStart = 0;
            }
        }
        else if (state == 7) //Narrator Emotion2
        {
            if (statechange)
            {
                CalAudio[1].GetComponent<AudioSource>().Play();
                CalAudio[6].GetComponent<AudioSource>().Play();
                epochStart = 0;

                NarratorIconsSW();
            }

            if (isPaused == true && resume == false)
            {
                CalAudio[6].GetComponent<AudioSource>().Pause();
                pauseIconSW();
            }
            else if (resume == true && isPaused == true)
            {
                CalAudio[6].GetComponent<AudioSource>().Play();
                resumeIconSW();
            }
        }
        else if (state == 8)//Happy eyes open g4
        {
            if (statechange)
            {
                counter = waitEpoch;
                gesture = 4;
                eRec = true;
                epochStart = 1;

                CalAudio[0].GetComponent<AudioSource>().Play();

                CalibrateIconsSW();
            }

            if (stopRec == true)
            {
                deactivateRecIcons();
                activateBackIcon();
                eRec = false;
                epochStart = 0;
            }
        }
        else if (state == 9) //Narrator Emotion3
        {
            if (statechange)
            {
                CalAudio[1].GetComponent<AudioSource>().Play();
                CalAudio[7].GetComponent<AudioSource>().Play();
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
        else if (state == 10)//Sad eyes closed g5
        {
            if (statechange)
            {
                counter = waitEpoch;
                gesture = 5;
                eRec = true;
                epochStart = 1;

                CalAudio[0].GetComponent<AudioSource>().Play();

                CalibrateIconsSW();
            }

            if (stopRec == true)
            {
                deactivateRecIcons();
                activateBackIcon();
                eRec = false;
                epochStart = 0;
            }
        }
        else if (state == 11) //Narrator Emotion4
        {
            if (statechange)
            {
                CalAudio[1].GetComponent<AudioSource>().Play();
                CalAudio[8].GetComponent<AudioSource>().Play();
                epochStart = 0;

                NarratorIconsSW();
            }

            if (isPaused == true && resume == false)
            {
                CalAudio[8].GetComponent<AudioSource>().Pause();
                pauseIconSW();
            }
            else if (resume == true && isPaused == true)
            {
                CalAudio[8].GetComponent<AudioSource>().Play();
                resumeIconSW();
            }
        }
        else if (state == 12)//Sad eyes open g6
        {
            if (statechange)
            {
                counter = waitEpoch;
                gesture = 6;
                eRec = true;
                epochStart = 1;

                CalAudio[0].GetComponent<AudioSource>().Play();

                CalibrateIconsSW();
            }

            if (stopRec == true)
            {
                deactivateRecIcons();
                activateBackIcon();
                eRec = false;
                epochStart = 0;
            }
        }
        else if (state == 13) //Narrator Instrument1 closed
        {
            if (statechange)
            {
                if (_MA || _EA)
                {
                    CalAudio[1].GetComponent<AudioSource>().Play();
                    CalAudio[16].GetComponent<AudioSource>().Play();
                    epochStart = 0;

                    NarratorIconsSW();

                } else if (_A)
                {
                    CalAudio[1].GetComponent<AudioSource>().Play();
                    CalAudio[17].GetComponent<AudioSource>().Play();
                    epochStart = 0;

                    NarratorIconsSW();
           
                } else if (_All)
                {
                    CalAudio[1].GetComponent<AudioSource>().Play();
                    CalAudio[9].GetComponent<AudioSource>().Play();
                    epochStart = 0;

                    NarratorIconsSW();
                }
            }

            if (isPaused == true && resume == false)
            {
                if (_MA || _EA)
                {
                    CalAudio[16].GetComponent<AudioSource>().Pause();
                    pauseIconSW();
                }
                else if (_A)
                {
                    CalAudio[17].GetComponent<AudioSource>().Pause();
                    pauseIconSW();
                }
                else if (_All)
                {
                    CalAudio[9].GetComponent<AudioSource>().Pause();
                    pauseIconSW();
                }

            }
            else if (resume == true && isPaused == true)
            {
                if (_MA || _EA)
                {
                    CalAudio[16].GetComponent<AudioSource>().Play();
                    resumeIconSW();
                }
                else if (_A)
                {
                    CalAudio[17].GetComponent<AudioSource>().Play();
                    resumeIconSW();
                }
                else if (_All)
                {
                    CalAudio[9].GetComponent<AudioSource>().Play();
                    resumeIconSW();
                }
            }
        }
        else if (state == 14) //Instrument1 closed g7
        {
            if (statechange)
            {
                counter = waitEpoch;
                gesture = 7;
                aRec = true;
                epochStart = 1;

                CalibrateIconsSW();

                CalAudio[0].GetComponent<AudioSource>().Play();
                AudioHelmCalibrationManager.main.DroneEnable();
            }

            if (stopRec == true)
            {
                deactivateRecIcons();
                activateBackIcon();
                aRec = false;
                epochStart = 0;

                AudioHelmCalibrationManager.main.DroneDisable();
            }
        }
        else if (state == 15) //Narrator Instrument1 open
        {
            if (statechange)
            {
                AudioHelmCalibrationManager.main.DroneDisable();

                CalAudio[1].GetComponent<AudioSource>().Play();
                CalAudio[10].GetComponent<AudioSource>().Play();
                epochStart = 0;

                NarratorIconsSW();
            }

            if (isPaused == true && resume == false)
            {
                CalAudio[10].GetComponent<AudioSource>().Pause();
                pauseIconSW();
            }
            else if (resume == true && isPaused == true)
            {
                CalAudio[10].GetComponent<AudioSource>().Play();
                resumeIconSW();
            }
        }
        else if (state == 16)//Instrument1 eyes open g8
        {
            if (statechange)
            {
                counter = waitEpoch;
                gesture = 8;
                aRec = true;
                epochStart = 1;

                CalibrateIconsSW();

                CalAudio[0].GetComponent<AudioSource>().Play();
                AudioHelmCalibrationManager.main.DroneEnable();
            }

            if (stopRec == true)
            {
                deactivateRecIcons();
                activateBackIcon();
                aRec = false;
                epochStart = 0;

                AudioHelmCalibrationManager.main.DroneDisable();
            }
        }
        else if (state == 17) //Narrator Instrument2 closed
        {
            if (statechange)
            {
                AudioHelmCalibrationManager.main.DroneDisable();

                CalAudio[1].GetComponent<AudioSource>().Play();
                CalAudio[11].GetComponent<AudioSource>().Play();
                epochStart = 0;

                NarratorIconsSW();
            }

            if (isPaused == true && resume == false)
            {
                CalAudio[11].GetComponent<AudioSource>().Pause();
                pauseIconSW();
            }
            else if (resume == true && isPaused == true)
            {
                CalAudio[11].GetComponent<AudioSource>().Play();
                resumeIconSW();
            }
        }
        else if (state == 18)//Instrument2 closed g9
        {
            if (statechange)
            {
                counter = waitEpoch;
                gesture = 9;
                aRec = true;
                epochStart = 1;

                CalibrateIconsSW();

                CalAudio[0].GetComponent<AudioSource>().Play();
                AudioHelmCalibrationManager.main.BassEnable();
            }

            if (stopRec == true)
            {
                deactivateRecIcons();
                activateBackIcon();
                aRec = false;
                epochStart = 0;

                AudioHelmCalibrationManager.main.BassDisable();
            }
        }
        else if (state == 19) //Narrator Instrument2 open
        {
            if (statechange)
            {
                AudioHelmCalibrationManager.main.BassDisable();

                CalAudio[1].GetComponent<AudioSource>().Play();
                CalAudio[12].GetComponent<AudioSource>().Play();
                epochStart = 0;

                NarratorIconsSW();
            }

            if (isPaused == true && resume == false)
            {
                CalAudio[12].GetComponent<AudioSource>().Pause();
                pauseIconSW();
            }
            else if (resume == true && isPaused == true)
            {
                CalAudio[12].GetComponent<AudioSource>().Play();
                resumeIconSW();
            }
        }
        else if (state == 20)//Instrument2 open g10
        {
            if (statechange)
            {
                counter = waitEpoch;
                gesture = 10;
                aRec = true;
                epochStart = 1;

                CalibrateIconsSW();

                CalAudio[0].GetComponent<AudioSource>().Play();
                AudioHelmCalibrationManager.main.BassEnable();
            }

            if (stopRec == true)
            {
                deactivateRecIcons();
                activateBackIcon();
                aRec = false;
                epochStart = 0;

                AudioHelmCalibrationManager.main.BassDisable();
            }
        }
        else if (state == 21) //Narrator End (MENU)
        {
            if (statechange)
            {
                AudioHelmCalibrationManager.main.BassDisable();

                CalAudio[13].GetComponent<AudioSource>().Play();
                CalAudio[1].GetComponent<AudioSource>().Play();

                epochStart = 0;
                MenuIconsSW();
            }
        }
        else if (state == 23) // skip menu
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
        if (state == 2 && epochStart == 1) // Meditate closed
        {
            EpochRecorder();
            colorRec = true;
        }
        else if (state == 4 && epochStart == 1) // Meditate open
        {
            EpochRecorder();
            colorRec = true;
        }
        else if (state == 6 && epochStart == 1) //Happy closed
        {
            EpochRecorder();
            colorRec = true;
        }
        else if (state == 8 && epochStart == 1) //Happy open
        {
            EpochRecorder();
            colorRec = true;
        }
        else if (state == 10 && epochStart == 1) // Sad closed
        {
            EpochRecorder();
            colorRec = true;
        }
        else if (state == 12 && epochStart == 1) //Sad open
        {
            EpochRecorder();
            colorRec = true;
        }
        else if (state == 14 && epochStart == 1) // Instr1 closed
        {
            EpochRecorder();
            colorRec = true;
        }
        else if (state == 16 && epochStart == 1) //Instr1 open
        {
            EpochRecorder();
            colorRec = true;
        }
        else if (state == 18 && epochStart == 1) //Instr2 closed
        {
            EpochRecorder();
            colorRec = true;
        }
        else if (state == 20 && epochStart == 1) //Instr2 open
        {
            EpochRecorder();
            colorRec = true;
        }
        else if (epochStart == 0)
        {
            recOn = false;
            colorRec = false;
            DTW_Rec1.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
            DTW_Rec2.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
            epochStart = -1;
        }
    }
   
    public void EpochRecorder()
    {
        if (counter == waitEpoch)
        {
            recOn = true;
            DTW_Rec1.GetComponent<UniOSC.WekEventDispatcherButton>().Gesture(gesture);
            DTW_Rec1.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
            DTW_Rec2.GetComponent<UniOSC.WekEventDispatcherButton>().Gesture(gesture);
            DTW_Rec2.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
            counter -= Time.deltaTime;
        }
        else if (counter <= 0)
        {
            recOn = false;
            DTW_Rec1.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
            DTW_Rec2.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
            counter = waitEpoch;
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
            DTW_Rec1.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
            DTW_Rec2.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);

            stopAllAudio();
        }
    }
    private void Switch()
    {
        if (stopRec) stopRec = false;
    }

    public void playtestButtonClick()
    {
        if (mFin || eFin || aFin)
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
        Icons[8].GetComponent<RectTransform>().localPosition = Middle;
        Icons[9].GetComponent<RectTransform>().localPosition = Right;

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
        _A = StateManager.GetComponent<CalibrationStateManager>().Audio;
        _ME = StateManager.GetComponent<CalibrationStateManager>().MeditateEmo;
        _MA = StateManager.GetComponent<CalibrationStateManager>().MeditateAudio;
        _EA = StateManager.GetComponent<CalibrationStateManager>().EmotionAudio;
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
            else if (_ME)
            {
                Icons[7].GetComponent<RectTransform>().localPosition = Left;
                Icons[8].GetComponent<RectTransform>().localPosition = Right;
                Icons[9].GetComponent<ActivateObjects>().SetDeactive(true);
                Icons[12].GetComponent<ActivateObjects>().SetDeactive(true);
                Icons[11].GetComponent<ActivateObjects>().SetActive(true);
            }
            else if (_MA)
            {
                Icons[7].GetComponent<RectTransform>().localPosition = Left;
                Icons[9].GetComponent<RectTransform>().localPosition = Right;
                Icons[8].GetComponent<ActivateObjects>().SetDeactive(true);
                Icons[12].GetComponent<ActivateObjects>().SetDeactive(true);
                Icons[11].GetComponent<ActivateObjects>().SetActive(true);
            }
            else if (_E)
            {
                Icons[7].GetComponent<ActivateObjects>().SetDeactive(true);
                Icons[9].GetComponent<ActivateObjects>().SetDeactive(true);
                Icons[12].GetComponent<ActivateObjects>().SetDeactive(true);
                Icons[11].GetComponent<ActivateObjects>().SetActive(true);
            }
            else if (_EA)
            {
                Icons[8].GetComponent<RectTransform>().localPosition = Left;
                Icons[9].GetComponent<RectTransform>().localPosition = Right;
                Icons[7].GetComponent<ActivateObjects>().SetDeactive(true);
                Icons[12].GetComponent<ActivateObjects>().SetDeactive(true);
                Icons[11].GetComponent<ActivateObjects>().SetActive(true);
            }
            else if (_A)
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
        eFin = StateManager.GetComponent<CalibrationStateManager>().eFin;
        aFin = StateManager.GetComponent<CalibrationStateManager>().aFin;

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
                if (mFin && eFin)
                {
                    MedCol = finCol;
                    EmoCol = finCol;
                }
                else
                {
                    MedCol = finCol;
                }
            }
            if (!eFin)
            {
                EmoCol = StateManager.GetComponent<CalibrationStateManager>().ECol;
            }
            else if (eFin && statechange)
            {
                EmoCol = finCol;
            }
            if (!aFin)
            {
                AuCol = StateManager.GetComponent<CalibrationStateManager>().ACol;
            }
            else if (aFin && statechange)
            {
                AuCol = finCol;
            }
        }
        else if (colorRec)
        {
            if (mRec && !mFin)
            {
                MedCol = recCol;
            }
            if (eRec && !eFin)
            {
                EmoCol = recCol;
            }
            if (aRec && !aFin)
            {
                AuCol = recCol;
            }
        }
        if (RunCalibration)
        {
            if (_ME && !mFin)
            {
                EmoCol = waitCol;
            }
            if (_MA && !mFin)
            {
                AuCol = waitCol;
            }
            if (_EA && !eFin)
            {
                AuCol = waitCol;
            }

            if (_All && !mFin)
            {
                EmoCol = waitCol;
                AuCol = waitCol;
            }
            else if (_All && mFin && !eFin)
            {
                AuCol = waitCol;
            }
        }

        Icons[7].GetComponent<Image>().color = MedCol;
        Icons[8].GetComponent<Image>().color = EmoCol;
        Icons[9].GetComponent<Image>().color = AuCol;
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
        if(mFin || eFin || aFin)
        {
            Icons[15].GetComponent<Image>().color = Color.green;       
        }
        else
        {
            Icons[15].GetComponent<Image>().color = new Color32(219,39,32,255);
        }
    }


    //NOT BEING USED
    public void DeleteExamples()
    {
        deleteExamples = true;
        DTW_Delete1.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(deleteExamples);
        DTW_Delete2.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(deleteExamples);
        deleteExamples = false;
    }
}


