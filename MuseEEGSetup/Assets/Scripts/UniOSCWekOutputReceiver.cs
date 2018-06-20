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
        public float[] gestureFloats = new float[5];
        public bool isGesture1, isGesture2, isGesture3, isGesture4, isGesture5, isGesture6, isGesture7, isGesture8;
        public float[] nnet = new float[10];

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
                if (String.Equals(args.Address, addressArray[0]))
                {
                    for(int g = 0; g < msg.Data.Count; g++)
                    {
                        gestureFloats[g] = (float)msg.Data[g];
                    }                   
                }
                if (String.Equals(args.Address, addressArray[1]))
                {
                    isGesture1 = true;
                    //  if (isGesture2 == true || isGesture3 == true || isGesture4 == true || isGesture5 == true) Invoke("Switch", 0.3f);
                    Invoke("Switch", 0.3f);
                }
                if (String.Equals(args.Address, addressArray[2]))
                {
                    isGesture2 = true;
                    //   if(isGesture1 == true || isGesture3 == true || isGesture4 == true || isGesture5 == true) Invoke("Switch", 0.3f);
                    Invoke("Switch", 0.3f);
                }
                if (String.Equals(args.Address, addressArray[3]))
                {
                    isGesture3 = true;
                    // if (isGesture1 == true || isGesture2 == true || isGesture4 == true || isGesture5 == true) Invoke("Switch", 0.3f);
                    Invoke("Switch", 0.3f);
                }
                if (String.Equals(args.Address, addressArray[4]))
                {
                    isGesture4 = true;
                    //if (isGesture1 == true || isGesture2 == true || isGesture3 == true || isGesture5 == true ) Invoke("Switch", 0.3f);
                    Invoke("Switch", 0.3f);
                }
                if (String.Equals(args.Address, addressArray[5]))
                {
                    isGesture5 = true;
                    // if (isGesture1 == true || isGesture2 == true || isGesture3 == true || isGesture4 == true) Invoke("Switch", 0.3f);
                    Invoke("Switch", 0.3f);
                }
                if (String.Equals(args.Address, addressArray[6]))
                {
                    isGesture6 = true;
                    // if (isGesture1 == true || isGesture2 == true || isGesture3 == true || isGesture4 == true) Invoke("Switch", 0.3f);
                    Invoke("Switch", 0.3f);
                }
                if (String.Equals(args.Address, addressArray[7]))
                {
                    isGesture7 = true;
                    // if (isGesture1 == true || isGesture2 == true || isGesture3 == true || isGesture4 == true) Invoke("Switch", 0.3f);
                    Invoke("Switch", 0.3f);
                }
                if (String.Equals(args.Address, addressArray[8]))
                {
                    isGesture8 = true;
                    // if (isGesture1 == true || isGesture2 == true || isGesture3 == true || isGesture4 == true) Invoke("Switch", 0.3f);
                    Invoke("Switch", 0.3f);
                }
               // if (String.Equals(args.Address, addressArray[6]))
               // {
                 //   for(int n = 0; n < nnet.Length; n++)
                   // {
                     //   nnet[n] = (float)msg.Data[n];
                   // }
                //}
            }
        }
        void Switch()
        {
            if (isGesture1 == true) isGesture1 = false;
            if (isGesture2 == true) isGesture2 = false;
            if (isGesture3 == true) isGesture3 = false;
            if (isGesture4 == true) isGesture4 = false;
            if (isGesture5 == true) isGesture5 = false;
            if (isGesture6 == true) isGesture6 = false;
            if (isGesture7 == true) isGesture7 = false;
            if (isGesture8 == true) isGesture8 = false;
        }
    }
}
