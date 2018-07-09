using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadGame : MonoBehaviour
{
    public Image image;

    public float progress;

	public void Start ()
    {
        image.fillAmount = 0;
        StartCoroutine(loadScene());
    }

    private IEnumerator loadScene()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(1);
        op.allowSceneActivation = false;

        while (!op.isDone)
        {
            progress = op.progress;

            image.fillAmount = op.progress / 0.9f;

            if (op.progress >= 0.9f)
                if (Input.touchCount > 0)
                    op.allowSceneActivation = true;

            yield return null;
        }
    }
}
