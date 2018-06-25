using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateObjects : MonoBehaviour {
    public bool deactivateOnStart;
	// Use this for initialization
	void Start () {
        if (deactivateOnStart)
        {
            gameObject.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void SetActive(bool activate)
    {
        if (activate)
        {
            gameObject.SetActive(true);
        }
    }
    public void SetDeactive(bool deactivate)
    {
        if (deactivate)
        {
            gameObject.SetActive(false);
        }
    }
}
