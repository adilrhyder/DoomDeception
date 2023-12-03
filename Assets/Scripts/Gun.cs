using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//script to define behavior of gun through box collider
public class Gun : MonoBehaviour
{
    //variables defining range of gun (gun is basically a rectangle that detects when enemies are inside it and damages them if user clicks mouse button)
    public float range = 20f;
    public float verticalRange = 20f;
    public float gunShotRadius = 20f;   //radius at which enemies can hear gunshot
    
    private BoxCollider gunTrigger;     //get list to manage enemies in range of gun (we attach this variable in Unity by dragging and dropping "EnemyManager" in this field)
    public EnemyManager enemyManager;
    
    public float fireRate = 1.1f;         //variable defining rate at which gun can fire
    private float nextTimeToFire = 0f;  //to keep track of fire rate - don't need to do this (0 is assigned by default)
    
    public int maxAmmo; 
    private int ammo = 100;             //setting to 100 as default

    public float bigDamage = 2f;        //damage of gun (set to 2 by default - enemy health is also 2 for now)
    public float smallDamage = 1f;
    
    public LayerMask raycastLayerMask;  //layer mask for raycast of gun projectile
    public LayerMask enemyLayerMask;    //layer mask for enemy

    public bool isFiring;               //boolean variable for gun animation

    // Start is called before the first frame update
    void Start()
    {
        gunTrigger = GetComponent<BoxCollider>();
        gunTrigger.size = new Vector3(1, verticalRange, range);
        gunTrigger.center = new Vector3(0, 0, range * (0.5f));

        CanvasManager.Instance.UpdateAmmo(ammo);

    }

    // Update is called once per frame
    void Update()
    {
        isFiring = false;

        if (Input.GetMouseButton(0) && (Time.time > nextTimeToFire) && (ammo > 0))    //if mouse button is down/clicked and the time since the game has started is greater than the last time gun was fired
        {
            isFiring = true;
            Fire();
        }
    }

    //fire function to fire gun (pew pew)
    void Fire()
    {
        // Debug.Log("Firing at Time: " + Time.time);
        //simulate gunshot radius -> start at player position i.e. transform.position, extend over gunshot radius, and detect enemy layer mask
        //overlapsphere returns an array of colliders that fall within the given radius
        Collider[] enemyColliders;
        enemyColliders = Physics.OverlapSphere(transform.position, gunShotRadius, enemyLayerMask);

        //alert all enemies within radius
        foreach (var enemyCollider in enemyColliders)
        {
            enemyCollider.GetComponent<EnemyAwareness>().isAggro = true;
        }

        //play gun audio (stop first in case the audio is already playing so that the two don't overlap)
        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().Play();

        //damage enemies in list that come in line of raycast (will only damage the first in line)
        foreach (var enemy in enemyManager.enemiesInTrigger)
        {
            //calculating direction to enemy using enemy position and player position
            var dir = enemy.transform.position - transform.position;

            //raycast to make sure enemy is in line of sight
            RaycastHit hit;
            //origin is the player, represented by transform.position
            //direction is towards direction of enemy, which we calculated above
            // want it to be output in our hit variable
            // range should be a little more than the range of the gun incase the enemy is at the edge of the box that defines the range of our gun
            //we also use the layermask so the raycast ignores the box collider around the player

            if (Physics.Raycast(transform.position, dir, out hit, (range * 1.5f), raycastLayerMask))
            {
                if (hit.transform == enemy.transform)   //if hit is with enemy position and not with some intervening object
                {
                    //range check so enemies further away take less damage
                    float dist = Vector3.Distance(enemy.transform.position, transform.position);

                    if (dist > range * 0.5f)
                    {
                        //damage enemy with min damage if distance is further away
                        enemy.TakeDamage(smallDamage);
                    }
                    else
                    {
                        //damage enemy with max damage
                        enemy.TakeDamage(bigDamage);
                    }

                    //debug for visualization
                    // Debug.DrawRay(transform.position, dir, Color.green);
                    // Debug.Break();
                }
            }
        }


        //reset timer
        nextTimeToFire = Time.time + fireRate;
        // Debug.Log("Fire Rate: " + fireRate);
        // Debug.Log("Next Time to Fire: " + nextTimeToFire);


        //deduct ammo
        ammo--;

        CanvasManager.Instance.UpdateAmmo(ammo);
    }

    public void GiveAmmo(int amount, GameObject pickup)
    {
       if (ammo < maxAmmo)
       {
            ammo += amount;
            Destroy(pickup);
       } 

       if (ammo > maxAmmo)
       {
            ammo = maxAmmo;
       }

       CanvasManager.Instance.UpdateAmmo(ammo);
    }

    private void OnTriggerEnter(Collider other)
    {
        //add potential enemy to shoot
        //get enemy component from enemy object
        Enemy enemy = other.transform.GetComponent<Enemy>();

        if (enemy)
        {
            //add enemy to enemy manager list
            enemyManager.AddEnemy(enemy);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //remove potential enemy to shoot
        //get enemy component from enemy object
        Enemy enemy = other.transform.GetComponent<Enemy>();

        if (enemy)
        {
            //remove enemy from enemy manager list
            enemyManager.RemoveEnemy(enemy);
        }
    }
}
