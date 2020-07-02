using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Utils
{
    private static string[] splitStrs;
    private static string extends = "";

    /// <summary>
    /// 字符串对比。
    /// </summary>
    /// <param name="_a">字符串 A.</param>
    /// <param name="_b">字符串 B.</param>
    /// <param name="ignore">是否忽略大小写。</param>
    /// <returns>布尔值结果</returns>
    public static bool CompareStr(string _a, string _b, bool _ignore = false)
    {
        if (_ignore)
            return _a.ToLower().Equals(_b.ToLower());
        else
            return _a.Equals(_b);
    }

    /// <summary>
    /// 拷贝文件至目标地址。
    /// </summary>
    /// <param name="_sourcePath">源文件地址。</param>
    /// <param name="_targetPath">目标文件地址。</param>
    /// <param name="overwrite">如果存在是否重写。</param>
    public static void CopyFile(string _sourcePath, string _targetPath, bool overwrite = false)
    {
        FileInfo fileInfo = new FileInfo(_sourcePath);
        fileInfo.CopyTo(_targetPath, overwrite);
    }

    /// <summary>
    /// 将目录中的文件拷贝至目标目录。
    /// </summary>
    /// <param name="_sourceFloor">源文件目录。</param>
    /// <param name="_targetFloor">目标目录。</param>
    /// <param name="overwrite">如果存在是否重写。</param>
    public static void CopyFiles(string _sourceFloor, string _targetFloor, bool overwrite = false)
    {
        DirectoryInfo di = new DirectoryInfo(_sourceFloor);
        FileInfo[] fileInfos = di.GetFiles("*.*");
        foreach (FileInfo fileInfo in fileInfos)
            fileInfo.CopyTo(_targetFloor + fileInfo.Name, overwrite);
    }

    /// <summary>
    /// 删除某个文件。
    /// </summary>
    /// <param name="_sourcePath">文件地址。</param>
    public static void DelFile(string _sourcePath)
    {
        FileInfo fileInfo = new FileInfo(_sourcePath);
        fileInfo.Delete();
    }

    /// <summary>
    /// 删除目录中包含指定字符串的文件。
    /// </summary>
    /// <param name="_sourceFloor">目录地址。</param>
    /// <param name="_fileName">字符串。</param>
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
    /// 删除指定目录，指定扩展名的文件。
    /// </summary>
    /// <param name="_sourceFloor">目录地址。</param>
    /// <param name="_extends">扩展名。</param>
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
    /// 根据文件地址获取 AssetBundleName.
    /// </summary>
    /// <param name="_path">地址。</param>
    /// <returns>AssetBundleName.</returns>
    public static string PathToAssetBundleName(string _path)
    {
        extends = Path.GetExtension(_path);

        return _path.Replace(GlobalValues.PATH_PREFAB_BUNDLE, "").Replace("/", "_").Replace(".", "@").ToLower();
    }

    /// <summary>
    /// 根据 AssetBundleName 获取 AssetName.
    /// </summary>
    /// <param name="_assetBundleName">AssetBundleName.</param>
    /// <returns>AssetName.</returns>
    public static string AssetBundleNameToAssetName(string _assetBundleName)
    {
        splitStrs = _assetBundleName.Split('_');

        return splitStrs[splitStrs.Length - 1].Split('@')[0];
    }

    /// <summary>
    /// 根据 AssetBundleName 获取文件的地址。
    /// </summary>
    /// <param name="_assetBundleName">AssetBundleName.</param>
    /// <returns>文件地址.</returns>
    public static string AssetBundleNameToPath(string _assetBundleName)
    {
        return GlobalValues.PATH_PREFAB_BUNDLE + _assetBundleName.Replace("_", "/").Replace("@", ".");
    }

    /// <summary>
    /// 补间值方法。
    /// </summary>
    /// <param name="_callback">补间期间的回调。</param>
    /// <param name="_startValue">起始值。</param>
    /// <param name="_endValue">结束值。</param>
    /// <param name="_duration">补间的时间长度。</param>
    /// <param name="_onComplete">补间结束的回调。</param>
    public static void TweenValue(Action<float> _callback, float _startValue, float _endValue, float _duration, TweenCallback _onComplete = null)
    {
        Tweener tw = DOTween.To((value) => { _callback(value);}, _startValue, _endValue, _duration);
        if (_onComplete != null)
            tw.onComplete = _onComplete;
        tw.SetAutoKill(true);
        tw.PlayForward();
    }
}
