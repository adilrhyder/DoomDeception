using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public int damage = 10;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Player collided");
        if(collision.gameObject.tag == "Player")
        {
            playerHealth.DamagePlayer(damage);
        }
    }
}