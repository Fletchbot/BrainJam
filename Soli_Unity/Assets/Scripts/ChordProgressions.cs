using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SoliGameController;

namespace SoliSoundScape
{
    public class ChordProgressions : MonoBehaviour
    {
        DiatonicScales diatonicScales;
        SoliSoundscapeLvl2 lvl2;
        public GameController game_c;
        public GestureController gesture_c;

        [Header("Synth Section")]
        public AudioHelm.HelmController DroneSynth;

        [Header("Sequencer Section")]
        public AudioHelm.Sequencer DroneSeq;
        public int droneSeqPos;

        [Header("Key,Scale & Chord Picker")]
        public string Key, KeyType, ChordVoicing, ChordType;
        public bool[] chords = new bool[8];
        public int currChord;

        [Header("Level Picker")]
        public bool Run, reset, changePatch;

        // Use this for initialization
        void Start()
        {
            diatonicScales = this.GetComponent<DiatonicScales>();
            lvl2 = this.GetComponent<SoliSoundscapeLvl2>();
            Invoke("resetPatch", 1.0f);
        }

        void resetPatch()
        {
            changePatch = true;
        }

        // Update is called once per frame
        void Update()
        {
            droneSeqPos = (int)DroneSeq.GetSequencerPosition();
            diatonicScales.MajorScales(Key);
            diatonicScales.NatMinorScales(Key);
            changePatch = lvl2.changeInstrument;

            if (chords[1] || chords[2] || chords[3] || chords[4] || chords[5] || chords[6] || chords[7])
            {
                currentchord();
                DroneSynth.AllNotesOff();
                DroneSeq.Clear();
                DroneEnable();
            }

            if(game_c.HeadsetOn == 0 && !reset)
            {
                DroneSynth.AllNotesOff();
                DroneSeq.Clear();
                reset = true;
            }
            else if (game_c.HeadsetOn == 1 && reset)
            {
                Invoke("resetPatch", 1.0f);
                reset = false;
            }

        }

        public void DroneEnable()
        {
            for (int i = 0; i < chords.Length; i++)
            {
                if (chords[i])
                {
                    switch (i)
                    {
                        case 1: //CHORD I 

                            if (KeyType == "Major") //Ionian 
                            {
                                DroneSeq.AddNote(diatonicScales.Major_Scale1[1], droneSeqPos + 4.0f, droneSeqPos - 4.0f); //1
                                DroneSeq.AddNote(diatonicScales.Major_Scale2[3], droneSeqPos + 4.0f, droneSeqPos - 4.0f); //3

                                if (ChordVoicing == "Extended") // voicings with 3rd on bottom maj7(6)
                                {

                                    DroneSeq.AddNote(diatonicScales.Major_Scale2[6], droneSeqPos + 4.0f, droneSeqPos - 4.0f); //6
                                    DroneSeq.AddNote(diatonicScales.Major_Scale3[2], droneSeqPos + 4.0f, droneSeqPos - 4.0f); //9                              
                                    DroneSeq.AddNote(diatonicScales.Major_Scale3[5], droneSeqPos + 4.0f, droneSeqPos - 4.0f); //5
                                }
                                else
                                {
                                    DroneSeq.AddNote(diatonicScales.Major_Scale2[5], droneSeqPos + 4.0f, droneSeqPos - 4.0f); //5
                                }
                            }
                            else if (KeyType == "NaturalMinor") // Aeolian min7(9)
                            {
                                DroneSeq.AddNote(diatonicScales.NatMinor_Scale1[1], droneSeqPos + 4.0f, droneSeqPos - 4.0f); //1
                                DroneSeq.AddNote(diatonicScales.NatMinor_Scale2[3], droneSeqPos + 4.0f, droneSeqPos - 4.0f); //3                              

                                if (ChordVoicing == "Extended")
                                {
                                    DroneSeq.AddNote(diatonicScales.NatMinor_Scale2[7], droneSeqPos + 4.0f, droneSeqPos - 4.0f); //7
                                    DroneSeq.AddNote(diatonicScales.NatMinor_Scale3[2], droneSeqPos + 4.0f, droneSeqPos - 4.0f); //9
                                    DroneSeq.AddNote(diatonicScales.NatMinor_Scale3[5], droneSeqPos + 4.0f, droneSeqPos - 4.0f); //5
                                }
                                else
                                {

                                    DroneSeq.AddNote(diatonicScales.NatMinor_Scale2[5], droneSeqPos + 4.0f, droneSeqPos - 4.0f); //5
                                }
                            }
                            break;

                        case 2: //CHORD IImin7, II7, 

                            if (KeyType == "Major") //Dorian min7(9)
                            {
                                DroneSeq.AddNote(diatonicScales.Major_Scale1[2], droneSeqPos + 4.0f, droneSeqPos - 4.0f); //1

                                if (ChordType == "NonDiatonic") //II7
                                {
                                    DroneSeq.AddNote((diatonicScales.Major_Scale2[4] + 1), droneSeqPos + 4.0f, droneSeqPos - 4.0f); //3
                                }
                                else
                                {
                                    DroneSeq.AddNote(diatonicScales.Major_Scale2[4], droneSeqPos + 4.0f, droneSeqPos - 4.0f); //b3
                                }

                                if (ChordVoicing == "Extended") // voicings with 3rd on bottom
                                {

                                    DroneSeq.AddNote(diatonicScales.Major_Scale2[1], droneSeqPos + 4.0f, droneSeqPos - 4.0f); //7
                                    DroneSeq.AddNote(diatonicScales.Major_Scale3[3], droneSeqPos + 4.0f, droneSeqPos - 4.0f); //9
                                    DroneSeq.AddNote(diatonicScales.Major_Scale3[6], droneSeqPos + 4.0f, droneSeqPos - 4.0f); //5

                                }
                                else
                                {
                                    DroneSeq.AddNote(diatonicScales.Major_Scale2[6], droneSeqPos + 4.0f, droneSeqPos - 4.0f); //5
                                }
                            }
                            else if (KeyType == "NaturalMinor") // Locrian min7b5 3,5,1,11,7
                            {

                                DroneSeq.AddNote(diatonicScales.NatMinor_Scale2[4], droneSeqPos + 4.0f, droneSeqPos - 4.0f); //3
                                DroneSeq.AddNote(diatonicScales.NatMinor_Scale2[6], droneSeqPos + 4.0f, droneSeqPos - 4.0f); //5

                                if (ChordVoicing == "Extended")
                                {
                                    DroneSeq.AddNote(diatonicScales.NatMinor_Scale3[2], droneSeqPos + 4.0f, droneSeqPos - 4.0f); //1                                
                                    DroneSeq.AddNote(diatonicScales.NatMinor_Scale3[5], droneSeqPos + 4.0f, droneSeqPos - 4.0f); //11
                                    DroneSeq.AddNote(diatonicScales.NatMinor_Scale4[1], droneSeqPos + 4.0f, droneSeqPos - 4.0f); //7
                                }
                                else
                                {
                                    DroneSeq.AddNote(diatonicScales.NatMinor_Scale1[2], droneSeqPos + 4.0f, droneSeqPos - 4.0f); //1
                                }
                            }
                            break;

                        case 3: // CHORD III-7, IIImaj7, III7

                            if (KeyType == "Major") // Phrygian  
                            {
                                DroneSeq.AddNote(diatonicScales.Major_Scale1[3], droneSeqPos + 4.0f, droneSeqPos - 4.0f); //1
                                DroneSeq.AddNote(diatonicScales.Major_Scale2[5], droneSeqPos + 4.0f, droneSeqPos - 4.0f); //3

                                if (ChordVoicing == "Extended") // min7b913 voicing 7 on bottom
                                {
                                    DroneSeq.AddNote(diatonicScales.Major_Scale2[2], droneSeqPos + 4.0f, droneSeqPos - 4.0f); //7
                                    DroneSeq.AddNote(diatonicScales.Major_Scale3[1], droneSeqPos + 4.0f, droneSeqPos - 4.0f); //6
                                    DroneSeq.AddNote(diatonicScales.Major_Scale3[4], droneSeqPos + 4.0f, droneSeqPos - 4.0f); //9
                                    DroneSeq.AddNote(diatonicScales.Major_Scale3[7], droneSeqPos + 4.0f, droneSeqPos - 4.0f); //5
                                }
                                else
                                {
                                    DroneSeq.AddNote(diatonicScales.Major_Scale2[7], droneSeqPos + 4.0f, droneSeqPos - 4.0f); //5
                                }
                            }
                            else if (KeyType == "NaturalMinor") // Ionian Maj7(6)
                            {
                                DroneSeq.AddNote(diatonicScales.NatMinor_Scale1[3], droneSeqPos + 4.0f, droneSeqPos - 4.0f); //1
                                DroneSeq.AddNote(diatonicScales.NatMinor_Scale2[5], droneSeqPos + 4.0f, droneSeqPos - 4.0f); //3

                                if (ChordVoicing == "Extended")
                                {
                                    if (ChordType == "NonDiatonic") //7
                                    {
                                        DroneSeq.AddNote((diatonicScales.NatMinor_Scale3[2] - 1), droneSeqPos + 4.0f, droneSeqPos - 4.0f); //b7
                                    }
                                    else
                                    {
                                        DroneSeq.AddNote(diatonicScales.NatMinor_Scale3[2], droneSeqPos + 4.0f, droneSeqPos - 4.0f); //7
                                    }
                                    DroneSeq.AddNote(diatonicScales.NatMinor_Scale3[4], droneSeqPos + 4.0f, droneSeqPos - 4.0f); //9
                                    DroneSeq.AddNote(diatonicScales.NatMinor_Scale3[7], droneSeqPos + 4.0f, droneSeqPos - 4.0f); //5
                                }
                                else
                                {
                                    DroneSeq.AddNote(diatonicScales.NatMinor_Scale2[7], droneSeqPos + 4.0f, droneSeqPos - 4.0f); //5
                                }
                            }
                            break;

                        case 4: // CHORD IV 

                            if (KeyType == "Major") //Lydian 
                            {
                                DroneSeq.AddNote(diatonicScales.Major_Scale1[4], droneSeqPos + 4.0f, droneSeqPos - 4.0f); //1
                                DroneSeq.AddNote(diatonicScales.Major_Scale2[6], droneSeqPos + 4.0f, droneSeqPos - 4.0f); //3

                                if (ChordVoicing == "Extended") //maj7 voicing 3rd on bottom
                                {

                                    //     DroneSeq.AddNote(diatonicScales.Major_Scale3[3], droneSeqPos + 4.0f, droneSeqPos - 4.0f); //7
                                    DroneSeq.AddNote(diatonicScales.Major_Scale3[2], droneSeqPos + 4.0f, droneSeqPos - 4.0f); //6
                                    DroneSeq.AddNote(diatonicScales.Major_Scale3[5], droneSeqPos + 4.0f, droneSeqPos - 4.0f); //9
                                    DroneSeq.AddNote(diatonicScales.Major_Scale4[1], droneSeqPos + 4.0f, droneSeqPos - 4.0f); //5

                                    //     DroneSeq.AddNote(diatonicScales.Major_Scale3[7], droneSeqPos + 4.0f, droneSeqPos - 4.0f); //#11

                                }
                                else
                                {
                                    DroneSeq.AddNote(diatonicScales.Major_Scale2[1], droneSeqPos + 4.0f, droneSeqPos - 4.0f); //5
                                }
                            }
                            else if (KeyType == "NaturalMinor") //Dorian min7(9)
                            {
                                DroneSeq.AddNote(diatonicScales.NatMinor_Scale1[4], droneSeqPos + 4.0f, droneSeqPos - 4.0f); //1
                                DroneSeq.AddNote(diatonicScales.NatMinor_Scale2[6], droneSeqPos + 4.0f, droneSeqPos - 4.0f); //3


                                if (ChordVoicing == "Extended")
                                {
                                    DroneSeq.AddNote(diatonicScales.NatMinor_Scale3[3], droneSeqPos + 4.0f, droneSeqPos - 4.0f); //7
                                    DroneSeq.AddNote(diatonicScales.NatMinor_Scale3[5], droneSeqPos + 4.0f, droneSeqPos - 4.0f); //9
                                    DroneSeq.AddNote(diatonicScales.NatMinor_Scale4[1], droneSeqPos + 4.0f, droneSeqPos - 4.0f); //5
                                }
                                else
                                {
                                    DroneSeq.AddNote(diatonicScales.NatMinor_Scale2[1], droneSeqPos + 4.0f, droneSeqPos - 4.0f); //5
                                }
                            }
                            break;

                        case 5: // CHORD V7, V7alt(b5,b9), V-7

                            if (KeyType == "Major") // Mixolydian
                            {
                                DroneSeq.AddNote(diatonicScales.Major_Scale1[5], droneSeqPos + 4.0f, droneSeqPos - 4.0f); //1
                                DroneSeq.AddNote(diatonicScales.Major_Scale2[7], droneSeqPos + 4.0f, droneSeqPos - 4.0f); //3


                                if (ChordVoicing == "Extended") // dom7(9) voicings with 7 on bottom 73695
                                {
                                    if (ChordType == "NonDiatonic") //alt
                                    {
                                        DroneSeq.AddNote((diatonicScales.Major_Scale3[2] - 1), droneSeqPos + 4.0f, droneSeqPos - 4.0f); //b5
                                        DroneSeq.AddNote((diatonicScales.Major_Scale3[6] - 1), droneSeqPos + 4.0f, droneSeqPos - 4.0f); //b9
                                    }
                                    else
                                    {
                                        DroneSeq.AddNote(diatonicScales.Major_Scale4[2], droneSeqPos + 4.0f, droneSeqPos - 4.0f); //5
                                        DroneSeq.AddNote(diatonicScales.Major_Scale3[6], droneSeqPos + 4.0f, droneSeqPos - 4.0f); //9
                                        DroneSeq.AddNote(diatonicScales.Major_Scale3[3], droneSeqPos + 4.0f, droneSeqPos - 4.0f); //6
                                    }
                                    DroneSeq.AddNote(diatonicScales.Major_Scale2[4], droneSeqPos + 4.0f, droneSeqPos - 4.0f); //7
                                }
                                else
                                {
                                    DroneSeq.AddNote(diatonicScales.Major_Scale2[2], droneSeqPos + 4.0f, droneSeqPos - 4.0f); //5
                                }
                            }
                            else if (KeyType == "NaturalMinor") //Phrygian
                            {
                                DroneSeq.AddNote(diatonicScales.NatMinor_Scale1[5], droneSeqPos + 4.0f, droneSeqPos - 4.0f); //1
                                DroneSeq.AddNote(diatonicScales.NatMinor_Scale2[7], droneSeqPos + 4.0f, droneSeqPos - 4.0f); //3


                                if (ChordVoicing == "Extended") //min7(9)
                                {
                                    DroneSeq.AddNote(diatonicScales.NatMinor_Scale2[4], droneSeqPos + 4.0f, droneSeqPos - 4.0f); //7
                                    DroneSeq.AddNote(diatonicScales.NatMinor_Scale3[3], droneSeqPos + 4.0f, droneSeqPos - 4.0f); //6
                                    DroneSeq.AddNote(diatonicScales.NatMinor_Scale3[6], droneSeqPos + 4.0f, droneSeqPos - 4.0f); //9
                                    DroneSeq.AddNote(diatonicScales.NatMinor_Scale4[2], droneSeqPos + 4.0f, droneSeqPos - 4.0f); //5
                                }
                                else
                                {
                                    DroneSeq.AddNote(diatonicScales.NatMinor_Scale2[2], droneSeqPos + 4.0f, droneSeqPos - 4.0f); //5
                                }
                            }
                            break;

                        case 6: // CHORD VI-7, VImaj7, VI7  

                            if (KeyType == "Major") //Aeolian
                            {
                                DroneSeq.AddNote(diatonicScales.Major_Scale1[6], droneSeqPos + 4.0f, droneSeqPos - 4.0f); //1
                                DroneSeq.AddNote(diatonicScales.Major_Scale2[1], droneSeqPos + 4.0f, droneSeqPos - 4.0f); //3


                                if (ChordVoicing == "Extended") // min7(9) voicing 3  at bottom 3795
                                {
                                    DroneSeq.AddNote(diatonicScales.Major_Scale2[5], droneSeqPos + 4.0f, droneSeqPos - 4.0f); //7
                                    DroneSeq.AddNote(diatonicScales.Major_Scale2[7], droneSeqPos + 4.0f, droneSeqPos - 4.0f); //9
                                    DroneSeq.AddNote(diatonicScales.Major_Scale3[3], droneSeqPos + 4.0f, droneSeqPos - 4.0f); //5
                                }
                                else
                                {
                                    DroneSeq.AddNote(diatonicScales.Major_Scale2[3], droneSeqPos + 4.0f, droneSeqPos - 4.0f); //5
                                }
                            }
                            else if (KeyType == "NaturalMinor") //Lydian
                            {
                                DroneSeq.AddNote(diatonicScales.NatMinor_Scale1[6], droneSeqPos + 4.0f, droneSeqPos - 4.0f); //1
                                DroneSeq.AddNote(diatonicScales.NatMinor_Scale2[1], droneSeqPos + 4.0f, droneSeqPos - 4.0f); //3


                                if (ChordVoicing == "Extended") //maj7 and 7
                                {
                                    if (ChordType == "NonDiatonic")
                                    {
                                        DroneSeq.AddNote((diatonicScales.NatMinor_Scale2[5] - 1), droneSeqPos + 4.0f, droneSeqPos - 4.0f); //b7 
                                    }
                                    else
                                    {
                                        DroneSeq.AddNote(diatonicScales.NatMinor_Scale2[4], droneSeqPos + 4.0f, droneSeqPos - 4.0f); //6 
                                    }

                                    DroneSeq.AddNote(diatonicScales.NatMinor_Scale2[7], droneSeqPos + 4.0f, droneSeqPos - 4.0f); //9
                                    DroneSeq.AddNote(diatonicScales.NatMinor_Scale3[3], droneSeqPos + 4.0f, droneSeqPos - 4.0f); //5
                                }
                                else
                                {
                                    DroneSeq.AddNote(diatonicScales.NatMinor_Scale2[3], droneSeqPos + 4.0f, droneSeqPos - 4.0f); //5
                                }
                            }
                            break;

                        case 7: // CHORD VII0, VIIo, VII-7, VII7

                            if (KeyType == "Major") //Locrian 
                            {

                                DroneSeq.AddNote(diatonicScales.Major_Scale2[2], droneSeqPos + 4.0f, droneSeqPos - 4.0f); //b3
                                DroneSeq.AddNote(diatonicScales.Major_Scale2[4], droneSeqPos + 4.0f, droneSeqPos - 4.0f); //b5

                                if (ChordVoicing == "Extended") //min7b5 voicings 7 on bottom 3,5,1,11,7
                                {
                                    if (ChordType == "NonDiatonic") //VIIO
                                    {
                                        DroneSeq.AddNote((diatonicScales.Major_Scale3[6] - 1), droneSeqPos + 4.0f, droneSeqPos - 4.0f); //bb7
                                    }
                                    else
                                    {
                                        DroneSeq.AddNote(diatonicScales.Major_Scale3[6], droneSeqPos + 4.0f, droneSeqPos - 4.0f); //b7
                                    }
                                    DroneSeq.AddNote(diatonicScales.Major_Scale2[7], droneSeqPos + 4.0f, droneSeqPos - 4.0f); //1
                                    DroneSeq.AddNote(diatonicScales.Major_Scale3[3], droneSeqPos + 4.0f, droneSeqPos - 4.0f); //11

                                }
                                else
                                {
                                    DroneSeq.AddNote(diatonicScales.Major_Scale1[7], droneSeqPos + 4.0f, droneSeqPos - 4.0f); //1
                                }
                            }
                            else if (KeyType == "NaturalMinor") //Mixolydian
                            {
                                DroneSeq.AddNote(diatonicScales.NatMinor_Scale1[7], droneSeqPos + 4.0f, droneSeqPos - 4.0f); //1

                                if (ChordVoicing == "Extended") //dom7 73695
                                {
                                    if (ChordType == "NonDiatonic") //min7
                                    {
                                        DroneSeq.AddNote((diatonicScales.NatMinor_Scale3[2] - 1), droneSeqPos + 4.0f, droneSeqPos - 4.0f); //b3
                                    }
                                    else
                                    {
                                        DroneSeq.AddNote(diatonicScales.NatMinor_Scale3[2], droneSeqPos + 4.0f, droneSeqPos - 4.0f); //3
                                    }
                                    DroneSeq.AddNote(diatonicScales.NatMinor_Scale2[6], droneSeqPos + 4.0f, droneSeqPos - 4.0f); //b7
                                    DroneSeq.AddNote(diatonicScales.NatMinor_Scale3[1], droneSeqPos + 4.0f, droneSeqPos - 4.0f); //9
                                    DroneSeq.AddNote(diatonicScales.NatMinor_Scale3[4], droneSeqPos + 4.0f, droneSeqPos - 4.0f); //5
                                }
                                else
                                {
                                    DroneSeq.AddNote(diatonicScales.NatMinor_Scale2[2], droneSeqPos + 4.0f, droneSeqPos - 4.0f); //3
                                    DroneSeq.AddNote(diatonicScales.NatMinor_Scale2[4], droneSeqPos + 4.0f, droneSeqPos - 4.0f); //5
                                }
                            }
                            break;
                    }
                }
                chords[i] = false;
            }
        }

        public void currentchord()
        {
            if (chords[0]) currChord = 0;
            if (chords[1]) currChord = 1;
            if (chords[2]) currChord = 2;
            if (chords[3]) currChord = 3;
            if (chords[4]) currChord = 4;
            if (chords[5]) currChord = 5;
            if (chords[6]) currChord = 6;
            if (chords[7]) currChord = 7;
        }
    }
}
