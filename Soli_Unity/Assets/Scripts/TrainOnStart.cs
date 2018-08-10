using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainOnStart : MonoBehaviour {
    public GameObject train;
	// Use this for initialization
	void Start () {
        Invoke("trainOnStart", 1.5f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void trainOnStart()
    {
        train.GetComponent<UniOSC.WekEventDispatcherButton>().ButtonClick(true);
    }
}
