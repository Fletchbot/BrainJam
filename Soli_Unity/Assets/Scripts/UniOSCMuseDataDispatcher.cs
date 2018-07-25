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
        private bool AllValues, eegData, XYZ; 

        private float _gX, _gY, _gZ, _accX, _accY, _accZ,eeg0,eeg1,eeg2,eeg3,eeg4;
        private float _blink, _jc;

        private float t0_abs, t1_abs, t2_abs, t3_abs, a0_abs, a1_abs, a2_abs, a3_abs, b0_abs, b1_abs, b2_abs, b3_abs, g0_abs, g1_abs, g2_abs, g3_abs;
        private float theta_abs, alpha_abs, beta_abs, gamma_abs;
        private float theta_beta_abs, alpha_theta_abs;
        private float tb_abs0, tb_abs1, tb_abs2, tb_abs3, at_abs0, at_abs1, at_abs2, at_abs3;

        /*  private float d_r0, d_r1, d_r2, d_r3, t_r0, t_r1, t_r2, t_r3, a_r0, a_r1, a_r2, a_r3, b_r0, b_r1, b_r2, b_r3, g_r0, g_r1, g_r2, g_r3;
          private float delta_relative, theta_relative, alpha_relative, beta_relative, gamma_relative, theta_beta_r, alpha_theta_r;
          private float  tb_r0, tb_r1, tb_r2, tb_r3, at_r0, at_r1, at_r2, at_r3;
      */

        public override void Awake()
        {
            base.Awake();

        }
        public override void OnEnable()
        {
            
            if (MuseMonitor == null) MuseMonitor = gameObject;
            AllValues = MuseMonitor.GetComponent<UniOSCMuseMonitor>().AllValues;
            XYZ = MuseMonitor.GetComponent<UniOSCMuseMonitor>().XYZ;
            eegData = MuseMonitor.GetComponent<UniOSCMuseMonitor>().ReceiveEEG;
            //Here we setup our OSC message
            base.OnEnable();
            ClearData();
            //now we could add data;
            if (AllValues)
            {

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

                AppendData(0f);//tb_abs0
                AppendData(0f);//tb_abs1
                AppendData(0f);//tb_abs2
                AppendData(0f);//tb_abs3

                AppendData(0f);//tb_abs0
                AppendData(0f);//tb_abs1
                AppendData(0f);//tb_abs2
                AppendData(0f);//tb_abs3

            } else if(AllValues == false){
                AppendData(0f);//theta_abs
                AppendData(0f);//alpha_abs
                AppendData(0f);//beta_abs
                AppendData(0f);//gamma_abs

                AppendData(0f);//theta_beta_abs
                AppendData(0f);//alpha_theta_abs
            }
            if (XYZ)
            {
                AppendData(0f);//Gyro0
                AppendData(0f);//Gyro1
                AppendData(0f);//Gyro2

                AppendData(0f);//acc0
                AppendData(0f);//acc1
                AppendData(0f);//acc2
            }

            if (eegData) {
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

                tb_abs0 = MuseMonitor.GetComponent<UniOSCMuseMonitor>().tb_abs0;
                tb_abs1 = MuseMonitor.GetComponent<UniOSCMuseMonitor>().tb_abs1;
                tb_abs2 = MuseMonitor.GetComponent<UniOSCMuseMonitor>().tb_abs2;
                tb_abs3 = MuseMonitor.GetComponent<UniOSCMuseMonitor>().tb_abs3;

                at_abs0 = MuseMonitor.GetComponent<UniOSCMuseMonitor>().at_abs0;
                at_abs1 = MuseMonitor.GetComponent<UniOSCMuseMonitor>().at_abs1;
                at_abs2 = MuseMonitor.GetComponent<UniOSCMuseMonitor>().at_abs2;
                at_abs3 = MuseMonitor.GetComponent<UniOSCMuseMonitor>().at_abs3;

         /*       d_r0 = MuseMonitor.GetComponent<UniOSCMuseMonitor>().d_r0;
                d_r1 = MuseMonitor.GetComponent<UniOSCMuseMonitor>().d_r1;
                d_r2 = MuseMonitor.GetComponent<UniOSCMuseMonitor>().d_r2;
                d_r3 = MuseMonitor.GetComponent<UniOSCMuseMonitor>().d_r3;

                t_r0 = MuseMonitor.GetComponent<UniOSCMuseMonitor>().t_r0;
                t_r1 = MuseMonitor.GetComponent<UniOSCMuseMonitor>().t_r1;
                t_r2 = MuseMonitor.GetComponent<UniOSCMuseMonitor>().t_r2;
                t_r3 = MuseMonitor.GetComponent<UniOSCMuseMonitor>().t_r3;

                a_r0 = MuseMonitor.GetComponent<UniOSCMuseMonitor>().a_r0;
                a_r1 = MuseMonitor.GetComponent<UniOSCMuseMonitor>().a_r1;
                a_r2 = MuseMonitor.GetComponent<UniOSCMuseMonitor>().a_r2;
                a_r3 = MuseMonitor.GetComponent<UniOSCMuseMonitor>().a_r3;

                b_r0 = MuseMonitor.GetComponent<UniOSCMuseMonitor>().b_r0;
                b_r1 = MuseMonitor.GetComponent<UniOSCMuseMonitor>().b_r1;
                b_r2 = MuseMonitor.GetComponent<UniOSCMuseMonitor>().b_r2;
                b_r3 = MuseMonitor.GetComponent<UniOSCMuseMonitor>().b_r3;

                g_r0 = MuseMonitor.GetComponent<UniOSCMuseMonitor>().g_r0;
                g_r1 = MuseMonitor.GetComponent<UniOSCMuseMonitor>().g_r1;
                g_r2 = MuseMonitor.GetComponent<UniOSCMuseMonitor>().g_r2;
                g_r3 = MuseMonitor.GetComponent<UniOSCMuseMonitor>().g_r3;

                tb_r0 = MuseMonitor.GetComponent<UniOSCMuseMonitor>().tb_r0;
                tb_r1 = MuseMonitor.GetComponent<UniOSCMuseMonitor>().tb_r1;
                tb_r2 = MuseMonitor.GetComponent<UniOSCMuseMonitor>().tb_r2;
                tb_r3 = MuseMonitor.GetComponent<UniOSCMuseMonitor>().tb_r3;

                at_r0 = MuseMonitor.GetComponent<UniOSCMuseMonitor>().at_r0;
                at_r1 = MuseMonitor.GetComponent<UniOSCMuseMonitor>().at_r1;
                at_r2 = MuseMonitor.GetComponent<UniOSCMuseMonitor>().at_r2;
                at_r3 = MuseMonitor.GetComponent<UniOSCMuseMonitor>().at_r3; */

            } else if (AllValues == false)
            {
                theta_abs = MuseMonitor.GetComponent<UniOSCMuseMonitor>().theta_abs;
                alpha_abs = MuseMonitor.GetComponent<UniOSCMuseMonitor>().alpha_abs;
                beta_abs = MuseMonitor.GetComponent<UniOSCMuseMonitor>().beta_abs;
                gamma_abs = MuseMonitor.GetComponent<UniOSCMuseMonitor>().gamma_abs;
                theta_beta_abs = MuseMonitor.GetComponent<UniOSCMuseMonitor>().theta_beta_abs;
                alpha_theta_abs = MuseMonitor.GetComponent<UniOSCMuseMonitor>().alpha_theta_abs;

               /* delta_relative = MuseMonitor.GetComponent<UniOSCMuseMonitor>().delta_relative;
                theta_relative = MuseMonitor.GetComponent<UniOSCMuseMonitor>().theta_relative;
                alpha_relative = MuseMonitor.GetComponent<UniOSCMuseMonitor>().alpha_relative;
                beta_relative = MuseMonitor.GetComponent<UniOSCMuseMonitor>().beta_relative;
                gamma_relative = MuseMonitor.GetComponent<UniOSCMuseMonitor>().gamma_relative;
                theta_beta_r = MuseMonitor.GetComponent<UniOSCMuseMonitor>().theta_beta_r;
                alpha_theta_r = MuseMonitor.GetComponent<UniOSCMuseMonitor>().alpha_theta_r;*/

            }

            if (XYZ)
            {
                _gX = MuseMonitor.GetComponent<UniOSCMuseMonitor>().gyroX;
                _gY = MuseMonitor.GetComponent<UniOSCMuseMonitor>().gyroY;
                _gZ = MuseMonitor.GetComponent<UniOSCMuseMonitor>().gyroZ;

                _accX = MuseMonitor.GetComponent<UniOSCMuseMonitor>().accX;
                _accY = MuseMonitor.GetComponent<UniOSCMuseMonitor>().accY;
                _accZ = MuseMonitor.GetComponent<UniOSCMuseMonitor>().accZ;
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

              /*      msg.UpdateDataAt(4, d_r0);
                    msg.UpdateDataAt(5, d_r1);
                    msg.UpdateDataAt(6, d_r2);
                    msg.UpdateDataAt(7, d_r3);*/

                    msg.UpdateDataAt(0, t0_abs);
                    msg.UpdateDataAt(1, t1_abs);
                    msg.UpdateDataAt(2, t2_abs);
                    msg.UpdateDataAt(3, t3_abs);

       /*             msg.UpdateDataAt(12, t_r0);
                    msg.UpdateDataAt(13, t_r1);
                    msg.UpdateDataAt(14, t_r2);
                    msg.UpdateDataAt(15, t_r3);*/

                    msg.UpdateDataAt(4, a0_abs);
                    msg.UpdateDataAt(5, a1_abs);
                    msg.UpdateDataAt(6, a2_abs);
                    msg.UpdateDataAt(7, a3_abs);

           /*         msg.UpdateDataAt(20, a_r0);
                    msg.UpdateDataAt(21, a_r1);
                    msg.UpdateDataAt(22, a_r2);
                    msg.UpdateDataAt(23, a_r3);*/

                    msg.UpdateDataAt(8, b0_abs);
                    msg.UpdateDataAt(9, b1_abs);
                    msg.UpdateDataAt(10, b2_abs);
                    msg.UpdateDataAt(11, b3_abs);

             /*       msg.UpdateDataAt(28, b_r0);
                    msg.UpdateDataAt(29, b_r1);
                    msg.UpdateDataAt(30, b_r2);
                    msg.UpdateDataAt(31, b_r3); */

                    msg.UpdateDataAt(12, g0_abs);
                    msg.UpdateDataAt(13, g1_abs);
                    msg.UpdateDataAt(14, g2_abs);
                    msg.UpdateDataAt(15, g3_abs);

              /*      msg.UpdateDataAt(36, g_r0);
                    msg.UpdateDataAt(37, g_r1);
                    msg.UpdateDataAt(38, g_r2);
                    msg.UpdateDataAt(39, g_r3); */

                    msg.UpdateDataAt(16, tb_abs0);
                    msg.UpdateDataAt(17, tb_abs1);
                    msg.UpdateDataAt(18, tb_abs2);
                    msg.UpdateDataAt(19, tb_abs3);

           /*         msg.UpdateDataAt(44, tb_r0);
                    msg.UpdateDataAt(45, tb_r1);
                    msg.UpdateDataAt(46, tb_r2);
                    msg.UpdateDataAt(47, tb_r3);*/

                    msg.UpdateDataAt(20, at_abs0);
                    msg.UpdateDataAt(21, at_abs1);
                    msg.UpdateDataAt(22, at_abs2);
                    msg.UpdateDataAt(23, at_abs3);

          /*          msg.UpdateDataAt(52, at_r0);
                    msg.UpdateDataAt(53, at_r1);
                    msg.UpdateDataAt(54, at_r2);
                    msg.UpdateDataAt(55, at_r3);*/
                    if (XYZ)
                    {
                        msg.UpdateDataAt(56, _gX);
                        msg.UpdateDataAt(57, _gY);
                        msg.UpdateDataAt(58, _gZ);

                        msg.UpdateDataAt(59, _accX);
                        msg.UpdateDataAt(60, _accY);
                        msg.UpdateDataAt(61, _accZ);
                    }
                    if (eegData && XYZ)
                    {
                        msg.UpdateDataAt(62, eeg0);
                        msg.UpdateDataAt(63, eeg1);
                        msg.UpdateDataAt(64, eeg2);
                        msg.UpdateDataAt(65, eeg3);
                        msg.UpdateDataAt(66, eeg4);
                    }
                    else if (eegData && !XYZ)
                    {
                        msg.UpdateDataAt(56, eeg0);
                        msg.UpdateDataAt(57, eeg1);
                        msg.UpdateDataAt(58, eeg2);
                        msg.UpdateDataAt(59, eeg3);
                        msg.UpdateDataAt(60, eeg4);
                    }
                }
                else if (AllValues == false)
                {
                    msg.UpdateDataAt(0, theta_abs);
                    msg.UpdateDataAt(1, alpha_abs);
                    msg.UpdateDataAt(2, beta_abs);
                    msg.UpdateDataAt(3, gamma_abs);

                    msg.UpdateDataAt(4, theta_beta_abs);
                    msg.UpdateDataAt(5, alpha_theta_abs);

                    if (XYZ)
                    {
                        msg.UpdateDataAt(14, _gX);
                        msg.UpdateDataAt(15, _gY);
                        msg.UpdateDataAt(16, _gZ);

                        msg.UpdateDataAt(17, _accX);
                        msg.UpdateDataAt(18, _accY);
                        msg.UpdateDataAt(19, _accZ);
                    }
                    if (eegData && XYZ)
                    {
                        msg.UpdateDataAt(22, eeg0);
                        msg.UpdateDataAt(23, eeg1);
                        msg.UpdateDataAt(24, eeg2);
                        msg.UpdateDataAt(25, eeg3);
                        msg.UpdateDataAt(26, eeg4);
                    }
                    else if (eegData && !XYZ)
                    {
                        msg.UpdateDataAt(14, eeg0);
                        msg.UpdateDataAt(15, eeg1);
                        msg.UpdateDataAt(16, eeg2);
                        msg.UpdateDataAt(17, eeg3);
                        msg.UpdateDataAt(18, eeg4);
                    }
                }

            }

              _SendOSCMessage(_OSCeArg);
           
          }
        }
    }
