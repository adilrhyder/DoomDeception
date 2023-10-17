using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //list storing enemies (we attach this variable in Unity by dragging and dropping "EnemyManager" in this field FOR EVERY ENEMY)
    public EnemyManager enemyManager;
    private float enemyHealth = 2f; //variable for enemy health

    public GameObject gunHitEffect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //check if enemy health has dropped to zero
        if (enemyHealth <= 0)
        {
            //remove this object from list 
            enemyManager.RemoveEnemy(this);
            
            //destroy game object 
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float damage)
    {
        //apply gun hit effect so enemies die properly
        Instantiate(gunHitEffect, transform.position, Quaternion.identity);
        
        //update enemy health
        enemyHealth -= damage;
    }
}
