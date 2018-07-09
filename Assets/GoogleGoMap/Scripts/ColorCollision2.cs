using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//player hits an object on the map, change color twice
public class ColorCollision2 : MonoBehaviour
//TODO: Have one script that is instantiated over multiple objects.
{
	public float recolorPeriod = 0;
	public int recolorTime = 3;
	bool hit = false;
	// Use this for initialization

	void OnCollisionEnter(Collision other)
	{
		Debug.Log ("Collision Detected");
		gameObject.GetComponent<Renderer>().material.color = Color.cyan;
		recolorPeriod = recolorTime;
		hit = true;
	}

	void Update()
	{
		if(hit)
		{
			if(recolorPeriod > 0)
			{
				recolorPeriod-=Time.deltaTime;
			}
			if (recolorPeriod < 0)
			{
				gameObject.GetComponent<Renderer>().material.color = Color.white;
				hit = false;
			}
		}
	}

}