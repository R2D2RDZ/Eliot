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
    [SerializeField] private TMP_Text console;

    [SerializeField] private float gravity = -9.81f;
    private Vector3 movement;
    private float radius = 0.3f;

    private void Update()
    {
        console.text = transform.position.ToString();
        console.text += "\n" + movement;

        bool isGrounded = IsGrounded();
        bool isBounce = IsBounce();

        // Gravedad
        if (!isGrounded && !isBounce)
        {
            movement.y += gravity * Time.deltaTime;
        }

        // Contacto con el suelo
        if (isGrounded)
        {
            console.text += "\nIsGrounded";
            movement.y = -1f; // Mantener contacto con el suelo.
        }

        // Rebote
        if (isBounce)
        {
            console.text += "\nIsBounce";
            Bounce();
        }

        // Salto
        if (jumpButton.action.WasPressedThisFrame() && isGrounded)
        {
            Debug.Log("Button pressed");
            Jump();
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
        movement.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }

    private void Bounce()
    {
        movement.y = Mathf.Sqrt(jumpHeight * -2.5f * gravity);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, radius);
    }
}
