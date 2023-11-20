using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    [SerializeField] private GameObject Bullet;
    [SerializeField] private Transform Shootpoint;

    public ObjectPool Pool;

    /// <summary>
    /// Fires a bullet in the direction the player is looking (implement in UnityEditor(Under PlayerInput))
    /// </summary>
    /// <param name="cxt"></param>
    public void Fire(InputAction.CallbackContext cxt)
    {
        if (cxt.started)
        {
            Quaternion baseRotation = Shootpoint.rotation;
            Vector3 basePosition = Shootpoint.position;

            GameObject bulletTransform = Pool.GetPooledObject(basePosition, baseRotation, null);
            if (!bulletTransform.GetComponent<Bullet>())
                bulletTransform.AddComponent<Bullet>();

            Vector3 shootDir = (Shootpoint.transform.position - transform.position).normalized;

            bulletTransform.GetComponent<Bullet>().Init(shootDir);
        }
    }
}
