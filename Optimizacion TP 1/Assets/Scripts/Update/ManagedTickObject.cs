using UnityEngine;

public class ManagedTickObject : MonoBehaviour
{
    /// <summary>
    /// Sets Target FPS.
    /// *Should be Read Only.*
    /// </summary>
    [SerializeField] [Range(30, 240)] private int _targetFPS = 30;

    private float _lastUpdate;

    /// <summary>
    /// Returns Target FPS.
    /// </summary>
    public int GetTargetFPS => _targetFPS;

    /// <summary>
    /// Returns Last Update Time.
    /// </summary>
    private float GetLastUpdate => _lastUpdate;

    /// <summary>
    /// Returns the Time needed to reach the Target FPS.
    /// </summary>
    public float GetTickInterval => 1 / (float)GetTargetFPS;

    /// <summary>
    /// Returns Delta Time for Current Object.
    /// </summary>
    public float GetDeltaTime => Time.time - GetLastUpdate;

    /// <summary>
    /// Updates the Object.
    /// </summary>
    public void DoTick()
    {
        _lastUpdate = Time.time;
    }
}
