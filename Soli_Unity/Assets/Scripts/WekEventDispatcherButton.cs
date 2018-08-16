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
        public bool bundleMode, DTW_mode, IsWekRun, SVMTrain, SVMclasses;
        public string oscOutAddress2;
        private List<UniOSCEventDispatcherCB> senderList = new List<UniOSCEventDispatcherCB>();
        public static WekEventDispatcherButton main;
        private int gesture;
        private float svmClass;

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
                if (oscOutAddress2 != null) AppendData(new OscMessage(oscOutAddress2, 0));
            }
            else
            {
                ClearData();
                AppendData(0f);
                AppendData(0f);
            }

            if (SVMTrain)
            {
                ButtonClick(true);
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
        void FixedUpdate()
        {
            _Update();
        }
        void Update()
        {
            
            if (!DTW_mode && !SVMTrain && !SVMclasses)
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
            OscMessage msg = null;
            if (_OSCeArg.Packet is OscMessage)
            {
                msg = ((OscMessage)_OSCeArg.Packet);
                if (SVMclasses)
                {
                    msg.UpdateDataAt(0, svmClass);
                    msg.UpdateDataAt(1, svmClass);
                }
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
                    else if (SVMclasses)
                    {
                      
                      m.UpdateDataAt(0, svmClass);
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

            if (DTW_mode || SVMTrain)
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
            else if (!DTW_mode && !SVMclasses)
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
            else if (SVMclasses)
            {
                SendOSCMessageDown();
            }

        }
        public void Gesture(int val)
        {
            gesture = val;
        }
        public void svm_Class (float c)
        {
            svmClass = c;
        }
    }
}