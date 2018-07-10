using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SoundSetting : MonoBehaviour, IPointerClickHandler
{
    public AudioSource SoundOnOff;

    public bool muted;

    private float volume;

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        //Use this to tell when the user left-clicks on the Button
        if (pointerEventData.button == PointerEventData.InputButton.Left)

            if (muted == false) {volume = AudioListener.volume; AudioListener.volume = 0; muted = true; Debug.Log("Sound is Off"); }
            else { AudioListener.volume = volume; muted = false; Debug.Log("Sound is " +volume); SoundOnOff.Play(); }

    }
}