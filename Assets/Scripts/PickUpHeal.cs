using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpHeal : MonoBehaviour
{
    public int healCount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("heal");
        var player = collision.GetComponent<Player>();
        if (player != null)
        {
            if (player.HealBonus(healCount))
            {
                Destroy(gameObject);
            }
        }
    }

}
