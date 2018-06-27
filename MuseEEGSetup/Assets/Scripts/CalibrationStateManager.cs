using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalibrationStateManager : MonoBehaviour {
    public GameObject timerText, PickOneText;
    Text text;
    public float counter;
    public float speed = 1;
    public int state;
    public bool runCalibration, paused, statechange;
    public bool Meditate, Emotion, Audio, MeditateAudio, MeditateEmo, EmotionAudio, AllSelected;
    public Color32 MCol, ECol, ACol;
    private Color32 OffColor, OnColor;
    private int _mClick, _eClick, _aClick;
    private bool PickText;

    // Use this for initialization
    public void OnEnable()
    {
        text = timerText.GetComponent<Text>();
        state = -1;
        Reset();
    }
    public void Update()
    {
        _stateChanger();
        _counter();
        _calibrationSelector();
    }

    public void Reset()
    {
        OffColor = new Color32(0, 201, 255, 255);
        OnColor = new Color32(0, 255, 155, 255);
        MCol = OffColor;
        ECol = OffColor;
        ACol = OffColor;

        _mClick = 0;
        _eClick = 0;
        _aClick = 0;

        counter = 30.0f;
        statechange = true;
    }
    public void calibrateReset()
    {
        Meditate = false;
        Emotion = false;
        Audio = false;
        MeditateEmo = false;
        MeditateAudio = false;
        EmotionAudio = false;
        AllSelected = false;
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
            if (counter == 30)
            {
                Reset();
            }
            else if (counter <= 0) //state 1 = Narrator Meditation1
            {
                state++;
                counter = 47.0f;
                statechange = true;
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
        else if (state == 2)  //state 3 = Narrator Meditation2
        {
            if (counter <= 0)
            {
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
                }
                else if (MeditateAudio)
                {
                    counter = 0;
                    state = 12;
                }
                else
                {
                    state++;
                    counter = 42.0f;
                    statechange = true;
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
                    state = -1;
                }
                else
                {
                    state++;
                    counter = 32.0f;
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
                    state = -1;
                }
                else
                {
                    state++;
                    counter = 30.0f;
                    statechange = true;
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
            }

        }
    }
    public void _calibrationSelector()
    {
        if (Meditate && Emotion)
        {
            MeditateEmo = true;
            Meditate = false;
            Emotion = false;
            AllSelected = false;
        }
        else if (Meditate && Audio)
        {
            MeditateAudio = true;
            Meditate = false;
            Audio = false;
            AllSelected = false;
        }
        else if (Emotion && Audio)
        {
            EmotionAudio = true;
            Audio = false;
            Emotion = false;
            AllSelected = false;
        }
        else if (Meditate && EmotionAudio || MeditateEmo && Audio || MeditateAudio && Emotion)
        {
            AllSelected = true;
            Meditate = false;
            Emotion = false;
            Audio = false;
            MeditateEmo = false;
            MeditateAudio = false;
            EmotionAudio = false;
        }
    }
    public void _calibrationDispatcher()
    {
        if (Meditate || MeditateEmo || MeditateAudio)
        {
            counter = 0;
            state = 0;
            calibrateReset();
        }
        else if (Emotion || EmotionAudio)
        {
            counter = 0;
            state = 4;
            calibrateReset();
        }
        else if (Audio)
        {
            counter = 0;
            state = 12;
            calibrateReset();
        }
        else if (AllSelected)
        {
            state = 0;
            calibrateReset();
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
            calibrateReset();
            state = -1;
            counter = 0;
            Reset();
            runCalibration = false;
        }

    }
    public void ClickMeditate(bool _m)
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
    public void ClickEmo(bool _e)
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
    public void ClickAudio(bool _a)
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
}

