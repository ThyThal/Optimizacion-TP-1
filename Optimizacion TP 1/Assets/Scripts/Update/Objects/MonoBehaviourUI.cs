using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoBehaviourUI : ManagedTickObject
{
    [Range(30, 240)] private int TargetFPS = 30;

    // Start is called before the first frame update
    void Awake()
    {
        SetTargetFPS(TargetFPS);
    }
}