using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System;
using OSCsharp.Data;


namespace UniOSC
{

    /// <summary>
    /// Muse EEG data OSC Dispatcher
    /// </summary>
    [AddComponentMenu("UniOSC/MuseDataDispatcher")]
    [ExecuteInEditMode]
    public class UniOSCMuseDataDispatcher : UniOSCEventDispatcher
    {

        public GameObject MuseMonitor;
        private bool AllValues, eegData, deltaWek;
        private float eeg0, eeg1, eeg2, eeg3, eeg4;
        private float _gX, _gY, _gZ, _accX, _accY, _accZ;
        private float _blink, _jc;

        private float d0_abs, d1_abs, d2_abs, d3_abs, t0_abs, t1_abs, t2_abs, t3_abs, a0_abs, a1_abs, a2_abs, a3_abs, b0_abs, b1_abs, b2_abs, b3_abs, g0_abs, g1_abs, g2_abs, g3_abs;
        private float delta_abs, theta_abs, alpha_abs, beta_abs, gamma_abs;

        public override void Awake()
        {
            base.Awake();

        }
        public override void OnEnable()
        {

            if (MuseMonitor == null) MuseMonitor = gameObject;
            AllValues = MuseMonitor.GetComponent<UniOSCMuseMonitor>().AllValues;
            eegData = MuseMonitor.GetComponent<UniOSCMuseMonitor>().ReceiveEEG;
            deltaWek = MuseMonitor.GetComponent<UniOSCMuseMonitor>().deltaWek;

            //Here we setup our OSC message
            base.OnEnable();
            ClearData();
            //now we could add data;
            if (AllValues)
            {
                if (deltaWek)
                {
                    AppendData(0f);//d0_abs
                    AppendData(0f);//d1_abs
                    AppendData(0f);//d2_abs
                    AppendData(0f);//d3_abs
                }

                AppendData(0f);//t0_abs
                AppendData(0f);//t1_abs
                AppendData(0f);//t2_abs
                AppendData(0f);//t3_abs

                AppendData(0f);//a0_abs
                AppendData(0f);//a1_abs
                AppendData(0f);//a2_abs
                AppendData(0f);//a3_abs

                AppendData(0f);//b0_abs
                AppendData(0f);//b1_abs
                AppendData(0f);//b2_abs
                AppendData(0f);//b3_abs

                AppendData(0f);//g0_abs
                AppendData(0f);//g1_abs
                AppendData(0f);//g2_abs
                AppendData(0f);//g3_abs

            }
            else if (AllValues == false)
            {
                if (deltaWek) AppendData(0f);//delta_abs
                AppendData(0f);//theta_abs
                AppendData(0f);//alpha_abs
                AppendData(0f);//beta_abs
                AppendData(0f);//gamma_abs
            }

            if (eegData)
            {
                AppendData(0f);//eeg0
                AppendData(0f);//egg1
                AppendData(0f);//eeg2
                AppendData(0f);//eeg3
                AppendData(0f);//eeg4
            }

        }

        public override void OnDisable()
        {
            base.OnDisable();
        }

        void FixedUpdate()
        {
            _Update();
        }
        protected override void _Update()
        {
            base._Update();

            if (MuseMonitor == null) return;

            if (AllValues)
            {
                if (deltaWek)
                {
                    d0_abs = MuseMonitor.GetComponent<UniOSCMuseMonitor>().d0;
                    d1_abs = MuseMonitor.GetComponent<UniOSCMuseMonitor>().d1;
                    d2_abs = MuseMonitor.GetComponent<UniOSCMuseMonitor>().d2;
                    d3_abs = MuseMonitor.GetComponent<UniOSCMuseMonitor>().d3;
                }

                t0_abs = MuseMonitor.GetComponent<UniOSCMuseMonitor>().t0;
                t1_abs = MuseMonitor.GetComponent<UniOSCMuseMonitor>().t1;
                t2_abs = MuseMonitor.GetComponent<UniOSCMuseMonitor>().t2;
                t3_abs = MuseMonitor.GetComponent<UniOSCMuseMonitor>().t3;

                a0_abs = MuseMonitor.GetComponent<UniOSCMuseMonitor>().a0;
                a1_abs = MuseMonitor.GetComponent<UniOSCMuseMonitor>().a1;
                a2_abs = MuseMonitor.GetComponent<UniOSCMuseMonitor>().a2;
                a3_abs = MuseMonitor.GetComponent<UniOSCMuseMonitor>().a3;

                b0_abs = MuseMonitor.GetComponent<UniOSCMuseMonitor>().b0;
                b1_abs = MuseMonitor.GetComponent<UniOSCMuseMonitor>().b1;
                b2_abs = MuseMonitor.GetComponent<UniOSCMuseMonitor>().b2;
                b3_abs = MuseMonitor.GetComponent<UniOSCMuseMonitor>().b3;

                g0_abs = MuseMonitor.GetComponent<UniOSCMuseMonitor>().g0;
                g1_abs = MuseMonitor.GetComponent<UniOSCMuseMonitor>().g1;
                g2_abs = MuseMonitor.GetComponent<UniOSCMuseMonitor>().g2;
                g3_abs = MuseMonitor.GetComponent<UniOSCMuseMonitor>().g3;

            }
            else if (AllValues == false)
            {
                if (deltaWek) delta_abs = MuseMonitor.GetComponent<UniOSCMuseMonitor>().delta_abs;
                theta_abs = MuseMonitor.GetComponent<UniOSCMuseMonitor>().theta_abs;
                alpha_abs = MuseMonitor.GetComponent<UniOSCMuseMonitor>().alpha_abs;
                beta_abs = MuseMonitor.GetComponent<UniOSCMuseMonitor>().beta_abs;
                gamma_abs = MuseMonitor.GetComponent<UniOSCMuseMonitor>().gamma_abs;
            }

            if (eegData)
            {
                eeg0 = MuseMonitor.GetComponent<UniOSCMuseMonitor>().eeg0;
                eeg1 = MuseMonitor.GetComponent<UniOSCMuseMonitor>().eeg1;
                eeg2 = MuseMonitor.GetComponent<UniOSCMuseMonitor>().eeg2;
                eeg3 = MuseMonitor.GetComponent<UniOSCMuseMonitor>().eeg3;
                eeg4 = MuseMonitor.GetComponent<UniOSCMuseMonitor>().eeg4;
            }

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
                if (AllValues)
                {
                    msg.UpdateDataAt(0, t0_abs);
                    msg.UpdateDataAt(1, t1_abs);
                    msg.UpdateDataAt(2, t2_abs);
                    msg.UpdateDataAt(3, t3_abs);

                    msg.UpdateDataAt(4, a0_abs);
                    msg.UpdateDataAt(5, a1_abs);
                    msg.UpdateDataAt(6, a2_abs);
                    msg.UpdateDataAt(7, a3_abs);

                    msg.UpdateDataAt(8, b0_abs);
                    msg.UpdateDataAt(9, b1_abs);
                    msg.UpdateDataAt(10, b2_abs);
                    msg.UpdateDataAt(11, b3_abs);

                    msg.UpdateDataAt(12, g0_abs);
                    msg.UpdateDataAt(13, g1_abs);
                    msg.UpdateDataAt(14, g2_abs);
                    msg.UpdateDataAt(15, g3_abs);

                    if (deltaWek)
                    {
                        msg.UpdateDataAt(16, d0_abs);
                        msg.UpdateDataAt(17, d1_abs);
                        msg.UpdateDataAt(18, d2_abs);
                        msg.UpdateDataAt(19, d3_abs);
                    }

                    if (eegData && deltaWek)
                    {
                        msg.UpdateDataAt(20, eeg0);
                        msg.UpdateDataAt(21, eeg1);
                        msg.UpdateDataAt(22, eeg2);
                        msg.UpdateDataAt(23, eeg3);
                        msg.UpdateDataAt(24, eeg4);
                    }
                    else if (eegData && !deltaWek)
                    {
                        msg.UpdateDataAt(16, eeg0);
                        msg.UpdateDataAt(17, eeg1);
                        msg.UpdateDataAt(18, eeg2);
                        msg.UpdateDataAt(19, eeg3);
                        msg.UpdateDataAt(20, eeg4);
                    }
                }
                else if (AllValues == false)
                {
                    msg.UpdateDataAt(0, theta_abs);
                    msg.UpdateDataAt(1, alpha_abs);
                    msg.UpdateDataAt(2, beta_abs);
                    msg.UpdateDataAt(3, gamma_abs);

                    if (deltaWek)
                    {
                        msg.UpdateDataAt(4, delta_abs);
                    }

                    if (eegData && deltaWek)
                    {
                        msg.UpdateDataAt(5, eeg0);
                        msg.UpdateDataAt(6, eeg1);
                        msg.UpdateDataAt(7, eeg2);
                        msg.UpdateDataAt(8, eeg3);
                        msg.UpdateDataAt(9, eeg4);
                    }
                    else if (eegData && !deltaWek)
                    {
                        msg.UpdateDataAt(4, eeg0);
                        msg.UpdateDataAt(5, eeg1);
                        msg.UpdateDataAt(6, eeg2);
                        msg.UpdateDataAt(7, eeg3);
                        msg.UpdateDataAt(8, eeg4);
                    }
                }

            }

            _SendOSCMessage(_OSCeArg);

        }
    }
}
