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
        public float meditateFloat, emotions, instruments, happyFloat, sadFloat, unsureFloat, instr1Float, instr2Float, noInstrFloat;
        public bool isMeditate;

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
                            meditateFloat = (float)msg.Data[0];
                    }
                if (String.Equals(args.Address, addressArray[1]))
                    {
                        isMeditate = true;
                        Invoke("Switch", 0.3f);
                    }

                if (String.Equals(args.Address, addressArray[2]))
                {

                    emotions = (float)msg.Data[0];
                    instruments = (float)msg.Data[1];

                }
                if (String.Equals(args.Address, addressArray[3]))
                {
                    unsureFloat = (float)msg.Data[0];
                    happyFloat = (float)msg.Data[1];
                    sadFloat = (float)msg.Data[2];

                }
                if (String.Equals(args.Address, addressArray[4]))
                {
                    noInstrFloat = (float)msg.Data[0];
                    instr1Float = (float)msg.Data[1];
                    instr2Float = (float)msg.Data[2];
                }

            }
        }
        void Switch()
        {
            if (isMeditate == true) isMeditate = false;

        }
    }
}
