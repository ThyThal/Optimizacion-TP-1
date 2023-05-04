using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoBehaviourGameplay : ManagedTickObject
{
    void Awake()
    {
        Debug.Log("Gameplay Object Awake");
        CustomUpdateManager.Instance.CustomUpdateGameplay.AddObject(this);
    }

    void OnDestroy()
    {
        Debug.Log("Gameplay Object Destroy");
        CustomUpdateManager.Instance.CustomUpdateGameplay.RemoveObject(this);
    }
}