using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro; // ABSTRACTION: Using TextMeshPro abstracts away the lower-level details of handling text rendering.

public class ScoreManager : MonoBehaviour // INHERITANCE: ScoreManager inherits from MonoBehaviour, which allows it to function as a Unity component attached to a GameObject.
{
    // References to the UI elements for displaying the score and high score
    public TextMeshProUGUI scoreText; // ENCAPSULATION: `scoreText` is a public reference to the UI element that displays the score, but its internal management is handled by the `ScoreManager`.
    public TextMeshProUGUI highScoreText; // ENCAPSULATION: Same as `scoreText`, this reference to the UI for the high score is managed within the class.

    // Variables to hold the current score and the high score
    public float scoreCount; // ENCAPSULATION: `scoreCount` is the current score, encapsulated within this class.
    public float highScoreCount; // ENCAPSULATION: `highScoreCount` stores the highest score, encapsulated within this class.

    // Points awarded per second
    public float pointsPerSecond; // ENCAPSULATION: `pointsPerSecond` is encapsulated to control how fast the score increases.

    // Boolean to control whether the score is increasing
    public bool scoreIncreasing; // ENCAPSULATION: `scoreIncreasing` determines if the score should keep increasing, encapsulated for internal control.

    // Start is called before the first frame update
    void Start()
    {
        // Retrieve the saved high score from PlayerPrefs, defaulting to 0 if not found
        highScoreCount = PlayerPrefs.GetFloat("HighScore", 0); // ABSTRACTION: `PlayerPrefs` abstracts the complexity of saving and loading player preferences.
        scoreCount = 0; // Initialize current score to 0
        pointsPerSecond = 10f; // Set how many points are earned per second
        scoreIncreasing = true; // Start increasing the score
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the score should be increasing
        if (scoreIncreasing)
        {
            // Increase the score based on points per second and elapsed time
            scoreCount += pointsPerSecond * Time.deltaTime; // ABSTRACTION: `Time.deltaTime` is used here to handle frame rate independence, abstracting the time calculation.

            // Check if the current score exceeds the high score
            if (scoreCount > highScoreCount)
            {
                highScoreCount = scoreCount; // Update high score
                PlayerPrefs.SetFloat("HighScore", highScoreCount); // ABSTRACTION: `PlayerPrefs.SetFloat` abstracts the saving mechanism for the high score.
            }
        }

        // Update the UI text elements with the current score and high score
        scoreText.text = "Score: " + Mathf.Round(scoreCount); // ABSTRACTION: `Mathf.Round` abstracts the rounding of the score to make it more user-friendly.
        highScoreText.text = "High Score: " + Mathf.Round(highScoreCount); // ABSTRACTION: Same for `highScoreText`, displaying the rounded high score.
    }
}
