using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SoundOn : MonoBehaviour {
    public Sprite voloff;
    public Sprite volon;
    public Button but;
    public void ChangeImage()
    {
        if (but.image.sprite == voloff)
            but.image.sprite = volon;
        else
        {
            but.image.sprite = voloff;
        }
    }
    public AudioSource AudioSource;

    // Use this for initialization
    void Start () {
                  }
	
	// Update is called once per frame
        void Update()
        {
 //           if (GUI.Button(rect(55, 200, 180, 40), "Audio Mute"))
            {
                AudioListener.volume = + - AudioListener.volume;
            }
        }
}
