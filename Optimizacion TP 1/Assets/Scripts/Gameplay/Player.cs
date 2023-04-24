using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviourGameplay
{
    public override void ManagedUpdate()
    {
        CustomLogger.Log("Hello Player...");
    }
}
