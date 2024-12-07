using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    [SerializeField] private float fallDelay = 1f;
    [SerializeField] private float fallSpeed = 10f;
    [SerializeField] private float resetDelay = 3f; // Delay before resetting the platform
    [SerializeField] private float resetSpeed = 5f; // Speed at which the platform resets
    [SerializeField] private float resetDistance = 0.1f; // Distance to reset the platform

    private Rigidbody2D rb;
    private Vector3 initialPosition;
    private bool hasFallen = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Static; // Initially set as static
        initialPosition = transform.position; // Store the initial position
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !hasFallen)
        {
            Invoke("Fall", fallDelay); // Start falling after delay
            hasFallen = true;
        }
    }

    private void Fall()
    {
        rb.bodyType = RigidbodyType2D.Dynamic; // Set as dynamic to allow falling
        rb.velocity = new Vector2(0, -fallSpeed); // Set falling velocity
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Invoke("ResetPlatform", resetDelay); // Reset platform after delay
        }
    }

    private void ResetPlatform()
    {
        rb.bodyType = RigidbodyType2D.Static; // Set as static
        rb.velocity = Vector2.zero; // Reset velocity
        transform.position = initialPosition; // Reset position

        // If the platform has moved too far from its initial position, move it back slowly
        if (Vector3.Distance(transform.position, initialPosition) > resetDistance)
        {
            float step = resetSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, initialPosition, step);
        }

        hasFallen = false; // Reset the fallen state
    }
}
