using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointTest : MonoBehaviour {
    public AudioSource findDing;
    public AudioSource lostDing;
    public Text findText;
    DateTime Secs;
    [HideInInspector]
    public GameObject testObject;
    // Use this for initialization
    void Start () {
    Secs = DateTime.Now;
    }

    // Update is called once per frame
    void Update()
    {
        if (testObject == null)
        {
            GameObject[] objectsOnMap;
            int secsDiff = DateTime.Now.Subtract(Secs).Seconds;
            int findObject;
            if (secsDiff >= 30)
            {
                findDing.Play();
                objectsOnMap = GameObject.FindGameObjectsWithTag("ObjectOnMap");
                findObject = UnityEngine.Random.Range(0, objectsOnMap.Length);
                testObject = objectsOnMap[findObject];
                findText.text = "Find the landmark..." + testObject.name;
                foreach (Renderer r in testObject.GetComponentsInChildren<Renderer>())
                {
                    if (r.name == "LandmarkName") continue;
                    r.material.color = Color.magenta;
                }
                Secs = DateTime.Now;
            }

        }
        else
        {
            int secsDiff = DateTime.Now.Subtract(Secs).Seconds;
            if (secsDiff >= 30)
            {
                lostDing.Play();
                foreach (Renderer r in testObject.GetComponentsInChildren<Renderer>())
                {
                    r.material.color = Color.white;
                }
                testObject = null;
                findText.text = "";
                Secs = DateTime.Now;
            }
        }
    }
}
