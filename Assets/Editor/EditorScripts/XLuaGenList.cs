using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XLua;

public static class XLuaGenList {
    [LuaCallCSharp]
    public static List<System.Type> xluaGenList = new List<System.Type>() {
        typeof(Object),
        typeof(GameObject),
        typeof(Vector2),
        typeof(Vector3),
        typeof(Color),
        typeof(AudioSource),

        typeof(Main),
        typeof(AssetLoader),
        typeof(SceneLoader),
        typeof(Timer),
        typeof(Log),
    };
}
