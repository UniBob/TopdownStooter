using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    enum ZombieStates
    {
        IDLE,
        WALK,
        DEAD,
        ATACK
    }

    [Header("Base stats")]
    public float searchRadius;
    public float atackRadius;
    public float speed;

    [Header("Atack")]
    public int atackDamage;
    public float atackRate;


    Rigidbody2D rb;
    Animator anim;
    Player player;

    bool isPlayerSeen = false;
    bool isAlive;
    float nextAtackTime;
    ZombieStates state;

    void Start()
    {
        state = ZombieStates.IDLE;
        nextAtackTime = Time.time;
        isAlive = true;
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<Player>();
        anim = GetComponentInChildren<Animator>();
    }

    void FixedUpdate()
    {
        switch(state)
        {
            case ZombieStates.IDLE:
                {
                    SearchPlayer();
                    if (isPlayerSeen)
                    {
                        Move(player.transform.position + (-transform.position));
                        anim.SetBool("isSeen", isPlayerSeen);
                        state = ZombieStates.WALK;
                    }
                    break;
                }

            case ZombieStates.DEAD:
                {
                    break;
                }

            case ZombieStates.ATACK:
                {
                    break;
                }

            case ZombieStates.WALK:
                {
                    SearchPlayer();
                    if (isPlayerSeen)
                    {
                        Move(player.transform.position + (-transform.position));
                    }
                    else
                    {
                        Move(Vector3.zero);
                    }
                    break;
                }
        }
    }



    void Move(Vector3 velocity)
    {
        rb.velocity = velocity.normalized * speed;
    }


    private void SearchPlayer()
    {
        var distansToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distansToPlayer < searchRadius && distansToPlayer > atackRadius)
        {
            isPlayerSeen = true; 
        }
        else
        {
            isPlayerSeen = false;
            if (distansToPlayer < atackRadius)
                Atack();
        }

    }

    void Atack()
    {
        if (nextAtackTime <= Time.time && player.GetStatus())
        {
            nextAtackTime = Time.time + atackRate;
            anim.SetTrigger("Atack");
            player.GetDamage(atackDamage);
        }
    }

}
