using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System;
using OSCsharp.Data;


namespace UniOSC {

    /// <summary>
    /// Muse EEG data OSC Dispatcher
    /// </summary>
    [AddComponentMenu("UniOSC/MuseDataDispatcher")]
    [ExecuteInEditMode]
    public class UniOSCMuseDataDispatcher : UniOSCEventDispatcher
    {

        public GameObject MuseMonitor;

      //  public UniOSCConnection[] connectionArray;
       // private List<UniOSCEventDispatcherCB> senderList = new List<UniOSCEventDispatcherCB>();

        private float _gX, _gY, _gZ, _accX, _accY, _accZ, _eeg0, _eeg1, _eeg2, _eeg3, _eeg4;
        private float d0_abs, d1_abs, d2_abs, d3_abs, t0_abs, t1_abs, t2_abs, t3_abs, a0_abs, a1_abs, a2_abs, a3_abs, b0_abs, b1_abs, b2_abs, b3_abs, g0_abs, g1_abs, g2_abs, g3_abs;
        private float _blink, _jc, _istouching, _batt, _hs0, _hs1, _hs2, _hs3;

        public override void Awake()
        {
            base.Awake();

        }
        public override void OnEnable()
        {

            if (MuseMonitor == null) MuseMonitor = gameObject;
            //Here we setup our OSC message
            base.OnEnable();
            ClearData();
            //now we could add data;

            AppendData(0f);//d0_abs
            AppendData(0f);//d1_abs
            AppendData(0f);//d2_abs
            AppendData(0f);//d3_abs

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

            AppendData(0f);//Gyro0
            AppendData(0f);//Gyro1
            AppendData(0f);//Gyro2

            AppendData(0f);//acc0
            AppendData(0f);//acc1
            AppendData(0f);//acc2

            AppendData(0f);//eeg0
            AppendData(0f);//eeg1
            AppendData(0f);//eeg2
            AppendData(0f);//eeg3
            AppendData(0f);//eeg4

            AppendData(0f);//blink
            AppendData(0f);//jaw

            AppendData(0f);//istouching
            AppendData(0f);//batt
            AppendData(0f);//hs0
            AppendData(0f);//hs1
            AppendData(0f);//hs2
            AppendData(0f);//hs3

  /*          if (connectionArray != null)
            {
                foreach (var c in connectionArray)
                {
                    if (c == null) continue;
                    UniOSCEventDispatcherCB oscSender = new UniOSCEventDispatcherCBSimple(oscOutAddress, c);//OSCOutAddress,OSCConnection

                    //oscSender.ClearData();
                    oscSender.Enable();

                    oscSender.AppendData(0f);//d0_abs
                    oscSender.AppendData(0f);//d1_abs
                    oscSender.AppendData(0f);//d2_abs
                    oscSender.AppendData(0f);//d3_abs

                    oscSender.AppendData(0f);//t0_abs
                    oscSender.AppendData(0f);//t1_abs
                    oscSender.AppendData(0f);//t2_abs
                    oscSender.AppendData(0f);//t3_abs

                    oscSender.AppendData(0f);//a0_abs
                    oscSender.AppendData(0f);//a1_abs
                    oscSender.AppendData(0f);//a2_abs
                    oscSender.AppendData(0f);//a3_abs

                    oscSender.AppendData(0f);//b0_abs
                    oscSender.AppendData(0f);//b1_abs
                    oscSender.AppendData(0f);//b2_abs
                    oscSender.AppendData(0f);//b3_abs

                    oscSender.AppendData(0f);//g0_abs
                    oscSender.AppendData(0f);//g1_abs
                    oscSender.AppendData(0f);//g2_abs
                    oscSender.AppendData(0f);//g3_abs

                    oscSender.AppendData(0f);//Gyro0
                    oscSender.AppendData(0f);//Gyro1
                    oscSender.AppendData(0f);//Gyro2

                    oscSender.AppendData(0f);//acc0
                    oscSender.AppendData(0f);//acc1
                    oscSender.AppendData(0f);//acc2

                    oscSender.AppendData(0f);//eeg0
                    oscSender.AppendData(0f);//eeg1
                    oscSender.AppendData(0f);//eeg2
                    oscSender.AppendData(0f);//eeg3
                    oscSender.AppendData(0f);//eeg4

                    oscSender.AppendData(0f);//blink
                    oscSender.AppendData(0f);//jaw

                    oscSender.AppendData(0f);//istouching
                    oscSender.AppendData(0f);//batt
                    oscSender.AppendData(0f);//hs0
                    oscSender.AppendData(0f);//hs1
                    oscSender.AppendData(0f);//hs2
                    oscSender.AppendData(0f);//hs3

                    senderList.Add(oscSender);
                }
            }*/
        }

        public override void OnDisable()
        {
            base.OnDisable();
            StopSendIntervalTimer();

            // ClearData();
         /*   for (var i = 0; i < senderList.Count; i++)
            {
                senderList[i].Dispose();
                senderList[i] = null;
            }
            senderList.Clear();*/
        }

        void FixedUpdate()
        {
            _Update();
        }
        protected override void _Update()
        {

            base._Update();

            if (MuseMonitor == null) return;

            d0_abs = MuseMonitor.GetComponent<UniOSCMuseMonitor>().d0;
            d1_abs = MuseMonitor.GetComponent<UniOSCMuseMonitor>().d1;
            d2_abs = MuseMonitor.GetComponent<UniOSCMuseMonitor>().d2;
            d3_abs = MuseMonitor.GetComponent<UniOSCMuseMonitor>().d3;

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

            _gX = MuseMonitor.GetComponent<UniOSCMuseMonitor>().gyroX;
            _gY = MuseMonitor.GetComponent<UniOSCMuseMonitor>().gyroY;
            _gZ = MuseMonitor.GetComponent<UniOSCMuseMonitor>().gyroZ;

            _accX = MuseMonitor.GetComponent<UniOSCMuseMonitor>().accX;
            _accY = MuseMonitor.GetComponent<UniOSCMuseMonitor>().accY;
            _accZ = MuseMonitor.GetComponent<UniOSCMuseMonitor>().accZ;

            _eeg0 = MuseMonitor.GetComponent<UniOSCMuseMonitor>().eeg0;
            _eeg1 = MuseMonitor.GetComponent<UniOSCMuseMonitor>().eeg1;
            _eeg2 = MuseMonitor.GetComponent<UniOSCMuseMonitor>().eeg2;
            _eeg3 = MuseMonitor.GetComponent<UniOSCMuseMonitor>().eeg3;
            _eeg4 = MuseMonitor.GetComponent<UniOSCMuseMonitor>().eeg4;

            _blink = MuseMonitor.GetComponent<UniOSCMuseMonitor>().blink;
            _jc = MuseMonitor.GetComponent<UniOSCMuseMonitor>().jc;

            _istouching = MuseMonitor.GetComponent<UniOSCMuseMonitor>().touchingforehead;
            _batt = MuseMonitor.GetComponent<UniOSCMuseMonitor>().batt*100;
            _hs0 = MuseMonitor.GetComponent<UniOSCMuseMonitor>().hs0;
            _hs1 = MuseMonitor.GetComponent<UniOSCMuseMonitor>().hs1;
            _hs2 = MuseMonitor.GetComponent<UniOSCMuseMonitor>().hs2;
            _hs3 = MuseMonitor.GetComponent<UniOSCMuseMonitor>().hs3;

         /*   try
            {
                if (_OSCpkg is OscMessage)
                {
                    ((OscMessage)_OSCpkg).UpdateDataAt(0, d0_abs);
                    ((OscMessage)_OSCpkg).UpdateDataAt(1, d1_abs);
                    ((OscMessage)_OSCpkg).UpdateDataAt(2, d2_abs);
                    ((OscMessage)_OSCpkg).UpdateDataAt(3, d3_abs);

                    ((OscMessage)_OSCpkg).UpdateDataAt(4, t0_abs);
                    ((OscMessage)_OSCpkg).UpdateDataAt(5, t1_abs);
                    ((OscMessage)_OSCpkg).UpdateDataAt(6, t2_abs);
                    ((OscMessage)_OSCpkg).UpdateDataAt(7, t3_abs);

                    ((OscMessage)_OSCpkg).UpdateDataAt(8, a0_abs);
                    ((OscMessage)_OSCpkg).UpdateDataAt(9, a1_abs);
                    ((OscMessage)_OSCpkg).UpdateDataAt(10, a2_abs);
                    ((OscMessage)_OSCpkg).UpdateDataAt(11, a3_abs);

                    ((OscMessage)_OSCpkg).UpdateDataAt(12, b0_abs);
                    ((OscMessage)_OSCpkg).UpdateDataAt(13, b1_abs);
                    ((OscMessage)_OSCpkg).UpdateDataAt(14, b2_abs);
                    ((OscMessage)_OSCpkg).UpdateDataAt(15, b3_abs);

                    ((OscMessage)_OSCpkg).UpdateDataAt(16, g0_abs);
                    ((OscMessage)_OSCpkg).UpdateDataAt(17, g1_abs);
                    ((OscMessage)_OSCpkg).UpdateDataAt(18, g2_abs);
                    ((OscMessage)_OSCpkg).UpdateDataAt(19, g3_abs);

                    ((OscMessage)_OSCpkg).UpdateDataAt(20, _gX);
                    ((OscMessage)_OSCpkg).UpdateDataAt(21, _gY);
                    ((OscMessage)_OSCpkg).UpdateDataAt(22, _gZ);

                    ((OscMessage)_OSCpkg).UpdateDataAt(23, _accX);
                    ((OscMessage)_OSCpkg).UpdateDataAt(24, _accY);
                    ((OscMessage)_OSCpkg).UpdateDataAt(25, _accZ);

                    ((OscMessage)_OSCpkg).UpdateDataAt(26, _eeg0);
                    ((OscMessage)_OSCpkg).UpdateDataAt(27, _eeg1);
                    ((OscMessage)_OSCpkg).UpdateDataAt(28, _eeg2);
                    ((OscMessage)_OSCpkg).UpdateDataAt(29, _eeg3);
                    ((OscMessage)_OSCpkg).UpdateDataAt(30, _eeg4);

                    ((OscMessage)_OSCpkg).UpdateDataAt(31, _blink);
                    ((OscMessage)_OSCpkg).UpdateDataAt(32, _jc);

                    ((OscMessage)_OSCpkg).UpdateDataAt(33, _istouching);
                    ((OscMessage)_OSCpkg).UpdateDataAt(34, _batt);
                    ((OscMessage)_OSCpkg).UpdateDataAt(35, _hs0);
                    ((OscMessage)_OSCpkg).UpdateDataAt(36, _hs1);
                    ((OscMessage)_OSCpkg).UpdateDataAt(37, _hs2);
                    ((OscMessage)_OSCpkg).UpdateDataAt(38, _hs3);
                }
            }
            catch (Exception)
            {

            }
            //Here we trigger the sending                  
            SendOSCMessage();

            foreach (var s in senderList)
            {
                s.SendOSCMessage();
            } */

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
                  msg.UpdateDataAt(0, d0_abs);
                  msg.UpdateDataAt(1, d1_abs);
                  msg.UpdateDataAt(2, d2_abs);
                  msg.UpdateDataAt(3, d3_abs);

                  msg.UpdateDataAt(4, t0_abs);
                  msg.UpdateDataAt(5, t1_abs);
                  msg.UpdateDataAt(6, t2_abs);
                  msg.UpdateDataAt(7, t3_abs);

                  msg.UpdateDataAt(8, a0_abs);
                  msg.UpdateDataAt(9, a1_abs);
                  msg.UpdateDataAt(10, a2_abs);
                  msg.UpdateDataAt(11, a3_abs);

                  msg.UpdateDataAt(12, b0_abs);
                  msg.UpdateDataAt(13, b1_abs);
                  msg.UpdateDataAt(14, b2_abs);
                  msg.UpdateDataAt(15, b3_abs);

                  msg.UpdateDataAt(16, g0_abs);
                  msg.UpdateDataAt(17, g1_abs);
                  msg.UpdateDataAt(18, g2_abs);
                  msg.UpdateDataAt(19, g3_abs);

                  msg.UpdateDataAt(20, _gX);
                  msg.UpdateDataAt(21, _gY);
                  msg.UpdateDataAt(22, _gZ);

                  msg.UpdateDataAt(23, _accX);
                  msg.UpdateDataAt(24, _accY);
                  msg.UpdateDataAt(25, _accZ);

                  msg.UpdateDataAt(26, _eeg0);
                  msg.UpdateDataAt(27, _eeg1);
                  msg.UpdateDataAt(28, _eeg2);
                  msg.UpdateDataAt(29, _eeg3);
                  msg.UpdateDataAt(30, _eeg4);

                  msg.UpdateDataAt(31, _blink);
                  msg.UpdateDataAt(32, _jc);

                  msg.UpdateDataAt(33, _istouching);
                  msg.UpdateDataAt(34, _batt);
                  msg.UpdateDataAt(35, _hs0);
                  msg.UpdateDataAt(36, _hs1);
                  msg.UpdateDataAt(37, _hs2);
                  msg.UpdateDataAt(38, _hs3);
              }

              _SendOSCMessage(_OSCeArg);
           
          }
        }
    }
