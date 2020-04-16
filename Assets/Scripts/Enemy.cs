using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health;
    public float searchRadius;
    public float speed;

    Rigidbody2D rb;
    Animator anim;
    Player player;
    public bool isPlayerSeen = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<Player>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
        SearchPlayer();
        if (isPlayerSeen) { Move(player.transform.position + (-transform.position)); }
                     else { Move(new Vector3(0, 0, 0));  }
      
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
        if (Mathf.Sqrt(a * a + b * b) < searchRadius) { isPlayerSeen = true; }
        else { isPlayerSeen = false; }
        anim.SetBool("isSeen", isPlayerSeen);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var damage = collision.GetComponent<DamageDiller>();
        if (damage != null)
        {
            health -= damage.damage;

            if (health <= 0)
                Destroy(gameObject);

            Destroy(collision.gameObject);
        }

    }   
}
