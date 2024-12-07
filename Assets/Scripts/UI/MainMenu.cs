using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Slider volumeSlider;
    public Button playButton;
    public Button quitButton;

    void Start()
    {
        // Add listeners to the buttons
        playButton.onClick.AddListener(PlayGame);
        quitButton.onClick.AddListener(QuitGame);

        // Set initial volume value
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume", 1f);
        // Add listener to the volume slider
        volumeSlider.onValueChanged.AddListener(ChangeVolume);
    }

    void PlayGame()
    {
        // Load the next scene (replace "NextScene" with the name of your scene)
        SceneManager.LoadScene(1);
    }

    void QuitGame()
    {
        // Quit the application
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; //Exits play mode (will only be executed in the editor)
#endif
    }

    void ChangeVolume(float value)
    {
        // Map the slider value to the desired volume range (0 to 1)
        float volume = Mathf.Lerp(0f, 1f, value);

        // Change the music volume using SoundManager
        SoundManager.instance.ChangeMusicVolume(volume);
    }

}
