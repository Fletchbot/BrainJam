using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoliSoundScape
{
    public class ChordProgressions : MonoBehaviour
    {
        DiatonicScales diatonicScales;

        [Header("Synth Section")]
        public AudioHelm.HelmController DroneSynth;
        [Header("Sequencer Section")]
        public AudioHelm.Sequencer DroneSeq;
        public int droneSeqPos;
        [Header("Key,Scale & Chord Picker")]
        public string Key, KeyType, ChordVoicing;
        public bool[] chords = new bool[8];
        [Header("Level Picker")]
        public bool Run, Level1, Level2, Level3;

        // Use this for initialization
        void Start()
        {
            diatonicScales = this.GetComponent<DiatonicScales>();

        }

        // Update is called once per frame
        void Update()
        {
            droneSeqPos = (int)DroneSeq.GetSequencerPosition();

            diatonicScales.MajorScales(Key);
            diatonicScales.NatMinorScales(Key);

            if (chords[1] || chords[2] || chords[3] || chords[4] || chords[5] || chords[6] || chords[7])
            {
                DroneEnable();
            }
        }

        public void DroneEnable()
        {
            DroneSynth.AllNotesOff();
            DroneSeq.Clear();
        
            for (int i = 0; i < chords.Length; i++)
            {
                if (chords[i])
                {
                    switch (i)
                    {
                        case 1: //CHORD I

                            if (KeyType == "Major")
                            {
                                DroneSeq.AddNote(diatonicScales.Major_Scale1[1], droneSeqPos + 2.5f, droneSeqPos - 4.0f);
                                DroneSeq.AddNote(diatonicScales.Major_Scale2[3], droneSeqPos + 2.5f, droneSeqPos - 4.0f);

                                if (ChordVoicing == "Triad") DroneSeq.AddNote(diatonicScales.Major_Scale2[5], droneSeqPos + 2.5f, droneSeqPos - 4.0f);

                                if (ChordVoicing == "7" || ChordVoicing == "9")
                                {
                                    DroneSeq.AddNote(diatonicScales.Major_Scale2[7], droneSeqPos + 2.5f, droneSeqPos - 4.0f);
                                    if (ChordVoicing == "9") DroneSeq.AddNote(diatonicScales.Major_Scale2[2], droneSeqPos + 2.5f, droneSeqPos - 4.0f);
                                }
                            }
                            else if (KeyType == "NaturalMinor")
                            {
                                DroneSeq.AddNote(diatonicScales.NatMinor_Scale1[1], droneSeqPos + 2.5f, droneSeqPos - 4.0f);
                                DroneSeq.AddNote(diatonicScales.NatMinor_Scale2[3], droneSeqPos + 2.5f, droneSeqPos - 4.0f);
                                if (ChordVoicing == "Triad") DroneSeq.AddNote(diatonicScales.NatMinor_Scale2[5], droneSeqPos + 2.5f, droneSeqPos - 4.0f);
                                if (ChordVoicing == "7" || ChordVoicing == "9")
                                {
                                    DroneSeq.AddNote(diatonicScales.NatMinor_Scale2[7], droneSeqPos + 2.5f, droneSeqPos - 4.0f);
                                    if (ChordVoicing == "9") DroneSeq.AddNote(diatonicScales.NatMinor_Scale2[2], droneSeqPos + 2.5f, droneSeqPos - 4.0f);
                                }
                            }
                            break;

                        case 2: //CHORD II

                            if (KeyType == "Major")
                            {
                                DroneSeq.AddNote(diatonicScales.Major_Scale1[2], droneSeqPos + 2.5f, droneSeqPos - 4.0f);
                                DroneSeq.AddNote(diatonicScales.Major_Scale2[4], droneSeqPos + 2.5f, droneSeqPos - 4.0f);
                                if (ChordVoicing == "Triad") DroneSeq.AddNote(diatonicScales.Major_Scale2[6], droneSeqPos + 2.5f, droneSeqPos - 4.0f);
                                if (ChordVoicing == "7" || ChordVoicing == "9")
                                {
                                    DroneSeq.AddNote(diatonicScales.Major_Scale2[1], droneSeqPos + 2.5f, droneSeqPos - 4.0f);
                                    if (ChordVoicing == "9") DroneSeq.AddNote(diatonicScales.Major_Scale2[3], droneSeqPos + 2.5f, droneSeqPos - 4.0f);
                                }
                            }
                            else if (KeyType == "NaturalMinor")
                            {
                                DroneSeq.AddNote(diatonicScales.NatMinor_Scale1[2], droneSeqPos + 2.5f, droneSeqPos - 4.0f);
                                DroneSeq.AddNote(diatonicScales.NatMinor_Scale2[4], droneSeqPos + 2.5f, droneSeqPos - 4.0f);
                                if (ChordVoicing == "Triad") DroneSeq.AddNote(diatonicScales.NatMinor_Scale2[6], droneSeqPos + 2.5f, droneSeqPos - 4.0f);
                                if (ChordVoicing == "7" || ChordVoicing == "9")
                                {
                                    DroneSeq.AddNote(diatonicScales.NatMinor_Scale2[1], droneSeqPos + 2.5f, droneSeqPos - 4.0f);
                                    if (ChordVoicing == "9") DroneSeq.AddNote(diatonicScales.NatMinor_Scale2[3], droneSeqPos + 2.5f, droneSeqPos - 4.0f);
                                }
                            }
                            break;

                        case 3: // CHORD III

                            if (KeyType == "Major")
                            {
                                DroneSeq.AddNote(diatonicScales.Major_Scale1[3], droneSeqPos + 2.5f, droneSeqPos - 4.0f);
                                DroneSeq.AddNote(diatonicScales.Major_Scale2[5], droneSeqPos + 2.5f, droneSeqPos - 4.0f);
                                if (ChordVoicing == "Triad") DroneSeq.AddNote(diatonicScales.Major_Scale2[7], droneSeqPos + 2.5f, droneSeqPos - 4.0f);
                                if (ChordVoicing == "7" || ChordVoicing == "9")
                                {
                                    DroneSeq.AddNote(diatonicScales.Major_Scale2[2], droneSeqPos + 2.5f, droneSeqPos - 4.0f);
                                    if (ChordVoicing == "9") DroneSeq.AddNote(diatonicScales.Major_Scale2[4], droneSeqPos + 2.5f, droneSeqPos - 4.0f);
                                }
                            }
                            else if (KeyType == "NaturalMinor")
                            {
                                DroneSeq.AddNote(diatonicScales.NatMinor_Scale1[3], droneSeqPos + 2.5f, droneSeqPos - 4.0f);
                                DroneSeq.AddNote(diatonicScales.NatMinor_Scale2[5], droneSeqPos + 2.5f, droneSeqPos - 4.0f);
                                if (ChordVoicing == "Triad") DroneSeq.AddNote(diatonicScales.NatMinor_Scale2[7], droneSeqPos + 2.5f, droneSeqPos - 4.0f);
                                if (ChordVoicing == "7" || ChordVoicing == "9")
                                {
                                    DroneSeq.AddNote(diatonicScales.NatMinor_Scale2[2], droneSeqPos + 2.5f, droneSeqPos - 4.0f);
                                    if (ChordVoicing == "9") DroneSeq.AddNote(diatonicScales.NatMinor_Scale2[4], droneSeqPos + 2.5f, droneSeqPos - 4.0f);
                                }
                            }
                            break;

                        case 4: // CHORD IV

                            if (KeyType == "Major")
                            {
                                DroneSeq.AddNote(diatonicScales.Major_Scale1[4], droneSeqPos + 2.5f, droneSeqPos - 4.0f);
                                DroneSeq.AddNote(diatonicScales.Major_Scale2[6], droneSeqPos + 2.5f, droneSeqPos - 4.0f);
                                if (ChordVoicing == "Triad") DroneSeq.AddNote(diatonicScales.Major_Scale2[1], droneSeqPos + 2.5f, droneSeqPos - 4.0f);
                                if (ChordVoicing == "7" || ChordVoicing == "9")
                                {
                                    DroneSeq.AddNote(diatonicScales.Major_Scale2[3], droneSeqPos + 2.5f, droneSeqPos - 4.0f);
                                    if (ChordVoicing == "9") DroneSeq.AddNote(diatonicScales.Major_Scale2[5], droneSeqPos + 2.5f, droneSeqPos - 4.0f);
                                }
                            }
                            else if (KeyType == "NaturalMinor")
                            {
                                DroneSeq.AddNote(diatonicScales.NatMinor_Scale1[4], droneSeqPos + 2.5f, droneSeqPos - 4.0f);
                                DroneSeq.AddNote(diatonicScales.NatMinor_Scale2[6], droneSeqPos + 2.5f, droneSeqPos - 4.0f);
                                if (ChordVoicing == "Triad") DroneSeq.AddNote(diatonicScales.NatMinor_Scale2[1], droneSeqPos + 2.5f, droneSeqPos - 4.0f);
                                if (ChordVoicing == "7" || ChordVoicing == "9")
                                {
                                    DroneSeq.AddNote(diatonicScales.NatMinor_Scale2[3], droneSeqPos + 2.5f, droneSeqPos - 4.0f);
                                    if (ChordVoicing == "9") DroneSeq.AddNote(diatonicScales.NatMinor_Scale2[5], droneSeqPos + 2.5f, droneSeqPos - 4.0f);
                                }
                            }
                            break;

                        case 5: // CHORD V

                            if (KeyType == "Major")
                            {
                                DroneSeq.AddNote(diatonicScales.Major_Scale1[5], droneSeqPos + 2.5f, droneSeqPos - 4.0f);
                                DroneSeq.AddNote(diatonicScales.Major_Scale2[7], droneSeqPos + 2.5f, droneSeqPos - 4.0f);
                                if (ChordVoicing == "Triad") DroneSeq.AddNote(diatonicScales.Major_Scale2[2], droneSeqPos + 2.5f, droneSeqPos - 4.0f);
                                if (ChordVoicing == "7" || ChordVoicing == "9")
                                {
                                    DroneSeq.AddNote(diatonicScales.Major_Scale2[4], droneSeqPos + 2.5f, droneSeqPos - 4.0f);
                                    if (ChordVoicing == "9") DroneSeq.AddNote(diatonicScales.Major_Scale2[6], droneSeqPos + 2.5f, droneSeqPos - 4.0f);
                                }
                            }
                            else if (KeyType == "NaturalMinor")
                            {
                                DroneSeq.AddNote(diatonicScales.NatMinor_Scale1[5], droneSeqPos + 2.5f, droneSeqPos - 4.0f);
                                DroneSeq.AddNote(diatonicScales.NatMinor_Scale2[7], droneSeqPos + 2.5f, droneSeqPos - 4.0f);
                                if (ChordVoicing == "Triad") DroneSeq.AddNote(diatonicScales.NatMinor_Scale2[2], droneSeqPos + 2.5f, droneSeqPos - 4.0f);
                                if (ChordVoicing == "7" || ChordVoicing == "9")
                                {
                                    DroneSeq.AddNote(diatonicScales.NatMinor_Scale2[4], droneSeqPos + 2.5f, droneSeqPos - 4.0f);
                                    if (ChordVoicing == "9") DroneSeq.AddNote(diatonicScales.NatMinor_Scale2[6], droneSeqPos + 2.5f, droneSeqPos - 4.0f);
                                }
                            }
                            break;

                        case 6: // CHORD VI

                            if (KeyType == "Major")
                            {
                                DroneSeq.AddNote(diatonicScales.Major_Scale1[6], droneSeqPos + 2.5f, droneSeqPos - 4.0f);
                                DroneSeq.AddNote(diatonicScales.Major_Scale2[1], droneSeqPos + 2.5f, droneSeqPos - 4.0f);
                                if (ChordVoicing == "Triad") DroneSeq.AddNote(diatonicScales.Major_Scale2[3], droneSeqPos + 2.5f, droneSeqPos - 4.0f);
                                if (ChordVoicing == "7" || ChordVoicing == "9")
                                {
                                    DroneSeq.AddNote(diatonicScales.Major_Scale2[5], droneSeqPos + 2.5f, droneSeqPos - 4.0f);
                                    if (ChordVoicing == "9") DroneSeq.AddNote(diatonicScales.Major_Scale2[7], droneSeqPos + 2.5f, droneSeqPos - 4.0f);
                                }
                            }
                            else if (KeyType == "NaturalMinor")
                            {
                                DroneSeq.AddNote(diatonicScales.NatMinor_Scale1[6], droneSeqPos + 2.5f, droneSeqPos - 4.0f);
                                DroneSeq.AddNote(diatonicScales.NatMinor_Scale2[1], droneSeqPos + 2.5f, droneSeqPos - 4.0f);
                                if (ChordVoicing == "Triad") DroneSeq.AddNote(diatonicScales.NatMinor_Scale2[3], droneSeqPos + 2.5f, droneSeqPos - 4.0f);
                                if (ChordVoicing == "7" || ChordVoicing == "9")
                                {
                                    DroneSeq.AddNote(diatonicScales.NatMinor_Scale2[5], droneSeqPos + 2.5f, droneSeqPos - 4.0f);
                                    if (ChordVoicing == "9") DroneSeq.AddNote(diatonicScales.NatMinor_Scale2[7], droneSeqPos + 2.5f, droneSeqPos - 4.0f);
                                }
                            }
                            break;

                        case 7: // CHORD VII

                            if (KeyType == "Major")
                            {
                                DroneSeq.AddNote(diatonicScales.Major_Scale1[7], droneSeqPos + 2.5f, droneSeqPos - 4.0f);
                                DroneSeq.AddNote(diatonicScales.Major_Scale2[2], droneSeqPos + 2.5f, droneSeqPos - 4.0f);
                                if (ChordVoicing == "Triad") DroneSeq.AddNote(diatonicScales.Major_Scale2[4], droneSeqPos + 2.5f, droneSeqPos - 4.0f);
                                if (ChordVoicing == "7" || ChordVoicing == "9")
                                {
                                    DroneSeq.AddNote(diatonicScales.Major_Scale2[6], droneSeqPos + 2.5f, droneSeqPos - 4.0f);
                                    if (ChordVoicing == "9") DroneSeq.AddNote(diatonicScales.Major_Scale2[1], droneSeqPos + 2.5f, droneSeqPos - 4.0f);
                                }
                            }
                            else if (KeyType == "NaturalMinor")
                            {
                                DroneSeq.AddNote(diatonicScales.NatMinor_Scale1[7], droneSeqPos + 2.5f, droneSeqPos - 4.0f);
                                DroneSeq.AddNote(diatonicScales.NatMinor_Scale2[2], droneSeqPos + 2.5f, droneSeqPos - 4.0f);
                                if (ChordVoicing == "Triad") DroneSeq.AddNote(diatonicScales.NatMinor_Scale2[4], droneSeqPos + 2.5f, droneSeqPos - 4.0f);
                                if (ChordVoicing == "7" || ChordVoicing == "9")
                                {
                                    DroneSeq.AddNote(diatonicScales.NatMinor_Scale2[6], droneSeqPos + 2.5f, droneSeqPos - 4.0f);
                                    if (ChordVoicing == "9") DroneSeq.AddNote(diatonicScales.NatMinor_Scale2[1], droneSeqPos + 2.5f, droneSeqPos - 4.0f);
                                }
                            }
                            break;
                    }

                    chords[i] = false;
                }
            }
        }

    }
}
