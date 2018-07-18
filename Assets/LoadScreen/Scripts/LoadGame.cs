using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadGame : MonoBehaviour
{
    public Image image;

	public void Update()
    {
        image.fillAmount = SceneChangeManager.obj.GetComponent<SceneChangeManager>().progress;
    }
}
