using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public bool isGameActive;
    public Button restartButton;
    public GameObject titleScreen;



    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update(){

    }
    

        public void GameOver() 
        {
            restartButton.gameObject.SetActive(true);
            gameOverText.gameObject.SetActive(true); 
            isGameActive = false;
        }


        public void RestartGame() { 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
        }

}

