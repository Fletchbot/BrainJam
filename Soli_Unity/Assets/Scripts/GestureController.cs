using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniOSC;
namespace SoliGameController
{
    public class GestureController : MonoBehaviour
    {
        GameController gc;
        [Header("Wekinator")]
        public GameObject WekOSC_Receiver;
        public float wek_mFloat, wek_fFloat, wek_hFloat, wek_sFloat, wek_mood, wek_facialExpression, happyDiff, sadDiff;
        public bool isMeditate, isFocus, isHappy, isSad, isUnsure, wekisFocus;
        [Header("Gesture Parameters")]
        public float mTarget, mOut, fTarget, fOut, hTarget, sTarget, h_guiVal, s_guiVal;
        public float fprevFloat, fcurrFloat, fVelocity;
        [Header("Timer Section")]
        public float meditateCountdown, focusCountdown;
        public float unsureCountdown, happyCountdown, sadCountdown;
        private float sixtysecCounter, thirtysecCounter, tensecCounter, fivesecCounter, foursecCounter, threesecCounter, twosecCounter, secCounter, halfsecCounter;

        // Use this for initialization
        public void OnEnable()
        {
            gc = this.GetComponent<GameController>();

            sixtysecCounter = 60.0f;
            thirtysecCounter = 30.0f;
            tensecCounter = 10.0f;
            fivesecCounter = 5.0f;
            foursecCounter = 4.0f;
            threesecCounter = 3.0f;
            twosecCounter = 2.0f;
            secCounter = 1.0f;
            halfsecCounter = 0.2f;

            resetValues();

        }
        // Update is called once per frame
        public void Update()
        {
            if (gc.HeadsetOn == 1)
            {
                UpdateMuseHeadset();
                MeditateStates();
                EmotionStates();
                FocusStates();
            }
            else if (gc.HeadsetOn == 0 && gc.isWekRun)
            {
                resetValues();
            }

        }

        public void resetValues()
        {
            meditateCountdown = twosecCounter;
            focusCountdown = secCounter;

            unsureCountdown = twosecCounter;
            happyCountdown = twosecCounter;
            sadCountdown = twosecCounter;

            mTarget = 4.0f;
            mOut = 7.0f;
            fTarget = 3.0f;
            fOut = 3.5f;
            hTarget = 0.8f;
            sTarget = 1.5f;
        }

        public void UpdateMuseHeadset()
        {
            wek_mFloat = WekOSC_Receiver.GetComponent<UniOSCWekOutputReceiver>().meditateFloat;
            wek_fFloat = WekOSC_Receiver.GetComponent<UniOSCWekOutputReceiver>().focusFloat;
            wekisFocus = WekOSC_Receiver.GetComponent<UniOSCWekOutputReceiver>().isFocus;
            wek_hFloat = WekOSC_Receiver.GetComponent<UniOSCWekOutputReceiver>().happyFloat;
            wek_sFloat = WekOSC_Receiver.GetComponent<UniOSCWekOutputReceiver>().sadFloat;
            wek_mood = WekOSC_Receiver.GetComponent<UniOSCWekOutputReceiver>().mood;
            wek_facialExpression = WekOSC_Receiver.GetComponent<UniOSCWekOutputReceiver>().facialExpression;
        }

        public void MeditateStates()
        {
            if (wek_mFloat <= mTarget)
            {
                if (meditateCountdown <= 0.0f)
                {
                    isMeditate = true;
                    meditateCountdown = twosecCounter;
                }
                else
                {
                    meditateCountdown -= Time.deltaTime;
                }
            }
            else if (wek_mFloat >= mOut)
            {
                if (meditateCountdown <= 0.0f)
                {
                    isMeditate = false;
                    meditateCountdown = twosecCounter;
                }
                else
                {
                    meditateCountdown -= Time.deltaTime;
                }
            }
        }

        public void FocusStates()
        {
            //velocity
            if (focusCountdown == halfsecCounter)
            {
                fcurrFloat = wek_fFloat;
            }
            else if (focusCountdown < 0.0f)
            {
                fprevFloat = wek_fFloat;
                focusCountdown = secCounter;
            }

            if (focusCountdown >= 0.0f)
            {
                focusCountdown -= Time.deltaTime;
            }

            //isFocus
            if (wek_fFloat <= fTarget || wekisFocus)
            {
                    if (!isFocus)
                    {
                        isFocus = true;
                    }
             }
             else if (wekisFocus && wek_fFloat <= fTarget + 2.0f)
             {
                    isFocus = true;

            }
            else if (wek_fFloat >= fOut || !wekisFocus)
            {
                isFocus = false;
            }

            fVelocity = fprevFloat - fcurrFloat;


        }

        public void EmotionStates()
        {
            //HAPPY
            happyDiff = wek_sFloat - wek_hFloat;
            sadDiff = wek_hFloat - wek_sFloat;

            if (happyDiff >= hTarget && wek_hFloat <= 5.0f && happyCountdown <= 0.0f)
            {
                isSad = false;
                isHappy = true;
                happyCountdown = twosecCounter;
                h_guiVal = 2.5f;
                s_guiVal = 0.0f;
            }
            else if (happyDiff >= (hTarget - 0.3f) && wek_mood == 2 && wek_facialExpression == 2 || happyDiff >= (hTarget - 0.3f) && wek_mood == 2 || happyDiff >= (hTarget - 0.3f) && wek_facialExpression == 2)
            {
                isSad = false;
                isHappy = true;
                happyCountdown = twosecCounter;
                s_guiVal = 0.0f;
                h_guiVal = 2.5f;
            }
            else if (happyDiff <= 0.2f && happyCountdown <= 0.0f || wek_hFloat >= 5.5f)
            {
                happyCountdown = twosecCounter;
                isHappy = false;
                h_guiVal = 0.0f;
            }
            else if (happyDiff >= hTarget || happyDiff <= 0.3f)
            {
                if (happyDiff >= hTarget)
                {
                    h_guiVal += Time.deltaTime;
                }
                else if (happyDiff <= 0.3f)
                {
                    h_guiVal -= Time.deltaTime;
                }
                happyCountdown -= Time.deltaTime;
            }


            //SAD
            if (sadDiff >= sTarget && wek_sFloat <= 5.0f && sadCountdown <= 0.0f)
            {
                isHappy = false;
                isSad = true;
                sadCountdown = twosecCounter;
                h_guiVal = 0.0f;
                s_guiVal = 2.5f;
            }
            else if (sadDiff >= (sTarget - 0.3f) && wek_mood == 3 && wek_facialExpression == 3 || sadDiff >= (sTarget - 0.3f) && wek_mood == 3 || sadDiff >= (sTarget - 0.3f) && wek_facialExpression == 3)
            {
                isHappy = false;
                isSad = true;
                sadCountdown = twosecCounter;
                h_guiVal = 0.0f;
                s_guiVal = 2.5f;
            }
            else if (sadDiff <= 0.3f && sadCountdown <= 0.0f || wek_sFloat >= 5.5f)
            {
                sadCountdown = twosecCounter;
                isSad = false;
                h_guiVal = 0.0f;
            }
            else if (sadDiff >= sTarget || sadDiff <= 0.4f)
            {
                if (sadDiff >= sTarget)
                {
                    s_guiVal += Time.deltaTime;
                }
                else if (sadDiff <= 0.4f)
                {
                    s_guiVal -= Time.deltaTime;
                }
                sadCountdown -= Time.deltaTime;
            }

            //UNSURE
            if (!isHappy && !isSad && !isUnsure || wek_mood == 1 && wek_facialExpression == 1 && happyDiff <= hTarget && sadDiff <= sTarget)
            {
                if (unsureCountdown <= 0.0f)
                {
                    isUnsure = true;
                    isHappy = false;
                    isSad = false;
                    unsureCountdown = twosecCounter;

                    h_guiVal = 0.0f;
                    s_guiVal = 0.0f;
                }
                else
                {
                    unsureCountdown -= Time.deltaTime;
                }
            }
            else if (isHappy && isUnsure || isSad && isUnsure)
            {
                isUnsure = false;
                if(isSad) isHappy = false;
                if(isHappy) isSad = false;
                unsureCountdown = twosecCounter;
            }
        }

        /*   public void SVMEmotionStates()
           {
               if (emotions == 1 && !isUnsure)
               {
                   if (unsureCountdown <= 0)
                   {
                       isHappy = false;
                       isSad = false;
                       isUnsure = true;
                       unsureCountdown = secCounter;
                       Debug.Log("isUnsure");
                   }
                   else
                   {
                       unsureCountdown -= Time.deltaTime;

                       happyCountdown = twosecCounter;
                       sadCountdown = twosecCounter;
                   }
               }
               else if (emotions == 2 && !isHappy && !isMeditate)
               {
                   if (happyCountdown <= 0)
                   {
                       isHappy = true;
                       isSad = false;
                       isUnsure = false;
                       happyCountdown = twosecCounter;
                       Debug.Log("isHappy");
                   }
                   else
                   {
                       happyCountdown -= Time.deltaTime;

                       unsureCountdown = secCounter;
                       sadCountdown = twosecCounter;
                   }

               }
               else if (emotions == 3 && !isSad && !isMeditate)
               {
                   if (sadCountdown <= 0)
                   {
                       isHappy = false;
                       isSad = true;
                       isUnsure = false;
                       sadCountdown = twosecCounter;
                       Debug.Log("isSad");
                   }
                   else
                   {
                       sadCountdown -= Time.deltaTime;

                       happyCountdown = twosecCounter;
                       unsureCountdown = secCounter;
                   }
               }
           }
       */

    }
}
