using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomUpdateManager : MonoBehaviour
{
    [SerializeField] private ManagedTickObject[] _managedTickObject;

    private void Start()
    {
        // Finds all Tick Objects in the Scene, and Store them.
        _managedTickObject = FindObjectsOfType<ManagedTickObject>();

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
                #if ENABLE_LOGS
                CustomLogger.Log($"{currentObject.gameObject.name}: Target FPS={currentObject.GetTargetFPS} | Tick Interval={currentObject.GetTickInterval}");
                #endif

                currentObject.DoTick();
                currentObject.ManagedUpdate();
            }
        }
    }
}
