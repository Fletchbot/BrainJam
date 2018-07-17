/*
* UniOSC
* Copyright © 2014-2015 Stefan Schlupek
* All rights reserved
* info@monoflow.org
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System;
using OSCsharp.Data;


namespace UniOSC
{

    /// <summary>
    /// Dispatcher button that forces a OSCConnection to send a OSC Message.
    /// Two separate states: Down and Up 
    /// </summary>
    [AddComponentMenu("UniOSC/WekEventDispatcherButton")]
    [ExecuteInEditMode]
    public class WekEventDispatcherButton : UniOSCEventDispatcher
    {
        public bool bundleMode, DTW_mode, IsWekRun;
        public string oscOutAddress2;
        private List<UniOSCEventDispatcherCB> senderList = new List<UniOSCEventDispatcherCB>();
        public static WekEventDispatcherButton main;
        private int gesture;

        public override void Awake()
        {
            base.Awake();
            main = this;
        }

        public override void OnEnable()
        {
            base.OnEnable();

            if (bundleMode)
            {
                SetBundleMode(true);
                ClearData();
                AppendData(new OscMessage(oscOutAddress, 0));
              if(oscOutAddress2 != null) AppendData(new OscMessage(oscOutAddress2, 0));
            }
            else
            {
                ClearData();
                UniOSCEventDispatcherCB oscSender = new UniOSCEventDispatcherCBSimple(oscOutAddress, explicitConnection);//OSCOutAddress,OSCConnection
                oscSender.AppendData(0);
                oscSender.Enable();
                senderList.Add(oscSender);
                if (oscOutAddress2 != null)
                {
                    UniOSCEventDispatcherCB oscSender2 = new UniOSCEventDispatcherCBSimple(oscOutAddress2, explicitConnection);//OSCOutAddress,OSCConnection
                    oscSender2.AppendData(0);
                    oscSender2.Enable();
                    senderList.Add(oscSender2);
                }              
            }
        }

        public override void OnDisable()
        {
            base.OnDisable();

            for (var i = 0; i < senderList.Count; i++)
            {
                senderList[i].Dispose();
                senderList[i] = null;
            }
            senderList.Clear();
        }

        public void Update()
        {
            if (!DTW_mode)
            {
                if (IsWekRun)
                {
                    SendOSCMessageDown();
                } 
                else if (!IsWekRun)
                {
                    SendOSCMessageUp();
                }
            }
        }

        /// <summary>
        /// Sends the OSC message with the downOSCDataValue.
        /// </summary>
        public void SendOSCMessageDown()
        {
  
            if (_OSCeArg.Packet is OscMessage)
            {
              //  ((OscMessage)_OSCeArg.Packet).UpdateDataAt(0, 0);
              //  ((OscMessage)_OSCeArg.Packet).Address = oscOutAddress;
            }
            else if (_OSCeArg.Packet is OscBundle)
            {
                foreach (OscMessage m in ((OscBundle)_OSCeArg.Packet).Messages)
                {
                    m.Address = oscOutAddress;
                    if (DTW_mode)
                    {
                        m.UpdateDataAt(0, gesture);
                    }
                    else
                    {
                        m.UpdateDataAt(0, 0);
                    }
                }
            }
            _SendOSCMessage(_OSCeArg);

            foreach (var s in senderList)
            {
                s.SendOSCMessage();
            }
        }

        /// <summary>
        /// Sends the OSC message with the upOSCDataValue.
        /// </summary>
        public void SendOSCMessageUp()
        {
            if (_OSCeArg.Packet is OscMessage)
            {
                //((OscMessage)_OSCeArg.Packet).UpdateDataAt(1, 0);
               // ((OscMessage)_OSCeArg.Packet).Address = oscOutAddress2;
            }
            else if (_OSCeArg.Packet is OscBundle)
            {
                foreach (OscMessage m2 in ((OscBundle)_OSCeArg.Packet).Messages)
                {
                    m2.Address = oscOutAddress2;    
                    m2.UpdateDataAt(0, 0);
                }
            }

            _SendOSCMessage(_OSCeArg);

            foreach (var s in senderList)
            {
                s.SendOSCMessage();
            }
        }

        public void ButtonClick(bool isOn)
        {

            if (DTW_mode)
            {
                if (isOn == true)
                {
                    SendOSCMessageDown();
                }
                else if (isOn == false)
                {
                    SendOSCMessageUp();
                }
            }
            else if (!DTW_mode)
            {
                if (isOn == true)
                {
                    IsWekRun = true;
                }
                else if (isOn == false)
                {
                    IsWekRun = false;
                }
            }

        }
        public void Gesture(int val)
        {
            gesture = val;
        }
    }
}