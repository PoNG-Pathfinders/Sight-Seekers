using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager> {

    [HideInInspector]
    public bool locationServicesIsRunning = false;

    public GameObject mainMap;
    public GameObject newMap;
    public GameObject[] outerMaps = new GameObject[8];

    public GameObject player;
    public GeoPoint playerGeoPosition;
    public PlayerLocationService player_loc;

    public Text mapLoc;

    public enum PlayerStatus { TiedToDevice, FreeFromDevice }

    private PlayerStatus _playerStatus;
    public PlayerStatus playerStatus
    {
        get { return _playerStatus; }
        set { _playerStatus = value; }
    }

    private GoogleStaticMap.MapRectangle.GetCorner NW = GoogleStaticMap.MapRectangle.GetCorner.NW;
    private GoogleStaticMap.MapRectangle.GetCorner NE = GoogleStaticMap.MapRectangle.GetCorner.NE;
    private GoogleStaticMap.MapRectangle.GetCorner SW = GoogleStaticMap.MapRectangle.GetCorner.SW;
    private GoogleStaticMap.MapRectangle.GetCorner SE = GoogleStaticMap.MapRectangle.GetCorner.SE;

    void Awake() {
        Time.timeScale = 1;
        playerStatus = PlayerStatus.TiedToDevice;
        player_loc = player.GetComponent<PlayerLocationService>();

        newMap.GetComponent<MeshRenderer>().enabled = false;
        newMap.SetActive(false);
    }

    public GoogleStaticMap getMainMap()
    {
        return mainMap.GetComponent<GoogleStaticMap>();
    }

    public GoogleStaticMap getNewMap()
    {
        return newMap.GetComponent<GoogleStaticMap>();
    }

    public GoogleStaticMap getOuterMap(GameObject obj)
    {
        return obj.GetComponent<GoogleStaticMap>();
    }

    IEnumerator Start() {

        getMainMap().initialize();
        foreach (GameObject map in outerMaps)
            getOuterMap(map).initialize();

        yield return StartCoroutine(player_loc._StartLocationService());
        StartCoroutine(player_loc.RunLocationService());

        locationServicesIsRunning = player_loc.locServiceIsRunning;
        Debug.Log("Player loc from GameManager: " + player_loc.loc);

        GoogleStaticMap mainMap = getMainMap();
        mainMap.centerMercator = mainMap.tileCenterMercator(player_loc.loc);
        mainMap.DrawMap();

        mainMap.transform.localScale = Vector3.Scale(
            new Vector3(mainMap.mapRectangle.getWidthMeters(), mainMap.mapRectangle.getHeightMeters(), 1),
            new Vector3(mainMap.realWorldtoUnityWorldScale.x, mainMap.realWorldtoUnityWorldScale.y, 1));

        getOuterMap(outerMaps[0]).centerMercator = newCenterMercator(mainMap, NW);
        getOuterMap(outerMaps[1]).centerMercator = newCenterMercator(mainMap, NW, NE);
        getOuterMap(outerMaps[2]).centerMercator = newCenterMercator(mainMap, NE);
        getOuterMap(outerMaps[3]).centerMercator = newCenterMercator(mainMap, NE, SE);
        getOuterMap(outerMaps[4]).centerMercator = newCenterMercator(mainMap, SE);
        getOuterMap(outerMaps[5]).centerMercator = newCenterMercator(mainMap, SW, SE);
        getOuterMap(outerMaps[6]).centerMercator = newCenterMercator(mainMap, SW);
        getOuterMap(outerMaps[7]).centerMercator = newCenterMercator(mainMap, NW, SW);

        foreach (GameObject map in outerMaps)
            getOuterMap(map).DrawMap();

        for (int i = 0; i < outerMaps.Length; i++)
        {
            GameObject map = outerMaps[i];
            GoogleStaticMap mapMap = getOuterMap(map);
            map.transform.localScale = Vector3.Scale(
                new Vector3(mapMap.mapRectangle.getWidthMeters(), mapMap.mapRectangle.getHeightMeters(), 1F),
                new Vector3(mapMap.realWorldtoUnityWorldScale.x, mapMap.realWorldtoUnityWorldScale.y, 1F));
            setNewPos(i);
        }

        player.GetComponent<ObjectPosition>().setPositionOnMap(player_loc.loc);

        GameObject[] objectsOnMap = GameObject.FindGameObjectsWithTag("ObjectOnMap");

        foreach (GameObject obj in objectsOnMap)
            obj.GetComponent<ObjectPosition>().setPositionOnMap();
    }

    void Update() {
        if (!locationServicesIsRunning) {
            mapLoc.text = "Location Service is not enabled.\nLatitude: " + player_loc.loc.lat_d + ", Longitude: " + player_loc.loc.lon_d;
            //TODO: Show location service is not enabled error. 
            return;
        }

        // playerGeoPosition = getMainMapMap ().getPositionOnMap(new Vector2(player.transform.position.x, player.transform.position.z));
        playerGeoPosition = new GeoPoint();
        // GeoPoint playerGeoPosition = getMainMapMap ().getPositionOnMap(new Vector2(player.transform.position.x, player.transform.position.z));
        if (playerStatus == PlayerStatus.TiedToDevice) {
            playerGeoPosition = player_loc.loc;
            player.GetComponent<ObjectPosition>().setPositionOnMap(playerGeoPosition);
        } else if (playerStatus == PlayerStatus.FreeFromDevice) {
            playerGeoPosition = getOuterMap(mainMap).getPositionOnMap(new Vector2(player.transform.position.x, player.transform.position.z));
        }

        GoogleStaticMap.MyPoint tileCenterMercator = getOuterMap(mainMap).tileCenterMercator(playerGeoPosition);
        mapLoc.text = tileCenterMercator.ToString() + "\nLatitude: " + player_loc.loc.lat_d + ", Longitude: " + player_loc.loc.lon_d;

        if (!getOuterMap(mainMap).centerMercator.isEqual(tileCenterMercator))
        {
            // Re-add original map tile system, have outer maps generate each time around the current main map

            newMap.SetActive(true);
            GoogleStaticMap newMapMap = getNewMap();
            newMapMap.initialize();
            newMapMap.centerMercator = tileCenterMercator;
            newMapMap.DrawMap();
            newMapMap.transform.localScale = Vector3.Scale(
                new Vector3(newMapMap.mapRectangle.getWidthMeters(), newMapMap.mapRectangle.getHeightMeters(), 1),
                new Vector3(newMapMap.realWorldtoUnityWorldScale.x, newMapMap.realWorldtoUnityWorldScale.y, 1));

            Vector2 tempPosition = getMainMap().getPositionOnMap(newMapMap.centerLatLon);
            newMap.transform.position = new Vector3(tempPosition.x, 0, tempPosition.y);

            GameObject temp = newMap;
            newMap = mainMap;
            mainMap = temp;

            foreach (GameObject map in outerMaps)
                getOuterMap(map).initialize();

            getOuterMap(outerMaps[0]).centerMercator = newCenterMercator(getOuterMap(mainMap), NW);
            getOuterMap(outerMaps[1]).centerMercator = newCenterMercator(getOuterMap(mainMap), NW, NE);
            getOuterMap(outerMaps[2]).centerMercator = newCenterMercator(getOuterMap(mainMap), NE);
            getOuterMap(outerMaps[3]).centerMercator = newCenterMercator(getOuterMap(mainMap), NE, SE);
            getOuterMap(outerMaps[4]).centerMercator = newCenterMercator(getOuterMap(mainMap), SE);
            getOuterMap(outerMaps[5]).centerMercator = newCenterMercator(getOuterMap(mainMap), SW, SE);
            getOuterMap(outerMaps[6]).centerMercator = newCenterMercator(getOuterMap(mainMap), SW);
            getOuterMap(outerMaps[7]).centerMercator = newCenterMercator(getOuterMap(mainMap), NW, SW);

            foreach (GameObject map in outerMaps)
                getOuterMap(map).DrawMap();

            for (int i = 0; i < outerMaps.Length; i++)
            {
                GameObject map = outerMaps[i];
                GoogleStaticMap mapMap = getOuterMap(map);
                map.transform.localScale = Vector3.Scale(
                    new Vector3(mapMap.mapRectangle.getWidthMeters(), mapMap.mapRectangle.getHeightMeters(), 1F),
                    new Vector3(mapMap.realWorldtoUnityWorldScale.x, mapMap.realWorldtoUnityWorldScale.y, 1F));
                setNewPos(i);
            }
        }
        if (getMainMap().isDrawn && !mainMap.GetComponent<MeshRenderer>().enabled)
        {
            mainMap.GetComponent<MeshRenderer>().enabled = true;
            newMap.GetComponent<MeshRenderer>().enabled = false;
            newMap.SetActive(false);
        }
    }

    public Vector3? ScreenPointToMapPosition(Vector2 point) {
        var ray = Camera.main.ScreenPointToRay(point);
        //RaycastHit hit;
        // create a plane at 0,0,0 whose normal points to +Y:
        Plane hPlane = new Plane(Vector3.up, Vector3.zero);
        float distance = 0;
        if (!hPlane.Raycast(ray, out distance)) {
            // get the hit point:
            return null;
        }
        Vector3 location = ray.GetPoint(distance);
        return location;
    }

    private GoogleStaticMap.MyPoint newCenterMercator(GoogleStaticMap map, GoogleStaticMap.MapRectangle.GetCorner corner)
    {
        Vector2 centerVec = map.getPositionOnMap(map.centerLatLon);
        Vector2 cornerVec = map.getPositionOnMap(map.mapRectangle.getCornerLatLon(corner));
        Vector2 newVec = cornerVec + (cornerVec - centerVec);
        return map.tileCenterMercator(map.getPositionOnMap(newVec));
    }

    private GoogleStaticMap.MyPoint newCenterMercator(GoogleStaticMap map, GoogleStaticMap.MapRectangle.GetCorner cornerOne, GoogleStaticMap.MapRectangle.GetCorner cornerTwo)
    {
        Vector2 centerVec = map.getPositionOnMap(map.centerLatLon);
        Vector2 cornerVecOne = map.getPositionOnMap(map.mapRectangle.getCornerLatLon(cornerOne));
        Vector2 cornerVecTwo = map.getPositionOnMap(map.mapRectangle.getCornerLatLon(cornerTwo));

        Vector2 newVec = cornerVecOne + cornerVecTwo - centerVec;
        return map.tileCenterMercator(map.getPositionOnMap(newVec));
    }

    private void setNewPos(int index)
    {
        GameObject map = outerMaps[index];
        GoogleStaticMap mapMap = map.GetComponent<GoogleStaticMap>();
        Vector2 newPos = getOuterMap(mainMap).getPositionOnMap(mapMap.centerLatLon);
        float yOff = 0.001f * (2 - Mathf.Abs(index - 4));
        map.transform.position = new Vector3(newPos.x, yOff, newPos.y);
    }
}
