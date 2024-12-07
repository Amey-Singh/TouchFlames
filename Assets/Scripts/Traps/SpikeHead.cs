using UnityEngine;

public class Spikehead : MonoBehaviour
{
    [SerializeField] private float horizontalMovementRange;
    [SerializeField] private float horizontalSpeed;
    [SerializeField] private float verticalMovementRange;
    [SerializeField] private float verticalSpeed;
    [SerializeField] private float damage;
    [SerializeField] private float horizontalIdleTime = 1f; // Idle time after reaching the end of horizontal movement
    [SerializeField] private float verticalIdleTime = 1f;   // Idle time after reaching the end of vertical movement

    private bool movingLeft;
    private bool movingDown;
    private float leftEdge;
    private float rightEdge;
    private float topEdge;
    private float bottomEdge;
    private float horizontalIdleTimer;
    private float verticalIdleTimer;

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
        if (movingLeft)
        {
            if (transform.position.x > leftEdge)
            {
                transform.Translate(-horizontalSpeed * Time.deltaTime, 0, 0);
            }
            else
            {
                horizontalIdleTimer += Time.deltaTime;
                if (horizontalIdleTimer >= horizontalIdleTime)
                {
                    movingLeft = false;
                    horizontalIdleTimer = 0f;
                }
            }
        }
        else
        {
            if (transform.position.x < rightEdge)
            {
                transform.Translate(horizontalSpeed * Time.deltaTime, 0, 0);
            }
            else
            {
                horizontalIdleTimer += Time.deltaTime;
                if (horizontalIdleTimer >= horizontalIdleTime)
                {
                    movingLeft = true;
                    horizontalIdleTimer = 0f;
                }
            }
        }
    }

    private void MoveVertically()
    {
        if (movingDown)
        {
            if (transform.position.y > bottomEdge)
            {
                transform.Translate(0, -verticalSpeed * Time.deltaTime, 0);
            }
            else
            {
                verticalIdleTimer += Time.deltaTime;
                if (verticalIdleTimer >= verticalIdleTime)
                {
                    movingDown = false;
                    verticalIdleTimer = 0f;
                }
            }
        }
        else
        {
            if (transform.position.y < topEdge)
            {
                transform.Translate(0, verticalSpeed * Time.deltaTime, 0);
            }
            else
            {
                verticalIdleTimer += Time.deltaTime;
                if (verticalIdleTimer >= verticalIdleTime)
                {
                    movingDown = true;
                    verticalIdleTimer = 0f;
                }
            }
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
