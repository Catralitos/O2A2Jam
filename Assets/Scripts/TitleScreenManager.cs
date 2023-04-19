using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// This class manages the title screen.
/// </summary>
/// <seealso cref="UnityEngine.MonoBehaviour" />
public class TitleScreenManager : MonoBehaviour
{
    /// <summary>
    /// The start game button
    /// </summary>
    [Header("Buttons")] public Button startButton;
    public Button exitButton;
        
    /// <summary>
    /// The audio manager
    /// </summary>
    private AudioManager _audioManager;
        
        
    /// <summary>
    /// Starts this instance.
    /// </summary>
    private void Start()
    {
        startButton.onClick.AddListener(StartGame);
        exitButton.onClick.AddListener(ExitGame);
        /*_audioManager = GetComponent<AudioManager>();
            _audioManager.Play("MainMenu");*/

    }
        

    /// <summary>
    /// Starts the game.
    /// </summary>
    private void StartGame()
    {
        //_audioManager.Stop("MenuMusic");
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// Exits the game.
    /// </summary>
    private static void ExitGame()
    {
        Application.Quit();
    }
}