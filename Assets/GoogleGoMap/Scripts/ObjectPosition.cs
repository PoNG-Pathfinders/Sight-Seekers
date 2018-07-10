using UnityEngine;
using System.Collections;

public class ObjectPosition : MonoBehaviour {

	public float lat_d = 0.0f, lon_d = 0.0f;

	private GeoPoint pos;

	void Start (){
		pos = new GeoPoint ();
		pos.setLatLon_deg (lat_d, lon_d);
        setPositionOnMap();
	}

	public void setPositionOnMap () {
        Vector2 tempPosition = GameManager.Instance.getMainMapMap().getPositionOnMap (this.pos);
		transform.position = new Vector3 (tempPosition.x, transform.position.y, tempPosition.y);
	}

	public void setPositionOnMap (GeoPoint pos) {
		this.pos = pos;
		setPositionOnMap ();
	}

    public void Update()
    {
        if (this.gameObject.CompareTag("ObjectOnMap"))
        {
            bool active = isOnMap();
            for (int i = 0; i < this.transform.childCount; i++)
            {
                this.transform.GetChild(i).gameObject.SetActive(active);
                this.GetComponent<BoxCollider>().enabled = active;
                this.GetComponent<MeshRenderer>().enabled = active;
            }
        }
    }

    private bool isOnMap()
    {
        GoogleStaticMap mainMap = GameManager.Instance.getMainMapMap();
        return mainMap.centerMercator.Equals(mainMap.tileCenterMercator(pos));
    }
}
