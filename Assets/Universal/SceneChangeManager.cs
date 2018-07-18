using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeManager : Singleton<SceneChangeManager> {

    public enum Scenes
    {
        LoadScreen,
        Settings,
        Statistics,
        MainGame,
        MainMenu
    }

    public static Array SceneList = Enum.GetValues(typeof(Scenes));

    public static GameObject obj;

    public bool finishedInit;
    public int numScenes;
    public float progress;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        obj = gameObject;
        finishedInit = false;
        progress = 0;
        numScenes = SceneList.Length;
        StartCoroutine(loadSceneSequence());
    }

    IEnumerator loadSceneSequence()
    {
        yield return new WaitForSeconds(1);
        foreach (Scenes scene in SceneList)
        {
            AsyncOperation op = SceneManager.LoadSceneAsync((int)scene, LoadSceneMode.Additive);
            while (!op.isDone)
            {
                progress = (op.progress + ((int)scene - 0.9f)) / (numScenes - 1);
                yield return null;
            }
            yield return new WaitForSeconds(1);
            if (scene < Scenes.MainGame)
            {
                op = SceneManager.UnloadSceneAsync((int)scene);
                while (!op.isDone)
                    yield return null;
            }
        }
        finishedInit = true;
        SceneManager.UnloadSceneAsync((int)Scenes.LoadScreen);
    }

    IEnumerator changeScene(Scenes scene)
    {


        yield return null;
    }
}
