using UnityEngine;

public class EnemySideways : MonoBehaviour
{
    [SerializeField] private float horizontalMovementRange;
    [SerializeField] private float horizontalSpeed;
    [SerializeField] private float verticalMovementRange;
    [SerializeField] private float verticalSpeed;
    [SerializeField] private float damage;

    private bool movingLeft;
    private bool movingDown;
    private float leftEdge;
    private float rightEdge;
    private float topEdge;
    private float bottomEdge;

    private void Awake()
    {
        leftEdge = transform.position.x - horizontalMovementRange / 2;
        rightEdge = transform.position.x + horizontalMovementRange / 2;
        topEdge = transform.position.y + verticalMovementRange / 2;
        bottomEdge = transform.position.y - verticalMovementRange / 2;
    }

    private void Update()
    {
        MoveSideways();
        MoveVertically();
    }

    private void MoveSideways()
    {
        float horizontalMovement = horizontalSpeed * Time.deltaTime * (movingLeft ? -1 : 1);
        transform.Translate(horizontalMovement, 0, 0);

        if ((movingLeft && transform.position.x <= leftEdge) || (!movingLeft && transform.position.x >= rightEdge))
        {
            movingLeft = !movingLeft;
        }
    }

    private void MoveVertically()
    {
        float verticalMovement = verticalSpeed * Time.deltaTime * (movingDown ? -1 : 1);
        transform.Translate(0, verticalMovement, 0);

        if ((movingDown && transform.position.y <= bottomEdge) || (!movingDown && transform.position.y >= topEdge))
        {
            movingDown = !movingDown;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Health playerHealth = collision.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }
}
