using UnityEngine;

public class DestinationPoint : MonoBehaviour
{
    [SerializeField] RideManager rideManager;

    void Start()
    {
        if (rideManager == null)
        {
            rideManager = FindFirstObjectByType<RideManager>();
        }

        // start hidden until passenger picked up
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            rideManager.OnDestinationReached();
        }
    }
}
