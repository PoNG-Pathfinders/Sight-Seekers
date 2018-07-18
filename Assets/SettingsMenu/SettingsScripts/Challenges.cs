﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Challenges : MonoBehaviour, IPointerClickHandler
{
    public Sprite Sprite1;
    public Sprite Sprite2;
    public Button but;
    public void ChangeImage()
    {
        if (but.image.sprite == Sprite1)
            but.image.sprite = Sprite2;
        else
        {
            but.image.sprite = Sprite1;
        }
    }
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (pointerEventData.button == PointerEventData.InputButton.Left)
        {
            Debug.Log("This has not been implemented yet.");
//            SceneManager.LoadSceneAsync(0);
        }
    }
}