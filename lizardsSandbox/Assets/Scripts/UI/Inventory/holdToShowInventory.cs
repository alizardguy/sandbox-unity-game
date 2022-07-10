using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class holdToShowInventory : MonoBehaviour
{
    //refs and vars
    [SerializeField] GameObject backpackUI;

    //input binding stuff
    public BasicControl playerControl;
    private InputAction Backpack;

    private void OnEnable()
    {
        Backpack = playerControl.Player.Backpack;
        Backpack.Enable();
    }

    private void OnDisable()
    {
        Backpack.Disable();  
    }

    private void Awake()
    {
       playerControl = new BasicControl();
    }

    void Update()
    {
        checkForHold();     
    }

    void checkForHold()
    {
        if (Backpack.ReadValue<float>() > 0.5f)
        {
            print("feet");
        }
        else
        {
            print("no feet");
        }

    }
}
