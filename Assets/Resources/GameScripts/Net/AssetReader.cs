using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public abstract class AssetReader
{
    private AssetPool assetPool = AssetPool.Instance;
    private AssetBundle maniAssetBundle;
    private AssetBundleManifest manifest;

    public string path;
    public AssetBundle assetBundle;
    public Object obj;
    public AssetBox assetBox;

    /// <summary>
    /// 异步加载，子类实现。
    /// </summary>
    /// <param name="_assetBundleName">AssetBundleName.</param>
    /// <param name="_callback">加载完成后的回调。</param>
    public abstract void LoadAsync(string _assetBundleName, Action<AssetBox> _callback);

    /// <summary>
    /// 同步加载，子类实现。
    /// </summary>
    /// <param name="_assetBundleName">AssetBundleName.</param>
    /// <returns>AssetBox.</returns>
    public abstract AssetBox LoadSync(string _assetBundleName);

    /// <summary>
    /// 获取包含 Manifest 的 AssetBundle.
    /// </summary>
    /// <returns>AssetBundle.</returns>
    public AssetBundle GetManifestAssetBundle()
    {
        if (maniAssetBundle == null)
            maniAssetBundle = AssetBundle.LoadFromFile(GlobalValues.GET_MANI_AB_LOAD_PATH);

        return maniAssetBundle;
    }

    /// <summary>
    /// 获取 AssetBundleManifest.
    /// </summary>
    /// <returns>AssetBundleManifest.</returns>
    public AssetBundleManifest GetManifest()
    {

        assetBundle = GetManifestAssetBundle();
        if (manifest == null)
            manifest = (AssetBundleManifest)assetBundle.LoadAsset(GlobalValues.STR_AB_MANI);

        return manifest;
    }

    /// <summary>
    /// 根据 AssetBundleName 获取资源的依赖关系
    /// </summary>
    /// <param name="_assetBundleName">AssetBundleName.</param>
    /// <returns>依赖关系。</returns>
    public string[] GetDependences(string _assetBundleName)
    {
        return GetManifest().GetAllDependencies(_assetBundleName);
    }

    public bool Has(string _assetBundleName)
    {
        return assetPool.Has(_assetBundleName);
    }

    public void Add(AssetBox _assetBox)
    {
        assetPool.Add(_assetBox);
    }

    public AssetBox Get(string _assetBundleName)
    {
        return assetPool.Get(_assetBundleName);
    }

    public void GC(RECYCLEPRI _recyclePri)
    {
        assetPool.GC(_recyclePri);
    }
}
