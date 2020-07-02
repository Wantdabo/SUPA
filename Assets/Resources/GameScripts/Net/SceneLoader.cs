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

    /// <summary>
    /// 根据场景名加载场景。
    /// </summary>
    /// <param name="_sceneName">场景名。</param>
    /// <param name="_assetBundleName">AssetBundleName.</param>
    /// <param name="_onUpdate">更新回调。</param>
    /// <param name="_onComplete">完成回调。</param>
    public void LoadScene(string _sceneName, string _assetBundleName, Action<float> _onUpdate, System.Action _onComplete)
    {
#if UNITY_EDITOR
        StartCoroutine(Load(_sceneName, _onUpdate, _onComplete));
#else
        AssetLoader.Instance.LoadAsset(_assetBundleName, (assetBox)=> {
            StartCoroutine(Load(_sceneName, _onUpdate, _onComplete));
        });
#endif
    }

    /// <summary>
    /// 加载时的协程。
    /// </summary>
    /// <param name="_sceneName">场景名。</param>
    /// <param name="_onUpdate">更新回调。</param>
    /// <param name="_onComplete">完成回调。</param>
    /// <returns></returns>
    private IEnumerator Load(string _sceneName, Action<float> _onUpdate, System.Action _onComplete)
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
