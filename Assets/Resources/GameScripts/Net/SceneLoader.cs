using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private static SceneLoader sceneLoader;
    private AsyncOperation asyncOperation;

    public static SceneLoader Instance
    {
        get
        {
            if (sceneLoader == null)
                sceneLoader = Camera.main.GetComponent<SceneLoader>();

            return sceneLoader;
        }
    }

    public void LoadScene(string _sceneName, string _assetBundleName, EVENT_DEL_VOID_FLOAT _onUpdate, Action _onComplete)
    {
#if UNITY_EDITOR
        StartCoroutine(Load(_sceneName, _onUpdate, _onComplete));
#else
        AssetLoader.Instance.LoadAsset(_assetBundleName, (assetBox)=> {
            StartCoroutine(Load(_sceneName, _onUpdate, _onComplete));
        });
#endif
    }

    private IEnumerator Load(string _sceneName, EVENT_DEL_VOID_FLOAT _onUpdate, Action _onComplete)
    {
        asyncOperation = SceneManager.LoadSceneAsync(_sceneName);
        while (!asyncOperation.isDone)
        {
            _onUpdate(asyncOperation.progress);

            yield return null;
        }
        _onUpdate(1f);
        _onComplete();
        AssetLoader.Instance.GC(RECYCLEPRI.SCENE_GC);
    }
}
