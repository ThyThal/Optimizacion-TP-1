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
    [SerializeField] private Vector3 sizeHalfEnemy;
    [SerializeField] private LayerMask maskCollitionAround;
    [SerializeField] private Collider[] otherCollider = new Collider[3];
    private int layerPlayer = 6;
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        _dir = Vector3.forward;
        Move();

        NonAllocColiider();

       
    }

    public void Move()
    {
        _rb.velocity = _dir.normalized * speed;
    }

    public void ChangeDirection(LayerMask layerWall)
    {
        // Cambiar direccion al chocar con una pared
        if (layerWall == 8)
        {
            Debug.Log("ENTRO");
            _dir = -Vector3.forward;
            Rotate(_dir);
        }
        //else if (¿)
        //{
        //    _dir = -Vector3.right;
        //    Rotate(_dir);
        //}
        //else if ()
        //{
        //    _dir = -Vector3.forward;
        //    Rotate(_dir);
        //}
        //else if (s)
        //{
        //    _dir = Vector3.right;
        //    Rotate(_dir);
        //}
    }

    public void Rotate(Vector3 lookDir)
    {
        transform.rotation = Quaternion.LookRotation(lookDir);
    }
    

    private void NonAllocColiider()
    {
        int hitCount = Physics.OverlapBoxNonAlloc(transform.position, sizeHalfEnemy, otherCollider, Quaternion.identity, maskCollitionAround);
        for (int i = 0; i < hitCount; i++)
        {
            if (otherCollider[i].gameObject.layer == layerPlayer)
            {
                //ME DESTRUYO CON EL PLAYER
                Destroy(this.gameObject);
                Debug.Log("Hit CHOCASTE CON EL PLAYER " + otherCollider[i].gameObject.name);
            }

            ChangeDirection(otherCollider[i].gameObject.layer);
        }
    }
    public void Shoot()
    {

    }

    public void Die()
    {

    }
}
