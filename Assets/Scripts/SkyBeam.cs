using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBeam : MonoBehaviour
{
    public bool damageActive = false;
    
    private SpriteRenderer _renderer;
    
    void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }
    
    public void Show()
    {
        _renderer.enabled = true;
    }

    public void Hide()
    {
        _renderer.enabled = false;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (damageActive)
        {
            if (other.gameObject.tag == "Player")
            {
                other.gameObject.GetComponent<PlayerHealth>().TakeDamage(1);
            }
        }
    }
}
