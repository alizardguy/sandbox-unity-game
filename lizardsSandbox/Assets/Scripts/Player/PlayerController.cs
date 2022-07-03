using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Vars and fields
    [SerializeField] Transform playerCamera;
    [SerializeField] float mouseSensitivity = 3.5f;

    [SerializeField] bool lockCursor = true;

    float cameraPitch = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }    
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMouseLook(); //calls UpdateMouseLook   
    }

    // Called by update void
    void UpdateMouseLook()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        cameraPitch -= mouseDelta.y * mouseSensitivity; //need to apply inverse of the delta so that up is up

        cameraPitch = Mathf.Clamp(cameraPitch, -90.0f, 90.0f); //clamp to not allow you to snap your neck

        playerCamera.localEulerAngles = Vector3.right * cameraPitch;

        transform.Rotate(Vector3.up * mouseDelta.x * mouseSensitivity); //affects the object the script is on (the player root slot)
    }
}
