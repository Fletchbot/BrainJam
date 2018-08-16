using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalibrationStateManager : MonoBehaviour
{
    [Header("Text Section")]
    public GameObject timerText, PickOneText;
    Text text;
    [Header("Timer Section")]
    public float counter;
    public float speed = 1;
    [Header("Calibration Section")]
    public int state;
    public bool runCalibration, paused, statechange, mDTW, fDTW, eDTW, eSVM;
    [Header("Calibration Seq Section")]
    public bool Meditate, Emotion, Focus, MeditateFocus, MeditateEmo, FocusEmo, AllSelected;
    public Color32 MCol, ECol, FCol, recColor, finishColor;
    public bool mFin, eFin, fFin, cancel;
    private Color32 OffColor, OnColor;
    private int _mClick, _eClick, _fClick, narrator;
    private bool PickText;
    private float thirtySecTimer, fifteenSecTimer;


    // Use this for initialization
    public void OnEnable()
    {
        text = timerText.GetComponent<Text>();
        state = -1;
        thirtySecTimer = 30.4f;
        fifteenSecTimer = 15.4f;
        colorReset();
        Reset();
    } 
    public void Update()
    {
        _stateChanger();
        _counter();
        _calibrationSelector();
        _calibrateReset();
    }

    public void Reset()
    {
        _mClick = 0;
        _eClick = 0;
        _fClick = 0;

        counter = thirtySecTimer;
        statechange = true;
    }
    public void _calibrateReset()
    {
        if (mFin)
        {
            Meditate = false;
        }
        if (eFin)
        {
            Emotion = false;
        }
        if (fFin)
        {
            Focus = false;
        }
        if (mFin && eFin)
        {
            MeditateEmo = false;
        }
        if (mFin && fFin)
        {
            MeditateFocus = false;
        }
        if (fFin && eFin)
        {
            FocusEmo = false;
        }
        if (mFin && fFin && eFin)
        {
            AllSelected = false;
        }
    }
    public void colorReset()
    {
        OffColor = new Color32(0, 201, 255, 255);
        OnColor = new Color32(0, 255, 155, 255);
        recColor = new Color32(219, 39, 32, 255);
        finishColor = new Color32(250, 189, 95, 255);

        MCol = OffColor;
        ECol = OffColor;
        FCol = OffColor;

    }

    public void _counter()
    {
            if (state == 12 || state == -1) //menu
            {
            mDTW = false;
            fDTW = false;
            eDTW = false;
            eSVM = false;

            text.text = "";
            }
            else if (paused == false)
            {
                counter -= Time.deltaTime * speed;
                if (counter <= 0)
                {
                    counter = 0;
                    string seconds = Mathf.Round(counter % 60).ToString("00");
                    string minutes = Mathf.Round((counter % 3600) / 60).ToString("00");
                }
                else
                {
                    string minutes = Mathf.Floor((counter % 3600) / 60).ToString("00");
                    string seconds = (counter % 60).ToString("00");
                    text.text = minutes + ":" + seconds;
                }
            }
    }
    public void _stateChanger()
    {
        statechange = false;

        if (state == 0)     //state 0 = Narrator Intro
        {
            narrator = 1;
            if (counter == 30)
            {
                Reset();
            }
            else if (counter <= 0) //state 1 = Narrator Meditate open
            {
                narrator = 2;
                state++;
                statechange = true;

                if (Meditate || MeditateEmo || MeditateFocus)
                {
                    counter = 67.0f;
                }
                else if (AllSelected)
                {
                    counter = 47.0f;
                }
            }
        }
        else if (state == 1 || state == 3)
        {
            if (counter <= 0) //state 2/ state 4 Meditate Closed/Open Wek
            {
                mDTW = true;
                fDTW = false;
                eDTW = false;
                eSVM = true;

                state++;
                counter = thirtySecTimer;
                statechange = true;
            }
        }
        else if (state == 2)
        {
            if (counter <= 0) //state 3 = Narrator Meditate closed
            {
                narrator = 3;
                state++;
                counter = 40.0f;
                statechange = true;
            }
        }
        else if (state == 4)
        {
            if (counter <= 0) //state 5 = Narrator Focus
            {
                if (MeditateEmo || Meditate)
                {
                    statechange = true;
                    state = -1;
                    mFin = true;
                    Invoke("Stop_RunCalibration", 1.0f);
                }
                else if (AllSelected || FocusEmo || MeditateFocus)
                {
                    narrator = 4;
                    state++;
                    counter = 32.0f;
                    statechange = true;
                    mFin = true;
                }
                else if (Focus)
                {
                    narrator = 4;
                    state++;
                    counter = 52.0f;
                    statechange = true;
                }
            }
        }
        else if (state == 5)
        {
            if (counter <= 0) //state 6 =  focus  wek
            {
                mDTW = false;
                fDTW = true;
                eDTW = false;
                eSVM = true;    

                state++;
                counter = fifteenSecTimer;
                statechange = true;
            }
        }
        else if (state == 6)
        {
            if (counter <= 0)    // Narrator Emotions Happy
            {
                if (Focus || MeditateFocus)
                {
                    state = -1;
                    fFin = true;
                    statechange = true;
                    Invoke("Stop_RunCalibration", 1.0f);
                }
                else if (Emotion || MeditateEmo)
                {
                    narrator = 5;
                    state++;
                    counter = 62.0f;
                    statechange = true;

                }
                else if (AllSelected || FocusEmo)
                {
                    narrator = 5;
                    state++;
                    counter = 42.0f;
                    statechange = true;
                    fFin = true;
                }
            }
        }
        else if (state == 7 || state == 9 )
        {
            if (counter <= 0)  //state 8/10 = emotions happy sad closed wek
            {
                mDTW = false;
                fDTW = false;
                eDTW = true;
                eSVM = true;

                state++;
                counter = thirtySecTimer;
                statechange = true;
            }
        }
        else if (state == 8)
        {
            if (counter <= 0)  //state 9 = Narrator Emotions Sad
            {
                narrator = 6;
                state++;
                counter = 42.0f;
                statechange = true;
            }
        }
        else if (state == 10)
        {
            if (counter <= 0) //state 11 = Narrator End Menu
            {
                if (Emotion || MeditateEmo || FocusEmo)
                {
                    statechange = true;
                    state = -1;
                    eFin = true;
                    Invoke("Stop_RunCalibration", 1.0f);
                }
                else
                { 
                    state++;
                    counter = 0.0f;
                    statechange = true;
                    eFin = true;
                }
            }
        }
        else if (state == 13) //skip menu
        {
            if (counter <= 0)
            {
                if (narrator == 1)
                {
                    counter = 0;
                    state = 1;
                }
                else if (narrator == 2)
                {
                    counter = 0;
                    state = 1;
                }
                else if (narrator == 3)
                {
                    counter = 0;
                    state = 3;

                }
                else if (narrator == 4)
                {
                    counter = 0;
                    state = 5;
                }
                else if (narrator == 5)
                {
                    counter = 0;
                    state = 7;
                }
                else if (narrator == 6)
                {
                    counter = 0;
                    state = 9;
                }
                else if (narrator == 7)
                {
                    counter = 0;
                    state = 11;
                }
            }
        }
    }
    public void _calibrationSelector()
    {
        if (Meditate && Emotion && Focus)
        {
            MeditateEmo = false;
            MeditateFocus = false;
            FocusEmo = false;
            AllSelected = true;
        }
        else if (Meditate == true && Emotion == false && Focus == false)
        {
            MeditateEmo = false;
            MeditateFocus = false;
            FocusEmo = false;
            AllSelected = false;
        }
        else if (Meditate == false && Emotion == true && Focus == false)
        {
            MeditateEmo = false;
            MeditateFocus = false;
            FocusEmo = false;
            AllSelected = false;
        }
        else if (Meditate == false && Emotion == false && Focus == true)
        {
            MeditateEmo = false;
            MeditateFocus = false;
            FocusEmo = false;
            AllSelected = false;
        }
        else if (Meditate == true && Emotion == true && Focus == false)
        {
            MeditateEmo = true;
            MeditateFocus = false;
            FocusEmo = false;
            AllSelected = false;
        }
        else if (Meditate == true && Emotion == false && Focus == true)
        {
            MeditateEmo = false;
            MeditateFocus = true;
            FocusEmo = false;
            AllSelected = false;
        }
        else if (Meditate == false && Emotion == true && Focus == true)
        {
            MeditateEmo = false;
            MeditateFocus = false;
            FocusEmo = true;
            AllSelected = false;
        }
        else if (Meditate == true && Emotion == true && Focus == true)
        {
            MeditateEmo = false;
            MeditateFocus = false;
            FocusEmo = false;
            AllSelected = true;
        }

        if (cancel)
        {
            AllSelected = false;
            Meditate = false;
            Emotion = false;
            Focus = false;
            MeditateEmo = false;
            MeditateFocus = false;
            FocusEmo = false;
            cancel = false;
        }
    }
    public void _calibrationDispatcher()
    {
        if (Meditate && !MeditateEmo && !MeditateFocus && !AllSelected)
        {
            counter = 0;
            state = 0;
        }
        else if (MeditateEmo || MeditateFocus)
        {
            Meditate = false;
            Emotion = false;
            counter = 0;
            state = 0;
        }
        else if (Focus && !FocusEmo && !MeditateFocus && !AllSelected)
        {
            counter = 0;
            state = 4;
        }
        else if (FocusEmo)
        {
            Emotion = false;
            Focus = false;
            counter = 0;
            state = 4;
        }
        else if (Emotion && !FocusEmo && !MeditateEmo)
        {
            counter = 0;
            state = 6;
        }

        if (AllSelected)
        {
            counter = 30;
            state = 0;
            Meditate = false;
            Focus = false;
            Emotion = false;
            MeditateEmo = false;
            MeditateFocus = false;
            FocusEmo = false;

        }
        runCalibration = false;

    }

    public void ClickPlay(bool play)
    {
        if (play) paused = false;
    }
    public void ClickPause(bool pause)
    {
        if (pause) paused = true;
    }
    public void ClickSkip(bool skip)
    {
        if (skip)
        {
            counter = 10.5f;
            state = 13;
        }
    }
    public void ClickBack(bool back)
    {
        if (back == true)
        {
            state -= 2;
            counter = 0;
            paused = false;
        }
    }
    public void ClickCancel(bool _c)
    {
        if (_c)
        {
            cancel = true;
            state = -1;
            counter = 0;
            Reset();
            runCalibration = false;
        }

    }
    public void ClickMeditate(bool _m)
    {
        if (!runCalibration)
        {
            _mClick++;
            if (_mClick == 1)
            {
                MCol = OnColor;
                Meditate = _m;
                PickTextDisable();
            }
            else if (_mClick == 2)
            {
                MCol = OffColor;
                Meditate = false;
                _mClick = 0;
            }
        }
    }
    public void ClickFocus(bool _f)
    {
        if (!runCalibration)
        {
            _fClick++;

            if (_fClick == 1)
            {
                FCol = OnColor;
                Focus = _f;
                PickTextDisable();
            }
            else if (_fClick == 2)
            {
                FCol = OffColor;
                Focus = false;
                _fClick = 0;
            }
        }
    }
    public void ClickEmo(bool _e)
    {
        if (!runCalibration)
        {
            _eClick++;
            if (_eClick == 1)
            {
                ECol = OnColor;
                Emotion = _e;
                PickTextDisable();
            }
            else if (_eClick == 2)
            {
                ECol = OffColor;
                Emotion = false;
                _eClick = 0;
            }
        }
    }

    public void ClickRun(bool run)
    {
        if (run)
        {
            if (Meditate || Emotion || Focus || MeditateEmo || MeditateFocus || FocusEmo || AllSelected)
            {
                _calibrationDispatcher();
                runCalibration = true;
            }
            else
            {
                runCalibration = false;
                PickText = true;
                PickOneText.GetComponent<ActivateObjects>().SetActive(PickText);
            }
        }
    }
    private void PickTextDisable()
    {
        if (PickText)
        {
            PickOneText.GetComponent<ActivateObjects>().SetDeactive(PickText);
            PickText = false;
        }
    }

    private void Stop_RunCalibration()
    {
        runCalibration = false;
    }
}

