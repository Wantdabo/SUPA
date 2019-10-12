using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Threading;

public class AssetImportHelper : AssetPostprocessor
{
    private static void OnPostprocessAllAssets(string[] _importedAssets, string[] _deletedAssets, string[] _movedAssets, string[] _movedFromAssetPaths)
    {
        // New a Asset Auto Set AssetBundleName.
        foreach (string path in _importedAssets)
        {
            if (path.Contains(GlobalValues.STR_PREFAB_BUNDLES))
                EditorUtils.SetAssetBundleName(path);
        }

        // Asset Change Set AssetBundleName.
        foreach (string path in _movedAssets)
        {
            if (path.Contains(GlobalValues.STR_PREFAB_BUNDLES))
                EditorUtils.SetAssetBundleName(path);
            else
                EditorUtils.RemoveAssetBundleName(path);
        }
    }
}
