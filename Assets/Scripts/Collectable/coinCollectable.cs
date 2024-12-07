using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    [SerializeField] private AudioClip pickupSound;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SoundManager.instance.PlaySound(pickupSound);
            Animator animator = GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetTrigger("End");
            }

            // Notify GameManager about collectible collected
            CoinManager.Instance.CollectibleCollected();

            // Destroy collectible after animation
            Destroy(gameObject, 0.5f); // Change the time according to your animation length
        }
    }
}
