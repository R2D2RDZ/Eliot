using UnityEngine;

public class BubbleCustomGrav : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Vector3 customGravity = new Vector3(0f, -9.81f, 0f); // Default gravity direction and strength

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Disable the default gravity
        rb.useGravity = false;
    }

    void FixedUpdate()
    {
        // Apply custom gravity to the Rigidbody
        rb.AddForce(customGravity, ForceMode.Acceleration);
    }
}
