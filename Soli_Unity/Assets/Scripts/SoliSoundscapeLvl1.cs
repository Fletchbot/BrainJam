using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoliSoundScape
{
    public class SoliSoundscapeLvl1 : MonoBehaviour
    {
        ChordProgressions cp;
        ChordSelector cs;
        SoliSoundscapeLvl2 lvl2;

        public int lvl1State, unsureRandom;
        private int lvl2State;
        private bool Happy, Sad, Unsure, prevH, currH, prevS, currS, prevU, currU, h_trainSW, s_trainSW;
        private string[] lvl1Array = new string[3];

        // Use this for initialization
        void Start()
        {
            cp = this.GetComponent<ChordProgressions>();
            cs = this.GetComponent<ChordSelector>();
            lvl2 = this.GetComponent<SoliSoundscapeLvl2>();

            lvl1State = 0;

        }

        // Update is called once per frame
        void Update()
        {
            Happy = cs.Happy;
            Sad = cs.Sad;
            Unsure = cs.Unsure;
            lvl2State = lvl2.lvl2State;

            if (lvl2State == -1 && lvl2.changeInstrument)
            {
                lvl1State = 0;
                lvl2.changeInstrument = false;
            }
        }

        public void Lvl1ChordStates()
        {
            if (cs.gameState == -1)
            {
                if (lvl1State == 0) // START LEVEL 1 MAJOR OR MINOR
                {
                    if (Happy && !currH)
                    {
                        cp.KeyType = "Major";
                        currH = true;
                        prevH = true;

                    }
                    else if (Sad && !currS)
                    {
                        cp.KeyType = "NaturalMinor";
                        currS = true;
                        prevS = true;
                    }
                    else if (Unsure && !currU) //if unsure either major or minor
                    {
                        unsureRandom = Random.Range(0, 1);
                        currU = true;
                        prevU = true;

                        if (unsureRandom == 0)
                        {
                            cp.KeyType = "Major";
                        }
                        else
                        {
                            cp.KeyType = "NaturalMinor";
                        }
                    }

                    if (currH && !Happy)
                    {
                        currH = false;
                        lvl1State++;
                    }
                    else if (currS && !Sad)
                    {
                        currS = false;
                        lvl1State++;
                    }
                    else if (currU && !Unsure)
                    {
                        currU = false;
                        lvl1State++;
                    }
                    cp.ChordVoicing = "";
                    cp.ChordType = "";
                    cp.chords[1] = true;

                }
                else if (lvl1State == 1) // LVL 1.1 
                {
                    if (Happy && !currH)
                    {
                        if (prevH) cp.chords[4] = true;
                        if (prevS) cp.chords[6] = true;

                        if (prevU && cp.KeyType == "Major")
                        {
                            cp.chords[4] = true;
                        }
                        else if (prevU && cp.KeyType == "NaturalMinor")
                        {
                            cp.chords[6] = true;
                        }

                        currH = true;
                        prevH = true;

                        prevS = false;
                        prevU = false;
                        lvl1Array[0] = "H";
                    }
                    else if (Sad && !currS)
                    {
                        if (prevH) cp.chords[6] = true;
                        if (prevS) cp.chords[4] = true;

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
                        lvl1Array[0] = "S";
                    }
                    else if (Unsure)
                    {
                        cp.chords[5] = true;

                        currU = true;
                        prevU = true;

                        prevH = false;
                        prevS = false;
                        lvl1Array[0] = "U";
                    }

                    if (currH && !Happy)
                    {
                        currH = false;
                        lvl1State++;
                    }
                    else if (currS && !Sad)
                    {
                        currS = false;
                        lvl1State++;
                    }
                    else if (currU && !Unsure)
                    {
                        currU = false;
                        lvl1State++;
                    }
                }
                else if (lvl1State == 2) //LVL 1.2
                {
                    if (Happy && !currH)
                    {
                        if (prevH && cp.KeyType == "Major")
                        {
                            cp.chords[1] = true;
                        }
                        else if (prevH && cp.KeyType == "NaturalMinor")
                        {
                            cp.chords[7] = true;
                        }

                        if (prevS && cp.KeyType == "Major")
                        {
                            cp.chords[4] = true;
                        }
                        else if (prevS && cp.KeyType == "NaturalMinor")
                        {
                            int c = Random.Range(0, 1);
                            if (c == 0) cp.chords[7] = true;
                            if (c == 1) cp.chords[5] = true;
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
                        lvl1Array[1] = "H";
                    }
                    else if (Sad && !currS)
                    {
                        if (prevH && cp.KeyType == "Major")
                        {
                            int c = Random.Range(0, 1);
                            if (c == 0) cp.chords[6] = true;
                            if (c == 1) cp.chords[2] = true;
                        }
                        else if (prevH && cp.KeyType == "NaturalMinor")
                        {
                            cp.chords[4] = true;
                        }

                        if (prevS) cp.chords[2] = true;

                        if (prevU && cp.KeyType == "Major")
                        {
                            cp.chords[6] = true;
                        }
                        else if (prevU && cp.KeyType == "NaturalMinor")
                        {
                            int c = Random.Range(0, 1);
                            if (c == 0) cp.chords[4] = true;
                            if (c == 1) cp.chords[2] = true;
                        }

                        currS = true;
                        prevS = true;

                        prevH = false;
                        prevU = false;
                        lvl1Array[1] = "S";
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
                        lvl1Array[1] = "U";
                    }

                    if (currH && !Happy)
                    {
                        currH = false;
                        lvl1State++;
                    }
                    else if (currS && !Sad)
                    {
                        currS = false;
                        lvl1State++;
                    }
                    else if (currU && !Unsure)
                    {
                        currU = false;
                        lvl1State++;
                    }
                }
                else if (lvl1State == 3) //LVL 1.3
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
                        lvl1Array[2] = "H";
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
                        lvl1Array[2] = "S";
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
                        lvl1Array[2] = "U";
                    }

                    if (currH && !Happy)
                    {
                        currH = false;
                        lvl1State++;
                    }
                    else if (currS && !Sad)
                    {
                        currS = false;
                        lvl1State++;
                    }
                    else if (currU && !Unsure)
                    {
                        currU = false;
                        lvl1State++;
                    }
                }
                else if (lvl1State == 4) //LVL 1.4
                {

                    if (cp.KeyType == "Major")
                    {
                        //Return back to LEVEL1
                        if (lvl1Array[0] == "H" && lvl1Array[1] == "U" && lvl1Array[2] == "H" || lvl1Array[0] == "H" && lvl1Array[1] == "U" && lvl1Array[2] == "U")
                        {
                            lvl1State = 0;
                        }
                        else if (lvl1Array[0] == "S" && lvl1Array[1] == "U" && lvl1Array[2] == "H" || lvl1Array[0] == "S" && lvl1Array[1] == "U" && lvl1Array[2] == "U")
                        {
                            lvl1State = 0;
                        }
                        else if (lvl1Array[0] == "U" && lvl1Array[1] == "U" && lvl1Array[2] == "U" || lvl1Array[0] == "U" && lvl1Array[1] == "U" && lvl1Array[2] == "H")
                        {
                            lvl1State = 0;
                        }
                        //If sad go to minor i
                        else if (lvl1Array[0] == "S" && lvl1Array[1] == "S" && lvl1Array[2] == "S" && Sad || lvl1Array[0] == "H" && lvl1Array[1] == "S" && lvl1Array[2] == "S" && Sad || lvl1Array[0] == "S" && lvl1Array[1] == "H" && lvl1Array[2] == "S" && Sad || lvl1Array[0] == "S" && lvl1Array[1] == "S" && lvl1Array[2] == "H" && Sad)
                        {
                            cp.KeyType = "NaturalMinor";
                            lvl1State = -1;
                        }
                        else if (lvl1Array[0] == "U" && lvl1Array[1] == "S" && lvl1Array[2] == "S" && Sad || lvl1Array[0] == "S" && lvl1Array[1] == "U" && lvl1Array[2] == "S" && Sad || lvl1Array[0] == "S" && lvl1Array[1] == "S" && lvl1Array[2] == "U" && Sad)
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
                        if (lvl1Array[0] == "H" && lvl1Array[1] == "H" && lvl1Array[2] == "U" || lvl1Array[0] == "H" && lvl1Array[1] == "S" && lvl1Array[2] == "U")
                        {
                            lvl1State = 0;
                        }
                        else if (lvl1Array[0] == "S" && lvl1Array[1] == "H" && lvl1Array[2] == "U" || lvl1Array[0] == "S" && lvl1Array[1] == "S" && lvl1Array[2] == "U")
                        {
                            lvl1State = 0;
                        }
                        else if (lvl1Array[0] == "U" && lvl1Array[1] == "H" && lvl1Array[2] == "U" || lvl1Array[0] == "U" && lvl1Array[1] == "S" && lvl1Array[2] == "U")
                        {
                            lvl1State = 0;
                        }
                        //If Happy go to major I
                        else if (lvl1Array[0] == "H" && lvl1Array[1] == "H" && lvl1Array[2] == "H" && Happy || lvl1Array[0] == "S" && lvl1Array[1] == "H" && lvl1Array[2] == "H" && Happy || lvl1Array[0] == "H" && lvl1Array[1] == "S" && lvl1Array[2] == "H" && Happy || lvl1Array[0] == "H" && lvl1Array[1] == "H" && lvl1Array[2] == "S" && Happy)
                        {
                            cp.KeyType = "Major";
                            lvl1State = -1;
                        }
                        else if (lvl1Array[0] == "U" && lvl1Array[1] == "H" && lvl1Array[2] == "H" && Happy || lvl1Array[0] == "H" && lvl1Array[1] == "U" && lvl1Array[2] == "H" && Happy || lvl1Array[0] == "H" && lvl1Array[1] == "H" && lvl1Array[2] == "U" && Happy)
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
                else if (cs.gameState == 2 && cs.gameState == 3)
                {
                    if (Happy && !h_trainSW)
                    {
                        cp.ChordVoicing = "";
                        cp.ChordType = "";
                        cp.KeyType = "Major";
                        cp.chords[1] = true;
                        h_trainSW = true;
                    }
                    else if (Sad && !s_trainSW)
                    {
                        cp.ChordVoicing = "";
                        cp.ChordType = "";
                        cp.KeyType = "NaturalMinor";
                        cp.chords[1] = true;
                        s_trainSW = true;
                    }
                }
            }
        }
    }
}
