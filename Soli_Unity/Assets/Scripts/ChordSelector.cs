using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SoliGameController;

namespace SoliSoundScape
{
    public class ChordSelector : MonoBehaviour
    {
        public GameController game_c;
        ChordProgressions cp;
        SoliSoundscapeLvl1 lvl1;
        SoliSoundscapeLvl2 lvl2;

        public bool Happy, Sad, Unsure, isHappy, isSad, pickKey;
        public int startKey, gameState, lvl1State, lvl2State;
        // Use this for initialization
        void Start()
        {
            cp = this.GetComponent<ChordProgressions>();
            lvl1 = this.GetComponent<SoliSoundscapeLvl1>();
            lvl2 = this.GetComponent<SoliSoundscapeLvl2>();

            lvl1State = lvl1.lvl1State;
            lvl2State = lvl2.lvl2State;
        }

        // Update is called once per frame
        void Update()
        {
            Happy = game_c.hHeld_Reached;
            Sad = game_c.sHeld_Reached;
            Unsure = game_c.uHeld_Reached;
            isHappy = game_c.Happy;
            isSad = game_c.Sad;
            gameState = game_c.state;

            randomKey();

            lvl1.Lvl1ChordStates();
            lvl2.Lvl2ChordStates();
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
    }
}
