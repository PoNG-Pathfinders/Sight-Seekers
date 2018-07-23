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
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Main Menu"));
    }

    public IEnumerator changeScene(Scenes scene)
    {
        String gamePath = SceneManager.GetSceneByBuildIndex((int)Scenes.MainGame).path;
        String scenePath = SceneUtility.GetScenePathByBuildIndex((int)scene);
        AsyncOperation op;

        if (!gamePath.Equals(scenePath))
        {
            op = SceneManager.LoadSceneAsync(scenePath, LoadSceneMode.Additive);
            while (!op.isDone)
                yield return null;
        }

        Queue<Scene> toUnload = new Queue<Scene>();
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene sc = SceneManager.GetSceneAt(i);
            if (sc.path.Equals(scenePath))
                SceneManager.SetActiveScene(sc);
            else if (!sc.path.Equals(gamePath))
                toUnload.Enqueue(sc);
        }

        while (toUnload.Count > 0)
        {
            Scene sc = toUnload.Dequeue();
            op = SceneManager.UnloadSceneAsync(sc);
            while (!op.isDone)
                yield return null;
        }
    }
}
