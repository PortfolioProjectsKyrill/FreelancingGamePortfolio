using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.InputSystem;

public class Bullet : Subject
{
    [Header("Bullet Init")]
    public bool hasFired = false;
    public Rigidbody rb;
    public float bulletSpeed = 15;
    private Vector3 ShootDir;

    [Header("Colliding")]
    protected Collision collidingObject;
    public static int BulletDamage = 10;
    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        hasFired = true;
        rb.velocity = Vector3.zero;
    }

    public void Init(Vector3 shootDir)
    {
        ShootDir = shootDir;
    }

    private void FixedUpdate()
    {
        if (hasFired)
        {
            rb.AddForce(ShootDir * 4f, ForceMode.Impulse);
        }
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        collidingObject = collision;
        if (collidingObject.gameObject.CompareTag("Enemy"))
        {
            NotifyObserver();
        }
    }

    private void OnDisable()
    {
        hasFired = false;
    }

    protected void NotifyObserver()
    {
        NotifyObservers(BulletDamage);
    }
}
