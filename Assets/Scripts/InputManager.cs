using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    public PlayerInput.OnFootActions onFoot;   
    private PlayerMotor motor;
    private PlayerLook look;

    // Start is called before the first frame update
    void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;
        motor = GetComponent<PlayerMotor>();
        
        // anytime onFoot.Jump is performed, we're using a callback context (ctx) to call our motor.Jump() function
        // possible states include performed, started, and canceled
        onFoot.Jump.performed += ctx => motor.Jump();

        look = GetComponent<PlayerLook>();

    }

    // Update is called once per frame
    void Update() {
        //to lock in the center of window
        Cursor.lockState = CursorLockMode.Locked;
        //to hide the cursor
        Cursor.visible = false;
    }

    void FixedUpdate()
    {
        // tell the PlayerMotor to move using the value from our player action
        motor.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
    }

    void LateUpdate() 
    {
        look.ProcessLook(onFoot.Look.ReadValue<Vector2>());
    }

    private void OnEnable() {
        onFoot.Enable();
    }

    private void OnDisable() {
        onFoot.Disable();
    }
}
