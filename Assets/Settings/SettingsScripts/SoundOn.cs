using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundOn : MonoBehaviour {
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
