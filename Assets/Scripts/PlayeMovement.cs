using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayeMovement : MonoBehaviour
{
    public float speed;

    Rigidbody2D rb;
    Animator anim;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();        
    }

    
    void Update()
    {
        if (anim.GetBool("isAlive"))
        {
            Move();
            Rotate();
        }
        else
        {
            rb.velocity = new Vector3(0,0,0);
        }
    }

    private void Move()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        anim.SetFloat("Speed",rb.velocity.magnitude);
        rb.velocity = new Vector2(inputX, inputY) * speed;
    }

    private void Rotate()
    {
        var mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mouseWorldPosition - transform.position;
        transform.up = -direction;
    }
}
