using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Vars and fields
    [SerializeField] Transform playerCamera;
    [SerializeField] float mouseSensitivity = 3.5f;
    [SerializeField] float walkSpeed = 6;
    [SerializeField][Range(0.0f, 0.5f)] float moveSmoothTime = 0.3f;
    [SerializeField] [Range(0.0f, 0.5f)] float mouseSmoothTime = 0.03f;

    [SerializeField] bool lockCursor = true;

    float cameraPitch = 0.0f;
    CharacterController controller = null;

    Vector2 currentDir = Vector2.zero;
    Vector2 currentDirVelocity = Vector2.zero;

    Vector2 currentMouseDelta = Vector2.zero;
    Vector2 currentMouseDeltaVelocity = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>(); //get character controller off the slot

        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }    
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMouseLook(); 
        UpdateMovement();
    }

    // Called by update void
    void UpdateMouseLook()
    {
        Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);

        cameraPitch -= currentMouseDelta.y * mouseSensitivity; //need to apply inverse of the delta so that up is up

        cameraPitch = Mathf.Clamp(cameraPitch, -90.0f, 90.0f); //clamp to not allow you to snap your neck

        playerCamera.localEulerAngles = Vector3.right * cameraPitch;

        transform.Rotate(Vector3.up * currentMouseDelta.x * mouseSensitivity); //affects the object the script is on (the player root slot)
    }

    // Movment update void
    void UpdateMovement()
    {
        Vector2 targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")); //input direction value (hopefully this is the modern binding system)
        targetDir.Normalize();

        currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, moveSmoothTime);

        Vector3 velocity = (transform.forward * currentDir.y + transform.right * currentDir.x) * walkSpeed;

        controller.Move(velocity * Time.deltaTime); //apply movement
    }
}
