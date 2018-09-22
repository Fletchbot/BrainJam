using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SoliSoundScape;

namespace AudioHelm
{
    public class audioHelmPatchManager : MonoBehaviour
    {
        public HelmPatch patch1, patch2, patch3, patch4;
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
                    currPatch = 1;
                }
                else if (currPatch == 1)
                {
                    DroneSynth.LoadPatch(patch1);
                    currPatch = 2;
                }
                else if (currPatch == 2)
                {
                    DroneSynth.LoadPatch(patch2);
                    currPatch = 0;
                }
 /*               else if (currPatch == 3)
                {
                    DroneSynth.LoadPatch(patch3);
                    currPatch = 4;
                }
                else if (currPatch == 4)
                {
                    DroneSynth.LoadPatch(patch4);
                    currPatch = 0;
                } */
                patch_sw = true;
            }
            else if (!changePatch && patch_sw)
            {
                patch_sw = false;
            }

        }
    }
}
