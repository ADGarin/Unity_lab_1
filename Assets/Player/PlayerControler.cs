using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
<<<<<<< HEAD
    //moveSpeed float
    //rigidbody2d.velocity
    [SerializeField] float moveSpeed = 100;
    [SerializeField] float teleportDistance = 5f;
    GameObject firePrefab; 

=======
    [SerializeField] float moveSpeed = 100;
    [SerializeField] float jumpImpulse = 5;
>>>>>>> a026a88ec3a97a3c941f1b5cbffd1047998c587e

    Rigidbody2D rb;
    CollisionTouchCheck colTouchCheck;
    SpriteRenderer spriteRenderer;

<<<<<<< HEAD

=======
    
>>>>>>> a026a88ec3a97a3c941f1b5cbffd1047998c587e

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

<<<<<<< HEAD
    }
=======
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

>>>>>>> a026a88ec3a97a3c941f1b5cbffd1047998c587e
    void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput.x * moveSpeed * Time.fixedDeltaTime, rb.velocity.y);
    }
<<<<<<< HEAD
    [SerializeField]
    float jumpImpulse = 20;
=======

>>>>>>> a026a88ec3a97a3c941f1b5cbffd1047998c587e
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

<<<<<<< HEAD
    public void OnTeleport(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            StartCoroutine(Teleport());
        }
    }

    private IEnumerator Teleport()
    {

       
        Vector2 newPosition = (Vector2)transform.position + new Vector2(teleportDistance * (spriteRenderer.flipX ? -1 : 1), 0);
        if (!Physics2D.OverlapCircle(newPosition, 0.1f)) // используйте подходящий радиус
        {
            firePrefab = Resources.Load<GameObject>("FireEffect");
            GameObject Fire = Instantiate(firePrefab, transform.position, Quaternion.identity);
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(0.2f);
            transform.position = newPosition;
            spriteRenderer.enabled = true;
            GameObject Fire1 = Instantiate(firePrefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(1f);
            Destroy(Fire);
            Destroy(Fire1);

        }
        else
        {
            Debug.Log("Телепортация заблокирована: препятствие");
        }
    }
}

=======
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
>>>>>>> a026a88ec3a97a3c941f1b5cbffd1047998c587e
