using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {
    int rotation;

	// Use this for initialization
	void Start ()
    {
        rotation = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
        this.transform.localEulerAngles = new Vector3(0, 0, rotation--);
	}
}
