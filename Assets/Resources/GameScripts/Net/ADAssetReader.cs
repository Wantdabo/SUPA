using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif
using Object = UnityEngine.Object;

public class ADAssetReader : AssetReader
{
    public override AssetBox LoadSync(string _assetBundleName)
    {
#if UNITY_EDITOR
        if (Has(_assetBundleName))
            return Get(_assetBundleName);

        path = Utils.AssetBundleNameToPath(_assetBundleName);
        obj = AssetDatabase.LoadAssetAtPath<Object>(path);
        assetBox.name = obj.name;
        assetBox.assetBundleName = _assetBundleName;
        assetBox.path = path;
        assetBox.obj = obj;
        Add(assetBox);
#endif
        return assetBox;
    }

    public override void LoadAsync(string _assetBundleName, Action<AssetBox> _callback)
    {
        throw new System.NotImplementedException();
    }
}