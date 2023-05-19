using UnityEngine;

public class CustomGravity : MonoBehaviour
{
    public float gravity = 9.8f;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    private void Update()
    {
        Vector3 customGravity = -transform.up * gravity;
        rb.AddForce(customGravity / Time.fixedDeltaTime * Time.deltaTime, ForceMode.Acceleration);
    }
}