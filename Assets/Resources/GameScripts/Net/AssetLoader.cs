using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetLoader
{
    private static AssetLoader assetLoader;
    private AssetReader assetReader;
    private string[] deps;

    public static AssetLoader Instance
    {
        get
        {
            if (assetLoader == null)
                assetLoader = new AssetLoader();

            return assetLoader;
        }
    }

    private AssetLoader()
    {
        if (assetReader == null)
        {
#if UNITY_EDITOR
            assetReader = new ADAssetReader();
#else
            assetReader = new ABAssetReader();
#endif
        }
    }

    public void LoadAsset(string _assetBundleName, Action<AssetBox> _callback)
    {
#if UNITY_EDITOR
        _callback(assetReader.LoadSync(_assetBundleName));
#else
        deps = assetReader.GetDependences(_assetBundleName);
        if (deps.Length > 0)
        {
            bool loaded = false;
            bool canLoad;
            foreach (string dep in deps)
            {
                assetReader.LoadAsync(dep, (AssetBox _assetBox) =>
                {
                    if (loaded) return;
                    canLoad = true;
                    foreach (string dep2 in deps)
                    {
                        canLoad = assetReader.Has(dep2);
                        if (!canLoad) break;
                    }
                    if (!canLoad) return;
                    assetReader.LoadAsync(_assetBundleName, _callback);
                    loaded = true;
                });
            }
        }
        else
        {
            assetReader.LoadAsync(_assetBundleName, _callback);
        }
#endif
    }

    public void GC(RECYCLEPRI _recyclePri) {
        assetReader.GC(_recyclePri);
    }
}
