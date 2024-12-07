using UnityEngine;

public class OscillatingPlatform : MonoBehaviour
{
    public Transform startPos; // Starting position of the platform
    public Transform endPos; // Ending position of the platform
    public float moveSpeed = 2f; // Speed at which the platform moves

    private Vector3 targetPos; // The position the platform is currently moving towards

    // Reference to the player GameObject
    private GameObject player;

    void Start()
    {
        // Set the initial target position to the end position
        targetPos = endPos.position;

        // Find the player GameObject by tag
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        // Move the platform towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

        // If the platform reaches the target position, swap the target position to the other end
        if (transform.position == targetPos)
        {
            if (targetPos == startPos.position)
            {
                targetPos = endPos.position;
            }
            else
            {
                targetPos = startPos.position;
            }
        }
    }

    // Visualize the platform's movement range in the Unity editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(startPos.position, endPos.position);
    }

    // When the player stays on the platform, parent the player to the platform
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.transform.SetParent(transform);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null);
    }
}
