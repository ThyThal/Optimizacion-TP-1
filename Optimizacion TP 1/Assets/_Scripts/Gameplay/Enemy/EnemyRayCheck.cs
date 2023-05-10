using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class EnemyRayCheck : MonoBehaviourGameplay
{
    [SerializeField] private SphereCollider _collider;
    [SerializeField] private bool _obstructed = false;
    [SerializeField] private Vector3 _rotateVector;
    [SerializeField] public EnemyRotateDirection RotateDirection;
    [SerializeField] private LayerMask _layerMask;

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

    private void Awake()
    {
        CheckStartCollisions();
    }


    // Start is called before the first frame update
    void Start()
    {
        if (_collider == null)
        {
            _collider = GetComponent<SphereCollider>();
        }
    }

    public void CheckStartCollisions()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 0.25f, _layerMask);
        foreach (var item in colliders)
        {
            if (item.CompareTag("Wall"))
            {
                _obstructed = true;
            }

            else if (item.CompareTag("Breakable"))
            {
                _obstructed = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            _obstructed = true;
        }

        else if (other.CompareTag("Breakable"))
        {
            _obstructed = true;
        }

        else if (other.CompareTag("Enemy"))
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

        else if (other.CompareTag("Breakable"))
        {
            _obstructed = false;
        }

        else if (other.CompareTag("Enemy"))
        {
            _obstructed = false;
        }
    }
}
