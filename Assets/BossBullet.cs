using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    //Health and damage stuff
    public int damage = 10;
    public float bulletSpeed = 10f;
    public GameObject explosion;

    // Start is called before the first frame update
    /*void Start()
    {
        StartCoroutine(AttackType());
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * 20;
        StartCoroutine(DestroyAfterSeconds());
    }*/

    private IEnumerator DestroyAfterSeconds()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }

    private IEnumerator AttackType()
    {
        yield return new WaitForSeconds(6);
        float attackNumber = Random.Range(1, 3);

        if (attackNumber == 1f)
        {
            //do attack type 1 - spread shot (plus and diagonal)
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.velocity = transform.right * 20;
        }

        else if (attackNumber == 2f)
        {
            //do attack type 2 - plus shot
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.velocity = transform.right * 20;
        }

        else if (attackNumber == 3f)
        {
            //do attack type 3 - diagonal shot
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(1f, 1f) * 20;
        }
    }

    public void SetDirection(Vector2 direction)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = direction * bulletSpeed;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        PlayerHealth player = hitInfo.GetComponent<PlayerHealth>();
        if (hitInfo.gameObject.tag == "Player")
        {
            player.GetComponent<PlayerHealth>().TakeDamage(damage);
            Destroy(gameObject);
//            GameObject new_Explosion = Instantiate(explosion);
//            new_Explosion.transform.position = transform.position;
//            Destroy(new_Explosion, 0.4f);
        }
    }
}
