using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
namespace SoliGameController
{
    public class AudioPlaytestManager : MonoBehaviour
    {
        GameController gameController;
        TrainingStates ts;
        [Header("NarratorAudio")]
        public AudioSource[] NarratorClips;
        public int N_Intro;
        [Header("Audio FX")]
        public AudioSource LavaAU, ThunderAU;
        [Header("AudioMixer Section")]
        public AudioMixer synthMixer;

        private bool eruptAu;
        private float sfxlvl;
        private int headsetOn;
        private bool sfxPlaying, sfxFadedown, sfxFadeup, reset;

        // Use this for initialization
        void Start()
        {
            gameController = this.GetComponent<GameController>();
            ts = this.GetComponent<TrainingStates>();

            resetValues();
        }

        // Update is called once per frame
        void Update()
        {
            headsetOn = gameController.HeadsetOn;

            PlayEruption();

            if (headsetOn == 1)
            {
                SetSFXLvl();
                TrainingAudio();
                if (reset) reset = false;
            }
            else if (headsetOn == 0 && !reset)
            {
                resetValues();
                reset = true;
            }
        }

        public void SetSFXLvl()
        {
            sfxPlaying = ThunderAU.GetComponent<AudioSource>().isPlaying;

            if (sfxPlaying && sfxFadedown)
            {
                if (sfxlvl <= 0.1f && sfxlvl >= -6.0f)
                {

                    sfxlvl = sfxlvl - 0.1f;
                }
                else
                {
                    sfxFadedown = false;
                }
            }
            else if (sfxPlaying && sfxFadeup)
            {
                if (sfxlvl >= -6.1f && sfxlvl <= 0.0f)
                {
                    sfxlvl = sfxlvl + 0.1f;
                }
                else
                {
                    sfxFadeup = false;
                }
            }

            synthMixer.SetFloat("sfxVol", sfxlvl);
        }

        public void TrainingAudio()
        {
            if (gameController.state == 0) // NARRATOR MEDITATE TRAINING
            {
                if (N_Intro == 0)
                {
                    sfxFadedown = true;
                    NarratorClips[0].GetComponent<AudioSource>().Play();
                    N_Intro = 1;
                }

                if (!NarratorClips[0].GetComponent<AudioSource>().isPlaying && N_Intro == 1)
                {
                    N_Intro = 2;
                }
            }
            if (gameController.state == 2) //NARRATOR EMOTIONS TRAINING
            {
                if (N_Intro == 2)
                {
                    NarratorClips[1].GetComponent<AudioSource>().Play();
                    N_Intro = 3;
                }

                if (!NarratorClips[1].GetComponent<AudioSource>().isPlaying && N_Intro == 3)
                {
                    N_Intro = 4;
                }

                if (ts.happy && N_Intro == 4)
                {
                    NarratorClips[2].GetComponent<AudioSource>().Play();
                    N_Intro = 5;
                }

                if(!NarratorClips[2].GetComponent<AudioSource>().isPlaying && N_Intro == 5)
                {
                    N_Intro = 6;
                }
            }
            if (gameController.state == 4) //NARRATOR FOCUS TRAINING
            {
                if (N_Intro == 6)
                {
                    sfxFadeup = true;
                    NarratorClips[3].GetComponent<AudioSource>().Play();
                    N_Intro = 7;
                }

                if (!NarratorClips[3].GetComponent<AudioSource>().isPlaying && N_Intro == 7)
                {
                    N_Intro = 8;
                }
            }
            if(gameController.state == 5)//Narrator End Training
            {
                if(N_Intro == 8)
                {
                    NarratorClips[4].GetComponent<AudioSource>().Play();
                    N_Intro = 9;
                }
            }
        }

        public void PlayEruption()
        {
            if (gameController.NoGesture && !eruptAu)
            {
                ThunderAU.GetComponent<AudioSource>().Play();
                LavaAU.GetComponent<AudioSource>().Play();
                eruptAu = true;
            }
            else if (!gameController.NoGesture && eruptAu)
            {
                eruptAu = false;
                ThunderAU.GetComponent<AudioSource>().Stop();
                LavaAU.GetComponent<AudioSource>().Stop();
            }

        }

        public void resetValues()
        {
            sfxlvl = 0.0f;
            N_Intro = 0;
            eruptAu = false;
            sfxFadeup = false;
            sfxFadedown = false;

            for(int n = 0; n < NarratorClips.Length; n++)
            {
                NarratorClips[n].GetComponent<AudioSource>().Stop();
            }

        }
    }
}
