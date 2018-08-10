using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using OSCsharp.Data;

namespace UniOSC
{

    /// <summary>
    /// Muse EEG Headset: MuseMonitor OSC Manager
    /// </summary>
    [AddComponentMenu("UniOSC/MuseMonitor")]
    public class UniOSCMuseMonitor : UniOSCEventTarget
    {
        public bool AllValues, ReceiveEEG, XYZ, deltaWek;

        public string Delta_Address;
        public string Theta_Address;
        public string Alpha_Address;
        public string Beta_Address;
        public string Gamma_Address;

        public string Gyro_Address;
        public string Acc_Address;

        public string Blink_Address;
        public string JawClench_Address;
        public string TouchingForehead_Address;
        public string Horseshoe_Address;
        public string Batt_Address;
        public string eeg_Address;

        public static UniOSCMuseMonitor main;

        public float eeg0, eeg1, eeg2, eeg3, eeg4;
        public float gyroX, gyroY, gyroZ, accX, accY, accZ;
        public float blink, jc, touchingforehead, batt, hs0, hs1, hs2, hs3;

        public float d0, d1, d2, d3, t0, t1, t2, t3, a0, a1, a2, a3, b0, b1, b2, b3, g0, g1, g2, g3;
        public float delta_abs, alpha_abs, theta_abs, beta_abs, gamma_abs;

        /*  public float theta_beta_abs, alpha_theta_abs;
        public float tb_abs0, tb_abs1, tb_abs2, tb_abs3, at_abs0, at_abs1, at_abs2, at_abs3;
        public float delta_relative, alpha_relative, theta_relative, beta_relative, gamma_relative, theta_beta_r, alpha_theta_r;
        public float a_r0, a_r1, a_r2, a_r3, b_r0, b_r1, b_r2, b_r3, g_r0, g_r1, g_r2, g_r3, t_r0, t_r1, t_r2, t_r3, d_r0, d_r1, d_r2, d_r3;
        public float tb_r0, tb_r1, tb_r2, tb_r3, at_r0, at_r1, at_r2, at_r3;*/

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
                if (deltaWek) _oscAddresses.Add(Delta_Address);
                _oscAddresses.Add(Theta_Address);
                _oscAddresses.Add(Alpha_Address);
                _oscAddresses.Add(Beta_Address);
                _oscAddresses.Add(Gamma_Address);
                if (XYZ)
                {
                    _oscAddresses.Add(Gyro_Address);
                    _oscAddresses.Add(Acc_Address);
                }

                _oscAddresses.Add(Blink_Address);
                _oscAddresses.Add(JawClench_Address);
                _oscAddresses.Add(TouchingForehead_Address);
                _oscAddresses.Add(Horseshoe_Address);
                _oscAddresses.Add(Batt_Address);

                if (ReceiveEEG) _oscAddresses.Add(eeg_Address);

            }
        }
        private void FixedUpdate()
        {/*
            if (AllValues)
            {
                d_r0 = (d0 / (d0 + a0 + b0 + g0 + t0));
                d_r1 = (d1 / (d1 + a1 + b1 + g1 + t1));
                d_r2 = (d2 / (d2 + a2 + b2 + g2 + t2));
                d_r3 = (d3 / (d3 + a3 + b3 + g3 + t3));

                t_r0 = (t0 / (t0 + a0 + d0 + g0 + b0));
                t_r1 = (t1 / (t1 + a1 + d1 + g1 + b1));
                t_r2 = (t2 / (t2 + a2 + d2 + g2 + b2));
                t_r3 = (t3 / (t3 + a3 + d3 + g3 + b3));

                a_r0 = (a0 / (a0 + b0 + d0 + g0 + t0));
                a_r1 = (a1 / (a1 + b1 + d1 + g1 + t1));
                a_r2 = (a2 / (a2 + b2 + d2 + g2 + t2));
                a_r3 = (a3 / (a3 + b3 + d3 + g3 + t3));

                b_r0 = (b0 / (b0 + a0 + d0 + g0 + t0));
                b_r1 = (b1 / (b1 + a1 + d1 + g1 + t1));
                b_r2 = (b2 / (b2 + a2 + d2 + g2 + t2));
                b_r3 = (b3 / (b3 + a3 + d3 + g3 + t3));

                g_r0 = (g0 / (g0 + a0 + d0 + b0 + t0));
                g_r1 = (g1 / (g1 + a1 + d1 + b1 + t1));
                g_r2 = (g2 / (g2 + a2 + d2 + b2 + t2));
                g_r3 = (g3 / (g3 + a3 + d3 + b3 + t3)); 

                tb_abs0 = ( t0/ b0);
                tb_abs1 = (t1 / b1);
                tb_abs2 = (t2 / b2);
                tb_abs3 = (t3 / b3);

                at_abs0 = (a0 / t0);
                at_abs1 = (a1 / t1);
                at_abs2 = (a2 / t2);
                at_abs3 = (a3 / t3);

                tb_r0 = (t_r0 / b_r0);
                tb_r1 = (t_r1 / b_r1);
                tb_r2 = (t_r2 / b_r2);
                tb_r3 = (t_r3 / b_r3);

                at_r0 = (a_r0 / t_r0);
                at_r1 = (a_r1 / t_r1);
                at_r2 = (a_r2 / t_r2);
                at_r3 = (a_r3 / t_r3);
                
            } else if (AllValues == false)
            {
                theta_beta_abs = (theta_abs / beta_abs);
                alpha_theta_abs = (alpha_abs / theta_abs);

                delta_relative = (delta_abs / (delta_abs + theta_abs + alpha_abs + beta_abs + gamma_abs));
                theta_relative = (theta_abs / (delta_abs + theta_abs + alpha_abs + beta_abs + gamma_abs));
                alpha_relative = (alpha_abs / (delta_abs + theta_abs + alpha_abs + beta_abs + gamma_abs));
                beta_relative = (beta_abs / (delta_abs + theta_abs + alpha_abs + beta_abs + gamma_abs));
                gamma_relative = (gamma_abs / (delta_abs + theta_abs + alpha_abs + beta_abs + gamma_abs));
                theta_beta_r = (theta_relative / beta_relative);
                alpha_theta_r = (alpha_relative / theta_relative);
            }          */

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

            if (deltaWek)
            {
                if (String.Equals(args.Address, Delta_Address))
                {
                    if (AllValues)
                    {
                        d0 = (float)msg.Data[0];
                        d1 = (float)msg.Data[1];
                        d2 = (float)msg.Data[2];
                        d3 = (float)msg.Data[3];
                    }
                    else delta_abs = (float)msg.Data[0];
                }
            }

            if (String.Equals(args.Address, Theta_Address))
            {
                if (AllValues)
                {
                    t0 = (float)msg.Data[0];
                    t1 = (float)msg.Data[1];
                    t2 = (float)msg.Data[2];
                    t3 = (float)msg.Data[3];
                }
                else theta_abs = (float)msg.Data[0];
            }
            if (String.Equals(args.Address, Alpha_Address))
            {
                if (AllValues)
                {
                    a0 = (float)msg.Data[0];
                    a1 = (float)msg.Data[1];
                    a2 = (float)msg.Data[2];
                    a3 = (float)msg.Data[3];
                }
                else alpha_abs = (float)msg.Data[0];
            }
            if (String.Equals(args.Address, Beta_Address))
            {
                if (AllValues)
                {
                    b0 = (float)msg.Data[0];
                    b1 = (float)msg.Data[1];
                    b2 = (float)msg.Data[2];
                    b3 = (float)msg.Data[3];
                }
                else beta_abs = (float)msg.Data[0];
            }
            if (String.Equals(args.Address, Gamma_Address))
            {
                if (AllValues)
                {
                    g0 = (float)msg.Data[0];
                    g1 = (float)msg.Data[1];
                    g2 = (float)msg.Data[2];
                    g3 = (float)msg.Data[3];
                }
                else gamma_abs = (float)msg.Data[0];
            }
            if (XYZ)
            {
                if (String.Equals(args.Address, Gyro_Address))
                {
                    gyroX = (float)msg.Data[0];
                    gyroY = (float)msg.Data[1];
                    gyroZ = (float)msg.Data[2];
                }
                if (String.Equals(args.Address, Acc_Address))
                {
                    accX = (float)msg.Data[0];
                    accY = (float)msg.Data[1];
                    accZ = (float)msg.Data[2];
                }
            }
            if (String.Equals(args.Address, Horseshoe_Address))
            {
                hs0 = (float)msg.Data[0];
                hs1 = (float)msg.Data[1];
                hs2 = (float)msg.Data[2];
                hs3 = (float)msg.Data[3];
            }
            if (String.Equals(args.Address, Blink_Address))
            {
                blink = (int)msg.Data[0];
                Invoke("blinkOff", 0.3f);
            }
            if (String.Equals(args.Address, JawClench_Address))
            {
                jc = (int)msg.Data[0];
                Invoke("jawOff", 0.3f);
            }
            if (String.Equals(args.Address, TouchingForehead_Address))
            {
                touchingforehead = (int)msg.Data[0];
            }
            if (String.Equals(args.Address, Batt_Address))
            {
                batt = (int)msg.Data[0] / 100;
            }
            if (ReceiveEEG)
            {
                if (String.Equals(args.Address, eeg_Address))
                {
                    eeg0 = (float)msg.Data[0];
                    eeg1 = (float)msg.Data[1];
                    eeg2 = (float)msg.Data[2];
                    eeg3 = (float)msg.Data[3];
                    eeg4 = (float)msg.Data[4];
                }
            }
        }
        void blinkOff()
        {
            blink = 0;
        }
        void jawOff()
        {
            jc = 0;
        }
    }
}