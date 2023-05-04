using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoBehaviourUI : ManagedTickObject
{
    void Awake()
    {
        Debug.Log("UI Object Awake");
        CustomUpdateManager.Instance.CustomUpdateUI.AddObject(this);
    }

    void OnDestroy()
    {
        Debug.Log("UI Object Destroy");
        CustomUpdateManager.Instance.CustomUpdateUI.RemoveObject(this);
    }
}