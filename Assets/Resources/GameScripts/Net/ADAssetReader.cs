using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ADAssetReader : AssetReader
{
    public override AssetBox LoadSync(string _assetBundleName)
    {
#if UNITY_EDITOR
        if (Has(_assetBundleName))
            return Get(_assetBundleName);

        path = Utils.AssetBundleNameToPath(_assetBundleName);
        obj = AssetDatabase.LoadAssetAtPath(path, typeof(Object));
        assetBox.name = obj.name;
        assetBox.assetBundleName = _assetBundleName;
        assetBox.path = path;
        assetBox.obj = obj;
        Add(assetBox);
#endif
        return assetBox;
    }

    public override void LoadAsync(string _assetBundleName, EVENT_DEL_VOID_ASSETBOX _callback)
    {
        throw new System.NotImplementedException();
    }
}