using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Compass_Button : MonoBehaviour, IPointerClickHandler
{
    public PlayerLocationService Service;
    public AudioSource Right_Answer;
    public AudioSource Wrong_Answer;
    public Sprite E_CompassColor;
    public Sprite N_CompassColor;
    public Sprite W_CompassColor;
    public Sprite S_CompassColor;
    [HideInInspector]
    public GameObject landmark;

    private Sprite eSprite;
    private Sprite nSprite;
    private Sprite wSprite;
    private Sprite sSprite;

    public byte Direction;

    private void Start()
    {
        switch (gameObject.name)
        {
            case "E-Button":
                eSprite = this.gameObject.GetComponent<Image>().sprite;
                break;
            case "N-Button":
                 nSprite = this.gameObject.GetComponent<Image>().sprite;
                break;
            case "W-Button":
                wSprite = this.gameObject.GetComponent<Image>().sprite;
                break;
            case "S-Button":
                sSprite = this.gameObject.GetComponent<Image>().sprite;
                break;
        }
        Debug.Log("SpriteState Reset");
    }
    void Update ()
    {
            landmark = GameObject.FindGameObjectWithTag("Player").GetComponent<PointTest>().testObject;
        if (landmark == null)
        {
            Direction = byte.MaxValue;

            switch (gameObject.name)
            {
                case "E-Button":
                    this.gameObject.GetComponent<Image>().sprite = eSprite;
                    break;
                case "N-Button":
                    this.gameObject.GetComponent<Image>().sprite = nSprite;
                    break;
                case "W-Button":
                    this.gameObject.GetComponent<Image>().sprite = wSprite;
                    break;
                case "S-Button":
                    this.gameObject.GetComponent<Image>().sprite = sSprite;
                    break;
            }
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

        Debug.Log("GeoPoint object Detected");

        switch (gameObject.name)
        {
            case "E-Button":
                {
                    this.gameObject.GetComponent<Image>().sprite = E_CompassColor;
                    Debug.Log("E-sprite changed");
                }
                break;
            case "N-Button":
                {
                    this.gameObject.GetComponent<Image>().sprite = N_CompassColor;
                    Debug.Log("N-sprite changed");
                }
                break;
            case "W-Button":
                {
                    this.gameObject.GetComponent<Image>().sprite = W_CompassColor;
                    Debug.Log("W-sprite changed");
                }
                break;
            case "S-Button":
                {
                    this.gameObject.GetComponent<Image>().sprite = S_CompassColor;
                    Debug.Log("S-sprite changed");
                }
                break;
        }

        float angle = Mathf.Atan2(objectPos.lat_d - playerPos.lat_d, objectPos.lon_d - playerPos.lon_d);
        if (angle < (-Mathf.PI / 2))
        {
            Direction = 2;
            Debug.Log("Direction=2");
        }
        else if (angle < 0)
        {
            Direction = 3;
            Debug.Log("Direction=3");
        }
        else if (angle < Mathf.PI / 2)
        {
            Direction = 0;
            Debug.Log("Direction=0");
        }
        else
        {
            Direction = 1;
            Debug.Log("Direction=1");
        }
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (pointerEventData.button == PointerEventData.InputButton.Left)
        {
            Debug.Log(name + " Button Pressed!");

            switch (Direction)
            {
                case 0:
                    if (gameObject.name.Equals("E-Button") || gameObject.name.Equals("N-Button"))
                    {
                        Right_Answer.Play();
                        Debug.Log("You have selected the right answer!");
                    }
                    else
                    {
                        Wrong_Answer.Play();
                        Debug.Log("You have selected the wrong answer!");
                    }
                    break;
                case 1:
                    if (gameObject.name.Equals("N-Button") || gameObject.name.Equals("W-Button"))
                    {
                        Right_Answer.Play();
                        Debug.Log("You have selected the right answer!");
                    }
                    else
                    {
                        Wrong_Answer.Play();
                        Debug.Log("You have selected the wrong answer!");
                    }
                    break;
                case 2:
                    if (gameObject.name.Equals("W-Button") || gameObject.name.Equals("S-Button"))
                    {
                        Right_Answer.Play();
                        Debug.Log("You have selected the right answer!");
                    }
                    else
                    {
                        Wrong_Answer.Play();
                        Debug.Log("You have selected the wrong answer!");
                    }
                    break;
                case 3:
                    if (gameObject.name.Equals("S-Button") || gameObject.name.Equals("E-Button"))
                    {
                        Right_Answer.Play();
                        Debug.Log("You have selected the right answer!");
                    }
                    else
                    {
                        Wrong_Answer.Play();
                        Debug.Log("You have selected the wrong answer!");
                    }
                    break;
                default:
                    Debug.Log("No Direction");
                    break;
            }
         }
    }
}

