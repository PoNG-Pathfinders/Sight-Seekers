using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    int rotation = 0;
	
	void Update ()
    {
        this.transform.localEulerAngles = new Vector3(0, 0, rotation--);
	}
}
