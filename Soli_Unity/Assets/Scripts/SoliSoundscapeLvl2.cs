using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoliSoundScape
{
    public class SoliSoundscapeLvl2 : MonoBehaviour
    {
        ChordProgressions cp;
        ChordSelector cs;
        SoliSoundscapeLvl1 lvl1;

        public bool Happy, Sad, Unsure, prevH, currH, prevS, currS, prevU, currU, h_trainSW, s_trainSW;
        public int lvl1State, lvl2State, unsureRandom;
        private string[] lvl2Array = new string[3];

        // Use this for initialization
        void Start()
        {
            cp = this.GetComponent<ChordProgressions>();
            cs = this.GetComponent<ChordSelector>();
            lvl1 = this.GetComponent<SoliSoundscapeLvl1>();


        }

        // Update is called once per frame
        void Update()
        {
            lvl1State = lvl1.lvl1State;

            Happy = cs.Happy;
            Sad = cs.Sad;
            Unsure = cs.Unsure;

            if (lvl1State >= 0)
            {
                lvl2State = -1;
            }
            else if (lvl1State == -1 && lvl2State == -1)
            {
                lvl2State = 0;
            }          
        }
        private void MajorSW()
        {
            cp.KeyType = "Major";
        }
        private void NaturalMinorSW()
        {
            cp.KeyType = "NaturalMinor";
        }
        private void DiatonicSW()
        {
            cp.ChordType = "";
        }

        public void Lvl2ChordStates()
        {
            if (cs.gameState == -1)
            {
                if (lvl2State == 0) // START LEVEL 2 MAJOR OR MINOR
                {
                    if (Happy && !currH)
                    {
                        currH = true;
                        prevH = true;
                    }
                    else if (Sad && !currS)
                    {
                        currS = true;
                        prevS = true;
                    }
                    else if (Unsure && !currU) 
                    {
                        currU = true;
                        prevU = true;
                    }

                    if (currH && !Happy)
                    {
                        currH = false;
                        lvl2State++;
                    }
                    else if (currS && !Sad)
                    {
                        currS = false;
                        lvl2State++;
                    }
                    else if (currU && !Unsure)
                    {
                        currU = false;
                        lvl2State++;
                    }
                    cp.ChordVoicing = "Extended";
                    cp.ChordType = "";
                    cp.chords[1] = true;

                }
                else if (lvl2State == 1) // LVL 2.1 
                {
                    if (Happy && !currH)
                    {
                        if (prevH && cp.KeyType == "Major")
                        {
                            cp.KeyType = "NaturalMinor";
                            cp.ChordType = "";
                            cp.chords[6] = true;
                            Invoke("MajorSW", 1.0f);
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
                            Invoke("MajorSW", 1.0f);
                            Invoke("DiatonicSW", 1.0f);
                        }
                        else if (prevU && cp.KeyType == "NaturalMinor")
                        {
                            cp.chords[7] = true;
                        }
                        currH = true;
                        prevH = true;

                        prevS = false;
                        prevU = false;
                        lvl2Array[0] = "H";
                    }
                    else if (Sad && !currS)
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

                        currS = true;
                        prevS = true;

                        prevH = false;
                        prevU = false;
                        lvl2Array[0] = "S";
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
                            currU = true;
                            prevU = true;

                            prevH = false;
                            prevS = false;
                            lvl2Array[0] = "U";
                        }

                    if (currH && !Happy)
                    {
                        currH = false;
                        lvl2State++;
                    }
                    else if (currS && !Sad)
                    {
                        currS = false;
                        lvl2State++;
                    }
                    else if (currU && !Unsure)
                    {
                        currU = false;
                        lvl2State++;
                    }
                }
                else if (lvl2State == 2) //LVL 2.2
                {
                    if (Happy && !currH)
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
                            Invoke("MajorSW", 1.0f);
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
                            Invoke("NaturalMinorSW", 1.0f);
                            Invoke("DiatonicSW", 1.0f);
                        }

                        currH = true;
                        prevH = true;

                        prevS = false;
                        prevU = false;
                        lvl2Array[1] = "H";
                    }
                    else if (Sad && !currS)
                    {
                        if (prevH && cp.KeyType == "Major")
                        {
                            cp.KeyType = "NaturalMinor";
                            cp.ChordType = "NonDiatonic";
                            cp.chords[3] = true;
                            Invoke("MajorSW", 1.0f);
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

                        }
                        else if (prevS && cp.KeyType == "NaturalMinor")
                        {

                        }

                        if (prevU && cp.KeyType == "Major")
                        {

                        }
                        else if (prevU && cp.KeyType == "NaturalMinor")
                        {

                        }

                        currS = true;
                        prevS = true;

                        prevH = false;
                        prevU = false;
                        lvl2Array[1] = "S";
                    }
                    else if (Unsure)
                    {
                        if (prevH && cp.KeyType == "Major")
                        {

                        }
                        else if (prevH && cp.KeyType == "NaturalMinor")
                        {

                        }

                        if (prevS && cp.KeyType == "Major")
                        {

                        }
                        else if (prevS && cp.KeyType == "NaturalMinor")
                        {

                        }

                        if (prevU && cp.KeyType == "Major")
                        {

                        }
                        else if (prevU && cp.KeyType == "NaturalMinor")
                        {

                        }

                        currU = true;
                        prevU = true;

                        prevH = false;
                        prevS = false;
                        lvl2Array[1] = "U";
                    }

                    if (currH && !Happy)
                    {
                        currH = false;
                        lvl2State++;
                    }
                    else if (currS && !Sad)
                    {
                        currS = false;
                        lvl2State++;
                    }
                    else if (currU && !Unsure)
                    {
                        currU = false;
                        lvl2State++;
                    }
                }
                else if (lvl2State == 3) //LVL 2.3
                {
                    if (Happy && !currH)
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
                            cp.chords[5] = true;
                        }
                        else if (prevS && cp.KeyType == "NaturalMinor")
                        {
                            cp.chords[7] = true;
                        }

                        if (prevU && cp.KeyType == "Major")
                        {
                            cp.chords[1] = true;
                        }
                        else if (prevU && cp.KeyType == "NaturalMinor")
                        {
                            cp.chords[7] = true;
                        }

                        currH = true;
                        prevH = true;

                        prevS = false;
                        prevU = false;
                        lvl2Array[2] = "H";
                    }
                    else if (Sad && !currS)
                    {
                        if (prevH && cp.KeyType == "Major")
                        {
                            cp.chords[6] = true;
                        }
                        else if (prevH && cp.KeyType == "NaturalMinor")
                        {
                            cp.chords[4] = true;
                        }

                        if (prevS && cp.KeyType == "Major")
                        {
                            cp.chords[7] = true;
                        }
                        else if (prevS && cp.KeyType == "NaturalMinor")
                        {
                            cp.chords[5] = true;
                        }

                        if (prevU && cp.KeyType == "Major")
                        {
                            cp.chords[6] = true;
                        }
                        else if (prevU && cp.KeyType == "NaturalMinor")
                        {
                            cp.chords[4] = true;

                        }

                        currS = true;
                        prevS = true;

                        prevH = false;
                        prevU = false;
                        lvl2Array[2] = "S";
                    }
                    else if (Unsure)
                    {
                        if (prevH && cp.KeyType == "Major")
                        {
                            cp.chords[5] = true;
                        }
                        else if (prevH && cp.KeyType == "NaturalMinor")
                        {
                            cp.chords[3] = true;
                        }

                        if (prevS && cp.KeyType == "Major")
                        {
                            cp.chords[4] = true;
                        }
                        else if (prevS && cp.KeyType == "NaturalMinor")
                        {
                            cp.chords[1] = true;
                        }

                        if (prevU && cp.KeyType == "Major")
                        {
                            cp.chords[4] = true;
                        }
                        else if (prevU && cp.KeyType == "NaturalMinor")
                        {
                            cp.chords[1] = true;
                        }


                        currU = true;
                        prevU = true;

                        prevH = false;
                        prevS = false;
                        lvl2Array[2] = "U";
                    }

                    if (currH && !Happy)
                    {
                        currH = false;
                        lvl2State++;
                    }
                    else if (currS && !Sad)
                    {
                        currS = false;
                        lvl2State++;
                    }
                    else if (currU && !Unsure)
                    {
                        currU = false;
                        lvl2State++;
                    }
                }
                else if (lvl2State == 4) //LVL 2.4
                {

                    if (cp.KeyType == "Major")
                    {
                        //Return back to LEVEL1
                        if (lvl2Array[0] == "H" && lvl2Array[1] == "U" && lvl2Array[2] == "H" || lvl2Array[0] == "H" && lvl2Array[1] == "U" && lvl2Array[2] == "U")
                        {
                            lvl1State = 0;
                        }
                        else if (lvl2Array[0] == "S" && lvl2Array[1] == "U" && lvl2Array[2] == "H" || lvl2Array[0] == "S" && lvl2Array[1] == "U" && lvl2Array[2] == "U")
                        {
                            lvl1State = 0;
                        }
                        else if (lvl2Array[0] == "U" && lvl2Array[1] == "U" && lvl2Array[2] == "U" || lvl2Array[0] == "U" && lvl2Array[1] == "U" && lvl2Array[2] == "H")
                        {
                            lvl1State = 0;
                        }
                        //If sad go to minor i
                        else if (lvl2Array[0] == "S" && lvl2Array[1] == "S" && lvl2Array[2] == "S" && Sad || lvl2Array[0] == "H" && lvl2Array[1] == "S" && lvl2Array[2] == "S" && Sad || lvl2Array[0] == "S" && lvl2Array[1] == "H" && lvl2Array[2] == "S" && Sad || lvl2Array[0] == "S" && lvl2Array[1] == "S" && lvl2Array[2] == "H" && Sad)
                        {
                            cp.KeyType = "NaturalMinor";
                            lvl1State = -1;
                        }
                        else if (lvl2Array[0] == "U" && lvl2Array[1] == "S" && lvl2Array[2] == "S" && Sad || lvl2Array[0] == "S" && lvl2Array[1] == "U" && lvl2Array[2] == "S" && Sad || lvl2Array[0] == "S" && lvl2Array[1] == "S" && lvl2Array[2] == "U" && Sad)
                        {
                            cp.KeyType = "NaturalMinor";
                            lvl1State = -1;
                        }
                        else
                        {
                            lvl1State = -1;
                        }
                    }
                    else if (cp.KeyType == "NaturalMinor")
                    {
                        //Return back to LEVEL1
                        if (lvl2Array[0] == "H" && lvl2Array[1] == "H" && lvl2Array[2] == "U" || lvl2Array[0] == "H" && lvl2Array[1] == "S" && lvl2Array[2] == "U")
                        {
                            lvl1State = 0;
                        }
                        else if (lvl2Array[0] == "S" && lvl2Array[1] == "H" && lvl2Array[2] == "U" || lvl2Array[0] == "S" && lvl2Array[1] == "S" && lvl2Array[2] == "U")
                        {
                            lvl1State = 0;
                        }
                        else if (lvl2Array[0] == "U" && lvl2Array[1] == "H" && lvl2Array[2] == "U" || lvl2Array[0] == "U" && lvl2Array[1] == "S" && lvl2Array[2] == "U")
                        {
                            lvl1State = 0;
                        }
                        //If Happy go to major I
                        else if (lvl2Array[0] == "H" && lvl2Array[1] == "H" && lvl2Array[2] == "H" && Happy || lvl2Array[0] == "S" && lvl2Array[1] == "H" && lvl2Array[2] == "H" && Happy || lvl2Array[0] == "H" && lvl2Array[1] == "S" && lvl2Array[2] == "H" && Happy || lvl2Array[0] == "H" && lvl2Array[1] == "H" && lvl2Array[2] == "S" && Happy)
                        {
                            cp.KeyType = "Major";
                            lvl1State = -1;
                        }
                        else if (lvl2Array[0] == "U" && lvl2Array[1] == "H" && lvl2Array[2] == "H" && Happy || lvl2Array[0] == "H" && lvl2Array[1] == "U" && lvl2Array[2] == "H" && Happy || lvl2Array[0] == "H" && lvl2Array[1] == "H" && lvl2Array[2] == "U" && Happy)
                        {
                            cp.KeyType = "Major";
                            lvl1State = -1;
                        }
                        else
                        {
                            lvl1State = -1;
                        }
                    }
                }
            }
        }
    }
}
