using System.Collections;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

public class EditorUtils
{
    private static AssetImporter assetImporter;
    private static string path = "";
    private static string extends = "";

    /// <summary>
    /// Compare String.
    /// </summary>
    /// <param name="_a">Left Value</param>
    /// <param name="_b">Right Value</param>
    /// <param name="ignore">Ignore case</param>
    /// <returns>Compare Result</returns>
    public static bool CompareStr(string _a, string _b, bool _ignore = false)
    {
        return Utils.CompareStr(_a, _b);
    }

    /// <summary>
    /// Copy File To Target Path.
    /// </summary>
    /// <param name="_sourcePath">Source Path.</param>
    /// <param name="_targetPath">Target Path.</param>
    /// <param name="overwrite">Overwrite. Default False.</param>
    public static void CopyFile(string _sourcePath, string _targetPath, bool overwrite = false)
    {
        FileInfo fileInfo = new FileInfo(_sourcePath);
        fileInfo.CopyTo(_targetPath, overwrite);
    }

    /// <summary>
    /// Copy Files.
    /// </summary>
    /// <param name="_sourceFloor">Sources Floor.</param>
    /// <param name="_targetFloor">Target Floor.</param>
    /// <param name="_title">Title.</param>
    /// <param name="overwrite">Overwrite.</param>
    public static void CopyFiles(string _sourceFloor, string _targetFloor, string _title, bool overwrite = false)
    {
        DirectoryInfo di = new DirectoryInfo(_sourceFloor);
        FileInfo[] fileInfos = di.GetFiles("*.*");
        for (int i = 0; i < fileInfos.Length; i++)
        {
            DisplayProgressBar(_title, fileInfos[i].Name, i + 1, fileInfos.Length);
            fileInfos[i].CopyTo(_targetFloor + fileInfos[i].Name, overwrite);
        }

        ClearProgressBar();
    }

    /// <summary>
    /// Delete File.
    /// </summary>
    /// <param name="_sourcePath">Source Path.</param>
    public static void DelFile(string _sourcePath)
    {
        FileInfo fileInfo = new FileInfo(_sourcePath);
        fileInfo.Delete();
    }

    /// <summary>
    /// Delete Files.
    /// </summary>
    /// <param name="_targetFloor">Target Floor.</param>
    public static void DelFiles(string _targetFloor)
    {
        DirectoryInfo di = new DirectoryInfo(_targetFloor);
        FileInfo[] fileInfos = di.GetFiles("*.*");
        foreach (FileInfo fileInfo in fileInfos)
            fileInfo.Delete();
    }

    /// <summary>
    /// Delete Files By Extends.
    /// </summary>
    /// <param name="_sourceFloor">Source Floor.</param>
    /// <param name="_extends">Extends.</param>
    public static void DelFilesByExtends(string _sourceFloor, string _extends)
    {
        DirectoryInfo di = new DirectoryInfo(_sourceFloor);
        FileInfo[] fileInfos = di.GetFiles("*" + _extends + "*");
        foreach (FileInfo fileInfo in fileInfos)
            fileInfo.Delete();
    }

    /// <summary>
    /// Set AssetBundleName By Object.
    /// </summary>
    /// <param name="_obj">Asset ObjectType.</param>
    public static void SetAssetBundleName(Object _obj)
    {
        path = AssetDatabase.GetAssetPath(_obj);
        assetImporter = AssetImporter.GetAtPath(path);
        assetImporter.assetBundleName = PathToAssetBundleName(path);
    }

    /// <summary>
    /// Set AssetBundle Name By Path.
    /// </summary>
    /// <param name="_path">Asset Path.</param>
    public static void SetAssetBundleName(string _path)
    {
        assetImporter = AssetImporter.GetAtPath(_path);
        assetImporter.assetBundleName = PathToAssetBundleName(_path);
    }

    /// <summary>
    /// Remove AssetBundleName By Path.
    /// </summary>
    /// <param name="_path">Asset Path.</param>
    public static void RemoveAssetBundleName(string _path)
    {
        assetImporter = AssetImporter.GetAtPath(_path);
        assetImporter.assetBundleName = "";
    }

    /// <summary>
    /// Remove AssetBundleName By AssetImporter. 
    /// </summary>
    /// <param name="_assetImporter">AssetImporter.</param>
    public static void RemoveAssetBundleName(AssetImporter _assetImporter)
    {
        _assetImporter.assetBundleName = "";
    }

    /// <summary>
    /// Recycle Unused AssetBundleName.
    /// </summary>
    public static void RecycleUnusedAssetBundleName()
    {
        AssetDatabase.RemoveUnusedAssetBundleNames();
    }

    /// <summary>
    /// Display ProgressBar.
    /// </summary>
    /// <param name="_title">Set Your Title.</param>
    /// <param name="_info">Set Your Infomation.</param>
    /// <param name="_cur">Set Current Value.</param>
    /// <param name="_max">Set Max Value.</param>
    public static void DisplayProgressBar(string _title, string _info, int _cur = 0, int _max = 0)
    {
        EditorUtility.DisplayProgressBar(_title, string.Format("{0} / {1}\t", _cur, _max) + _info, 1f);
    }

    /// <summary>
    /// Clear ProgressBar.
    /// </summary>
    public static void ClearProgressBar()
    {
        EditorUtility.ClearProgressBar();
    }

    /// <summary>
    /// Copy AssetBundleName to Clip Board.
    /// </summary>
    [MenuItem("Assets/Utils/Copy AssetBundleName")]
    public static void CopyAssetBundleName()
    {
        Object[] objs = Selection.GetFiltered<Object>(SelectionMode.Assets);
        if (objs.Length > 1)
            return;
        string path = AssetDatabase.GetAssetPath(objs[0]);

        TextEditor te = new TextEditor();
        te.text = EditorUtils.PathToAssetBundleName(path);
        te.SelectAll();
        te.Copy();
    }

    public static string PathToAssetBundleName(string _path)
    {
        return Utils.PathToAssetBundleName(_path);
    }

    public static string AssetBundleNameToAssetName(string _assetBundleName)
    {
        return Utils.AssetBundleNameToAssetName(_assetBundleName);
    }

    public static string AssetBundleNameToPath(string _assetBundleName)
    {
        return Utils.AssetBundleNameToPath(_assetBundleName);
    }
}
