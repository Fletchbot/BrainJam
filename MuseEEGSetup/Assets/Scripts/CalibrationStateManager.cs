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
    public bool runCalibration, paused, statechange;
    [Header("Calibration Seq Section")]
    public bool Meditate, Emotion, Audio, MeditateAudio, MeditateEmo, EmotionAudio, AllSelected;
    public Color32 MCol, ECol, ACol, recColor, finishColor;
    public bool mFin, eFin, aFin, cancel;
    private Color32 OffColor, OnColor;
    private int _mClick, _eClick, _aClick, narrator;
    private bool PickText;


    // Use this for initialization
    public void OnEnable()
    {
        text = timerText.GetComponent<Text>();
        state = -1;
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
        _aClick = 0;

        counter = 30.0f;
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
        if (aFin)
        {
            Audio = false;
        }
        if (mFin && eFin)
        {
            MeditateEmo = false;
        }
        if (mFin && aFin)
        {
            MeditateAudio = false;
        }
        if (eFin && aFin)
        {
            EmotionAudio = false;
        }
        if (mFin && aFin && eFin)
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
        ACol = OffColor;

    }

    public void _counter()
    {
        if (state > -1)
        {
            if (paused == false)
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
        else
        {
            text.text = "";
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
            else if (counter <= 0) //state 1 = Narrator Meditation1
            {
                narrator = 2;
                state++;
                statechange = true;

                if (Meditate || MeditateEmo || MeditateAudio)
                {
                    counter = 67.0f;
                }
                else if (AllSelected)
                {
                    counter = 47.0f;
                }
            }
        }
        else if (state == 1)
        {
            if (counter <= 0) //state 2 = breath meditation eyes closed g1
            {
                state++;
                counter = 30.0f;
                statechange = true;
            }
        }
        else if (state == 2)
        {
            if (counter <= 0) //state 3 = Narrator Meditation2
            {
                narrator = 3;
                state++;
                counter = 40.0f;
                statechange = true;
            }
        }
        else if (state == 3)
        {
            if (counter <= 0)  //state 4 = breath meditation eyes open g2
            {
                state++;
                counter = 30.0f;
                statechange = true;
            }
        }
        else if (state == 4)
        {
            if (counter <= 0)    //state 5 = Narrator Emotion1
            {
                if (Meditate)
                {
                    state = -1;
                    mFin = true;
                    statechange = true;
                    Invoke("Stop_RunCalibration", 1.0f);
                }
                else if (MeditateAudio)
                {
                    counter = 0;
                    state = 12;
                    mFin = true;
                }
                else if (Emotion || EmotionAudio)
                {
                    narrator = 4;
                    state++;
                    counter = 62.0f;
                    statechange = true;
                }
                else if (AllSelected || MeditateEmo)
                {
                    narrator = 4;
                    state++;
                    counter = 42.0f;
                    statechange = true;
                    mFin = true;
                }
            }
        }
        else if (state == 5)
        {
            if (counter <= 0)    //state 6 = Happy eyes closed g3
            {
                state++;
                counter = 30.0f;
                statechange = true;
            }
        }
        else if (state == 6)
        {
            if (counter <= 0) //state 7 = Narrator Emotion2
            {
                narrator = 5;
                state++;
                counter = 42.0f;
                statechange = true;
            }
        }
        else if (state == 7)
        {
            if (counter <= 0)     //state 8 = Happy eyes open g4
            {
                state++;
                counter = 30.0f;
                statechange = true;
            }
        }
        else if (state == 8)
        {
            if (counter <= 0) //state 9 = Narrator Emotion3
            {
                narrator = 6;
                state++;
                counter = 42.0f;
                statechange = true;
            }
        }
        else if (state == 9)
        {
            if (counter <= 0)   //state 10 = sad eyes closed g5
            {
                state++;
                counter = 30.0f;
                statechange = true;
            }
        }
        else if (state == 10)
        {
            if (counter <= 0) //state 11 = Narrator Emotion4
            {
                narrator = 7;
                state++;
                counter = 42.0f;
                statechange = true;
            }
        }
        else if (state == 11)
        {
            if (counter <= 0)    //state 12 = sad eyes closed g6
            {
                state++;
                counter = 30.0f;
                statechange = true;
            }
        }
        else if (state == 12)
        {
            if (counter <= 0) //state 13 = Narrator Instrument1 closed
            {
                if (MeditateEmo || Emotion)
                {
                    statechange = true;
                    state = -1;
                    eFin = true;
                    Invoke("Stop_RunCalibration", 1.0f);
                }
                else if (AllSelected || EmotionAudio || MeditateAudio)
                {
                    narrator = 8;
                    state++;
                    counter = 32.0f;
                    statechange = true;
                    eFin = true;
                }
                else if (Audio)
                {
                    narrator = 8;
                    state++;
                    counter = 52.0f;
                    statechange = true;
                }
            }
        }
        else if (state == 13)
        {
            if (counter <= 0)  //state 14 = Recognise Instrument 1: eyes closed g7
            {
                state++;
                counter = 30.0f;
                statechange = true;
            }
        }
        else if (state == 14)
        {
            if (counter <= 0) //state 15 = Narrator Instrument1 open
            {
                narrator = 9;
                state++;
                counter = 30.0f;
                statechange = true;
            }
        }
        else if (state == 15)
        {
            if (counter <= 0)   //state 16 = Recognise Instrument 1: eyes open g8
            {
                state++;
                counter = 30.0f;
                statechange = true;
            }
        }
        else if (state == 16)
        {
            if (counter <= 0) //state 17 = Narrator Instrument2 closed
            {
                narrator = 10;
                state++;
                counter = 30.0f;
                statechange = true;
            }
        }
        else if (state == 17)
        {
            if (counter <= 0)  //state 18 = Recognise Instrument 2: eyes closed g9
            {
                state++;
                counter = 30.0f;
                statechange = true;
            }
        }
        else if (state == 18)
        {
            if (counter <= 0) //state 19 = Narrator Instrument2 open
            {
                narrator = 11;
                state++;
                counter = 28.0f;
                statechange = true;
            }
        }
        else if (state == 19)
        {
            if (counter <= 0)   //state 20 = Recognise Instrument 2: eyes open g10
            {
                state++;
                counter = 30.0f;
                statechange = true;
            }
        }
        else if (state == 20)
        {
            if (counter <= 0) //state 21 = Narrator End
            {
                if (Audio || MeditateAudio || EmotionAudio)
                {
                    statechange = true;
                    state = -1;
                    aFin = true;
                    Invoke("Stop_RunCalibration", 1.0f);
                }
                else if (AllSelected)
                {
                    narrator = 12;
                    state++;
                    counter = 30.0f;
                    statechange = true;
                    aFin = true;
                }
            }
        }
        else if (state == 21)
        {
            if (counter <= 0) //state 22 = Scene Switch
            {
                state++;
                counter = 0f;
                statechange = true;
                Invoke("Stop_RunCalibration", 1.0f);
            }
        }
        else if (state == 23)
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
                else if (narrator == 8)
                {
                    counter = 0;
                    state = 13;
                }
                else if (narrator == 9)
                {
                    counter = 0;
                    state = 15;
                }
                else if (narrator == 10)
                {
                    counter = 0;
                    state = 17;
                }
                else if (narrator == 11)
                {
                    counter = 0;
                    state = 19;
                }
                else if (narrator == 12)
                {
                    counter = 0;
                    state = 21;
                }
            }
        }
    }
    public void _calibrationSelector()
    {
        if (Meditate && Emotion && Audio)
        {
            MeditateEmo = false;
            MeditateAudio = false;
            EmotionAudio = false;
            AllSelected = true;
        }
        else if (Meditate == true && Emotion == false && Audio == false)
        {
            MeditateEmo = false;
            MeditateAudio = false;
            EmotionAudio = false;
            AllSelected = false;
        }
        else if (Meditate == false && Emotion == true && Audio == false)
        {
            MeditateEmo = false;
            MeditateAudio = false;
            EmotionAudio = false;
            AllSelected = false;
        }
        else if (Meditate == false && Emotion == false && Audio == true)
        {
            MeditateEmo = false;
            MeditateAudio = false;
            EmotionAudio = false;
            AllSelected = false;
        }
        else if (Meditate == true && Emotion == true && Audio == false)
        {
            MeditateEmo = true;
            MeditateAudio = false;
            EmotionAudio = false;
            AllSelected = false;
        }
        else if (Meditate == true && Emotion == false && Audio == true)
        {
            MeditateEmo = false;
            MeditateAudio = true;
            EmotionAudio = false;
            AllSelected = false;
        }
        else if (Meditate == false && Emotion == true && Audio == true)
        {
            MeditateEmo = false;
            MeditateAudio = false;
            EmotionAudio = true;
            AllSelected = false;
        }
        else if (Meditate == true && Emotion == true && Audio == true)
        {
            MeditateEmo = false;
            MeditateAudio = false;
            EmotionAudio = false;
            AllSelected = true;
        }

        if (cancel)
        {
            AllSelected = false;
            Meditate = false;
            Emotion = false;
            Audio = false;
            MeditateEmo = false;
            MeditateAudio = false;
            EmotionAudio = false;
            cancel = false;
        }
    }
    public void _calibrationDispatcher()
    {
        if (Meditate && MeditateEmo == false && MeditateAudio == false && AllSelected == false)
        {
            counter = 0;
            state = 0;
        }
        else if (MeditateEmo)
        {
            Meditate = false;
            Emotion = false;
            counter = 0;
            state = 0;
        }
        else if (MeditateAudio)
        {
            Meditate = false;
            Audio = false;
            counter = 0;
            state = 0;
        }
        else if (Emotion && EmotionAudio == false && MeditateEmo == false)
        {
            counter = 0;
            state = 4;
        }
        else if (EmotionAudio)
        {
            Emotion = false;
            Audio = false;
            counter = 0;
            state = 4;
        }
        else if (Audio && EmotionAudio == false && MeditateAudio == false && AllSelected == false)
        {
            counter = 0;
            state = 12;
        }

        if (AllSelected)
        {
            counter = 30;
            state = 0;
            Meditate = false;
            Audio = false;
            Emotion = false;
            MeditateEmo = false;
            MeditateAudio = false;
            EmotionAudio = false;

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
            state = 23;
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
    public void ClickAudio(bool _a)
    {
        if (!runCalibration)
        {
            _aClick++;

            if (_aClick == 1)
            {
                ACol = OnColor;
                Audio = _a;
                PickTextDisable();
            }
            else if (_aClick == 2)
            {
                ACol = OffColor;
                Audio = false;
                _aClick = 0;
            }
        }
    }
    public void ClickRun(bool run)
    {
        if (run)
        {
            if (Meditate || Emotion || Audio || MeditateEmo || MeditateAudio || EmotionAudio || AllSelected)
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

