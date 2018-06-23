using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class DTW_Calibration : MonoBehaviour
{

    public GameObject DTW_Rec, DTW_Run, DTW_Delete;
    public AudioSource calibrationAudio;
    public AudioClip[] calibrationClips;
    public float waitState, waitEpoch = 1.5f;
    public int state, gesture, epochStart;
    public bool recOn, deleteExamples;
    //state 0 = Interval - narrator
    //state 1 = breath meditation eyes closed g1
    //state 2 = Interval - narrator
    //state 3 = breath meditation eyes open g2
    //state 4 = Interval - narrator 
    //state 5 = Happy eyes closed g3
    //state 6 = Interval - narrator
    //state 7 = Happy eyes open g4
    //state 8 = Interval - narrator
    //state 9 = sad eyes closed g5
    //state 10 = Interval - narrator
    //state 11 = sad eyes open g6
    //state 12 = Interval - narrator
    //state 13 = Recognise Instrument 1: eyes closed g7
    //state 14 = Interval - narrator
    //state 15 = Recognise Instrument 1: eyes open g8
    //state 16 = Interval - narrator
    //state 17 = Recognise Instrument 2: eyes closed g9
    //state 18 = Interval - narrator
    //state 19 = Recognise Instrument 2: eyes open g9
    //state 20 = End Calibration

    // Use this for initialization
    void Start()
    {
        waitState = 30.0f;
        state = -1;
        StartCoroutine(StateChange());
        calibrationAudio.GetComponent<AudioSource>().PlayOneShot(calibrationClips[2]);
    }
    IEnumerator StateChange()
    {
        yield return new WaitForSeconds(waitState);

        state++;
       if (state == 0)//state 0 = Interval - narrator 
        {
            calibrationAudio.GetComponent<AudioSource>().PlayOneShot(calibrationClips[3]);
            waitState = 45.0f;
        }
       else if (state == 1)  //state 1 = breath meditation eyes closed g1
        {
            waitState = 30.0f;
            calibrationAudio.GetComponent<AudioSource>().PlayOneShot(calibrationClips[0]);
            recOn = true;
            gesture = 1;
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().Gesture(gesture);
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
            epochStart = 1;
            StartCoroutine(RepeatTimer());
        }
        else if (state == 2)//state 2 = Interval - narrator 
        {
            epochStart = 0;
            calibrationAudio.GetComponent<AudioSource>().PlayOneShot(calibrationClips[1]);
            calibrationAudio.GetComponent<AudioSource>().PlayOneShot(calibrationClips[4]);
            recOn = false;
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
            waitState = 40.0f;

        }
        else if (state == 3)//state 3 = breath meditation eyes open g2
        {
            waitState = 30.0f;
            epochStart = 0;
            calibrationAudio.GetComponent<AudioSource>().PlayOneShot(calibrationClips[0]);
            recOn = false;
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
            recOn = true;
            gesture = 2;
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().Gesture(gesture);
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
            epochStart = 1;
        }
        else if (state == 4)//state 4 = Interval - narrator 
        {
            epochStart = 0;
            calibrationAudio.GetComponent<AudioSource>().PlayOneShot(calibrationClips[1]);
            calibrationAudio.GetComponent<AudioSource>().PlayOneShot(calibrationClips[5]);
            recOn = false;
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
            waitState = 40.0f;
        }
        else if (state == 5)//state 5 = Happy: eyes closed g3
        {
            waitState = 30.0f;
            calibrationAudio.GetComponent<AudioSource>().PlayOneShot(calibrationClips[0]);
            recOn = true;
            gesture = 3;
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().Gesture(gesture);
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
            epochStart = 1;
        }
        else if (state == 6)//state 6 = Interval - narrator 
        {
            epochStart = 0;
            calibrationAudio.GetComponent<AudioSource>().PlayOneShot(calibrationClips[1]);
            calibrationAudio.GetComponent<AudioSource>().PlayOneShot(calibrationClips[6]);
            recOn = false;
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
            waitState = 40.0f;
        }
        else if (state == 7) //state 7 = Happy: eyes open g4
        {
            waitState = 30.0f;
            epochStart = 0;
            calibrationAudio.GetComponent<AudioSource>().PlayOneShot(calibrationClips[0]);
            recOn = false;
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
            recOn = true;
            gesture = 4;
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().Gesture(gesture);
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
            epochStart = 1;
        }
        else if (state == 8) //state 8 = Interval - narrator
        {
            epochStart = 0;
            calibrationAudio.GetComponent<AudioSource>().PlayOneShot(calibrationClips[1]);
            calibrationAudio.GetComponent<AudioSource>().PlayOneShot(calibrationClips[7]);
            recOn = false;
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
            waitState = 40.0f;
        }
        else if (state == 9) //state 9 = Sad eyes closed g5
        {
            waitState = 30.0f;
            calibrationAudio.GetComponent<AudioSource>().PlayOneShot(calibrationClips[0]);
            recOn = false;
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
            recOn = true;
            gesture = 5;
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().Gesture(gesture);
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
            epochStart = 1;
        }
        else if (state == 10) //state 10 = Interval - narrator
        {
            epochStart = 0;
            calibrationAudio.GetComponent<AudioSource>().PlayOneShot(calibrationClips[1]);
            calibrationAudio.GetComponent<AudioSource>().PlayOneShot(calibrationClips[8]);
            recOn = false;
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
            waitState = 40.0f;
        }
        else if (state == 11) //state 11 = Sad eyes open g6
        {
            waitState = 30.0f;
            epochStart = 0;
            calibrationAudio.GetComponent<AudioSource>().PlayOneShot(calibrationClips[0]);
            recOn = false;
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
            recOn = true;
            gesture = 6;
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().Gesture(gesture);
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
            epochStart = 1;
        }
        else if (state == 12)//state 12 = Interval - narrator 
        {
            epochStart = 0;
            calibrationAudio.GetComponent<AudioSource>().PlayOneShot(calibrationClips[1]);
            calibrationAudio.GetComponent<AudioSource>().PlayOneShot(calibrationClips[9]);
            recOn = false;
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
            waitState = 30.0f;
        }
        else if (state == 13) //state 13 = Recognise Instrument 1: eyes closed g7
        {
            waitState = 30.0f;
            calibrationAudio.GetComponent<AudioSource>().PlayOneShot(calibrationClips[0]);
            recOn = false;
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
            recOn = true;
            gesture = 7;
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().Gesture(gesture);
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
            epochStart = 1;
        }
        else if (state == 14) //state 14 = Interval - narrator
        {
            epochStart = 0;
            calibrationAudio.GetComponent<AudioSource>().PlayOneShot(calibrationClips[1]);
            calibrationAudio.GetComponent<AudioSource>().PlayOneShot(calibrationClips[10]);
            recOn = false;
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
            waitState = 28.0f;
        }
        else if (state == 15) //state 15 = Recognise Instrument 1: eyes open g8 
        {
            waitState = 30.0f;
            epochStart = 0;
            calibrationAudio.GetComponent<AudioSource>().PlayOneShot(calibrationClips[0]);
            recOn = false;
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
            recOn = true;
            gesture = 8;
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().Gesture(gesture);
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
            epochStart = 1;
            waitState = 26.0f;
        }
        else if (state == 16)//state 16 = Interval - narrator 
        {
            epochStart = 0;
            calibrationAudio.GetComponent<AudioSource>().PlayOneShot(calibrationClips[1]);
            calibrationAudio.GetComponent<AudioSource>().PlayOneShot(calibrationClips[11]);
            recOn = false;
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
            waitState = 28.0f;
        }
        else if (state == 17) //state 17 = Recognise Instrument 2: eyes closed g9
        {
            waitState = 30.0f;
            calibrationAudio.GetComponent<AudioSource>().PlayOneShot(calibrationClips[0]);
            recOn = false;
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
            recOn = true;
            gesture = 9;
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().Gesture(gesture);
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
            epochStart = 1;
        }
        else if (state == 18) //state 18 = Interval - narrator
        {
            epochStart = 0;
            calibrationAudio.GetComponent<AudioSource>().PlayOneShot(calibrationClips[1]);
            calibrationAudio.GetComponent<AudioSource>().PlayOneShot(calibrationClips[12]);
            recOn = false;
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
            waitState = 26.0f;
        }
        else if (state == 19) //state 19 = Recognise Instrument 2: eyes open g10 
        {
            waitState = 30.0f;
            epochStart = 0;
            calibrationAudio.GetComponent<AudioSource>().PlayOneShot(calibrationClips[0]);
            recOn = false;
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
            recOn = true;
            gesture = 8;
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().Gesture(gesture);
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
            epochStart = 1;
        }
        else if (state == 20)//state 20 = End Calibration 
        {
            epochStart = 0;
            calibrationAudio.GetComponent<AudioSource>().PlayOneShot(calibrationClips[1]);
            calibrationAudio.GetComponent<AudioSource>().PlayOneShot(calibrationClips[13]);
            recOn = false;
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
            waitState = 8.0f;
        }
        //   yield return null;
        StartCoroutine(StateChange());
    }
    IEnumerator RepeatTimer()
    {
        yield return new WaitForSeconds(waitEpoch);

        if (state == 1 && epochStart == 1)
        {
            recOn = false;
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
            recOn = true;
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
        }
        else if (state == 3 && epochStart == 1)
        {
            recOn = false;
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
            recOn = true;
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
        }
        if (state == 5 && epochStart == 1)
        {
            recOn = false;
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
            recOn = true;
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
        }
        else if (state == 7 && epochStart == 1)
        {
            recOn = false;
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
            recOn = true;
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
        }
        if (state == 9 && epochStart == 1)
        {
            recOn = false;
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
            recOn = true;
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
        }
        else if (state == 11 && epochStart == 1)
        {
            recOn = false;
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
            recOn = true;
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
        }
        if (state == 13 && epochStart == 1)
        {
            recOn = false;
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
            recOn = true;
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
        }
        else if (state == 15 && epochStart == 1)
        {
            recOn = false;
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
            recOn = true;
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
        }
        if (state == 17 && epochStart == 1)
        {
            recOn = false;
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
            recOn = true;
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
        }
        else if (state == 19 && epochStart == 1)
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
        recOn = false;
        DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
        deleteExamples = true;
        DTW_Delete.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(deleteExamples);
        deleteExamples = false;
    }
}

