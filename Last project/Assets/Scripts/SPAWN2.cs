using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game1 : MonoBehaviour
{
    // Array to hold different obstacle prefabs that can be spawned
    public GameObject[] obstaclePrefabs; 
    private Vector3 spawnPos = new Vector3(25, 0, -8); // Position where obstacles will spawn
    private float startDelay = 1; // Delay before the first obstacle spawns
    private float repeatRate = 2; // Rate at which obstacles will spawn (every 2 seconds)
    private PlayerController playerControllerScript; // Reference to the PlayerController script

    // Start is called before the first frame update
    void Start()
    {
        // Start the obstacle spawning process after a delay, repeating at the specified rate
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);

        // Find the Player GameObject and get its PlayerController component for game state management
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        // This method is currently empty; you can add functionality here if needed
    }

    // Method to spawn an obstacle
    void SpawnObstacle()
    {
        // Check if the game is still ongoing (not over) before spawning
        if (playerControllerScript.gameOver == false)
        {
            // Randomly select a prefab from the obstaclePrefabs array
            int randomIndex = Random.Range(0, obstaclePrefabs.Length);
            GameObject obstaclePrefab = obstaclePrefabs[randomIndex];

            // Instantiate the selected obstacle at the spawn position with its rotation
            Instantiate(obstaclePrefab, spawnPos, obstaclePrefab.transform.rotation);
            Debug.Log("Obstacle Spawned at: " + spawnPos); // Log the position of the spawned obstacle
        }
    }
}
