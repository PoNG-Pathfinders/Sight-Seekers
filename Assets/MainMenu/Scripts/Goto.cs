using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Goto : MonoBehaviour, IPointerClickHandler
{
    public AudioSource ButtonSound;
    public ButtonType Type;

    public Sprite Sprite1;
    public Sprite Sprite2;
    public Button but;
    public Button but2;
// The enum says to reference the button by it's type, identified by a byte variable.
    public enum ButtonType : byte
    {
        EnterMainGame = 0,
        EnterSettings = (1 << 0)
    }
    //    public void ChangeImage()
    //    {
    // Some code here was adapted from the other code in SoundSettings
    //        if (but.image.sprite == voloff)
    //            but.image.sprite = volon;
    //        else
    //        {
    //            but.image.sprite = voloff;
    //        }
    //    }
    //    public AudioSource AudioSource;
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        //Use this to tell when the user left-clicks on the Button
        if (pointerEventData.button == PointerEventData.InputButton.Left)
// The Switch (Type) coding says that whenever it reads a "Type", check it's buttontype, and do whatever is under the particular case.
        switch (Type)
        {
            case ButtonType.EnterMainGame:
                Debug.Log("Now entering the Main Game!");
                    StartCoroutine(SceneChangeManager.Instance.changeScene(SceneChangeManager.Scenes.MainGame));
                break;
            case ButtonType.EnterSettings:
                Debug.Log("Now entering the Settings.");
                    StartCoroutine(SceneChangeManager.Instance.changeScene(SceneChangeManager.Scenes.Settings));
                break;
        }
    }
}