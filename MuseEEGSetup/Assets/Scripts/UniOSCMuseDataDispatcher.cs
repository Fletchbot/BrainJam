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
        //public bool limitData;

        private float _gX, _gY, _gZ, _accX, _accY, _accZ;
        private float d0_abs, d1_abs, d2_abs, d3_abs, t0_abs, t1_abs, t2_abs, t3_abs, a0_abs, a1_abs, a2_abs, a3_abs, b0_abs, b1_abs, b2_abs, b3_abs, g0_abs, g1_abs, g2_abs, g3_abs;
        private float _blink, _jc;
        private float d_r0, d_r1, d_r2, d_r3, t_r0, t_r1, t_r2, t_r3, a_r0, a_r1, a_r2, a_r3, b_r0, b_r1, b_r2, b_r3, g_r0, g_r1, g_r2, g_r3;
        private float delta_abs, delta_relative, theta_abs, theta_relative, alpha_abs, alpha_relative, beta_abs, beta_relative, gamma_abs, gamma_relative;
        private float theta_beta_abs, theta_beta_r, alpha_theta_abs, alpha_theta_r;

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
           // if (limitData == true) {

          //  }
            AppendData(0f);//d0_abs
            AppendData(0f);//d1_abs
            AppendData(0f);//d2_abs
            AppendData(0f);//d3_abs
            AppendData(0f);//d0_r
            AppendData(0f);//d1_r
            AppendData(0f);//d2_r
            AppendData(0f);//d3_r
            AppendData(0f);//delta_abs
            AppendData(0f);//delta_relative

            AppendData(0f);//t0_abs
            AppendData(0f);//t1_abs
            AppendData(0f);//t2_abs
            AppendData(0f);//t3_abs
            AppendData(0f);//t0_r
            AppendData(0f);//t1_r
            AppendData(0f);//t2_r
            AppendData(0f);//t3_r
            AppendData(0f);//theta_abs
            AppendData(0f);//theta_relative

            AppendData(0f);//a0_abs
            AppendData(0f);//a1_abs
            AppendData(0f);//a2_abs
            AppendData(0f);//a3_abs
            AppendData(0f);//a0_r
            AppendData(0f);//a1_r
            AppendData(0f);//a2_r
            AppendData(0f);//a3_r
            AppendData(0f);//alpha_abs
            AppendData(0f);//alpha_relative

            AppendData(0f);//b0_abs
            AppendData(0f);//b1_abs
            AppendData(0f);//b2_abs
            AppendData(0f);//b3_abs
            AppendData(0f);//b0_r
            AppendData(0f);//b1_r
            AppendData(0f);//b2_r
            AppendData(0f);//b3_r
            AppendData(0f);//beta_abs
            AppendData(0f);//beta_relative

            AppendData(0f);//g0_abs
            AppendData(0f);//g1_abs
            AppendData(0f);//g2_abs
            AppendData(0f);//g3_abs
            AppendData(0f);//g0_r
            AppendData(0f);//g1_r
            AppendData(0f);//g2_r
            AppendData(0f);//g3_r
            AppendData(0f);//g_abs
            AppendData(0f);//gamma_relative

            AppendData(0f);//theta_beta_abs
            AppendData(0f);//theta_beta_r
            AppendData(0f);//alpha_theta_abs
            AppendData(0f);//alpha_theta_r

            AppendData(0f);//Gyro0
            AppendData(0f);//Gyro1
            AppendData(0f);//Gyro2

            AppendData(0f);//acc0
            AppendData(0f);//acc1
            AppendData(0f);//acc2

            AppendData(0f);//blink
            AppendData(0f);//jaw

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

            d0_abs = MuseMonitor.GetComponent<UniOSCMuseMonitor>().d0;
            d1_abs = MuseMonitor.GetComponent<UniOSCMuseMonitor>().d1;
            d2_abs = MuseMonitor.GetComponent<UniOSCMuseMonitor>().d2;
            d3_abs = MuseMonitor.GetComponent<UniOSCMuseMonitor>().d3;
            delta_abs = MuseMonitor.GetComponent<UniOSCMuseMonitor>().delta_abs;

            d_r0 = MuseMonitor.GetComponent<UniOSCMuseMonitor>().d_r0;
            d_r1 = MuseMonitor.GetComponent<UniOSCMuseMonitor>().d_r1;
            d_r2 = MuseMonitor.GetComponent<UniOSCMuseMonitor>().d_r2;
            d_r3= MuseMonitor.GetComponent<UniOSCMuseMonitor>().d_r3;
            delta_relative = MuseMonitor.GetComponent<UniOSCMuseMonitor>().delta_relative;

            t0_abs = MuseMonitor.GetComponent<UniOSCMuseMonitor>().t0;
            t1_abs = MuseMonitor.GetComponent<UniOSCMuseMonitor>().t1;
            t2_abs = MuseMonitor.GetComponent<UniOSCMuseMonitor>().t2;
            t3_abs = MuseMonitor.GetComponent<UniOSCMuseMonitor>().t3;
            theta_abs = MuseMonitor.GetComponent<UniOSCMuseMonitor>().theta_abs;

            t_r0 = MuseMonitor.GetComponent<UniOSCMuseMonitor>().t_r0;
            t_r1 = MuseMonitor.GetComponent<UniOSCMuseMonitor>().t_r1;
            t_r2 = MuseMonitor.GetComponent<UniOSCMuseMonitor>().t_r2;
            t_r3 = MuseMonitor.GetComponent<UniOSCMuseMonitor>().t_r3; 
            theta_relative = MuseMonitor.GetComponent<UniOSCMuseMonitor>().theta_relative;

            a0_abs = MuseMonitor.GetComponent<UniOSCMuseMonitor>().a0;
            a1_abs = MuseMonitor.GetComponent<UniOSCMuseMonitor>().a1;
            a2_abs = MuseMonitor.GetComponent<UniOSCMuseMonitor>().a2;
            a3_abs = MuseMonitor.GetComponent<UniOSCMuseMonitor>().a3;
            alpha_abs = MuseMonitor.GetComponent<UniOSCMuseMonitor>().alpha_abs;

            a_r0 = MuseMonitor.GetComponent<UniOSCMuseMonitor>().a_r0;
            a_r1 = MuseMonitor.GetComponent<UniOSCMuseMonitor>().a_r1;
            a_r2 = MuseMonitor.GetComponent<UniOSCMuseMonitor>().a_r2;
            a_r3 = MuseMonitor.GetComponent<UniOSCMuseMonitor>().a_r3;
            alpha_relative = MuseMonitor.GetComponent<UniOSCMuseMonitor>().alpha_relative;

            b0_abs = MuseMonitor.GetComponent<UniOSCMuseMonitor>().b0;
            b1_abs = MuseMonitor.GetComponent<UniOSCMuseMonitor>().b1;
            b2_abs = MuseMonitor.GetComponent<UniOSCMuseMonitor>().b2;
            b3_abs = MuseMonitor.GetComponent<UniOSCMuseMonitor>().b3;
            beta_abs = MuseMonitor.GetComponent<UniOSCMuseMonitor>().beta_abs;

            b_r0 = MuseMonitor.GetComponent<UniOSCMuseMonitor>().b_r0;
            b_r1 = MuseMonitor.GetComponent<UniOSCMuseMonitor>().b_r1;
            b_r2 = MuseMonitor.GetComponent<UniOSCMuseMonitor>().b_r2;
            b_r3 = MuseMonitor.GetComponent<UniOSCMuseMonitor>().b_r3;
            beta_relative = MuseMonitor.GetComponent<UniOSCMuseMonitor>().beta_relative;

            g0_abs = MuseMonitor.GetComponent<UniOSCMuseMonitor>().g0;
            g1_abs = MuseMonitor.GetComponent<UniOSCMuseMonitor>().g1;
            g2_abs = MuseMonitor.GetComponent<UniOSCMuseMonitor>().g2;
            g3_abs = MuseMonitor.GetComponent<UniOSCMuseMonitor>().g3;
            gamma_abs = MuseMonitor.GetComponent<UniOSCMuseMonitor>().gamma_abs;

            g_r0 = MuseMonitor.GetComponent<UniOSCMuseMonitor>().g_r0;
            g_r1 = MuseMonitor.GetComponent<UniOSCMuseMonitor>().g_r1;
            g_r2 = MuseMonitor.GetComponent<UniOSCMuseMonitor>().g_r2;
            g_r3 = MuseMonitor.GetComponent<UniOSCMuseMonitor>().g_r3;
            gamma_relative = MuseMonitor.GetComponent<UniOSCMuseMonitor>().gamma_relative;

            theta_beta_abs = MuseMonitor.GetComponent<UniOSCMuseMonitor>().theta_beta_abs;
            theta_beta_r = MuseMonitor.GetComponent<UniOSCMuseMonitor>().theta_beta_r;
            alpha_theta_abs = MuseMonitor.GetComponent<UniOSCMuseMonitor>().alpha_theta_abs;
            alpha_theta_r = MuseMonitor.GetComponent<UniOSCMuseMonitor>().alpha_theta_r;

            _gX = MuseMonitor.GetComponent<UniOSCMuseMonitor>().gyroX;
            _gY = MuseMonitor.GetComponent<UniOSCMuseMonitor>().gyroY;
            _gZ = MuseMonitor.GetComponent<UniOSCMuseMonitor>().gyroZ;

            _accX = MuseMonitor.GetComponent<UniOSCMuseMonitor>().accX;
            _accY = MuseMonitor.GetComponent<UniOSCMuseMonitor>().accY;
            _accZ = MuseMonitor.GetComponent<UniOSCMuseMonitor>().accZ;


            _blink = MuseMonitor.GetComponent<UniOSCMuseMonitor>().blink;
            _jc = MuseMonitor.GetComponent<UniOSCMuseMonitor>().jc;

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
                  msg.UpdateDataAt(4, delta_abs);
                  msg.UpdateDataAt(5, d_r0);
                  msg.UpdateDataAt(6, d_r1);
                  msg.UpdateDataAt(7, d_r2);
                  msg.UpdateDataAt(8, d_r3);
                  msg.UpdateDataAt(9, delta_relative);


                  msg.UpdateDataAt(10, t0_abs);
                  msg.UpdateDataAt(11, t1_abs);
                  msg.UpdateDataAt(12, t2_abs);
                  msg.UpdateDataAt(13, t3_abs);
                  msg.UpdateDataAt(14, theta_abs);
                  msg.UpdateDataAt(15, t_r0);
                  msg.UpdateDataAt(16, t_r1);
                  msg.UpdateDataAt(17, t_r2);
                  msg.UpdateDataAt(18, t_r3);
                  msg.UpdateDataAt(19, theta_relative);

                  msg.UpdateDataAt(20, a0_abs);
                  msg.UpdateDataAt(21, a1_abs);
                  msg.UpdateDataAt(22, a2_abs);
                  msg.UpdateDataAt(23, a3_abs);
                  msg.UpdateDataAt(24, alpha_abs);
                  msg.UpdateDataAt(25, a_r0);
                  msg.UpdateDataAt(26, a_r1);
                  msg.UpdateDataAt(27, a_r2);
                  msg.UpdateDataAt(28, a_r3);
                  msg.UpdateDataAt(29, alpha_relative);

                  msg.UpdateDataAt(30, b0_abs);
                  msg.UpdateDataAt(31, b1_abs);
                  msg.UpdateDataAt(32, b2_abs);
                  msg.UpdateDataAt(33, b3_abs);
                  msg.UpdateDataAt(34, beta_abs);
                  msg.UpdateDataAt(35, b_r0);
                  msg.UpdateDataAt(36, b_r1);
                  msg.UpdateDataAt(37, b_r2);
                  msg.UpdateDataAt(38, b_r3);
                  msg.UpdateDataAt(39, beta_relative);

                  msg.UpdateDataAt(40, g0_abs);
                  msg.UpdateDataAt(41, g1_abs);
                  msg.UpdateDataAt(42, g2_abs);
                  msg.UpdateDataAt(43, g3_abs);
                  msg.UpdateDataAt(44, gamma_abs);
                  msg.UpdateDataAt(45, g_r0);
                  msg.UpdateDataAt(46, g_r1);
                  msg.UpdateDataAt(47, g_r2);
                  msg.UpdateDataAt(48, g_r3);
                  msg.UpdateDataAt(49, gamma_relative);

                  msg.UpdateDataAt(50, theta_beta_abs);
                  msg.UpdateDataAt(51, theta_beta_r);
                  msg.UpdateDataAt(52, alpha_theta_abs);
                  msg.UpdateDataAt(53, alpha_theta_r);
     
                  msg.UpdateDataAt(54, _gX);
                  msg.UpdateDataAt(55, _gY);
                  msg.UpdateDataAt(56, _gZ);

                  msg.UpdateDataAt(57, _accX);
                  msg.UpdateDataAt(58, _accY);
                  msg.UpdateDataAt(59, _accZ);

                

                  msg.UpdateDataAt(60, _blink);
                  msg.UpdateDataAt(61, _jc);

            }

              _SendOSCMessage(_OSCeArg);
           
          }
        }
    }
