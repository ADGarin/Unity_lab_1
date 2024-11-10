using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 2f;

    [SerializeField]
    private Transform groundCheck; 
    [SerializeField]
    private float groundCheckDistance = 1f; 
    [SerializeField]
    private LayerMask groundLayer; 

    private bool movingRight = true;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();
        CheckForPlatform();
    }

    private void Move()
    {
        Vector2 movement = new Vector2(movingRight ? moveSpeed : -moveSpeed, rb.velocity.y);
        rb.velocity = movement;
    }

    private void CheckForPlatform()
    {
        
        RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundLayer);

        
        if (hit.collider == null)
        {
            ChangeDirection();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.collider.CompareTag("Wall"))
        {
            ChangeDirection();
        }
    }

    private void ChangeDirection()
    {
        movingRight = !movingRight;
        Flip();
    }

    private void Flip()
    {
        Vector3 enemyScale = transform.localScale;
        enemyScale.x *= -1;
        transform.localScale = enemyScale;
    }
}
