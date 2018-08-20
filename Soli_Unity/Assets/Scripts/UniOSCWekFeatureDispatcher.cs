using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System;
using OSCsharp.Data;


namespace UniOSC
{
    /// <summary>
    /// Wek Feature OSC Dispatcher
    /// </summary>
    [AddComponentMenu("UniOSC/WekFeatureDispatcher")]
    [ExecuteInEditMode]

    public class UniOSCWekFeatureDispatcher : UniOSCEventDispatcher
    {
        public GameObject WekOutputReceiver;
        public float mood, facialExpression, happyFloat, sadFloat;
        // Use this for initialization
        public override void Awake()
        {
            base.Awake();

        }
        public override void OnEnable()
        {
            //Here we setup our OSC message
            base.OnEnable();
            ClearData();
            //now we could add data;
            AppendData(0f);//h_float
            AppendData(0f);//s_float
            AppendData(0f);//mood
            AppendData(0f);//facialExpression

        }

        public override void OnDisable()
        {
            base.OnDisable();
        }

        void FixedUpdate()
        {
            _Update();
        }
        // Update is called once per frame
        protected override void _Update()
        {
            base._Update();


            happyFloat = WekOutputReceiver.GetComponent<UniOSCWekOutputReceiver>().happyFloat;
            sadFloat = WekOutputReceiver.GetComponent<UniOSCWekOutputReceiver>().sadFloat;
            mood = WekOutputReceiver.GetComponent<UniOSCWekOutputReceiver>().mood;
            facialExpression = WekOutputReceiver.GetComponent<UniOSCWekOutputReceiver>().facialExpression;

            OscMessage msg = null;
            if (_OSCeArg.Packet is OscMessage)
            {
                msg = ((OscMessage)_OSCeArg.Packet);
            }
            else if (_OSCeArg.Packet is OscBundle)
            {
                //bundle version
                msg = ((OscBundle)_OSCeArg.Packet).Messages[0];
            }

            if (msg != null)
            {
                msg.UpdateDataAt(0, happyFloat);
                msg.UpdateDataAt(1, sadFloat);
                msg.UpdateDataAt(2, mood);
                msg.UpdateDataAt(3, facialExpression);
            }

            _SendOSCMessage(_OSCeArg);

        }
    }
}
