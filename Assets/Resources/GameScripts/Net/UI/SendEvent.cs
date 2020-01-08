using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendEvent : MonoBehaviour
{
    [SerializeField]
    public string eventID;
    [SerializeField]
    public string eventKey;
    
    public void Send() {
        Lua.Instance.SendEvent(eventID, eventKey);
    }
}
