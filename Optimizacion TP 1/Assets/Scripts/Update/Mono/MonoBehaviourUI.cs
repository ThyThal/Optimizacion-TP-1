using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoBehaviourUI : ManagedTickObject
{
    void Awake()
    {
        CustomUpdateManager.Instance.CustomUpdateUI.AddObject(this);
    }

    void OnDestroy()
    {
        CustomUpdateManager.Instance.CustomUpdateUI.RemoveObject(this);
    }
}