using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SoliGameController;

namespace SoliSoundScape
{
    public class ChordProgressions : MonoBehaviour
    {
        DiatonicScales diatonicScales;
        public GameController game_c;
        public GestureController gesture_c;

        [Header("Synth Section")]
        public AudioHelm.HelmController DroneSynth;
        public AudioHelm.Sampler Piano;
        [Header("Sequencer Section")]
        public AudioHelm.Sequencer DroneSeq;
        public int droneSeqPos;
        [Header("Key,Scale & Chord Picker")]
        public string Key, KeyType, ChordVoicing, ChordType;
        public bool[] chords = new bool[8];
        public int currChord, trumpetNote, saxNote, trumpetRange, saxRange, prevTpt_Range, prevSax_Range;
        [Header("Level Picker")]
        public bool Run, isFocus, f_sw;
        [Header("NoteParameters")]
        //   public float ;

        private int[] MajDomnoteSelector = new int[5];
        private int[] MinnoteSelector = new int[5];
        private int[] LydiannoteSelector = new int[6];
        private int[] PhygiannoteSelector = new int[4];
        private int[] LocriannoteSelector = new int[5];

        // Use this for initialization
        void Start()
        {
            diatonicScales = this.GetComponent<DiatonicScales>();
            chord1NoteShift();
        }

        // Update is called once per frame
        void Update()
        {
            droneSeqPos = (int)DroneSeq.GetSequencerPosition();

            isFocus = gesture_c.isFocus;

            diatonicScales.MajorScales(Key);
            diatonicScales.NatMinorScales(Key);

            currentchord();
            SamplerEnable();

            if (chords[1] || chords[2] || chords[3] || chords[4] || chords[5] || chords[6] || chords[7])
            {
                DroneSynth.AllNotesOff();
                DroneSeq.Clear();
                DroneEnable();
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

        private void chord1NoteShift()
        {
            MajDomnoteSelector[0] = 2;
            MajDomnoteSelector[1] = 3;
            MajDomnoteSelector[2] = 5;
            MajDomnoteSelector[3] = 6;
            MajDomnoteSelector[4] = 7;

            MinnoteSelector[0] = 2;
            MinnoteSelector[1] = 3;
            MinnoteSelector[2] = 4;
            MinnoteSelector[3] = 5;
            MinnoteSelector[4] = 7;

            LydiannoteSelector[0] = 2;
            LydiannoteSelector[1] = 3;
            LydiannoteSelector[2] = 4;
            LydiannoteSelector[3] = 5;
            LydiannoteSelector[4] = 6;
            LydiannoteSelector[5] = 7;

            PhygiannoteSelector[0] = 3;
            PhygiannoteSelector[1] = 4;
            PhygiannoteSelector[2] = 5;
            PhygiannoteSelector[3] = 7;

            LocriannoteSelector[0] = 3;
            LocriannoteSelector[1] = 4;
            LocriannoteSelector[2] = 5;
            LocriannoteSelector[3] = 6;
            LocriannoteSelector[4] = 7;
        }
        private void chord2NoteShift()
        {
            MajDomnoteSelector[0] = 3;
            MajDomnoteSelector[1] = 4;
            MajDomnoteSelector[2] = 6;
            MajDomnoteSelector[3] = 7;
            MajDomnoteSelector[4] = 1;

            MinnoteSelector[0] = 3;
            MinnoteSelector[1] = 4;
            MinnoteSelector[2] = 5;
            MinnoteSelector[3] = 6;
            MinnoteSelector[4] = 1;

            LydiannoteSelector[0] = 3;
            LydiannoteSelector[1] = 4;
            LydiannoteSelector[2] = 5;
            LydiannoteSelector[3] = 6;
            LydiannoteSelector[4] = 7;
            LydiannoteSelector[5] = 1;

            PhygiannoteSelector[0] = 4;
            PhygiannoteSelector[1] = 5;
            PhygiannoteSelector[2] = 6;
            PhygiannoteSelector[3] = 1;

            LocriannoteSelector[0] = 4;
            LocriannoteSelector[1] = 5;
            LocriannoteSelector[2] = 6;
            LocriannoteSelector[3] = 7;
            LocriannoteSelector[4] = 1;
        }
        private void chord3NoteShift()
        {
            MajDomnoteSelector[0] = 4;
            MajDomnoteSelector[1] = 5;
            MajDomnoteSelector[2] = 7;
            MajDomnoteSelector[3] = 1;
            MajDomnoteSelector[4] = 2;

            MinnoteSelector[0] = 4;
            MinnoteSelector[1] = 5;
            MinnoteSelector[2] = 6;
            MinnoteSelector[3] = 7;
            MinnoteSelector[4] = 2;

            LydiannoteSelector[0] = 4;
            LydiannoteSelector[1] = 5;
            LydiannoteSelector[2] = 6;
            LydiannoteSelector[3] = 7;
            LydiannoteSelector[4] = 1;
            LydiannoteSelector[5] = 2;

            PhygiannoteSelector[0] = 5;
            PhygiannoteSelector[1] = 6;
            PhygiannoteSelector[2] = 7;
            PhygiannoteSelector[3] = 2;

            LocriannoteSelector[0] = 5;
            LocriannoteSelector[1] = 6;
            LocriannoteSelector[2] = 7;
            LocriannoteSelector[3] = 1;
            LocriannoteSelector[4] = 2;
        }
        private void chord4NoteShift()
        {
            MajDomnoteSelector[0] = 5;
            MajDomnoteSelector[1] = 6;
            MajDomnoteSelector[2] = 1;
            MajDomnoteSelector[3] = 2;
            MajDomnoteSelector[4] = 3;

            MinnoteSelector[0] = 5;
            MinnoteSelector[1] = 6;
            MinnoteSelector[2] = 7;
            MinnoteSelector[3] = 1;
            MinnoteSelector[4] = 3;

            LydiannoteSelector[0] = 5;
            LydiannoteSelector[1] = 6;
            LydiannoteSelector[2] = 7;
            LydiannoteSelector[3] = 1;
            LydiannoteSelector[4] = 2;
            LydiannoteSelector[5] = 3;

            PhygiannoteSelector[0] = 6;
            PhygiannoteSelector[1] = 7;
            PhygiannoteSelector[2] = 1;
            PhygiannoteSelector[3] = 3;

            LocriannoteSelector[0] = 6;
            LocriannoteSelector[1] = 7;
            LocriannoteSelector[2] = 1;
            LocriannoteSelector[3] = 2;
            LocriannoteSelector[4] = 3;
        }
        private void chord5NoteShift()
        {
            MajDomnoteSelector[0] = 6;
            MajDomnoteSelector[1] = 7;
            MajDomnoteSelector[2] = 2;
            MajDomnoteSelector[3] = 3;
            MajDomnoteSelector[4] = 4;

            MinnoteSelector[0] = 6;
            MinnoteSelector[1] = 7;
            MinnoteSelector[2] = 1;
            MinnoteSelector[3] = 2;
            MinnoteSelector[4] = 4;

            LydiannoteSelector[0] = 6;
            LydiannoteSelector[1] = 7;
            LydiannoteSelector[2] = 1;
            LydiannoteSelector[3] = 2;
            LydiannoteSelector[4] = 3;
            LydiannoteSelector[5] = 4;

            PhygiannoteSelector[0] = 7;
            PhygiannoteSelector[1] = 1;
            PhygiannoteSelector[2] = 2;
            PhygiannoteSelector[3] = 4;

            LocriannoteSelector[0] = 7;
            LocriannoteSelector[1] = 1;
            LocriannoteSelector[2] = 2;
            LocriannoteSelector[3] = 3;
            LocriannoteSelector[4] = 4;
        }
        private void chord6NoteShift()
        {
            MajDomnoteSelector[0] = 7;
            MajDomnoteSelector[1] = 1;
            MajDomnoteSelector[2] = 3;
            MajDomnoteSelector[3] = 4;
            MajDomnoteSelector[4] = 5;

            MinnoteSelector[0] = 7;
            MinnoteSelector[1] = 1;
            MinnoteSelector[2] = 2;
            MinnoteSelector[3] = 3;
            MinnoteSelector[4] = 5;

            LydiannoteSelector[0] = 7;
            LydiannoteSelector[1] = 1;
            LydiannoteSelector[2] = 2;
            LydiannoteSelector[3] = 3;
            LydiannoteSelector[4] = 4;
            LydiannoteSelector[5] = 5;

            PhygiannoteSelector[0] = 1;
            PhygiannoteSelector[1] = 2;
            PhygiannoteSelector[2] = 3;
            PhygiannoteSelector[3] = 5;

            LocriannoteSelector[0] = 1;
            LocriannoteSelector[1] = 2;
            LocriannoteSelector[2] = 3;
            LocriannoteSelector[3] = 4;
            LocriannoteSelector[4] = 5;
        }
        private void chord7NoteShift()
        {
            MajDomnoteSelector[0] = 1;
            MajDomnoteSelector[1] = 2;
            MajDomnoteSelector[2] = 4;
            MajDomnoteSelector[3] = 5;
            MajDomnoteSelector[4] = 6;

            MinnoteSelector[0] = 1;
            MinnoteSelector[1] = 2;
            MinnoteSelector[2] = 3;
            MinnoteSelector[3] = 4;
            MinnoteSelector[4] = 6;

            LydiannoteSelector[0] = 1;
            LydiannoteSelector[1] = 2;
            LydiannoteSelector[2] = 3;
            LydiannoteSelector[3] = 4;
            LydiannoteSelector[4] = 5;
            LydiannoteSelector[5] = 6;

            PhygiannoteSelector[0] = 2;
            PhygiannoteSelector[1] = 3;
            PhygiannoteSelector[2] = 4;
            PhygiannoteSelector[3] = 6;

            LocriannoteSelector[0] = 2;
            LocriannoteSelector[1] = 3;
            LocriannoteSelector[2] = 4;
            LocriannoteSelector[3] = 5;
            LocriannoteSelector[4] = 6;
        }

        private void MajDom_TNoteSelector()
        {
            var noteSelector = Random.Range(0, MajDomnoteSelector.Length);
            if (noteSelector == 0) trumpetNote = MajDomnoteSelector[0];
            if (noteSelector == 1) trumpetNote = MajDomnoteSelector[1];
            if (noteSelector == 2) trumpetNote = MajDomnoteSelector[2];
            if (noteSelector == 3) trumpetNote = MajDomnoteSelector[3];
            if (noteSelector == 4) trumpetNote = MajDomnoteSelector[4];
            Debug.Log("TRUMPETNOTE " + trumpetNote);
        }
        private void Min_TNoteSelector()
        {
            var noteSelector = Random.Range(0, MinnoteSelector.Length);
            if (noteSelector == 0) trumpetNote = MinnoteSelector[0];
            if (noteSelector == 1) trumpetNote = MinnoteSelector[1];
            if (noteSelector == 2) trumpetNote = MinnoteSelector[2];
            if (noteSelector == 3) trumpetNote = MinnoteSelector[3];
            if (noteSelector == 4) trumpetNote = MinnoteSelector[4];
            Debug.Log("TRUMPETNOTE " + trumpetNote);
        }
        private void Locrian_TNoteSelector()
        {
            var noteSelector = Random.Range(0, LocriannoteSelector.Length);
            if (noteSelector == 0) trumpetNote = LocriannoteSelector[0];
            if (noteSelector == 1) trumpetNote = LocriannoteSelector[1];
            if (noteSelector == 2) trumpetNote = LocriannoteSelector[2];
            if (noteSelector == 3) trumpetNote = LocriannoteSelector[3];
            if (noteSelector == 4) trumpetNote = LocriannoteSelector[4];
            Debug.Log("TRUMPETNOTE " + trumpetNote);
        }
        private void Phygian_TNoteSelector()
        {
            var noteSelector = Random.Range(0, PhygiannoteSelector.Length);
            if (noteSelector == 0) trumpetNote = PhygiannoteSelector[0];
            if (noteSelector == 1) trumpetNote = PhygiannoteSelector[1];
            if (noteSelector == 2) trumpetNote = PhygiannoteSelector[2];
            if (noteSelector == 3) trumpetNote = PhygiannoteSelector[3];
            Debug.Log("TRUMPETNOTE " + trumpetNote);
        }
        private void Lydian_TNoteSelector()
        {
            var noteSelector = Random.Range(0, LydiannoteSelector.Length);
            if (noteSelector == 0) trumpetNote = LydiannoteSelector[0];
            if (noteSelector == 1) trumpetNote = LydiannoteSelector[1];
            if (noteSelector == 2) trumpetNote = LydiannoteSelector[2];
            if (noteSelector == 3) trumpetNote = LydiannoteSelector[3];
            if (noteSelector == 4) trumpetNote = LydiannoteSelector[4];
            if (noteSelector == 5) trumpetNote = LydiannoteSelector[5];
            Debug.Log("TRUMPETNOTE " + trumpetNote);
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
                                DroneSeq.AddNote(diatonicScales.Major_Scale1[1], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //1
                                DroneSeq.AddNote(diatonicScales.Major_Scale2[3], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //3

                                if (ChordVoicing == "Extended") // voicings with 3rd on bottom maj7(6)
                                {

                                    DroneSeq.AddNote(diatonicScales.Major_Scale2[6], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //6
                                    DroneSeq.AddNote(diatonicScales.Major_Scale3[2], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //9                              
                                    DroneSeq.AddNote(diatonicScales.Major_Scale3[5], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //5
                                }
                                else
                                {
                                    DroneSeq.AddNote(diatonicScales.Major_Scale2[5], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //5
                                }
                            }
                            else if (KeyType == "NaturalMinor") // Aeolian min7(9)
                            {
                                DroneSeq.AddNote(diatonicScales.NatMinor_Scale1[1], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //1
                                DroneSeq.AddNote(diatonicScales.NatMinor_Scale2[3], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //3                              

                                if (ChordVoicing == "Extended")
                                {
                                    DroneSeq.AddNote(diatonicScales.NatMinor_Scale2[7], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //7
                                    DroneSeq.AddNote(diatonicScales.NatMinor_Scale3[2], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //9
                                    DroneSeq.AddNote(diatonicScales.NatMinor_Scale3[5], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //5
                                }
                                else
                                {

                                    DroneSeq.AddNote(diatonicScales.NatMinor_Scale2[5], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //5
                                }
                            }
                            break;

                        case 2: //CHORD IImin7, II7, 

                            if (KeyType == "Major") //Dorian min7(9)
                            {
                                DroneSeq.AddNote(diatonicScales.Major_Scale1[2], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //1

                                if (ChordType == "NonDiatonic") //II7
                                {
                                    DroneSeq.AddNote((diatonicScales.Major_Scale2[4] + 1), droneSeqPos + 2.5f, droneSeqPos - 4.0f); //3
                                }
                                else
                                {
                                    DroneSeq.AddNote(diatonicScales.Major_Scale2[4], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //b3
                                }

                                if (ChordVoicing == "Extended") // voicings with 3rd on bottom
                                {

                                    DroneSeq.AddNote(diatonicScales.Major_Scale2[1], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //7
                                    DroneSeq.AddNote(diatonicScales.Major_Scale3[3], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //9
                                    DroneSeq.AddNote(diatonicScales.Major_Scale3[6], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //5

                                }
                                else
                                {
                                    DroneSeq.AddNote(diatonicScales.Major_Scale2[6], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //5
                                }
                            }
                            else if (KeyType == "NaturalMinor") // Locrian min7b5 3,5,1,11,7
                            {

                                DroneSeq.AddNote(diatonicScales.NatMinor_Scale2[4], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //3
                                DroneSeq.AddNote(diatonicScales.NatMinor_Scale2[6], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //5

                                if (ChordVoicing == "Extended")
                                {
                                    DroneSeq.AddNote(diatonicScales.NatMinor_Scale3[2], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //1                                
                                    DroneSeq.AddNote(diatonicScales.NatMinor_Scale3[5], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //11
                                    DroneSeq.AddNote(diatonicScales.NatMinor_Scale4[1], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //7
                                }
                                else
                                {
                                    DroneSeq.AddNote(diatonicScales.NatMinor_Scale1[2], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //1
                                }
                            }
                            break;

                        case 3: // CHORD III-7, IIImaj7, III7

                            if (KeyType == "Major") // Phrygian  
                            {
                                DroneSeq.AddNote(diatonicScales.Major_Scale1[3], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //1
                                DroneSeq.AddNote(diatonicScales.Major_Scale2[5], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //3

                                if (ChordVoicing == "Extended") // min7b913 voicing 7 on bottom
                                {
                                    DroneSeq.AddNote(diatonicScales.Major_Scale2[2], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //7
                                    DroneSeq.AddNote(diatonicScales.Major_Scale3[1], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //6
                                    DroneSeq.AddNote(diatonicScales.Major_Scale3[4], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //9
                                    DroneSeq.AddNote(diatonicScales.Major_Scale3[7], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //5
                                }
                                else
                                {
                                    DroneSeq.AddNote(diatonicScales.Major_Scale2[7], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //5
                                }
                            }
                            else if (KeyType == "NaturalMinor") // Ionian Maj7(6)
                            {
                                DroneSeq.AddNote(diatonicScales.NatMinor_Scale1[3], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //1
                                DroneSeq.AddNote(diatonicScales.NatMinor_Scale2[5], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //3

                                if (ChordVoicing == "Extended")
                                {
                                    if (ChordType == "NonDiatonic") //7
                                    {
                                        DroneSeq.AddNote((diatonicScales.NatMinor_Scale3[2] - 1), droneSeqPos + 2.5f, droneSeqPos - 4.0f); //b7
                                    }
                                    else
                                    {
                                        DroneSeq.AddNote(diatonicScales.NatMinor_Scale3[2], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //7
                                    }
                                    DroneSeq.AddNote(diatonicScales.NatMinor_Scale3[4], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //9
                                    DroneSeq.AddNote(diatonicScales.NatMinor_Scale3[7], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //5
                                }
                                else
                                {
                                    DroneSeq.AddNote(diatonicScales.NatMinor_Scale2[7], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //5
                                }
                            }
                            break;

                        case 4: // CHORD IV 

                            if (KeyType == "Major") //Lydian 
                            {
                                DroneSeq.AddNote(diatonicScales.Major_Scale1[4], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //1
                                DroneSeq.AddNote(diatonicScales.Major_Scale2[6], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //3

                                if (ChordVoicing == "Extended") //maj7 voicing 3rd on bottom
                                {

                                    //     DroneSeq.AddNote(diatonicScales.Major_Scale3[3], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //7
                                    DroneSeq.AddNote(diatonicScales.Major_Scale3[2], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //6
                                    DroneSeq.AddNote(diatonicScales.Major_Scale3[5], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //9
                                    DroneSeq.AddNote(diatonicScales.Major_Scale4[1], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //5

                                    //     DroneSeq.AddNote(diatonicScales.Major_Scale3[7], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //#11

                                }
                                else
                                {
                                    DroneSeq.AddNote(diatonicScales.Major_Scale2[1], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //5
                                }
                            }
                            else if (KeyType == "NaturalMinor") //Dorian min7(9)
                            {
                                DroneSeq.AddNote(diatonicScales.NatMinor_Scale1[4], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //1
                                DroneSeq.AddNote(diatonicScales.NatMinor_Scale2[6], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //3


                                if (ChordVoicing == "Extended")
                                {
                                    DroneSeq.AddNote(diatonicScales.NatMinor_Scale3[3], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //7
                                    DroneSeq.AddNote(diatonicScales.NatMinor_Scale3[5], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //9
                                    DroneSeq.AddNote(diatonicScales.NatMinor_Scale4[1], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //5
                                }
                                else
                                {
                                    DroneSeq.AddNote(diatonicScales.NatMinor_Scale2[1], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //5
                                }
                            }
                            break;

                        case 5: // CHORD V7, V7alt(b5,b9), V-7

                            if (KeyType == "Major") // Mixolydian
                            {
                                DroneSeq.AddNote(diatonicScales.Major_Scale1[5], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //1
                                DroneSeq.AddNote(diatonicScales.Major_Scale2[7], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //3


                                if (ChordVoicing == "Extended") // dom7(9) voicings with 7 on bottom 73695
                                {
                                    if (ChordType == "NonDiatonic") //alt
                                    {
                                        DroneSeq.AddNote((diatonicScales.Major_Scale3[2] - 1), droneSeqPos + 2.5f, droneSeqPos - 4.0f); //b5
                                        DroneSeq.AddNote((diatonicScales.Major_Scale3[6] - 1), droneSeqPos + 2.5f, droneSeqPos - 4.0f); //b9
                                    }
                                    else
                                    {
                                        DroneSeq.AddNote(diatonicScales.Major_Scale4[2], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //5
                                        DroneSeq.AddNote(diatonicScales.Major_Scale3[6], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //9
                                        DroneSeq.AddNote(diatonicScales.Major_Scale3[3], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //6
                                    }
                                    DroneSeq.AddNote(diatonicScales.Major_Scale2[4], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //7
                                }
                                else
                                {
                                    DroneSeq.AddNote(diatonicScales.Major_Scale2[2], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //5
                                }
                            }
                            else if (KeyType == "NaturalMinor") //Phrygian
                            {
                                DroneSeq.AddNote(diatonicScales.NatMinor_Scale1[5], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //1
                                DroneSeq.AddNote(diatonicScales.NatMinor_Scale2[7], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //3


                                if (ChordVoicing == "Extended") //min7(9)
                                {
                                    DroneSeq.AddNote(diatonicScales.NatMinor_Scale2[4], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //7
                                    DroneSeq.AddNote(diatonicScales.NatMinor_Scale3[3], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //6
                                    DroneSeq.AddNote(diatonicScales.NatMinor_Scale3[6], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //9
                                    DroneSeq.AddNote(diatonicScales.NatMinor_Scale4[2], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //5
                                }
                                else
                                {
                                    DroneSeq.AddNote(diatonicScales.NatMinor_Scale2[2], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //5
                                }
                            }
                            break;

                        case 6: // CHORD VI-7, VImaj7, VI7  

                            if (KeyType == "Major") //Aeolian
                            {
                                DroneSeq.AddNote(diatonicScales.Major_Scale1[6], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //1
                                DroneSeq.AddNote(diatonicScales.Major_Scale2[1], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //3


                                if (ChordVoicing == "Extended") // min7(9) voicing 3  at bottom 3795
                                {
                                    DroneSeq.AddNote(diatonicScales.Major_Scale2[5], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //7
                                    DroneSeq.AddNote(diatonicScales.Major_Scale2[7], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //9
                                    DroneSeq.AddNote(diatonicScales.Major_Scale3[3], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //5
                                }
                                else
                                {
                                    DroneSeq.AddNote(diatonicScales.Major_Scale2[3], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //5
                                }
                            }
                            else if (KeyType == "NaturalMinor") //Lydian
                            {
                                DroneSeq.AddNote(diatonicScales.NatMinor_Scale1[6], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //1
                                DroneSeq.AddNote(diatonicScales.NatMinor_Scale2[1], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //3


                                if (ChordVoicing == "Extended") //maj7 and 7
                                {
                                    if (ChordType == "NonDiatonic")
                                    {
                                        DroneSeq.AddNote((diatonicScales.NatMinor_Scale2[5] - 1), droneSeqPos + 2.5f, droneSeqPos - 4.0f); //b7 
                                    }
                                    else
                                    {
                                        DroneSeq.AddNote(diatonicScales.NatMinor_Scale2[4], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //6 
                                    }

                                    DroneSeq.AddNote(diatonicScales.NatMinor_Scale2[7], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //9
                                    DroneSeq.AddNote(diatonicScales.NatMinor_Scale3[3], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //5
                                }
                                else
                                {
                                    DroneSeq.AddNote(diatonicScales.NatMinor_Scale2[3], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //5
                                }
                            }
                            break;

                        case 7: // CHORD VII0, VIIo, VII-7, VII7

                            if (KeyType == "Major") //Locrian 
                            {

                                DroneSeq.AddNote(diatonicScales.Major_Scale2[2], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //b3
                                DroneSeq.AddNote(diatonicScales.Major_Scale2[4], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //b5

                                if (ChordVoicing == "Extended") //min7b5 voicings 7 on bottom 3,5,1,11,7
                                {
                                    if (ChordType == "NonDiatonic") //VIIO
                                    {
                                        DroneSeq.AddNote((diatonicScales.Major_Scale3[6] - 1), droneSeqPos + 2.5f, droneSeqPos - 4.0f); //bb7
                                    }
                                    else
                                    {
                                        DroneSeq.AddNote(diatonicScales.Major_Scale3[6], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //b7
                                    }
                                    DroneSeq.AddNote(diatonicScales.Major_Scale2[7], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //1
                                    DroneSeq.AddNote(diatonicScales.Major_Scale3[3], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //11

                                }
                                else
                                {
                                    DroneSeq.AddNote(diatonicScales.Major_Scale1[7], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //1
                                }
                            }
                            else if (KeyType == "NaturalMinor") //Mixolydian
                            {
                                DroneSeq.AddNote(diatonicScales.NatMinor_Scale1[7], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //1

                                if (ChordVoicing == "Extended") //dom7 73695
                                {
                                    if (ChordType == "NonDiatonic") //min7
                                    {
                                        DroneSeq.AddNote((diatonicScales.NatMinor_Scale3[2] - 1), droneSeqPos + 2.5f, droneSeqPos - 4.0f); //b3
                                    }
                                    else
                                    {
                                        DroneSeq.AddNote(diatonicScales.NatMinor_Scale3[2], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //3
                                    }
                                    DroneSeq.AddNote(diatonicScales.NatMinor_Scale2[6], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //b7
                                    DroneSeq.AddNote(diatonicScales.NatMinor_Scale3[1], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //9
                                    DroneSeq.AddNote(diatonicScales.NatMinor_Scale3[4], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //5
                                }
                                else
                                {
                                    DroneSeq.AddNote(diatonicScales.NatMinor_Scale2[2], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //3
                                    DroneSeq.AddNote(diatonicScales.NatMinor_Scale2[4], droneSeqPos + 2.5f, droneSeqPos - 4.0f); //5
                                }
                            }
                            break;
                    }
                }
                chords[i] = false;
            }
        }

        public void SamplerEnable()
        {
            if (isFocus)
            {
                switch (currChord)
                {
                    case 1: //CHORD I 

                        if (KeyType == "Major") //Ionian 
                        {

                            MajDom_TNoteSelector();

                            if (prevTpt_Range == 0 || prevTpt_Range == 2) trumpetRange = Random.Range(1, 3);
                            if (prevTpt_Range == 1) trumpetRange = Random.Range(1, 2);
                            if (prevTpt_Range == 3) trumpetRange = Random.Range(2, 3);

                            if (trumpetRange == 1)
                            {
                                Piano.NoteOn(diatonicScales.Major_Scale1[trumpetNote], 1.0f);
                                prevTpt_Range = 1;
                            }
                            else if (trumpetRange == 2)
                            {
                                Piano.NoteOn(diatonicScales.Major_Scale2[trumpetNote], 1.0f);
                                prevTpt_Range = 2;
                            }
                            else if (trumpetRange == 3)
                            {
                                Piano.NoteOn(diatonicScales.Major_Scale3[trumpetNote], 1.0f);
                                prevTpt_Range = 3;
                            }

                        }
                        else if (KeyType == "NaturalMinor") // Aeolian min7(9)
                        {
                            Min_TNoteSelector();

                            if (prevTpt_Range == 0 || prevTpt_Range == 2) trumpetRange = Random.Range(1, 3);
                            if (prevTpt_Range == 1) trumpetRange = Random.Range(1, 2);
                            if (prevTpt_Range == 3) trumpetRange = Random.Range(2, 3);

                            if (trumpetRange == 1)
                            {
                                Piano.NoteOn(diatonicScales.NatMinor_Scale1[trumpetNote], 1.0f);
                                prevTpt_Range = 1;
                            }
                            else if (trumpetRange == 2)
                            {
                                Piano.NoteOn(diatonicScales.NatMinor_Scale2[trumpetNote], 1.0f);
                                prevTpt_Range = 2;
                            }
                            else if (trumpetRange == 3)
                            {
                                Piano.NoteOn(diatonicScales.NatMinor_Scale3[trumpetNote], 1.0f);
                                prevTpt_Range = 3;
                            }
                        }

                        break;

                    case 2: //CHORD IImin7, II7, 

                        chord2NoteShift();

                        if (KeyType == "Major") //Dorian min7(9)
                        {
                            if (ChordType == "NonDiatonic")
                            {
                                MajDom_TNoteSelector();
                            }
                            else
                            {
                                Min_TNoteSelector();
                            }

                            if (prevTpt_Range == 0 || prevTpt_Range == 2) trumpetRange = Random.Range(1, 3);
                            if (prevTpt_Range == 1) trumpetRange = Random.Range(1, 2);
                            if (prevTpt_Range == 3) trumpetRange = Random.Range(2, 3);

                            if (trumpetRange == 1)
                            {
                                if (ChordType == "NonDiatonic" && trumpetNote == 4) //II7
                                {
                                    Piano.NoteOn((diatonicScales.Major_Scale1[trumpetNote] + 1), 1.0f); //3
                                    var tNote = (diatonicScales.Major_Scale1[trumpetNote] + 1);
                                    var oldtnote = diatonicScales.Major_Scale1[trumpetNote];
                                    Debug.Log("minthird" + oldtnote);
                                    Debug.Log("domthird" + tNote);
                                }
                                else
                                {
                                    Piano.NoteOn(diatonicScales.Major_Scale1[trumpetNote], 1.0f);
                                }
                                prevTpt_Range = 1;
                            }
                            else if (trumpetRange == 2)
                            {
                                if (ChordType == "NonDiatonic" && trumpetNote == 4) //II7
                                {
                                    Piano.NoteOn((diatonicScales.Major_Scale2[trumpetNote] + 1), 1.0f); //3
                                    var tNote = (diatonicScales.Major_Scale2[trumpetNote] + 1);
                                    var oldtnote = diatonicScales.Major_Scale2[trumpetNote];
                                    Debug.Log("minthird" + oldtnote);
                                    Debug.Log("domthird" + tNote);
                                }
                                else
                                {
                                    Piano.NoteOn(diatonicScales.Major_Scale2[trumpetNote], 1.0f);
                                }
                                prevTpt_Range = 2;
                            }
                            else if (trumpetRange == 3)
                            {
                                if (ChordType == "NonDiatonic" && trumpetNote == 4) //II7
                                {
                                    Piano.NoteOn((diatonicScales.Major_Scale3[trumpetNote] + 1), 1.0f); //3
                                    var tNote = (diatonicScales.Major_Scale3[trumpetNote] + 1);
                                    var oldtnote = diatonicScales.Major_Scale3[trumpetNote];
                                    Debug.Log("minthird" + oldtnote);
                                    Debug.Log("domthird" + tNote);
                                }
                                else
                                {
                                    Piano.NoteOn(diatonicScales.Major_Scale3[trumpetNote], 1.0f);
                                }
                                prevTpt_Range = 3;
                            }
                        }
                        else if (KeyType == "NaturalMinor") // Locrian min7b5 3,5,1,11,7
                        {
                            Locrian_TNoteSelector();

                            if (prevTpt_Range == 0 || prevTpt_Range == 2) trumpetRange = Random.Range(1, 3);
                            if (prevTpt_Range == 1) trumpetRange = Random.Range(1, 2);
                            if (prevTpt_Range == 3) trumpetRange = Random.Range(2, 3);

                            if (trumpetRange == 1)
                            {
                                Piano.NoteOn(diatonicScales.NatMinor_Scale1[trumpetNote], 1.0f);
                                prevTpt_Range = 1;
                            }
                            else if (trumpetRange == 2)
                            {
                                Piano.NoteOn(diatonicScales.NatMinor_Scale2[trumpetNote], 1.0f);
                                prevTpt_Range = 2;
                            }
                            else if (trumpetRange == 3)
                            {
                                Piano.NoteOn(diatonicScales.NatMinor_Scale3[trumpetNote], 1.0f);
                                prevTpt_Range = 3;
                            }

                        }
                        break;

                    case 3: // CHORD III-7, IIImaj7, III7

                        chord3NoteShift();

                        if (KeyType == "Major") // Phrygian  
                        {
                            Phygian_TNoteSelector();

                            if (prevTpt_Range == 0 || prevTpt_Range == 2) trumpetRange = Random.Range(1, 3);
                            if (prevTpt_Range == 1) trumpetRange = Random.Range(1, 2);
                            if (prevTpt_Range == 3) trumpetRange = Random.Range(2, 3);

                            if (trumpetRange == 1)
                            {
                                Piano.NoteOn(diatonicScales.Major_Scale1[trumpetNote], 1.0f);
                                prevTpt_Range = 1;
                            }
                            else if (trumpetRange == 2)
                            {
                                Piano.NoteOn(diatonicScales.Major_Scale2[trumpetNote], 1.0f);
                                prevTpt_Range = 2;
                            }
                            else if (trumpetRange == 3)
                            {
                                Piano.NoteOn(diatonicScales.Major_Scale3[trumpetNote], 1.0f);
                                prevTpt_Range = 3;
                            }
                        }
                        else if (KeyType == "NaturalMinor") // Ionian Maj7(6)
                        {
                            MajDom_TNoteSelector();

                            if (prevTpt_Range == 0 || prevTpt_Range == 2) trumpetRange = Random.Range(1, 3);
                            if (prevTpt_Range == 1) trumpetRange = Random.Range(1, 2);
                            if (prevTpt_Range == 3) trumpetRange = Random.Range(2, 3);

                            if (trumpetRange == 1)
                            {
                                if (ChordType == "NonDiatonic" && trumpetNote == 2) //7
                                {
                                    Piano.NoteOn((diatonicScales.NatMinor_Scale1[trumpetNote] - 1), 1.0f); //b7
                                    var tNote = (diatonicScales.NatMinor_Scale1[trumpetNote] - 1);
                                    var oldtnote = diatonicScales.NatMinor_Scale1[trumpetNote];
                                    Debug.Log("majseven" + oldtnote);
                                    Debug.Log("domseven" + tNote);
                                }
                                else
                                {
                                    Piano.NoteOn(diatonicScales.NatMinor_Scale1[trumpetNote], 1.0f); //7
                                }
                                prevTpt_Range = 1;
                            }
                            else if (trumpetRange == 2)
                            {
                                if (ChordType == "NonDiatonic" && trumpetNote == 2) //7
                                {
                                    Piano.NoteOn((diatonicScales.NatMinor_Scale2[trumpetNote] - 1), 1.0f); //b7
                                    var tNote = (diatonicScales.NatMinor_Scale2[trumpetNote] - 1);
                                    var oldtnote = diatonicScales.NatMinor_Scale2[trumpetNote];
                                    Debug.Log("majseven" + oldtnote);
                                    Debug.Log("domseven" + tNote);
                                }
                                else
                                {
                                    Piano.NoteOn(diatonicScales.NatMinor_Scale2[trumpetNote], 1.0f); //7
                                }
                                prevTpt_Range = 2;
                            }
                            else if (trumpetRange == 3)
                            {
                                if (ChordType == "NonDiatonic" && trumpetNote == 2) //7
                                {
                                    Piano.NoteOn((diatonicScales.NatMinor_Scale3[trumpetNote] - 1), 1.0f); //b7
                                    var tNote = (diatonicScales.NatMinor_Scale3[trumpetNote] - 1);
                                    var oldtnote = diatonicScales.NatMinor_Scale3[trumpetNote];
                                    Debug.Log("majseven" + oldtnote);
                                    Debug.Log("domseven" + tNote);
                                }
                                else
                                {
                                    Piano.NoteOn(diatonicScales.NatMinor_Scale3[trumpetNote], 1.0f); //7
                                }
                                prevTpt_Range = 3;
                            }
                        }
                        break;

                    case 4: // CHORD IV 

                        chord4NoteShift();

                        if (KeyType == "Major") //Lydian 
                        {
                            Lydian_TNoteSelector();

                            if (prevTpt_Range == 0 || prevTpt_Range == 2) trumpetRange = Random.Range(1, 3);
                            if (prevTpt_Range == 1) trumpetRange = Random.Range(1, 2);
                            if (prevTpt_Range == 3) trumpetRange = Random.Range(2, 3);

                            if (trumpetRange == 1)
                            {
                                Piano.NoteOn(diatonicScales.Major_Scale1[trumpetNote], 1.0f);
                                prevTpt_Range = 1;
                            }
                            else if (trumpetRange == 2)
                            {
                                Piano.NoteOn(diatonicScales.Major_Scale2[trumpetNote], 1.0f);
                                prevTpt_Range = 2;
                            }
                            else if (trumpetRange == 3)
                            {
                                Piano.NoteOn(diatonicScales.Major_Scale3[trumpetNote], 1.0f);
                                prevTpt_Range = 3;
                            }

                        }
                        else if (KeyType == "NaturalMinor") //Dorian min7(9)
                        {
                            Min_TNoteSelector();

                            if (prevTpt_Range == 0 || prevTpt_Range == 2) trumpetRange = Random.Range(1, 3);
                            if (prevTpt_Range == 1) trumpetRange = Random.Range(1, 2);
                            if (prevTpt_Range == 3) trumpetRange = Random.Range(2, 3);

                            if (trumpetRange == 1)
                            {
                                Piano.NoteOn(diatonicScales.NatMinor_Scale1[trumpetNote], 1.0f);
                                prevTpt_Range = 1;
                            }
                            else if (trumpetRange == 2)
                            {
                                Piano.NoteOn(diatonicScales.NatMinor_Scale2[trumpetNote], 1.0f);
                                prevTpt_Range = 2;
                            }
                            else if (trumpetRange == 3)
                            {
                                Piano.NoteOn(diatonicScales.NatMinor_Scale3[trumpetNote], 1.0f);
                                prevTpt_Range = 3;
                            }
                        }
                        break;

                    case 5: // CHORD V7, V7alt(b5,b9), V-7

                        chord5NoteShift();

                        if (KeyType == "Major") // Mixolydian
                        {
                            MajDom_TNoteSelector();

                            if (prevTpt_Range == 0 || prevTpt_Range == 2) trumpetRange = Random.Range(1, 3);
                            if (prevTpt_Range == 1) trumpetRange = Random.Range(1, 2);
                            if (prevTpt_Range == 3) trumpetRange = Random.Range(2, 3);

                            if (trumpetRange == 1)
                            {
                                if (ChordType == "NonDiatonic" && trumpetNote == 2 || ChordType == "NonDiatonic" && trumpetNote == 6) //alt7 
                                {
                                    Piano.NoteOn((diatonicScales.Major_Scale1[trumpetNote] - 1), 1.0f); //b5, b9
                                    var tNote = (diatonicScales.Major_Scale1[trumpetNote] - 1);
                                    var oldtnote = diatonicScales.Major_Scale1[trumpetNote];
                                    Debug.Log("dom" + oldtnote);
                                    Debug.Log("alt" + tNote);
                                }
                                else
                                {
                                    Piano.NoteOn(diatonicScales.Major_Scale1[trumpetNote], 1.0f);
                                }
                                prevTpt_Range = 1;
                            }
                            else if (trumpetRange == 2)
                            {
                                if (ChordType == "NonDiatonic" && trumpetNote == 2 || ChordType == "NonDiatonic" && trumpetNote == 6) //alt7 
                                {
                                    Piano.NoteOn((diatonicScales.Major_Scale2[trumpetNote] - 1), 1.0f); //b5, b9
                                    var tNote = (diatonicScales.Major_Scale2[trumpetNote] - 1);
                                    var oldtnote = diatonicScales.Major_Scale2[trumpetNote];
                                    Debug.Log("dom" + oldtnote);
                                    Debug.Log("alt" + tNote);
                                }
                                else
                                {
                                    Piano.NoteOn(diatonicScales.Major_Scale2[trumpetNote], 1.0f);
                                }
                                prevTpt_Range = 2;
                            }
                            else if (trumpetRange == 3)
                            {
                                if (ChordType == "NonDiatonic" && trumpetNote == 2 || ChordType == "NonDiatonic" && trumpetNote == 6) //alt7 
                                {
                                    Piano.NoteOn((diatonicScales.Major_Scale3[trumpetNote] - 1), 1.0f); //b5, b9
                                    var tNote = (diatonicScales.Major_Scale3[trumpetNote] - 1);
                                    var oldtnote = diatonicScales.Major_Scale3[trumpetNote];
                                    Debug.Log("dom" + oldtnote);
                                    Debug.Log("alt" + tNote);
                                }
                                else
                                {
                                    Piano.NoteOn(diatonicScales.Major_Scale3[trumpetNote], 1.0f);
                                }
                                prevTpt_Range = 3;
                            }
                        }
                        else if (KeyType == "NaturalMinor") //Phrygian
                        {
                            Phygian_TNoteSelector();

                            if (prevTpt_Range == 0 || prevTpt_Range == 2) trumpetRange = Random.Range(1, 3);
                            if (prevTpt_Range == 1) trumpetRange = Random.Range(1, 2);
                            if (prevTpt_Range == 3) trumpetRange = Random.Range(2, 3);

                            if (trumpetRange == 1)
                            {
                                Piano.NoteOn(diatonicScales.NatMinor_Scale1[trumpetNote], 1.0f);
                                prevTpt_Range = 1;
                            }
                            else if (trumpetRange == 2)
                            {
                                Piano.NoteOn(diatonicScales.NatMinor_Scale2[trumpetNote], 1.0f);
                                prevTpt_Range = 2;
                            }
                            else if (trumpetRange == 3)
                            {
                                Piano.NoteOn(diatonicScales.NatMinor_Scale3[trumpetNote], 1.0f);
                                prevTpt_Range = 3;
                            }
                        }
                        break;

                    case 6: // CHORD VI-7, VImaj7, VI7  

                        chord6NoteShift();

                        if (KeyType == "Major") //Aeolian
                        {
                            Min_TNoteSelector();

                            if (prevTpt_Range == 0 || prevTpt_Range == 2) trumpetRange = Random.Range(1, 3);
                            if (prevTpt_Range == 1) trumpetRange = Random.Range(1, 2);
                            if (prevTpt_Range == 3) trumpetRange = Random.Range(2, 3);

                            if (trumpetRange == 1)
                            {
                                Piano.NoteOn(diatonicScales.Major_Scale1[trumpetNote], 1.0f);
                                prevTpt_Range = 1;
                            }
                            else if (trumpetRange == 2)
                            {
                                Piano.NoteOn(diatonicScales.Major_Scale2[trumpetNote], 1.0f);
                                prevTpt_Range = 2;
                            }
                            else if (trumpetRange == 3)
                            {
                                Piano.NoteOn(diatonicScales.Major_Scale3[trumpetNote], 1.0f);
                                prevTpt_Range = 3;
                            }

                        }
                        else if (KeyType == "NaturalMinor") //Lydian
                        {
                            if (ChordType == "NonDiatonic")
                            {
                                MajDom_TNoteSelector();
                            }
                            else
                            {
                                Lydian_TNoteSelector();
                            }

                            if (prevTpt_Range == 0 || prevTpt_Range == 2) trumpetRange = Random.Range(1, 3);
                            if (prevTpt_Range == 1) trumpetRange = Random.Range(1, 2);
                            if (prevTpt_Range == 3) trumpetRange = Random.Range(2, 3);

                            if (trumpetRange == 1)
                            {
                                if (ChordType == "NonDiatonic" && trumpetNote == 5) //7
                                {
                                    Piano.NoteOn((diatonicScales.NatMinor_Scale1[trumpetNote] - 1), 1.0f); //b7
                                    var tNote = (diatonicScales.NatMinor_Scale1[trumpetNote] - 1);
                                    var oldtnote = diatonicScales.NatMinor_Scale1[trumpetNote];
                                    Debug.Log("majseven" + oldtnote);
                                    Debug.Log("domseven" + tNote);
                                }
                                else
                                {
                                    Piano.NoteOn(diatonicScales.NatMinor_Scale1[trumpetNote], 1.0f); //7
                                }
                                prevTpt_Range = 1;
                            }
                            else if (trumpetRange == 2)
                            {
                                if (ChordType == "NonDiatonic" && trumpetNote == 5) //7
                                {
                                    Piano.NoteOn((diatonicScales.NatMinor_Scale2[trumpetNote] - 1), 1.0f); //b7
                                    var tNote = (diatonicScales.NatMinor_Scale2[trumpetNote] - 1);
                                    var oldtnote = diatonicScales.NatMinor_Scale2[trumpetNote];
                                    Debug.Log("majseven" + oldtnote);
                                    Debug.Log("domseven" + tNote);
                                }
                                else
                                {
                                    Piano.NoteOn(diatonicScales.NatMinor_Scale2[trumpetNote], 1.0f); //7
                                }
                                prevTpt_Range = 2;
                            }
                            else if (trumpetRange == 3)
                            {
                                if (ChordType == "NonDiatonic" && trumpetNote == 5) //7
                                {
                                    Piano.NoteOn((diatonicScales.NatMinor_Scale3[trumpetNote] - 1), 1.0f); //b7
                                    var tNote = (diatonicScales.NatMinor_Scale3[trumpetNote] - 1);
                                    var oldtnote = diatonicScales.NatMinor_Scale3[trumpetNote];
                                    Debug.Log("majseven" + oldtnote);
                                    Debug.Log("domseven" + tNote);
                                }
                                else
                                {
                                    Piano.NoteOn(diatonicScales.NatMinor_Scale3[trumpetNote], 1.0f); //7
                                }
                                prevTpt_Range = 3;
                            }
                        }
                        break;

                    case 7: // CHORD VII0, VIIo, VII-7, VII7

                        chord7NoteShift();

                        if (KeyType == "Major") //Locrian //min7b5
                        {
                            Locrian_TNoteSelector();

                            if (prevTpt_Range == 0 || prevTpt_Range == 2) trumpetRange = Random.Range(1, 3);
                            if (prevTpt_Range == 1) trumpetRange = Random.Range(1, 2);
                            if (prevTpt_Range == 3) trumpetRange = Random.Range(2, 3);

                            if (trumpetRange == 1)
                            {
                                if (ChordType == "NonDiatonic" && trumpetNote == 6) //VIIO
                                {
                                    Piano.NoteOn((diatonicScales.Major_Scale1[trumpetNote] - 1), 1.0f); //bb7
                                    var tNote = (diatonicScales.Major_Scale1[trumpetNote] - 1);
                                    var oldtnote = diatonicScales.Major_Scale1[trumpetNote];
                                    Debug.Log("dom" + oldtnote);
                                    Debug.Log("bbseven" + tNote);
                                }
                                else
                                {
                                    Piano.NoteOn(diatonicScales.Major_Scale1[trumpetNote], 1.0f);
                                }
                                prevTpt_Range = 1;
                            }
                            else if (trumpetRange == 2)
                            {
                                if (ChordType == "NonDiatonic" && trumpetNote == 6)
                                {
                                    Piano.NoteOn((diatonicScales.Major_Scale2[trumpetNote] - 1), 1.0f);
                                    var tNote = (diatonicScales.Major_Scale2[trumpetNote] - 1);
                                    var oldtnote = diatonicScales.Major_Scale2[trumpetNote];
                                    Debug.Log("dom" + oldtnote);
                                    Debug.Log("bbseven" + tNote);
                                }
                                else
                                {
                                    Piano.NoteOn(diatonicScales.Major_Scale2[trumpetNote], 1.0f);
                                }
                                prevTpt_Range = 2;
                            }
                            else if (trumpetRange == 3)
                            {
                                if (ChordType == "NonDiatonic" && trumpetNote == 6)
                                {
                                    Piano.NoteOn((diatonicScales.Major_Scale3[trumpetNote] - 1), 1.0f);
                                    var tNote = (diatonicScales.Major_Scale3[trumpetNote] - 1);
                                    var oldtnote = diatonicScales.Major_Scale3[trumpetNote];
                                    Debug.Log("dom" + oldtnote);
                                    Debug.Log("bbseven" + tNote);
                                }
                                else
                                {
                                    Piano.NoteOn(diatonicScales.Major_Scale3[trumpetNote], 1.0f);
                                }
                                prevTpt_Range = 3;
                            }
                        }
                        else if (KeyType == "NaturalMinor") //Mixolydian//dom7 
                        {
                            if (ChordType == "NonDiatonic")
                            {
                                Min_TNoteSelector();
                            }
                            else
                            {
                                MajDom_TNoteSelector();
                            }

                            if (prevTpt_Range == 0 || prevTpt_Range == 2) trumpetRange = Random.Range(1, 3);
                            if (prevTpt_Range == 1) trumpetRange = Random.Range(1, 2);
                            if (prevTpt_Range == 3) trumpetRange = Random.Range(2, 3);

                            if (trumpetRange == 1)
                            {
                                if (ChordType == "NonDiatonic" && trumpetNote == 2) //min7
                                {
                                    Piano.NoteOn((diatonicScales.NatMinor_Scale1[trumpetNote] - 1), 1.0f); //b3
                                    var tNote = (diatonicScales.NatMinor_Scale1[trumpetNote] - 1);
                                    var oldtnote = diatonicScales.NatMinor_Scale1[trumpetNote];
                                    Debug.Log("majthird" + oldtnote);
                                    Debug.Log("minthird" + tNote);
                                }
                                else
                                {
                                    Piano.NoteOn(diatonicScales.NatMinor_Scale1[trumpetNote], 1.0f);
                                }
                                prevTpt_Range = 1;
                            }
                            else if (trumpetRange == 2)
                            {
                                if (ChordType == "NonDiatonic" && trumpetNote == 2) //min7
                                {
                                    Piano.NoteOn((diatonicScales.NatMinor_Scale2[trumpetNote] - 1), 1.0f); //b3
                                    var tNote = (diatonicScales.NatMinor_Scale2[trumpetNote] - 1);
                                    var oldtnote = diatonicScales.NatMinor_Scale2[trumpetNote];
                                    Debug.Log("majthird" + oldtnote);
                                    Debug.Log("minthird" + tNote);
                                }
                                else
                                {
                                    Piano.NoteOn(diatonicScales.NatMinor_Scale2[trumpetNote], 1.0f);
                                }
                                prevTpt_Range = 2;
                            }
                            else if (trumpetRange == 3)
                            {
                                if (ChordType == "NonDiatonic" && trumpetNote == 2) //min7
                                {
                                    Piano.NoteOn((diatonicScales.NatMinor_Scale3[trumpetNote] - 1), 1.0f); //b3
                                    var tNote = (diatonicScales.NatMinor_Scale3[trumpetNote] - 1);
                                    var oldtnote = diatonicScales.NatMinor_Scale3[trumpetNote];
                                    Debug.Log("majthird" + oldtnote);
                                    Debug.Log("minthird" + tNote);
                                }
                                else
                                {
                                    Piano.NoteOn(diatonicScales.NatMinor_Scale3[trumpetNote], 1.0f);
                                }
                                prevTpt_Range = 3;
                            }
                        }
                        break;
                }
            }
            else
            {
                Piano.AllNotesOff();
            }
        }
    }
}
