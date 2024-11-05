using UnityEngine;
using UnityEngine.UI;

class GameManager : MonoBehaviour // INHERITANCE: GameManager class inherits from MonoBehaviour (base class in Unity)
{
    // References to UI buttons for starting and restarting the game
    public Button startButton;
    public Button restartButton; // New restart button

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        Time.timeScale = 0f; // Freeze the game time at startup
        restartButton.gameObject.SetActive(false); // Hide the restart button initially
    }

    // OnEnable is called when the object becomes enabled and active
    private void OnEnable()
    {
        // Add listeners to buttons for handling click events
        startButton.onClick.AddListener(StartGame);
        restartButton.onClick.AddListener(RestartGame); // Add listener for the restart button
    }

    // OnDisable is called when the behaviour becomes disabled
    private void OnDisable()
    {
        // Remove listeners to prevent memory leaks
        startButton.onClick.RemoveListener(StartGame);
        restartButton.onClick.RemoveListener(RestartGame); // Remove listener for the restart button
    }

    // Method to start the game
    private void StartGame()
    {
        Time.timeScale = 1f; // Resume game time
        startButton.gameObject.SetActive(false); // Hide the start button
    }

    // Method called when the game is over
    private void GameOver()
    {
        Time.timeScale = 0f; // Pause the game
        restartButton.gameObject.SetActive(true); // Show the restart button
    }

    // Method to restart the game
    private void RestartGame()
    {
        Time.timeScale = 1f; // Resume the game time
        restartButton.gameObject.SetActive(false); // Hide the restart button

        // Reset game state here (e.g., reset score, player position, etc.)
        // This would typically call methods on other components or scripts.
    }

    // Call this method when the game is over
    public void TriggerGameOver() // ABSTRACTION: The details of how game over is handled are hidden from the user of this method.
    {
        GameOver(); // Call the GameOver method, which encapsulates the logic of handling a game over.
    }
}
