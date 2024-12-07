using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    private float direction;
    private bool hit;
    private float lifetime;

    private Animator anim;
    private BoxCollider2D boxCollider;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (hit) return;
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);

        lifetime += Time.deltaTime;
        if (lifetime > 5) gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collider belongs to the RoomColliders layer
        if (collision.gameObject.layer == LayerMask.NameToLayer("RoomColliders"))
        {
            // Ignore collision with room colliders
            return;
        }

        hit = true;
        boxCollider.enabled = false;
        anim.SetTrigger("explode");

        if (collision.CompareTag("Enemy"))
            collision.GetComponent<Health>()?.TakeDamage(1);
    }

    public void SetDirection(float playerFacingDirection)
    {
        lifetime = 0;
        direction = playerFacingDirection; // Use the player's facing direction
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;

        // Flip the sprite based on the direction
        if (direction < 0)
        {
            // If the direction is negative, flip the sprite
            transform.localScale = new Vector3(-1f, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            // If the direction is positive (or zero), ensure the sprite is not flipped
            transform.localScale = new Vector3(1f, transform.localScale.y, transform.localScale.z);
        }
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
