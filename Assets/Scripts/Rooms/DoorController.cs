using UnityEngine;

public class DoorController : MonoBehaviour
{
    // Reference to the Animator component on the door
    public Animator animator;

    // The name of the boolean parameter in the Animator that controls the door state
    public string doorOpenParameter = "IsDoorOpen";

    // The name of the tag for the player character
    public string playerTag = "Player";

    // This method is called when another collider enters the trigger collider
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the other collider has the player tag
        if (other.CompareTag(playerTag))
        {
            // Set the Animator boolean parameter to true to open the door
            animator.SetBool(doorOpenParameter, true);
        }
    }

    // This method is called when another collider exits the trigger collider
    private void OnTriggerExit2D(Collider2D other)
    {
        // Check if the other collider has the player tag
        if (other.CompareTag(playerTag))
        {
            // Set the Animator boolean parameter to false to close the door
            animator.SetBool(doorOpenParameter, false);
        }
    }
}
