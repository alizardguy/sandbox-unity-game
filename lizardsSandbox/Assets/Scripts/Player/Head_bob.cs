using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head_bob : MonoBehaviour
{
    [SerializeField] private bool _enable = true;

    [SerializeField, Range(0, 0.1f)] private float _amplitude = 0.015f;
    [SerializeField, Range(0, 30f)] private float _frequency = 10.0f;

    [SerializeField] private Transform camera;
    [SerializeField] private Transform cameraHolder;

    private float _toggleSpeed = 3.0f;
    private Vector3 _startPos;
    private CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        _startPos = camera.localPosition;
    }

    // Update is called once (1) per frame
    void Update()
    {
        if (!_enable) return;

        CheckMotion();
        ResetPosition();
    }

    private void CheckMotion() //check if motion should be played
    {
        ResetPosition();
        float speed = new Vector3(controller.velocity.x, 0, controller.velocity.z).magnitude;

        if (speed < _toggleSpeed) return; // less than speed exit
        if (!controller.isGrounded) return; //not grounded exit

        PlayMotion(FootStepMotion());
    }

    private void PlayMotion(Vector3 motion) //play
    {
        camera.localPosition += motion;
    }

    private Vector3 FootStepMotion() //the foot step math
    {
        Vector3 pos = Vector3.zero; //setup the value that will be returned
        pos.y += Mathf.Sin(Time.deltaTime * _frequency) * _amplitude;
        pos.x += Mathf.Cos(Time.deltaTime * _frequency / 2) * _amplitude * 2;
        return pos; //return the pos value
    }

    private void ResetPosition()
    {
        if (camera.localPosition == _startPos) return;
        camera.localPosition = Vector3.Lerp(camera.localPosition, _startPos, 1 * Time.deltaTime);
    }
}
