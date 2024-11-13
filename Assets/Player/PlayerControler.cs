using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Playercontroller : MonoBehaviour
{
    //moveSpeed float
    //rigidbody2d.velocity
    [SerializeField] float moveSpeed = 100;
    [SerializeField] float teleportDistance = 5f;
    GameObject firePrefab; 


    Rigidbody2D rb;
    CollisionTouchCheck colTouchCheck;
    SpriteRenderer spriteRenderer;



    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        colTouchCheck = GetComponent<CollisionTouchCheck>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // Получаем компонент SpriteRenderer
    }
    Vector2 moveInput;
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        // Изменяем направление спрайта в зависимости от направления движения
        if (moveInput.x > 0)
        {
            spriteRenderer.flipX = false; // Смотрит вправо
        }
        else if (moveInput.x < 0)
        {
            spriteRenderer.flipX = true; // Смотрит влево
        }

    }
    void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput.x * moveSpeed * Time.fixedDeltaTime, rb.velocity.y);
    }
    [SerializeField]
    float jumpImpulse = 20;
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (colTouchCheck.IsGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + jumpImpulse);
            }
        }
        else
        {
            if (context.canceled)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.3f);
            }
        }
    }

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

