using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour
{
    [SerializeField, Tooltip("The mouse sensitivity")]
    private float _mouseSensitivity;

    [SerializeField, Tooltip("Reference to the player view camera")]
    private Camera _playerView;

    const float MIN_LOOK_ANGLE = -80.0f;
    const float MAX_LOOK_ANGLE = 80.0f;


    private float _mouseX;
    private float _mouseY;




    // Game Loop Methods---------------------------------------------------------------------------
    private void Update()
    {
        var mouseDelta = Mouse.current.delta.ReadValue();

        _mouseX = mouseDelta.x * _mouseSensitivity * Time.deltaTime;
        _mouseY = mouseDelta.y * _mouseSensitivity * Time.deltaTime;

        transform.Rotate(Vector3.up * _mouseX);

        // the minus 1 because the look direction is inverted
        _mouseY = -1 * _mouseY;
        _mouseY = Mathf.Clamp(_mouseY, MIN_LOOK_ANGLE, MAX_LOOK_ANGLE);
        // _playerView.transform.Rotate(Vector3.right, _mouseY);
        
        // Clampan

        if (_playerView.transform.localEulerAngles.x <= MIN_LOOK_ANGLE)
        {
            _playerView.transform.localEulerAngles = new Vector3(360 - MIN_LOOK_ANGLE, 0, 0);
        }
        // else if (_playerView.transform.localEulerAngles.x <= MAX_LOOK_ANGLE && _playerView.transform.localEulerAngles.x > 0)
        // {
        //     _playerView.transform.localEulerAngles = new Vector3(MAX_LOOK_ANGLE, 0, 0);
        // }
        else
        {
            // _playerView.transform.localEulerAngles += new Vector3(_mouseY, 0, 0);
            _playerView.transform.Rotate(Vector3.right, _mouseY);
        }

        Debug.Log(_playerView.transform.localEulerAngles.x);
        
        // _playerView.transform.localEulerAngles = new Vector3(MIN_LOOK_ANGLE, 0, 0);
        // _playerView.transform.localEulerAngles += new Vector3(MAX_LOOK_ANGLE, 0, 0);
    }
}
