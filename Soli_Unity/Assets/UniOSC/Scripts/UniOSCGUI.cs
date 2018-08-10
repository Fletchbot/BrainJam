/*
* UniOSC
* Copyright © 2014-2015 Stefan Schlupek
* All rights reserved
* info@monoflow.org
*/
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Text;
using OSCsharp.Data;

namespace UniOSC{

	/// <summary>
	/// GUI class that mimics the UniOSC editor interface for runtime use
	/// You can start/stop the OSCConnections and trace OSC data messages
	/// </summary>
	[AddComponentMenu("UniOSC/OSCGUI")]
	[ExecuteInEditMode]
	public class UniOSCGUI : MonoBehaviour {
		
		#region private
		private string _IPAddress;
		private bool _showGUI= false;
		private GUIStyle gs_textArea ;
		private Vector2 traceScrollpos = new Vector2();
		private string msgMode;
		private OscMessage _oscMessage;
		private OscPacket _oscPacket;
		private string _oscTraceStr="";
		private StringBuilder _osctraceStrb = new StringBuilder();
		private Texture2D tex_logo;
        #endregion

        #region public
        public GameObject GUIpanel;
		public bool ShowInEditMode;
		public bool traceMessages;
        public float conScale;
        
		#endregion

		#region Start
		void Awake(){
            if (Application.isPlaying)
            {
                DontDestroyOnLoad(this.gameObject);
            }
        }

		void Start () {

        }

		void OnEnable(){
            conScale = 0.5f;

			_IPAddress = UniOSCUtils.GetLocalIPAddress();
			foreach(var con in UniOSCConnection.Instances){
				con.OSCMessageReceived+=OnOSCMessageReceived;
				con.OSCMessageSend+=OnOSCMessageSended;
			}
			tex_logo = Resources.Load(UniOSCUtils.LOGO32_NAME,typeof(Texture2D)) as Texture2D;
        }

		#endregion Start

		void OnDisable(){
			foreach(var con in UniOSCConnection.Instances){
				con.OSCMessageReceived-=OnOSCMessageReceived;
				con.OSCMessageSend-=OnOSCMessageSended;
			}
		}
	
		void Update () {
		
		}

		#region GUI

		void OnGUI(){
			if(!Application.isPlaying){
				if(!ShowInEditMode)return;
			}

			GUIScaler.Begin();
			GUILayout.BeginVertical(GUILayout.Width(Screen.width/GUIScaler.GuiScale.x),GUILayout.Height(Screen.height/(GUIScaler.GuiScale.y)));//
			GUILayout.Space(1f);

            GUIpanel.GetComponent<ActivateObjects>().SetActive(true);

			#region IPAddress
				GUILayout.BeginHorizontal();
					GUILayout.Space(5f);
					UniOSCUtils.DrawTexture(tex_logo);
					GUILayout.FlexibleSpace();
					GUILayout.Label(_IPAddress,GUILayout.Height(15f));
					GUILayout.FlexibleSpace();
					_showGUI = GUILayout.Toggle(_showGUI,new GUIContent(_showGUI? "Hide GUI":"Show GUI"),GUI.skin.button,GUILayout.Height(30),GUILayout.Width(130));
					GUILayout.Space(5f);
				GUILayout.EndHorizontal();
			#endregion IPAddress

			if(!_showGUI){
				GUILayout.EndVertical();
					GUIScaler.End();
                GUIpanel.GetComponent<ActivateObjects>().SetDeactive(true);
                return;
			}

			GUILayout.Space(1f);

			GUILayout.BeginHorizontal();

			GUILayout.FlexibleSpace();

			#region OSCConnectionsL
			GUILayout.BeginVertical();

            UniOSCConnection.Instances[0].RenderGUI();
            UniOSCConnection.Instances[1].RenderGUI();
            UniOSCConnection.Instances[2].RenderGUI();
            GUILayout.Space(0.1f);
            UniOSCConnection.Instances[0].oscOutIPAddress = _IPAddress;
            UniOSCConnection.Instances[1].oscOutIPAddress = _IPAddress;
            UniOSCConnection.Instances[2].oscOutIPAddress = _IPAddress;

            GUILayout.EndVertical();
            #endregion OSCConnectionsL

			GUILayout.FlexibleSpace();

            #region OSCConnectionsR
            GUILayout.BeginVertical();

            UniOSCConnection.Instances[3].RenderGUI();
            UniOSCConnection.Instances[4].RenderGUI();
            UniOSCConnection.Instances[5].RenderGUI();
            GUILayout.Space(0.1f);
            UniOSCConnection.Instances[3].oscOutIPAddress = _IPAddress;
            UniOSCConnection.Instances[4].oscOutIPAddress = _IPAddress;
            UniOSCConnection.Instances[5].oscOutIPAddress = _IPAddress;

            GUILayout.EndVertical();
            #endregion OSCConnectionsR

           // GUILayout.FlexibleSpace();
/*
			#region trace
			if(gs_textArea == null){
			gs_textArea = new GUIStyle(GUI.skin.textArea);
			gs_textArea.fontSize=10;
			gs_textArea.normal.textColor =  Color.yellow;
			}

			GUILayout.BeginVertical(GUILayout.Width(200f));

				traceScrollpos = GUILayout.BeginScrollView(traceScrollpos,  GUILayout.Height (((Screen.height/GUIScaler.GuiScale.y)*0.75f)-40f), GUILayout.ExpandHeight (false));
					GUILayout.TextArea(_oscTraceStr,gs_textArea,GUILayout.ExpandHeight(true));
				GUILayout.EndScrollView();
				
				if (GUILayout.Button ("Clear Trace",GUILayout.Height(30))){
					_oscTraceStr = "";
					_osctraceStrb = new StringBuilder();
				}

			GUILayout.EndVertical();
			#endregion trace
            */
				GUILayout.Space(5f);
			GUILayout.EndHorizontal();

			GUILayout.EndVertical();

			GUIScaler.End();
		}

		#endregion GUI

		#region Message

		private void OnOSCMessageSended(object sender, UniOSCEventArgs args){
			if(traceMessages)_TraceOSCMessage( sender,args,false);
		}

		private void OnOSCMessageReceived(object sender, UniOSCEventArgs args){
				if(!_showGUI)return;
			if(traceMessages)_TraceOSCMessage(sender,args,true);
		}

		private void _TraceOSCMessage(object sender,UniOSCEventArgs args,bool oscIn){
		
			msgMode = oscIn ? "IN" : "OUT";
			//_oscMessage = args.Message;
			_oscPacket = args.Packet;
			_osctraceStrb.AppendLine("----Message "+msgMode+"-----");
			if(oscIn){
				_osctraceStrb.AppendLine("Port:" +((UniOSCConnection)sender).oscPort);
			}else{
				_osctraceStrb.AppendLine("Destination IP:" +((UniOSCConnection)sender).oscOutIPAddress);
				_osctraceStrb.AppendLine("Port:" +((UniOSCConnection)sender).oscOutPort);
			}


			if (_oscPacket.IsBundle) 
			{
				_osctraceStrb.AppendLine("Bundle:");
				foreach (var m in ((OscBundle)_oscPacket).Messages)
				{
					_osctraceStrb.AppendLine(" Address: " + m.Address.ToString());
					
					if (m.Data.Count > 0)
					{
						for (int i = 0; i < m.Data.Count; i++)
						{
							_osctraceStrb.AppendLine("  Data: " + m.Data[i].ToString());
						}
						
					}
					
				}//for

			}
			else
			{
				_osctraceStrb.AppendLine("Address: " + _oscPacket.Address.ToString());
				
				if (((OscMessage)_oscPacket).Data.Count > 0)
				{
					for (int i = 0; i < ((OscMessage)_oscPacket).Data.Count; i++)
					{
						_osctraceStrb.AppendLine("Data: " + ((OscMessage)_oscPacket).Data[i].ToString());
					}
					
				}
			}

			
			_oscTraceStr = _osctraceStrb.ToString();
			//autoscroll to bottom
			traceScrollpos = new Vector2(0f,Mathf.Infinity);
		}
		#endregion Message

	}
}