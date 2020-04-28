using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrelScript : MonoBehaviour
{
    public GameObject children;
    public Sprite explodeSprite;
    public float exploadRadius;
    public int exploadDamage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var damage = collision.GetComponent<DamageDiller>();
        if (damage != null)
        {
            children.SetActive(true);
            var tmp = GetComponent<SpriteRenderer>();
            Destroy(tmp);


            Collider2D[] blocksOnRadius = Physics2D.OverlapCircleAll(transform.position, exploadRadius, LayerMask.GetMask("Enemy"));
            foreach (Collider2D i in blocksOnRadius)
            {
                if (i.gameObject != gameObject) i.GetComponent<Enemy>().GetDamage(exploadDamage);
            }


            blocksOnRadius = Physics2D.OverlapCircleAll(transform.position, exploadRadius, LayerMask.GetMask("Player"));
            foreach (Collider2D i in blocksOnRadius)
            {
                if (i.gameObject != gameObject) i.GetComponent<Player>().GetDamage(exploadDamage);
            }

            
        }

    }
}
