
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BouncyBall : MonoBehaviour
{
    public float launchImpulse = 5f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.AddForce(
            new Vector3(1.2f, launchImpulse, 0.5f),
            ForceMode.Impulse
        );
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > 1f)
        {
            Debug.Log("Ball collided with: " + collision.gameObject.name);
        }
    }
}
