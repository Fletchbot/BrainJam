//ink Font Browser v.1.0
//Script By Kourosh Ghahremani

using UnityEngine;
using UnityEditor;
using System;
using System.Collections;

using ink;// namespace for the ink class library.

public class FontBrowser : EditorWindow {
	
	// String array to hold installed font families.
	static string[] fontFamilies;
	// Preview Text.
	static string txtPreview = "Type Here"; 
	// To Hold font type (OpenType or TrueType).
	static string fontType; 
	//GUIStyle to use for preview text.
	static GUIStyle guiStyle = new GUIStyle();
	// index to GUILayout Popup menu for font families.
	static int index = 0; 
	//integer to compare with current index to identify change in Popup menu.
	static int indexLast = -1; 
	//To check whether the starup method is implemeted.
	static bool isSetUp = false; 
	//Imported font.
	static Font newFont =null;
	// Use this for initialization
	[MenuItem ("Window/ink Font Browser")]

	static void init () {
		
		EditorWindow.GetWindow (typeof (FontBrowser));
		
	}
	
	//Will run the startup method when the extension is run from window menu or when the unity editor starts.
	void OnEnable () {
    	StartUp();
	} 
	
	// Update is called once per frame
	void OnGUI () {
		
		//Show a message if the loading takes time.
		if(!isSetUp){
			GUILayout.Label("Retrieving installed fonts...");
		} else {
			
			if(indexLast != index) 
			{
				indexLast = index;
				ChangeFont(true);
	
			}
			EditorGUILayout.Space();
			
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.PrefixLabel("Installed Fonts:");
			if(GUILayout.Button("<",GUILayout.Width(20))){
				index--;
				index = Mathf.Max(0,index);
			}
			//Show the installed fonts in windows.
			index = EditorGUILayout.Popup(index,fontFamilies);
			if(GUILayout.Button(">",GUILayout.Width(20))){
				index++;
				index = Mathf.Min(fontFamilies.Length-1,index);
			}
			EditorGUILayout.EndHorizontal();
			//Shows the font type.
			EditorGUILayout.LabelField("Font Type:",fontType);
			EditorGUILayout.Space();
			EditorGUILayout.BeginHorizontal();
			
			//Preview TextField.
			txtPreview = GUILayout.TextField(txtPreview,guiStyle);
			EditorGUILayout.EndHorizontal();
			
			EditorGUILayout.Space();
			
			if(GUILayout.Button("Import")){
				ChangeFont(false);
			}
			//Update button to read the font files again in case new fonts has been installed in windows.
			if(GUILayout.Button("Update")){
				StartUp();
			}
		}

	}
	
	//It's run to retrieve installed fonts and configure GUIStyle for Preview Text.
	static void StartUp()
	{
		guiStyle.fontSize = 24;
		guiStyle.normal.textColor = Color.white;
		guiStyle.alignment = TextAnchor.MiddleCenter;
		fontFamilies = FontLib.GetInstalledFonts();	//Retrieve installed fonts.
		FontLib.SaveFontList();//Saves an xml file containing the list of all installed fonts.
		isSetUp = true;
	}
	
	//Is called when a new font is to be imported in project folder.
	//The isPreview boolean indicates whether the import is for the purpose of preview or not.
	//If it's false, the font will be imported with full name.
	//If it's true, the font will be imported and the will be named "preview".
	static void ChangeFont(bool isPrivew)
	{
		fontType = FontLib.GetFontType(fontFamilies[index]);
		newFont = FontLib.ImportFont(fontFamilies[index], isPrivew);
		guiStyle.font = newFont;
	}
}
