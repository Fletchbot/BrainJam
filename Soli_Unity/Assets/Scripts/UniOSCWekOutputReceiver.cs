using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using OSCsharp.Data;

namespace UniOSC
{

    /// <summary>
    /// Muse EEG Headset: Wekinator OSC Manager
    /// </summary>
    [AddComponentMenu("UniOSC/WekInputReceiver")]
    public class UniOSCWekOutputReceiver : UniOSCEventTarget
    {
        public string[] addressArray;
        
        public bool isMeditate, isFocus, isHappy, isSad;
        public float meditateFloat, focusFloat, mood, facialExpression ,happyFloat, sadFloat, unsureFloat;

        public static UniOSCWekOutputReceiver main;

        void Awake()
        {
            main = this;
        }

        public override void OnEnable()
        {
            _Init();
            base.OnEnable();
        }

        private void _Init()
        {

            //receiveAllAddresses = false;
            _oscAddresses.Clear();
            if (!_receiveAllAddresses)
            {
                if (addressArray != null)
                {
                    foreach (var d in addressArray)
                    {
                        if (String.IsNullOrEmpty(d)) continue;
                        _oscAddresses.Add(d);
                    }
                }
            }
        }

        public override void OnOSCMessageReceived(UniOSCEventArgs args)
        {
            //args.Address
            //(OscMessage)args.Packet) => The OscMessage object
            //(OscMessage)args.Packet).Data  (get the data of the OscMessage as an object[] array)
            OscMessage msg = (OscMessage)args.Packet;

            //if (msg.Data.Count < 1)
            //	return;
            //if (!(msg.Data [0] is System.Single))
            //	return;

            if (addressArray != null)
                {
                if (String.Equals(args.Address, addressArray[0])) //dtwMeditate
                    {
                        meditateFloat = (float)msg.Data[0];
                    }
                if (String.Equals(args.Address, addressArray[1])) //meditate bool
                    {
                        isMeditate = true;
                        Invoke("mSwitch", 0.3f);
                    }

                if (String.Equals(args.Address, addressArray[2])) //dtwFocus
                {
                    focusFloat = (float)msg.Data[0];
                }
                if (String.Equals(args.Address, addressArray[3])) //focus bool
                {
                    isFocus = true;
                    Invoke("fSwitch", 0.3f);
                }
                if (String.Equals(args.Address, addressArray[4])) //svm emotions
                {
                    mood = (float)msg.Data[0];
                    facialExpression = (float)msg.Data[1];
                }
                if (String.Equals(args.Address, addressArray[5])) //dtw emotions
                {
                    happyFloat = (float)msg.Data[0];
                    sadFloat = (float)msg.Data[1];
                }
                if (String.Equals(args.Address, addressArray[6])) //dtw happy bool
                {
                    isHappy = true;
                    Invoke("hSwitch", 0.3f);
                }
                if (String.Equals(args.Address, addressArray[7])) //dtw sad bool
                {
                    isSad = true;
                    Invoke("sSwitch", 0.3f);
                }

            }
        }
        void mSwitch()
        {
            if (isMeditate == true) isMeditate = false;

        }
        void fSwitch()
        {
            if (isFocus == true) isFocus = false;
        }

        void hSwitch()
        {
            if (isHappy == true) isHappy = false;

        }
        void sSwitch()
        {
            if (isSad == true) isSad = false;

        }

    }
}
