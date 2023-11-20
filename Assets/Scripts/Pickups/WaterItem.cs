using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaterItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private bool HasPressed = false;

    [SerializeField] private Transform player;

    private void Start()
    {
        text.gameObject.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        text.gameObject.SetActive(true);

        if (other.CompareTag("Player") && HasPressed)
        {
            // Increase the player's water value
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.currentWater = playerHealth.maxWater;
            }

            // Destroy the water object
            text.gameObject.SetActive(false);

            Destroy(gameObject);
        }
    }

    private void Update()
    {
        float dist = Vector3.Distance(transform.position, player.transform.position);
        if (Input.GetKeyDown(KeyCode.E) && dist <= 2)
        { 
            HasPressed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        text.gameObject.SetActive(false);
    }
}
