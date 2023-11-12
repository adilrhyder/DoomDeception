using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAwareness : MonoBehaviour
{
    // public Material aggroMat;
    public bool isAggro = false;    //public because other scripts are going to need to reference this 
    private Transform playersTransform;
    public float awarenessRadius = 8f;   //radius at which player is detected
    public float damageRadius = 4f; 


    // private bool damagingPlayer;
    private PlayerHealth playerHealth;

    public int damageAmount = 5;
    public float timeBetweenDamage = 1.5f; 

    private float damageCounter;

    // Start is called before the first frame update
    private void Start()
    {
        playersTransform = FindObjectOfType<PlayerMove>().transform;
        damageCounter = timeBetweenDamage;
        playerHealth = FindObjectOfType<PlayerHealth>();
    }

    private void Update()
    {
        // Calculating distance between player and enemy
        var dist = Vector3.Distance(transform.position, playersTransform.position);

        // If distance is within radius of enemy's awareness, set bool
        if (dist < awarenessRadius)
        {
            isAggro = true;

            if (dist <= damageRadius)
            {
                if (damageCounter >= timeBetweenDamage)
                {
                    playerHealth.DamagePlayer(damageAmount);
                    damageCounter = 0f;  // Reset the counter after dealing damage
                }
            }
        }
        else
        {
            // If the player is outside the awarenessRadius, reset the aggro and damageCounter
            // isAggro = false;
            damageCounter = timeBetweenDamage;
        }

        if (isAggro)
        {
            // Update the damageCounter only when the player is within the damageRadius
            if (dist <= damageRadius)
            {
                damageCounter += Time.deltaTime;
            }
        }
    }

}
