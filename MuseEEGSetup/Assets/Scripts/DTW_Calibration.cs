using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class DTW_Calibration : MonoBehaviour {

    public GameObject DTW_Rec, DTW_Run, DTW_Delete, audioclip;
    private AudioClip gong;
    public float waitState = 30f, waitEpoch = 1.5f;
    public int state, gesture, epochStart;
    public bool recOn, deleteExamples;
    //state 0 = Interval - narrator
    //state 1 = breath meditation eyes open g1
    //state 2 = breath meditation eyes closed g2
    //state 3 = Interval - narrator 
    //state 4 = Happy eyes open g3
    //state 5 = Happy eyes closed g4
    //state 6 = Interval - narrator
    //state 7 = sad eyes open g5
    //state 8 = sad eyes closed g6
    //state 9 = Interval - narrator
    //state 10 = Recognise Instrument 1 g7
    //state 11 = Recognise Instrument 2 g8
    //state 12 = End Calibration

    // Use this for initialization
    void Start()
    {
        StartCoroutine(StateChange());
        gong = audioclip.GetComponent<AudioSource>().clip;


    }
    IEnumerator StateChange()
    {
        yield return new WaitForSeconds(waitState);

        state++;

        if (state == 1)  //state 1 = breath meditation eyes open g1
        {         
            audioclip.GetComponent<AudioSource>().PlayOneShot(gong);
            recOn = true;
            gesture = 1;
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().Gesture(gesture);
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
            epochStart=1;
            StartCoroutine(RepeatTimer());
        }
        else if (state == 2)//state 2 = breath meditation eyes closed g2
        {
            epochStart = 0;
            audioclip.GetComponent<AudioSource>().PlayOneShot(gong);
            recOn = false;
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
            recOn = true;
            gesture = 2;
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().Gesture(gesture);
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
            epochStart = 1;
        }
        else if (state == 3)//state 3 = Interval - narrator 
        {
            epochStart = 0;
            audioclip.GetComponent<AudioSource>().PlayOneShot(gong);
            recOn = false;
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
        }
        else if (state == 4)//state 7 = sad eyes open g3
        {
            audioclip.GetComponent<AudioSource>().PlayOneShot(gong);
            recOn = true;
            gesture = 3 ;
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().Gesture(gesture);
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
            epochStart=1;
        }
        else if (state == 5) //state 5 = Happy eyes closed g4
        {
            epochStart = 0;
            audioclip.GetComponent<AudioSource>().PlayOneShot(gong);
            recOn = false;
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
            recOn = true;
            gesture = 4;
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().Gesture(gesture);
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
            epochStart = 1;
        }
        else if (state == 6) //state 6 = Interval - narrator
        {
            epochStart = 0;
            audioclip.GetComponent<AudioSource>().PlayOneShot(gong);
            recOn = false;
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
        }
        else if (state == 7) //state 7 = Sad eyes open g5
        {
            audioclip.GetComponent<AudioSource>().PlayOneShot(gong);
            recOn = false;
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
            recOn = true;
            gesture = 5;
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().Gesture(gesture);
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
            epochStart = 1;
        }
        else if (state == 8) //state 5 = Sad eyes closed g6
        {
            epochStart = 0;
            audioclip.GetComponent<AudioSource>().PlayOneShot(gong);
            recOn = false;
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
            recOn = true;
            gesture = 6;
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().Gesture(gesture);
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
            epochStart = 1;
        }
        else if (state == 9)//state 9 = Interval - narrator 
        {
            epochStart = 0;
            audioclip.GetComponent<AudioSource>().PlayOneShot(gong);
            recOn = false;
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
        }
    /*    else if (state == 10) //state 10 = Recognise Instrument 1 g7
        {
            audioclip.GetComponent<AudioSource>().PlayOneShot(gong);
            recOn = false;
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
            recOn = true;
            gesture = 7;
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().Gesture(gesture);
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
            epochStart = 1;
        }
        else if (state == 11) //state 11 = Recognise Instrument 2 g8 
        {
            epochStart = 0;
            audioclip.GetComponent<AudioSource>().PlayOneShot(gong);
            recOn = false;
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
            recOn = true;
            gesture = 8;
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().Gesture(gesture);
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
            epochStart = 1;
        }
        else if (state == 12)//state 12 = End Calibration 
        {
            epochStart = 0;
            audioclip.GetComponent<AudioSource>().PlayOneShot(gong);
            recOn = false;
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
        }*/
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
        else if (state == 2 && epochStart == 1)
        {
            recOn = false;
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
            recOn = true;
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
        }
        if (state == 4 && epochStart == 1)
        {
            recOn = false;
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
            recOn = true;
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
        }
        else if (state == 5 && epochStart == 1)
        {
            recOn = false;
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
            recOn = true;
            DTW_Rec.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(recOn);
        }
        if (state == 7 && epochStart == 1)
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
        else if (state == 11 && epochStart == 1)
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

