using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CameraManager cameraManager;

    [SerializeField] private Transform camHolder;

    [Header("Camera")]
    [SerializeField] public bool Clamping;
    [SerializeField] float UpClamp, DownClamp;
    [SerializeField] float multiplier = 0.01f;
    [SerializeField] float xRotation, yRotation;
    [SerializeField] public float x, y, damp;

    private void Update()
    {
        // This one does the actual clamping of the X axis rotation of the camera
        xRotation = cameraManager.clampCam ? Mathf.Clamp(xRotation, UpClamp, DownClamp) : xRotation;
        
        cameraManager.playerManager.transform.localEulerAngles = new Vector3(0, y, 0);

        // Rotate both the camera's X and Y axis, for Z axis check CameraManager
        camHolder.transform.localEulerAngles = new Vector3(x, y, !cameraManager.reduceMotion ? cameraManager.targetMoveTilt + cameraManager.moveBobRot.z : 0f);

        Look();
    }

    private void Look()
    {
        // Does the actual checking whether camera is aiming right at the Up/Down clamps.
        Clamping = ((xRotation == UpClamp) || (xRotation == DownClamp));

        // Camera smoothing, lerps to actual x and y location of the camera
        x = Mathf.Lerp(x, xRotation, damp * 10f * Time.deltaTime);
        y = Mathf.Lerp(y, yRotation, damp * 10f * Time.deltaTime);


        yRotation += PlayerInput.Instance.look.x * cameraManager.sensX * multiplier;

        xRotation -= PlayerInput.Instance.look.y * cameraManager.sensY * multiplier;
    }
}