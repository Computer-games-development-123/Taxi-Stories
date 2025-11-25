using UnityEngine;
using TMPro;

public class PassengerDialogue : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] TextMeshProUGUI passengerText;

    RideManager rideManager;

    void Awake()
    {
        rideManager = FindFirstObjectByType<RideManager>();
    }

    void Start()
    {
        if (panel != null)
            panel.SetActive(false);
    }

    public void ShowDialogue(string line)
    {
        if (panel != null)
            panel.SetActive(true);

        if (passengerText != null)
            passengerText.text = line;
    }

    public void OnPoliteAnswer()
    {
        rideManager.OnDialogueChoice(polite: true);
        Close();
    }

    public void OnRudeAnswer()
    {
        rideManager.OnDialogueChoice(polite: false);
        Close();
    }

    void Close()
    {
        if (panel != null)
            panel.SetActive(false);
    }
}
