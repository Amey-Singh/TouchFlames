using UnityEngine;
using UnityEngine.UI;

public class EndMenu : MonoBehaviour
{
    public Text collectiblesText; // Reference to the UI text field to display the total collectibles
    public Text timeText; // Reference to the UI text field to display the time taken
    public Button quitButton; // Reference to the quit button

    private void Start()
    {
        // Display total collectibles and time taken
        DisplayStats();

        // Add listener to the quit button
        if (quitButton != null)
        {
            quitButton.onClick.AddListener(QuitGame);
        }
    }

    private void DisplayStats()
    {
        if (CoinManager.Instance != null)
        {
            int totalCollectibles = CoinManager.Instance.GetTotalCollectibles();
            float timeTaken = CoinManager.Instance.GetTimeTaken();

            if (collectiblesText != null)
            {
                collectiblesText.text = "Collectibles: " + totalCollectibles.ToString();
            }

            if (timeText != null)
            {
                timeText.text = "Completion Time: " + timeTaken.ToString("F2") + " sec";
            }
        }
        else
        {
            Debug.LogWarning("CoinManager is not found!");
        }
    }

    private void QuitGame()
    {
        // Quit the application
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; //Exits play mode (will only be executed in the editor)
#endif
    }
}
