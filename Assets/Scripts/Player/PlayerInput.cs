using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerInput : MonoBehaviour
{
    public static PlayerInput Instance;
    PlayerInputActions playerInputActions;
    public Vector2 move { get { return _move; } set { _move = value; } }
    private Vector2 _move;
    public Vector2 look { get { return _look; } set { _look = value; } }
    private Vector2 _look;
    public bool jump { get { return _jump; } set { _jump = value; } }
    private bool _jump;
    public InputAction jumpAction { get { return _jumpAction; } set { _jumpAction = value; } }
    private InputAction _jumpAction;
    public bool crouchHold { get { return _crouchHold; } set { _crouchHold = value; } }
    private bool _crouchHold;
    public InputAction crouchAction { get { return _crouchAction; } set { _crouchAction = value; } }
    private InputAction _crouchAction;
    public bool sprint { get { return _sprint; } set { _sprint = value; } }
    private bool _sprint;
    public InputAction sprintAction { get { return _sprintAction; } set { _sprintAction = value; } }
    private InputAction _sprintAction;

    /**
    Some stuff can't be done with just event-based input like sending contexts using .performed and .canceled
    because I can't find a workaround for them. So I use InputAction.triggered and InputAction.IsPressed() instead.
    */
    private void Awake()
    {
        Instance = this;
        playerInputActions = new PlayerInputActions();
    }

    private void Start()
    {
        jumpAction = playerInputActions.Default.Jump;
        crouchAction = playerInputActions.Default.Crouch;
        sprintAction = playerInputActions.Default.Sprint;
    }

    private void OnEnable()
    {
        playerInputActions.Default.Enable();
    }
    // These public voids are accessed by the Input System's Player Input script that has its behaviour set to "Invoke Unity Events"
    public void MoveInput(InputAction.CallbackContext ctx)
    {
        _move = ctx.ReadValue<Vector2>();
    }

    public void LookInput(InputAction.CallbackContext ctx)
    {
        _look = ctx.ReadValue<Vector2>();
    }

    public void JumpInput(InputAction.CallbackContext ctx)
    {
        _jump = ctx.ReadValueAsButton();
    }

    public void HoldCrouch(InputAction.CallbackContext ctx)
    {
        _crouchHold = ctx.ReadValueAsButton();
    }

    public void SprintInput(InputAction.CallbackContext ctx)
    {
        _sprint = ctx.ReadValueAsButton();
    }

    private void OnDisable()
    {
        playerInputActions.Default.Disable();
    }
}