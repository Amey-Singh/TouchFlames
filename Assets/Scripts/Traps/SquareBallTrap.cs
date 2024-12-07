using UnityEngine;

public class SquareBallTrap : MonoBehaviour
{
    [SerializeField] private float squareSize = 5f;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float damage = 1f;
    [SerializeField] private float idleTime = 1f; // Idle time at each corner and when changing direction

    private Vector2[] squarePoints;
    private int currentIndex = 0;
    private bool movingForward = true;
    private bool atCorner = false;
    private bool changingDirection = false;
    private float idleTimer = 0f;

    private void Start()
    {
        InitializeSquarePoints();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (squarePoints.Length < 2)
            return;

        Vector2 targetPosition = squarePoints[currentIndex];
        Vector2 newPosition = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        transform.position = newPosition;

        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            if (movingForward)
            {
                currentIndex++;
                if (currentIndex >= squarePoints.Length)
                {
                    currentIndex = squarePoints.Length - 1;
                    movingForward = false;
                    atCorner = true;
                    idleTimer = idleTime;
                }
            }
            else
            {
                currentIndex--;
                if (currentIndex < 0)
                {
                    currentIndex = 0;
                    movingForward = true;
                    atCorner = true;
                    idleTimer = idleTime;
                }
            }
        }

        if (atCorner || changingDirection)
        {
            idleTimer -= Time.deltaTime;
            if (idleTimer <= 0)
            {
                idleTimer = 0;
                atCorner = false;
                changingDirection = false;
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

    private void InitializeSquarePoints()
    {
        Vector2 startPos = transform.position;
        Vector2 endPos = startPos + Vector2.right * squareSize;
        Vector2 topPos = endPos + Vector2.up * squareSize;
        Vector2 bottomPos = startPos + Vector2.up * squareSize;

        squarePoints = new Vector2[] { startPos, endPos, topPos, bottomPos, startPos };
    }
}
