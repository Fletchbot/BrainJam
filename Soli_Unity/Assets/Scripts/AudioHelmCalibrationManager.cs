using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioHelmCalibrationManager : MonoBehaviour
{
    [Header("Synth Section")]
    public AudioHelm.HelmController DroneSynth;
    public AudioHelm.HelmController Bass;
    [Header("Sequencer Section")]
    public AudioHelm.Sequencer BassSeq;
    [Header("AudioMixer Section")]
    public AudioMixer synthMixer;
    [Header("Chord Picker")]
    public bool[] chords = new bool[3];

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
        BassDisable();
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

}
