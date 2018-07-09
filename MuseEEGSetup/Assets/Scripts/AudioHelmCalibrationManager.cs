using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioHelmCalibrationManager : MonoBehaviour
{
    [Header("Synth Section")]
    public AudioHelm.HelmController DroneSynth; //ref to helm controller to play synth
    public AudioHelm.HelmController ArpSynth;
    [Header("Sequencer Section")]
    public AudioHelm.Sequencer ArpSeq;
    [Header("AudioMixer Section")]
    public AudioMixer synthMixer;
    [Header("Chord Picker")]
    public bool[] chords = new bool[3];
    [Header("Startup invoke timer")]
    public bool invokeonstart;
    public float counter;
    //Timer to invoke sequencers/synths on startup
    public float time = 1.5f;

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

    public static AudioHelmCalibrationManager main;

    void Awake()
    {
        main = this;
    }

    // Use this for initialization
    void Start()
    {
        DroneDisable();
        ArpDisable();
        counter = 0;

        if (invokeonstart)
        {
            DemoSequence();
        }

    }
    // Update is called once per frame
    void Update()
    {
        if (invokeonstart)
        {
            DemoSequence();
        }
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

    public void ArpEnable()
    {
        for (int i = 0; i < chords.Length; i++)
        {
            if (chords[i])
            {
                ArpSeq.Clear();
                switch (i)
                {
                    case 0: //Eb Maj 69
                        ArpSeq.AddNote(C4, 0, 17);
                        ArpSeq.AddNote(G4, 0, 17);
                        ArpSeq.AddNote(D4, 0, 17);
                        ArpSeq.AddNote(F5, 0, 17);
                        break;

                    case 1: // D min9
                        ArpSeq.AddNote(F4, 0, 17);
                        ArpSeq.AddNote(C5, 0, 17);
                        ArpSeq.AddNote(E4, 0, 17);
                        ArpSeq.AddNote(A5, 0, 17);
                        break;

                    case 2: // F9
                        ArpSeq.AddNote(Eb4, 0, 17);
                        ArpSeq.AddNote(G4, 0, 17);
                        ArpSeq.AddNote(Eb4, 0, 17);
                        ArpSeq.AddNote(A5, 0, 17);
                        break;
                }
            }
        }
    }

    public void DroneDisable()
    {
        DroneSynth.AllNotesOff();
    }
    public void ArpDisable()
    {
        ArpSeq.Clear();
    }

    public void SetDroneSynthLvl(float synthLvl)
    {
        //   synthMixer.SetFloat("synthVol", synthLvl);
    }
    public void SetArpSynthLvl(float synthLvl)
    {
        //   synthMixer.SetFloat("synthVol", synthLvl);
    }

    public void DemoSequence()
    {
        counter += Time.deltaTime;
        if (counter >= 30.0f && counter <= 30.1f)
        {
            chords[0] = false;
            chords[1] = false;
            chords[2] = true;
            DroneDisable();
            DroneEnable();
         //   Invoke("DroneEnable", 0.1f);
        }
        else if (counter >= 60.0f && counter <= 60.1f)
        {
            chords[0] = true;
            chords[1] = false;
            chords[2] = false;
            DroneDisable();
            ArpDisable();
            DroneEnable();
            ArpEnable();
     //       Invoke("DroneEnable", 0.1f);
    //        Invoke("ArpEnable", 0.1f);

        }
        else if (counter >= 90.0f && counter <= 90.1f)
        {
            chords[0] = false;
            chords[1] = true;
            chords[2] = false;
            DroneDisable();
            ArpDisable();
            DroneEnable();
            ArpEnable();
            //     Invoke("DroneEnable", 0.1f);
            //  Invoke("ArpEnable", 0.1f);
        }
        else if (counter >= 120.0f && counter <= 120.1f)
        {
            chords[0] = false;
            chords[1] = false;
            chords[2] = true;
            DroneDisable();
            ArpDisable();
            DroneEnable();
            ArpEnable();
        //    Invoke("DroneEnable", 0.1f);
          //  Invoke("ArpEnable", 0.1f);
        }

    }
}
