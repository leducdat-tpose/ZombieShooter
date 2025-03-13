using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.SceneManagement;
public static class Loader
{
    public class DummyLoading: MonoBehaviour{}
    public enum SceneType
    {
        MainMenuScene,
        LoadingScene,
        GameScene
    }
    private static Action _onLoaderCallback;
    private static AsyncOperation _loadingAsyncOperation;
    public static void LoadScene(SceneType type)
    {
        SceneManager.LoadScene(SceneType.LoadingScene.ToString());
        _onLoaderCallback = () => {
            GameObject loadingObj = new GameObject("Loading object");
            loadingObj.AddComponent<DummyLoading>().StartCoroutine(LoadSceneAsync(type));
        };
    }

    public static IEnumerator LoadSceneAsync(SceneType type)
    {
        yield return null;
        _loadingAsyncOperation = SceneManager.LoadSceneAsync(type.ToString());
        if(!_loadingAsyncOperation.isDone)
        {
            yield return null;
        }
    }

    public static float GetLoadingProgress()
    {
        if(_loadingAsyncOperation != null)
        {
            Debug.Log(_loadingAsyncOperation.progress);
            return _loadingAsyncOperation.progress;
        }
        return 1;
    }

    public static void OnLoaderCallBack()
    {
        if(_onLoaderCallback == null) return;
        _onLoaderCallback();
        _onLoaderCallback = null;
    }
}
