using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour {

    void Awake()
    {

        if (Application.isPlaying)
        {

            DontDestroyOnLoad(this.gameObject);
        }
    }

}
