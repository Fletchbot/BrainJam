using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SoliGameController
{
    public class MeditateState : MonoBehaviour
    {
        GameController gc;
        public GameObject MeditateMeter;
        AudioPlaytestManager au;

        public float wek_mFloat, mTarget, mOut, meditateCountdown, counter, meditateThresholdTimer, thresholdCounter;
        public bool wek_isM, isMeditate, m_trainSW, m_meterSW, enableState, reset;
        public float prevT_Meditate, currT_Meditate, mean_Meditate;
        public Color meditateTransColor, meditateActiveColor, meditateDeactiveColor, lerpedMCol;
        private Vector3 meditateGamePos;
        private Vector2 middleAnchorMin, middleAnchorMax, topRightAnchorMin, topRightAnchorMax;

        public float trainCountdown, trainCounter;
        public bool train_on, train_sw;

        public void resetValues()
        {
            counter = 2.0f;
            meditateCountdown = counter;
            thresholdCounter = 2.5f;
            meditateThresholdTimer = thresholdCounter;

            mTarget = 4.5f;
            mOut = 6.5f;

            MeditateMeter.GetComponent<PieMeter>().MinValuec1 = mOut;
            MeditateMeter.GetComponent<PieMeter>().MinValuec2 = mOut;
            MeditateMeter.GetComponent<PieMeter>().MaxValuec1 = mTarget;
            MeditateMeter.GetComponent<PieMeter>().MaxValuec2 = mTarget;

            m_trainSW = false;
            m_meterSW = false;

            trainCounter = 4.0f;
            trainCountdown = trainCounter;
            train_sw = false;
            train_on = false;
        }

        public void m_MeterReset()
        {
            MeditateMeter.GetComponent<ActivateObjects>().SetDeactive(true);
        }

        public void OnEnable()
        {
            gc = this.GetComponent<GameController>();
            au = this.GetComponent<AudioPlaytestManager>();

            meditateTransColor = new Color32(20, 200, 210, 100);
            meditateActiveColor = new Color32(20, 100, 210, 200);
            meditateDeactiveColor = new Color32(20, 200, 210, 0);
            meditateGamePos = new Vector3(-240, -60, 0);

            middleAnchorMin = new Vector2(0.5f, 0.5f);
            middleAnchorMax = new Vector2(0.5f, 0.5f);
            topRightAnchorMin = new Vector2(1f, 1f);
            topRightAnchorMax = new Vector2(1f, 1f);

            resetValues();
        }

        void Update()
        {
            wek_mFloat = gc.wek_mFloat;
            wek_isM = gc.wek_isM;
            enableState = gc.isRunning;

            if(enableState)
            {
                MeditateTraining();
                MeditateStates();
                m_MeterUpdate();
                if (reset) reset = false;
            }
            else if (!enableState && !reset)
            {
                resetValues();
                reset = true;
            }
        }

        public void m_MeterUpdate()
        {
            if (gc.state == 0 && m_meterSW) //meditatemeter to middle & scale up
            {
                MeditateMeter.GetComponent<ActivateObjects>().SetActive(true);
                MeditateMeter.transform.localScale = new Vector3(2, 2, 2);
                MeditateMeter.GetComponent<RectTransform>().anchorMin = new Vector2(middleAnchorMin.x, middleAnchorMin.y);
                MeditateMeter.GetComponent<RectTransform>().anchorMax = new Vector2(middleAnchorMax.x, middleAnchorMax.y);
                MeditateMeter.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
            }
            else if (gc.state == 2 && m_meterSW)
            {
                //place meditatemeter to game pos & scale down
                MeditateMeter.transform.localScale = new Vector3(1, 1, 1);
                MeditateMeter.GetComponent<RectTransform>().localPosition = new Vector3(meditateGamePos.x, meditateGamePos.y, 0);
                MeditateMeter.GetComponent<RectTransform>().anchorMin = new Vector2(topRightAnchorMin.x, topRightAnchorMin.y);
                MeditateMeter.GetComponent<RectTransform>().anchorMax = new Vector2(topRightAnchorMax.x, topRightAnchorMax.y);
                m_meterSW = false;
            }
    //        -----------------------------------------------------------------------------------------------------------------
            //MEDITATE COLOUR CHANGES
            MeditateMeter.GetComponent<PieMeter>().Valuec1 = gc.wek_mFloat;
            MeditateMeter.GetComponent<PieMeter>().Valuec2 = gc.wek_mFloat;

            if (isMeditate)
            {
                MeditateMeter.GetComponent<PieMeter>().fillersemiC1.color = meditateActiveColor;
                MeditateMeter.GetComponent<PieMeter>().fillersemiC2.color = meditateActiveColor;
            }
            else if (wek_mFloat <= mTarget)
            {
                lerpedMCol = Color.Lerp(meditateTransColor, meditateActiveColor, Mathf.PingPong(Time.time, 0.5f));
                MeditateMeter.GetComponent<PieMeter>().fillersemiC1.color = lerpedMCol;
                MeditateMeter.GetComponent<PieMeter>().fillersemiC2.color = lerpedMCol;
            }
            else if (wek_mFloat >= mOut)
            {
                MeditateMeter.GetComponent<PieMeter>().fillersemiC1.color = meditateDeactiveColor;
                MeditateMeter.GetComponent<PieMeter>().fillersemiC2.color = meditateDeactiveColor;
            }
            else if (wek_mFloat <= mOut && wek_mFloat >= mTarget)
            {
                lerpedMCol = Color.Lerp(meditateDeactiveColor, meditateTransColor, Mathf.PingPong(Time.time, 0.5f));
                MeditateMeter.GetComponent<PieMeter>().fillersemiC1.color = lerpedMCol;
                MeditateMeter.GetComponent<PieMeter>().fillersemiC2.color = lerpedMCol;
            }
        }
        
        public void MeditateStates()
        {
            if (wek_mFloat <= mTarget || wek_isM)
            {
                if (meditateCountdown <= 0.0f)
                {
                    isMeditate = true;
                    meditateCountdown = counter;
                }
                else
                {
                    meditateCountdown -= Time.deltaTime;
                }

                if (wek_isM)
                {
                    isMeditate = true;
                    meditateCountdown = counter;
                }
            }
            else if (wek_mFloat >= mOut && !wek_isM)
            {
                if (meditateCountdown <= 0.0f)
                {
                    isMeditate = false;
                    meditateCountdown = counter;
                }
                else
                {
                    meditateCountdown -= Time.deltaTime;
                }
            }
            else
            {
                meditateCountdown = counter;
            }
        }
        public void MeditateTraining() //state 0 & 1
        {
            //Intro
            if (gc.state == 0) //INTRO (Lava Erupting)
            {
                if (!train_sw) //Enable eruption, place and scale meditatemeter in middle of screen
                {
                    gc.NoG_Enable();
                    m_meterSW = true;
                    train_sw = true;
                }

                if (au.N_Intro == 1 && isMeditate)
                {
                    if (isMeditate)
                    {
                        trainCountdown -= Time.deltaTime;
                    }

                    if (trainCountdown <= 0.0f)
                    {
                        train_on = true;
                    }
                }
                //after Narrator and meditate lasts 2 secs go to emotions training
                if (au.N_Intro == 2 && train_on)
                {
                    trainCountdown = trainCounter;
                    train_sw = false;
                    gc.state++;
                }
                else if (au.N_Intro == 2 && !train_on && !m_trainSW)
                {
                    MeditateDifficultyLevel();
                }
                else if (au.N_Intro == 2 && !train_on && m_trainSW && isMeditate)
                {
                    train_on = true;
                    m_trainSW = false;
                }
            }
        }

        public void MeditateDifficultyLevel()
        {
            if (meditateThresholdTimer == thresholdCounter)
            {
                prevT_Meditate = wek_mFloat;
                meditateThresholdTimer -= Time.deltaTime;
            }
            else if (meditateThresholdTimer >= 0.0f)
            {
                meditateThresholdTimer -= Time.deltaTime;
            }
            else if (meditateThresholdTimer <= 0.0f)
            {
                currT_Meditate = wek_mFloat;
                mean_Meditate = (prevT_Meditate + currT_Meditate) / 2;

                if (mean_Meditate >= 10.01f)
                {
                    mTarget = 10.5f;
                    mOut = 12.5f;
                    Debug.Log("mTrain: 10.5-12.5");
                }
                else if (mean_Meditate >= 9.01f && mean_Meditate <= 10.0f)
                {
                    mTarget = 9.5f;
                    mOut = 11.5f;
                    Debug.Log("mTrain: 9.5-11.5");
                }
                else if (mean_Meditate >= 8.01f && mean_Meditate <= 9.0f)
                {
                    mTarget = 8.5f;
                    mOut = 10.5f;
                    Debug.Log("mTrain: 8.5-10.5");
                }
                else if (mean_Meditate >= 7.01f && mean_Meditate <= 8.0f)
                {
                    mTarget = 7.5f;
                    mOut = 9.5f;
                    Debug.Log("mTrain: 7.5-9.5");
                }
                else if (mean_Meditate >= 6.01f && mean_Meditate <= 7.0f)
                {
                    mTarget = 6.5f;
                    mOut = 8.5f;
                    Debug.Log("mTrain: 6.5-8.5");
                }
                else if (mean_Meditate >= 5.01f && mean_Meditate <= 6.0f)
                {
                    mTarget = 5.5f;
                    mOut = 7.5f;
                    Debug.Log("mTrain: 5.5-7.5");
                }
                else if (mean_Meditate >= 4.01f && mean_Meditate <= 5.0f)
                {
                    mTarget = 4.5f;
                    mOut = 6.5f;
                    Debug.Log("mTrain: 4.5-6.5");
                }

                MeditateMeter.GetComponent<PieMeter>().MinValuec1 = mOut;
                MeditateMeter.GetComponent<PieMeter>().MinValuec2 = mOut;
                MeditateMeter.GetComponent<PieMeter>().MaxValuec1 = mTarget;
                MeditateMeter.GetComponent<PieMeter>().MaxValuec2 = mTarget;

                meditateThresholdTimer = thresholdCounter;
                m_trainSW = true;
                Debug.Log("meditateTrained");
            }
        }
    }
}
