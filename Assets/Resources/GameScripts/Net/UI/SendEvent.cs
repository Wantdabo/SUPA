using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendEvent : MonoBehaviour
{
    [SerializeField]
    public string eventID;
    [SerializeField]
    public string eventKey;

    private EventBox eventBox;

    public void Send() {
        eventBox.eventID = eventID;
        eventBox.eventKey = eventKey;
        Lua.Instance.SendEvent(eventBox);
    }
}
