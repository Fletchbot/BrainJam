using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoliSoundScape
{
    public class DiatonicScales : MonoBehaviour
    {
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

        public int[] CmidiNotes, DbmidiNotes, DmidiNotes, EbmidiNotes, EmidiNotes, FmidiNotes, GbmidiNotes, GmidiNotes, AbmidiNotes, AmidiNotes, BbmidiNotes, BmidiNotes;
        public int[] Major_Scale1 = new int[8], Major_Scale2 = new int[8], Major_Scale3 = new int[8], Major_Scale4 = new int[8], Major_Scale5 = new int[8];
        public int[] NatMinor_Scale1 = new int[8], NatMinor_Scale2 = new int[8], NatMinor_Scale3 = new int[8], NatMinor_Scale4 = new int[8], NatMinor_Scale5 = new int[8];

        // Use this for initialization
        public DiatonicScales()
        {
            MidiNotesSetup();
        }


        public void MajorScales(string Key)
        {
            if (Key == "C")
            {
                Major_Scale1[0] = 0;
                Major_Scale1[1] = CmidiNotes[0];
                Major_Scale1[2] = DmidiNotes[0];
                Major_Scale1[3] = EmidiNotes[0];
                Major_Scale1[4] = FmidiNotes[0];
                Major_Scale1[5] = GmidiNotes[0];
                Major_Scale1[6] = AmidiNotes[0];
                Major_Scale1[7] = BmidiNotes[0];

                Major_Scale2[0] = 0;
                Major_Scale2[1] = CmidiNotes[1];
                Major_Scale2[2] = DmidiNotes[1];
                Major_Scale2[3] = EmidiNotes[1];
                Major_Scale2[4] = FmidiNotes[1];
                Major_Scale2[5] = GmidiNotes[1];
                Major_Scale2[6] = AmidiNotes[1];
                Major_Scale2[7] = BmidiNotes[1];

                Major_Scale3[0] = 0;
                Major_Scale3[1] = CmidiNotes[2];
                Major_Scale3[2] = DmidiNotes[2];
                Major_Scale3[3] = EmidiNotes[2];
                Major_Scale3[4] = FmidiNotes[2];
                Major_Scale3[5] = GmidiNotes[2];
                Major_Scale3[6] = AmidiNotes[2];
                Major_Scale3[7] = BmidiNotes[2];

                Major_Scale4[0] = 0;
                Major_Scale4[1] = CmidiNotes[3];
                Major_Scale4[2] = DmidiNotes[3];
                Major_Scale4[3] = EmidiNotes[3];
                Major_Scale4[4] = FmidiNotes[3];
                Major_Scale4[5] = GmidiNotes[3];
                Major_Scale4[6] = AmidiNotes[3];
                Major_Scale4[7] = BmidiNotes[3];

                Major_Scale5[0] = 0;
                Major_Scale5[1] = CmidiNotes[4];
                Major_Scale5[2] = DmidiNotes[4];
                Major_Scale5[3] = EmidiNotes[4];
                Major_Scale5[4] = FmidiNotes[4];
                Major_Scale5[5] = GmidiNotes[4];
                Major_Scale5[6] = AmidiNotes[4];
                Major_Scale5[7] = BmidiNotes[4];
            }
            else if (Key == "Db")
            {
                Major_Scale1[0] = 0;
                Major_Scale1[1] = DbmidiNotes[0];
                Major_Scale1[2] = EbmidiNotes[0];
                Major_Scale1[3] = FmidiNotes[0];
                Major_Scale1[4] = GbmidiNotes[0];
                Major_Scale1[5] = AbmidiNotes[0];
                Major_Scale1[6] = BbmidiNotes[0];
                Major_Scale1[7] = CmidiNotes[0];

                Major_Scale2[0] = 0;
                Major_Scale2[1] = DbmidiNotes[1];
                Major_Scale2[2] = EbmidiNotes[1];
                Major_Scale2[3] = FmidiNotes[1];
                Major_Scale2[4] = GbmidiNotes[1];
                Major_Scale2[5] = AbmidiNotes[1];
                Major_Scale2[6] = BbmidiNotes[1];
                Major_Scale2[7] = CmidiNotes[1];

                Major_Scale3[0] = 0;
                Major_Scale3[1] = DbmidiNotes[2];
                Major_Scale3[2] = EbmidiNotes[2];
                Major_Scale3[3] = FmidiNotes[2];
                Major_Scale3[4] = GbmidiNotes[2];
                Major_Scale3[5] = AbmidiNotes[2];
                Major_Scale3[6] = BbmidiNotes[2];
                Major_Scale3[7] = CmidiNotes[2];

                Major_Scale4[0] = 0;
                Major_Scale4[1] = DbmidiNotes[3];
                Major_Scale4[2] = EbmidiNotes[3];
                Major_Scale4[3] = FmidiNotes[3];
                Major_Scale4[4] = GbmidiNotes[3];
                Major_Scale4[5] = AbmidiNotes[3];
                Major_Scale4[6] = BbmidiNotes[3];
                Major_Scale4[7] = CmidiNotes[3];

                Major_Scale5[0] = 0;
                Major_Scale5[1] = DbmidiNotes[4];
                Major_Scale5[2] = EbmidiNotes[4];
                Major_Scale5[3] = FmidiNotes[4];
                Major_Scale5[4] = GbmidiNotes[4];
                Major_Scale5[5] = AbmidiNotes[4];
                Major_Scale5[6] = BbmidiNotes[4];
                Major_Scale5[7] = CmidiNotes[4];
            }
            else if (Key == "D")
            {
                Major_Scale1[0] = 0;
                Major_Scale1[1] = DmidiNotes[0];
                Major_Scale1[2] = EmidiNotes[0];
                Major_Scale1[3] = GbmidiNotes[0];
                Major_Scale1[4] = GmidiNotes[0];
                Major_Scale1[5] = AmidiNotes[0];
                Major_Scale1[6] = BmidiNotes[0];
                Major_Scale1[7] = DbmidiNotes[0];

                Major_Scale2[0] = 0;
                Major_Scale2[1] = DmidiNotes[1];
                Major_Scale2[2] = EmidiNotes[1];
                Major_Scale2[3] = GbmidiNotes[1];
                Major_Scale2[4] = GmidiNotes[1];
                Major_Scale2[5] = AmidiNotes[1];
                Major_Scale2[6] = BmidiNotes[1];
                Major_Scale2[7] = DbmidiNotes[1];

                Major_Scale3[0] = 0;
                Major_Scale3[1] = DmidiNotes[2];
                Major_Scale3[2] = EmidiNotes[2];
                Major_Scale3[3] = GbmidiNotes[2];
                Major_Scale3[4] = GmidiNotes[2];
                Major_Scale3[5] = AmidiNotes[2];
                Major_Scale3[6] = BmidiNotes[2];
                Major_Scale3[7] = DbmidiNotes[2];

                Major_Scale4[0] = 0;
                Major_Scale4[1] = DmidiNotes[3];
                Major_Scale4[2] = EmidiNotes[3];
                Major_Scale4[3] = GbmidiNotes[3];
                Major_Scale4[4] = GmidiNotes[3];
                Major_Scale4[5] = AmidiNotes[3];
                Major_Scale4[6] = BmidiNotes[3];
                Major_Scale4[7] = DbmidiNotes[3];

                Major_Scale5[0] = 0;
                Major_Scale5[1] = DmidiNotes[4];
                Major_Scale5[2] = EmidiNotes[4];
                Major_Scale5[3] = GbmidiNotes[4];
                Major_Scale5[4] = GmidiNotes[4];
                Major_Scale5[5] = AmidiNotes[4];
                Major_Scale5[6] = BmidiNotes[4];
                Major_Scale5[7] = DbmidiNotes[4];
            }
            else if (Key == "Eb")
            {
                Major_Scale1[0] = 0;
                Major_Scale1[1] = EbmidiNotes[0];
                Major_Scale1[2] = FmidiNotes[0];
                Major_Scale1[3] = GmidiNotes[0];
                Major_Scale1[4] = AbmidiNotes[0];
                Major_Scale1[5] = BbmidiNotes[0];
                Major_Scale1[6] = CmidiNotes[0];
                Major_Scale1[7] = DmidiNotes[0];

                Major_Scale2[0] = 0;
                Major_Scale2[1] = EbmidiNotes[1];
                Major_Scale2[2] = FmidiNotes[1];
                Major_Scale2[3] = GmidiNotes[1];
                Major_Scale2[4] = AbmidiNotes[1];
                Major_Scale2[5] = BbmidiNotes[1];
                Major_Scale2[6] = CmidiNotes[1];
                Major_Scale2[7] = DmidiNotes[1];

                Major_Scale3[0] = 0;
                Major_Scale3[1] = EbmidiNotes[2];
                Major_Scale3[2] = FmidiNotes[2];
                Major_Scale3[3] = GmidiNotes[2];
                Major_Scale3[4] = AbmidiNotes[2];
                Major_Scale3[5] = BbmidiNotes[2];
                Major_Scale3[6] = CmidiNotes[2];
                Major_Scale3[7] = DmidiNotes[2];

                Major_Scale4[0] = 0;
                Major_Scale4[1] = EbmidiNotes[3];
                Major_Scale4[2] = FmidiNotes[3];
                Major_Scale4[3] = GmidiNotes[3];
                Major_Scale4[4] = AbmidiNotes[3];
                Major_Scale4[5] = BbmidiNotes[3];
                Major_Scale4[6] = CmidiNotes[3];
                Major_Scale4[7] = DmidiNotes[3];

                Major_Scale5[0] = 0;
                Major_Scale5[1] = EbmidiNotes[4];
                Major_Scale5[2] = FmidiNotes[4];
                Major_Scale5[3] = GmidiNotes[4];
                Major_Scale5[4] = AbmidiNotes[4];
                Major_Scale5[5] = BbmidiNotes[4];
                Major_Scale5[6] = CmidiNotes[4];
                Major_Scale5[7] = DmidiNotes[4];
            }
            else if (Key == "E")
            {
                Major_Scale1[0] = 0;
                Major_Scale1[1] = EmidiNotes[0];
                Major_Scale1[2] = GbmidiNotes[0];
                Major_Scale1[3] = AbmidiNotes[0];
                Major_Scale1[4] = AmidiNotes[0];
                Major_Scale1[5] = BmidiNotes[0];
                Major_Scale1[6] = DbmidiNotes[0];
                Major_Scale1[7] = EbmidiNotes[0];

                Major_Scale2[0] = 0;
                Major_Scale2[1] = EmidiNotes[1];
                Major_Scale2[2] = GbmidiNotes[1];
                Major_Scale2[3] = AbmidiNotes[1];
                Major_Scale2[4] = AmidiNotes[1];
                Major_Scale2[5] = BmidiNotes[1];
                Major_Scale2[6] = DbmidiNotes[1];
                Major_Scale2[7] = EbmidiNotes[1];

                Major_Scale3[0] = 0;
                Major_Scale3[1] = EmidiNotes[2];
                Major_Scale3[2] = GbmidiNotes[2];
                Major_Scale3[3] = AbmidiNotes[2];
                Major_Scale3[4] = AmidiNotes[2];
                Major_Scale3[5] = BmidiNotes[2];
                Major_Scale3[6] = DbmidiNotes[2];
                Major_Scale3[7] = EbmidiNotes[2];

                Major_Scale4[0] = 0;
                Major_Scale4[1] = EmidiNotes[3];
                Major_Scale4[2] = GbmidiNotes[3];
                Major_Scale4[3] = AbmidiNotes[3];
                Major_Scale4[4] = AmidiNotes[3];
                Major_Scale4[5] = BmidiNotes[3];
                Major_Scale4[6] = DbmidiNotes[3];
                Major_Scale4[7] = EbmidiNotes[3];

                Major_Scale5[0] = 0;
                Major_Scale5[1] = EmidiNotes[4];
                Major_Scale5[2] = GbmidiNotes[4];
                Major_Scale5[3] = AbmidiNotes[4];
                Major_Scale5[4] = AmidiNotes[4];
                Major_Scale5[5] = BmidiNotes[4];
                Major_Scale5[6] = DbmidiNotes[4];
                Major_Scale5[7] = EbmidiNotes[4];
            }
            else if (Key == "F")
            {
                Major_Scale1[0] = 0;
                Major_Scale1[1] = FmidiNotes[0];
                Major_Scale1[2] = GmidiNotes[0];
                Major_Scale1[3] = AmidiNotes[0];
                Major_Scale1[4] = BbmidiNotes[0];
                Major_Scale1[5] = CmidiNotes[0];
                Major_Scale1[6] = DmidiNotes[0];
                Major_Scale1[7] = EmidiNotes[0];

                Major_Scale2[0] = 0;
                Major_Scale2[1] = FmidiNotes[1];
                Major_Scale2[2] = GmidiNotes[1];
                Major_Scale2[3] = AmidiNotes[1];
                Major_Scale2[4] = BbmidiNotes[1];
                Major_Scale2[5] = CmidiNotes[1];
                Major_Scale2[6] = DmidiNotes[1];
                Major_Scale2[7] = EmidiNotes[1];

                Major_Scale3[0] = 0;
                Major_Scale3[1] = FmidiNotes[2];
                Major_Scale3[2] = GmidiNotes[2];
                Major_Scale3[3] = AmidiNotes[2];
                Major_Scale3[4] = BbmidiNotes[2];
                Major_Scale3[5] = CmidiNotes[2];
                Major_Scale3[6] = DmidiNotes[2];
                Major_Scale3[7] = EmidiNotes[2];

                Major_Scale4[0] = 0;
                Major_Scale4[1] = FmidiNotes[3];
                Major_Scale4[2] = GmidiNotes[3];
                Major_Scale4[3] = AmidiNotes[3];
                Major_Scale4[4] = BbmidiNotes[3];
                Major_Scale4[5] = CmidiNotes[3];
                Major_Scale4[6] = DmidiNotes[3];
                Major_Scale4[7] = EmidiNotes[3];

                Major_Scale5[0] = 0;
                Major_Scale5[1] = FmidiNotes[4];
                Major_Scale5[2] = GmidiNotes[4];
                Major_Scale5[3] = AmidiNotes[4];
                Major_Scale5[4] = BbmidiNotes[4];
                Major_Scale5[5] = CmidiNotes[4];
                Major_Scale5[6] = DmidiNotes[4];
                Major_Scale5[7] = EmidiNotes[4];
            }
            else if (Key == "Gb")
            {
                Major_Scale1[0] = 0;
                Major_Scale1[1] = GbmidiNotes[0];
                Major_Scale1[2] = AbmidiNotes[0];
                Major_Scale1[3] = BbmidiNotes[0];
                Major_Scale1[4] = BmidiNotes[0];
                Major_Scale1[5] = DbmidiNotes[0];
                Major_Scale1[6] = EbmidiNotes[0];
                Major_Scale1[7] = FmidiNotes[0];

                Major_Scale2[0] = 0;
                Major_Scale2[1] = GbmidiNotes[1];
                Major_Scale2[2] = AbmidiNotes[1];
                Major_Scale2[3] = BbmidiNotes[1];
                Major_Scale2[4] = BmidiNotes[1];
                Major_Scale2[5] = DbmidiNotes[1];
                Major_Scale2[6] = EbmidiNotes[1];
                Major_Scale2[7] = FmidiNotes[1];

                Major_Scale3[0] = 0;
                Major_Scale3[1] = GbmidiNotes[2];
                Major_Scale3[2] = AbmidiNotes[2];
                Major_Scale3[3] = BbmidiNotes[2];
                Major_Scale3[4] = BmidiNotes[2];
                Major_Scale3[5] = DbmidiNotes[2];
                Major_Scale3[6] = EbmidiNotes[2];
                Major_Scale3[7] = FmidiNotes[2];

                Major_Scale4[0] = 0;
                Major_Scale4[1] = GbmidiNotes[3];
                Major_Scale4[2] = AbmidiNotes[3];
                Major_Scale4[3] = BbmidiNotes[3];
                Major_Scale4[4] = BmidiNotes[3];
                Major_Scale4[5] = DbmidiNotes[3];
                Major_Scale4[6] = EbmidiNotes[3];
                Major_Scale4[7] = FmidiNotes[3];

                Major_Scale5[0] = 0;
                Major_Scale5[1] = GbmidiNotes[4];
                Major_Scale5[2] = AbmidiNotes[4];
                Major_Scale5[3] = BbmidiNotes[4];
                Major_Scale5[4] = BmidiNotes[4];
                Major_Scale5[5] = DbmidiNotes[4];
                Major_Scale5[6] = EbmidiNotes[4];
                Major_Scale5[7] = FmidiNotes[4];
            }
            else if (Key == "G")
            {
                Major_Scale1[0] = 0;
                Major_Scale1[1] = GmidiNotes[0];
                Major_Scale1[2] = AmidiNotes[0];
                Major_Scale1[3] = BmidiNotes[0];
                Major_Scale1[4] = CmidiNotes[0];
                Major_Scale1[5] = DmidiNotes[0];
                Major_Scale1[6] = EmidiNotes[0];
                Major_Scale1[7] = GbmidiNotes[0];

                Major_Scale2[0] = 0;
                Major_Scale2[1] = GmidiNotes[1];
                Major_Scale2[2] = AmidiNotes[1];
                Major_Scale2[3] = BmidiNotes[1];
                Major_Scale2[4] = CmidiNotes[1];
                Major_Scale2[5] = DmidiNotes[1];
                Major_Scale2[6] = EmidiNotes[1];
                Major_Scale2[7] = GbmidiNotes[1];

                Major_Scale3[0] = 0;
                Major_Scale3[1] = GmidiNotes[2];
                Major_Scale3[2] = AmidiNotes[2];
                Major_Scale3[3] = BmidiNotes[2];
                Major_Scale3[4] = CmidiNotes[2];
                Major_Scale3[5] = DmidiNotes[2];
                Major_Scale3[6] = EmidiNotes[2];
                Major_Scale3[7] = GbmidiNotes[2];

                Major_Scale4[0] = 0;
                Major_Scale4[1] = GmidiNotes[3];
                Major_Scale4[2] = AmidiNotes[3];
                Major_Scale4[3] = BmidiNotes[3];
                Major_Scale4[4] = CmidiNotes[3];
                Major_Scale4[5] = DmidiNotes[3];
                Major_Scale4[6] = EmidiNotes[3];
                Major_Scale4[7] = GbmidiNotes[3];

                Major_Scale5[0] = 0;
                Major_Scale5[1] = GmidiNotes[4];
                Major_Scale5[2] = AmidiNotes[4];
                Major_Scale5[3] = BmidiNotes[4];
                Major_Scale5[4] = CmidiNotes[4];
                Major_Scale5[5] = DmidiNotes[4];
                Major_Scale5[6] = EmidiNotes[4];
                Major_Scale5[7] = GbmidiNotes[4];
            }
            else if (Key == "Ab")
            {
                Major_Scale1[0] = 0;
                Major_Scale1[1] = AbmidiNotes[0];
                Major_Scale1[2] = BbmidiNotes[0];
                Major_Scale1[3] = CmidiNotes[0];
                Major_Scale1[4] = DbmidiNotes[0];
                Major_Scale1[5] = EbmidiNotes[0];
                Major_Scale1[6] = FmidiNotes[0];
                Major_Scale1[7] = GmidiNotes[0];

                Major_Scale2[0] = 0;
                Major_Scale2[1] = AbmidiNotes[1];
                Major_Scale2[2] = BbmidiNotes[1];
                Major_Scale2[3] = CmidiNotes[1];
                Major_Scale2[4] = DbmidiNotes[1];
                Major_Scale2[5] = EbmidiNotes[1];
                Major_Scale2[6] = FmidiNotes[1];
                Major_Scale2[7] = GmidiNotes[1];

                Major_Scale3[0] = 0;
                Major_Scale3[1] = AbmidiNotes[2];
                Major_Scale3[2] = BbmidiNotes[2];
                Major_Scale3[3] = CmidiNotes[2];
                Major_Scale3[4] = DbmidiNotes[2];
                Major_Scale3[5] = EbmidiNotes[2];
                Major_Scale3[6] = FmidiNotes[2];
                Major_Scale3[7] = GmidiNotes[2];

                Major_Scale4[0] = 0;
                Major_Scale4[1] = AbmidiNotes[3];
                Major_Scale4[2] = BbmidiNotes[3];
                Major_Scale4[3] = CmidiNotes[3];
                Major_Scale4[4] = DbmidiNotes[3];
                Major_Scale4[5] = EbmidiNotes[3];
                Major_Scale4[6] = FmidiNotes[3];
                Major_Scale4[7] = GmidiNotes[3];

                Major_Scale5[0] = 0;
                Major_Scale5[1] = AbmidiNotes[4];
                Major_Scale5[2] = BbmidiNotes[4];
                Major_Scale5[3] = CmidiNotes[4];
                Major_Scale5[4] = DbmidiNotes[4];
                Major_Scale5[5] = EbmidiNotes[4];
                Major_Scale5[6] = FmidiNotes[4];
                Major_Scale5[7] = GmidiNotes[4];
            }
            else if (Key == "A")
            {
                Major_Scale1[0] = 0;
                Major_Scale1[1] = AmidiNotes[0];
                Major_Scale1[2] = BmidiNotes[0];
                Major_Scale1[3] = DbmidiNotes[0];
                Major_Scale1[4] = DmidiNotes[0];
                Major_Scale1[5] = EmidiNotes[0];
                Major_Scale1[6] = GbmidiNotes[0];
                Major_Scale1[7] = AbmidiNotes[0];

                Major_Scale2[0] = 0;
                Major_Scale2[1] = AmidiNotes[1];
                Major_Scale2[2] = BmidiNotes[1];
                Major_Scale2[3] = DbmidiNotes[1];
                Major_Scale2[4] = DmidiNotes[1];
                Major_Scale2[5] = EmidiNotes[1];
                Major_Scale2[6] = GbmidiNotes[1];
                Major_Scale2[7] = AbmidiNotes[1];

                Major_Scale3[0] = 0;
                Major_Scale3[1] = AmidiNotes[2];
                Major_Scale3[2] = BmidiNotes[2];
                Major_Scale3[3] = DbmidiNotes[2];
                Major_Scale3[4] = DmidiNotes[2];
                Major_Scale3[5] = EmidiNotes[2];
                Major_Scale3[6] = GbmidiNotes[2];
                Major_Scale3[7] = AbmidiNotes[2];

                Major_Scale4[0] = 0;
                Major_Scale4[1] = AmidiNotes[3];
                Major_Scale4[2] = BmidiNotes[3];
                Major_Scale4[3] = DbmidiNotes[3];
                Major_Scale4[4] = DmidiNotes[3];
                Major_Scale4[5] = EmidiNotes[3];
                Major_Scale4[6] = GbmidiNotes[3];
                Major_Scale4[7] = AbmidiNotes[3];

                Major_Scale5[0] = 0;
                Major_Scale5[1] = AmidiNotes[4];
                Major_Scale5[2] = BmidiNotes[4];
                Major_Scale5[3] = DbmidiNotes[4];
                Major_Scale5[4] = DmidiNotes[4];
                Major_Scale5[5] = EmidiNotes[4];
                Major_Scale5[6] = GbmidiNotes[4];
                Major_Scale5[7] = AbmidiNotes[4];
            }
            else if (Key == "Bb")
            {
                Major_Scale1[0] = 0;
                Major_Scale1[1] = BbmidiNotes[0];
                Major_Scale1[2] = CmidiNotes[0];
                Major_Scale1[3] = DmidiNotes[0];
                Major_Scale1[4] = EbmidiNotes[0];
                Major_Scale1[5] = FmidiNotes[0];
                Major_Scale1[6] = GmidiNotes[0];
                Major_Scale1[7] = AmidiNotes[0];

                Major_Scale2[0] = 0;
                Major_Scale2[1] = BbmidiNotes[1];
                Major_Scale2[2] = CmidiNotes[1];
                Major_Scale2[3] = DmidiNotes[1];
                Major_Scale2[4] = EbmidiNotes[1];
                Major_Scale2[5] = FmidiNotes[1];
                Major_Scale2[6] = GmidiNotes[1];
                Major_Scale2[7] = AmidiNotes[1];

                Major_Scale3[0] = 0;
                Major_Scale3[1] = BbmidiNotes[2];
                Major_Scale3[2] = CmidiNotes[2];
                Major_Scale3[3] = DmidiNotes[2];
                Major_Scale3[4] = EbmidiNotes[2];
                Major_Scale3[5] = FmidiNotes[2];
                Major_Scale3[6] = GmidiNotes[2];
                Major_Scale3[7] = AmidiNotes[2];

                Major_Scale4[0] = 0;
                Major_Scale4[1] = BbmidiNotes[3];
                Major_Scale4[2] = CmidiNotes[3];
                Major_Scale4[3] = DmidiNotes[3];
                Major_Scale4[4] = EbmidiNotes[3];
                Major_Scale4[5] = FmidiNotes[3];
                Major_Scale4[6] = GmidiNotes[3];
                Major_Scale4[7] = AmidiNotes[3];

                Major_Scale5[0] = 0;
                Major_Scale5[1] = BbmidiNotes[4];
                Major_Scale5[2] = CmidiNotes[4];
                Major_Scale5[3] = DmidiNotes[4];
                Major_Scale5[4] = EbmidiNotes[4];
                Major_Scale5[5] = FmidiNotes[4];
                Major_Scale5[6] = GmidiNotes[4];
                Major_Scale5[7] = AmidiNotes[4];
            }
            else if (Key == "B")
            {
                Major_Scale1[0] = 0;
                Major_Scale1[1] = BmidiNotes[0];
                Major_Scale1[2] = DbmidiNotes[0];
                Major_Scale1[3] = EbmidiNotes[0];
                Major_Scale1[4] = EmidiNotes[0];
                Major_Scale1[5] = GbmidiNotes[0];
                Major_Scale1[6] = AbmidiNotes[0];
                Major_Scale1[7] = BbmidiNotes[0];

                Major_Scale2[0] = 0;
                Major_Scale2[1] = BmidiNotes[1];
                Major_Scale2[2] = DbmidiNotes[1];
                Major_Scale2[3] = EbmidiNotes[1];
                Major_Scale2[4] = EmidiNotes[1];
                Major_Scale2[5] = GbmidiNotes[1];
                Major_Scale2[6] = AbmidiNotes[1];
                Major_Scale2[7] = BbmidiNotes[1];

                Major_Scale3[0] = 0;
                Major_Scale3[1] = BmidiNotes[2];
                Major_Scale3[2] = DbmidiNotes[2];
                Major_Scale3[3] = EbmidiNotes[2];
                Major_Scale3[4] = EmidiNotes[2];
                Major_Scale3[5] = GbmidiNotes[2];
                Major_Scale3[6] = AbmidiNotes[2];
                Major_Scale3[7] = BbmidiNotes[2];

                Major_Scale4[0] = 0;
                Major_Scale4[1] = BmidiNotes[3];
                Major_Scale4[2] = DbmidiNotes[3];
                Major_Scale4[3] = EbmidiNotes[3];
                Major_Scale4[4] = EmidiNotes[3];
                Major_Scale4[5] = GbmidiNotes[3];
                Major_Scale4[6] = AbmidiNotes[3];
                Major_Scale4[7] = BbmidiNotes[3];

                Major_Scale5[0] = 0;
                Major_Scale5[1] = BmidiNotes[4];
                Major_Scale5[2] = DbmidiNotes[4];
                Major_Scale5[3] = EbmidiNotes[4];
                Major_Scale5[4] = EmidiNotes[4];
                Major_Scale5[5] = GbmidiNotes[4];
                Major_Scale5[6] = AbmidiNotes[4];
                Major_Scale5[7] = BbmidiNotes[4];
            }

        }

        public void NatMinorScales(string Key)
        {
            if (Key == "C")
            {
                NatMinor_Scale1[0] = 0;
                NatMinor_Scale1[1] = CmidiNotes[0];
                NatMinor_Scale1[2] = DmidiNotes[0];
                NatMinor_Scale1[3] = EbmidiNotes[0];
                NatMinor_Scale1[4] = FmidiNotes[0];
                NatMinor_Scale1[5] = GmidiNotes[0];
                NatMinor_Scale1[6] = AbmidiNotes[0];
                NatMinor_Scale1[7] = BbmidiNotes[0];

                NatMinor_Scale2[0] = 0;
                NatMinor_Scale2[1] = CmidiNotes[1];
                NatMinor_Scale2[2] = DmidiNotes[1];
                NatMinor_Scale2[3] = EbmidiNotes[1];
                NatMinor_Scale2[4] = FmidiNotes[1];
                NatMinor_Scale2[5] = GmidiNotes[1];
                NatMinor_Scale2[6] = AbmidiNotes[1];
                NatMinor_Scale2[7] = BbmidiNotes[1];

                NatMinor_Scale3[0] = 0;
                NatMinor_Scale3[1] = CmidiNotes[2];
                NatMinor_Scale3[2] = DmidiNotes[2];
                NatMinor_Scale3[3] = EbmidiNotes[2];
                NatMinor_Scale3[4] = FmidiNotes[2];
                NatMinor_Scale3[5] = GmidiNotes[2];
                NatMinor_Scale3[6] = AbmidiNotes[2];
                NatMinor_Scale3[7] = BbmidiNotes[2];

                NatMinor_Scale4[0] = 0;
                NatMinor_Scale4[1] = CmidiNotes[3];
                NatMinor_Scale4[2] = DmidiNotes[3];
                NatMinor_Scale4[3] = EbmidiNotes[3];
                NatMinor_Scale4[4] = FmidiNotes[3];
                NatMinor_Scale4[5] = GmidiNotes[3];
                NatMinor_Scale4[6] = AbmidiNotes[3];
                NatMinor_Scale4[7] = BbmidiNotes[3];

                NatMinor_Scale5[0] = 0;
                NatMinor_Scale5[1] = CmidiNotes[4];
                NatMinor_Scale5[2] = DmidiNotes[4];
                NatMinor_Scale5[3] = EbmidiNotes[4];
                NatMinor_Scale5[4] = FmidiNotes[4];
                NatMinor_Scale5[5] = GmidiNotes[4];
                NatMinor_Scale5[6] = AbmidiNotes[4];
                NatMinor_Scale5[7] = BbmidiNotes[4];
            }
            else if (Key == "Db")
            {
                NatMinor_Scale1[0] = 0;
                NatMinor_Scale1[1] = DbmidiNotes[0];
                NatMinor_Scale1[2] = EbmidiNotes[0];
                NatMinor_Scale1[3] = EmidiNotes[0];
                NatMinor_Scale1[4] = GbmidiNotes[0];
                NatMinor_Scale1[5] = AbmidiNotes[0];
                NatMinor_Scale1[6] = AmidiNotes[0];
                NatMinor_Scale1[7] = BmidiNotes[0];

                NatMinor_Scale2[0] = 0;
                NatMinor_Scale2[1] = DbmidiNotes[1];
                NatMinor_Scale2[2] = EbmidiNotes[1];
                NatMinor_Scale2[3] = EmidiNotes[1];
                NatMinor_Scale2[4] = GbmidiNotes[1];
                NatMinor_Scale2[5] = AbmidiNotes[1];
                NatMinor_Scale2[6] = AmidiNotes[1];
                NatMinor_Scale2[7] = BmidiNotes[1];

                NatMinor_Scale3[0] = 0;
                NatMinor_Scale3[1] = DbmidiNotes[2];
                NatMinor_Scale3[2] = EbmidiNotes[2];
                NatMinor_Scale3[3] = EmidiNotes[2];
                NatMinor_Scale3[4] = GbmidiNotes[2];
                NatMinor_Scale3[5] = AbmidiNotes[2];
                NatMinor_Scale3[6] = AmidiNotes[2];
                NatMinor_Scale3[7] = BmidiNotes[2];

                NatMinor_Scale4[0] = 0;
                NatMinor_Scale4[1] = DbmidiNotes[3];
                NatMinor_Scale4[2] = EbmidiNotes[3];
                NatMinor_Scale4[3] = EmidiNotes[3];
                NatMinor_Scale4[4] = GbmidiNotes[3];
                NatMinor_Scale4[5] = AbmidiNotes[3];
                NatMinor_Scale4[6] = AmidiNotes[3];
                NatMinor_Scale4[7] = BmidiNotes[3];

                NatMinor_Scale5[0] = 0;
                NatMinor_Scale5[1] = DbmidiNotes[4];
                NatMinor_Scale5[2] = EbmidiNotes[4];
                NatMinor_Scale5[3] = EmidiNotes[4];
                NatMinor_Scale5[4] = GbmidiNotes[4];
                NatMinor_Scale5[5] = AbmidiNotes[4];
                NatMinor_Scale5[6] = AmidiNotes[4];
                NatMinor_Scale5[7] = BmidiNotes[4];
            }
            else if (Key == "D")
            {
                NatMinor_Scale1[0] = 0;
                NatMinor_Scale1[1] = DmidiNotes[0];
                NatMinor_Scale1[2] = EmidiNotes[0];
                NatMinor_Scale1[3] = FmidiNotes[0];
                NatMinor_Scale1[4] = GmidiNotes[0];
                NatMinor_Scale1[5] = AmidiNotes[0];
                NatMinor_Scale1[6] = BbmidiNotes[0];
                NatMinor_Scale1[7] = CmidiNotes[0];

                NatMinor_Scale2[0] = 0;
                NatMinor_Scale2[1] = DmidiNotes[1];
                NatMinor_Scale2[2] = EmidiNotes[1];
                NatMinor_Scale2[3] = FmidiNotes[1];
                NatMinor_Scale2[4] = GmidiNotes[1];
                NatMinor_Scale2[5] = AmidiNotes[1];
                NatMinor_Scale2[6] = BbmidiNotes[1];
                NatMinor_Scale2[7] = CmidiNotes[1];

                NatMinor_Scale3[0] = 0;
                NatMinor_Scale3[1] = DmidiNotes[2];
                NatMinor_Scale3[2] = EmidiNotes[2];
                NatMinor_Scale3[3] = FmidiNotes[2];
                NatMinor_Scale3[4] = GmidiNotes[2];
                NatMinor_Scale3[5] = AmidiNotes[2];
                NatMinor_Scale3[6] = BbmidiNotes[2];
                NatMinor_Scale3[7] = CmidiNotes[2];

                NatMinor_Scale4[0] = 0;
                NatMinor_Scale4[1] = DmidiNotes[3];
                NatMinor_Scale4[2] = EmidiNotes[3];
                NatMinor_Scale4[3] = FmidiNotes[3];
                NatMinor_Scale4[4] = GmidiNotes[3];
                NatMinor_Scale4[5] = AmidiNotes[3];
                NatMinor_Scale4[6] = BbmidiNotes[3];
                NatMinor_Scale4[7] = CmidiNotes[3];

                NatMinor_Scale5[0] = 0;
                NatMinor_Scale5[1] = DmidiNotes[4];
                NatMinor_Scale5[2] = EmidiNotes[4];
                NatMinor_Scale5[3] = FmidiNotes[4];
                NatMinor_Scale5[4] = GmidiNotes[4];
                NatMinor_Scale5[5] = AmidiNotes[4];
                NatMinor_Scale5[6] = BbmidiNotes[4];
                NatMinor_Scale5[7] = CmidiNotes[4];
            }
            else if (Key == "Eb")
            {
                NatMinor_Scale1[0] = 0;
                NatMinor_Scale1[1] = EbmidiNotes[0];
                NatMinor_Scale1[2] = FmidiNotes[0];
                NatMinor_Scale1[3] = GbmidiNotes[0];
                NatMinor_Scale1[4] = AbmidiNotes[0];
                NatMinor_Scale1[5] = BbmidiNotes[0];
                NatMinor_Scale1[6] = BmidiNotes[0];
                NatMinor_Scale1[7] = DbmidiNotes[0];

                NatMinor_Scale2[0] = 0;
                NatMinor_Scale2[1] = EbmidiNotes[1];
                NatMinor_Scale2[2] = FmidiNotes[1];
                NatMinor_Scale2[3] = GbmidiNotes[1];
                NatMinor_Scale2[4] = AbmidiNotes[1];
                NatMinor_Scale2[5] = BbmidiNotes[1];
                NatMinor_Scale2[6] = BmidiNotes[1];
                NatMinor_Scale2[7] = DbmidiNotes[1];

                NatMinor_Scale3[0] = 0;
                NatMinor_Scale3[1] = EbmidiNotes[2];
                NatMinor_Scale3[2] = FmidiNotes[2];
                NatMinor_Scale3[3] = GbmidiNotes[2];
                NatMinor_Scale3[4] = AbmidiNotes[2];
                NatMinor_Scale3[5] = BbmidiNotes[2];
                NatMinor_Scale3[6] = BmidiNotes[2];
                NatMinor_Scale3[7] = DbmidiNotes[2];

                NatMinor_Scale4[0] = 0;
                NatMinor_Scale4[1] = EbmidiNotes[3];
                NatMinor_Scale4[2] = FmidiNotes[3];
                NatMinor_Scale4[3] = GbmidiNotes[3];
                NatMinor_Scale4[4] = AbmidiNotes[3];
                NatMinor_Scale4[5] = BbmidiNotes[3];
                NatMinor_Scale4[6] = BmidiNotes[3];
                NatMinor_Scale4[7] = DbmidiNotes[3];

                NatMinor_Scale5[0] = 0;
                NatMinor_Scale5[1] = EbmidiNotes[4];
                NatMinor_Scale5[2] = FmidiNotes[4];
                NatMinor_Scale5[3] = GbmidiNotes[4];
                NatMinor_Scale5[4] = AbmidiNotes[4];
                NatMinor_Scale5[5] = BbmidiNotes[4];
                NatMinor_Scale5[6] = BmidiNotes[4];
                NatMinor_Scale5[7] = DbmidiNotes[4];
            }
            else if (Key == "E")
            {
                NatMinor_Scale1[0] = 0;
                NatMinor_Scale1[1] = EmidiNotes[0];
                NatMinor_Scale1[2] = GbmidiNotes[0];
                NatMinor_Scale1[3] = GmidiNotes[0];
                NatMinor_Scale1[4] = AmidiNotes[0];
                NatMinor_Scale1[5] = BmidiNotes[0];
                NatMinor_Scale1[6] = CmidiNotes[0];
                NatMinor_Scale1[7] = DmidiNotes[0];

                NatMinor_Scale2[0] = 0;
                NatMinor_Scale2[1] = EmidiNotes[1];
                NatMinor_Scale2[2] = GbmidiNotes[1];
                NatMinor_Scale2[3] = GmidiNotes[1];
                NatMinor_Scale2[4] = AmidiNotes[1];
                NatMinor_Scale2[5] = BmidiNotes[1];
                NatMinor_Scale2[6] = CmidiNotes[1];
                NatMinor_Scale2[7] = DmidiNotes[1];

                NatMinor_Scale3[0] = 0;
                NatMinor_Scale3[1] = EmidiNotes[2];
                NatMinor_Scale3[2] = GbmidiNotes[2];
                NatMinor_Scale3[3] = GmidiNotes[2];
                NatMinor_Scale3[4] = AmidiNotes[2];
                NatMinor_Scale3[5] = BmidiNotes[2];
                NatMinor_Scale3[6] = CmidiNotes[2];
                NatMinor_Scale3[7] = DmidiNotes[2];

                NatMinor_Scale4[0] = 0;
                NatMinor_Scale4[1] = EmidiNotes[3];
                NatMinor_Scale4[2] = GbmidiNotes[3];
                NatMinor_Scale4[3] = GmidiNotes[3];
                NatMinor_Scale4[4] = AmidiNotes[3];
                NatMinor_Scale4[5] = BmidiNotes[3];
                NatMinor_Scale4[6] = CmidiNotes[3];
                NatMinor_Scale4[7] = DmidiNotes[3];

                NatMinor_Scale5[0] = 0;
                NatMinor_Scale5[1] = EmidiNotes[4];
                NatMinor_Scale5[2] = GbmidiNotes[4];
                NatMinor_Scale5[3] = GmidiNotes[4];
                NatMinor_Scale5[4] = AmidiNotes[4];
                NatMinor_Scale5[5] = BmidiNotes[4];
                NatMinor_Scale5[6] = CmidiNotes[4];
                NatMinor_Scale5[7] = DmidiNotes[4];
            }
            else if (Key == "F")
            {
                NatMinor_Scale1[0] = 0;
                NatMinor_Scale1[1] = FmidiNotes[0];
                NatMinor_Scale1[2] = GmidiNotes[0];
                NatMinor_Scale1[3] = AbmidiNotes[0];
                NatMinor_Scale1[4] = BbmidiNotes[0];
                NatMinor_Scale1[5] = CmidiNotes[0];
                NatMinor_Scale1[6] = DbmidiNotes[0];
                NatMinor_Scale1[7] = EbmidiNotes[0];

                NatMinor_Scale2[0] = 0;
                NatMinor_Scale2[1] = FmidiNotes[1];
                NatMinor_Scale2[2] = GmidiNotes[1];
                NatMinor_Scale2[3] = AbmidiNotes[1];
                NatMinor_Scale2[4] = BbmidiNotes[1];
                NatMinor_Scale2[5] = CmidiNotes[1];
                NatMinor_Scale2[6] = DbmidiNotes[1];
                NatMinor_Scale2[7] = EbmidiNotes[1];

                NatMinor_Scale3[0] = 0;
                NatMinor_Scale3[1] = FmidiNotes[2];
                NatMinor_Scale3[2] = GmidiNotes[2];
                NatMinor_Scale3[3] = AbmidiNotes[2];
                NatMinor_Scale3[4] = BbmidiNotes[2];
                NatMinor_Scale3[5] = CmidiNotes[2];
                NatMinor_Scale3[6] = DbmidiNotes[2];
                NatMinor_Scale3[7] = EbmidiNotes[2];

                NatMinor_Scale4[0] = 0;
                NatMinor_Scale4[1] = FmidiNotes[3];
                NatMinor_Scale4[2] = GmidiNotes[3];
                NatMinor_Scale4[3] = AbmidiNotes[3];
                NatMinor_Scale4[4] = BbmidiNotes[3];
                NatMinor_Scale4[5] = CmidiNotes[3];
                NatMinor_Scale4[6] = DbmidiNotes[3];
                NatMinor_Scale4[7] = EbmidiNotes[3];

                NatMinor_Scale5[0] = 0;
                NatMinor_Scale5[1] = FmidiNotes[4];
                NatMinor_Scale5[2] = GmidiNotes[4];
                NatMinor_Scale5[3] = AbmidiNotes[4];
                NatMinor_Scale5[4] = BbmidiNotes[4];
                NatMinor_Scale5[5] = CmidiNotes[4];
                NatMinor_Scale5[6] = DbmidiNotes[4];
                NatMinor_Scale5[7] = EbmidiNotes[4];
            }
            else if (Key == "Gb")
            {
                NatMinor_Scale1[0] = 0;
                NatMinor_Scale1[1] = GbmidiNotes[0];
                NatMinor_Scale1[2] = AbmidiNotes[0];
                NatMinor_Scale1[3] = AmidiNotes[0];
                NatMinor_Scale1[4] = BmidiNotes[0];
                NatMinor_Scale1[5] = DbmidiNotes[0];
                NatMinor_Scale1[6] = DmidiNotes[0];
                NatMinor_Scale1[7] = EmidiNotes[0];

                NatMinor_Scale2[0] = 0;
                NatMinor_Scale2[1] = GbmidiNotes[1];
                NatMinor_Scale2[2] = AbmidiNotes[1];
                NatMinor_Scale2[3] = AmidiNotes[1];
                NatMinor_Scale2[4] = BmidiNotes[1];
                NatMinor_Scale2[5] = DbmidiNotes[1];
                NatMinor_Scale2[6] = DmidiNotes[1];
                NatMinor_Scale2[7] = EmidiNotes[1];

                NatMinor_Scale3[0] = 0;
                NatMinor_Scale3[1] = GbmidiNotes[2];
                NatMinor_Scale3[2] = AbmidiNotes[2];
                NatMinor_Scale3[3] = AmidiNotes[2];
                NatMinor_Scale3[4] = BmidiNotes[2];
                NatMinor_Scale3[5] = DbmidiNotes[2];
                NatMinor_Scale3[6] = DmidiNotes[2];
                NatMinor_Scale3[7] = EmidiNotes[2];

                NatMinor_Scale4[0] = 0;
                NatMinor_Scale4[1] = GbmidiNotes[3];
                NatMinor_Scale4[2] = AbmidiNotes[3];
                NatMinor_Scale4[3] = AmidiNotes[3];
                NatMinor_Scale4[4] = BmidiNotes[3];
                NatMinor_Scale4[5] = DbmidiNotes[3];
                NatMinor_Scale4[6] = DmidiNotes[3];
                NatMinor_Scale4[7] = EmidiNotes[3];

                NatMinor_Scale5[0] = 0;
                NatMinor_Scale5[1] = GbmidiNotes[4];
                NatMinor_Scale5[2] = AbmidiNotes[4];
                NatMinor_Scale5[3] = AmidiNotes[4];
                NatMinor_Scale5[4] = BmidiNotes[4];
                NatMinor_Scale5[5] = DbmidiNotes[4];
                NatMinor_Scale5[6] = DmidiNotes[4];
                NatMinor_Scale5[7] = EmidiNotes[4];
            }
            else if (Key == "G")
            {
                NatMinor_Scale1[0] = 0;
                NatMinor_Scale1[1] = GmidiNotes[0];
                NatMinor_Scale1[2] = AmidiNotes[0];
                NatMinor_Scale1[3] = BbmidiNotes[0];
                NatMinor_Scale1[4] = CmidiNotes[0];
                NatMinor_Scale1[5] = DmidiNotes[0];
                NatMinor_Scale1[6] = EbmidiNotes[0];
                NatMinor_Scale1[7] = FmidiNotes[0];

                NatMinor_Scale2[0] = 0;
                NatMinor_Scale2[1] = GmidiNotes[1];
                NatMinor_Scale2[2] = AmidiNotes[1];
                NatMinor_Scale2[3] = BbmidiNotes[1];
                NatMinor_Scale2[4] = CmidiNotes[1];
                NatMinor_Scale2[5] = DmidiNotes[1];
                NatMinor_Scale2[6] = EbmidiNotes[1];
                NatMinor_Scale2[7] = FmidiNotes[1];

                NatMinor_Scale3[0] = 0;
                NatMinor_Scale3[1] = GmidiNotes[2];
                NatMinor_Scale3[2] = AmidiNotes[2];
                NatMinor_Scale3[3] = BbmidiNotes[2];
                NatMinor_Scale3[4] = CmidiNotes[2];
                NatMinor_Scale3[5] = DmidiNotes[2];
                NatMinor_Scale3[6] = EbmidiNotes[2];
                NatMinor_Scale3[7] = FmidiNotes[2];

                NatMinor_Scale4[0] = 0;
                NatMinor_Scale4[1] = GmidiNotes[3];
                NatMinor_Scale4[2] = AmidiNotes[3];
                NatMinor_Scale4[3] = BbmidiNotes[3];
                NatMinor_Scale4[4] = CmidiNotes[3];
                NatMinor_Scale4[5] = DmidiNotes[3];
                NatMinor_Scale4[6] = EbmidiNotes[3];
                NatMinor_Scale4[7] = FmidiNotes[3];

                NatMinor_Scale5[0] = 0;
                NatMinor_Scale5[1] = GmidiNotes[4];
                NatMinor_Scale5[2] = AmidiNotes[4];
                NatMinor_Scale5[3] = BbmidiNotes[4];
                NatMinor_Scale5[4] = CmidiNotes[4];
                NatMinor_Scale5[5] = DmidiNotes[4];
                NatMinor_Scale5[6] = EbmidiNotes[4];
                NatMinor_Scale5[7] = FmidiNotes[4];
            }
            else if (Key == "Ab")
            {
                NatMinor_Scale1[0] = 0;
                NatMinor_Scale1[1] = AbmidiNotes[0];
                NatMinor_Scale1[2] = BbmidiNotes[0];
                NatMinor_Scale1[3] = BmidiNotes[0];
                NatMinor_Scale1[4] = DbmidiNotes[0];
                NatMinor_Scale1[5] = EbmidiNotes[0];
                NatMinor_Scale1[6] = EmidiNotes[0];
                NatMinor_Scale1[7] = GbmidiNotes[0];

                NatMinor_Scale2[0] = 0;
                NatMinor_Scale2[1] = AbmidiNotes[1];
                NatMinor_Scale2[2] = BbmidiNotes[1];
                NatMinor_Scale2[3] = BmidiNotes[1];
                NatMinor_Scale2[4] = DbmidiNotes[1];
                NatMinor_Scale2[5] = EbmidiNotes[1];
                NatMinor_Scale2[6] = EmidiNotes[1];
                NatMinor_Scale2[7] = GbmidiNotes[1];

                NatMinor_Scale3[0] = 0;
                NatMinor_Scale3[1] = AbmidiNotes[2];
                NatMinor_Scale3[2] = BbmidiNotes[2];
                NatMinor_Scale3[3] = BmidiNotes[2];
                NatMinor_Scale3[4] = DbmidiNotes[2];
                NatMinor_Scale3[5] = EbmidiNotes[2];
                NatMinor_Scale3[6] = EmidiNotes[2];
                NatMinor_Scale3[7] = GbmidiNotes[2];

                NatMinor_Scale4[0] = 0;
                NatMinor_Scale4[1] = AbmidiNotes[3];
                NatMinor_Scale4[2] = BbmidiNotes[3];
                NatMinor_Scale4[3] = BmidiNotes[3];
                NatMinor_Scale4[4] = DbmidiNotes[3];
                NatMinor_Scale4[5] = EbmidiNotes[3];
                NatMinor_Scale4[6] = EmidiNotes[3];
                NatMinor_Scale4[7] = GbmidiNotes[3];

                NatMinor_Scale5[0] = 0;
                NatMinor_Scale5[1] = AbmidiNotes[4];
                NatMinor_Scale5[2] = BbmidiNotes[4];
                NatMinor_Scale5[3] = BmidiNotes[4];
                NatMinor_Scale5[4] = DbmidiNotes[4];
                NatMinor_Scale5[5] = EbmidiNotes[4];
                NatMinor_Scale5[6] = EmidiNotes[4];
                NatMinor_Scale5[7] = GbmidiNotes[4];
            }
            else if (Key == "A")
            {
                NatMinor_Scale1[0] = 0;
                NatMinor_Scale1[1] = AmidiNotes[0];
                NatMinor_Scale1[2] = BmidiNotes[0];
                NatMinor_Scale1[3] = CmidiNotes[0];
                NatMinor_Scale1[4] = DmidiNotes[0];
                NatMinor_Scale1[5] = EmidiNotes[0];
                NatMinor_Scale1[6] = FmidiNotes[0];
                NatMinor_Scale1[7] = GmidiNotes[0];

                NatMinor_Scale2[0] = 0;
                NatMinor_Scale2[1] = AmidiNotes[1];
                NatMinor_Scale2[2] = BmidiNotes[1];
                NatMinor_Scale2[3] = CmidiNotes[1];
                NatMinor_Scale2[4] = DmidiNotes[1];
                NatMinor_Scale2[5] = EmidiNotes[1];
                NatMinor_Scale2[6] = FmidiNotes[1];
                NatMinor_Scale2[7] = GmidiNotes[1];

                NatMinor_Scale3[0] = 0;
                NatMinor_Scale3[1] = AmidiNotes[2];
                NatMinor_Scale3[2] = BmidiNotes[2];
                NatMinor_Scale3[3] = CmidiNotes[2];
                NatMinor_Scale3[4] = DmidiNotes[2];
                NatMinor_Scale3[5] = EmidiNotes[2];
                NatMinor_Scale3[6] = FmidiNotes[2];
                NatMinor_Scale3[7] = GmidiNotes[2];

                NatMinor_Scale4[0] = 0;
                NatMinor_Scale4[1] = AmidiNotes[3];
                NatMinor_Scale4[2] = BmidiNotes[3];
                NatMinor_Scale4[3] = CmidiNotes[3];
                NatMinor_Scale4[4] = DmidiNotes[3];
                NatMinor_Scale4[5] = EmidiNotes[3];
                NatMinor_Scale4[6] = FmidiNotes[3];
                NatMinor_Scale4[7] = GmidiNotes[3];

                NatMinor_Scale5[0] = 0;
                NatMinor_Scale5[1] = AmidiNotes[4];
                NatMinor_Scale5[2] = BmidiNotes[4];
                NatMinor_Scale5[3] = CmidiNotes[4];
                NatMinor_Scale5[4] = DmidiNotes[4];
                NatMinor_Scale5[5] = EmidiNotes[4];
                NatMinor_Scale5[6] = FmidiNotes[4];
                NatMinor_Scale5[7] = GmidiNotes[4];
            }
            else if (Key == "Bb")
            {
                NatMinor_Scale1[0] = 0;
                NatMinor_Scale1[1] = BbmidiNotes[0];
                NatMinor_Scale1[2] = CmidiNotes[0];
                NatMinor_Scale1[3] = DbmidiNotes[0];
                NatMinor_Scale1[4] = EbmidiNotes[0];
                NatMinor_Scale1[5] = FmidiNotes[0];
                NatMinor_Scale1[6] = GbmidiNotes[0];
                NatMinor_Scale1[7] = AbmidiNotes[0];

                NatMinor_Scale2[0] = 0;
                NatMinor_Scale2[1] = BbmidiNotes[1];
                NatMinor_Scale2[2] = CmidiNotes[1];
                NatMinor_Scale2[3] = DbmidiNotes[1];
                NatMinor_Scale2[4] = EbmidiNotes[1];
                NatMinor_Scale2[5] = FmidiNotes[1];
                NatMinor_Scale2[6] = GbmidiNotes[1];
                NatMinor_Scale2[7] = AbmidiNotes[1];

                NatMinor_Scale3[0] = 0;
                NatMinor_Scale3[1] = BbmidiNotes[2];
                NatMinor_Scale3[2] = CmidiNotes[2];
                NatMinor_Scale3[3] = DbmidiNotes[2];
                NatMinor_Scale3[4] = EbmidiNotes[2];
                NatMinor_Scale3[5] = FmidiNotes[2];
                NatMinor_Scale3[6] = GbmidiNotes[2];
                NatMinor_Scale3[7] = AbmidiNotes[2];

                NatMinor_Scale4[0] = 0;
                NatMinor_Scale4[1] = BbmidiNotes[3];
                NatMinor_Scale4[2] = CmidiNotes[3];
                NatMinor_Scale4[3] = DbmidiNotes[3];
                NatMinor_Scale4[4] = EbmidiNotes[3];
                NatMinor_Scale4[5] = FmidiNotes[3];
                NatMinor_Scale4[6] = GbmidiNotes[3];
                NatMinor_Scale4[7] = AbmidiNotes[3];

                NatMinor_Scale5[0] = 0;
                NatMinor_Scale5[1] = BbmidiNotes[4];
                NatMinor_Scale5[2] = CmidiNotes[4];
                NatMinor_Scale5[3] = DbmidiNotes[4];
                NatMinor_Scale5[4] = EbmidiNotes[4];
                NatMinor_Scale5[5] = FmidiNotes[4];
                NatMinor_Scale5[6] = GbmidiNotes[4];
                NatMinor_Scale5[7] = AbmidiNotes[4];
            }
            else if (Key == "B")
            {
                NatMinor_Scale1[0] = 0;
                NatMinor_Scale1[1] = BmidiNotes[0];
                NatMinor_Scale1[2] = DbmidiNotes[0];
                NatMinor_Scale1[3] = DmidiNotes[0];
                NatMinor_Scale1[4] = EmidiNotes[0];
                NatMinor_Scale1[5] = GbmidiNotes[0];
                NatMinor_Scale1[6] = GmidiNotes[0];
                NatMinor_Scale1[7] = AmidiNotes[0];

                NatMinor_Scale2[0] = 0;
                NatMinor_Scale2[1] = BmidiNotes[1];
                NatMinor_Scale2[2] = DbmidiNotes[1];
                NatMinor_Scale2[3] = DmidiNotes[1];
                NatMinor_Scale2[4] = EmidiNotes[1];
                NatMinor_Scale2[5] = GbmidiNotes[1];
                NatMinor_Scale2[6] = GmidiNotes[1];
                NatMinor_Scale2[7] = AmidiNotes[1];

                NatMinor_Scale3[0] = 0;
                NatMinor_Scale3[1] = BmidiNotes[2];
                NatMinor_Scale3[2] = DbmidiNotes[2];
                NatMinor_Scale3[3] = DmidiNotes[2];
                NatMinor_Scale3[4] = EmidiNotes[2];
                NatMinor_Scale3[5] = GbmidiNotes[2];
                NatMinor_Scale3[6] = GmidiNotes[2];
                NatMinor_Scale3[7] = AmidiNotes[2];

                NatMinor_Scale4[0] = 0;
                NatMinor_Scale4[1] = BmidiNotes[3];
                NatMinor_Scale4[2] = DbmidiNotes[3];
                NatMinor_Scale4[3] = DmidiNotes[3];
                NatMinor_Scale4[4] = EmidiNotes[3];
                NatMinor_Scale4[5] = GbmidiNotes[3];
                NatMinor_Scale4[6] = GmidiNotes[3];
                NatMinor_Scale4[7] = AmidiNotes[3];

                NatMinor_Scale5[0] = 0;
                NatMinor_Scale5[1] = BmidiNotes[4];
                NatMinor_Scale5[2] = DbmidiNotes[4];
                NatMinor_Scale5[3] = DmidiNotes[4];
                NatMinor_Scale5[4] = EmidiNotes[4];
                NatMinor_Scale5[5] = GbmidiNotes[4];
                NatMinor_Scale5[6] = FmidiNotes[4];
                NatMinor_Scale5[7] = AmidiNotes[4];
            }
        }

        public void MidiNotesSetup()
        {
            C_MidiNotes();
            Db_MidiNotes();
            D_MidiNotes();
            Eb_MidiNotes();
            E_MidiNotes();
            F_MidiNotes();
            Gb_MidiNotes();
            G_MidiNotes();
            Ab_MidiNotes();
            A_MidiNotes();
            Bb_MidiNotes();
            B_MidiNotes();
        }

        public void C_MidiNotes()
        {
            CmidiNotes = new int[6];
            CmidiNotes[0] = C2;
            CmidiNotes[1] = C3;
            CmidiNotes[2] = C4;
            CmidiNotes[3] = C5;
            CmidiNotes[4] = C6;
            CmidiNotes[5] = C7;
        }
        public void Db_MidiNotes()
        {
            DbmidiNotes = new int[5];
            DbmidiNotes[0] = Db2;
            DbmidiNotes[1] = Db3;
            DbmidiNotes[2] = Db4;
            DbmidiNotes[3] = Db5;
            DbmidiNotes[4] = Db6;
        }
        public void D_MidiNotes()
        {
            DmidiNotes = new int[5];
            DmidiNotes[0] = D2;
            DmidiNotes[1] = D3;
            DmidiNotes[2] = D4;
            DmidiNotes[3] = D5;
            DmidiNotes[4] = D6;

        }
        public void Eb_MidiNotes()
        {
            EbmidiNotes = new int[5];
            EbmidiNotes[0] = Eb2;
            EbmidiNotes[1] = Eb3;
            EbmidiNotes[2] = Eb4;
            EbmidiNotes[3] = Eb5;
            EbmidiNotes[4] = Eb6;
        }
        public void E_MidiNotes()
        {
            EmidiNotes = new int[5];
            EmidiNotes[0] = E2;
            EmidiNotes[1] = E3;
            EmidiNotes[2] = E4;
            EmidiNotes[3] = E5;
            EmidiNotes[4] = E6;
        }
        public void F_MidiNotes()
        {
            FmidiNotes = new int[5];
            FmidiNotes[0] = F2;
            FmidiNotes[1] = F3;
            FmidiNotes[2] = F4;
            FmidiNotes[3] = F5;
            FmidiNotes[4] = F6;
        }
        public void Gb_MidiNotes()
        {
            GbmidiNotes = new int[5];
            GbmidiNotes[0] = Gb2;
            GbmidiNotes[1] = Gb3;
            GbmidiNotes[2] = Gb4;
            GbmidiNotes[3] = Gb5;
            GbmidiNotes[4] = Gb6;
        }
        public void G_MidiNotes()
        {
            GmidiNotes = new int[5];
            GmidiNotes[0] = G2;
            GmidiNotes[1] = G3;
            GmidiNotes[2] = G4;
            GmidiNotes[3] = G5;
            GmidiNotes[4] = G6;
        }
        public void Ab_MidiNotes()
        {
            AbmidiNotes = new int[5];
            AbmidiNotes[0] = Ab2;
            AbmidiNotes[1] = Ab3;
            AbmidiNotes[2] = Ab4;
            AbmidiNotes[3] = Ab5;
            AbmidiNotes[4] = Ab6;
        }
        public void A_MidiNotes()
        {
            AmidiNotes = new int[5];
            AmidiNotes[0] = A2;
            AmidiNotes[1] = A3;
            AmidiNotes[2] = A4;
            AmidiNotes[3] = A5;
            AmidiNotes[4] = A6;
        }
        public void Bb_MidiNotes()
        {
            BbmidiNotes = new int[5];
            BbmidiNotes[0] = Bb2;
            BbmidiNotes[1] = Bb3;
            BbmidiNotes[2] = Bb4;
            BbmidiNotes[3] = Bb5;
            BbmidiNotes[4] = Bb6;
        }
        public void B_MidiNotes()
        {
            BmidiNotes = new int[5];
            BmidiNotes[0] = B2;
            BmidiNotes[1] = B3;
            BmidiNotes[2] = B4;
            BmidiNotes[3] = B5;
            BmidiNotes[4] = B6;
        }
    }
}
