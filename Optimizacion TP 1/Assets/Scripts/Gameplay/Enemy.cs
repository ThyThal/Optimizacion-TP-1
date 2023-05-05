using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float shootFrequency;
    [SerializeField] GameObject bulletPrefab;
    Vector3 _dir;
    Rigidbody _rb;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Move()
    {
        _rb.velocity = _dir.normalized * speed;
    }

    public void ChangeDirection()
    {
        // Cambiar direccion al chocar con una pared
    }

    public void Rotate(Vector3 lookDir)
    {
        transform.rotation = Quaternion.LookRotation(lookDir);
    }

    public void Shoot()
    {

    }

    public void Die()
    {

    }
}
