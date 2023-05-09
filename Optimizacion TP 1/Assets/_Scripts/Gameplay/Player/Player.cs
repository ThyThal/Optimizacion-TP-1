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
    float _cooldown;



    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _spawnPoint = transform.position;

        _cooldown = 0;
    }

    public override void ManagedUpdate()
    {
        if (Input.GetAxisRaw("Vertical") != 0)
        {
            _dir = Vector3.forward * Input.GetAxisRaw("Vertical");
            Rotate(_dir);
        }
        else if (Input.GetAxisRaw("Horizontal") != 0)
        {
            _dir = Vector3.right * Input.GetAxisRaw("Horizontal");
            Rotate(_dir);
        }
        else 
        {
            if (_dir != Vector3.zero) 
            {
                _dir = Vector3.zero;
                _rb.velocity = _dir;
            }
        }

        if (_cooldown <= shootFrequency)
        {
            _cooldown += Time.deltaTime;
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            Shoot();
        }

        Debug.Log(_cooldown);
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
        Debug.Log("Spawn Bullet");
        _cooldown = 0;
        GameManager.Instance.SpawnBullet(this);
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
