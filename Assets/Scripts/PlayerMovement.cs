using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] float speed;
    [Header("Component References")]
    Animator animator_C;
    SpriteRenderer spriteRenderer_C;
    Rigidbody2D rb2D_C;
    BoxCollider2D boxCollider2D_C;
    PlayerInput playerInput; // a class not a component
    private void Awake()
    {
        animator_C = GetComponent<Animator>();
        playerInput = new PlayerInput();
        spriteRenderer_C = GetComponent<SpriteRenderer>();
        rb2D_C = GetComponent<Rigidbody2D>();
        boxCollider2D_C = GetComponent<BoxCollider2D>();

    }


    private void Start()
    {
        // Enable player input first
        playerInput.Player.Enable();

    }
    #region Subscribe/Unsubscribe to Event and Methods
    void OnEnable()
    {
        playerInput.Player.Hit.performed += OnHit;
        playerInput.Player.Hit.canceled += OnUnHit;
    }
    private void OnDisable()
    {
        playerInput.Player.Hit.performed -= OnHit;
        playerInput.Player.Hit.canceled -= OnUnHit;
    }
    private void OnHit(InputAction.CallbackContext context)
    {
        animator_C.SetBool("hit", true);
    }
    private void OnUnHit(InputAction.CallbackContext context)
    {
        animator_C.SetBool("hit", false);
    }
    #endregion
    private void Update()
    {
        OnRun();
    }
    void OnRun()
    {
        Vector2 velocity = playerInput.Player.Move.ReadValue<Vector2>();
        Debug.Log(velocity);
        bool isRunning = Mathf.Abs(velocity.x) > 0;
        if (isRunning)
        {
            animator_C.SetBool("isRunning", true);
            transform.localScale = new Vector3(Mathf.Sign(velocity.x), 1, 1);
        }
        else
        {
            animator_C.SetBool("isRunning", false);
        }

        rb2D_C.velocity = velocity * speed;
    }


}
