using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSelfDestruct : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //remove this from list
            GameManager.instance.colliders.Remove(gameObject);
            //destroy
            Destroy(gameObject);
        }
    }
}
