using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    Text text;
    public float counter;
    public float speed = 1;
    public int state;
    public bool paused, statechange;

    // Use this for initialization
    public void OnEnable()
    {
        text = GetComponent<Text>();
        counter = 30.0f;
        statechange = true;
        state = 0;
    }

    // Update is called once per frame
    public void Update()
    {
        _stateChanger();
        _counter();
    }

    public void _counter()
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
    public void _stateChanger()
    {
        statechange = false;

        if (state == 0)     //state 0 = Narrator Intro
        {
            if (counter <= 0) //state 1 = Narrator Meditation1
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
                state++;
                counter = 42.0f;
                statechange = true;
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
                state++;
                counter = 32.0f;
                statechange = true;
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
                state++;
                counter = 30.0f;
                statechange = true;
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
}
