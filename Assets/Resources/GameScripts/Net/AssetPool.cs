using System.Collections.Generic;

public class AssetPool
{
    private static AssetPool assetPool;
    private AssetPoolBox assetPoolBox;
    private string[] keys;
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
    /// 检查池子里面是否存在。
    /// </summary>
    /// <param name="_assetBundleName">AssetBundleName.</param>
    /// <returns>布尔值，是否存在。</returns>
    public bool Has(string _assetBundleName)
    {
        return assetBoxPools.ContainsKey(_assetBundleName);
    }

    /// <summary>
    /// 根据 AssetBundleName 获取 AssetBox.
    /// </summary>
    /// <param name="_assetBundleName">AssetBundleName.</param>
    /// <returns>AssetBox.</returns>
    public AssetBox Get(string _assetBundleName)
    {
        return assetBoxPools[_assetBundleName].assetBox;
    }

    /// <summary>
    /// 将 AssetBox 添加至池子。
    /// </summary>
    /// <param name="_assetBox">AssetBox.</param>
    /// <param name="_recyclePri">GC 权限，默认过场景清。</param>
    public void Add(AssetBox _assetBox, RECYCLEPRI _recyclePri = RECYCLEPRI.SCENE_GC)
    {
        assetPoolBox.assetBundlename = _assetBox.assetBundleName;
        assetPoolBox.assetBox = _assetBox;
        assetPoolBox.recyclePri = _recyclePri;
        assetBoxPools.Add(assetPoolBox.assetBundlename, assetPoolBox);
    }

    /// <summary>
    /// 根据 AssetBundleName 从池子中移除该 AssetBox.
    /// </summary>
    /// <param name="_assetBundleName">AssetBundleName.</param>
    /// <returns>Success true, Fail False.</returns>
    public bool Remove(string _assetBundleName)
    {
        return assetBoxPools.Remove(_assetBundleName);
    }

    /// <summary>
    /// GC 方法。
    /// </summary>
    /// <param name="_recyclePri">GC 权限。</param>
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
    /// 根据 AssetBundleName 卸载资源。
    /// </summary>
    /// <param name="_key">AssetBundleName.</param>
    /// <param name="_force">实例出来的 GameObject 都会受到影响。</param>
    public void UnLoadAssetBundle(string _key, bool _force = false)
    {
#if !UNITY_EDITOR
        assetBoxPools[_key].assetBox.assetBundle.Unload(_force);
#endif
    }
}
