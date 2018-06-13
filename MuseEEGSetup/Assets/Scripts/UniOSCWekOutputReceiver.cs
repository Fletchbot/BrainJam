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

        #region public
        public string wekOutputs_Address;
        public string isFocused_Address;
        public string notFocused_Address;
        public bool isFocused, notFocused;
        public float f, nf;
        //public float brainwave_absAvg, brainwave_relativeAvg, brainwave_abs, brainwave_relative, head_movement;
        public static UniOSCWekOutputReceiver main;
        #endregion

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
                _oscAddresses.Add(wekOutputs_Address);
                _oscAddresses.Add(isFocused_Address);
                _oscAddresses.Add(notFocused_Address);

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
            if (String.Equals(args.Address, wekOutputs_Address))
            {
                f = (float)msg.Data[0];
                nf = (float)msg.Data[1];

            }


            if (String.Equals(args.Address, isFocused_Address))
            {
                isFocused = true;
                Invoke("Switch", 0.3f);
                Debug.Log("yay!");

            }
            if (String.Equals(args.Address, notFocused_Address))
            {
                notFocused = true;
                Invoke("Switch", 0.3f);
                Debug.Log("yayyay!");

            }
        }
        void Switch()
        {
            if (notFocused == true) notFocused = false;
            if (isFocused == true) isFocused = false;
        }
    }
}