using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TickManager : MonoBehaviour
{
    private ManagedTickObject[] _managedTickObject;

    private void Start()
    {
        // Finds all Tick Objects in the Scene, and Store them.
        _managedTickObject = GetComponents<ManagedTickObject>();
    }

    private void Update()
    {
        int objectCount = _managedTickObject.Length;
        for (int i = 0; i < objectCount; i++)
        {
            // Get Current Tick Object.
            ManagedTickObject currentObject = _managedTickObject[i];

            // Update Object if Delta Time needed for FPS is Reached.
            if (currentObject.GetDeltaTime >= currentObject.GetTickInterval)
            {
                CustomLogger.Log($"{currentObject.gameObject.name}: Target FPS={currentObject.GetTargetFPS} | Tick Interval={currentObject.GetTickInterval}");

                currentObject.DoTick();
            }
        }
    }
}
