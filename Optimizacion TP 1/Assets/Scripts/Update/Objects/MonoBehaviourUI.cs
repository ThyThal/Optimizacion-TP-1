using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoBehaviourUI : NewManagedTickObject
{
    void Awake()
    {
        Debug.Log("UI Object Awake");
        NewCustomUpdateManager.Instance.CustomUpdateUI.AddObject(this);
    }

    void OnDestroy()
    {
        Debug.Log("UI Object Destroy");
        NewCustomUpdateManager.Instance.CustomUpdateUI.RemoveObject(this);
    }
}