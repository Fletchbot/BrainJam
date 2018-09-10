using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SoliSoundScape;

namespace AudioHelm
{
    public class audioHelmPatchManager : MonoBehaviour
    {
        public HelmPatch hp;
        public GameObject chordProg;
        [Header("Synth Section")]
        public AudioHelm.HelmController DroneSynth;

        public int currPatch;
        public bool changePatch, patch_sw;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
           changePatch = chordProg.GetComponent<ChordProgressions>().changePatch;



            if (changePatch && !patch_sw)
            {
                if (currPatch == 0)
                {
                    DroneSynth.LoadPatch(hp);
                    currPatch = 1;
                }
                else if (currPatch == 1)
                {
                    DroneSynth.LoadPatch(hp);
                    currPatch = 0;
                }
                patch_sw = true;
            }
            else if (!changePatch && patch_sw)
            {
                patch_sw = false;
            }

        }
    }
}
