using System.Collections.Generic;

public class AssetPool
{
    private static AssetPool assetPool;
    private AssetPoolBox assetPoolBox;
    private string[] keys;
    // Object Pool.
    private Dictionary<string, AssetPoolBox> assetBoxPools;

    public static AssetPool Instance
    {
        get
        {
            if (assetPool == null)
                assetPool = new AssetPool();

            return assetPool;
        }
    }

    private AssetPool()
    {
        assetBoxPools = new Dictionary<string, AssetPoolBox>();
    }

    /// <summary>
    /// Check If The Object Pool Has.
    /// </summary>
    /// <param name="_assetBundleName">AssetBundleName</param>
    /// <returns>Has True, Empty False.</returns>
    public bool Has(string _assetBundleName)
    {
        return assetBoxPools.ContainsKey(_assetBundleName);
    }

    /// <summary>
    /// Get AssetBox By AssetBundleName.
    /// </summary>
    /// <param name="_assetBundleName">AssetBundleName.</param>
    /// <returns>AssetBox</returns>
    public AssetBox Get(string _assetBundleName)
    {
        return assetBoxPools[_assetBundleName].assetBox;
    }

    /// <summary>
    /// Add AssetBox to Object Pool.
    /// </summary>
    /// <param name="_assetBox">AssetBox.</param>
    /// <param name="_recyclePri">GC PRI, Default SCENE_GC</param>
    public void Add(AssetBox _assetBox, RECYCLEPRI _recyclePri = RECYCLEPRI.SCENE_GC)
    {
        assetPoolBox.assetBundlename = _assetBox.assetBundleName;
        assetPoolBox.assetBox = _assetBox;
        assetPoolBox.recyclePri = _recyclePri;
        assetBoxPools.Add(assetPoolBox.assetBundlename, assetPoolBox);
    }

    /// <summary>
    /// Remove AssetBox Form Object Pool By AssetBundleName.
    /// </summary>
    /// <param name="_assetBundleName">AssetBundleName.</param>
    /// <returns>Success true, Fail False.</returns>
    public bool Remove(string _assetBundleName)
    {
        return assetBoxPools.Remove(_assetBundleName);
    }

    /// <summary>
    /// GC Object Pool By GC PRI.
    /// </summary>
    /// <param name="_recyclePri">GC PRI.</param>
    public void GC(RECYCLEPRI _recyclePri)
    {
        keys = new string[assetBoxPools.Keys.Count];
        assetBoxPools.Keys.CopyTo(keys, 0);
        foreach (string key in keys)
        {
            if (assetBoxPools[key].recyclePri == _recyclePri)
            {
                UnLoadAssetBundle(key, false);
                assetBoxPools.Remove(key);
            }
        }
        System.GC.Collect();
    }

    /// <summary>
    /// 卸载 AssetBundle.
    /// </summary>
    /// <param name="_key">AssetBundleName.</param>
    /// <param name="_force">卸载所有，如果为 true，实例出来的 GameObject 会受到影响，默认为 false.</param>
    public void UnLoadAssetBundle(string _key, bool _force = false)
    {
#if !UNITY_EDITOR
        assetBoxPools[_key].assetBox.assetBundle.Unload(_force);
#endif
    }
}
