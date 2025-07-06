using UnityEngine;

public class BrakeScript : MonoBehaviour
{
    public float brakeForce = 5f; // Adjust brake intensity
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.B))
        {
            rb.velocity *= (1 - brakeForce * Time.deltaTime);
        }
    }
}
