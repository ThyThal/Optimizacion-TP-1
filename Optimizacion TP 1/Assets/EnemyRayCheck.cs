using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class EnemyRayCheck : MonoBehaviour
{
    [SerializeField] private SphereCollider _collider;
    [SerializeField] private bool _obstructed = false;
    [SerializeField] private Vector3 _rotateVector;
    [SerializeField] public EnemyRotateDirection RotateDirection;

    public enum EnemyRotateDirection
    {
        Forward,
        Back,
        Left,
        Right
    }

    public bool IsObstructed()
    {
        return _obstructed;
    }

    public Vector3 GetRotateDirection => _rotateVector;


    // Start is called before the first frame update
    void Start()
    {
        if (_collider == null)
        {
            _collider = GetComponent<SphereCollider>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            _obstructed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            _obstructed = false;
        }
    }
}
