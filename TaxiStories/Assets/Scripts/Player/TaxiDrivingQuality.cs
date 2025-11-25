using UnityEngine;

public class TaxiDrivingQuality : MonoBehaviour
{
    [SerializeField] float accelerationThreshold = 8f;
    RideManager rideManager;
    Rigidbody2D rb;

    Vector2 lastVelocity;

    void Awake()
    {
        rideManager = FindFirstObjectByType<RideManager>();
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Vector2 acceleration = (rb.linearVelocity - lastVelocity) / Time.fixedDeltaTime;

        if (acceleration.magnitude > accelerationThreshold)
        {
            rideManager.AddDiscomfort(2);
        }

        lastVelocity = rb.linearVelocity;
    }
}
