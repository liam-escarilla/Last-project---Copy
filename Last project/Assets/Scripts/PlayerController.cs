using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour // INHERITANCE: PlayerController inherits from MonoBehaviour, allowing the script to be attached to a Unity GameObject and use Unity methods.
{
    // References for components and gameplay variables
    private Rigidbody playerRb; // ENCAPSULATION: `playerRb` is a private reference to the Rigidbody, encapsulating the physics interactions.
    private Animator playerAnim; // ENCAPSULATION: `playerAnim` is a private reference to the Animator, encapsulating the animation logic.
    public ParticleSystem explosionParticle; // ABSTRACTION: Explosion particle system is abstracted to be used easily without worrying about internal implementation.
    public ParticleSystem dirtParticle; // ABSTRACTION: Dirt particle effect for jumping is abstracted and can be reused without knowing the underlying details.
    public AudioClip jumpSound; // ABSTRACTION: The jump sound is abstracted as a public variable, allowing easy configuration in the inspector.
    public AudioClip crashSound; // ABSTRACTION: The crash sound is abstracted in the same way as `jumpSound`.
    private AudioSource playerAudio; // ENCAPSULATION: `playerAudio` is a private reference to the AudioSource, encapsulating how sounds are played.
    public float jumpForce; // ENCAPSULATION: `jumpForce` is exposed to set the force applied when jumping but is encapsulated in this class to control its use.
    public float gravityModifier; // ENCAPSULATION: `gravityModifier` is used internally to adjust the global gravity, encapsulating this functionality.
    public bool isOnGround = true; // ENCAPSULATION: `isOnGround` is a private flag that tracks whether the player is grounded, keeping this state internally controlled.
    public bool gameOver = false; // ENCAPSULATION: `gameOver` is used to track the game's state, encapsulated within the `PlayerController`.
    public float horizontalInput; // ENCAPSULATION: `horizontalInput` could be used for later expansions, but it’s encapsulated in this class.
    public float speed = 10.0f; // ENCAPSULATION: `speed` is a private variable controlling how fast the player moves, keeping movement speed encapsulated.
    public float xRange = 10; // ENCAPSULATION: `xRange` defines the player's X-axis boundary, encapsulated to prevent movement beyond this range.
    public float zRange = 10; // ENCAPSULATION: `zRange` similarly controls the Z-axis boundary for the player's movement.

    private ScoreManager scoreManager; // ENCAPSULATION: `scoreManager` is encapsulated as a reference to manage the player's score and is hidden from other systems.

    void Start()
    {
        // Initialize references
        playerRb = GetComponent<Rigidbody>(); // ABSTRACTION: The internal logic of Rigidbody is abstracted, we simply use it for physics interactions.
        playerAnim = GetComponent<Animator>(); // ABSTRACTION: The Animator component is abstracted for animation control.
        Physics.gravity *= gravityModifier; // ENCAPSULATION: Gravity modification is handled internally by this script, encapsulating its effects on gameplay.
        playerAudio = GetComponent<AudioSource>(); // ABSTRACTION: The AudioSource component is abstracted for easy sound playback.

        // Find the ScoreManager in the scene
        scoreManager = FindObjectOfType<ScoreManager>(); // ABSTRACTION: The `ScoreManager` is abstracted to handle the score, without exposing its internal workings.
    }

    void Update()
    {
        // Handle X position bounds
        // Prevent player from moving out of X bounds
        if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z); // ABSTRACTION: The position is managed via transform, abstracting direct control over the GameObject's position.
        }
        if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z); // Same as above
        }

        // Handle Z movement with A and D keys
        // Move the player in Z direction based on input
        if (Input.GetKey(KeyCode.D) && !gameOver) // ENCAPSULATION: Movement logic is encapsulated within the Update method.
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 
                transform.position.z - speed * Time.deltaTime); // ABSTRACTION: Movement is handled by Unity's Transform, abstracting how it's done.
        }
        if (Input.GetKey(KeyCode.A) && !gameOver) // ENCAPSULATION: Movement logic is encapsulated within the Update method.
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 
                transform.position.z + speed * Time.deltaTime); // Same as above
        }

        // Keep Z position within bounds
        // Prevent player from moving out of Z bounds
        if (transform.position.z < -zRange)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -zRange); // ABSTRACTION: Transform controls position within bounds, abstracting the math.
        }
        if (transform.position.z > zRange)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zRange); // Same as above
        }

        // Handle jumping
        // Allow jumping only when on the ground and not in a game-over state
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // ABSTRACTION: The physics engine is abstracted away with the `AddForce` call.
            isOnGround = false; // ENCAPSULATION: The flag for the player's ground state is managed internally.
            playerAnim.SetTrigger("Jump_trig"); // ABSTRACTION: Animation is triggered via a simple method, abstracting the internal animation state machine.
            dirtParticle.Stop(); // ABSTRACTION: Particle effects are abstracted by `Stop()`, with no need to manage them directly.
            playerAudio.PlayOneShot(jumpSound, 1.0f); // ABSTRACTION: Sound playback is abstracted by `PlayOneShot`, no need to manage sound playback manually.
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Handle collision with ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true; // ENCAPSULATION: The player’s ground state is encapsulated and updated on collision.
            dirtParticle.Play(); // ABSTRACTION: Particle effects are abstracted by simply calling `Play()`.
        }
        // Handle collision with obstacles
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Game Over!"); // ENCAPSULATION: Game over state is encapsulated within this method and handled locally.
            gameOver = true; // ENCAPSULATION: Game over logic is encapsulated here.
            playerAnim.SetBool("Death_b", true); // ABSTRACTION: The animation system is abstracted, and the animation is triggered.
            playerAnim.SetInteger("Deathtype_int", 1); // ABSTRACTION: Another abstraction of the animation system.
            explosionParticle.Play(); // ABSTRACTION: Explosion particle effect is abstracted.
            dirtParticle.Stop(); // ABSTRACTION: Dirt particle effect is stopped.
            playerAudio.PlayOneShot(crashSound, 1.0f); // ABSTRACTION: The crash sound is played via `PlayOneShot`, abstracting how it's done.

            // Stop score from increasing when collided with an obstacle
            if (scoreManager != null)
            {
                scoreManager.scoreIncreasing = false; // ENCAPSULATION: The score management is encapsulated within the `ScoreManager`.
            }
        }
    }
}
