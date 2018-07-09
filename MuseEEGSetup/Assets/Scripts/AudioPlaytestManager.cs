using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioPlaytestManager : MonoBehaviour
{
    [Header("Text Section")]
    public GameObject timerText;
    Text text;
    [Header("Gesture Controller")]
    public GameObject GC;
    [Header("NarratorAudio")]
    public AudioSource[] NarratorClips;
    [Header("Audio FX")]
    public AudioSource LavaAU, ThunderAU;
    [Header("Synth Section")]
    public AudioHelm.HelmController DroneSynth;
    public AudioHelm.HelmController Bass;
    [Header("Sequencer Section")]
    public AudioHelm.Sequencer BassSeq;
    [Header("AudioMixer Section")]
    public AudioMixer synthMixer;
    [Header("Chord Picker")]
    public bool[] chords = new bool[3];


    private bool NoGesture, Mediate, Happy, Sad, Instr1, Instr2;
    private bool G_sw, M_sw, H_sw, S_sw, I1_sw, I2_sw;
    private int N_Intro, N_Emotion, N_Instru, N_EndComplete, N_EndTwoG, N_EndReCalibrate;
    private float sfxlvl, dronelvl, basslvl;
    private bool sfxPlaying, sfxFadedown, sfxFadeup, sfxDuck, mDuck, eDuck, InstrDuck;

    private bool startPlaytest;
    private float counter;

    //Is the midi note 0 to 127 = to music note
    private int C2 = 48;
    private int Db2 = 49;
    private int D2 = 50;
    private int Eb2 = 51;
    private int E2 = 52;
    private int F2 = 53;
    private int Gb2 = 54;
    private int G2 = 55;
    private int Ab2 = 56;
    private int A2 = 57;
    private int Bb2 = 58;
    private int B2 = 59;

    private int C3 = 60;
    private int Db3 = 61;
    private int D3 = 62;
    private int Eb3 = 63;
    private int E3 = 64;
    private int F3 = 65;
    private int Gb3 = 66;
    private int G3 = 67;
    private int Ab3 = 68;
    private int A3 = 69;
    private int Bb3 = 70;
    private int B3 = 71;

    private int C4 = 72;
    private int Db4 = 73;
    private int D4 = 74;
    private int Eb4 = 75;
    private int E4 = 76;
    private int F4 = 77;
    private int Gb4 = 78;
    private int G4 = 79;
    private int Ab4 = 80;
    private int A4 = 81;
    private int Bb4 = 82;
    private int B4 = 83;

    private int C5 = 84;
    private int Db5 = 85;
    private int D5 = 86;
    private int Eb5 = 87;
    private int E5 = 88;
    private int F5 = 89;
    private int Gb5 = 90;
    private int G5 = 91;
    private int Ab5 = 92;
    private int A5 = 93;
    private int Bb5 = 94;
    private int B5 = 95;

    private int C6 = 96;
    private int Db6 = 97;
    private int D6 = 98;
    private int Eb6 = 99;
    private int E6 = 100;
    private int F6 = 101;
    private int Gb6 = 102;
    private int G6 = 103;
    private int Ab6 = 104;
    private int A6 = 105;
    private int Bb6 = 106;
    private int B6 = 107;
    private int C7 = 108;

    public static AudioPlaytestManager main;

    void Awake()
    {
        main = this;
    }

    // Use this for initialization
    void Start()
    {
        text = timerText.GetComponent<Text>();
        DroneDisable();
        BassDisable();
        sfxlvl = 0.0f;
    }
    // Update is called once per frame
    void Update()
    {
        AudioGestureControl();
        SetSFXLvl();
        SetSynthsLvl();
        NarratorEndings();
    }
    public void DroneEnable()
    {
        DroneSynth.AllNotesOff();

        for (int i = 0; i < chords.Length; i++)
        {
            if (chords[i])
            {
                switch (i)
                {
                    case 0: //Eb Maj69
                        DroneSynth.NoteOn(Eb2, 1.0f);
                        DroneSynth.NoteOn(G3, 1.0f);
                        DroneSynth.NoteOn(C3, 1.0f);
                        DroneSynth.NoteOn(F3, 1.0f);
                        break;

                    case 1: // D min9
                        DroneSynth.NoteOn(D2, 1.0f);
                        DroneSynth.NoteOn(F3, 1.0f);
                        DroneSynth.NoteOn(C3, 1.0f);
                        DroneSynth.NoteOn(E3, 1.0f);
                        break;

                    case 2: // F9
                        DroneSynth.NoteOn(F2, 1.0f);
                        DroneSynth.NoteOn(A3, 1.0f);
                        DroneSynth.NoteOn(Eb3, 1.0f);
                        DroneSynth.NoteOn(G3, 1.0f);
                        break;
                }
            }
        }
    }
    public void BassEnable()
    {
        BassSeq.Clear();
        for (int i = 0; i < chords.Length; i++)
        {
            if (chords[i])
            {
                switch (i)
                {
                    case 0: //Eb Maj69
                        BassSeq.AddNote(Eb2, 0, 15);
                        BassSeq.AddNote(Eb3, 0, 15);

                        BassSeq.AddNote(G2, 16, 31);
                        BassSeq.AddNote(G3, 16, 31);


                        BassSeq.AddNote(F2, 32, 47);
                        BassSeq.AddNote(F3, 32, 47);

                        BassSeq.AddNote(C2, 48, 65);
                        BassSeq.AddNote(C3, 48, 65);

                        break;
                    case 1: // D min9
                        BassSeq.AddNote(D2, 0, 15);
                        BassSeq.AddNote(D3, 0, 15);

                        BassSeq.AddNote(F2, 16, 31);
                        BassSeq.AddNote(F3, 16, 31);


                        BassSeq.AddNote(E2, 32, 47);
                        BassSeq.AddNote(E3, 32, 47);

                        BassSeq.AddNote(A2, 48, 65);
                        BassSeq.AddNote(A3, 48, 65);
                        break;
                    case 2: // F9
                        BassSeq.AddNote(F2, 0, 15);
                        BassSeq.AddNote(F3, 0, 15);

                        BassSeq.AddNote(G2, 16, 31);
                        BassSeq.AddNote(G3, 16, 31);


                        BassSeq.AddNote(A2, 32, 47);
                        BassSeq.AddNote(A3, 32, 47);

                        BassSeq.AddNote(Eb2, 48, 65);
                        BassSeq.AddNote(Eb3, 48, 65);
                        break;
                }
            }
        }
    }

    public void DroneDisable()
    {
        DroneSynth.AllNotesOff();
    }
    public void BassDisable()
    {
        BassSeq.Clear();
    }

    public void SetSynthsLvl()
    {
        mDuck = NarratorClips[1].GetComponent<AudioSource>().isPlaying;
        eDuck = NarratorClips[2].GetComponent<AudioSource>().isPlaying;

        if (InstrDuck)
        {
            dronelvl = -8.0f;
            basslvl = -8.0f;
        }
        else if (mDuck || eDuck)
        {
            if (basslvl >= -6.0f && basslvl <= 0.1f) basslvl -= 0.1f;
            if (dronelvl >= -6.0f && dronelvl <= 0.1f) dronelvl -= 0.1f;
        }
        else if (!mDuck || !eDuck)
        {
            if (basslvl >= -8.1f && basslvl <= -0.1f) basslvl += 0.1f;
            if (dronelvl >= -8.1f && dronelvl <= -0.1f) dronelvl += 0.1f;
        }

        synthMixer.SetFloat("bassVol", basslvl);
        synthMixer.SetFloat("droneVol", dronelvl);
    }
    public void SetSFXLvl()
    {
        sfxPlaying = ThunderAU.GetComponent<AudioSource>().isPlaying;
        sfxDuck = NarratorClips[0].GetComponent<AudioSource>().isPlaying;

        if (sfxPlaying && sfxFadedown)
        {
            if(sfxlvl <= 0.1f && sfxlvl >= -39.9f)
            {

                sfxlvl = sfxlvl - 0.1f;
            }
            else
            {
                ThunderAU.GetComponent<AudioSource>().Stop();
                LavaAU.GetComponent<AudioSource>().Stop();
                sfxFadedown = false;
            }
        }
        else if (sfxPlaying && sfxFadeup)
        {
            if(sfxlvl >= -40.0f && sfxlvl <= 0.0f)
            {
                sfxlvl = sfxlvl + 0.1f;
            } else
            {
                sfxFadeup = false;
            }
        }
        else if (sfxDuck)
        {
            sfxlvl = -6.0f;
        }
        else if (!sfxDuck)
        {

           if(sfxlvl <=0.0f) sfxlvl += 0.1f;
        }
                  
            synthMixer.SetFloat("sfxVol", sfxlvl);
    }

    public void AudioGestureControl()
    {
        NoGesture = GC.GetComponent<GestureController>().NoGesture;
        Mediate = GC.GetComponent<GestureController>().Mediate;
        Happy = GC.GetComponent<GestureController>().Happy;
        Sad = GC.GetComponent<GestureController>().Sad;
        Instr1 = GC.GetComponent<GestureController>().Instr1;
        Instr2 = GC.GetComponent<GestureController>().Instr2;

        if (NoGesture && !G_sw)
        {
            DroneDisable();
            BassDisable();
            InstrDuck = true;

            ThunderAU.GetComponent<AudioSource>().Play();
            LavaAU.GetComponent<AudioSource>().Play();

            S_sw = false;
            G_sw = true;
            H_sw = false;
            M_sw = false;
            I1_sw = false;
            I2_sw = false;

            sfxFadeup = true;
            sfxFadedown = false;

            if (N_Intro == 0) N_Intro = 1;

            NarratorUpdate();
        }
        else if (Mediate && !M_sw)
        {
            chords[0] = false;
            chords[1] = false;
            chords[2] = true;

            DroneEnable();
            BassEnable();
            InstrDuck = false;

            S_sw = false;
            G_sw = false;
            H_sw = false;
            M_sw = true;
            I1_sw = false;
            I2_sw = false;

            sfxFadedown = true;
            sfxFadeup = false;

            if (N_Emotion == 0)
            {
                N_Emotion = 1;
                Invoke("NarratorUpdate", 5.0f);
            }
        }
        else if (Happy && !H_sw)
        {
            chords[0] = true;
            chords[1] = false;
            chords[2] = false;

            DroneEnable();
            BassEnable();

            S_sw = false;
            G_sw = false;
            H_sw = true;
            M_sw = false;
            I1_sw = false;
            I2_sw = false;

            sfxFadedown = true;
            sfxFadeup = false;

            if (N_Instru >= 0 && N_Instru <= 2)
            {
                N_Instru++;
                if (N_Instru == 2) Invoke("NarratorUpdate", 5.0f);
            }
        }
        else if (Sad && !S_sw)
        {
            chords[0] = false;
            chords[1] = true;
            chords[2] = false;

            DroneEnable();
            BassEnable();

            S_sw = true;
            G_sw = false;
            H_sw = false;
            M_sw = false;
            I1_sw = false;
            I2_sw = false;

            sfxFadedown = true;
            sfxFadeup = false;

            if (N_Instru >= 0 && N_Instru <= 2)
            {
                N_Instru++;
                if (N_Instru == 2) Invoke("NarratorUpdate", 5.0f);
            }
        }
        else if (Instr1 && !I1_sw)
        {
            chords[0] = false;
            chords[1] = false;
            chords[2] = true;

            DroneEnable();
            BassDisable();

            S_sw = false;
            G_sw = false;
            H_sw = false;
            M_sw = false;
            I1_sw = true;
            I2_sw = false;

            sfxFadedown = true;
            sfxFadeup = false;

            if (N_EndComplete >= 0 && N_EndComplete <= 2)
            {
                N_EndComplete++;
            }
        }
        else if (Instr2 && !I2_sw)
        {
            chords[0] = false;
            chords[1] = false;
            chords[2] = true;

            DroneDisable();
            BassEnable();

            S_sw = false;
            G_sw = false;
            H_sw = false;
            M_sw = false;
            I1_sw = false;
            I2_sw = true;

            sfxFadedown = true;
            sfxFadeup = false;

            if (N_EndComplete >= 0 && N_EndComplete <= 2)
            {
                N_EndComplete++;            
            }
        }
    }
    public void NarratorUpdate()
    {
        if (N_Intro == 1)
        {
            NarratorClips[0].GetComponent<AudioSource>().Play();
            N_Intro = 2;
            Invoke("counterReset", 25.0f);
        }
        else if (N_Emotion == 1)
        {
            NarratorClips[1].GetComponent<AudioSource>().Play();
            N_Emotion = 2;
            startPlaytest = false;
            Invoke("counterReset", 34.0f);
        }
        else if (N_Instru == 1)
        {
            counterReset();
        }
        else if (N_Instru == 2)
        {
            NarratorClips[2].GetComponent<AudioSource>().Play();
            N_Instru = 3;
            startPlaytest = false;
            Invoke("counterReset", 31.0f);
        }
        else if (N_EndComplete == 1)
        {
            counterReset();
        }
        else if (N_EndComplete == 2)
        {
            NarratorClips[3].GetComponent<AudioSource>().Play();
            N_EndComplete = 3;
            DroneDisable();
            BassDisable();
        }
        else if (N_EndTwoG == 1)
        {
            NarratorClips[4].GetComponent<AudioSource>().Play();
            N_EndTwoG = 2;
            DroneDisable();
            BassDisable();
        }
        else if (N_EndReCalibrate == 1)
        {
            NarratorClips[5].GetComponent<AudioSource>().Play();
            N_EndReCalibrate = 2;
            DroneDisable();
            BassDisable();
        }

    }
    public void NarratorEndings()
    {
        if (startPlaytest)
        {
            counter -= Time.deltaTime;
            if (counter <= 0)
            {
                counter = 0;
                string seconds = Mathf.Round(counter % 60).ToString("00");
                string minutes = Mathf.Round((counter % 3600) / 60).ToString("00");

                if (NoGesture || Mediate)
                {
                    N_EndReCalibrate = 1;
                    NarratorUpdate();
                }
                else if (Happy || Sad)
                {
                    N_EndTwoG = 1;
                    NarratorUpdate();
                }
                else if (Instr1 || Instr2)
                {
                    if (N_EndComplete == 2) NarratorUpdate();
                }
            }
            else
            {
                string minutes = Mathf.Floor((counter % 3600) / 60).ToString("00");
                string seconds = (counter % 60).ToString("00");
                text.text = minutes + ":" + seconds;
            }
        }
        else
        {
            text.text = "";
        }
    }

    public void counterReset()
    {
        counter = 60.0f;

        if (!startPlaytest)
        {
            startPlaytest = true;
        }
    }
}
