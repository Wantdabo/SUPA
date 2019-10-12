using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using UnityEditor;
using UnityEngine;

public class Package
{
    private static string path = "";
    private static string extends = "";
    private static Object obj;

    [MenuItem("Package/Build")]
    public static void Build()
    {
        if (Directory.Exists(GlobalValues.PATH_EXPORT_AB))
            EditorUtils.DelFiles(GlobalValues.PATH_EXPORT_AB);
        else
            Directory.CreateDirectory(GlobalValues.PATH_EXPORT_AB);

        BuildPipeline.BuildAssetBundles(GlobalValues.PATH_EXPORT_AB, BuildAssetBundleOptions.None, (BuildTarget)GlobalValues.TYPE_PLAT);
        EditorUtils.RecycleUnusedAssetBundleName();
        EditorUtils.DelFilesByExtends(GlobalValues.PATH_EXPORT_AB, GlobalValues.EXT_MANIFEST);
        AssetDatabase.Refresh();
    }

    [MenuItem("Package/Export")]
    public static void Export()
    {
        if (Directory.Exists(GlobalValues.PATH_AB_LOAD))
            EditorUtils.DelFiles(GlobalValues.PATH_AB_LOAD);
        else
            Directory.CreateDirectory(GlobalValues.PATH_AB_LOAD);

        EditorUtils.CopyFiles(GlobalValues.PATH_EXPORT_AB, GlobalValues.PATH_AB_LOAD, "Export Files", true);
        AssetDatabase.Refresh();
    }
}
