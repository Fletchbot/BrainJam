using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoliSoundScape
{
    public class SoliSoundscapeLvl2 : MonoBehaviour
    {
        ChordProgressions cp;
        SoliSoundscapeLvl1 lvl1;

        public int lvl2State;
        public bool changeInstrument;
        private bool Happy, Sad, Unsure, prevH, curisHappy, prevS, curisSad, prevU, curisUnsure;
        private bool isHappy, isSad, isUnsure;
        private bool h_sw, s_sw, u_sw;
        private int lvl1State;

        // Use this for initialization
        void Start()
        {
            cp = this.GetComponent<ChordProgressions>();
            lvl1 = this.GetComponent<SoliSoundscapeLvl1>();
        }

        // Update is called once per frame
        void Update()
        {
            lvl1State = lvl1.lvl1State;

            isHappy = lvl1.isHappy;
            isSad = lvl1.isSad;
            isUnsure = lvl1.isUnsure;

            if (lvl1State == -1 && lvl2State == -1)
            {
                lvl2State = 0;
                lvl1.lvl1State = lvl1State;
                lvl1State = -1;
            }

            if (lvl1.headsetOn == 1)
            {
                e_states();
                Lvl2ChordStates();
                e_off();
                e_sw();
            }

        }

        public void e_states()
        {
            if (isHappy && !h_sw)
            {
                Happy = true;
                h_sw = true;
            }
            if (isSad && !s_sw)
            {
                Sad = true;
                s_sw = true;
            }
            if (isUnsure && !u_sw)
            {
                Unsure = true;
                u_sw = true;
            }
        }

        private void MajoisSadW()
        {
            cp.KeyType = "Major";
        }
        private void NaturalMinoisSadW()
        {
            cp.KeyType = "NaturalMinor";
        }
        private void DiatonicSW()
        {
            cp.ChordType = "";
        }

        public void Lvl2ChordStates()
        {
            if (lvl1.gameState == -1)
            {
                if (lvl2State == 0) // START LEVEL 2 MAJOR OR MINOR
                {
                    if (Happy && !curisHappy)
                    {
                        curisHappy = true;
                        prevH = true;
                    }
                    else if (Sad && !curisSad)
                    {
                        curisSad = true;
                        prevS = true;
                    }
                    else if (Unsure && !curisUnsure) 
                    {
                        curisUnsure = true;
                        prevU = true;
                    }

                    if (curisHappy && !Happy)
                    {
                        curisHappy = false;
                        lvl2State++;
                    }
                    else if (curisSad && !Sad)
                    {
                        curisSad = false;
                        lvl2State++;
                    }
                    else if (curisUnsure && !Unsure)
                    {
                        curisUnsure = false;
                        lvl2State++;
                    }
                    cp.ChordVoicing = "Extended";
                    cp.ChordType = "";
                    cp.chords[1] = true;

                }
                else if (lvl2State == 1) // LVL 2.1 
                {
                    if (Happy && !curisHappy)
                    {
                        if (prevH && cp.KeyType == "Major")
                        {
                            cp.KeyType = "NaturalMinor";
                            cp.ChordType = "";
                            cp.chords[6] = true;
                            Invoke("MajoisSadW", 1.0f);
                        }
                        else if (prevH && cp.KeyType == "NaturalMinor")
                        {
                            cp.chords[7] = true;
                        }
                        if (prevS && cp.KeyType == "Major")
                        {
                            cp.ChordType = "NonDiatonic";
                            cp.chords[2] = true;
                            Invoke("DiatonicSW", 1.0f);
                        }
                        else if (prevS && cp.KeyType == "NaturalMinor")
                        {
                            cp.chords[7] = true;
                        }

                        if (prevU && cp.KeyType == "Major")
                        {
                            cp.KeyType = "NaturalMinor";
                            cp.ChordType = "NonDiatonic";
                            cp.chords[6] = true;
                            Invoke("MajoisSadW", 1.0f);
                            Invoke("DiatonicSW", 1.0f);
                        }
                        else if (prevU && cp.KeyType == "NaturalMinor")
                        {
                            cp.chords[7] = true;
                        }
                        curisHappy = true;
                        prevH = true;

                        prevS = false;
                        prevU = false;                       
                    }
                    else if (Sad && !curisSad)
                    {
                        if (prevH && cp.KeyType == "Major")
                        {
                            cp.chords[2] = true;
                        }
                        else if (prevH && cp.KeyType == "NaturalMinor")
                        {
                            cp.ChordType = "NonDiatonic";
                            cp.chords[7] = true;
                            Invoke("DiatonicSW", 1.0f);
                        }

                        if (prevS && cp.KeyType == "Major")
                        {
                            cp.chords[7] = true;
                        }
                        else if (prevS && cp.KeyType == "NaturalMinor")
                        {
                            cp.ChordType = "NonDiatonic";
                            cp.chords[7] = true;
                            Invoke("DiatonicSW", 1.0f);
                        }

                        if (prevU && cp.KeyType == "Major")
                        {
                            cp.chords[2] = true;
                        }
                        else if (prevU && cp.KeyType == "NaturalMinor")
                        {
                            cp.chords[2] = true;
                        }

                        curisSad = true;
                        prevS = true;

                        prevH = false;
                        prevU = false;
                    }
                    else if (Unsure)
                    {
                        if (prevH && cp.KeyType == "Major")
                        {
                            cp.ChordType = "NonDiatonic";
                            cp.chords[2] = true;
                            Invoke("DiatonicSW", 1.0f);
                        }
                        else if (prevH && cp.KeyType == "NaturalMinor")
                        {
                            cp.ChordType = "NonDiatonic";
                            cp.chords[6] = true;
                            Invoke("DiatonicSW", 1.0f);
                        }

                        if (prevS && cp.KeyType == "Major")
                        {
                            cp.chords[2] = true;
                        }
                        else if (prevS && cp.KeyType == "NaturalMinor")
                        {
                            cp.chords[2] = true;
                        }

                        if (prevU && cp.KeyType == "Major")
                        {
                            cp.ChordType = "NonDiatonic";
                            cp.chords[2] = true;
                            Invoke("DiatonicSW", 1.0f);
                        }
                        else if (prevU && cp.KeyType == "NaturalMinor")
                        {
                            cp.ChordType = "NonDiatonic";
                            cp.chords[6] = true;
                            Invoke("DiatonicSW", 1.0f);
                        }
                            curisUnsure = true;
                            prevU = true;

                            prevH = false;
                            prevS = false;
                        }

                    if (curisHappy && !Happy)
                    {
                        curisHappy = false;
                        lvl2State++;
                    }
                    else if (curisSad && !Sad)
                    {
                        curisSad = false;
                        lvl2State++;
                    }
                    else if (curisUnsure && !Unsure)
                    {
                        curisUnsure = false;
                        lvl2State++;
                    }
                }
                else if (lvl2State == 2) //LVL 2.2
                {
                    if (Happy && !curisHappy)
                    {
                        if (prevH && cp.KeyType == "Major")
                        {
                            cp.chords[5] = true;
                        }
                        else if (prevH && cp.KeyType == "NaturalMinor")
                        {
                            cp.chords[6] = true;
                        }

                        if (prevS && cp.KeyType == "Major")
                        {
                            cp.KeyType = "NaturalMinor";
                            cp.ChordType = "";
                            cp.chords[7] = true;
                            Invoke("MajoisSadW", 1.0f);
                        }
                        else if (prevS && cp.KeyType == "NaturalMinor")
                        {
                            cp.chords[6] = true;
                        }

                        if (prevU && cp.KeyType == "Major")
                        {
                            cp.ChordType = "NonDiatonic";
                            cp.chords[2] = true;
                            Invoke("DiatonicSW", 1.0f);
                        }
                        else if (prevU && cp.KeyType == "NaturalMinor")
                        {
                            cp.KeyType = "Major";
                            cp.ChordType = "NonDiatonic";
                            cp.chords[5] = true;
                            Invoke("NaturalMinoisSadW", 1.0f);
                            Invoke("DiatonicSW", 1.0f);
                        }

                        curisHappy = true;
                        prevH = true;

                        prevS = false;
                        prevU = false;
                    }
                    else if (Sad && !curisSad)
                    {
                        if (prevH && cp.KeyType == "Major")
                        {
                            cp.KeyType = "NaturalMinor";
                            cp.ChordType = "NonDiatonic";
                            cp.chords[3] = true;
                            Invoke("MajoisSadW", 1.0f);
                            Invoke("DiatonicSW", 1.0f);
                        }
                        else if (prevH && cp.KeyType == "NaturalMinor")
                        {
                            cp.ChordType = "NonDiatonic";
                            cp.chords[6] = true;
                            Invoke("DiatonicSW", 1.0f);
                        }

                        if (prevS && cp.KeyType == "Major")
                        {
                            cp.chords[2] = true;
                        }
                        else if (prevS && cp.KeyType == "NaturalMinor")
                        {
                            cp.KeyType = "Major";
                            cp.ChordType = "NonDiatonic";
                            cp.chords[5] = true;
                            Invoke("NaturalMinoisSadW", 1.0f);
                            Invoke("DiatonicSW", 1.0f);
                        }

                        if (prevU && cp.KeyType == "Major")
                        {
                            cp.chords[2] = true;
                        }
                        else if (prevU && cp.KeyType == "NaturalMinor")
                        {
                            cp.ChordType = "NonDiatonic";
                            cp.chords[6] = true;
                            Invoke("DiatonicSW", 1.0f);
                        }

                        curisSad = true;
                        prevS = true;

                        prevH = false;
                        prevU = false;
                    }
                    else if (Unsure)
                    {
                        if (prevH && cp.KeyType == "Major")
                        {
                            cp.ChordType = "NonDiatonic";
                            cp.chords[2] = true;
                            Invoke("DiatonicSW", 1.0f);
                        }
                        else if (prevH && cp.KeyType == "NaturalMinor")
                        {
                            cp.KeyType = "Major";
                            cp.ChordType = "";
                            cp.chords[5] = true;
                            Invoke("NaturalMinoisSadW", 1.0f);
                            Invoke("DiatonicSW", 1.0f);
                        }

                        if (prevS && cp.KeyType == "Major")
                        {
                            cp.chords[5] = true;
                        }
                        else if (prevS && cp.KeyType == "NaturalMinor")
                        {
                            cp.ChordType = "NonDiatonic";
                            cp.chords[6] = true;
                            Invoke("DiatonicSW", 1.0f);
                        }

                        if (prevU && cp.KeyType == "Major")
                        {
                            cp.KeyType = "NaturalMinor";
                            cp.ChordType = "NonDiatonic";
                            cp.chords[3] = true;
                            Invoke("MajoisSadW", 1.0f);
                            Invoke("DiatonicSW", 1.0f);
                        }
                        else if (prevU && cp.KeyType == "NaturalMinor")
                        {
                            cp.chords[6] = true;
                        }

                        curisUnsure = true;
                        prevU = true;

                        prevH = false;
                        prevS = false;
                    }

                    if (curisHappy && !Happy)
                    {
                        curisHappy = false;
                        lvl2State++;
                    }
                    else if (curisSad && !Sad)
                    {
                        curisSad = false;
                        lvl2State++;
                    }
                    else if (curisUnsure && !Unsure)
                    {
                        curisUnsure = false;
                        lvl2State++;
                    }
                }
                else if (lvl2State == 3) //LVL 2.3
                {
                    if (Happy && !curisHappy)
                    {
                        if (prevH && cp.KeyType == "Major")
                        {
                            cp.ChordType = "NonDiatonic";
                            cp.chords[5] = true;
                            Invoke("DiatonicSW", 1.0f);
                        }
                        else if (prevH && cp.KeyType == "NaturalMinor")
                        {
                            cp.KeyType = "Major";
                            cp.chords[5] = true;
                            Invoke("NaturalMinoisSadW", 1.0f);
                            Invoke("DiatonicSW", 1.0f);
                        }

                        if (prevS && cp.KeyType == "Major")
                        {
                            cp.KeyType = "NaturalMinor";
                            cp.chords[3] = true;
                            Invoke("MajoisSadW", 1.0f);
                        }
                        else if (prevS && cp.KeyType == "NaturalMinor")
                        {
                            cp.KeyType = "Major";
                            cp.chords[5] = true;
                            Invoke("NaturalMinoisSadW", 1.0f);
                            Invoke("DiatonicSW", 1.0f);
                        }

                        if (prevU && cp.KeyType == "Major")
                        {
                            cp.ChordType = "NonDiatonic";
                            cp.chords[2] = true;
                            Invoke("DiatonicSW", 1.0f);
                        }
                        else if (prevU && cp.KeyType == "NaturalMinor")
                        {
                            cp.KeyType = "Major";
                            cp.chords[5] = true;
                            Invoke("NaturalMinoisSadW", 1.0f);
                            Invoke("DiatonicSW", 1.0f);
                        }

                        curisHappy = true;
                        prevH = true;

                        prevS = false;
                        prevU = false;
                    }
                    else if (Sad && !curisSad)
                    {
                        if (prevH && cp.KeyType == "Major")
                        {
                            cp.KeyType = "NaturalMinor";
                            cp.chords[5] = true;
                            Invoke("MajoisSadW", 1.0f);
                        }
                        else if (prevH && cp.KeyType == "NaturalMinor")
                        {
                            cp.chords[1] = true;
                        }

                        if (prevS && cp.KeyType == "Major")
                        {
                            cp.chords[6] = true;
                        }
                        else if (prevS && cp.KeyType == "NaturalMinor")
                        {
                            cp.chords[1] = true;
                        }

                        if (prevU && cp.KeyType == "Major")
                        {
                            cp.chords[2] = true;
                        }
                        else if (prevU && cp.KeyType == "NaturalMinor")
                        {
                            cp.chords[1] = true;
                        }

                        curisSad = true;
                        prevS = true;

                        prevH = false;
                        prevU = false;
                    }
                    else if (Unsure)
                    {
                        if (prevH && cp.KeyType == "Major")
                        {
                            cp.KeyType = "NaturalMinor";
                            cp.chords[3] = true;
                            Invoke("MajoisSadW", 1.0f);
                        }
                        else if (prevH && cp.KeyType == "NaturalMinor")
                        {
                            cp.chords[4] = true;
                        }

                        if (prevS && cp.KeyType == "Major")
                        {
                            cp.KeyType = "NaturalMinor";
                            cp.chords[5] = true;
                            Invoke("MajoisSadW", 1.0f);
                        }
                        else if (prevS && cp.KeyType == "NaturalMinor")
                        {
                            cp.chords[4] = true;
                        }

                        if (prevU && cp.KeyType == "Major")
                        {
                            cp.ChordType = "NonDiatonic";
                            cp.chords[5] = true;
                            Invoke("DiatonicSW", 1.0f);
                        }
                        else if (prevU && cp.KeyType == "NaturalMinor")
                        {
                            cp.chords[4] = true;
                        }

                        curisUnsure = true;
                        prevU = true;

                        prevH = false;
                        prevS = false;
                    }

                    if (curisHappy && !Happy)
                    {
                        curisHappy = false;
                        lvl2State++;
                    }
                    else if (curisSad && !Sad)
                    {
                        curisSad = false;
                        lvl2State++;
                    }
                    else if (curisUnsure && !Unsure)
                    {
                        curisUnsure = false;
                        lvl2State++;
                    }
                }
                else if (lvl2State == 4) //LVL 2.4
                {
                    cp.chords[1] = true;
                    lvl2State = -1;
                    changeInstrument = true;
                }
            }
        }

        public void e_off()
        {
            if (h_sw)
            {
                Happy = false;
            }
            if (s_sw)
            {
                Sad = false;
            }
            if (u_sw)
            {
                Unsure = false;
            }
        }

        public void e_sw()
        {
            if (!isHappy) h_sw = false;
            if (!isSad) s_sw = false;
            if (!isUnsure) u_sw = false;
        }
    }
}
