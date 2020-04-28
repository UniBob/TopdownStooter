using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [Header("Prefabs")]
    public GameObject shotPrefab;
    public Transform shotPosition;

    [Header("Other options")]
    public float fireRate;
    public int currentHealth;
    public int maxHealth;


    Animator anim;
    float nextShotTime;
    bool isAlive;

    // Start is called before the first frame update
    void Start()
    {
        isAlive = true;
        currentHealth = 100;
        anim = GetComponentInChildren<Animator>();
        nextShotTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && nextShotTime <= Time.time && isAlive)
        {
            Instantiate(shotPrefab, shotPosition.position, transform.rotation);
            nextShotTime = Time.time + fireRate;
            anim.SetTrigger("Shoot");
        }
    }

    public void GetDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        isAlive = false;
        anim.SetBool("isAlive", false);
    }

    public bool GetStatus()
    {
        return isAlive;
    }

    public bool HealBonus(int heal)
    {
        if(currentHealth == maxHealth)
        {
            return false;
        }
        else
        {
            currentHealth += heal;
            return true;
        }
    }
}
