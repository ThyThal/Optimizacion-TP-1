using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] float speed;
    [SerializeField] float shootFrequency;
    [SerializeField] GameObject bulletPrefab;
    Vector3 _dir;
    Vector3 _spawnPoint;
    Rigidbody _rb;



    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _spawnPoint = transform.position;
    }

    public override void ManagedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            _dir = Vector3.forward;
            Rotate(_dir);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            _dir = -Vector3.right;
            Rotate(_dir);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            _dir = -Vector3.forward;
            Rotate(_dir);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            _dir = Vector3.right;
            Rotate(_dir);
        }

        else
        {
            _dir = Vector3.zero;
            _rb.velocity = Vector3.zero;
        }

    }

    public void Move()
    {
        _rb.velocity = _dir.normalized * speed;
    }

    public void Rotate(Vector3 lookDir)
    {
        transform.rotation = Quaternion.LookRotation(lookDir);
        Move();
    }

    public void Shoot()
    {

    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);

        if (!IsAlive())
        {
            Respawn();
        }
    }

    public void Respawn()
    {
        CustomLogger.Log("Respawn Player");

        transform.position = _spawnPoint;
        Health.DoHeal(Health.MaxHealth);
    }

    private void OnCollisionEnter(Collision collision)
    {
        var enemy = collision.gameObject.GetComponent<Enemy>();
        if(enemy != null)
        {
            Respawn();
            enemy.Die();
        }
    }


}