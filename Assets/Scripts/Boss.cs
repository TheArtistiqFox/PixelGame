using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{

    public Transform player;
    public GameObject bBullet;
    public Transform FirePoint;

    public bool isFlipped = false;

    //Health and Damage stuff
    public int health = 100;

    public float jumpForce = 10f;
    [SerializeField] private bool _isJumping = false;
    private Rigidbody2D _rb;

    void Start()
    {
        //StartCoroutine(AttackType());
//        _rb = GetComponent<Rigidbody2D>();
//        Rigidbody2D Bulletrb = bBullet.GetComponent<Rigidbody2D>();
//        //Bulletrb.velocity = transform.right
//        StartCoroutine(DestroyAfterSeconds());
    }


    // start new

    //Health and damage stuff
    public int damage = 40;
    public GameObject explosion;

    //rb.velocity = transform.right * 20;

    /*private IEnumerator AttackType()
    {
        float attackNumber = Random.Range(1f, 3f);
      
        if (attackNumber == 1f)
        {
            //spread shot all directions
            // b1 = Instantiate(bBullet, FirePoint.position, FirePoint.rotation);
        }

        else if (attackNumber == 2f)
        {
            // up and down shot
        }

        else if (attackNumber == 3f)
        {
            //diagonal shot
        }
    }
    */

    private IEnumerator DestroyAfterSeconds()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Boss boss = hitInfo.GetComponent<Boss>();
        if (boss != null)
        {
            boss.GetComponent<PlayerHealth>().TakeDamage(damage);
            Destroy(gameObject);
            GameObject new_Explosion = Instantiate(explosion);
            new_Explosion.transform.position = transform.position;
            Destroy(new_Explosion, 0.4f);
        }
    }

    // end new








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
