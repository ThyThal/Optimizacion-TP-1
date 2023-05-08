using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Enemy : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float shootFrequency;
    [SerializeField] GameObject bulletPrefab;
    Rigidbody _rb;

    [SerializeField] private List<EnemyRayCheck> _FREE;

    private Dictionary<EnemyRayCheck.EnemyRotateDirection, EnemyRayCheck> _directionDic = new Dictionary<EnemyRayCheck.EnemyRotateDirection, EnemyRayCheck>();

    private void Awake()
    {
        var a = GetComponentsInChildren<EnemyRayCheck>();

        foreach (var item in a)
        {
            _directionDic.Add(item.RotateDirection, item);
        }
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Move();       
    }

    public void Move()
    {
        _rb.velocity = transform.forward * speed;
    }
    
    public void Shoot()
    {

    }

    public void Die()
    {
        gameObject.SetActive(false);
    }

    void OnDrawGizmosSelected()
    {
        // Draws a 5 unit long red line in front of the object
        Gizmos.color = Color.red;
        Vector3 direction = transform.TransformDirection(Vector3.forward) * 1;
        Gizmos.DrawRay(transform.position, direction);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Wall") && _directionDic.GetValueOrDefault(EnemyRayCheck.EnemyRotateDirection.Forward).IsObstructed())
        {
            DoRotation();
        }

        else if (collision.collider.CompareTag("Breakable") && _directionDic.GetValueOrDefault(EnemyRayCheck.EnemyRotateDirection.Forward).IsObstructed())
        {
            DoRotation();
        }

        else if(collision.collider.CompareTag("Enemy") && _directionDic.GetValueOrDefault(EnemyRayCheck.EnemyRotateDirection.Forward).IsObstructed())
        {
            DoRotation();
        }
    }

    private void DoRotation()
    {
        _FREE = new List<EnemyRayCheck>();

        foreach (var enemyRay in _directionDic.Values)
        {
            if (!enemyRay.IsObstructed())
            {
                _FREE.Add(enemyRay);
            }
        }

        // Get Random Direction and Rotate
        var a = _FREE[Random.Range(0, _FREE.Count)];

        switch (a.RotateDirection)
        {
            case EnemyRayCheck.EnemyRotateDirection.Forward:
                transform.rotation = Quaternion.LookRotation(transform.forward);
                return;

            case EnemyRayCheck.EnemyRotateDirection.Back:
                transform.rotation = Quaternion.LookRotation(-transform.forward);
                return;

            case EnemyRayCheck.EnemyRotateDirection.Left:
                transform.rotation = Quaternion.LookRotation(-transform.right);
                return;

            case EnemyRayCheck.EnemyRotateDirection.Right:
                transform.rotation = Quaternion.LookRotation(transform.right);
                return;
        }
    }
}
