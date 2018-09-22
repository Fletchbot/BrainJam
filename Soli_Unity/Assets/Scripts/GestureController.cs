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
        public bool isMeditate, isFocus, isHappy, isSad, isUnsure, wekisFocus, reset, wek_isM;
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

                if (reset) reset = false;
            }
            else if (gc.HeadsetOn == 0 && !reset)
            {
                resetValues();
                reset = true;
            }
            

        }

        public void resetValues()
        {
            meditateCountdown = twosecCounter;
            focusCountdown = secCounter;
            unsureCountdown = secCounter;
            happyCountdown = secCounter;
            sadCountdown = secCounter;

            mTarget = 4.0f;
            mOut = 6.0f;
            fTarget = 2.0f;
            fOut = 2.7f;
            hTarget = 0.8f;
            sTarget = 1.0f;
        }

        public void UpdateMuseHeadset()
        {
            wek_mFloat = WekOSC_Receiver.GetComponent<UniOSCWekOutputReceiver>().meditateFloat;
            wek_isM = WekOSC_Receiver.GetComponent<UniOSCWekOutputReceiver>().isMeditate;
            wek_fFloat = WekOSC_Receiver.GetComponent<UniOSCWekOutputReceiver>().focusFloat;
            wekisFocus = WekOSC_Receiver.GetComponent<UniOSCWekOutputReceiver>().isFocus;
            wek_hFloat = WekOSC_Receiver.GetComponent<UniOSCWekOutputReceiver>().happyFloat;
            wek_sFloat = WekOSC_Receiver.GetComponent<UniOSCWekOutputReceiver>().sadFloat;
            wek_mood = WekOSC_Receiver.GetComponent<UniOSCWekOutputReceiver>().mood;
            wek_facialExpression = WekOSC_Receiver.GetComponent<UniOSCWekOutputReceiver>().facialExpression;
        }

        public void MeditateStates()
        {
            if (wek_mFloat <= mTarget || wek_isM)
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
                if (wek_isM) isMeditate = true;
            }
            else if (wek_mFloat >= mOut && !wek_isM)
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

            fVelocity = fprevFloat - fcurrFloat;

            //isFocus
            if (wek_fFloat <= fTarget)
            {
                    if (!isFocus)
                    {
                        isFocus = true;
                    }
             }
             else if (wekisFocus && wek_fFloat <= (fTarget + 2.0f))
             {
                    isFocus = true;

            }
            else if (wek_fFloat >= fOut || !wekisFocus && wek_fFloat >= (fTarget + 2.0f))
            {
                isFocus = false;
            }

        }

        public void EmotionStates()
        {
            //HAPPY
            happyDiff = wek_sFloat - wek_hFloat;
            sadDiff = wek_hFloat - wek_sFloat;

            if (wek_mood == 2 && wek_facialExpression == 2)
            {
                isSad = false;
                isHappy = true;
                happyCountdown = secCounter;
                s_guiVal = 0.0f;
                h_guiVal = 2.5f;
            }
            else if (happyDiff >= (hTarget - 0.3f) && happyCountdown <= 0.0f && wek_mood == 2 || happyDiff >= (hTarget - 0.3f) && wek_facialExpression == 2 && happyCountdown <= 0.0f && wek_mood <= 2)
            {
                isSad = false;
                isHappy = true;
                happyCountdown = secCounter;
                h_guiVal = 2.5f;
                s_guiVal = 0.0f;
            }
            else if (happyDiff >= hTarget && happyCountdown >= 0.0f && wek_mood == 2 || happyDiff >= hTarget && wek_facialExpression == 2 && happyCountdown >= 0.0f && wek_mood <= 2)
            {
                happyCountdown -= Time.deltaTime;
            }           
            else if (happyDiff <= 0.2f && happyCountdown <= 0.0f || wek_hFloat >= 5.5f)
            {
                happyCountdown = secCounter;
                isHappy = false;
                h_guiVal = 0.0f;
            }
            else if (happyDiff <= 0.2f && happyCountdown >= 0.0f)
            {
                happyCountdown -= Time.deltaTime;
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
            }


            //SAD
            if (wek_mood == 3 && wek_facialExpression == 3)
            {
                isHappy = false;
                isSad = true;
                sadCountdown = secCounter;
                h_guiVal = 0.0f;
                s_guiVal = 2.5f;
            }
            else if (sadDiff >= sTarget && sadCountdown <= 0.0f && wek_mood == 3 || sadDiff >= sTarget && sadCountdown <= 0.0f && wek_facialExpression == 3 && wek_mood == 1 || sadDiff >= sTarget && sadCountdown <= 0.0f && wek_facialExpression == 3 && wek_mood == 3)
            {
                isHappy = false;
                isSad = true;
                sadCountdown = secCounter;
                h_guiVal = 0.0f;
                s_guiVal = 2.5f;
            }
            else if (sadDiff >= sTarget && sadCountdown >= 0.0f && wek_mood == 3 || sadDiff >= sTarget && sadCountdown >= 0.0f && wek_facialExpression == 3 && wek_mood == 1 || sadDiff >= sTarget && sadCountdown >= 0.0f && wek_facialExpression == 3 && wek_mood == 3)
            {
                sadCountdown -= Time.deltaTime;
            }
            else if (sadDiff <= 0.3f && sadCountdown <= 0.0f || wek_sFloat >= 5.5f)
            {
                sadCountdown = secCounter;
                isSad = false;
                s_guiVal = 0.0f;
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
            }

            //UNSURE
            if (!isHappy && !isSad && !isUnsure || wek_mood == 1 && wek_facialExpression == 1 && happyDiff <= hTarget && sadDiff <= sTarget)
            {
                if (unsureCountdown <= 0.0f)
                {
                    isUnsure = true;
                    isHappy = false;
                    isSad = false;
                    unsureCountdown = secCounter;

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
                unsureCountdown = secCounter;
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
