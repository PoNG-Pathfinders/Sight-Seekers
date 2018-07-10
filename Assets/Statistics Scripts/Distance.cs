using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Distance : MonoBehaviour {

    public GeoPoint loc = new GeoPoint();
    [HideInInspector]
    public float trueHeading;
    public bool locServiceIsRunning = false;
    public int maxWait = 30; // seconds
    private float locationUpdateInterval = 0.2f; // seconds
    private double lastLocUpdate = 0.0; //seconds
    public bool hasMobile = true;

    public Text distText;

    [HideInInspector]
    public const float EARTH_RADIUS = 3959;

    public float distTraveled=0;

    public float prevLat;
    public float prevLon;
    // Use this for initialization
    void Start () {
        prevLat = loc.lat_r;
        prevLon = loc.lon_r;
        distTraveled = 0;
    }
	
	// Update is called once per frame
	void Update () {
        float lat = loc.lat_r;
        float lon = loc.lon_r;

        float diffLat = lat - prevLat;
        float diffLon = lon - prevLon;

        Debug.Log(loc.lat_r);

        float a = Mathf.Sin(diffLat / 2) * Mathf.Sin(diffLat / 2)
            + Mathf.Cos(lat) * Mathf.Cos(prevLat)
            * Mathf.Sin(diffLon / 2) * Mathf.Sin(diffLon / 2);
        float c = 2 * Mathf.Atan2(Mathf.Sqrt(a), Mathf.Sqrt(1 - a));

        float dist = EARTH_RADIUS * c;
        //if (dist != 0)
            Debug.Log("Distance Traveled: " + distTraveled);
        distText.text = "Distance Traveled: " + distTraveled;

        Debug.Log("Geoposition = " + GetComponent<GameManager>().newMap.transform.position);
        distTraveled += dist;

        prevLat = lat;
        prevLon = lon;
    }
}