using UnityEngine;
using TMPro;

public class RideManager : MonoBehaviour
{
    [Header("Stats")]
    public int money = 0;
    public int reputation = 0;
    [Header("Ride Quality")]
    public int rideQuality = 100;

    [Header("UI")]
    [SerializeField] TextMeshProUGUI moneyText;
    [SerializeField] TextMeshProUGUI reputationText;
    [SerializeField] TextMeshProUGUI rideStatusText;
    [SerializeField] TextMeshProUGUI rideQualityText;

    [Header("References")]
    [SerializeField] GameObject passengerPoint;
    [SerializeField] GameObject destinationPoint;

    [Header("Ride Timer")]
    [SerializeField] float rideTimeLimit = 30f;
    [SerializeField] TextMeshProUGUI rideTimerText;

    [Header("Passenger Mood")]
    public int discomfort = 0;
    [SerializeField] TextMeshProUGUI discomfortText;


    float currentRideTime = 0f;
    bool rideActive = false;
    bool passengerOnBoard = false;
    PassengerDialogue passengerDialogue;


    void Start()
    {
        passengerDialogue = FindFirstObjectByType<PassengerDialogue>();
        currentRideTime = 0f;
        rideActive = false;
        UpdateUI();
        if (rideStatusText != null)
            rideStatusText.text = "Pick up passenger";

        if (passengerPoint != null) passengerPoint.SetActive(true);
        if (destinationPoint != null) destinationPoint.SetActive(false);
    }

    void Update()
    {
        if (rideActive)
        {
            currentRideTime += Time.deltaTime;

            float remaining = rideTimeLimit - currentRideTime;
            if (remaining < 0f) remaining = 0f;

            if (rideTimerText != null)
            {
                int seconds = Mathf.CeilToInt(remaining);
                rideTimerText.text = $"Time: {seconds}s";
            }

            if (currentRideTime >= rideTimeLimit)
            {
                rideActive = false;
                reputation -= 1;
                UpdateUI();

                if (rideStatusText != null)
                    rideStatusText.text = "Late! Passenger is upset...";
            }
        }
    }


    public void OnPassengerPickedUp()
    {
        passengerOnBoard = true;
        rideQuality = 100;
        currentRideTime = 0f;
        rideActive = true;

        UpdateUI();

        if (rideStatusText != null)
            rideStatusText.text = "Drive to destination";

        if (destinationPoint != null)
            destinationPoint.SetActive(true);
    }



    public void OnDestinationReached()
    {
        if (!passengerOnBoard) return;

        passengerOnBoard = false;
        rideActive = false;

        int baseFare = 50;
        int qualityBonus = rideQuality / 10;

        bool onTime = currentRideTime <= rideTimeLimit;
        int timeBonus = onTime ? 10 : 0;

        money += baseFare + qualityBonus + timeBonus;

        if (rideQuality > 80)      reputation += 2;
        else if (rideQuality > 40) reputation += 1;
        else                       reputation -= 1;

        if (!onTime) reputation -= 1;
        if (passengerDialogue != null)
        {
            passengerDialogue.ShowDialogue("So, how was the ride? The city is crazy today, right?");
        }

        if (discomfort > 80)      reputation -= 2;
        else if (discomfort > 40) reputation -= 1;
        else                      reputation += 1;

        UpdateUI();

        if (rideStatusText != null)
            rideStatusText.text = onTime ? "Ride complete!" : "Arrived, but late...";
    }



    public void OnTaxiCollision(int penalty)
    {
        rideQuality -= penalty;
        if (rideQuality < 0) rideQuality = 0;
        discomfort += 5;
        if (discomfort > 100) discomfort = 100;
        UpdateUI();
    }

    public void StartNewRide()
    {
        passengerOnBoard = false;
        rideActive = false;
        currentRideTime = 0f;
        rideQuality = 100;

        UpdateUI();

        if (rideStatusText != null)
            rideStatusText.text = "Pick up passenger";

        Vector2 randomPos = new Vector2(Random.Range(-8f, 8f), Random.Range(-4f, 4f));
        passengerPoint.transform.position = randomPos;
        passengerPoint.SetActive(true);

        destinationPoint.SetActive(false);
    }

    public void AddDiscomfort(int amount)
    {
        discomfort += amount;
        if (discomfort > 100) discomfort = 100;
        UpdateUI();
    }

    public void OnDialogueChoice(bool polite)
    {
        if (polite)
        {
            reputation += 1;
            if (rideStatusText != null)
                rideStatusText.text = "Passenger liked your attitude.";
        }
        else
        {
            reputation -= 1;
            if (rideStatusText != null)
                rideStatusText.text = "Passenger disliked your attitude.";
        }

        UpdateUI();
        StartNewRide();
    }



    void UpdateUI()
    {
        if (moneyText != null) moneyText.text = $"Money: {money}₪";
        if (reputationText != null) reputationText.text = $"Rep: {reputation}";
        if (rideQualityText != null) rideQualityText.text = $"Quality: {rideQuality}%";
        if (discomfortText != null) discomfortText.text = $"Discomfort: {discomfort}%";
    }
}
