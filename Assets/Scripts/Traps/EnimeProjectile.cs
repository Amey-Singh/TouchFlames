using UnityEngine;

public class EnemyProjectile : EnemyDamage
{
    [SerializeField] private float speed;
    [SerializeField] private float resetTime;
    private float lifetime;

    public void ActivateProjectile()
    {
        lifetime = 0;
        gameObject.SetActive(true);
    }

    private void Update()
    {
        float movementSpeed = speed * Time.deltaTime;
        transform.Translate(movementSpeed, 0, 0);

        lifetime += Time.deltaTime;
        if (lifetime > resetTime)
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collider belongs to the RoomColliders layer
        if (collision.gameObject.layer == LayerMask.NameToLayer("RoomColliders"))
        {
            // Ignore collision with room colliders
            return;
        }

        // Execute logic from parent script first
        base.OnTriggerEnter2D(collision);

        // Deactivate arrow when it hits any object except room colliders
        gameObject.SetActive(false);
    }
}
