using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour // INHERITANCE: MoveLeft class inherits from MonoBehaviour (base class for Unity scripts)
{
    private float speed = 30; // ENCLOSURE: `speed` is encapsulated as a private variable to manage the speed of the object's movement
    private PlayerController playerControllerScript; // ENCAPSULATION: `playerControllerScript` is encapsulating access to the PlayerController component of the Player GameObject
    private float leftBound = -45; // ENCAPSULATION: `leftBound` is encapsulated to specify the limit for removing objects

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>(); // ABSTRACTION: Finding and getting the PlayerController script from the "Player" GameObject abstracts away the internal logic of that component.
    }

    // Update is called once per frame
    void Update()
    {
        if(playerControllerScript.gameOver == false) // ENCAPSULATION: The condition `gameOver` is part of `PlayerController` and is encapsulated to control movement based on game state.
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed); // ABSTRACTION: The internal translation logic of the object is abstracted through `transform.Translate`.
        }

        if(transform.position.x < leftBound &&  gameObject.CompareTag("Obstacle")) // ENCAPSULATION: Object's position and its comparison with `leftBound` are encapsulated for removing it when needed.
        {
            Destroy(gameObject); // ABSTRACTION: Destroying the object is abstracted through a simple method call, no need to know the implementation details.
        }
    }
}
