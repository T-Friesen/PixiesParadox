using UnityEngine;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    public TMP_Text pickupCountText;             // Text component to display the pickup count
    private int pickupCount = 0;             // Current pickup count

    private void Start()
    {
        // Initialize the UI with the initial pickup count
        UpdatePickupCountUI();
    }

    public void AddPickupCount(int amount)
    {
        pickupCount += amount;
        UpdatePickupCountUI();
    }

    private void UpdatePickupCountUI()
    {
        // pickupCountText.text = "Pickup Count: " + pickupCount.ToString();
        pickupCountText.text = pickupCount.ToString();
    }
}
