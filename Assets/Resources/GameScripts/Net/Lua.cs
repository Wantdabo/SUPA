using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

public class Lua
{
    private static Lua lua;
    private LuaEnv luaEnv;
    private TextAsset textAsset;
    private EventBox eventBox;

    private LuaFunction startFunc;
    private LuaFunction handleEventFunc;

    public static Lua Instance
    {
        get
        {
            if (lua == null)
                lua = new Lua();

            return lua;
        }
    }

    public LuaEnv LuaEnv
    {
        get
        {
            return luaEnv;
        }
    }

    private Lua()
    {
        luaEnv = new LuaEnv();
        luaEnv.AddLoader((ref string _filePath) =>
        {
            // Add HotFix in the future.
            textAsset = Resources.Load<TextAsset>(GlobalValues.PATH_LUA + _filePath + GlobalValues.EXT_LUA);

            return textAsset.bytes;
        });

        luaEnv.DoString(string.Format("require('{0}')", GlobalValues.STR_MAIN_LUA));

        startFunc = luaEnv.Global.Get<LuaFunction>(GlobalValues.FUNC_START);
        handleEventFunc = luaEnv.Global.Get<LuaFunction>(GlobalValues.FUNC_HANDLE_EVENT);
    }

    public void Start()
    {
        startFunc.Call();
    }

    public void SendEvent(string _eventID, string _eventKey)
    {
        eventBox.eventID = _eventID;
        eventBox.eventKey = _eventKey;
        handleEventFunc.Call(eventBox);
    }
}
