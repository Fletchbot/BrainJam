using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SoliGameController
{
    public class EmotionState : MonoBehaviour
    {
        GameController gc;
        public GameObject EmotionMeter;
        AudioPlaytestManager au;

        public float wek_hFloat, wek_sFloat, wek_mood, wek_facialExpression, happyDiff, sadDiff, emotionThresholdTimer, thresholdCounter;
        public float unsureCountdown, happyCountdown, sadCountdown, counter;
        public float hTarget, sTarget, h_guiVal, s_guiVal;
        public bool isHappy, isSad, isUnsure, h_trainSW, s_trainSW, e_meterSW, enableState, reset;
        public float prevT_happy, currT_happy, mean_happy, prevT_sad, currT_sad, mean_sad;
        private Vector3 emotionsGamePos;
        private Vector2 middleAnchorMin, middleAnchorMax, topRightAnchorMin, topRightAnchorMax;
        public Color sadTransColor, sadActiveColor, sadDeactiveColor, lerpedSCol, happyTransColor, happyActiveColor, happyDeactiveColor, lerpedHCol;
        private bool sCol_SW, sTrans_SW, sLerp_SW, emoDeactiveCol_SW, hCol_SW, hTrans_SW, hLerp_SW;

        public float trainCountdown, trainCounter;
        public bool train_sw, happy;

        public void resetValues()
        {
            counter = 1.0f;
            happyCountdown = counter;
            sadCountdown = counter;
            unsureCountdown = counter;
            thresholdCounter = 2.0f;
            emotionThresholdTimer = thresholdCounter;

            hTarget = 1.0f;
            sTarget = 1.8f;

            EmotionMeter.GetComponent<PieMeter>().MinValuec1 = 0.0f;
            EmotionMeter.GetComponent<PieMeter>().MinValuec2 = 0.0f;
            EmotionMeter.GetComponent<PieMeter>().MaxValuec1 = 2.5f;
            EmotionMeter.GetComponent<PieMeter>().MaxValuec2 = 2.5f;

            e_meterSW = false;
            h_trainSW = false;
            s_trainSW = false;

            trainCounter = 2.5f;
            trainCountdown = trainCounter;
            train_sw = false;
            happy = false;
        }

        public void e_MeterReset()
        {
            EmotionMeter.GetComponent<ActivateObjects>().SetDeactive(true);
        }

        // Use this for initialization
        public void OnEnable()
        {
            gc = this.GetComponent<GameController>();
            au = this.GetComponent<AudioPlaytestManager>();

            happyTransColor = new Color32(220, 160, 30, 100);
            happyActiveColor = new Color32(220, 220, 30, 200);
            happyDeactiveColor = new Color32(220, 220, 30, 0);
            sadTransColor = new Color32(220, 80, 30, 100);
            sadActiveColor = new Color32(220, 40, 30, 200);
            sadDeactiveColor = new Color32(220, 40, 30, 0);
            emotionsGamePos = new Vector3(-150, -60, 0);

            middleAnchorMin = new Vector2(0.5f, 0.5f);
            middleAnchorMax = new Vector2(0.5f, 0.5f);
            topRightAnchorMin = new Vector2(1f, 1f);
            topRightAnchorMax = new Vector2(1f, 1f);

            resetValues();
        }


        // Update is called once per frame
        void Update()
        {
            wek_hFloat = gc.wek_hFloat;
            wek_sFloat = gc.wek_sFloat;
            wek_mood = gc.wek_mood;
            wek_facialExpression = gc.wek_facialExpression;
            happyDiff = wek_sFloat - wek_hFloat;
            sadDiff = wek_hFloat - wek_sFloat;

            enableState = gc.isRunning;

            if (enableState)
            {
                EmotionsTraining();
                HappyState();
                SadState();
                UnsureState();
                e_MeterUpdate();
                if (reset) reset = false;
            }
            else if (!enableState && !reset)
            {
                resetValues();
                reset = true;
            }
        }

        public void e_MeterUpdate()
        {
            if (gc.state == 2 && e_meterSW)
            {
                //place emotionmeter to middle & scale up
                EmotionMeter.GetComponent<ActivateObjects>().SetActive(true);
                EmotionMeter.transform.localScale = new Vector3(2, 2, 2);
                EmotionMeter.GetComponent<RectTransform>().anchorMin = new Vector2(middleAnchorMin.x, middleAnchorMin.y);
                EmotionMeter.GetComponent<RectTransform>().anchorMax = new Vector2(middleAnchorMax.x, middleAnchorMax.y);
                EmotionMeter.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
            }
            else if (gc.state == 4 && e_meterSW)
            {
                //place emotionmeter to game pos & scale down
                EmotionMeter.transform.localScale = new Vector3(1, 1, 1);
                EmotionMeter.GetComponent<RectTransform>().localPosition = new Vector3(emotionsGamePos.x, emotionsGamePos.y, 0);
                EmotionMeter.GetComponent<RectTransform>().anchorMin = new Vector2(topRightAnchorMin.x, topRightAnchorMin.y);
                EmotionMeter.GetComponent<RectTransform>().anchorMax = new Vector2(topRightAnchorMax.x, topRightAnchorMax.y);
                e_meterSW = false;
            }
            //-------------------------------------------------------------------------------------------------------------
            //EMOTION COLOUR CHANGES
            EmotionMeter.GetComponent<PieMeter>().Valuec1 = h_guiVal;
            EmotionMeter.GetComponent<PieMeter>().Valuec2 = s_guiVal;

            if (isHappy)
            {
                EmotionMeter.GetComponent<PieMeter>().fillersemiC1.color = happyActiveColor;
            }
            else if (!isHappy)
            {
                if (happyDiff >= hTarget && !isSad)
                {
                    lerpedHCol = Color.Lerp(happyTransColor, happyActiveColor, Mathf.PingPong(Time.time, 0.5f));
                    EmotionMeter.GetComponent<PieMeter>().fillersemiC1.color = lerpedHCol;
                }
                else
                {
                    EmotionMeter.GetComponent<PieMeter>().fillersemiC1.color = happyDeactiveColor;
                }
            }
            else if (isSad)
            {
                EmotionMeter.GetComponent<PieMeter>().fillersemiC2.color = sadActiveColor;
            }
            else if (!isSad)
            {
                if (sadDiff >= sTarget && !isHappy)
                {
                    lerpedSCol = Color.Lerp(sadTransColor, sadActiveColor, Mathf.PingPong(Time.time, 0.5f));
                    EmotionMeter.GetComponent<PieMeter>().fillersemiC2.color = lerpedSCol;
                }
                else
                {
                    EmotionMeter.GetComponent<PieMeter>().fillersemiC1.color = happyDeactiveColor;
                }
            }
            else if (isUnsure)
            {
                EmotionMeter.GetComponent<PieMeter>().fillersemiC1.color = happyDeactiveColor;
                EmotionMeter.GetComponent<PieMeter>().fillersemiC2.color = sadDeactiveColor;
            }
        }

        public void HappyState()
        {
            if (wek_mood == 1 && wek_facialExpression == 1 && happyDiff >= hTarget && happyCountdown <= 0.0f ||wek_mood == 1 && wek_facialExpression == 2 && happyDiff >= hTarget && happyCountdown <= 0.0f || wek_mood == 2 && wek_facialExpression == 1 && happyDiff >= 0.0f && happyCountdown <= 0.0f  || wek_mood == 2 && wek_facialExpression == 2 && happyDiff >= 0.0f && happyCountdown <= 0.0f) 
            {
                isSad = false;
                isHappy = true;
                happyCountdown = counter;
                h_guiVal = 2.5f;
                s_guiVal = 0.0f;
            }
            else if (wek_mood == 1 && wek_facialExpression == 1 && happyDiff >= hTarget && happyCountdown >= 0.0f || wek_mood == 1 && wek_facialExpression == 2 && happyDiff >= hTarget && happyCountdown >= 0.0f || wek_mood == 2 && wek_facialExpression == 1 && happyDiff >= 0.0f && happyCountdown >= 0.0f  || wek_mood == 2 && wek_facialExpression == 2 && happyDiff >= 0.0f && happyCountdown >= 0.0f)
            {
                happyCountdown -= Time.deltaTime;
                h_guiVal += Time.deltaTime;
            }
            else if (happyDiff <= 0.0f && happyCountdown <= 0.0f || wek_hFloat >= 6.0f)
            {
                happyCountdown = counter;
                isHappy = false;
                h_guiVal = 0.0f;
            }
            else if (happyDiff <= 0.0f && happyCountdown >= 0.0f)
            {
                happyCountdown -= Time.deltaTime;
                h_guiVal -= Time.deltaTime;
            }
            else
            {
                happyCountdown = counter;
            }
        }

        public void SadState()
        {
            if (wek_mood == 1 && wek_facialExpression == 1 && sadDiff >= sTarget && sadCountdown <= 0.0f || wek_mood == 1 && wek_facialExpression == 3 && sadDiff >= sTarget && sadCountdown <= 0.0f || wek_mood == 3 && wek_facialExpression == 1 && sadDiff >= 0.0f && sadCountdown <= 0.0f || wek_mood == 3 && wek_facialExpression == 3 && sadDiff >= 0.0f && sadCountdown <= 0.0f)
            {
                isSad = true;
                isHappy = false;
                sadCountdown = counter;
                s_guiVal = 2.5f;
                h_guiVal = 0.0f;
            }
            else if (wek_mood == 1 && wek_facialExpression == 1 && sadDiff >= sTarget && sadCountdown >= 0.0f || wek_mood == 1 && wek_facialExpression == 3 && sadDiff >= sTarget && sadCountdown >= 0.0f || wek_mood == 3 && wek_facialExpression == 1 && sadDiff >= 0.0f && sadCountdown >= 0.0f || wek_mood == 3 && wek_facialExpression == 3 && sadDiff >= 0.0f && sadCountdown >= 0.0f)
            {
                sadCountdown -= Time.deltaTime;
                s_guiVal += Time.deltaTime;
            }
            else if (sadDiff <= 0.0f && sadCountdown <= 0.0f || wek_sFloat >= 6.0f)
            {
                sadCountdown = counter;
                isSad = false;
                s_guiVal = 0.0f;
            }
            else if (sadDiff <= 0.0f && sadCountdown >= 0.0f)
            {
                sadCountdown -= Time.deltaTime;
                s_guiVal -= Time.deltaTime;
            }
            else
            {
                sadCountdown = counter;
            }
        }

        public void UnsureState()
        {
            //UNSURE
            if (!isHappy && !isSad && !isUnsure || wek_mood == 1 && wek_facialExpression == 1 && happyDiff <= hTarget && sadDiff <= sTarget || wek_mood == 1 && wek_facialExpression == 2 && happyDiff <= hTarget && sadDiff <= sTarget || wek_mood == 2 && wek_facialExpression == 1 && happyDiff <= hTarget && sadDiff <= sTarget || wek_mood == 2 && wek_facialExpression == 3 && happyDiff <= hTarget && sadDiff <= sTarget || wek_mood == 3 && wek_facialExpression == 2 && happyDiff <= hTarget && sadDiff <= sTarget)
            {
                if (unsureCountdown <= 0.0f)
                {
                    isUnsure = true;
                    isHappy = false;
                    isSad = false;
                    unsureCountdown = counter;

                    h_guiVal = 0.0f;
                    s_guiVal = 0.0f;
                }
                else if (unsureCountdown >= 0.0f)
                {
                    unsureCountdown -= Time.deltaTime;
                }
            }
            else if (isHappy && isUnsure || isSad && isUnsure)
            {
                isUnsure = false;
                if (isSad) isHappy = false;
                if (isHappy) isSad = false;
                unsureCountdown = counter;
            }
            else
            {
                unsureCountdown = counter;
            }
        }

        public void EmotionsTraining() //state 2 & 3 
        {
            if (gc.state == 2) //EMOTION TRAINING INTRO
            {
                if (!train_sw) // place/scale meditate to gamepos and place and scale emotionsmeter in middle of screen
                {
                    e_meterSW = true;
                    train_sw = true;
                }

                if (au.N_Intro == 4 && isHappy || au.N_Intro == 6 && isSad)
                {
                    if (au.N_Intro == 4 && isHappy && !happy)
                    {
                        trainCountdown = trainCounter;
                        h_trainSW = false;
                        happy = true;
                    }
                    else if (au.N_Intro == 6 && isSad && happy)
                    {
                        trainCountdown = trainCounter;
                        s_trainSW = false;
                        train_sw = false;
                        gc.state++;
                    }
                }
                else if (au.N_Intro == 4 && !isHappy && trainCountdown <= 0.0f && !h_trainSW)
                {
                    HappyDifficultyLevel();
                }
                else if (au.N_Intro == 6 && !isSad && trainCountdown <= 0.0f && !s_trainSW)
                {
                    SadDifficultyLevel();
                }
                else if (au.N_Intro == 4 && !isHappy && trainCountdown >= 0.0f || au.N_Intro == 6 && !isSad && trainCountdown >= 0.0f)
                {
                    trainCountdown -= Time.deltaTime;
                }

            }
        }
        public void HappyDifficultyLevel()
        {
            if(emotionThresholdTimer == thresholdCounter)
            {
                prevT_happy = happyDiff;
                emotionThresholdTimer -= Time.deltaTime;
            }
            else if (emotionThresholdTimer >= 0.0f)
            {
                emotionThresholdTimer -= Time.deltaTime;
            }
            else if (emotionThresholdTimer <= 0.0f)
            {
                currT_happy = happyDiff;
                mean_happy = (prevT_happy + currT_happy) / 2;

                if(mean_happy <= 0.2f)
                {
                    hTarget = 0.2f;
                    Debug.Log("hTrain: 0.2");
                }
                else if (mean_happy <= 0.5f)
                {
                    hTarget = 0.5f;
                    Debug.Log("hTrain: 0.5");
                }
                else if (mean_happy <= 0.8f)
                {
                    hTarget = 0.8f;
                    Debug.Log("hTrain: 0.8");
                }
                else if (mean_happy <= 1.0f)
                {
                    hTarget = 1.0f;
                    Debug.Log("hTrain: 1.0");
                }
                else if (mean_happy <= 1.5f)
                {
                    hTarget = 1.5f;
                    Debug.Log("hTrain: 1.5");
                }

                h_trainSW = true;
                emotionThresholdTimer = thresholdCounter;
                Debug.Log("happyTrained");
            }
        }
        public void SadDifficultyLevel()
        {
            if (emotionThresholdTimer == thresholdCounter)
            {
                prevT_sad = sadDiff;
                emotionThresholdTimer -= Time.deltaTime;
            }
            else if (emotionThresholdTimer >= 0.0f)
            {
                emotionThresholdTimer -= Time.deltaTime;
            }
            else if (emotionThresholdTimer <= 0.0f)
            {
                currT_sad = sadDiff;
                mean_sad = (prevT_sad + currT_sad) / 2;

                if (mean_sad <= 0.2f)
                {
                    sTarget = 0.2f;
                    Debug.Log("sTrain: 0.2");
                }
                else if (mean_sad <= 0.5f)
                {
                    sTarget = 0.5f;
                    Debug.Log("sTrain: 0.5");
                }
                else if (mean_sad <= 0.8f)
                {
                    sTarget = 0.8f;
                    Debug.Log("sTrain: 0.8");
                }
                else if (mean_sad <= 1.0f)
                {
                    sTarget = 1.0f;
                    Debug.Log("sTrain: 1.0");
                }
                else if (mean_sad <= 1.5f)
                {
                    sTarget = 1.5f;
                    Debug.Log("sTrain: 1.0");
                }
                else if (mean_sad <= 1.8f)
                {
                    sTarget = 1.8f;
                    Debug.Log("sTrain: 1.0");
                }

                s_trainSW = true;
                emotionThresholdTimer = thresholdCounter;
                Debug.Log("sadTrained");
            }
        }
    }
}
