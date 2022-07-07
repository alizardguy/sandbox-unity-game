using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    //Vars and fields
    [SerializeField] Transform playerCamera;
    [SerializeField][Range(0.001f, 4f)] float mouseSensitivity = 0.13f;
    [SerializeField] float walkSpeed = 6;
    [SerializeField] float gravity = -13.0f;
    [SerializeField][Range(0.0f, 0.5f)] float moveSmoothTime = 0.3f;
    [SerializeField][Range(0.0f, 0.5f)] float mouseSmoothTime = 0.03f;

    [SerializeField] bool lockCursor = true;

    float cameraPitch = 0.0f;
    float velocityY = 0.0f;
    CharacterController controller = null;

    //input binding stuff
    public BasicControl playerControl;
    private InputAction move;
    private InputAction look;

    Vector2 currentDir = Vector2.zero;
    Vector2 currentDirVelocity = Vector2.zero;

    Vector2 currentMouseDelta = Vector2.zero;
    Vector2 currentMouseDeltaVelocity = Vector2.zero;

    private void OnEnable()
    {
        move = playerControl.Player.Movement;
        move.Enable();
        look = playerControl.Player.look;
        look.Enable();
        Debug.Log("player controls enabled");
    }

    private void OnDisable()
    {
        playerControl.Disable();
        move.Disable();
        Debug.Log("player controls disabled");
    }

    private void Awake()
    {
        playerControl = new BasicControl();
    }

    void Start()
    {
        controller = GetComponent<CharacterController>(); //get character controller off the slot

        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }


    }

    void Update()
    {
        UpdateMouseLook(); 
        UpdateMovement();
    }

    void UpdateMouseLook()
    {
        Vector2 targetMouseDelta = look.ReadValue<Vector2>();

        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);

        cameraPitch -= currentMouseDelta.y * mouseSensitivity; //need to apply inverse of the delta so that up is up

        cameraPitch = Mathf.Clamp(cameraPitch, -90.0f, 90.0f); //clamp to not allow you to snap your neck

        playerCamera.localEulerAngles = Vector3.right * cameraPitch;

        transform.Rotate(Vector3.up * currentMouseDelta.x * mouseSensitivity); //affects the object the script is on (the player root slot)
    }

    void UpdateMovement()
    {
        Vector2 targetDir = move.ReadValue<Vector2>();
        targetDir.Normalize();

        currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, moveSmoothTime);

        if (controller.isGrounded)
        {
            velocityY = 0.0f;
        }

        velocityY += gravity * Time.deltaTime; //apply gravity

        Vector3 velocity = (transform.forward * currentDir.y + transform.right * currentDir.x) * walkSpeed + Vector3.up * velocityY;

        controller.Move(velocity * Time.deltaTime); //apply movement
    }
}
