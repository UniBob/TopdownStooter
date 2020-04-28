using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Base stats")]
    public float health;
    public float searchRadius;
    public float atackRadius;
    public float speed;

    [Header("Atack")]
    public int atackDamage;
    public float atackRate;


    Rigidbody2D rb;
    Animator anim;
    Player player;
    public bool isPlayerSeen = false;
    bool isAlive;
    float nextAtackTime;

    private void Start()
    {
        nextAtackTime = Time.time;
        isAlive = true;
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<Player>();
        anim = GetComponentInChildren<Animator>();
        anim.SetBool("isAlive", true);
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            Rotate();
            SearchPlayer();
            if (isPlayerSeen) 
            {
                Move(player.transform.position + (-transform.position)); 
            }
            else 
            {
                Move(Vector3.zero); 
            }
        }
      
    }

    void Move(Vector3 velocity)
    {
        rb.velocity = velocity * speed;
    }

    void Rotate()
    {
        var playerPosition = player.transform.position;
        Vector2 direction = playerPosition - transform.position;
        transform.up = -direction;
    }

    private void SearchPlayer()
    {
        var a = Mathf.Abs(transform.position.x - player.transform.position.x);
        var b = Mathf.Abs(transform.position.y - player.transform.position.y);
        var distansToPlayer = Mathf.Sqrt(a * a + b * b);
        if (distansToPlayer < searchRadius && distansToPlayer > atackRadius) { isPlayerSeen = true; }
        else 
        {
            isPlayerSeen = false;
            if (distansToPlayer < atackRadius)
                Atack();
        }
        anim.SetBool("isSeen", isPlayerSeen);
    }

    void Atack()
    {
        if (nextAtackTime <= Time.time && player.GetStatus())
        {
            nextAtackTime = Time.time + atackRate;
            anim.SetTrigger("Atack");
            DoDamageToPlayer();
        }
    }

    public void DoDamageToPlayer()
    {
        player.GetDamage(atackDamage);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var damage = collision.GetComponent<DamageDiller>();
        if (damage != null && isAlive)
        {
            GetDamage(damage.damage);

            Destroy(collision.gameObject);
        }

    }   

    public void GetDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        anim.SetBool("isAlive",false);
        isAlive = false;
        rb.velocity = Vector3.zero;
    }
}
