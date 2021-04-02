using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //public float speed;
    [SerializeField] private float speed, jumpSpeed;
    [SerializeField] private LayerMask ground;
    [SerializeField] private GameObject _bulletPrefab;
    private PlayerActionControls playerActionControls;

    private Rigidbody2D rb;
    private Collider2D col;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    public Transform _firePoint;

    public float maxVelocity = 8f;
    public GameObject shootAnim;
    public AudioSource audioSource;
    public AudioClip jumpSound;

    private bool _FacingRight = true;

    private void Awake()
    {
        playerActionControls = new PlayerActionControls();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVelocity);
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
        playerActionControls.WASD.Shoot.performed += _ => Shoot();
    }

    private void Shoot()
    {
        //Debug.Log("shoot");
        Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);

        GameObject shooting = Instantiate(shootAnim);
        shooting.transform.position = _firePoint.transform.position;
        //shooting.transform.rotation = transform.rotation;
        Vector3 scale = shooting.transform.localScale;
        if (!_FacingRight)
        {
            scale.y *= -1;
            shooting.transform.localScale = scale;
        }
        Destroy(shooting, 0.4f);
    }

    private void Jump()
    {
        if (IsGrounded())
        {
            rb.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
            //_animator.SetTrigger("Jump");
            audioSource.PlayOneShot(jumpSound);
        
        }
    }

    private bool IsGrounded()
    {
        Debug.DrawLine(col.bounds.center, col.bounds.center + (Vector3.down * col.bounds.size.y / 2f), Color.red);
        RaycastHit2D rayCastHit2D = Physics2D.Raycast(col.bounds.center, Vector2.down, col.bounds.size.y / 2f + 0.1f, ground);
        return rayCastHit2D.collider != null;
    }

    void Update()
    {
        Move();

        //new sprite flipping code (flips the sprite and its children)
        float movementInput = playerActionControls.WASD.Move.ReadValue<float>();
        if (movementInput == -1 && _FacingRight)
            Flip();
        if (movementInput == 1 && !_FacingRight)
            Flip();

        _animator.SetBool("IsJumping", !IsGrounded());
        
    }

    private void Move()
    {
        // Read the movement value
        float movementInput = playerActionControls.WASD.Move.ReadValue<float>();
        // Move the player
        //Vector3 currentPosition = transform.position;
        //currentPosition.x += movementInput * speed * Time.deltaTime;
        //transform.position = currentPosition;
        if (movementInput == 0)
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }
        else
        {
            float xForce = movementInput * speed * Time.deltaTime;
            rb.AddForce(new Vector2(xForce, 0));
        }

        //Animation
        if (movementInput != 0) _animator.SetBool("Run", true);
        else _animator.SetBool("Run", false);

        //sprite flip (original - only flips the sprite)
        //if (movementInput == 1)
        //    _spriteRenderer.flipX = true;
        //if (movementInput == -1)
        //    _spriteRenderer.flipX = false;
    }

    private void Flip()
    {
        rb.velocity = new Vector2(0, rb.velocity.y);
        _FacingRight = !_FacingRight;
        transform.Rotate(0f, 180f, 0f);
        
    }
}