using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerMovement))]
public class PlayerManager : MonoBehaviour
{
    [Header("References")]
    public GameObject camPrefab;
    public PlayerAudio playerAudio;
    public CameraManager cameraManager;
    public CameraController cameraController;
    public PlayerMovement playerMovement;
    public CharacterController controller;
    public Transform orientation;
    public Transform head;

    public IEnumerator Start() // Initialize Player Camera if there is none, no need to put it yourself.
    {
        if (cameraManager == null && cameraController == null)
        {
            cameraManager = Instantiate(camPrefab, Vector3.zero, Quaternion.identity).GetComponent<CameraManager>();
            cameraManager.playerManager = this;
            cameraController = cameraManager.cameraController;
        }
        yield return new WaitForEndOfFrame();
    }
}
