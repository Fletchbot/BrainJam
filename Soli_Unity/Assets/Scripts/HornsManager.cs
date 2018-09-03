using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SoliGameController;

namespace SoliSoundScape
{
    public class HornsManager : MonoBehaviour
    {
        ChordProgressions cp;
        DiatonicScales diatonicScales;
        public AudioHelm.Sampler Trumpet;
        public AudioHelm.Sampler Saxophone;
        public int currChord, trumpetNote, saxNote, trumpetRange, saxophoneRange, prevTpt_Range, prevSax_Range;
        public bool isFocus, tpt_f_sw, sax_f_sw;

        private int[] MajDomnoteSelector = new int[6];
        private int[] MinnoteSelector = new int[6];
        private int[] LydiannoteSelector = new int[7];
        private int[] PhygiannoteSelector = new int[5];
        private int[] LocriannoteSelector = new int[6];
        // Use this for initialization
        void Start()
        {
            cp = this.GetComponent<ChordProgressions>();
            diatonicScales = this.GetComponent<DiatonicScales>();
            chord1NoteShift();
        }

        // Update is called once per frame
        void Update()
        {            
            isFocus = cp.gesture_c.isFocus;
            currChord = cp.currChord;

            TrumpetEnable();
            SaxophoneEnable();
        }

        public void TrumpetEnable()
        {
         //   if (isFocus)
        //    {
                if (Input.GetButton("Vertical"))
            {
                switch (currChord)
                {
                    case 1: //CHORD I 

                        if (cp.KeyType == "Major") //Ionian 
                        {
                            if (!tpt_f_sw)
                            {
                                chord1NoteShift();
                                MajDom_TNoteSelector();
                                tptRange();

                                if (trumpetRange == 1)
                                {
                                    Trumpet.NoteOn(diatonicScales.Major_Scale1[trumpetNote], 1.0f);
                                    prevTpt_Range = 1;
                                }
                                else if (trumpetRange == 2)
                                {
                                    Trumpet.NoteOn(diatonicScales.Major_Scale2[trumpetNote], 1.0f);
                                    prevTpt_Range = 2;
                                }
                                else if (trumpetRange == 3)
                                {
                                    Trumpet.NoteOn(diatonicScales.Major_Scale3[trumpetNote], 1.0f);
                                    prevTpt_Range = 3;
                                }
                                else if (trumpetRange == 4)
                                {
                                    Trumpet.NoteOn(diatonicScales.Major_Scale4[trumpetNote], 1.0f);
                                    prevTpt_Range = 4;
                                }
                                tpt_f_sw = true;
                            }
                        }
                        else if (cp.KeyType == "NaturalMinor") // Aeolian min7(9)
                        {
                            if (!tpt_f_sw)
                            {
                                Min_TNoteSelector();
                                tptRange();

                                if (trumpetRange == 1)
                                {
                                    Trumpet.NoteOn(diatonicScales.NatMinor_Scale1[trumpetNote], 1.0f);
                                    prevTpt_Range = 1;
                                }
                                else if (trumpetRange == 2)
                                {
                                    Trumpet.NoteOn(diatonicScales.NatMinor_Scale2[trumpetNote], 1.0f);
                                    prevTpt_Range = 2;
                                }
                                else if (trumpetRange == 3)
                                {
                                    Trumpet.NoteOn(diatonicScales.NatMinor_Scale3[trumpetNote], 1.0f);
                                    prevTpt_Range = 3;
                                }
                                else if (trumpetRange == 4)
                                {
                                    Trumpet.NoteOn(diatonicScales.NatMinor_Scale4[trumpetNote], 1.0f);
                                    prevTpt_Range = 4;
                                }
                                tpt_f_sw = true;
                            }
                        }
                        break;

                    case 2: //CHORD IImin7, II7, 

                        if (cp.KeyType == "Major") //Dorian min7(9)
                        {
                            if (!tpt_f_sw)
                            {
                                chord2NoteShift();
                                if (cp.ChordType == "NonDiatonic")
                                {
                                    MajDom_TNoteSelector();
                                }
                                else
                                {
                                    Min_TNoteSelector();
                                }
                                tptRange();

                                if (trumpetRange == 1)
                                {
                                    if (cp.ChordType == "NonDiatonic" && trumpetNote == 4) //II7
                                    {
                                        Trumpet.NoteOn((diatonicScales.Major_Scale1[trumpetNote] + 1), 1.0f); //3
                                        var tNote = (diatonicScales.Major_Scale1[trumpetNote] + 1);
                                        var oldtnote = diatonicScales.Major_Scale1[trumpetNote];
                                        Debug.Log("minthird" + oldtnote);
                                        Debug.Log("domthird" + tNote);
                                    }
                                    else
                                    {
                                        Trumpet.NoteOn(diatonicScales.Major_Scale1[trumpetNote], 1.0f);
                                    }
                                    prevTpt_Range = 1;
                                }
                                else if (trumpetRange == 2)
                                {
                                    if (cp.ChordType == "NonDiatonic" && trumpetNote == 4) //II7
                                    {
                                        Trumpet.NoteOn((diatonicScales.Major_Scale2[trumpetNote] + 1), 1.0f); //3
                                        var tNote = (diatonicScales.Major_Scale2[trumpetNote] + 1);
                                        var oldtnote = diatonicScales.Major_Scale2[trumpetNote];
                                        Debug.Log("minthird" + oldtnote);
                                        Debug.Log("domthird" + tNote);
                                    }
                                    else
                                    {
                                        Trumpet.NoteOn(diatonicScales.Major_Scale2[trumpetNote], 1.0f);
                                    }
                                    prevTpt_Range = 2;
                                }
                                else if (trumpetRange == 3)
                                {
                                    if (cp.ChordType == "NonDiatonic" && trumpetNote == 4) //II7
                                    {
                                        Trumpet.NoteOn((diatonicScales.Major_Scale3[trumpetNote] + 1), 1.0f); //3
                                        var tNote = (diatonicScales.Major_Scale3[trumpetNote] + 1);
                                        var oldtnote = diatonicScales.Major_Scale3[trumpetNote];
                                        Debug.Log("minthird" + oldtnote);
                                        Debug.Log("domthird" + tNote);
                                    }
                                    else
                                    {
                                        Trumpet.NoteOn(diatonicScales.Major_Scale3[trumpetNote], 1.0f);
                                    }
                                    prevTpt_Range = 3;
                                }
                                tpt_f_sw = true;
                            }
                        }
                        else if (cp.KeyType == "NaturalMinor") // Locrian min7b5 3,5,1,11,7
                        {
                            if (!tpt_f_sw)
                            {
                                chord2NoteShift();
                                Locrian_TNoteSelector();
                                tptRange();

                                if (trumpetRange == 1)
                                {
                                    Trumpet.NoteOn(diatonicScales.NatMinor_Scale1[trumpetNote], 1.0f);
                                    prevTpt_Range = 1;
                                }
                                else if (trumpetRange == 2)
                                {
                                    Trumpet.NoteOn(diatonicScales.NatMinor_Scale2[trumpetNote], 1.0f);
                                    prevTpt_Range = 2;
                                }
                                else if (trumpetRange == 3)
                                {
                                    Trumpet.NoteOn(diatonicScales.NatMinor_Scale3[trumpetNote], 1.0f);
                                    prevTpt_Range = 3;
                                }
                                tpt_f_sw = true;
                            }
                        }
                        break;

                    case 3: // CHORD III-7, IIImaj7, III7
                        if (cp.KeyType == "Major") // Phrygian  
                        {
                            if (!tpt_f_sw)
                            {
                                chord3NoteShift();
                                Phygian_TNoteSelector();
                                tptRange();

                                if (trumpetRange == 1)
                                {
                                    Trumpet.NoteOn(diatonicScales.Major_Scale1[trumpetNote], 1.0f);
                                    prevTpt_Range = 1;
                                }
                                else if (trumpetRange == 2)
                                {
                                    Trumpet.NoteOn(diatonicScales.Major_Scale2[trumpetNote], 1.0f);
                                    prevTpt_Range = 2;
                                }
                                else if (trumpetRange == 3)
                                {
                                    Trumpet.NoteOn(diatonicScales.Major_Scale3[trumpetNote], 1.0f);
                                    prevTpt_Range = 3;
                                }
                                tpt_f_sw = true;
                            }
                        }
                        else if (cp.KeyType == "NaturalMinor") // Ionian Maj7(6)
                        {
                            if (!tpt_f_sw)
                            {
                                chord3NoteShift();
                                MajDom_TNoteSelector();
                                tptRange();

                                if (trumpetRange == 1)
                                {
                                    if (cp.ChordType == "NonDiatonic" && trumpetNote == 2) //7
                                    {
                                        Trumpet.NoteOn((diatonicScales.NatMinor_Scale1[trumpetNote] - 1), 1.0f); //b7
                                        var tNote = (diatonicScales.NatMinor_Scale1[trumpetNote] - 1);
                                        var oldtnote = diatonicScales.NatMinor_Scale1[trumpetNote];
                                        Debug.Log("majseven" + oldtnote);
                                        Debug.Log("domseven" + tNote);
                                    }
                                    else
                                    {
                                        Trumpet.NoteOn(diatonicScales.NatMinor_Scale1[trumpetNote], 1.0f); //7
                                    }
                                    prevTpt_Range = 1;
                                }
                                else if (trumpetRange == 2)
                                {
                                    if (cp.ChordType == "NonDiatonic" && trumpetNote == 2) //7
                                    {
                                        Trumpet.NoteOn((diatonicScales.NatMinor_Scale2[trumpetNote] - 1), 1.0f); //b7
                                        var tNote = (diatonicScales.NatMinor_Scale2[trumpetNote] - 1);
                                        var oldtnote = diatonicScales.NatMinor_Scale2[trumpetNote];
                                        Debug.Log("majseven" + oldtnote);
                                        Debug.Log("domseven" + tNote);
                                    }
                                    else
                                    {
                                        Trumpet.NoteOn(diatonicScales.NatMinor_Scale2[trumpetNote], 1.0f); //7
                                    }
                                    prevTpt_Range = 2;
                                }
                                else if (trumpetRange == 3)
                                {
                                    if (cp.ChordType == "NonDiatonic" && trumpetNote == 2) //7
                                    {
                                        Trumpet.NoteOn((diatonicScales.NatMinor_Scale3[trumpetNote] - 1), 1.0f); //b7
                                        var tNote = (diatonicScales.NatMinor_Scale3[trumpetNote] - 1);
                                        var oldtnote = diatonicScales.NatMinor_Scale3[trumpetNote];
                                        Debug.Log("majseven" + oldtnote);
                                        Debug.Log("domseven" + tNote);
                                    }
                                    else
                                    {
                                        Trumpet.NoteOn(diatonicScales.NatMinor_Scale3[trumpetNote], 1.0f); //7
                                    }
                                    prevTpt_Range = 3;
                                }
                                tpt_f_sw = true;
                            }
                        }
                        break;

                    case 4: // CHORD IV 
                        if (cp.KeyType == "Major") //Lydian 
                        {
                            if (!tpt_f_sw)
                            {
                                chord4NoteShift();
                                Lydian_TNoteSelector();
                                tptRange();

                                if (trumpetRange == 1)
                                {
                                    Trumpet.NoteOn(diatonicScales.Major_Scale1[trumpetNote], 1.0f);
                                    prevTpt_Range = 1;
                                }
                                else if (trumpetRange == 2)
                                {
                                    Trumpet.NoteOn(diatonicScales.Major_Scale2[trumpetNote], 1.0f);
                                    prevTpt_Range = 2;
                                }
                                else if (trumpetRange == 3)
                                {
                                    Trumpet.NoteOn(diatonicScales.Major_Scale3[trumpetNote], 1.0f);
                                    prevTpt_Range = 3;
                                }
                                tpt_f_sw = true;
                            }
                        }
                        else if (cp.KeyType == "NaturalMinor") //Dorian min7(9)
                        {
                            if (!tpt_f_sw)
                            {
                                chord4NoteShift();
                                Min_TNoteSelector();
                                tptRange();

                                if (trumpetRange == 1)
                                {
                                    Trumpet.NoteOn(diatonicScales.NatMinor_Scale1[trumpetNote], 1.0f);
                                    prevTpt_Range = 1;
                                }
                                else if (trumpetRange == 2)
                                {
                                    Trumpet.NoteOn(diatonicScales.NatMinor_Scale2[trumpetNote], 1.0f);
                                    prevTpt_Range = 2;
                                }
                                else if (trumpetRange == 3)
                                {
                                    Trumpet.NoteOn(diatonicScales.NatMinor_Scale3[trumpetNote], 1.0f);
                                    prevTpt_Range = 3;
                                }
                                tpt_f_sw = true;
                            }
                        }
                        break;

                    case 5: // CHORD V7, V7alt(b5,b9), V-7
                        if (cp.KeyType == "Major") // Mixolydian
                        {
                            if (!tpt_f_sw)
                            {
                                chord5NoteShift();
                                MajDom_TNoteSelector();
                                tptRange();

                                if (trumpetRange == 1)
                                {
                                    if (cp.ChordType == "NonDiatonic" && trumpetNote == 2 || cp.ChordType == "NonDiatonic" && trumpetNote == 6) //alt7 
                                    {
                                        Trumpet.NoteOn((diatonicScales.Major_Scale1[trumpetNote] - 1), 1.0f); //b5, b9
                                        var tNote = (diatonicScales.Major_Scale1[trumpetNote] - 1);
                                        var oldtnote = diatonicScales.Major_Scale1[trumpetNote];
                                        Debug.Log("dom" + oldtnote);
                                        Debug.Log("alt" + tNote);
                                    }
                                    else
                                    {
                                        Trumpet.NoteOn(diatonicScales.Major_Scale1[trumpetNote], 1.0f);
                                    }
                                    prevTpt_Range = 1;
                                }
                                else if (trumpetRange == 2)
                                {
                                    if (cp.ChordType == "NonDiatonic" && trumpetNote == 2 || cp.ChordType == "NonDiatonic" && trumpetNote == 6) //alt7 
                                    {
                                        Trumpet.NoteOn((diatonicScales.Major_Scale2[trumpetNote] - 1), 1.0f); //b5, b9
                                        var tNote = (diatonicScales.Major_Scale2[trumpetNote] - 1);
                                        var oldtnote = diatonicScales.Major_Scale2[trumpetNote];
                                        Debug.Log("dom" + oldtnote);
                                        Debug.Log("alt" + tNote);
                                    }
                                    else
                                    {
                                        Trumpet.NoteOn(diatonicScales.Major_Scale2[trumpetNote], 1.0f);
                                    }
                                    prevTpt_Range = 2;
                                }
                                else if (trumpetRange == 3)
                                {
                                    if (cp.ChordType == "NonDiatonic" && trumpetNote == 2 || cp.ChordType == "NonDiatonic" && trumpetNote == 6) //alt7 
                                    {
                                        Trumpet.NoteOn((diatonicScales.Major_Scale3[trumpetNote] - 1), 1.0f); //b5, b9
                                        var tNote = (diatonicScales.Major_Scale3[trumpetNote] - 1);
                                        var oldtnote = diatonicScales.Major_Scale3[trumpetNote];
                                        Debug.Log("dom" + oldtnote);
                                        Debug.Log("alt" + tNote);
                                    }
                                    else
                                    {
                                        Trumpet.NoteOn(diatonicScales.Major_Scale3[trumpetNote], 1.0f);
                                    }
                                    prevTpt_Range = 3;
                                }
                                tpt_f_sw = true;
                            }
                        }
                        else if (cp.KeyType == "NaturalMinor") //Phrygian
                        {
                            if (!tpt_f_sw)
                            {
                                chord5NoteShift();
                                Phygian_TNoteSelector();
                                tptRange();

                                if (trumpetRange == 1)
                                {
                                    Trumpet.NoteOn(diatonicScales.NatMinor_Scale1[trumpetNote], 1.0f);
                                    prevTpt_Range = 1;
                                }
                                else if (trumpetRange == 2)
                                {
                                    Trumpet.NoteOn(diatonicScales.NatMinor_Scale2[trumpetNote], 1.0f);
                                    prevTpt_Range = 2;
                                }
                                else if (trumpetRange == 3)
                                {
                                    Trumpet.NoteOn(diatonicScales.NatMinor_Scale3[trumpetNote], 1.0f);
                                    prevTpt_Range = 3;
                                }
                                tpt_f_sw = true;
                            }

                        }
                        break;

                    case 6: // CHORD VI-7, VImaj7, VI7  
                        if (cp.KeyType == "Major") //Aeolian
                        {
                            if (!tpt_f_sw)
                            {
                                chord6NoteShift();
                                Min_TNoteSelector();
                                tptRange();

                                if (trumpetRange == 1)
                                {
                                    Trumpet.NoteOn(diatonicScales.Major_Scale1[trumpetNote], 1.0f);
                                    prevTpt_Range = 1;
                                }
                                else if (trumpetRange == 2)
                                {
                                    Trumpet.NoteOn(diatonicScales.Major_Scale2[trumpetNote], 1.0f);
                                    prevTpt_Range = 2;
                                }
                                else if (trumpetRange == 3)
                                {
                                    Trumpet.NoteOn(diatonicScales.Major_Scale3[trumpetNote], 1.0f);
                                    prevTpt_Range = 3;
                                }
                                tpt_f_sw = true;
                            }
                        }
                        else if (cp.KeyType == "NaturalMinor") //Lydian
                        {
                            if (!tpt_f_sw)
                            {
                                chord6NoteShift();
                                if (cp.ChordType == "NonDiatonic")
                                {
                                    MajDom_TNoteSelector();
                                }
                                else
                                {
                                    Lydian_TNoteSelector();
                                }
                                tptRange();

                                if (trumpetRange == 1)
                                {
                                    if (cp.ChordType == "NonDiatonic" && trumpetNote == 5) //7
                                    {
                                        Trumpet.NoteOn((diatonicScales.NatMinor_Scale1[trumpetNote] - 1), 1.0f); //b7
                                        var tNote = (diatonicScales.NatMinor_Scale1[trumpetNote] - 1);
                                        var oldtnote = diatonicScales.NatMinor_Scale1[trumpetNote];
                                        Debug.Log("majseven" + oldtnote);
                                        Debug.Log("domseven" + tNote);
                                    }
                                    else
                                    {
                                        Trumpet.NoteOn(diatonicScales.NatMinor_Scale1[trumpetNote], 1.0f); //7
                                    }
                                    prevTpt_Range = 1;
                                }
                                else if (trumpetRange == 2)
                                {
                                    if (cp.ChordType == "NonDiatonic" && trumpetNote == 5) //7
                                    {
                                        Trumpet.NoteOn((diatonicScales.NatMinor_Scale2[trumpetNote] - 1), 1.0f); //b7
                                        var tNote = (diatonicScales.NatMinor_Scale2[trumpetNote] - 1);
                                        var oldtnote = diatonicScales.NatMinor_Scale2[trumpetNote];
                                        Debug.Log("majseven" + oldtnote);
                                        Debug.Log("domseven" + tNote);
                                    }
                                    else
                                    {
                                        Trumpet.NoteOn(diatonicScales.NatMinor_Scale2[trumpetNote], 1.0f); //7
                                    }
                                    prevTpt_Range = 2;
                                }
                                else if (trumpetRange == 3)
                                {
                                    if (cp.ChordType == "NonDiatonic" && trumpetNote == 5) //7
                                    {
                                        Trumpet.NoteOn((diatonicScales.NatMinor_Scale3[trumpetNote] - 1), 1.0f); //b7
                                        var tNote = (diatonicScales.NatMinor_Scale3[trumpetNote] - 1);
                                        var oldtnote = diatonicScales.NatMinor_Scale3[trumpetNote];
                                        Debug.Log("majseven" + oldtnote);
                                        Debug.Log("domseven" + tNote);
                                    }
                                    else
                                    {
                                        Trumpet.NoteOn(diatonicScales.NatMinor_Scale3[trumpetNote], 1.0f); //7
                                    }
                                    prevTpt_Range = 3;
                                }
                                tpt_f_sw = true;
                            }
                        }
                        break;

                    case 7: // CHORD VII0, VIIo, VII-7, VII7
                        if (cp.KeyType == "Major") //Locrian //min7b5
                        {
                            if (!tpt_f_sw)
                            {
                                chord7NoteShift();
                                Locrian_TNoteSelector();
                                tptRange();

                                if (trumpetRange == 1)
                                {
                                    if (cp.ChordType == "NonDiatonic" && trumpetNote == 6) //VIIO
                                    {
                                        Trumpet.NoteOn((diatonicScales.Major_Scale1[trumpetNote] - 1), 1.0f); //bb7
                                        var tNote = (diatonicScales.Major_Scale1[trumpetNote] - 1);
                                        var oldtnote = diatonicScales.Major_Scale1[trumpetNote];
                                        Debug.Log("dom" + oldtnote);
                                        Debug.Log("bbseven" + tNote);
                                    }
                                    else
                                    {
                                        Trumpet.NoteOn(diatonicScales.Major_Scale1[trumpetNote], 1.0f);
                                    }
                                    prevTpt_Range = 1;
                                }
                                else if (trumpetRange == 2)
                                {
                                    if (cp.ChordType == "NonDiatonic" && trumpetNote == 6)
                                    {
                                        Trumpet.NoteOn((diatonicScales.Major_Scale2[trumpetNote] - 1), 1.0f);
                                        var tNote = (diatonicScales.Major_Scale2[trumpetNote] - 1);
                                        var oldtnote = diatonicScales.Major_Scale2[trumpetNote];
                                        Debug.Log("dom" + oldtnote);
                                        Debug.Log("bbseven" + tNote);
                                    }
                                    else
                                    {
                                        Trumpet.NoteOn(diatonicScales.Major_Scale2[trumpetNote], 1.0f);
                                    }
                                    prevTpt_Range = 2;
                                }
                                else if (trumpetRange == 3)
                                {
                                    if (cp.ChordType == "NonDiatonic" && trumpetNote == 6)
                                    {
                                        Trumpet.NoteOn((diatonicScales.Major_Scale3[trumpetNote] - 1), 1.0f);
                                        var tNote = (diatonicScales.Major_Scale3[trumpetNote] - 1);
                                        var oldtnote = diatonicScales.Major_Scale3[trumpetNote];
                                        Debug.Log("dom" + oldtnote);
                                        Debug.Log("bbseven" + tNote);
                                    }
                                    else
                                    {
                                        Trumpet.NoteOn(diatonicScales.Major_Scale3[trumpetNote], 1.0f);
                                    }
                                    prevTpt_Range = 3;
                                }
                                tpt_f_sw = true;
                            }
                        }
                        else if (cp.KeyType == "NaturalMinor") //Mixolydian//dom7 
                        {
                            if (!tpt_f_sw)
                            {
                                chord7NoteShift();
                                if (cp.ChordType == "NonDiatonic")
                                {
                                    Min_TNoteSelector();
                                }
                                else
                                {
                                    MajDom_TNoteSelector();
                                }
                                tptRange();

                                if (trumpetRange == 1)
                                {
                                    if (cp.ChordType == "NonDiatonic" && trumpetNote == 2) //min7
                                    {
                                        Trumpet.NoteOn((diatonicScales.NatMinor_Scale1[trumpetNote] - 1), 1.0f); //b3
                                        var tNote = (diatonicScales.NatMinor_Scale1[trumpetNote] - 1);
                                        var oldtnote = diatonicScales.NatMinor_Scale1[trumpetNote];
                                        Debug.Log("majthird" + oldtnote);
                                        Debug.Log("minthird" + tNote);
                                    }
                                    else
                                    {
                                        Trumpet.NoteOn(diatonicScales.NatMinor_Scale1[trumpetNote], 1.0f);
                                    }
                                    prevTpt_Range = 1;
                                }
                                else if (trumpetRange == 2)
                                {
                                    if (cp.ChordType == "NonDiatonic" && trumpetNote == 2) //min7
                                    {
                                        Trumpet.NoteOn((diatonicScales.NatMinor_Scale2[trumpetNote] - 1), 1.0f); //b3
                                        var tNote = (diatonicScales.NatMinor_Scale2[trumpetNote] - 1);
                                        var oldtnote = diatonicScales.NatMinor_Scale2[trumpetNote];
                                        Debug.Log("majthird" + oldtnote);
                                        Debug.Log("minthird" + tNote);
                                    }
                                    else
                                    {
                                        Trumpet.NoteOn(diatonicScales.NatMinor_Scale2[trumpetNote], 1.0f);
                                    }
                                    prevTpt_Range = 2;
                                }
                                else if (trumpetRange == 3)
                                {
                                    if (cp.ChordType == "NonDiatonic" && trumpetNote == 2) //min7
                                    {
                                        Trumpet.NoteOn((diatonicScales.NatMinor_Scale3[trumpetNote] - 1), 1.0f); //b3
                                        var tNote = (diatonicScales.NatMinor_Scale3[trumpetNote] - 1);
                                        var oldtnote = diatonicScales.NatMinor_Scale3[trumpetNote];
                                        Debug.Log("majthird" + oldtnote);
                                        Debug.Log("minthird" + tNote);
                                    }
                                    else
                                    {
                                        Trumpet.NoteOn(diatonicScales.NatMinor_Scale3[trumpetNote], 1.0f);
                                    }
                                    prevTpt_Range = 3;
                                }
                                tpt_f_sw = true;
                            }
                        }
                        break;
                }
            }
            else
            {
                tpt_f_sw = false;
                Trumpet.AllNotesOff();
            }
        }

        public void SaxophoneEnable()
        {
       //     if (isFocus)
       //     {
                if (Input.GetButton("Vertical"))
            {
                switch (currChord)
                {
                    case 1: //CHORD I 

                        if (cp.KeyType == "Major") //Ionian 
                        {
                            if (!sax_f_sw)
                            {
                                chord1NoteShift();
                                MajDom_SNoteSelector();
                                saxRange();

                                if (saxophoneRange == 1)
                                {
                                    Saxophone.NoteOn(diatonicScales.Major_Scale1[saxNote], 1.0f);
                                    prevSax_Range = 1;
                                }
                                else if (saxophoneRange == 2)
                                {
                                    Saxophone.NoteOn(diatonicScales.Major_Scale2[saxNote], 1.0f);
                                    prevSax_Range = 2;
                                }
                                else if (saxophoneRange == 3)
                                {
                                    Saxophone.NoteOn(diatonicScales.Major_Scale3[saxNote], 1.0f);
                                    prevSax_Range = 3;
                                }
                                sax_f_sw = true;
                            }
                        }
                        else if (cp.KeyType == "NaturalMinor") // Aeolian min7(9)
                        {
                            if (!sax_f_sw)
                            {
                                Min_SNoteSelector();
                                saxRange();

                                if (saxophoneRange == 1)
                                {
                                    Saxophone.NoteOn(diatonicScales.NatMinor_Scale1[saxNote], 1.0f);
                                    prevSax_Range = 1;
                                }
                                else if (saxophoneRange == 2)
                                {
                                    Saxophone.NoteOn(diatonicScales.NatMinor_Scale2[saxNote], 1.0f);
                                    prevSax_Range = 2;
                                }
                                else if (saxophoneRange == 3)
                                {
                                    Saxophone.NoteOn(diatonicScales.NatMinor_Scale3[saxNote], 1.0f);
                                    prevSax_Range = 3;
                                }
                                sax_f_sw = true;
                            }
                        }
                        break;

                    case 2: //CHORD IImin7, II7, 

                        if (cp.KeyType == "Major") //Dorian min7(9)
                        {
                            if (!sax_f_sw)
                            {
                                chord2NoteShift();
                                if (cp.ChordType == "NonDiatonic")
                                {
                                    MajDom_SNoteSelector();
                                }
                                else
                                {
                                    Min_SNoteSelector();
                                }
                                saxRange();

                                if (saxophoneRange == 1)
                                {
                                    if (cp.ChordType == "NonDiatonic" && saxNote == 4) //II7
                                    {
                                        Saxophone.NoteOn((diatonicScales.Major_Scale1[saxNote] + 1), 1.0f); //3
                                        var sNote = (diatonicScales.Major_Scale1[saxNote] + 1);
                                        var oldsNote = diatonicScales.Major_Scale1[saxNote];
                                        Debug.Log("minthird" + oldsNote);
                                        Debug.Log("domthird" + sNote);
                                    }
                                    else
                                    {
                                        Saxophone.NoteOn(diatonicScales.Major_Scale1[saxNote], 1.0f);
                                    }
                                    prevSax_Range = 1;
                                }
                                else if (saxophoneRange == 2)
                                {
                                    if (cp.ChordType == "NonDiatonic" && saxNote == 4) //II7
                                    {
                                        Saxophone.NoteOn((diatonicScales.Major_Scale2[saxNote] + 1), 1.0f); //3
                                        var sNote = (diatonicScales.Major_Scale2[saxNote] + 1);
                                        var oldsNote = diatonicScales.Major_Scale2[saxNote];
                                        Debug.Log("minthird" + oldsNote);
                                        Debug.Log("domthird" + sNote);
                                    }
                                    else
                                    {
                                        Saxophone.NoteOn(diatonicScales.Major_Scale2[saxNote], 1.0f);
                                    }
                                    prevSax_Range = 2;
                                }
                                else if (saxophoneRange == 3)
                                {
                                    if (cp.ChordType == "NonDiatonic" && saxNote == 4) //II7
                                    {
                                        Saxophone.NoteOn((diatonicScales.Major_Scale3[saxNote] + 1), 1.0f); //3
                                        var sNote = (diatonicScales.Major_Scale3[saxNote] + 1);
                                        var oldsNote = diatonicScales.Major_Scale3[saxNote];
                                        Debug.Log("minthird" + oldsNote);
                                        Debug.Log("domthird" + sNote);
                                    }
                                    else
                                    {
                                        Saxophone.NoteOn(diatonicScales.Major_Scale3[saxNote], 1.0f);
                                    }
                                    prevSax_Range = 3;
                                }
                                sax_f_sw = true;
                            }
                        }
                        else if (cp.KeyType == "NaturalMinor") // Locrian min7b5 3,5,1,11,7
                        {
                            if (!sax_f_sw)
                            {
                                chord2NoteShift();
                                Locrian_SNoteSelector();
                                saxRange();

                                if (saxophoneRange == 1)
                                {
                                    Saxophone.NoteOn(diatonicScales.NatMinor_Scale1[saxNote], 1.0f);
                                    prevSax_Range = 1;
                                }
                                else if (saxophoneRange == 2)
                                {
                                    Saxophone.NoteOn(diatonicScales.NatMinor_Scale2[saxNote], 1.0f);
                                    prevSax_Range = 2;
                                }
                                else if (saxophoneRange == 3)
                                {
                                    Saxophone.NoteOn(diatonicScales.NatMinor_Scale3[saxNote], 1.0f);
                                    prevSax_Range = 3;
                                }
                                sax_f_sw = true;
                            }
                        }
                        break;

                    case 3: // CHORD III-7, IIImaj7, III7
                        if (cp.KeyType == "Major") // Phrygian  
                        {
                            if (!sax_f_sw)
                            {
                                chord3NoteShift();
                                Phygian_SNoteSelector();
                                saxRange();

                                if (saxophoneRange == 1)
                                {
                                    Saxophone.NoteOn(diatonicScales.Major_Scale1[saxNote], 1.0f);
                                    prevSax_Range = 1;
                                }
                                else if (saxophoneRange == 2)
                                {
                                    Saxophone.NoteOn(diatonicScales.Major_Scale2[saxNote], 1.0f);
                                    prevSax_Range = 2;
                                }
                                else if (saxophoneRange == 3)
                                {
                                    Saxophone.NoteOn(diatonicScales.Major_Scale3[saxNote], 1.0f);
                                    prevSax_Range = 3;
                                }
                                sax_f_sw = true;
                            }
                        }
                        else if (cp.KeyType == "NaturalMinor") // Ionian Maj7(6)
                        {
                            if (!sax_f_sw)
                            {
                                chord3NoteShift();
                                MajDom_SNoteSelector();
                                saxRange();

                                if (saxophoneRange == 1)
                                {
                                    if (cp.ChordType == "NonDiatonic" && saxNote == 2) //7
                                    {
                                        Saxophone.NoteOn((diatonicScales.NatMinor_Scale1[saxNote] - 1), 1.0f); //b7
                                        var sNote = (diatonicScales.NatMinor_Scale1[saxNote] - 1);
                                        var oldsNote = diatonicScales.NatMinor_Scale1[saxNote];
                                        Debug.Log("majseven" + oldsNote);
                                        Debug.Log("domseven" + sNote);
                                    }
                                    else
                                    {
                                        Saxophone.NoteOn(diatonicScales.NatMinor_Scale1[saxNote], 1.0f); //7
                                    }
                                    prevSax_Range = 1;
                                }
                                else if (saxophoneRange == 2)
                                {
                                    if (cp.ChordType == "NonDiatonic" && saxNote == 2) //7
                                    {
                                        Saxophone.NoteOn((diatonicScales.NatMinor_Scale2[saxNote] - 1), 1.0f); //b7
                                        var sNote = (diatonicScales.NatMinor_Scale2[saxNote] - 1);
                                        var oldsNote = diatonicScales.NatMinor_Scale2[saxNote];
                                        Debug.Log("majseven" + oldsNote);
                                        Debug.Log("domseven" + sNote);
                                    }
                                    else
                                    {
                                        Saxophone.NoteOn(diatonicScales.NatMinor_Scale2[saxNote], 1.0f); //7
                                    }
                                    prevSax_Range = 2;
                                }
                                else if (saxophoneRange == 3)
                                {
                                    if (cp.ChordType == "NonDiatonic" && saxNote == 2) //7
                                    {
                                        Saxophone.NoteOn((diatonicScales.NatMinor_Scale3[saxNote] - 1), 1.0f); //b7
                                        var sNote = (diatonicScales.NatMinor_Scale3[saxNote] - 1);
                                        var oldsNote = diatonicScales.NatMinor_Scale3[saxNote];
                                        Debug.Log("majseven" + oldsNote);
                                        Debug.Log("domseven" + sNote);
                                    }
                                    else
                                    {
                                        Saxophone.NoteOn(diatonicScales.NatMinor_Scale3[saxNote], 1.0f); //7
                                    }
                                    prevSax_Range = 3;
                                }
                                sax_f_sw = true;
                            }
                        }
                        break;

                    case 4: // CHORD IV 
                        if (cp.KeyType == "Major") //Lydian 
                        {
                            if (!sax_f_sw)
                            {
                                chord4NoteShift();
                                Lydian_SNoteSelector();
                                saxRange();

                                if (saxophoneRange == 1)
                                {
                                    Saxophone.NoteOn(diatonicScales.Major_Scale1[saxNote], 1.0f);
                                    prevSax_Range = 1;
                                }
                                else if (saxophoneRange == 2)
                                {
                                    Saxophone.NoteOn(diatonicScales.Major_Scale2[saxNote], 1.0f);
                                    prevSax_Range = 2;
                                }
                                else if (saxophoneRange == 3)
                                {
                                    Saxophone.NoteOn(diatonicScales.Major_Scale3[saxNote], 1.0f);
                                    prevSax_Range = 3;
                                }
                                sax_f_sw = true;
                            }
                        }
                        else if (cp.KeyType == "NaturalMinor") //Dorian min7(9)
                        {
                            if (!sax_f_sw)
                            {
                                chord4NoteShift();
                                Min_SNoteSelector();
                                saxRange();

                                if (saxophoneRange == 1)
                                {
                                    Saxophone.NoteOn(diatonicScales.NatMinor_Scale1[saxNote], 1.0f);
                                    prevSax_Range = 1;
                                }
                                else if (saxophoneRange == 2)
                                {
                                    Saxophone.NoteOn(diatonicScales.NatMinor_Scale2[saxNote], 1.0f);
                                    prevSax_Range = 2;
                                }
                                else if (saxophoneRange == 3)
                                {
                                    Saxophone.NoteOn(diatonicScales.NatMinor_Scale3[saxNote], 1.0f);
                                    prevSax_Range = 3;
                                }
                                sax_f_sw = true;
                            }
                        }
                        break;

                    case 5: // CHORD V7, V7alt(b5,b9), V-7
                        if (cp.KeyType == "Major") // Mixolydian
                        {
                            if (!sax_f_sw)
                            {
                                chord5NoteShift();
                                MajDom_SNoteSelector();
                                saxRange();

                                if (saxophoneRange == 1)
                                {
                                    if (cp.ChordType == "NonDiatonic" && saxNote == 2 || cp.ChordType == "NonDiatonic" && saxNote == 6) //alt7 
                                    {
                                        Saxophone.NoteOn((diatonicScales.Major_Scale1[saxNote] - 1), 1.0f); //b5, b9
                                        var sNote = (diatonicScales.Major_Scale1[saxNote] - 1);
                                        var oldsNote = diatonicScales.Major_Scale1[saxNote];
                                        Debug.Log("dom" + oldsNote);
                                        Debug.Log("alt" + sNote);
                                    }
                                    else
                                    {
                                        Saxophone.NoteOn(diatonicScales.Major_Scale1[saxNote], 1.0f);
                                    }
                                    prevSax_Range = 1;
                                }
                                else if (saxophoneRange == 2)
                                {
                                    if (cp.ChordType == "NonDiatonic" && saxNote == 2 || cp.ChordType == "NonDiatonic" && saxNote == 6) //alt7 
                                    {
                                        Saxophone.NoteOn((diatonicScales.Major_Scale2[saxNote] - 1), 1.0f); //b5, b9
                                        var sNote = (diatonicScales.Major_Scale2[saxNote] - 1);
                                        var oldsNote = diatonicScales.Major_Scale2[saxNote];
                                        Debug.Log("dom" + oldsNote);
                                        Debug.Log("alt" + sNote);
                                    }
                                    else
                                    {
                                        Saxophone.NoteOn(diatonicScales.Major_Scale2[saxNote], 1.0f);
                                    }
                                    prevSax_Range = 2;
                                }
                                else if (saxophoneRange == 3)
                                {
                                    if (cp.ChordType == "NonDiatonic" && saxNote == 2 || cp.ChordType == "NonDiatonic" && saxNote == 6) //alt7 
                                    {
                                        Saxophone.NoteOn((diatonicScales.Major_Scale3[saxNote] - 1), 1.0f); //b5, b9
                                        var sNote = (diatonicScales.Major_Scale3[saxNote] - 1);
                                        var oldsNote = diatonicScales.Major_Scale3[saxNote];
                                        Debug.Log("dom" + oldsNote);
                                        Debug.Log("alt" + sNote);
                                    }
                                    else
                                    {
                                        Saxophone.NoteOn(diatonicScales.Major_Scale3[saxNote], 1.0f);
                                    }
                                    prevSax_Range = 3;
                                }
                                sax_f_sw = true;
                            }
                        }
                        else if (cp.KeyType == "NaturalMinor") //Phrygian
                        {
                            if (!sax_f_sw)
                            {
                                chord5NoteShift();
                                Phygian_SNoteSelector();
                                saxRange();

                                if (saxophoneRange == 1)
                                {
                                    Saxophone.NoteOn(diatonicScales.NatMinor_Scale1[saxNote], 1.0f);
                                    prevSax_Range = 1;
                                }
                                else if (saxophoneRange == 2)
                                {
                                    Saxophone.NoteOn(diatonicScales.NatMinor_Scale2[saxNote], 1.0f);
                                    prevSax_Range = 2;
                                }
                                else if (saxophoneRange == 3)
                                {
                                    Saxophone.NoteOn(diatonicScales.NatMinor_Scale3[saxNote], 1.0f);
                                    prevSax_Range = 3;
                                }
                                sax_f_sw = true;
                            }

                        }
                        break;

                    case 6: // CHORD VI-7, VImaj7, VI7  
                        if (cp.KeyType == "Major") //Aeolian
                        {
                            if (!sax_f_sw)
                            {
                                chord6NoteShift();
                                Min_SNoteSelector();
                                saxRange();

                                if (saxophoneRange == 1)
                                {
                                    Saxophone.NoteOn(diatonicScales.Major_Scale1[saxNote], 1.0f);
                                    prevSax_Range = 1;
                                }
                                else if (saxophoneRange == 2)
                                {
                                    Saxophone.NoteOn(diatonicScales.Major_Scale2[saxNote], 1.0f);
                                    prevSax_Range = 2;
                                }
                                else if (saxophoneRange == 3)
                                {
                                    Saxophone.NoteOn(diatonicScales.Major_Scale3[saxNote], 1.0f);
                                    prevSax_Range = 3;
                                }
                                sax_f_sw = true;
                            }
                        }
                        else if (cp.KeyType == "NaturalMinor") //Lydian
                        {
                            if (!sax_f_sw)
                            {
                                chord6NoteShift();
                                if (cp.ChordType == "NonDiatonic")
                                {
                                    MajDom_SNoteSelector();
                                }
                                else
                                {
                                    Lydian_SNoteSelector();
                                }
                                saxRange();

                                if (saxophoneRange == 1)
                                {
                                    if (cp.ChordType == "NonDiatonic" && saxNote == 5) //7
                                    {
                                        Saxophone.NoteOn((diatonicScales.NatMinor_Scale1[saxNote] - 1), 1.0f); //b7
                                        var sNote = (diatonicScales.NatMinor_Scale1[saxNote] - 1);
                                        var oldsNote = diatonicScales.NatMinor_Scale1[saxNote];
                                        Debug.Log("majseven" + oldsNote);
                                        Debug.Log("domseven" + sNote);
                                    }
                                    else
                                    {
                                        Saxophone.NoteOn(diatonicScales.NatMinor_Scale1[saxNote], 1.0f); //7
                                    }
                                    prevSax_Range = 1;
                                }
                                else if (saxophoneRange == 2)
                                {
                                    if (cp.ChordType == "NonDiatonic" && saxNote == 5) //7
                                    {
                                        Saxophone.NoteOn((diatonicScales.NatMinor_Scale2[saxNote] - 1), 1.0f); //b7
                                        var sNote = (diatonicScales.NatMinor_Scale2[saxNote] - 1);
                                        var oldsNote = diatonicScales.NatMinor_Scale2[saxNote];
                                        Debug.Log("majseven" + oldsNote);
                                        Debug.Log("domseven" + sNote);
                                    }
                                    else
                                    {
                                        Saxophone.NoteOn(diatonicScales.NatMinor_Scale2[saxNote], 1.0f); //7
                                    }
                                    prevSax_Range = 2;
                                }
                                else if (saxophoneRange == 3)
                                {
                                    if (cp.ChordType == "NonDiatonic" && saxNote == 5) //7
                                    {
                                        Saxophone.NoteOn((diatonicScales.NatMinor_Scale3[saxNote] - 1), 1.0f); //b7
                                        var sNote = (diatonicScales.NatMinor_Scale3[saxNote] - 1);
                                        var oldsNote = diatonicScales.NatMinor_Scale3[saxNote];
                                        Debug.Log("majseven" + oldsNote);
                                        Debug.Log("domseven" + sNote);
                                    }
                                    else
                                    {
                                        Saxophone.NoteOn(diatonicScales.NatMinor_Scale3[saxNote], 1.0f); //7
                                    }
                                    prevSax_Range = 3;
                                }
                                sax_f_sw = true;
                            }
                        }
                        break;

                    case 7: // CHORD VII0, VIIo, VII-7, VII7
                        if (cp.KeyType == "Major") //Locrian //min7b5
                        {
                            if (!sax_f_sw)
                            {
                                chord7NoteShift();
                                Locrian_SNoteSelector();
                                saxRange();

                                if (saxophoneRange == 1)
                                {
                                    if (cp.ChordType == "NonDiatonic" && saxNote == 6) //VIIO
                                    {
                                        Saxophone.NoteOn((diatonicScales.Major_Scale1[saxNote] - 1), 1.0f); //bb7
                                        var sNote = (diatonicScales.Major_Scale1[saxNote] - 1);
                                        var oldsNote = diatonicScales.Major_Scale1[saxNote];
                                        Debug.Log("dom" + oldsNote);
                                        Debug.Log("bbseven" + sNote);
                                    }
                                    else
                                    {
                                        Saxophone.NoteOn(diatonicScales.Major_Scale1[saxNote], 1.0f);
                                    }
                                    prevSax_Range = 1;
                                }
                                else if (saxophoneRange == 2)
                                {
                                    if (cp.ChordType == "NonDiatonic" && saxNote == 6)
                                    {
                                        Saxophone.NoteOn((diatonicScales.Major_Scale2[saxNote] - 1), 1.0f);
                                        var sNote = (diatonicScales.Major_Scale2[saxNote] - 1);
                                        var oldsNote = diatonicScales.Major_Scale2[saxNote];
                                        Debug.Log("dom" + oldsNote);
                                        Debug.Log("bbseven" + sNote);
                                    }
                                    else
                                    {
                                        Saxophone.NoteOn(diatonicScales.Major_Scale2[saxNote], 1.0f);
                                    }
                                    prevSax_Range = 2;
                                }
                                else if (saxophoneRange == 3)
                                {
                                    if (cp.ChordType == "NonDiatonic" && saxNote == 6)
                                    {
                                        Saxophone.NoteOn((diatonicScales.Major_Scale3[saxNote] - 1), 1.0f);
                                        var sNote = (diatonicScales.Major_Scale3[saxNote] - 1);
                                        var oldsNote = diatonicScales.Major_Scale3[saxNote];
                                        Debug.Log("dom" + oldsNote);
                                        Debug.Log("bbseven" + sNote);
                                    }
                                    else
                                    {
                                        Saxophone.NoteOn(diatonicScales.Major_Scale3[saxNote], 1.0f);
                                    }
                                    prevSax_Range = 3;
                                }
                                sax_f_sw = true;
                            }
                        }
                        else if (cp.KeyType == "NaturalMinor") //Mixolydian//dom7 
                        {
                            if (!sax_f_sw)
                            {
                                chord7NoteShift();
                                if (cp.ChordType == "NonDiatonic")
                                {
                                    Min_SNoteSelector();
                                }
                                else
                                {
                                    MajDom_SNoteSelector();
                                }
                                saxRange();

                                if (saxophoneRange == 1)
                                {
                                    if (cp.ChordType == "NonDiatonic" && saxNote == 2) //min7
                                    {
                                        Saxophone.NoteOn((diatonicScales.NatMinor_Scale1[saxNote] - 1), 1.0f); //b3
                                        var sNote = (diatonicScales.NatMinor_Scale1[saxNote] - 1);
                                        var oldsNote = diatonicScales.NatMinor_Scale1[saxNote];
                                        Debug.Log("majthird" + oldsNote);
                                        Debug.Log("minthird" + sNote);
                                    }
                                    else
                                    {
                                        Saxophone.NoteOn(diatonicScales.NatMinor_Scale1[saxNote], 1.0f);
                                    }
                                    prevSax_Range = 1;
                                }
                                else if (saxophoneRange == 2)
                                {
                                    if (cp.ChordType == "NonDiatonic" && saxNote == 2) //min7
                                    {
                                        Saxophone.NoteOn((diatonicScales.NatMinor_Scale2[saxNote] - 1), 1.0f); //b3
                                        var sNote = (diatonicScales.NatMinor_Scale2[saxNote] - 1);
                                        var oldsNote = diatonicScales.NatMinor_Scale2[saxNote];
                                        Debug.Log("majthird" + oldsNote);
                                        Debug.Log("minthird" + sNote);
                                    }
                                    else
                                    {
                                        Saxophone.NoteOn(diatonicScales.NatMinor_Scale2[saxNote], 1.0f);
                                    }
                                    prevSax_Range = 2;
                                }
                                else if (saxophoneRange == 3)
                                {
                                    if (cp.ChordType == "NonDiatonic" && saxNote == 2) //min7
                                    {
                                        Saxophone.NoteOn((diatonicScales.NatMinor_Scale3[saxNote] - 1), 1.0f); //b3
                                        var sNote = (diatonicScales.NatMinor_Scale3[saxNote] - 1);
                                        var oldsNote = diatonicScales.NatMinor_Scale3[saxNote];
                                        Debug.Log("majthird" + oldsNote);
                                        Debug.Log("minthird" + sNote);
                                    }
                                    else
                                    {
                                        Saxophone.NoteOn(diatonicScales.NatMinor_Scale3[saxNote], 1.0f);
                                    }
                                    prevSax_Range = 3;
                                }
                                sax_f_sw = true;
                            }
                        }
                        break;
                }
            }
            else
            {
                sax_f_sw = false;
                Saxophone.AllNotesOff();
            }
        }

        private void tptRange()
        {
            if (prevTpt_Range == 0)
            {
                trumpetRange = Random.Range(1, 5);
            }
            else if (prevTpt_Range == 1)
            {
                trumpetRange = Random.Range(1, 4);
            }
            else if (prevTpt_Range == 2)
            {
                trumpetRange = Random.Range(1, 4);
            }
            else if (prevTpt_Range == 3)
            {
                trumpetRange = Random.Range(2, 5);
            }
            else if (prevTpt_Range == 4)
            {
                trumpetRange = Random.Range(3, 5);
            }
        }
        private void saxRange()
        {
            if (trumpetRange == 1)
            {
                saxophoneRange = 1;
            }
            else if (trumpetRange == 2)
            {
                saxophoneRange = 1;
            }
            else if (trumpetRange == 3)
            {
                saxophoneRange = 2;
            }

        }

        private void chord1NoteShift()
        {
            MajDomnoteSelector[0] = 2;
            MajDomnoteSelector[1] = 3;
            MajDomnoteSelector[2] = 5;
            MajDomnoteSelector[3] = 6;
            MajDomnoteSelector[4] = 7;
            MajDomnoteSelector[5] = 1;

            MinnoteSelector[0] = 2;
            MinnoteSelector[1] = 3;
            MinnoteSelector[2] = 4;
            MinnoteSelector[3] = 5;
            MinnoteSelector[4] = 7;
            MinnoteSelector[5] = 1;

            LydiannoteSelector[0] = 2;
            LydiannoteSelector[1] = 3;
            LydiannoteSelector[2] = 4;
            LydiannoteSelector[3] = 5;
            LydiannoteSelector[4] = 6;
            LydiannoteSelector[5] = 7;
            LydiannoteSelector[6] = 1;

            PhygiannoteSelector[0] = 3;
            PhygiannoteSelector[1] = 4;
            PhygiannoteSelector[2] = 5;
            PhygiannoteSelector[3] = 7;
            PhygiannoteSelector[4] = 1;

            LocriannoteSelector[0] = 3;
            LocriannoteSelector[1] = 4;
            LocriannoteSelector[2] = 5;
            LocriannoteSelector[3] = 6;
            LocriannoteSelector[4] = 7;
            LocriannoteSelector[5] = 1;
        }
        private void chord2NoteShift()
        {
            MajDomnoteSelector[0] = 3;
            MajDomnoteSelector[1] = 4;
            MajDomnoteSelector[2] = 6;
            MajDomnoteSelector[3] = 7;
            MajDomnoteSelector[4] = 1;
            MajDomnoteSelector[5] = 2;

            MinnoteSelector[0] = 3;
            MinnoteSelector[1] = 4;
            MinnoteSelector[2] = 5;
            MinnoteSelector[3] = 6;
            MinnoteSelector[4] = 1;
            MinnoteSelector[5] = 2;

            LydiannoteSelector[0] = 3;
            LydiannoteSelector[1] = 4;
            LydiannoteSelector[2] = 5;
            LydiannoteSelector[3] = 6;
            LydiannoteSelector[4] = 7;
            LydiannoteSelector[5] = 1;
            LydiannoteSelector[6] = 2;

            PhygiannoteSelector[0] = 4;
            PhygiannoteSelector[1] = 5;
            PhygiannoteSelector[2] = 6;
            PhygiannoteSelector[3] = 1;
            PhygiannoteSelector[4] = 2;

            LocriannoteSelector[0] = 4;
            LocriannoteSelector[1] = 5;
            LocriannoteSelector[2] = 6;
            LocriannoteSelector[3] = 7;
            LocriannoteSelector[4] = 1;
            LocriannoteSelector[5] = 2;
        }
        private void chord3NoteShift()
        {
            MajDomnoteSelector[0] = 4;
            MajDomnoteSelector[1] = 5;
            MajDomnoteSelector[2] = 7;
            MajDomnoteSelector[3] = 1;
            MajDomnoteSelector[4] = 2;
            MajDomnoteSelector[5] = 3;

            MinnoteSelector[0] = 4;
            MinnoteSelector[1] = 5;
            MinnoteSelector[2] = 6;
            MinnoteSelector[3] = 7;
            MinnoteSelector[4] = 2;
            MinnoteSelector[5] = 3;

            LydiannoteSelector[0] = 4;
            LydiannoteSelector[1] = 5;
            LydiannoteSelector[2] = 6;
            LydiannoteSelector[3] = 7;
            LydiannoteSelector[4] = 1;
            LydiannoteSelector[5] = 2;
            LydiannoteSelector[6] = 3;

            PhygiannoteSelector[0] = 5;
            PhygiannoteSelector[1] = 6;
            PhygiannoteSelector[2] = 7;
            PhygiannoteSelector[3] = 2;
            PhygiannoteSelector[4] = 3;

            LocriannoteSelector[0] = 5;
            LocriannoteSelector[1] = 6;
            LocriannoteSelector[2] = 7;
            LocriannoteSelector[3] = 1;
            LocriannoteSelector[4] = 2;
            LocriannoteSelector[5] = 3;
        }
        private void chord4NoteShift()
        {
            MajDomnoteSelector[0] = 5;
            MajDomnoteSelector[1] = 6;
            MajDomnoteSelector[2] = 1;
            MajDomnoteSelector[3] = 2;
            MajDomnoteSelector[4] = 3;
            MajDomnoteSelector[5] = 4;

            MinnoteSelector[0] = 5;
            MinnoteSelector[1] = 6;
            MinnoteSelector[2] = 7;
            MinnoteSelector[3] = 1;
            MinnoteSelector[4] = 3;
            MinnoteSelector[5] = 4;

            LydiannoteSelector[0] = 5;
            LydiannoteSelector[1] = 6;
            LydiannoteSelector[2] = 7;
            LydiannoteSelector[3] = 1;
            LydiannoteSelector[4] = 2;
            LydiannoteSelector[5] = 3;
            LydiannoteSelector[6] = 4;

            PhygiannoteSelector[0] = 6;
            PhygiannoteSelector[1] = 7;
            PhygiannoteSelector[2] = 1;
            PhygiannoteSelector[3] = 3;
            PhygiannoteSelector[4] = 4;

            LocriannoteSelector[0] = 6;
            LocriannoteSelector[1] = 7;
            LocriannoteSelector[2] = 1;
            LocriannoteSelector[3] = 2;
            LocriannoteSelector[4] = 3;
            LocriannoteSelector[5] = 4;
        }
        private void chord5NoteShift()
        {
            MajDomnoteSelector[0] = 6;
            MajDomnoteSelector[1] = 7;
            MajDomnoteSelector[2] = 2;
            MajDomnoteSelector[3] = 3;
            MajDomnoteSelector[4] = 4;
            MajDomnoteSelector[5] = 5;

            MinnoteSelector[0] = 6;
            MinnoteSelector[1] = 7;
            MinnoteSelector[2] = 1;
            MinnoteSelector[3] = 2;
            MinnoteSelector[4] = 4;
            MinnoteSelector[5] = 5;

            LydiannoteSelector[0] = 6;
            LydiannoteSelector[1] = 7;
            LydiannoteSelector[2] = 1;
            LydiannoteSelector[3] = 2;
            LydiannoteSelector[4] = 3;
            LydiannoteSelector[5] = 4;
            LydiannoteSelector[6] = 5;

            PhygiannoteSelector[0] = 7;
            PhygiannoteSelector[1] = 1;
            PhygiannoteSelector[2] = 2;
            PhygiannoteSelector[3] = 4;
            PhygiannoteSelector[4] = 5;

            LocriannoteSelector[0] = 7;
            LocriannoteSelector[1] = 1;
            LocriannoteSelector[2] = 2;
            LocriannoteSelector[3] = 3;
            LocriannoteSelector[4] = 4;
            LocriannoteSelector[5] = 5;
        }
        private void chord6NoteShift()
        {
            MajDomnoteSelector[0] = 7;
            MajDomnoteSelector[1] = 1;
            MajDomnoteSelector[2] = 3;
            MajDomnoteSelector[3] = 4;
            MajDomnoteSelector[4] = 5;
            MajDomnoteSelector[5] = 6;

            MinnoteSelector[0] = 7;
            MinnoteSelector[1] = 1;
            MinnoteSelector[2] = 2;
            MinnoteSelector[3] = 3;
            MinnoteSelector[4] = 5;
            MinnoteSelector[5] = 6;

            LydiannoteSelector[0] = 7;
            LydiannoteSelector[1] = 1;
            LydiannoteSelector[2] = 2;
            LydiannoteSelector[3] = 3;
            LydiannoteSelector[4] = 4;
            LydiannoteSelector[5] = 5;
            LydiannoteSelector[6] = 6;

            PhygiannoteSelector[0] = 1;
            PhygiannoteSelector[1] = 2;
            PhygiannoteSelector[2] = 3;
            PhygiannoteSelector[3] = 5;
            PhygiannoteSelector[4] = 6;

            LocriannoteSelector[0] = 1;
            LocriannoteSelector[1] = 2;
            LocriannoteSelector[2] = 3;
            LocriannoteSelector[3] = 4;
            LocriannoteSelector[4] = 5;
            LocriannoteSelector[5] = 6;
        }
        private void chord7NoteShift()
        {
            MajDomnoteSelector[0] = 1;
            MajDomnoteSelector[1] = 2;
            MajDomnoteSelector[2] = 4;
            MajDomnoteSelector[3] = 5;
            MajDomnoteSelector[4] = 6;
            MajDomnoteSelector[5] = 7;

            MinnoteSelector[0] = 1;
            MinnoteSelector[1] = 2;
            MinnoteSelector[2] = 3;
            MinnoteSelector[3] = 4;
            MinnoteSelector[4] = 6;
            MinnoteSelector[5] = 7;

            LydiannoteSelector[0] = 1;
            LydiannoteSelector[1] = 2;
            LydiannoteSelector[2] = 3;
            LydiannoteSelector[3] = 4;
            LydiannoteSelector[4] = 5;
            LydiannoteSelector[5] = 6;
            LydiannoteSelector[6] = 7;

            PhygiannoteSelector[0] = 2;
            PhygiannoteSelector[1] = 3;
            PhygiannoteSelector[2] = 4;
            PhygiannoteSelector[3] = 6;
            PhygiannoteSelector[4] = 7;

            LocriannoteSelector[0] = 2;
            LocriannoteSelector[1] = 3;
            LocriannoteSelector[2] = 4;
            LocriannoteSelector[3] = 5;
            LocriannoteSelector[4] = 6;
            LocriannoteSelector[5] = 7;
        }

        private void MajDom_TNoteSelector()
        {
            var noteSelector = Random.Range(0, MajDomnoteSelector.Length);
            if (noteSelector == 0) trumpetNote = MajDomnoteSelector[0];
            if (noteSelector == 1) trumpetNote = MajDomnoteSelector[1];
            if (noteSelector == 2) trumpetNote = MajDomnoteSelector[2];
            if (noteSelector == 3) trumpetNote = MajDomnoteSelector[3];
            if (noteSelector == 4) trumpetNote = MajDomnoteSelector[4];
            if (noteSelector == 5) trumpetNote = MajDomnoteSelector[5];
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
            if (noteSelector == 5) trumpetNote = MinnoteSelector[5];
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
            if (noteSelector == 5) trumpetNote = LocriannoteSelector[5];
            Debug.Log("TRUMPETNOTE " + trumpetNote);
        }
        private void Phygian_TNoteSelector()
        {
            var noteSelector = Random.Range(0, PhygiannoteSelector.Length);
            if (noteSelector == 0) trumpetNote = PhygiannoteSelector[0];
            if (noteSelector == 1) trumpetNote = PhygiannoteSelector[1];
            if (noteSelector == 2) trumpetNote = PhygiannoteSelector[2];
            if (noteSelector == 3) trumpetNote = PhygiannoteSelector[3];
            if (noteSelector == 4) trumpetNote = PhygiannoteSelector[4];
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
            if (noteSelector == 6) trumpetNote = LydiannoteSelector[6];
            Debug.Log("TRUMPETNOTE " + trumpetNote);
        }

        private void MajDom_SNoteSelector() //235671
        {
            var noteSelector = Random.Range(0, 5);
         //find harmony note oct/unison,3rds,4ths,6ths, 5ths
          
            if (noteSelector == 0 && trumpetNote == MajDomnoteSelector[0] || noteSelector == 1 && trumpetNote == MajDomnoteSelector[0] || noteSelector == 0 && trumpetNote == MajDomnoteSelector[2] || noteSelector == 0 && trumpetNote == MajDomnoteSelector[3] || noteSelector == 0 && trumpetNote == MajDomnoteSelector[4])
            {
                saxNote = MajDomnoteSelector[0];//2
            }
            if (noteSelector == 0 && trumpetNote == MajDomnoteSelector[1] || noteSelector == 1 && trumpetNote == MajDomnoteSelector[2] || noteSelector == 1 && trumpetNote == MajDomnoteSelector[3] || noteSelector == 2 && trumpetNote == MajDomnoteSelector[3] || noteSelector == 1 && trumpetNote == MajDomnoteSelector[4] || noteSelector == 0 && trumpetNote == MajDomnoteSelector[5])
            {
                saxNote = MajDomnoteSelector[1]; //3
            }
            if (noteSelector == 2 && trumpetNote == MajDomnoteSelector[0] || noteSelector == 1 && trumpetNote == MajDomnoteSelector[1] || noteSelector == 2 && trumpetNote == MajDomnoteSelector[2] || noteSelector == 2 && trumpetNote == MajDomnoteSelector[4] || noteSelector == 1 && trumpetNote == MajDomnoteSelector[5])
            {
                saxNote = MajDomnoteSelector[2]; //5
            }
            if (noteSelector == 3 && trumpetNote == MajDomnoteSelector[0] || noteSelector == 2 && trumpetNote == MajDomnoteSelector[1] || noteSelector == 3 && trumpetNote == MajDomnoteSelector[3] || noteSelector == 2 && trumpetNote == MajDomnoteSelector[5])
            {
                saxNote = MajDomnoteSelector[3]; //6
            }
            if (noteSelector == 4 && trumpetNote == MajDomnoteSelector[0] || noteSelector == 3 && trumpetNote == MajDomnoteSelector[1] || noteSelector == 3 && trumpetNote == MajDomnoteSelector[2] || noteSelector == 3 && trumpetNote == MajDomnoteSelector[4] || noteSelector == 4 && trumpetNote == MajDomnoteSelector[4])
            {
                saxNote = MajDomnoteSelector[4]; //7
            }
            if (noteSelector == 4 && trumpetNote == MajDomnoteSelector[1] || noteSelector == 4 && trumpetNote == MajDomnoteSelector[2] || noteSelector == 4 && trumpetNote == MajDomnoteSelector[3] || noteSelector == 3 && trumpetNote == MajDomnoteSelector[5] || noteSelector == 4 && trumpetNote == MajDomnoteSelector[5])
            {
                saxNote = MajDomnoteSelector[5]; //1
            }
            Debug.Log("SAXNOTE " + saxNote);
        }
        private void Min_SNoteSelector() //234571
        {
            var noteSelector = Random.Range(0, 5);
            //find harmony note oct/unison,3rds,4ths,6ths, 5ths

            if (noteSelector == 0 && trumpetNote == MinnoteSelector[0] || noteSelector == 1 && trumpetNote == MinnoteSelector[0] || noteSelector == 0 && trumpetNote == MinnoteSelector[2] || noteSelector == 0 && trumpetNote == MinnoteSelector[3] || noteSelector == 0 && trumpetNote == MinnoteSelector[4])
            {
                saxNote = MinnoteSelector[0];//2
            }
            if (noteSelector == 0 && trumpetNote == MinnoteSelector[1] || noteSelector == 1 && trumpetNote == MinnoteSelector[1] || noteSelector == 1 && trumpetNote == MinnoteSelector[3]  || noteSelector == 1 && trumpetNote == MinnoteSelector[4] || noteSelector == 0 && trumpetNote == MinnoteSelector[5])
            {
                saxNote = MinnoteSelector[1];//3
            }
            if (noteSelector == 2 && trumpetNote == MinnoteSelector[0] || noteSelector == 1 && trumpetNote == MinnoteSelector[2] || noteSelector == 2 && trumpetNote == MinnoteSelector[2] || noteSelector == 2 && trumpetNote == MinnoteSelector[4] || noteSelector == 1 && trumpetNote == MinnoteSelector[5])
            {
                saxNote = MinnoteSelector[2];//4
            }
            if (noteSelector == 3 && trumpetNote == MinnoteSelector[0] || noteSelector == 2 && trumpetNote == MinnoteSelector[1] || noteSelector == 2 && trumpetNote == MinnoteSelector[3] || noteSelector == 3 && trumpetNote == MinnoteSelector[4] || noteSelector == 2 && trumpetNote == MinnoteSelector[5])
            {
                saxNote = MinnoteSelector[3];//5
            }
            if (noteSelector == 4 && trumpetNote == MinnoteSelector[0] || noteSelector == 3 && trumpetNote == MinnoteSelector[1] || noteSelector == 3 && trumpetNote == MinnoteSelector[2] || noteSelector == 3 && trumpetNote == MinnoteSelector[3] || noteSelector == 4 && trumpetNote == MinnoteSelector[4])
            {
                saxNote = MinnoteSelector[4];//7
            }
            if (noteSelector == 4 && trumpetNote == MinnoteSelector[1] || noteSelector == 4 && trumpetNote == MinnoteSelector[2] || noteSelector == 4 && trumpetNote == MinnoteSelector[3] || noteSelector == 3 && trumpetNote == MinnoteSelector[5] || noteSelector == 4 && trumpetNote == MinnoteSelector[5])
            {
                saxNote = MinnoteSelector[5];//1
            }

            Debug.Log("SAXNOTE " + saxNote);
        }
        private void Locrian_SNoteSelector() //345671
        {
            var noteSelector = Random.Range(0, 5);
            //find harmony note oct/unison,3rds,4ths,6ths, 5ths

            if (noteSelector == 0 && trumpetNote == LocriannoteSelector[0] || noteSelector == 0 && trumpetNote == LocriannoteSelector[2] || noteSelector == 0 && trumpetNote == LocriannoteSelector[3] || noteSelector == 0 && trumpetNote == LocriannoteSelector[4] || noteSelector == 0 && trumpetNote == LocriannoteSelector[5])
            {
                saxNote = LocriannoteSelector[0];//3
            }
            if (noteSelector == 0 && trumpetNote == LocriannoteSelector[1] || noteSelector == 1 && trumpetNote == LocriannoteSelector[1] || noteSelector == 1 && trumpetNote == LocriannoteSelector[3] || noteSelector == 1 && trumpetNote == LocriannoteSelector[4] || noteSelector == 1 && trumpetNote == LocriannoteSelector[5])
            {
                saxNote = LocriannoteSelector[1];//4
            }
            if (noteSelector == 1 && trumpetNote == LocriannoteSelector[0] || noteSelector == 1 && trumpetNote == LocriannoteSelector[2] || noteSelector == 2 && trumpetNote == LocriannoteSelector[2] || noteSelector == 2 && trumpetNote == LocriannoteSelector[4] || noteSelector == 2 && trumpetNote == LocriannoteSelector[5])
            {
                saxNote = LocriannoteSelector[2];//5
            }
            if (noteSelector == 2 && trumpetNote == LocriannoteSelector[0] || noteSelector == 2 && trumpetNote == LocriannoteSelector[1] || noteSelector == 2 && trumpetNote == LocriannoteSelector[3] || noteSelector == 3 && trumpetNote == LocriannoteSelector[3] || noteSelector == 3 && trumpetNote == LocriannoteSelector[5])
            {
                saxNote = LocriannoteSelector[3];//6
            }
            if (noteSelector == 3 && trumpetNote == LocriannoteSelector[0] || noteSelector == 3 && trumpetNote == LocriannoteSelector[1] || noteSelector == 3 && trumpetNote == LocriannoteSelector[2] || noteSelector == 3 && trumpetNote == LocriannoteSelector[4] || noteSelector == 4 && trumpetNote == LocriannoteSelector[4])
            {
                saxNote = LocriannoteSelector[4];//7
            }
            if (noteSelector == 4 && trumpetNote == LocriannoteSelector[0] || noteSelector == 4 && trumpetNote == LocriannoteSelector[1] || noteSelector == 4 && trumpetNote == LocriannoteSelector[2] || noteSelector == 4 && trumpetNote == LocriannoteSelector[3] || noteSelector == 4 && trumpetNote == LocriannoteSelector[5])
            {
                saxNote = LocriannoteSelector[5];//1
            }

            Debug.Log("SAXNOTE " + saxNote);
        }
        private void Phygian_SNoteSelector() //34571
        {
            var noteSelector = Random.Range(0, 5);
            //find harmony note oct/unison,3rds,4ths,6ths, 5ths

            if (noteSelector == 0 && trumpetNote == PhygiannoteSelector[0] || noteSelector == 0 && trumpetNote == PhygiannoteSelector[2] || noteSelector == 0 && trumpetNote == PhygiannoteSelector[3] || noteSelector == 0 && trumpetNote == PhygiannoteSelector[4])
            {
                saxNote = PhygiannoteSelector[0];//3
            }
            if (noteSelector == 0 && trumpetNote == PhygiannoteSelector[1] || noteSelector == 1 && trumpetNote == PhygiannoteSelector[1] || noteSelector == 1 && trumpetNote == PhygiannoteSelector[3] || noteSelector == 1 && trumpetNote == PhygiannoteSelector[4])
            {
                saxNote = PhygiannoteSelector[1];//4
            }
            if (noteSelector == 1 && trumpetNote == PhygiannoteSelector[0] || noteSelector == 1 && trumpetNote == PhygiannoteSelector[2] || noteSelector == 2 && trumpetNote == PhygiannoteSelector[2] || noteSelector == 2 && trumpetNote == PhygiannoteSelector[3] || noteSelector == 2 && trumpetNote == PhygiannoteSelector[4])
            {
                saxNote = PhygiannoteSelector[2];//5
            }
            if (noteSelector == 3 && trumpetNote == PhygiannoteSelector[0] || noteSelector == 3 && trumpetNote == PhygiannoteSelector[1] || noteSelector == 3 && trumpetNote == PhygiannoteSelector[2] || noteSelector == 3 && trumpetNote == PhygiannoteSelector[3] || noteSelector == 4 && trumpetNote == PhygiannoteSelector[3])
            {
                saxNote = PhygiannoteSelector[3];//7
            }
            if (noteSelector == 4 && trumpetNote == PhygiannoteSelector[0] || noteSelector == 4 && trumpetNote == PhygiannoteSelector[1] || noteSelector == 4 && trumpetNote == PhygiannoteSelector[2] || noteSelector == 4 && trumpetNote == PhygiannoteSelector[4] || noteSelector == 4 && trumpetNote == PhygiannoteSelector[4])
            {
                saxNote = PhygiannoteSelector[4];//1
            }

            Debug.Log("SAXNOTE " + saxNote);
        }
        private void Lydian_SNoteSelector() //2345671
        {
            var noteSelector = Random.Range(0, 5);
            //find harmony note oct/unison,3rds,4ths,6ths, 5ths

            if (noteSelector == 0 && trumpetNote == LydiannoteSelector[0] || noteSelector == 0 && trumpetNote == LydiannoteSelector[2] || noteSelector == 0 && trumpetNote == LydiannoteSelector[3] || noteSelector == 0 && trumpetNote == LydiannoteSelector[4] || noteSelector == 0 && trumpetNote == LydiannoteSelector[5])
            {
                saxNote = LydiannoteSelector[0];//2
            }
            if (noteSelector == 0 && trumpetNote == LydiannoteSelector[1] || noteSelector == 1 && trumpetNote == LydiannoteSelector[3] || noteSelector == 1 && trumpetNote == LydiannoteSelector[4] || noteSelector == 1 && trumpetNote == LydiannoteSelector[5] || noteSelector == 0 && trumpetNote == LydiannoteSelector[6])
            {
                saxNote = LydiannoteSelector[1]; //3
            }
            if (noteSelector == 1 && trumpetNote == LydiannoteSelector[0] || noteSelector == 1 && trumpetNote == LydiannoteSelector[2] || noteSelector == 2 && trumpetNote == LydiannoteSelector[4] || noteSelector == 2 && trumpetNote == LydiannoteSelector[5] || noteSelector == 1 && trumpetNote == LydiannoteSelector[6])
            {
                saxNote = LydiannoteSelector[2]; //4
            }
            if (noteSelector == 2 && trumpetNote == LydiannoteSelector[0] || noteSelector == 1 && trumpetNote == LydiannoteSelector[1] || noteSelector == 2 && trumpetNote == LydiannoteSelector[3] || noteSelector == 3 && trumpetNote == LydiannoteSelector[5] || noteSelector == 2 && trumpetNote == LydiannoteSelector[6])
            {
                saxNote = LydiannoteSelector[3]; //5
            }
            if (noteSelector == 3 && trumpetNote == LydiannoteSelector[0] || noteSelector == 2 && trumpetNote == LydiannoteSelector[1] || noteSelector == 2 && trumpetNote == LydiannoteSelector[2] || noteSelector == 3 && trumpetNote == LydiannoteSelector[4] || noteSelector == 3 && trumpetNote == LydiannoteSelector[6])
            {
                saxNote = LydiannoteSelector[4]; //6
            }
            if (noteSelector == 4 && trumpetNote == LydiannoteSelector[0] || noteSelector == 3 && trumpetNote == LydiannoteSelector[1] || noteSelector == 3 && trumpetNote == LydiannoteSelector[2] || noteSelector == 3 && trumpetNote == LydiannoteSelector[3] || noteSelector == 4 && trumpetNote == LydiannoteSelector[5])
            {
                saxNote = LydiannoteSelector[5]; //7
            }
            if (noteSelector == 4 && trumpetNote == LydiannoteSelector[1] || noteSelector == 4 && trumpetNote == LydiannoteSelector[2] || noteSelector == 4 && trumpetNote == LydiannoteSelector[3] || noteSelector == 4 && trumpetNote == LydiannoteSelector[4] || noteSelector == 4 && trumpetNote == LydiannoteSelector[6])
            {
                saxNote = LydiannoteSelector[6]; //1
            }
            Debug.Log("SAXNOTE " + saxNote);
        }
    }
}
