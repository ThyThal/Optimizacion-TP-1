using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] float speed;
    [SerializeField] float shootFrequency;
    Vector3 _dir;
    Vector3 _spawnPoint;
    Rigidbody _rb;
    [SerializeField] float _cooldown;

    public bool CanAttack => _cooldown >= shootFrequency;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _spawnPoint = transform.position;
        _cooldown = 0;
    }

    public override void ManagedUpdate()
    {
        if (GameManager.Instance.GameFinished) return;

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
    }

    private void Update()
    {
        if (GameManager.Instance.GameFinished) return;

        if (_cooldown <= shootFrequency)
        {
            _cooldown += Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.Space) && CanAttack)
        {
            Shoot();
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
        GameManager.Instance.SpawnBullet(this);
        _cooldown = 0;
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
        transform.position = _spawnPoint;
        _rb.velocity = Vector3.zero;
        Health.DoHeal(Health.MaxHealth);
        GameManager.Instance.PlayerDeath();
    }

    private void OnCollisionEnter(Collision collision)
    {
        var enemy = collision.gameObject.GetComponent<Enemy>();
        if(enemy != null)
        {
            Respawn();
            enemy.OnDie();
        }
    }


}
