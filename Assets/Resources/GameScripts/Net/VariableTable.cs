using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

public class VariableTable : MonoBehaviour
{
    [SerializeField]
    public List<Variable> variableTables;

    public LuaTable Variables
    {
        get
        {
            LuaTable luaTable = Lua.Instance.LuaEnv.NewTable();
            foreach (Variable variable in variableTables)
                if (variable.type == VariableType.Component)
                    luaTable.Set(variable.name, variable.component);
                else
                    luaTable.Set(variable.name, variable.gameObject);

            return luaTable;
        }
    }
}

[System.Serializable]
public class Variable
{
    [SerializeField]
    public string name;

    [SerializeField]
    public VariableType type = VariableType.Component;

    [SerializeField]
    public Component component;

    [SerializeField]
    public GameObject gameObject;
}

public enum VariableType
{
    Component,
    GameObject
}