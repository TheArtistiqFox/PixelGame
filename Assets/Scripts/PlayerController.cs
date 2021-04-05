using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
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

    private bool _facingRight = true;

    [SerializeField] private bool _isJumping = false;
    private bool _isFalling = false;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        
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
        playerActionControls.WASD.Shoot.performed += _ => Shoot();
    }

    private void Shoot()
    {
        Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);

        GameObject shooting = Instantiate(shootAnim);
        shooting.transform.position = _firePoint.transform.position;
        Vector3 scale = shooting.transform.localScale;
        if (!_facingRight)
        {
            scale.y *= -1;
            shooting.transform.localScale = scale;
        }
        Destroy(shooting, 0.4f);
    }

    private void Jump()
    {
        if (!_isJumping)
        {
            _isJumping = true;
            rb.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
            audioSource.PlayOneShot(jumpSound);
        
        }
    }

    private bool IsGrounded()
    {
        Debug.DrawLine(col.bounds.center, col.bounds.center + (Vector3.down * col.bounds.size.y / 2f), Color.red);
        RaycastHit2D rayCastHit2D = Physics2D.Raycast(col.bounds.center, Vector2.down, col.bounds.size.y / 2f + 0.01f, ground);
        return rayCastHit2D.collider != null;
    }

    void Update()
    {
        Move();

        //new sprite flipping code (flips the sprite and its children)
        float movementInput = playerActionControls.WASD.Move.ReadValue<float>();
        if (movementInput == -1 && _facingRight)
            Flip();
        if (movementInput == 1 && !_facingRight)
            Flip();

        _isFalling = rb.velocity.y < 0f;
        
        bool isGrounded = IsGrounded();
        if (_isJumping && isGrounded && _isFalling)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f);
        }

        _isJumping = !isGrounded;
        _animator.SetBool("IsJumping", _isJumping);
        
    }

    private void Move()
    {
        float movementInput = playerActionControls.WASD.Move.ReadValue<float>();
        if (movementInput == 0)
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }
        else
        {
            float xVelocity = _isJumping ? maxVelocity *.8f : maxVelocity;
            rb.velocity = new Vector2(movementInput * xVelocity, rb.velocity.y);
        }

        //Animation
        if (movementInput != 0) _animator.SetBool("Run", true);
        else _animator.SetBool("Run", false);
    }

    private void Flip()
    {
        rb.velocity = new Vector2(0, rb.velocity.y);
        _facingRight = !_facingRight;
        transform.Rotate(0f, 180f, 0f);
        
    }
}