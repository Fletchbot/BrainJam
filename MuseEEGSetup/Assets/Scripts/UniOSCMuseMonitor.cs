using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using OSCsharp.Data;

namespace UniOSC{

	/// <summary>
	/// Muse EEG Headset: MuseMonitor OSC Manager
	/// </summary>
	[AddComponentMenu("UniOSC/MuseMonitor")]
	public class UniOSCMuseMonitor :  UniOSCEventTarget {

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

		public static UniOSCMuseMonitor main;

		public float d0,d1,d2,d3,t0,t1,t2,t3,a0,a1,a2,a3,b0,b1,b2,b3,g0,g1,g2,g3;
        public float gyroX, gyroY, gyroZ, accX, accY, accZ;
		public float blink, jc, touchingforehead, batt, hs0, hs1, hs2, hs3;

        public float a_r0, a_r1, a_r2, a_r3, b_r0,b_r1,b_r2,b_r3, g_r0, g_r1, g_r2, g_r3, t_r0, t_r1, t_r2, t_r3,d_r0,d_r1,d_r2,d_r3;
        public float delta_relative, alpha_relative, theta_relative, beta_relative, gamma_relative, delta_abs, alpha_abs, theta_abs, beta_abs, gamma_abs;
        public float theta_beta_abs, theta_beta_r, alpha_theta_abs, alpha_theta_r;

		void Awake(){
			main = this;
		}

		public override void OnEnable(){
			_Init();
			base.OnEnable();
		}

		private void _Init(){

			//receiveAllAddresses = false;
			_oscAddresses.Clear();
			if(!_receiveAllAddresses){
				_oscAddresses.Add(Delta_Address);
				_oscAddresses.Add(Theta_Address);
				_oscAddresses.Add(Alpha_Address);
				_oscAddresses.Add(Beta_Address);
				_oscAddresses.Add(Gamma_Address);

				_oscAddresses.Add(Gyro_Address);
				_oscAddresses.Add(Acc_Address);

				_oscAddresses.Add(Blink_Address);
				_oscAddresses.Add(JawClench_Address);
				_oscAddresses.Add (TouchingForehead_Address);
				_oscAddresses.Add(Horseshoe_Address);
				_oscAddresses.Add (Batt_Address);
			}
		}
        private void FixedUpdate()
        {
            d_r0 = (d0 / (d0 + a0 + b0 + g0 + t0));
            d_r1 = (d1 / (d1 + a1 + b1 + g1 + t1));
            d_r2 = (d2 / (d2 + a2 + b2 + g2 + t2));
            d_r3 = (d3 / (d3 + a3 + b3 + g3 + t3));

            delta_abs = (4 / (d0 + d1 + d2 + d3));
            delta_relative = (4 / (d_r0 + d_r1 + d_r2 + d_r3));

            t_r0 = (t0 / (t0 + a0 + d0 + g0 + b0));
            t_r1 = (t1 / (t1 + a1 + d1 + g1 + b1));
            t_r2 = (t2 / (t2 + a2 + d2 + g2 + b2));
            t_r3 = (t3 / (t3 + a3 + d3 + g3 + b3));

            theta_abs = (4 / (t0 + t1 + t2 + t3));
            theta_relative = (4 / (t_r0 + t_r1 + t_r2 + t_r3));

            a_r0 = (a0 / (a0 + b0 + d0 + g0 + t0));
            a_r1 = (a1 / (a1 + b1 + d1 + g1 + t1));
            a_r2 = (a2 / (a2 + b2 + d2 + g2 + t2));
            a_r3 = (a3 / (a3 + b3 + d3 + g3 + t3));

            alpha_abs = (4 / (a0 + a1 + a2 + a3));
            alpha_relative = (4 / (a_r0 + a_r1 + a_r2 + a_r3));

            b_r0 = (b0 / (b0 + a0 + d0 + g0 + t0));
            b_r1 = (b1 / (b1 + a1 + d1 + g1 + t1));
            b_r2 = (b2 / (b2 + a2 + d2 + g2 + t2));
            b_r3 = (b3 / (b3 + a3 + d3 + g3 + t3));

            beta_abs = (4 / (b0 + b1 + b2 + b3));
            beta_relative = (4 / (b_r0 + b_r1 + b_r2 + b_r3));

            g_r0 = (g0 / (g0 + a0 + d0 + b0 + t0));
            g_r1 = (g1 / (g1 + a1 + d1 + b1 + t1));
            g_r2 = (g2 / (g2 + a2 + d2 + b2 + t2));
            g_r3 = (g3 / (g3 + a3 + d3 + b3 + t3));

            gamma_abs = (4 / (g0 + g1 + g2 + g3));
            gamma_relative = (4 / (g_r0 + g_r1 + g_r2 + g_r3));

            theta_beta_abs = (theta_abs / beta_abs);
            theta_beta_r = (theta_relative / beta_relative);
            alpha_theta_abs = (alpha_abs / theta_abs);
            alpha_theta_r = (alpha_relative / theta_relative);

        }


        public override void OnOSCMessageReceived(UniOSCEventArgs args){
			//args.Address
			//(OscMessage)args.Packet) => The OscMessage object
			//(OscMessage)args.Packet).Data  (get the data of the OscMessage as an object[] array)
			OscMessage msg = (OscMessage)args.Packet;

			//if (msg.Data.Count < 1)
			//	return;
			//if (!(msg.Data [0] is System.Single))
			//	return;

			if (String.Equals (args.Address, Delta_Address)) {
				d0 = (float)msg.Data [0];
				d1 = (float)msg.Data [1];
				d2 = (float)msg.Data [2];
				d3 = (float)msg.Data [3];
			}
			if (String.Equals (args.Address, Theta_Address)) {
				t0 = (float)msg.Data [0];
				t1 = (float)msg.Data [1];
				t2 = (float)msg.Data [2];
				t3 = (float)msg.Data [3];
			}
			if (String.Equals (args.Address, Alpha_Address)) {
				a0 = (float)msg.Data [0];
				a1 = (float)msg.Data [1];
				a2 = (float)msg.Data [2];
				a3 = (float)msg.Data [3];
			}
			if (String.Equals (args.Address, Beta_Address)) {
				b0 = (float)msg.Data [0];
				b1 = (float)msg.Data [1];
				b2 = (float)msg.Data [2];
				b3 = (float)msg.Data [3];
			}
			if (String.Equals (args.Address, Gamma_Address)) {
				g0 = (float)msg.Data [0];
				g1 = (float)msg.Data [1];
				g2 = (float)msg.Data [2];
				g3 = (float)msg.Data [3];
			}
			if (String.Equals (args.Address, Gyro_Address)) {
				gyroX = (float)msg.Data [0];
				gyroY = (float)msg.Data [1];
				gyroZ = (float)msg.Data [2];
			}
			if (String.Equals (args.Address, Acc_Address)) {
				accX = (float)msg.Data [0];
				accY = (float)msg.Data [1];
				accZ = (float)msg.Data [2];
			}
			if(String.Equals(args.Address,Horseshoe_Address)){
				hs0 = (float)msg.Data [0];
				hs1 = (float)msg.Data [1];
				hs2 = (float)msg.Data [2];
				hs3 = (float)msg.Data [3];
			}
			if (String.Equals (args.Address, Blink_Address)) {
				blink = (int)msg.Data [0];
				Invoke ("blinkOff", 0.3f);
			}
			if (String.Equals (args.Address, JawClench_Address)) {
				jc = (int)msg.Data [0];
				Invoke ("jawOff", 0.3f);
			}
			if (String.Equals (args.Address, TouchingForehead_Address)) {
				touchingforehead = (int)msg.Data [0];
			}
			if (String.Equals (args.Address, Batt_Address)) {
				batt = (int)msg.Data [0]/100;
			}
		}
		void blinkOff(){
			blink = 0;
		}
		void jawOff(){
			jc = 0;
		}
	}
}