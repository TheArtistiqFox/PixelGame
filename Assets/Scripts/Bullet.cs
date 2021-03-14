using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    //Health and damage stuff
    public int damage = 40;
    public GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * 20;
        StartCoroutine(DestroyAfterSeconds());

    }

    private IEnumerator DestroyAfterSeconds()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        
        Debug.Log(hitInfo.name); //prints the thing that was hit by the bullet (you can delete this later)

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
    // Update is called once per frame
    void Update()
    {
        
    }
}
