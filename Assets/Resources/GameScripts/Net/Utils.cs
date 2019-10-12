using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Utils
{
    private static string[] splitStrs;
    private static string extends = "";

    /// <summary>
    /// Compare String.
    /// </summary>
    /// <param name="_a">Left Value.</param>
    /// <param name="_b">Right Value.</param>
    /// <param name="ignore">Ignore Case.</param>
    /// <returns>Compare Result</returns>
    public static bool CompareStr(string _a, string _b, bool _ignore = false)
    {
        if (_ignore)
            return _a.ToLower().Equals(_b.ToLower());
        else
            return _a.Equals(_b);
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
    /// Copy Files to Target Floor.
    /// </summary>
    /// <param name="_sourceFloor">Source Floor.</param>
    /// <param name="_targetFloor">Target Floor.</param>
    /// <param name="overwrite">Overwrite. Default False.</param>
    public static void CopyFiles(string _sourceFloor, string _targetFloor, bool overwrite = false)
    {
        DirectoryInfo di = new DirectoryInfo(_sourceFloor);
        FileInfo[] fileInfos = di.GetFiles("*.*");
        foreach (FileInfo fileInfo in fileInfos)
            fileInfo.CopyTo(_targetFloor + fileInfo.Name, overwrite);
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
    /// Delete Files By Name.
    /// </summary>
    /// <param name="_sourceFloor">Source Floor.</param>
    /// <param name="_fileName">File Name.</param>
    public static void DelFilesByName(string _sourceFloor, string _fileName)
    {
        DirectoryInfo di = new DirectoryInfo(_sourceFloor);
        FileInfo[] fileInfos = di.GetFiles("*.*");
        foreach (FileInfo fileInfo in fileInfos)
        {
            if (fileInfo.Name.Contains(_fileName))
                fileInfo.Delete();
        }
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
        {
            fileInfo.Delete();
        }
    }

    /// <summary>
    /// Get AssetBundleName By Path.
    /// </summary>
    /// <param name="_path">Path.</param>
    /// <returns>AssetBundleName.</returns>
    public static string PathToAssetBundleName(string _path)
    {
        extends = Path.GetExtension(_path);

        return _path.Replace(GlobalValues.PATH_PREFAB_BUNDLE, "").Replace("/", "_").Replace(".", "@").ToLower();
    }

    /// <summary>
    /// AssetBundleName To AssetName.
    /// </summary>
    /// <param name="_assetBundleName">AssetBundleName.</param>
    /// <returns>AssetName.</returns>
    public static string AssetBundleNameToAssetName(string _assetBundleName)
    {
        splitStrs = _assetBundleName.Split('_');

        return splitStrs[splitStrs.Length - 1].Split('@')[0];
    }

    /// <summary>
    /// AssetBundleName To Path.
    /// </summary>
    /// <param name="_assetBundleName">AssetBundleName.</param>
    /// <returns>Path.</returns>
    public static string AssetBundleNameToPath(string _assetBundleName)
    {
        return GlobalValues.PATH_PREFAB_BUNDLE + _assetBundleName.Replace("_", "/").Replace("@", ".");
    }

    /// <summary>
    /// TweenValue.
    /// </summary>
    /// <param name="_callback">Tween Update Callback.</param>
    /// <param name="_startValue">Tween Start Value.</param>
    /// <param name="_endValue">Tween End Value.</param>
    /// <param name="_duration">Tween Duration.</param>
    /// <param name="_onComplete">OnComplete Callback.</param>
    public static void TweenValue(EVENT_DEL_VOID_FLOAT _callback, float _startValue, float _endValue, float _duration, TweenCallback _onComplete = null)
    {
        Tweener tw = DOTween.To((value) => { _callback(value);}, _startValue, _endValue, _duration);
        if (_onComplete != null)
            tw.onComplete = _onComplete;
        tw.SetAutoKill(true);
        tw.PlayForward();
    }
}
