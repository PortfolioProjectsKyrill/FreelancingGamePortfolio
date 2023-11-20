using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", order = 1)]
public class PlayerMoveData : ScriptableObject
{
    [Header("Normal Data")]
    public float walkSpeed = 5f;
    public float sprintSpeed = 8f;
    public float crouchSpeed = 2.5f;
    public float walkDamp = 4.5f;
    public float sprintDamp = 3.95f;
    public float crouchDamp = 20f;
    public float airSmoothen = 2.25f;

    [Header("Crouch Data")]
    [Range(0, 20.0f)] public float heightLerp = 10f;
    public float standHeight = 2f;
    public float crouchHeight = 1f;

    [Header("Jump Data")]
    public float jumpForce = 2.5f;

    [Header("Ground Detection")]
    public LayerMask groundMask;
    public float CoyoteTimeMax = 0.1f;
    public float FallTimeMax = 0.25f;
    public float groundDistance = 1f;
    public float slopeSlideSpeed = 1f;
    public float antiBump = 5f;

}
