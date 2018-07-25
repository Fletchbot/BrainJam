using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniOSC;

public class GestureController : MonoBehaviour
{
    [Header("Mode")]
    public bool MuseSolo, MuseMulti, Standalone;
    [Header("Wekinator Receiver")]
    public GameObject WekOSC_SoloReceiver, WekOSC_MultiReceiver;
    public float meditateFloat, emotions, instruments, happyFloat, sadFloat, unsureFloat, instr1Float, instr2Float, noInstrFloat;
    public bool isMeditate, isHappy, isSad, isUnsure, isNoInstr, isInstr1, isInstr2;
    [Header("Wekinator Run Dispatcher")]
    public GameObject WekSoloDTW_Run, WekSoloSVM_Run, WekMultiDTW_Run, WekMultiSVM_Run;
    [Header("Game Gestures")]
    public bool NoGesture, Mediate, Happy, Sad, Instr1, Instr2, bothInstr, mindStateTimeOut;
    public bool Meditation, Happiness, Sadness, Instr1Solo, Instr2Solo;
    [Header("Timer Section")]
    public float countdown;
    public float counter;
    public float speed = 1;
    public int state;

    public bool Intro, isWekRun;

    private System.Random randomizer;

    // Use this for initialization
   public void OnEnable()
    {
        Intro = true;
        randomizer = new System.Random();

        if (Standalone)
        {
            state = -1;
            countdown = 45.0f;
        }
        else
        {
            isWekRun = true;
            countdown = 60.0f;
        }

        counter = countdown;

        NoG_Enable(); //VolcanoErupt

    }
    void GestureConvertor()
    {
        if (MuseSolo)
        {
            isMeditate = WekOSC_SoloReceiver.GetComponent<UniOSCWekOutputReceiver>().isMeditate;
            meditateFloat = WekOSC_SoloReceiver.GetComponent<UniOSCWekOutputReceiver>().meditateFloat;
            emotions = WekOSC_SoloReceiver.GetComponent<UniOSCWekOutputReceiver>().emotions;
            instruments = WekOSC_SoloReceiver.GetComponent<UniOSCWekOutputReceiver>().instruments;
            happyFloat = WekOSC_SoloReceiver.GetComponent<UniOSCWekOutputReceiver>().happyFloat;
            sadFloat = WekOSC_SoloReceiver.GetComponent<UniOSCWekOutputReceiver>().sadFloat;
            unsureFloat = WekOSC_SoloReceiver.GetComponent<UniOSCWekOutputReceiver>().unsureFloat;
            noInstrFloat = WekOSC_SoloReceiver.GetComponent<UniOSCWekOutputReceiver>().noInstrFloat;
            instr1Float = WekOSC_SoloReceiver.GetComponent<UniOSCWekOutputReceiver>().instr1Float;
            instr2Float = WekOSC_SoloReceiver.GetComponent<UniOSCWekOutputReceiver>().instr2Float;
        }
        else if (MuseMulti)
        {
            isMeditate = WekOSC_MultiReceiver.GetComponent<UniOSCWekOutputReceiver>().isMeditate;
            meditateFloat = WekOSC_MultiReceiver.GetComponent<UniOSCWekOutputReceiver>().meditateFloat;
            emotions = WekOSC_MultiReceiver.GetComponent<UniOSCWekOutputReceiver>().emotions;
            instruments = WekOSC_MultiReceiver.GetComponent<UniOSCWekOutputReceiver>().instruments;
            happyFloat = WekOSC_MultiReceiver.GetComponent<UniOSCWekOutputReceiver>().happyFloat;
            sadFloat = WekOSC_MultiReceiver.GetComponent<UniOSCWekOutputReceiver>().sadFloat;
            unsureFloat = WekOSC_MultiReceiver.GetComponent<UniOSCWekOutputReceiver>().unsureFloat;
            noInstrFloat = WekOSC_MultiReceiver.GetComponent<UniOSCWekOutputReceiver>().noInstrFloat;
            instr1Float = WekOSC_MultiReceiver.GetComponent<UniOSCWekOutputReceiver>().instr1Float;
            instr2Float = WekOSC_MultiReceiver.GetComponent<UniOSCWekOutputReceiver>().instr2Float;

        }


        if (Intro && !isMeditate)
        {
            NoG_Enable();
        }
        else if (Intro && isMeditate && !Meditation)
        {
            M_Enable();

            Debug.Log("MeditateState");
            mindStateTimeOut = true;
            Meditation = true;

            Intro = false;

            Invoke("mindStateDisable", 65.0f);
        }
        else if (!Happiness && isHappy)
        {
            H_Enable();

            Debug.Log("HappyState");
            mindStateTimeOut = true;
            Happiness = true;

            Invoke("mindStateDisable", 65.0f);
        }
        else if (!Sadness && isSad)
        {
            S_Enable();

            Debug.Log("SadState");
            mindStateTimeOut = true;
            Sadness = true;

            Invoke("mindStateDisable", 65.0f);

        }
        else if (!Instr1Solo && isInstr1)
        {
            I1_Enable();

            Debug.Log("Instr1State");
            mindStateTimeOut = true;
            Instr1Solo = true;

            Invoke("mindStateDisable", 65.0f);
        }
        else if (!Instr2Solo && isInstr2)
        {
            I2_Enable();

            Debug.Log("Instr2State");
            mindStateTimeOut = true;
            Instr2Solo = true;

            Invoke("mindStateDisable", 65.0f);
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
        if (MuseSolo || MuseMulti)
        {
            GestureConvertor();


            if (MuseSolo)
            {
                WekSoloDTW_Run.GetComponent<WekEventDispatcherButton>().ButtonClick(isWekRun);
                WekSoloSVM_Run.GetComponent<WekEventDispatcherButton>().ButtonClick(isWekRun);
            }
            else if (MuseMulti)
            {

                WekMultiDTW_Run.GetComponent<WekEventDispatcherButton>().ButtonClick(isWekRun);
                WekMultiSVM_Run.GetComponent<WekEventDispatcherButton>().ButtonClick(isWekRun);
            }
        }
        else if (Standalone)
        {
            StandaloneEnable();
            WekSoloDTW_Run.GetComponent<WekEventDispatcherButton>().ButtonClick(isWekRun);
            WekSoloSVM_Run.GetComponent<WekEventDispatcherButton>().ButtonClick(isWekRun);
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

    private void RandomGesture()
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

    private void mindStateDisable()
    { 
        mindStateTimeOut = false;
    }

    public void MuseSoloMode(bool solo)
    {
        if (solo)
        {
            MuseSolo = true;
            MuseMulti = false;
            Standalone = false;
        }
        else if (!solo)
        {
            MuseSolo = false;
        }
    }
    public void MuseMultiMode(bool multi)
    {
        if (multi)
        {
            MuseSolo = false;
            MuseMulti = true;
            Standalone = false;
        }
        else if (!multi)
        {
            MuseMulti = false;
        }

    }
    public void StandaloneMode(bool standalone)
    {
        if (standalone)
        {
            MuseSolo = false;
            MuseMulti = false;
            Standalone = true;
        }
        else if (!standalone)
        {
            Standalone = false;
        }
    }

    public void GestureUpdate()
    {
        if (emotions == 1)
        {
            isHappy = false;
            isSad = false;
            isUnsure = true;
        }
        else if (emotions == 2)
        {
            isHappy = true;
            isSad = false;
            isUnsure = false;
        }
        else if (emotions == 3)
        {
            isHappy = false;
            isSad = true;
            isUnsure = false;
        }

        if (instruments == 1)
        {
            isInstr1 = false;
            isInstr2 = false;
            isNoInstr = true;
        }
        else if (instruments == 2)
        {
            isInstr1 = true;
            isInstr2 = false;
            isNoInstr = false;
        }
        else if (instruments == 3)
        {
            isInstr1 = false;
            isInstr2 = true;
            isNoInstr = false;
        }
    }
}
