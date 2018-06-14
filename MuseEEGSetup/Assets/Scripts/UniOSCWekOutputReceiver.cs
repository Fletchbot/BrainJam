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
        private string dtwoutputs;

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
        private void FixedUpdate()
        {

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
                    Debug.Log("yay!d0");
                }
                if (String.Equals(args.Address, addressArray[1]))
                {
                    Debug.Log("yay!d1");
                }
                if (String.Equals(args.Address, addressArray[2]))
                {
                    Debug.Log("yay!d2");
                }
                if (String.Equals(args.Address, addressArray[3]))
                {
                    Debug.Log("yay!d3");
                }
                if (String.Equals(args.Address, addressArray[4]))
                {
                    Debug.Log("yay!d4");
                }
                if (String.Equals(args.Address, addressArray[5]))
                {
                    Debug.Log("yay!d5");
                }
            }
        }
    }
}
