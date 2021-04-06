using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed, jumpSpeed;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private float fallMultiplier = 2.5f;
    
    private PlayerActionControls playerActionControls;
    private Rigidbody2D rb;
    private Collider2D col;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    public Transform _firePoint;

    public GameObject shootAnim;
    public AudioSource audioSource;
    public AudioClip jumpSound;

    private bool _facingRight = true;

    private bool _canDoubleJump = false;
    private bool _hasDoubleJumped = false;
    private float _doubleJumpTimer = 0f;
    private float _timeTilDoubleJump = .3f;

    [SerializeField] private bool _isJumping = false;

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
        playerActionControls.WASD.Shoot.performed += _ => Shoot();
    }

    private void Shoot()
    {
        Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);

        GameObject shooting = Instantiate(shootAnim);
        shooting.transform.SetParent(_firePoint.transform);
        shooting.transform.localPosition = Vector3.zero;
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
        if (!_isJumping || _canDoubleJump)
        {
            if (_isJumping)
            {
                _hasDoubleJumped = true;
                _canDoubleJump = false;
            }
            _isJumping = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            audioSource.PlayOneShot(jumpSound);
        }
    }

    private bool _jumpPressed = false;
    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            _jumpPressed = true;
        }

        if (Keyboard.current.spaceKey.wasReleasedThisFrame)
        {
            if (_isJumping)
            {
                if (!_hasDoubleJumped)
                {
                    _canDoubleJump = true;
                }
            }
        }
        
        //new sprite flipping code (flips the sprite and its children)
        float movementInput = playerActionControls.WASD.Move.ReadValue<float>();
        if (movementInput == -1 && _facingRight)
            Flip();
        if (movementInput == 1 && !_facingRight)
            Flip();

        _animator.SetBool("IsJumping", _isJumping);
    }

    private void FixedUpdate()
    {
        if (_jumpPressed)
        {
            _jumpPressed = false;
            Jump();
        }

        Move();
    }

    private void Move()
    {
        float movementInput = playerActionControls.WASD.Move.ReadValue<float>();
        
        float xVelocity = movementInput * speed;
        float yVelocity = rb.velocity.y;
        if (yVelocity < 0)
        {
           yVelocity += Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }

        rb.velocity = new Vector2(xVelocity, yVelocity);

        //Animation
        if (movementInput != 0) _animator.SetBool("Run", true);
        else _animator.SetBool("Run", false);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Ground")
        {
            _isJumping = false;
            _doubleJumpTimer = 0f;
            _hasDoubleJumped = false;
            _canDoubleJump = false;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.tag == "Ground")
        {
           // _isJumping = true;
        }
    }

    private void Flip()
    {
        _facingRight = !_facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}