using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed, jumpSpeed;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float lowJumpMultiplier = 2f; 
    
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

    [SerializeField] private bool _isJumping = false;

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
            Debug.Log("JUMPING");
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed / rb.mass);
            audioSource.PlayOneShot(jumpSound);
        }
    }
    
    private void FixedUpdate()
    {
        Move();

        //new sprite flipping code (flips the sprite and its children)
        float movementInput = playerActionControls.WASD.Move.ReadValue<float>();
        if (movementInput == -1 && _facingRight)
            Flip();
        if (movementInput == 1 && !_facingRight)
            Flip();

        _animator.SetBool("IsJumping", _isJumping);
        
        if (playerActionControls.WASD.Jump.triggered)
        {
            Jump();
        }
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
        Debug.Log("COLLISION ENTER " + other.collider.tag);
        
        if (other.collider.tag == "Ground")
        {
            _isJumping = false;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        Debug.Log("COLLISION EXIT " + other.collider.tag);
        
        if (other.collider.tag == "Ground")
        {
            _isJumping = true;
        }
    }

    private void Flip()
    {
        _facingRight = !_facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}