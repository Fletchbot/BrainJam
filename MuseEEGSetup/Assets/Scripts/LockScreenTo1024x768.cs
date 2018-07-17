using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockScreenTo1024x768 : MonoBehaviour {
    private bool isFullscreen;
    // Use this for initialization
    void Start()
    {
        if (Screen.fullScreen == true)
        {
            isFullscreen = true;

        }
        else
        {
            isFullscreen = false;
        }

        if (Screen.currentResolution.width >= 1920 && Screen.currentResolution.height >= 1080)
        {
            Screen.SetResolution(1024, 768, isFullscreen);
        }
    }
}
