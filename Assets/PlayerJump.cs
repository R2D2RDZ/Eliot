using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] private InputActionProperty jumpButton;
    [SerializeField] private CharacterController cc;
    [SerializeField] private float jumpHeight = 3f;
    [SerializeField] private LayerMask groundLayers;
    [SerializeField] private LayerMask bounceLayers;
    [SerializeField] TMP_Text console;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private float gravity = Physics.gravity.y;
    private Vector3 movement;

    private void Update()
    {
        console.text = transform.position.ToString();
        bool _isGrounded = IsGrounded();
        bool _isBounce = IsBounce();
        if (jumpButton.action.WasPressedThisFrame())
        {
            Debug.Log("Button pressed");
            //console.text = "A Pressed";
        }
        if (jumpButton.action.WasPressedThisFrame() && _isGrounded)
        {
            Debug.Log("entro al if");
            //console.text = "Salto";
            Jump();
        }
        if (_isBounce)
        {
            console.text += "\nIsBounce";
            Bounce();
        }

        movement.y += gravity * Time.deltaTime;

        cc.Move(movement * Time.deltaTime);
    }
    private bool IsGrounded()
    {
        
        return Physics.CheckSphere(transform.position, 0.3f, groundLayers);
    }

    private bool IsBounce()
    {
        
        return Physics.CheckSphere(transform.position, 0.3f, bounceLayers);
    }

    private void Jump()
    {
        movement.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity); 
    }
    private void Bounce()
    {
        movement.y = Mathf.Sqrt(jumpHeight * -20.0f * gravity);
    }
}
