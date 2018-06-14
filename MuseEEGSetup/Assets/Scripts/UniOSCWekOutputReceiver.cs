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
        public bool isGesture1, isGesture2, isGesture3, isGesture4, isGesture5;
        public float[] nnet = new float[12];

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
                    gestureFloats[0] = (float)msg.Data[0];
                    gestureFloats[1] = (float)msg.Data[1];
                    gestureFloats[2] = (float)msg.Data[2];
                    gestureFloats[3] = (float)msg.Data[3];
                    gestureFloats[4] = (float)msg.Data[4];
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
                    nnet[0] = (float)msg.Data[0];
                    nnet[1] = (float)msg.Data[1];
                    nnet[2] = (float)msg.Data[2];
                    nnet[3] = (float)msg.Data[3];
                    nnet[4] = (float)msg.Data[4];
                    nnet[5] = (float)msg.Data[5];
                    nnet[6] = (float)msg.Data[6];
                    nnet[7] = (float)msg.Data[7];
                    nnet[8] = (float)msg.Data[8];
                    nnet[9] = (float)msg.Data[9];
                    nnet[10] = (float)msg.Data[10];
                    nnet[11] = (float)msg.Data[11];
                }
            }
        }
        void Switch()
        {
            if (isGesture1 == true) isGesture1 = false;
            if (isGesture2 == true) isGesture2 = false;
            if (isGesture3 == true) isGesture3 = false;
            if (isGesture4 == true) isGesture4 = false;
            if (isGesture5 == true) isGesture5 = false;
        }
    }
}
