using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 100;
    [SerializeField] float jumpImpulse = 5;

    Rigidbody2D rb;
    CollisionTouchCheck colTouchCheck;
    SpriteRenderer spriteRenderer;

    

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        colTouchCheck = GetComponent<CollisionTouchCheck>();
        spriteRenderer = GetComponent<SpriteRenderer>(); 
        
    }

    Vector2 moveInput;

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        if (moveInput.x > 0)
        {
            spriteRenderer.flipX = false; 
        }
        else if (moveInput.x < 0)
        {
            spriteRenderer.flipX = true; 
        }
    }

    void Update()
    {
        if (transform.position.y <= -10f)
        {
            RestartLevel();
        }

        if (Input.GetButtonDown("Cancel"))
        {
            SceneManager.LoadScene(0);  
        }

    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput.x * moveSpeed * Time.fixedDeltaTime, rb.velocity.y);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && colTouchCheck.IsGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + jumpImpulse);
        }
        else if (context.canceled)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.3f);
        }
    }

    void RestartLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void Die()
    {
        Debug.Log("Player died!");
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            Die();
        }
    }
}
