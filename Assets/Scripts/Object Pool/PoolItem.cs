using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolItem : MonoBehaviour
{
    private ObjectPool MyPool;

    public ObjectPool PoolProperty { set { MyPool = value; } }

    [SerializeField] private float DeathTimer;

    private void Update()
    {
        if (MyPool != null)
        {
            DeathTimer += Time.deltaTime;
            if (DeathTimer >= 5)
            {
                ReturnToPool();
            }
        }
    }

    private void Activate()
    {
        DeathTimer = 0.0f;
    }

    private void DeActivate()
    {
        gameObject.SetActive(false);
        gameObject.transform.position = MyPool.gameObject.transform.position;
    }

    public void Initialize(Vector3 position, Quaternion rotation, Transform parent)
    {
        transform.position = position;
        transform.rotation = rotation;
        transform.parent = parent;

        Activate();
    }

    public void ReturnToPool()
    {
        DeActivate();

        MyPool.ReturnPooledObject(this);
    }

    private void OnCollisionEnter(Collision collision)
    {
        ReturnToPool();
    }
}