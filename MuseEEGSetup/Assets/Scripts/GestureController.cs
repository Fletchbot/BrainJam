using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniOSC;

public class GestureController : MonoBehaviour
{
    [Header("Mode")]
    public bool Muse, Standalone;
    [Header("Wekinator Receiver")]
    public GameObject WekOSC_Receiver;
    public bool M_closed, M_open, H_closed, H_open, S_closed, S_open, I1_closed, I1_open, I2_closed, I2_open;
    public float M_closedF, M_openF, H_closedF, H_openF, S_closedF, S_openF, I1_closedF, I1_openF, I2_closedF, I2_openF;
    [Header("Wekinator Run Dispatcher")]
    public GameObject WekOSC_RunDispatcher;
    [Header("Game Gestures")]
    public bool NoGesture, Mediate, Happy, Sad, Instr1, Instr2, bothInstr, mindStateTimeOut;
    [Header("Timer Section")]
    public float countdown;
    public float counter;
    public float speed = 1;
    public int state;

    private bool Intro;

    private System.Random randomizer;

    // Use this for initialization
    void Start()
    {
        bool isWekRun = true;
        WekOSC_RunDispatcher.GetComponent<WekEventDispatcherButton>().ButtonClick(isWekRun);
        Intro = true;
        randomizer = new System.Random();

        if (Standalone)
        {
            state = -1;
            countdown = 45.0f;
        }
        else
        {
            countdown = 60.0f;
        }

        counter = countdown;

        NoG_Enable(); //VolcanoErupt

    }
    void GestureConvertor()
    {
        M_closed = WekOSC_Receiver.GetComponent<UniOSCWekOutputReceiver>().isGesture1;
        M_open = WekOSC_Receiver.GetComponent<UniOSCWekOutputReceiver>().isGesture2;
        H_closed = WekOSC_Receiver.GetComponent<UniOSCWekOutputReceiver>().isGesture3;
        H_open = WekOSC_Receiver.GetComponent<UniOSCWekOutputReceiver>().isGesture4;
        S_closed = WekOSC_Receiver.GetComponent<UniOSCWekOutputReceiver>().isGesture5;
        S_open = WekOSC_Receiver.GetComponent<UniOSCWekOutputReceiver>().isGesture6;
        I1_closed = WekOSC_Receiver.GetComponent<UniOSCWekOutputReceiver>().isGesture7;
        I1_open = WekOSC_Receiver.GetComponent<UniOSCWekOutputReceiver>().isGesture8;
        I2_closed = WekOSC_Receiver.GetComponent<UniOSCWekOutputReceiver>().isGesture9;
        I2_open = WekOSC_Receiver.GetComponent<UniOSCWekOutputReceiver>().isGesture10;

        M_closedF = WekOSC_Receiver.GetComponent<UniOSCWekOutputReceiver>().gestureFloats[0];
        M_openF = WekOSC_Receiver.GetComponent<UniOSCWekOutputReceiver>().gestureFloats[1];
        H_closedF = WekOSC_Receiver.GetComponent<UniOSCWekOutputReceiver>().gestureFloats[2];
        H_openF = WekOSC_Receiver.GetComponent<UniOSCWekOutputReceiver>().gestureFloats[3];
        S_closedF = WekOSC_Receiver.GetComponent<UniOSCWekOutputReceiver>().gestureFloats[4];
        S_openF = WekOSC_Receiver.GetComponent<UniOSCWekOutputReceiver>().gestureFloats[5];
        I1_closedF = WekOSC_Receiver.GetComponent<UniOSCWekOutputReceiver>().gestureFloats[6];
        I1_openF = WekOSC_Receiver.GetComponent<UniOSCWekOutputReceiver>().gestureFloats[7];
        I2_closedF = WekOSC_Receiver.GetComponent<UniOSCWekOutputReceiver>().gestureFloats[8];
        I2_openF = WekOSC_Receiver.GetComponent<UniOSCWekOutputReceiver>().gestureFloats[9];

        if (Intro && !M_closed || !M_open)
        {
            NoG_Enable();
        }
        else if (Intro && M_closed || M_open)
        {
            M_Enable();
            Intro = false;
        }
        else if (H_closed || H_open)
        {
            H_Enable();
        }
        else if (S_closed || S_open)
        {
            S_Enable();
        }
        else if (I1_closed || I1_open)
        {
            I1_Enable();
        }
        else if (I2_closed || I2_open)
        {
            I2_Enable();
        }
        else if (mindStateTimeOut == false)
        {
            DisableAllGestures();
        }

        if (bothInstr)
        {
            counter -= Time.deltaTime * speed;
            if (counter <= 0)
            {
                counter = countdown;
                mindStateTimeOut = true;
                NoG_Enable();
            }
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
            counter -= Time.deltaTime * speed;
            if (counter <= 0)
            {
                counter = 0;
            }
        }

        if (state == 0)
        {
            if (counter <= 0)
            {
                M_Enable();
                state++;
                counter = countdown;
            }

        }
        else if (state == 1)
        {
            if (counter <= 0)
            {
                H_Enable();
                state++;
                counter = countdown;
            }
        }
        else if (state == 2)
        {
            if (counter <= 0)
            {
                S_Enable();
                state++;
                counter = countdown;
            }
        }
        else if (state == 3)
        {
            if (counter <= 0)
            {
                I1_Enable();
                state++;
                counter = countdown;
            }
        }
        else if (state == 4)
        {
            if (counter <= 0)
            {
                DisableAllGestures();
                state++;
                counter = countdown;
            }
        }
        else if (state == 5)
        {
            if (counter <= 0)
            {
                I2_Enable();
                state++;
                counter = countdown;
            }
        }
        else if (state >= 6)
        {
            if (counter <= 0)
            {
                RandomGesture();
                state++;
                counter = countdown;
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Muse)
        {
            GestureConvertor();
        }
        else if (Standalone)
        {
            StandaloneEnable();
        }
    }

    void NoG_Enable()
    {
        NoGesture = true;
        Mediate = false;
        Happy = false;
        Sad = false;
        Instr1 = false;
        Instr2 = false;
        bothInstr = false;
    }

    void M_Enable()
    {
        NoGesture = false;
        Mediate = true;
        Happy = false;
        Sad = false;
        Instr1 = false;
        Instr2 = false;
        bothInstr = false;

        mindStateTimeOut = false;
    }

    void H_Enable()
    {
        NoGesture = false;
        Mediate = false;
        Happy = true;
        Sad = false;
        Instr1 = false;
        Instr2 = false;
        bothInstr = false;

        mindStateTimeOut = false;
    }

    void S_Enable()
    {
        NoGesture = false;
        Mediate = false;
        Happy = false;
        Sad = true;
        Instr1 = false;
        Instr2 = false;
        bothInstr = false;

        mindStateTimeOut = false;
    }

    void I1_Enable()
    {
        NoGesture = false;
        Mediate = false;
        Happy = false;
        Sad = false;
        Instr1 = true;
        Instr2 = false;
        bothInstr = false;

        mindStateTimeOut = false;
    }

    void I2_Enable()
    {
        NoGesture = false;
        Mediate = false;
        Happy = false;
        Sad = false;
        Instr1 = false;
        Instr2 = true;
        bothInstr = false;

        mindStateTimeOut = false;
    }

    void DisableAllGestures()
    {
        NoGesture = false;
        Mediate = false;
        Happy = false;
        Sad = false;
        Instr1 = false;
        Instr2 = false;
        bothInstr = true;
    }

    void RandomGesture()
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
                I1_Enable();
                break;
            case 5:
                I2_Enable();
                break;
            case 6:
                NoG_Enable();
                break;
        }

    }

}
