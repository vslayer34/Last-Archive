using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [field: Header("Required Components")]
    [field: SerializeField, Tooltip("Reference to the input manager")]
    public InputManager InputManager { get; private set; }


    [Space(10), SerializeField, Tooltip("Player movement speed")]
    private float _speed = 10.0f;
    private CharacterController _characterController;
    private Vector3 _moveDirection;



    // Game Loop Methods---------------------------------------------------------------------------

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        _moveDirection = transform.forward * InputManager.InputVectorNormalized.y + transform.right * InputManager.InputVectorNormalized.x;

        _characterController.Move(_moveDirection * _speed * Time.deltaTime);
    }
}
