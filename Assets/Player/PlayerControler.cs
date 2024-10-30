using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Playercontroller : MonoBehaviour
{
    //moveSpeed float
    //rigidbody2d.velocity
    [SerializeField]
    float moveSpeed = 100;

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
}