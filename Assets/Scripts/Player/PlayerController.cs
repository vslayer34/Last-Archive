using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class PlayerController : MonoBehaviour
{
    [field: Header("Required Components")]
    [field: SerializeField, Tooltip("Reference to the input manager")]
    public InputManager InputManager { get; private set; }


    [Space(10), SerializeField, Tooltip("Player movement speed")]
    private float _speed = 10.0f;
    private CharacterController _characterController;
    private Vector3 _moveDirection;

    // Gravity and jumping
    private const float GRAVITY = -9.8f;
    private const float GROUND_CHECK_SPHERE_RADIUS = 0.1f;
    private bool _isGrounded;

    [SerializeField, Tooltip("Layer mask for hard surfaces")]
    private LayerMask _groundMask;

    private Vector3 _verticalVelocity;

    [SerializeField, Tooltip("The jump force")]
    private float _jumpForce = 5.0f;

    private bool _isJumping;



    // Game Loop Methods---------------------------------------------------------------------------

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();

        InputManager.OnJumpPressed += InputManager_OnJumpPressed;
    }

    private void Update()
    {
        IsGrounded = Physics.CheckSphere(transform.position, GROUND_CHECK_SPHERE_RADIUS, _groundMask);

        _moveDirection = transform.forward * InputManager.InputVectorNormalized.y + transform.right * InputManager.InputVectorNormalized.x;

        if (!IsGrounded)
        {
            _verticalVelocity.y += GRAVITY * Time.deltaTime; 
        }
        else
        {
            // _verticalVelocity.y = 0.0f;
        }

        // Debug.Log(_moveDirection);

        _characterController.Move(_moveDirection * _speed * Time.deltaTime + _verticalVelocity);
        // Debug.Log(_verticalVelocity.y);
        Debug.Log(_isGrounded);
    }

    // Signal Methods------------------------------------------------------------------------------

    private void InputManager_OnJumpPressed()
    {
        _isJumping = true;
        Debug.Log(_isGrounded);

        if (IsGrounded)
        {
            _verticalVelocity.y = _jumpForce;
            _isJumping = false;
        }
    }

    // Getters & Setters---------------------------------------------------------------------------

    public bool IsGrounded
    {
        get => _isGrounded;
        set
        {
            if (value == true && !_isJumping)
            {
                _verticalVelocity.y = 0;
                _isGrounded = value;
            }
        }
    }
}
