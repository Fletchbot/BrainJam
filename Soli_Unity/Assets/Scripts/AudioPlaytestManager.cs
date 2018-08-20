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
        [Header("NarratorAudio")]
        public AudioSource[] NarratorClips;
        public int N_Intro;
        [Header("Audio FX")]
        public AudioSource LavaAU, ThunderAU;
        [Header("AudioMixer Section")]
        public AudioMixer synthMixer;

        private bool G_sw, M_sw, H_sw, S_sw, f_sw, eruptAu;
        private float sfxlvl;
        private bool sfxPlaying, sfxFadedown, sfxFadeup;

        // Use this for initialization
        void Start()
        {
            sfxlvl = 0.0f;
            gameController = this.GetComponent<GameController>();
        }

        // Update is called once per frame
        void Update()
        {
            SetSFXLvl();
            TrainingAudio();
            PlayEruption();
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
            if (gameController.state == 0 && N_Intro == 0) // INTRO FOCUS TO SKIP TO NEXT STATE
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
            if (gameController.state == 1 && N_Intro == 2) //MEDIATE TRAINING INTRO
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
            }
            if (gameController.state == 3 && N_Intro == 4) //EMOTIONS TRAINING INTRO
            {
                if (N_Intro == 4)
                {
                    sfxFadeup = true;
                    NarratorClips[2].GetComponent<AudioSource>().Play();
                    N_Intro = 5;
                }

                if (!NarratorClips[2].GetComponent<AudioSource>().isPlaying && N_Intro == 5)
                {
                    N_Intro = 6;
                }
            }
            if (gameController.state == 5 && N_Intro == 6) //FOCUS TRAINING INTRO
            {
                if (N_Intro == 6)
                {
                    NarratorClips[3].GetComponent<AudioSource>().Play();
                    N_Intro = 7;
                }

                if (!NarratorClips[3].GetComponent<AudioSource>().isPlaying && N_Intro == 7)
                {
                    N_Intro = 8;
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
    }
}
