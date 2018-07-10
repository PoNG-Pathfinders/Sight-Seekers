using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LabelLandmarks : MonoBehaviour
{
	public Text landmarkName;
	void Start ()
	{
        Transform tr = this.transform;
        Transform par = tr.parent;
        GameObject game = par.gameObject;
        string str = game.name;
		GetComponent<TextMesh> ().text = str;
		
		landmarkName.text = str;
	}

	// Update is called once per frame

		void Update()
	{
		if (gameObject.tag == "ObjectOnMap")
		{
			landmarkName.text = this.transform.parent.gameObject.name;
			
		}
	}

}