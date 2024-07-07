using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public event Action OnJumpPressed;
    private PlayerInputAction _playerInputAction;

    private Vector2 _inputVector;



    // Game Loop Methods---------------------------------------------------------------------------

    private void Awake()
    {
        _playerInputAction = new PlayerInputAction();
        _playerInputAction.Player.Enable();
    }

    private void Start()
    {
        _playerInputAction.Player.Jump.performed += Jump_Performed;
    }


    private void Update()
    {
        _inputVector = _playerInputAction.Player.Move.ReadValue<Vector2>();
    }

    // Signal Methods------------------------------------------------------------------------------

    private void Jump_Performed(InputAction.CallbackContext context)
    {
        OnJumpPressed?.Invoke();
    }

    // Getters and Setters-------------------------------------------------------------------------

    public Vector2 InputVectorNormalized { get => _inputVector.normalized; }
}
