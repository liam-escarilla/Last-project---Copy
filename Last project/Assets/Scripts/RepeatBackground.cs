using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour // INHERITANCE: RepeatBackground class inherits from MonoBehaviour, which allows it to be attached to a Unity GameObject and use Unity lifecycle methods.
{
    private Vector3 startPos; // ENCAPSULATION: `startPos` is encapsulated to store the starting position of the background.
    private float repeatWidth; // ENCAPSULATION: `repeatWidth` is encapsulated to store the width of the background for the repeating logic.

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position; // ENCAPSULATION: The initial position of the background is stored in `startPos`.
        repeatWidth = GetComponent<BoxCollider>().size.x / 2; // ABSTRACTION: Getting the width of the background's collider is abstracted by `GetComponent<BoxCollider>().size.x`, without manually calculating the collider dimensions.
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < startPos.x - repeatWidth) // ENCAPSULATION: The logic for checking the background's position relative to `startPos` is encapsulated within this condition.
        {
            transform.position = startPos; // ABSTRACTION: Resetting the position of the background to the starting position is abstracted by `transform.position = startPos`, hiding the internal workings of Unity’s transform system.
        }
    }
}
