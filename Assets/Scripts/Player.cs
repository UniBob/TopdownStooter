using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject shotPrefab;
    public Transform shotPosition;
    public float fireRate;

    Animator anim;

    float nextShotTime;
    float health;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        nextShotTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && nextShotTime <= Time.time)
        {
            Instantiate(shotPrefab, shotPosition.position, transform.rotation);
            nextShotTime = Time.time + fireRate;
            anim.SetTrigger("Shoot");
        }
    }
}
