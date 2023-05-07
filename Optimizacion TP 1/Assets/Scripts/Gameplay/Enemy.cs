using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Enemy : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float shootFrequency;
    [SerializeField] GameObject bulletPrefab;
    Vector3 _dir;
    Rigidbody _rb;
    //
    [SerializeField] private Vector3 sizeHalfEnemy;
    [SerializeField] private LayerMask maskCollitionAround;
    [SerializeField] private Collider[] otherCollider = new Collider[3];



    // Wall Detection
    [SerializeField] private EnemyRayCheck[] _directions;

    private int layerPlayer = 6;
    private int layerWall=8;
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _dir = Vector3.forward;
    }

    void Update()
    {
        Move();       
    }

    public void Move()
    {
        _rb.velocity = _dir.normalized * speed;
    }
    
    public void Shoot()
    {

    }

    public void Die()
    {

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
        if (collision.collider.CompareTag("Wall"))
        {
            DoRotation();
        }
    }

    private void DoRotation()
    {
        List<Vector3> _availableDirections = new List<Vector3>();

        foreach (var enemyRay in _directions)
        {
            if (!enemyRay.IsObstructed())
            {
                _availableDirections.Add(enemyRay.GetRotateDirection);
            }
        }

        // Get Random Direction and Rotate
        transform.rotation = Quaternion.LookRotation(_availableDirections[Random.Range(0, _availableDirections.Count - 1)]);

        Debug.Log("Do Rotation.");
    }
}
