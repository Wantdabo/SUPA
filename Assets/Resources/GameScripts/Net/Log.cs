using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log
{
    private static Log log;

    public static Log Instance
    {
        get
        {
            if (log == null)
                log = new Log();

            return log;
        }
    }

    private Log() { }

    public void Print(string _log)
    {
        Debug.Log(_log);
    }

    public void Warning(string _log)
    {
        Debug.LogWarning(_log);
    }

    public void Error(string _log)
    {
        Debug.LogError(_log);
    }
}
