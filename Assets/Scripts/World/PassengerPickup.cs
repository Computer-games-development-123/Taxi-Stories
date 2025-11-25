using UnityEngine;

public class PassengerPickup : MonoBehaviour
{
    bool pickedUp = false;

    [SerializeField] RideManager rideManager;

    void Start()
    {
        if (rideManager == null)
        {
            rideManager = FindFirstObjectByType<RideManager>();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (pickedUp) return;

        if (other.CompareTag("Player"))
        {
            pickedUp = true;
            rideManager.OnPassengerPickedUp();
            gameObject.SetActive(false);
        }
    }

    public void ResetPickup()
    {
        pickedUp = false;
        gameObject.SetActive(true);
    }

}
