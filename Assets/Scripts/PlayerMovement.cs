using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] float speed;
    [SerializeField] float range;
    [SerializeField] GameObject Gun;
    [SerializeField] Camera cam;
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
        playerInput.Player.Shoot.performed += OnShoot;
    }
    private void OnDisable()
    {
        playerInput.Player.Hit.performed -= OnHit;
        playerInput.Player.Hit.canceled -= OnUnHit;
        playerInput.Player.Shoot.performed -= OnShoot;
    }

    private void OnShoot(InputAction.CallbackContext obj)
    {
        RaycastHit2D raycastHit2D = Physics2D.Raycast(Gun.transform.position, cam.ScreenToWorldPoint(Mouse.current.position.ReadValue()).normalized, range, LayerMask.GetMask("Enemy")); // store info about target
        Debug.DrawLine(Gun.transform.position + new Vector3(0, 0, 200), cam.ScreenToWorldPoint(Mouse.current.position.ReadValue()), Color.white, 0.1f);
        Debug.Log("ff");

        if (raycastHit2D)
        {
            if (raycastHit2D.transform.gameObject.layer.Equals(LayerMask.NameToLayer("Enemy")))
            {
                // Destroy enemy
                Destroy(raycastHit2D.transform.gameObject);
            }

        }

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
            transform.rotation = (Quaternion.Euler(new Vector3(0, Mathf.Sign(velocity.x) == 1 ? 0 : 180, 0)));
        }
        else
        {
            animator_C.SetBool("isRunning", false);
        }

        rb2D_C.velocity = velocity * speed;
    }


}
