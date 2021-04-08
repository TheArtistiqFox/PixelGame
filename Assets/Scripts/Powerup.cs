using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        PowerupCollected();
        Destroy(gameObject);
    }

    protected virtual void PowerupCollected()
    {
                
    }
}
