using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoBehaviourGameplay : ManagedTickObject
{
    void Awake()
    {
        CustomUpdateManager.Instance.CustomUpdateGameplay.AddObject(this);
    }

    void OnDestroy()
    {
        CustomUpdateManager.Instance.CustomUpdateGameplay.RemoveObject(this);
    }
}