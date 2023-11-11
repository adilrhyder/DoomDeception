using UnityEngine;

public class DamageTrigger : MonoBehaviour
{
    private bool damagingPlayer;
    private PlayerHealth playerHealth;

    public int damageAmount = 10;
    public float timeBetweenDamage = 1.5f; 

    private float damageCounter;
    // Start is called before the first frame update
    void Start()
    {
        damageCounter = timeBetweenDamage;
        playerHealth = FindObjectOfType<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if(damagingPlayer)
        {
            // damage player every (timeBetweenDamage)
            if(damageCounter >= timeBetweenDamage)
            {
                playerHealth.DamagePlayer(damageAmount);

                // restart counter
                damageCounter = 0f; 
            }

            // add to the counter
            damageCounter += Time.deltaTime; 
        }   
        else
        {
            // keep the damage counter reset
            damageCounter = 0f; 
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            damageCounter = timeBetweenDamage; 
            damagingPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            damagingPlayer = false; 
        }
    }
}
