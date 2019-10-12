using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    [SerializeField]
    public Transform uiRoot;

    [SerializeField]
    public Transform uiSound;

    private static Main main;

    public static Main Instance
    {
        get
        {
            if(main == null)
                main = Camera.main.GetComponent<Main>();

            return main;
        }
    }

    private void Start()
    {
        Object[] objs = GameObject.FindObjectsOfType<GameObject>();
        foreach (Object obj in objs)
            DontDestroyOnLoad(obj);

        AddComponent();
        Lua.Instance.Start();
    }

    private void AddComponent() {
        gameObject.AddComponent<SceneLoader>();
    }
}