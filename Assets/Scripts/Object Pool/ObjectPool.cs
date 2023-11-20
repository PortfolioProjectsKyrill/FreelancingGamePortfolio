using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject PooledObject;

    [SerializeField] private int poolSize;
    [SerializeField] private Stack<PoolItem> objectPool;

    PlayerHealth playerHealth;
    EnemyHealth[] enemyHealth;
    private void Start()
    {
        objectPool = new Stack<PoolItem>();
        playerHealth = FindObjectOfType<PlayerHealth>();
        enemyHealth = FindObjectsOfType<EnemyHealth>();

        Expand(75);
    }

    public void Expand(int l_ExpandSize)
    {
        for (int i = 0; i < l_ExpandSize; i++)
        {
            GameObject newObject = Instantiate(PooledObject);
            PoolItem Item = newObject.GetComponent<PoolItem>();
            Subject item = newObject.GetComponent<Subject>();
            Item.PoolProperty = this;
            playerHealth._subjects.Add(item);
            for (int j = 0; j < enemyHealth.Length; j++) 
            {
                enemyHealth[j]._subjects.Add(item);
            }
            ReturnPooledObject(Item);
        }
    }

    public GameObject GetPooledObject(Vector3 position, Quaternion rotation, Transform parent = null)
    {
        if (objectPool.Count == 0)
        {
            Debug.Log("${name}: Pool is empty");
            return null;
        } 

        PoolItem l_poolItem = objectPool.Pop();
        l_poolItem.Initialize(position, rotation, parent != null ? parent : transform);
        l_poolItem.gameObject.SetActive(true);
        return l_poolItem.gameObject;
    }

    public void ReturnPooledObject(PoolItem l_poolItem)
    {
        if (!gameObject.activeSelf)
        {
            return;
        }

        l_poolItem.transform.parent = transform;
        l_poolItem.gameObject.SetActive(false);

        objectPool.Push(l_poolItem);
    }
}
