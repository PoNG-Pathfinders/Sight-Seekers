using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Compass_Button : MonoBehaviour {
    public PlayerLocationService Service;
    public AudioSource Right_Answer;
    public AudioSource Wrong_Answer;

    public byte Direction;

	void Update () {
        GameObject landmark = GameObject.FindGameObjectWithTag("Player").GetComponent<PointTest>().testObject;
        if (landmark == null)
        {
            Direction = byte.MaxValue;
            return;
        }

        GeoPoint playerPos;
        if (GameManager.Instance.playerStatus == GameManager.PlayerStatus.FreeFromDevice)
        {
            playerPos = GameManager.Instance.getMainMap().getPositionOnMap(new Vector2(Service.transform.position.x, Service.transform.position.z));
        }
        else
        {
            playerPos = Service.loc;
        }

        ObjectPosition obj = landmark.GetComponent<ObjectPosition>();
        GeoPoint objectPos = new GeoPoint(obj.lat_d, obj.lon_d);

        float angle = Mathf.Atan2(objectPos.lat_d - playerPos.lat_d, objectPos.lon_d - playerPos.lon_d);
        if (angle < (-Mathf.PI / 2))
            Direction = 2;
        else if (angle < 0)
            Direction = 3;
        else if (angle < Mathf.PI / 2)
            Direction = 0;
        else
            Direction = 1;
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (pointerEventData.button == PointerEventData.InputButton.Left)
        {
            switch (Direction)
            {
                case 0:
                    if (gameObject.name.Equals("E-Button") || gameObject.name.Equals("N-Button"))
                        Right_Answer.Play();
                    else
                        Wrong_Answer.Play();
                    break;
                case 1:
                    if (gameObject.name.Equals("N-Button") || gameObject.name.Equals("W-Button"))
                        Right_Answer.Play();
                    else
                        Wrong_Answer.Play();
                    break;
                case 2:
                    if (gameObject.name.Equals("W-Button") || gameObject.name.Equals("S-Button"))
                        Right_Answer.Play();
                    else
                        Wrong_Answer.Play();
                    break;
                case 3:
                    if (gameObject.name.Equals("S-Button") || gameObject.name.Equals("E-Button"))
                        Right_Answer.Play();
                    else
                        Wrong_Answer.Play();
                    break;
                default:
                    break;
            }
        }
    }
}

