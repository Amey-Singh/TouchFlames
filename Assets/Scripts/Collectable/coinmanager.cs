using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance { get; private set; }

    public int TotalCollectibles { get; private set; }
    private float startTime; // Store the start time of the current level

    public Text collectiblesText; // Reference to the UI text field to display the total collectibles
    public Text timeText; // Reference to the UI text field to display the time taken

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        startTime = Time.time; // Start counting time when the scene begins
    }

    private void Update()
    {
        UpdateUIText();
    }

    private void UpdateUIText()
    {
        if (collectiblesText != null)
        {
            collectiblesText.text = ":" + TotalCollectibles.ToString();
        }

        if (timeText != null)
        {
            // Calculate the time taken by subtracting the start time from the current time
            float currentTime = Time.time - startTime;
            timeText.text = "Time:" + currentTime.ToString("F2");
        }
    }

    public void CollectibleCollected()
    {
        TotalCollectibles++;
    }
    public int GetTotalCollectibles()
    {
        return TotalCollectibles;
    }

    // Get the time taken for the level
    public float GetTimeTaken()
    {
        // Calculate the time taken by subtracting the start time from the current time
        return Time.time - startTime;
    }
}
