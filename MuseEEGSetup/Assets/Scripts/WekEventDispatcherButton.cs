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


namespace UniOSC{

	/// <summary>
	/// Dispatcher button that forces a OSCConnection to send a OSC Message.
	/// Two separate states: Down and Up 
	/// </summary>
	[AddComponentMenu("UniOSC/WekEventDispatcherButton")]
	[ExecuteInEditMode]
	public class WekEventDispatcherButton: UniOSCEventDispatcher {
        
        public string ButtonName;
        public bool bundleMode, showGUI;
        public string oscOutAddress2;
        private List<UniOSCEventDispatcherCB> senderList = new List<UniOSCEventDispatcherCB>();
        public int gesture=1;
		public bool _btnDown, _btnUp;
        public float xPos, yPos;
        private GUIStyle _gs;

        public override void Awake()
		{
			base.Awake ();
			
		}

        public override void OnEnable()
        {
            base.OnEnable();

            if (bundleMode)
            {
                SetBundleMode(true);
                ClearData();
                AppendData(new OscMessage(oscOutAddress, 0));
                AppendData(new OscMessage(oscOutAddress2, 0));
            }
        }

		public override void OnDisable ()
		{
			base.OnDisable ();
        }
		void OnGUI(){
			if(!showGUI)return;
			RenderGUI();
		}

		void RenderGUI(){
            
			_gs = new GUIStyle(GUI.skin.button);
			_gs.fontSize=11;
           
            //gs.padding = new RectOffset(2,2,2,2);

            GUIScaler.Begin();

			Event e = Event.current;
			GUI.BeginGroup(new Rect((Screen.width/GUIScaler.GuiScale.x)*xPos,(Screen.height/GUIScaler.GuiScale.y)*yPos,(Screen.width/GUIScaler.GuiScale.x/2),(Screen.height/GUIScaler.GuiScale.y/2)));

			GUILayout.BeginVertical();
			GUILayout.FlexibleSpace();
			
			StringBuilder sb = new StringBuilder();
			sb.AppendLine("");
			sb.AppendLine(ButtonName);
			GUIContent buttonText = new GUIContent(sb.ToString());
			Rect buttonRect = GUILayoutUtility.GetRect(buttonText,_gs); 
			buttonRect.width *=1.0f;
			buttonRect.height*=1.0f;
      			
			if (e.isMouse && buttonRect.Contains(e.mousePosition)) { 
				if(e.type == EventType.MouseDown){
					SendOSCMessageDown();
                }
				if(e.type == EventType.MouseUp){
					SendOSCMessageUp();
				}
			} 
			
			GUI.Button(buttonRect, buttonText,_gs);
			
			GUILayout.EndVertical();
			GUI.EndGroup();

			GUIScaler.End();
		}

		/// <summary>
		/// Sends the OSC message with the downOSCDataValue.
		/// </summary>
		public void SendOSCMessageDown(){
			if(_OSCeArg.Packet is OscMessage)
			{
				((OscMessage)_OSCeArg.Packet).UpdateDataAt(0, gesture);
            }
			else if(_OSCeArg.Packet is OscBundle)
			{
                foreach (OscMessage m in ((OscBundle)_OSCeArg.Packet).Messages)
                {
                    m.Address = oscOutAddress;
                    m.UpdateDataAt(0, gesture);
                }				
			}
			_SendOSCMessage(_OSCeArg);
        }

		/// <summary>
		/// Sends the OSC message with the upOSCDataValue.
		/// </summary>
		public void SendOSCMessageUp(){
            Debug.Log("addresscount" + senderList.Count);
            if (_OSCeArg.Packet is OscMessage)
			{
                ((OscMessage)_OSCeArg.Packet).UpdateDataAt(0, 0);
            }
			else if(_OSCeArg.Packet is OscBundle)
			{
                foreach (OscMessage m in ((OscBundle)_OSCeArg.Packet).Messages)
                {
                   m.Address = oscOutAddress2;
                   m.UpdateDataAt(0, 0);
                }              
			}

			_SendOSCMessage(_OSCeArg);
            
        }

        public void buttonClicked()
        {
            _btnDown = true;
            if (_btnDown == true)
            {
                SendOSCMessageDown();
                Invoke("_buttonDown", 0.3f);
                
            }
            else { 
                _btnUp = true;
                if (_btnUp == true)
                {
                    SendOSCMessageUp();
                    _btnUp = false;
                }
            }
        }
        public void _buttonDown()
        {
            _btnDown = false;
        }


	}
}