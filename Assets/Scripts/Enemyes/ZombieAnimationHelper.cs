using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAnimationHelper : MonoBehaviour
{
    Enemy enemy;
   void Atack()
    {
        enemy.GetComponentsInParent<Enemy>();
        enemy.DoDamageToPlayer();
    }
}
