using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Weapon : MonoBehaviour
{
    public int attackDamage = 20;
    public Vector3 attackOffset;
    public float attackRange = 3f;
    public LayerMask attackMask;
    public BossBullet BulletPrefab;

    public void Attack()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
        if (colInfo != null)
        {
            //need the player to have health to use this.
            colInfo.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
        }
    }

    private void CreateBullet(Vector2 bulletDirection)
    {
        BossBullet bullet = Instantiate(BulletPrefab);
        bullet.transform.position = transform.position;
        bullet.SetDirection(bulletDirection);
    }

    public void SpreadShot()
    {
        CreateBullet(new Vector2(1f, 0f));
        CreateBullet(new Vector2(-1f, 0f));
        CreateBullet(new Vector2(0f, 1f));
        CreateBullet(new Vector2(1f, 1f));
        CreateBullet(new Vector2(-1f, 1f));
    }
}
