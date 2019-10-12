using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

#region OTHER_DEFINE
public enum PLATFORM
{
    PLAT_IOS = 9,
    PLAT_AND = 13,
    PLAT_WIN = 19
};

public enum RECYCLEPRI
{
    SCENE_GC,
    INVOKE_GC,
    DONT_GC
}

[CSharpCallLua]
public delegate void EVENT_DEL_VOID_ASSETBOX(AssetBox _assetBox);
[CSharpCallLua]
public delegate void EVENT_DEL_VOID_FLOAT(float _value);

public struct EventBox {
    public string eventID;
    public string eventKey;
}

public struct AssetBox
{
    public string name;
    public string path;
    public string assetBundleName;
    public Object obj;
    public AssetBundle assetBundle;
}

public struct AssetAsyncBox
{
    public string name;
    public string path;
}

public struct AssetPoolBox
{
    public string assetBundlename;
    public AssetBox assetBox;
    public RECYCLEPRI recyclePri;
}
#endregion

public class GlobalValues
{
    public static PLATFORM TYPE_PLAT = PLATFORM.PLAT_WIN;

    #region EXTENDS DEFINE
    public static string EXT_CS = ".cs";
    public static string EXT_ASSET = ".asset";
    public static string EXT_UNITY = ".unity";
    public static string EXT_PREFAB = ".prefab";
    public static string EXT_MANIFEST = ".manifest";
    public static string EXT_META = ".meta";
    public static string EXT_PNG = ".png";
    public static string EXT_JPG = ".jpg";
    public static string EXT_LUA = ".lua";
    #endregion

    #region STRING DEFINE
    public static string STR_AB_MANI = "AssetBundleManifest";
    public static string STR_ASSETS = "Assets";
    public static string STR_PREFAB_BUNDLES = "PrefabBundles";
    public static string STR_MAIN_LUA = "Main";
    #endregion
    #region LUA FUNC DEFINE
    public static string FUNC_START = "Start";
    public static string FUNC_HANDLE_EVENT = "HandleEvent";
    #endregion
    #region LUA EVENT DEFINE
    #endregion
    #region PATH DEFINE
    public static string PATH_LUA = "GameScripts/Lua/";
    public static string PATH_EXPORT_AB = string.Format("AssetBundles/{0}/", GET_AB_FLOOR_NAME);
    public static string PATH_PREFAB_BUNDLE = string.Format("Assets/Editor/{0}/", STR_PREFAB_BUNDLES);
    public static string PATH_STREAMING_PATH = Application.streamingAssetsPath;
    public static string PATH_AB_LOAD = PATH_STREAMING_PATH + "/" + STR_ASSETS.ToLower() + "/";
    #endregion

    #region GET_DEFINE
    public static string GET_AB_FLOOR_NAME
    {
        get
        {
            if (TYPE_PLAT == PLATFORM.PLAT_AND)
                return "android";
            else if (TYPE_PLAT == PLATFORM.PLAT_IOS)
                return "ios";
            else
                return "windows";
        }
    }

    public static string GET_AB_LOAD_PATH
    {
        get
        {
#if UNITY_EDITOR
            return PATH_EXPORT_AB;
#else
            return PATH_AB_LOAD;
#endif
        }
    }

    public static string GET_MANI_AB_LOAD_PATH
    {
        get
        {
            return GET_AB_LOAD_PATH + GET_AB_FLOOR_NAME.ToLower();
        }
    }
    #endregion
}
