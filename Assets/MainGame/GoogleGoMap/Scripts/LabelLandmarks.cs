using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LabelLandmarks : MonoBehaviour
{
    public Text landmarkName;
    public Transform playerCamera;
    void Start()
    {
        Transform tr = this.transform;
        Transform par = tr.parent;
        GameObject game = par.gameObject;
        string str = game.name;
        GetComponent<TextMesh>().text = str;

        landmarkName.text = str;
    }

    // Update is called once per frame

    void Update()
    {
        landmarkName.text = this.transform.parent.gameObject.name;
        gameObject.transform.localEulerAngles = playerCamera.localEulerAngles;
        //gameObject.transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y + 180, 0);
    }
}