using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoBehaviourGameplay : NewManagedTickObject
{
    void Awake()
    {
        Debug.Log("Gameplay Object Awake");
        NewCustomUpdateManager.Instance.CustomUpdateGameplay.AddObject(this);
    }

    void OnDestroy()
    {
        Debug.Log("Gameplay Object Destroy");
        NewCustomUpdateManager.Instance.CustomUpdateGameplay.RemoveObject(this);
    }
}