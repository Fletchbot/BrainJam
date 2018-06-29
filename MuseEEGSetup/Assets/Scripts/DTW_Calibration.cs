using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class DTW_Calibration : MonoBehaviour
{
    [Header("DTW Wekinator GameObjects")]
    public GameObject DTW_Rec, DTW_Run, DTW_Delete;
    [Header("Calibration GameObjects")]
    public GameObject StateManager;
    public GameObject[] Icons;
    public AudioSource[] CalAudio;
    [Header("Calibration Section")]
    public int state, sceneLoader;
    public bool RunCalibration, isPaused, resume, statechange;
    [Header("Calibration Seq Section")]
    private Color MedCol, EmoCol, AuCol, recCol, finCol, waitCol,lerpedCol;
    private bool _M, _E, _A, _ME, _MA, _EA, _All;
    private bool mRec, eRec, aRec, mFin, eFin, aFin;
    private Vector3 Middle, Left, Right;
    [Header("DTW Record Section")]
    public float waitEpoch = 1.5f;
    public int gesture, epochStart;
    public bool recOn, stopRec, deleteExamples;

    public void Update()
    {
        RunCalibration = StateManager.GetComponent<CalibrationStateManager>().runCalibration;
        
        DeactivateMindStateIcons();
        if (RunCalibration)
        {
            PlaybackUpdate();
        }
        changeIconColor();
        isCompleted();
    }
    public void changeIconColor()
    {
        mFin = StateManager.GetComponent<CalibrationStateManager>().mFin;
        eFin = StateManager.GetComponent<CalibrationStateManager>().eFin;
        aFin = StateManager.GetComponent<CalibrationStateManager>().aFin;

        recCol = StateManager.GetComponent<CalibrationStateManager>().recColor;
        finCol = StateManager.GetComponent<CalibrationStateManager>().finishColor;
        waitCol = new Color32(255, 255, 255, 255);

        if (!recOn || state == -1)
        {
            if (!mFin)
            {
                MedCol = StateManager.GetComponent<CalibrationStateManager>().MCol;
            } else if (mFin && statechange)
            {
                if(mFin && eFin)
                {
                    MedCol = finCol;
                    EmoCol = finCol;
                } else
                {
                    MedCol = finCol;
                }               
            }
            if (!eFin)
            {
                EmoCol = StateManager.GetComponent<CalibrationStateManager>().ECol;
            } else if (eFin && statechange)
            {
                EmoCol = finCol;
            }
            if (!aFin)
            {
                AuCol = StateManager.GetComponent<CalibrationStateManager>().ACol;
            } else if (aFin && statechange)
            {
                AuCol = finCol;
            }
        } else if (recOn)
        {
            if (mRec)
            {
                MedCol = recCol;
            } else if (eRec)
            {
                EmoCol = recCol;
            } else if (aRec)
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
            } else if (_All && mFin &&!eFin)
            {
                AuCol = waitCol;
            }
        }

        Icons[7].GetComponent<Image>().color = MedCol;
        Icons[8].GetComponent<Image>().color = EmoCol;
        Icons[9].GetComponent<Image>().color = AuCol;
    }

    public void PlaybackUpdate()
    {
        state = StateManager.GetComponent<CalibrationStateManager>().state;
        statechange = StateManager.GetComponent<CalibrationStateManager>().statechange;

        if (state == -1)
        {
            if (statechange)
            {
                stopAllAudio();
                CalAudio[1].GetComponent<AudioSource>().Play();               
            }
            Icons[10].GetComponent<ActivateObjects>().SetActive(true);
            activateMindStateIcons();
            deactivateSkip();
        }
        else if (state == 0) //Narrator Intro
        {
            if (statechange)
            {
                Icons[10].GetComponent<ActivateObjects>().SetDeactive(true);
                CalAudio[2].GetComponent<AudioSource>().Play();
                resumeIcon();
                voiceIcon();
                activateSkip();
            }

            if (isPaused == true && resume == false)
            {
                CalAudio[2].GetComponent<AudioSource>().Pause();
                pauseIcon();

            }
            else if (resume == true && isPaused == true)
            {
                CalAudio[2].GetComponent<AudioSource>().Play();
                resumeIcon();
            }

        }
        else if (state == 1) //Narrator Meditation1
        {
            if (statechange)
            {
                if(_M || _ME || _MA)
                {
                    Icons[10].GetComponent<ActivateObjects>().SetDeactive(true);
                    CalAudio[14].GetComponent<AudioSource>().Play();
                    resumeIcon();
                    voiceIcon();
                    activateSkip();
                } else if (_All)
                {
                    Icons[10].GetComponent<ActivateObjects>().SetDeactive(true);
                    CalAudio[3].GetComponent<AudioSource>().Play();
                    resumeIcon();
                    voiceIcon();
                    activateSkip();
                }
            }

            if (isPaused == true && resume == false)
            {
                if (_M || _ME || _MA)
                {
                    CalAudio[14].GetComponent<AudioSource>().Pause();
                    pauseIcon();
                } else if (_All) 
                {
                    CalAudio[3].GetComponent<AudioSource>().Pause();
                    pauseIcon();
                }
            }
            else if (resume == true && isPaused == true)
            {
                if (_M || _ME || _MA)
                {
                    CalAudio[14].GetComponent<AudioSource>().Play();
                    resumeIcon();
                }
                else if (_All)
                {
                    CalAudio[3].GetComponent<AudioSource>().Play();
                    resumeIcon();
                }
            }
        }
        else if (state == 2)//breath meditation eyes closed g1
        {
            if (statechange)
            {
                deactivateSkip();

                CalAudio[0].GetComponent<AudioSource>().Play();
                gesture = 1;
                Rec();
                mRec = true;
            }

            if (stopRec == true)
            {
                CancelCalibration();
                backIcon();
                mRec = false;
            }
        }
        else if (state == 3) //Narrator Meditation2
        {
            if (statechange)
            {
                CalAudio[1].GetComponent<AudioSource>().Play();
                CalAudio[4].GetComponent<AudioSource>().Play();
                resumeIcon();
                voiceIcon();
                activateSkip();
            }

            if (isPaused == true && resume == false)
            {
                CalAudio[4].GetComponent<AudioSource>().Pause();
                pauseIcon();
            }
            else if (resume == true && isPaused == true)
            {
                CalAudio[4].GetComponent<AudioSource>().Play();
                resumeIcon();
            }
        }
        else if (state == 4)//breath meditation eyes open g2
        {
            if (statechange)
            {
                deactivateSkip();

                CalAudio[0].GetComponent<AudioSource>().Play();
                gesture = 2;
                Rec();
                mRec = true;
            }

            if (stopRec == true)
            {
                CancelCalibration();
                backIcon();
                mRec = false;
            }
        }
        else if (state == 5) //Narrator Emotion1
        {
            if (statechange)
            {
                if (_E || _EA)
                {
                    Icons[10].GetComponent<ActivateObjects>().SetDeactive(true);
                    CalAudio[1].GetComponent<AudioSource>().Play();
                    CalAudio[15].GetComponent<AudioSource>().Play();
                    resumeIcon();
                    voiceIcon();
                    activateSkip();
                } else if (_All || _ME)
                {
                    Icons[10].GetComponent<ActivateObjects>().SetDeactive(true);
                    CalAudio[1].GetComponent<AudioSource>().Play();
                    CalAudio[5].GetComponent<AudioSource>().Play();
                    resumeIcon();
                    voiceIcon();
                    activateSkip();
                }
            }

            if (isPaused == true && resume == false)
            {
                if (_E || _EA)
                {
                    CalAudio[15].GetComponent<AudioSource>().Pause();
                    pauseIcon();
                }
                else if (_All || _ME)
                {
                    CalAudio[5].GetComponent<AudioSource>().Pause();
                    pauseIcon();
                }                  
            }
            else if (resume == true && isPaused == true)
            {
                if (_E || _EA)
                {
                    CalAudio[15].GetComponent<AudioSource>().Play();
                    resumeIcon();
                }
                else if (_All || _ME)
                {
                    CalAudio[5].GetComponent<AudioSource>().Play();
                    resumeIcon();
                }
            }
        }
        else if (state == 6) //Happy eyes closed g3
        {
            if (statechange)
            {
                deactivateSkip();

                CalAudio[0].GetComponent<AudioSource>().Play();
                gesture = 3;
                Rec();
                eRec = true;
            }

            if (stopRec == true)
            {
                CancelCalibration();
                backIcon();
                eRec = false;
            }
        }
        else if (state == 7) //Narrator Emotion2
        {
            if (statechange)
            {
                CalAudio[1].GetComponent<AudioSource>().Play();
                CalAudio[6].GetComponent<AudioSource>().Play();
                resumeIcon();
                voiceIcon();
                activateSkip();
            }

            if (isPaused == true && resume == false)
            {
                CalAudio[6].GetComponent<AudioSource>().Pause();
                pauseIcon();
            }
            else if (resume == true && isPaused == true)
            {
                CalAudio[6].GetComponent<AudioSource>().Play();
                resumeIcon();
            }
        }
        else if (state == 8)//Happy eyes open g4
        {
            if (statechange)
            {
                deactivateSkip();

                CalAudio[0].GetComponent<AudioSource>().Play();
                gesture = 4;
                Rec();
                eRec = true;
            }

            if (stopRec == true)
            {
                CancelCalibration();
                backIcon();
                eRec = false;
            }
        }
        else if (state == 9) //Narrator Emotion3
        {
            if (statechange)
            {
                CalAudio[1].GetComponent<AudioSource>().Play();
                CalAudio[7].GetComponent<AudioSource>().Play();
                resumeIcon();
                voiceIcon();
                activateSkip();
            }

            if (isPaused == true && resume == false)
            {
                CalAudio[7].GetComponent<AudioSource>().Pause();
                pauseIcon();
            }
            else if (resume == true && isPaused == true)
            {
                CalAudio[7].GetComponent<AudioSource>().Play();
                resumeIcon();
            }
        }
        else if (state == 10)//Sad eyes closed g5
        {
            if (statechange)
            {
                deactivateSkip();

                CalAudio[0].GetComponent<AudioSource>().Play();
                gesture = 5;
                Rec();
                eRec = true;
            }

            if (stopRec == true)
            {
                CancelCalibration();
                backIcon();
                eRec = false;
            }
        }
        else if (state == 11) //Narrator Emotion4
        {
            if (statechange)
            {
                CalAudio[1].GetComponent<AudioSource>().Play();
                CalAudio[8].GetComponent<AudioSource>().Play();
                resumeIcon();
                voiceIcon();
                activateSkip();
            }

            if (isPaused == true && resume == false)
            {
                CalAudio[8].GetComponent<AudioSource>().Pause();
                pauseIcon();
            }
            else if (resume == true && isPaused == true)
            {
                CalAudio[8].GetComponent<AudioSource>().Play();
                resumeIcon();
            }
        }
        else if (state == 12)//Sad eyes open g6
        {
            if (statechange)
            {
                deactivateSkip();

                CalAudio[0].GetComponent<AudioSource>().Play();
                gesture = 6;
                Rec();
                eRec = true;
            }

            if (stopRec == true)
            {
                CancelCalibration();
                backIcon();
                eRec = false;
            }
        }
        else if (state == 13) //Narrator Instrument1 closed
        {
            if (statechange)
            {
                if (_MA || _EA)
                {
                    Icons[10].GetComponent<ActivateObjects>().SetDeactive(true);
                    CalAudio[1].GetComponent<AudioSource>().Play();
                    CalAudio[16].GetComponent<AudioSource>().Play();
                    resumeIcon();
                    voiceIcon();
                    activateSkip();
                } else if (_A)
                {
                    Icons[10].GetComponent<ActivateObjects>().SetDeactive(true);
                    CalAudio[1].GetComponent<AudioSource>().Play();
                    CalAudio[17].GetComponent<AudioSource>().Play();
                    resumeIcon();
                    voiceIcon();
                    activateSkip();
                } else if (_All)
                {
                    Icons[10].GetComponent<ActivateObjects>().SetDeactive(true);
                    CalAudio[1].GetComponent<AudioSource>().Play();
                    CalAudio[9].GetComponent<AudioSource>().Play();
                    resumeIcon();
                    voiceIcon();
                    activateSkip();
                }
            }

            if (isPaused == true && resume == false)
            {
                if (_MA || _EA)
                {
                    CalAudio[16].GetComponent<AudioSource>().Pause();
                    pauseIcon();
                }
                else if (_A)
                {
                    CalAudio[17].GetComponent<AudioSource>().Pause();
                    pauseIcon();
                }
                else if (_All)
                {
                    CalAudio[9].GetComponent<AudioSource>().Pause();
                    pauseIcon();
                }

            }
            else if (resume == true && isPaused == true)
            {
                if (_MA || _EA)
                {
                    CalAudio[16].GetComponent<AudioSource>().Play();
                    resumeIcon();
                }
                else if (_A)
                {
                    CalAudio[17].GetComponent<AudioSource>().Play();
                    resumeIcon();
                }
                else if (_All)
                {
                    CalAudio[9].GetComponent<AudioSource>().Play();
                    resumeIcon();
                }
            }
        }
        else if (state == 14) //Instrument1 closed g7
        {
            if (statechange)
            {
                deactivateSkip();

                CalAudio[0].GetComponent<AudioSource>().Play();
                gesture = 7;
                Rec();
                aRec = true;

                AudioHelmCalibrationManager.main.DroneEnable();
            }

            if (stopRec == true)
            {
                CancelCalibration();
                backIcon();
                aRec = false;

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
                resumeIcon();
                voiceIcon();
                activateSkip();
            }

            if (isPaused == true && resume == false)
            {
                CalAudio[10].GetComponent<AudioSource>().Pause();
                pauseIcon();
            }
            else if (resume == true && isPaused == true)
            {
                CalAudio[10].GetComponent<AudioSource>().Play();
                resumeIcon();
            }
        }
        else if (state == 16)//Instrument1 eyes open g8
        {
            if (statechange)
            {
                deactivateSkip();

                CalAudio[0].GetComponent<AudioSource>().Play();
                gesture = 8;
                Rec();
                aRec = true;

                AudioHelmCalibrationManager.main.DroneEnable();
            }

            if (stopRec == true)
            {
                CancelCalibration();
                backIcon();
                aRec = false;

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
                resumeIcon();
                voiceIcon();
                activateSkip();
            }

            if (isPaused == true && resume == false)
            {
                CalAudio[11].GetComponent<AudioSource>().Pause();
                pauseIcon();
            }
            else if (resume == true && isPaused == true)
            {
                CalAudio[11].GetComponent<AudioSource>().Play();
                resumeIcon();
            }
        }
        else if (state == 18)//Instrument2 closed g9
        {
            if (statechange)
            {
                deactivateSkip();

                CalAudio[0].GetComponent<AudioSource>().Play();
                gesture = 9;
                Rec();
                aRec = true;

                AudioHelmCalibrationManager.main.ArpEnable();
            }

            if (stopRec == true)
            {
                CancelCalibration();
                backIcon();
                aRec = false;

                AudioHelmCalibrationManager.main.ArpDisable();
            }
        }
        else if (state == 19) //Narrator Instrument2 open
        {
            if (statechange)
            {
                AudioHelmCalibrationManager.main.ArpDisable();

                CalAudio[1].GetComponent<AudioSource>().Play();
                CalAudio[12].GetComponent<AudioSource>().Play();
                resumeIcon();
                voiceIcon();
                activateSkip();
            }

            if (isPaused == true && resume == false)
            {
                CalAudio[12].GetComponent<AudioSource>().Pause();
                pauseIcon();
            }
            else if (resume == true && isPaused == true)
            {
                CalAudio[12].GetComponent<AudioSource>().Play();
                resumeIcon();
            }
        }
        else if (state == 20)//Instrument2 open g10
        {
            if (statechange)
            {
                deactivateSkip();

                CalAudio[0].GetComponent<AudioSource>().Play();
                gesture = 10;
                Rec();
                aRec = true;

                AudioHelmCalibrationManager.main.ArpEnable();
            }

            if (stopRec == true)
            {
                CancelCalibration();
                backIcon();
                aRec = false;

                AudioHelmCalibrationManager.main.ArpDisable();
            }
        }
        else if (state == 21) //Narrator End
        {
            if (statechange)
            {
                AudioHelmCalibrationManager.main.ArpDisable();

                CalAudio[1].GetComponent<AudioSource>().Play();
                CalAudio[13].GetComponent<AudioSource>().Play();
                resumeIcon();
                voiceIcon();
                activateSkip();
            }

            if (isPaused == true && resume == false)
            {
                CalAudio[13].GetComponent<AudioSource>().Pause();
                pauseIcon();
            }
            else if (resume == true && isPaused == true)
            {
                CalAudio[13].GetComponent<AudioSource>().Play();
                resumeIcon();
            }
        }
        else if (state == 22)
        {
            if (statechange)
            {
                Icons[6].GetComponent<LoadSceneOnClick>().LoadByIndex(sceneLoader);
            }
        }
        else if (state == 23)
        {
            deactivateSkip();
            stopAllAudio();
        }
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
            Icons[10].GetComponent<ActivateObjects>().SetActive(true);
            activateMindStateIcons();
            resume = false;
            state = -1;
            CancelCalibration();
            stopAllAudio();
        }
    }
    private void Switch()
    {
        if (stopRec) stopRec = false;
    }

    IEnumerator RepeatTimer()
    {
        yield return new WaitForSeconds(waitEpoch);

        if (state == 2 && epochStart == 1)
        {
            recOn = false;
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
            recOn = true;
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
        }
        else if (state == 4 && epochStart == 1)
        {
            recOn = false;
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
            recOn = true;
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
        }
        if (state == 6 && epochStart == 1)
        {
            recOn = false;
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
            recOn = true;
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
        }
        else if (state == 8 && epochStart == 1)
        {
            recOn = false;
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
            recOn = true;
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
        }
        if (state == 10 && epochStart == 1)
        {
            recOn = false;
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
            recOn = true;
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
        }
        else if (state == 12 && epochStart == 1)
        {
            recOn = false;
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
            recOn = true;
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
        }
        if (state == 14 && epochStart == 1)
        {
            recOn = false;
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
            recOn = true;
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
        }
        else if (state == 16 && epochStart == 1)
        {
            recOn = false;
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
            recOn = true;
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
        }
        if (state == 18 && epochStart == 1)
        {
            recOn = false;
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
            recOn = true;
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
        }
        else if (state == 20 && epochStart == 1)
        {
            recOn = false;
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
            recOn = true;
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
        }
        //   yield return null;
        StartCoroutine(RepeatTimer());
    }

    public void CancelCalibration()
    {
        Icons[3].GetComponent<ActivateObjects>().SetDeactive(true);

        epochStart = 0;
        recOn = false;
        DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
        StopCoroutine(RepeatTimer());
    }

    private void voiceIcon()
    {
        StopCoroutine(RepeatTimer());

        Icons[0].GetComponent<ActivateObjects>().SetActive(true);
        Icons[3].GetComponent<ActivateObjects>().SetDeactive(true);
        Icons[4].GetComponent<ActivateObjects>().SetDeactive(true);

        epochStart = 0;
        recOn = false;
    }
    private void pauseIcon()
    {
        Icons[2].GetComponent<ActivateObjects>().SetDeactive(true);
        Icons[1].GetComponent<ActivateObjects>().SetActive(true);
    }
    private void resumeIcon()
    {
        Icons[1].GetComponent<ActivateObjects>().SetDeactive(true);
        Icons[2].GetComponent<ActivateObjects>().SetActive(true);

        isPaused = false;
        resume = false;
    }
    private void Rec()
    {
        Icons[0].GetComponent<ActivateObjects>().SetDeactive(true);
        Icons[3].GetComponent<ActivateObjects>().SetActive(true);

        Icons[2].GetComponent<ActivateObjects>().SetDeactive(true);
        Icons[4].GetComponent<ActivateObjects>().SetActive(true);


        epochStart = 1;
        recOn = true;

        StartCoroutine(RepeatTimer());

        DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().Gesture(gesture);
        DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
    }
    private void backIcon()
    {
        Icons[4].GetComponent<ActivateObjects>().SetDeactive(true);
        Icons[5].GetComponent<ActivateObjects>().SetActive(true);
    }

    public void DeleteExamples()
    {
        deleteExamples = true;
        DTW_Delete.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(deleteExamples);
        deleteExamples = false;
    }

    public void DeactivateMindStateIcons()
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
    public void activateMindStateIcons()
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
    public void activateSkip()
    {
        Icons[13].GetComponent<ActivateObjects>().SetActive(true);
    }
    public void deactivateSkip()
    {
        Icons[13].GetComponent<ActivateObjects>().SetDeactive(true);
    }
    public void stopAllAudio()
    {
        for (int i = 0; i < CalAudio.Length; i++)
        {
            CalAudio[i].GetComponent<AudioSource>().Stop();
        }
        AudioHelmCalibrationManager.main.DroneDisable();
        AudioHelmCalibrationManager.main.ArpDisable();
    }

    public void isCompleted()
    {

        if(mFin && eFin && aFin)
        {
            CalAudio[13].GetComponent<AudioSource>().Play();
            lerpedCol = Color.Lerp(Color.white, recCol, Mathf.PingPong(Time.time, 1));
            Icons[12].GetComponentInChildren<Image>().color = lerpedCol;       
        }
    }

}


