using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{

    public Transform player;

    public bool isFlipped = false;

    //Health and Damage stuff
    public int health = 100;

    public float jumpForce = 10f;
    [SerializeField] private bool _isJumping = false;
    private Rigidbody2D _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    
    //public GameObject deathEffect;
    //public void TakeDamage(int damage)
    //{
      //  health -= damage;
        //if (health <= 0)
        //{
         //   Die();
        //}
    //}

    //void Die()
    //{
     //   //Instantiate(deathEffect, transform.position, Quaternion.identity);
       // Destroy(gameObject);
    //}

    //ends here

    public void Jump()
    {
        if (!_isJumping)
        {
            _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
    
    public bool IsJumping()
    {
        return _isJumping;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Ground")
        {
            _isJumping = false;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.tag == "Ground")
        {
            _isJumping = true;
        }
    }

    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x < player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }
    
}
