using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //public float speed;
    [SerializeField] private float speed, jumpSpeed;
    [SerializeField] private LayerMask ground;
    private PlayerActionControls playerActionControls;

    private Rigidbody2D rb;
    private Collider2D col;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        playerActionControls = new PlayerActionControls();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        playerActionControls.Enable();
    }

    private void OnDisable()
    {
        playerActionControls.Disable();
    }

    void Start()
    {
        playerActionControls.WASD.Jump.performed += _ => Jump();
    }

    private void Jump()
    {
        if (IsGrounded())
        {
            rb.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
            _animator.SetTrigger("Jump");
        }
    }

    private bool IsGrounded()
    {
        Vector2 topLeftPoint = transform.position;
        topLeftPoint.x -= col.bounds.extents.x;
        topLeftPoint.y += col.bounds.extents.y;

        Vector2 bottomRightPoint = transform.position;
        bottomRightPoint.x += col.bounds.extents.x;
        bottomRightPoint.y -= col.bounds.extents.y;

        return Physics2D.OverlapArea(topLeftPoint, bottomRightPoint, ground);
    }

    void Update()
    {
        Move();
    }


    private void Move()
    {
        // Read the movement value
        float movementInput = playerActionControls.WASD.Move.ReadValue<float>();
        // Move the player
        Vector3 currentPosition = transform.position;
        currentPosition.x += movementInput * speed * Time.deltaTime;
        transform.position = currentPosition;

        //Animation
        if (movementInput != 0) _animator.SetBool("Run", true);
        else _animator.SetBool("Run", false);

        //sprite flip
        if (movementInput == 1)
            _spriteRenderer.flipX = true;
        else if (movementInput == -1)
            _spriteRenderer.flipX = false;         
    }
}