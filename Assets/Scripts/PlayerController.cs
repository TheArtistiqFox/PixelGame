using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _jumpSpeed = 1f;
    [SerializeField] private LayerMask _ground;

    private PlayerControls _playerControls;

    private Rigidbody2D _rigidbody2D;
    private Collider2D _collider2D;
    private Animator _animator;
    
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _collider2D = GetComponent<Collider2D>();
        _animator = GetComponent<Animator>();
        
        _playerControls = new PlayerControls();
    }

    private int Add(int a, int b)
    {
        int c = a + b;
        return c;
    }

    private void Start()
    {
        _playerControls.WASD.Jump.performed += Jump;
    }

    private void Jump(InputAction.CallbackContext obj)
    {
        if (IsGrounded())
        {
            _rigidbody2D.AddForce(new Vector2(0, _jumpSpeed), ForceMode2D.Impulse);
        }
    }

    private bool IsGrounded()
    {
        Vector2 topLeftPoint = transform.position;
        topLeftPoint.x -= _collider2D.bounds.extents.x;
        topLeftPoint.y += _collider2D.bounds.extents.y;

        Vector2 bottomRightPoint = transform.position;
        bottomRightPoint.x += _collider2D.bounds.extents.x;
        bottomRightPoint.y -= _collider2D.bounds.extents.y;

        return Physics2D.OverlapArea(topLeftPoint, bottomRightPoint, _ground);
    }

    private void OnEnable()
    {
        _playerControls.Enable();
    }

    private void OnDisable()
    {
        _playerControls.Disable();
    }

    private void Update()
    {
        float movementInput = _playerControls.WASD.Move.ReadValue<float>();

        Vector3 currentPosition = transform.position;
        currentPosition.x += movementInput * _speed * Time.deltaTime;
        transform.position = currentPosition;
        
        _animator.SetFloat("Speed", Mathf.Abs(movementInput));
        _animator.SetBool("IsJumping", !IsGrounded());

        if (movementInput != 0)
        {
            Vector3 currentScale = transform.localScale;
            currentScale.x = movementInput > 0 ? 1 : -1;
            transform.localScale = currentScale;
        }
    }
}
