using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Lift : MonoBehaviour
{
    [SerializeField] private Transform OtherLiftPoint;
    [SerializeField] private TextMeshProUGUI AccessText;
    public InputAction.CallbackContext ctx;

    [SerializeField] private GameObject Player;

    public static bool hasPressed = false;

    [SerializeField] private CharacterController characterController;
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private PlayerMovement pm;

    [SerializeField] private float dist;
    [SerializeField] private Transform player;

    private void Update()
    {
        dist = Vector3.Distance(transform.position, player.position);
        if (Input.GetKeyDown(KeyCode.E) && dist <= 2)
        {
            hasPressed = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AccessText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (hasPressed)
        {
            TeleportPlayer();
            hasPressed = false;
        }
    }

    private void TeleportPlayer()
    {
        Regulate(false);
        Player.transform.position = OtherLiftPoint.position;
        Regulate(true);
    }

    private void Regulate(bool Bool)
    {
        pm.enabled = Bool;
        playerManager.enabled = Bool;
        characterController.enabled = Bool;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AccessText.gameObject.SetActive(false);
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(gameObject.transform.position, 0.4f);
        Gizmos.DrawLine(transform.position, OtherLiftPoint.position);
    }
#endif
}
