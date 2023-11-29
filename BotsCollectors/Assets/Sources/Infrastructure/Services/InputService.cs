using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputService : MonoBehaviour
{
    private const string Vertical = "Vertical";
    private const string Horizontal = "Horizontal";
    private const string Multiplier = "Multiplier";
    private const string MouseScrollWheel = "Mouse ScrollWheel";

    public event Action<bool, bool> RotationAxis;
    public event Action<Vector2> MovementAxis;
    public event Action<float> MultiplayerAxis;
    public event Action<float> ScrollWheelAxis;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    void Update()
    {
        UpdateRotation();
        UpdateMovementAxis();
        UpdateMultiplayerAxis();
        UpdateScrollWheelAxis();
    }

    private void UpdateRotation()
    {
        bool isLeftRotation = Input.GetKey(KeyCode.Q);
        bool isRightRotation = Input.GetKey(KeyCode.E);
            
        RotationAxis?.Invoke(isLeftRotation, isRightRotation);
    }

    private void UpdateMovementAxis()
    {
        float horizontalInput = Input.GetAxis(Horizontal);
        float verticalInput = Input.GetAxis(Vertical);
        Vector2 movementDirection = new Vector2(horizontalInput, verticalInput);
            
        MovementAxis?.Invoke(movementDirection);
    }
    
    private void UpdateMultiplayerAxis()
    {
        float multiplier = Input.GetAxis(Multiplier);
            
        MultiplayerAxis?.Invoke(multiplier);
    }

    private void UpdateScrollWheelAxis()
    {
        float scrollWheelInput = Input.GetAxis(MouseScrollWheel);
        
        ScrollWheelAxis?.Invoke(scrollWheelInput);
    }
}
