using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TickUI : ManagedTickObject
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void DoTick()
    {
        CustomLogger.Log($"{gameObject.name} Do Tick!");
        base.DoTick();


    }
}