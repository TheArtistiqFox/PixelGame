using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    //Health and damage stuff
    public int damage = 40;

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
        //note to self: continue from about 10 minutes -or just before - on the "2D Shooting in Unity" video
        //Reason: You don't have an enemy yet to apply or subtract damage from
        // (no enemy script - nothing to hit or reference in this script)
        Boss boss = hitInfo.GetComponent<Boss>();
        if (boss != null)
        {
            boss.TakeDamage(damage);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
