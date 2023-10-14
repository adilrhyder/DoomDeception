using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyManager enemyManager;
    private float enemyHealth = 2f;


    // Update is called once per frame
    void Update()
    {
        if (enemyHealth <= 0) {
            enemyManager.RemoveEnemy(this);
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float damage) {
        enemyHealth -= damage;
    }
}
