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

    [SerializeField] private float gravity = Physics.gravity.y;
    private Vector3 movement;
    float radius = 0.05f;

    private void Update()
    {

        console.text = transform.position.ToString();
        console.text += "\n" + movement;
        bool _isGrounded = IsGrounded();
        bool _isBounce = IsBounce();
        if (!_isGrounded || !_isBounce)
        {
            movement.y += gravity * Time.deltaTime;
        }
        if (_isGrounded)
        {
            console.text += "\nIsGrounded";
            movement = Vector3.down;
        }
        if (_isBounce)
        {
            console.text += "\nIsBounce";
            _isGrounded = true;
            Bounce();
        }
        if (jumpButton.action.WasPressedThisFrame())
        {
            Debug.Log("Button pressed");
            if (_isGrounded)
            {
                Debug.Log("entro al if");
                //console.text = "Salto";
                Jump();
            }
            //console.text = "A Pressed";
        }

        


        cc.Move(movement * Time.deltaTime);

        
    }
    private bool IsGrounded()
    {
        
        return Physics.CheckSphere(transform.position, radius, groundLayers);
    }

    private bool IsBounce()
    {
        
        return Physics.CheckSphere(transform.position, radius, bounceLayers);
    }

    private void Jump()
    {
        movement.y = jumpHeight * -1.0f * gravity; 
    }
    private void Bounce()
    {
        movement.y = jumpHeight * -2.5f * gravity;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position,radius);
    }
}
