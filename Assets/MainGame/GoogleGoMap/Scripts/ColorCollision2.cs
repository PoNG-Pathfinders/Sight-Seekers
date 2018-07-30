using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//player hits an object on the map, change color twice
public class ColorCollision2 : MonoBehaviour
//TODO: Have one script that is instantiated over multiple objects.
{
	public float recolorPeriod = 0;
	public int recolorTime = 3;
    public AudioSource jingle;
	bool hit = false;
    // Use this for initialization
    private PointTest test;
    void OnCollisionEnter(Collision other)
    {
        if (!hit)
        {
            Debug.Log("Collision Detected");
            if (GameObject.FindGameObjectWithTag("CompassButton").GetComponent<Compass_Button>().landmark != null)
            {
                if (this.transform.parent.gameObject.name == GameObject.FindGameObjectWithTag("Player").GetComponent<PointTest>().testObject.name)
                {
                    GameObject.FindGameObjectWithTag("CompassButton").GetComponent<Compass_Button>().Right_Answer.Play();
                    Debug.Log("You have reached the right destination!");
                    test = GameObject.FindGameObjectWithTag("Player").GetComponent<PointTest>();
                    test.testObject = null;
                    test.resetTime();
                    return;
                }
                else
                {
                    GameObject.FindGameObjectWithTag("CompassButton").GetComponent<Compass_Button>().Wrong_Answer.Play();
                    Debug.Log("You have reached the wrong destination!");
                    test = GameObject.FindGameObjectWithTag("Player").GetComponent<PointTest>();
                    test.testObject = null;
                    test.resetTime();
                    return;
                }
            }
            else
            {
                PointTest pt = GameObject.FindGameObjectWithTag("Player").GetComponent<PointTest>();
                pt.testObject = null;
                pt.findText.text = "";
                jingle.Play();
                gameObject.GetComponent<Renderer>().material.color = Color.cyan;
                foreach (Renderer r in GetComponentsInChildren<Renderer>())
                {
                    r.material.color = Color.cyan;
                }
                recolorPeriod = recolorTime;
                hit = true;
                return;
            }
        }
    }
	void Update()
	{
		if(hit)
		{
            if (recolorPeriod > 0)
			{
				recolorPeriod-=Time.deltaTime;
			}
			if (recolorPeriod < 0)
			{
				gameObject.GetComponent<Renderer>().material.color = Color.white;
                foreach (Renderer r in GetComponentsInChildren<Renderer>())
                {
                    r.material.color = Color.white;
                }
                    hit = false;
			}
		}
	}

}