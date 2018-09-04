using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SoliGameController;

namespace SoliSoundScape
{
    public class SoliSoundscapeLvl1 : MonoBehaviour
    {
        public GameController game_c;

        ChordProgressions cp;
        SoliSoundscapeLvl2 lvl2;

        public int lvl1State, unsureRandom;
        private int lvl2State;
        public bool Happy, Sad, Unsure, prevH, curisHappy, prevS, curisSad, prevU, curisUnsure, h_trainSW, s_trainSW;
        public bool h_sw, s_sw, u_sw;
        public string[] lvl1Array = new string[3];
        public bool isHappy, isSad, isUnsure, pickKey;
        public int startKey, gameState;
        // Use this for initialization
        void Start()
        {
            cp = this.GetComponent<ChordProgressions>();
            lvl2 = this.GetComponent<SoliSoundscapeLvl2>();

            lvl1State = 0;
        }

        // Update is called once per frame
        void Update()
        {
            isHappy = game_c.Happy;
            isSad = game_c.Sad;
            isUnsure = game_c.uHeld_Reached;
            gameState = game_c.state;

            randomKey();

            lvl2State = lvl2.lvl2State;

            if (lvl2State == -1 && lvl2.changeInstrument)
            {
                lvl1State = 0;
                lvl2.changeInstrument = false;
                lvl2State = -1;
            }

            e_states();
            Lvl1ChordStates();
            e_off();
            e_sw();

            if (lvl1State <= 3)
            {

            }

        }

        void randomKey()
        {
            if (gameState == 0 && !pickKey) // KEY
            {
                startKey = Random.Range(1, 12);

                if (startKey == 1) cp.Key = "C";
                if (startKey == 2) cp.Key = "Db";
                if (startKey == 3) cp.Key = "D";
                if (startKey == 4) cp.Key = "Eb";
                if (startKey == 5) cp.Key = "E";
                if (startKey == 6) cp.Key = "F";
                if (startKey == 7) cp.Key = "Gb";
                if (startKey == 8) cp.Key = "G";
                if (startKey == 9) cp.Key = "Ab";
                if (startKey == 10) cp.Key = "A";
                if (startKey == 11) cp.Key = "Bb";
                if (startKey == 12) cp.Key = "B";

                pickKey = true;
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

        public void Lvl1ChordStates()
        {
            if (gameState == -1)
            {
                cp.ChordVoicing = "";
                cp.ChordType = "";

                if (lvl1State == 0) // START LEVEL 1 MAJOR OR MINOR
                {
                    if (Happy && !curisHappy)
                    {
                        cp.KeyType = "Major";
                        curisHappy = true;
                        prevH = true;
                        cp.chords[1] = true;
                    }
                    else if (Sad && !curisSad)
                    {
                        cp.KeyType = "NaturalMinor";
                        curisSad = true;
                        prevS = true;
                        cp.chords[1] = true;
                    }
                    else if (Unsure && !curisUnsure) //if unsure either major or minor
                    {
                        unsureRandom = Random.Range(0, 1);
                        curisUnsure = true;
                        prevU = true;

                        if (unsureRandom == 0)
                        {
                            cp.KeyType = "Major";
                            cp.chords[1] = true;
                        }
                        else
                        {
                            cp.KeyType = "NaturalMinor";
                            cp.chords[1] = true;
                        }
                    }

                    if (curisHappy && !Happy)
                    {
                        curisHappy = false;
                        lvl1State++;
                    }
                    else if (curisSad && !Sad)
                    {
                        curisSad = false;
                        lvl1State++;
                    }
                    else if (curisUnsure && !Unsure)
                    {
                        curisUnsure = false;
                        lvl1State++;
                    }

                }
                else if (lvl1State == 1) // LVL 1.1 
                {
                    if (Happy && !curisHappy)
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

                        curisHappy = true;
                        prevH = true;

                        prevS = false;
                        prevU = false;
                        lvl1Array[0] = "H";
                    }
                    else if (Sad && !curisSad)
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

                        curisSad = true;
                        prevS = true;

                        prevH = false;
                        prevU = false;
                        lvl1Array[0] = "S";
                    }
                    else if (Unsure)
                    {
                        cp.chords[5] = true;

                        curisUnsure = true;
                        prevU = true;

                        prevH = false;
                        prevS = false;
                        lvl1Array[0] = "U";
                    }

                    if (curisHappy && !Happy)
                    {
                        curisHappy = false;
                        lvl1State++;
                    }
                    else if (curisSad && !Sad)
                    {
                        curisSad = false;
                        lvl1State++;
                    }
                    else if (curisUnsure && !Unsure)
                    {
                        curisUnsure = false;
                        lvl1State++;
                    }
                }
                else if (lvl1State == 2) //LVL 1.2
                {
                    if (Happy && !curisHappy)
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

                        curisHappy = true;
                        prevH = true;

                        prevS = false;
                        prevU = false;
                        lvl1Array[1] = "H";
                    }
                    else if (Sad && !curisSad)
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

                        curisSad = true;
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


                        curisUnsure = true;
                        prevU = true;

                        prevH = false;
                        prevS = false;
                        lvl1Array[1] = "U";
                    }

                    if (curisHappy && !Happy)
                    {
                        curisHappy = false;
                        lvl1State++;
                    }
                    else if (curisSad && !Sad)
                    {
                        curisSad = false;
                        lvl1State++;
                    }
                    else if (curisUnsure && !Unsure)
                    {
                        curisUnsure = false;
                        lvl1State++;
                    }
                }
                else if (lvl1State == 3) //LVL 1.3
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

                        curisHappy = true;
                        prevH = true;

                        prevS = false;
                        prevU = false;
                        lvl1Array[2] = "H";
                    }
                    else if (Sad && !curisSad)
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

                        curisSad = true;
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


                        curisUnsure = true;
                        prevU = true;

                        prevH = false;
                        prevS = false;
                        lvl1Array[2] = "U";
                    }

                    if (curisHappy && !Happy)
                    {
                        curisHappy = false;
                        Happy = true;
                        lvl1State++;
                    }
                    else if (curisSad && !Sad)
                    {
                        curisSad = false;
                        Sad = true;
                        lvl1State++;
                    }
                    else if (curisUnsure && !Unsure)
                    {
                        curisUnsure = false;
                        Unsure = true;
                        lvl1State++;
                    }
                }
                else if (lvl1State == 4) //LVL 1.4
                {
                    if (cp.KeyType == "Major")
                    {
                        //Return back to LEVEL1
                        if (lvl1Array[0] == "H" && lvl1Array[1] == "U" && lvl1Array[2] == "H" || lvl1Array[0] == "S" && lvl1Array[1] == "U" && lvl1Array[2] == "H" || lvl1Array[0] == "U" && lvl1Array[1] == "U" && lvl1Array[2] == "H")
                        {
                            lvl1State = 0;
                        }
                        else if (lvl1Array[0] == "S" && lvl1Array[1] == "U" && lvl1Array[2] == "U" || lvl1Array[0] == "H" && lvl1Array[1] == "U" && lvl1Array[2] == "U" || lvl1Array[0] == "U" && lvl1Array[1] == "U" && lvl1Array[2] == "U")
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
            }
            else if (gameState >= 2 && gameState <= 4)
            {
                if (isHappy && !h_trainSW)
                {
                    cp.ChordVoicing = "";
                    cp.ChordType = "";
                    cp.KeyType = "Major";
                    cp.chords[1] = true;
                    h_trainSW = true;
                    Debug.Log("happychord");
                }
                else if (isSad && !s_trainSW)
                {
                    cp.ChordVoicing = "";
                    cp.ChordType = "";
                    cp.KeyType = "NaturalMinor";
                    cp.chords[1] = true;
                    s_trainSW = true;
                    Debug.Log("sadchord");
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
