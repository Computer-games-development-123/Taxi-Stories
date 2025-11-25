using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class TaxiCollisionHandler : MonoBehaviour
{
    [Header("Ride Quality")]
    [SerializeField] int collisionPenalty = 1;

    RideManager rideManager;

    void Awake()
    {
        rideManager = FindFirstObjectByType<RideManager>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // רק קירות/מכשולים מסומנים כ-Obstacle
        if (collision.collider.CompareTag("Obstacle") && rideManager != null)
        {
            rideManager.OnTaxiCollision(collisionPenalty);
        }
    }
}
