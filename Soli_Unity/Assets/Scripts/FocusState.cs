using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SoliGameController
{
    public class FocusState : MonoBehaviour
    {
        GameController gc;
        public GameObject FocusMeter;
        AudioPlaytestManager au;

        public bool wek_isF, isFocus, f_trainSW, f_meterSW, enableState, reset;
        public float wek_fFloat, fTarget, fOut, targetOffset;
        public float fcurrFloat, fprevFloat, fVelocity;
        public float focusCountdown, counter;
        public float prevT_Focus, currT_Focus, mean_Focus, focusThresholdTimer, thresholdCounter;
        private Vector3 focusGamePos;
        private Vector2 middleAnchorMin, middleAnchorMax, topRightAnchorMin, topRightAnchorMax;
        public Color focusTransColor, focusActiveColor, focusDeactiveColor, lerpedFCol;
        private bool fCol_SW, fTrans_SW, fLerp_SW, fDeactiveCol_SW;

        public float trainCountdown, trainCounter;
        public int traincount;
        public bool train_on, train_sw;
        // Use this for initialization

        public void resetValues()
        {
            counter = 2.0f;
            focusCountdown = counter;
            thresholdCounter = 3.0f;
            focusThresholdTimer = thresholdCounter;

            fTarget = 1.5f;
            fOut = 2.5f;
            targetOffset = 2.0f;

            fcurrFloat = 0.0f;
            fprevFloat = 0.0f;
            fVelocity = 0.0f;

            FocusMeter.GetComponent<PieMeter>().MinValuec1 = fOut;
            FocusMeter.GetComponent<PieMeter>().MinValuec2 = fOut;
            FocusMeter.GetComponent<PieMeter>().MaxValuec1 = fTarget;
            FocusMeter.GetComponent<PieMeter>().MaxValuec2 = fTarget;

            f_trainSW = false;
            f_meterSW = false;

            trainCounter = 4.0f;
            trainCountdown = trainCounter;
            train_sw = false;
            train_on = false;
        }

        public void f_MeterReset()
        {
            FocusMeter.GetComponent<ActivateObjects>().SetDeactive(true);
        }

        public void OnEnable()
        {
            gc = this.GetComponent<GameController>();
            au = this.GetComponent<AudioPlaytestManager>();

            focusTransColor = new Color32(120, 200, 100, 100);
            focusActiveColor = new Color32(80, 200, 50, 200);
            focusDeactiveColor = new Color32(120, 200, 100, 0);
            focusGamePos = new Vector3(-60, -60, 0);

            middleAnchorMin = new Vector2(0.5f, 0.5f);
            middleAnchorMax = new Vector2(0.5f, 0.5f);
            topRightAnchorMin = new Vector2(1f, 1f);
            topRightAnchorMax = new Vector2(1f, 1f);

            resetValues();
        }

        void Update()
        {
            wek_fFloat = gc.wek_fFloat;
            wek_isF = gc.wek_isF;
            enableState = gc.isRunning;

            if (enableState)
            {
                FocusTraining();
                FocusStates();
                f_MeterUpdate();
                if (reset) reset = false;
            }
            else if (!enableState && !reset)
            {
                resetValues();
                f_MeterReset();
                reset = true;
            }
        }

        public void f_MeterUpdate()
        {
            if (gc.state == 4 && f_meterSW)
            {
                //place focusmeter to middle & scale up
                FocusMeter.GetComponent<ActivateObjects>().SetActive(true);
                FocusMeter.transform.localScale = new Vector3(2, 2, 2);
                FocusMeter.GetComponent<RectTransform>().anchorMin = new Vector2(middleAnchorMin.x, middleAnchorMin.y);
                FocusMeter.GetComponent<RectTransform>().anchorMax = new Vector2(middleAnchorMax.x, middleAnchorMax.y);
                FocusMeter.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
            }
            else if (gc.state == 5 && f_meterSW)
            {
                //place focusmeter to game pos & scale down
                FocusMeter.transform.localScale = new Vector3(1, 1, 1);
                FocusMeter.GetComponent<RectTransform>().localPosition = new Vector3(focusGamePos.x, focusGamePos.y, 0);
                FocusMeter.GetComponent<RectTransform>().anchorMin = new Vector2(topRightAnchorMin.x, topRightAnchorMin.y);
                FocusMeter.GetComponent<RectTransform>().anchorMax = new Vector2(topRightAnchorMax.x, topRightAnchorMax.y);
                f_meterSW = false;
            }
            //-----------------------------------------------------------------------------------------------------------------
            //FOCUS COLOUR CHANGES
            FocusMeter.GetComponent<PieMeter>().Valuec1 = gc.wek_fFloat;
            FocusMeter.GetComponent<PieMeter>().Valuec2 = gc.wek_fFloat;

            if (isFocus)
            {
                FocusMeter.GetComponent<PieMeter>().fillersemiC1.color = focusActiveColor;
                FocusMeter.GetComponent<PieMeter>().fillersemiC2.color = focusActiveColor;
            }
            else if (wek_fFloat >= fOut)
            {
                FocusMeter.GetComponent<PieMeter>().fillersemiC1.color = focusDeactiveColor;
                FocusMeter.GetComponent<PieMeter>().fillersemiC2.color = focusDeactiveColor;
            }
            else if (wek_fFloat <= fOut && wek_fFloat >= fTarget)
            {
                lerpedFCol = Color.Lerp(focusDeactiveColor, focusTransColor, Mathf.PingPong(Time.time, 1));
                FocusMeter.GetComponent<PieMeter>().fillersemiC1.color = lerpedFCol;
                FocusMeter.GetComponent<PieMeter>().fillersemiC2.color = lerpedFCol;
            }
        }

        public void FocusStates()
        {
            //velocity
            if (focusCountdown >= 0.9f && focusCountdown <= 1.0f)
            {
                fcurrFloat = wek_fFloat;
            }
            else if (focusCountdown <= 0.0f)
            {
                fprevFloat = wek_fFloat;
                focusCountdown = counter;
            }

            if (focusCountdown >= 0.0f)
            {
                focusCountdown -= Time.deltaTime;
            }

            fVelocity = fprevFloat - fcurrFloat;

            //isFocus
            if (wek_fFloat <= fTarget)
            {
                if (!isFocus)
                {
                    isFocus = true;
                }
            }
            else if (wek_isF && wek_fFloat <= (fTarget + targetOffset))
            {
                isFocus = true;

            }
            else if (wek_fFloat >= fOut || !wek_isF && wek_fFloat >= (fTarget + targetOffset))
            {
                isFocus = false;
            }
        }

        public void FocusTraining() //state 4 & 5
        {
            if (gc.state == 4) //FOCUS TRAINING INTRO
            {
                if (!train_sw) // place/scale emotions to gamepos and place and scale focusmeter in middle of screen
                {
                    f_meterSW = true;
                    train_sw = true;
                }

                if (au.N_Intro >= 7)
                {
                    if (isFocus && !train_on)
                    {
                        traincount++;
                        train_on = true;
                    }
                    else if (!isFocus && train_on)
                    {
                        train_on = false;
                    }
                }

                if (au.N_Intro == 8 && trainCountdown >= 0.0f && !f_trainSW)
                {
                    FocusDifficultyLevel();
                    trainCountdown -= Time.deltaTime;
                }
                else if (au.N_Intro == 8 && traincount >= 2 && f_trainSW)
                {
                    train_sw = false;
                    f_trainSW = false;
                    trainCountdown = trainCounter;
                    gc.state++;
                }
            }
            else if (gc.state == 5)
            {
                if (au.N_Intro == 9)
                {
                    gc.state = -1;
                }
            }
        }

        public void FocusDifficultyLevel()
        {
            if (focusThresholdTimer == thresholdCounter)
            {
                prevT_Focus = wek_fFloat;
                focusThresholdTimer -= Time.deltaTime;
            }
            else if (focusThresholdTimer >= 0.0f)
            {
                focusThresholdTimer -= Time.deltaTime;
            }
            else if (focusThresholdTimer <= 0.0f)
            {
                currT_Focus = wek_fFloat;
                mean_Focus = (prevT_Focus + currT_Focus) / 2;

                if (mean_Focus >= 8.51f)
                {
                    fTarget = 8.5f;
                    fOut = 9.5f;
                    Debug.Log("fTrain: 8.5-9.5");
                }
                else if (mean_Focus >= 7.51f && mean_Focus <= 8.5f)
                {
                    fTarget = 7.5f;
                    fOut = 8.5f;
                    Debug.Log("fTrain: 7.5-8.5");
                }
                else if (mean_Focus >= 6.51f && mean_Focus <= 7.5f)
                {
                    fTarget = 6.5f;
                    fOut = 7.5f;
                    Debug.Log("fTrain: 6.5-7.5");
                }
                else if (mean_Focus >= 5.51f && mean_Focus <= 6.5f)
                {
                    fTarget = 5.5f;
                    fOut = 6.5f;
                    Debug.Log("fTrain: 5.5-6.5");
                }
                else if (mean_Focus >= 4.51f && mean_Focus <= 5.5f)
                {
                    fTarget = 4.5f;
                    fOut = 5.5f;
                    Debug.Log("fTrain: 4.5-5.5");
                }
                else if (mean_Focus >= 3.51f && mean_Focus <= 4.5f)
                {
                    fTarget = 3.5f;
                    fOut = 4.5f;
                    Debug.Log("fTrain: 3.5-4.5");
                }
                else if (mean_Focus >= 1.51f && mean_Focus <= 3.5f)
                {
                    fTarget = 2.5f;
                    fOut = 3.5f;
                    Debug.Log("fTrain: 2.5-3.5");
                }

                FocusMeter.GetComponent<PieMeter>().MinValuec1 = fOut;
                FocusMeter.GetComponent<PieMeter>().MinValuec2 = fOut;
                FocusMeter.GetComponent<PieMeter>().MaxValuec1 = fTarget;
                FocusMeter.GetComponent<PieMeter>().MaxValuec2 = fTarget;

                focusThresholdTimer = thresholdCounter;
                f_trainSW = true;
                Debug.Log("FocusTrained");
            }
        }
    }
}
