using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] private InputActionProperty jumpButton;
    [SerializeField] private CharacterController cc;
    [SerializeField] private float jumpHeight = 3f;
    [SerializeField] private LayerMask groundLayers;
    [SerializeField] private LayerMask bounceLayers;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private float gravity = Physics.gravity.y;
    private Vector3 movement;

    private void Update()
    {
        bool _isGrounded = IsGrounded();
        bool _isBounce = IsBounce();
        if (jumpButton.action.WasPressedThisFrame() && _isGrounded)
        {
            Jump();
        }
        if (_isBounce)
        {
            Bounce();
        }
        else
        {
            movement.y += gravity * Time.deltaTime;
        }
        cc.Move(movement * Time.deltaTime);
    }
    private bool IsGrounded()
    {
        return Physics.CheckSphere(transform.position, 0.5f, groundLayers);
    }

    private bool IsBounce()
    {
        return Physics.CheckSphere(transform.position, 0.5f, bounceLayers);
    }

    private void Jump()
    {
        movement.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity); 
    }

    private void Bounce()
    {
        movement.y = Mathf.Sqrt((jumpHeight * 50f) * -3.0f * gravity);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, 0.3f);
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 0.1f);
    }
}
