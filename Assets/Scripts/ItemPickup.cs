using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{

    public bool isHealth; 
    public bool isAmmo;
    public bool isArmor; 

    public int amount;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter (Collider other) {
        if (other.CompareTag("Player")) {
            if (isHealth) {
                //this.gameObject passes a reference to this object
                other.GetComponent<PlayerHealth>().GiveHealth(amount, this.gameObject);
            }
            if (isAmmo) {
                other.GetComponentInChildren<Gun>().GiveAmmo(amount, this.gameObject);
                // other.GetComponent<PlayerController>().ammo += amount;
            }
            if (isArmor) {
                other.GetComponent<PlayerHealth>().GiveArmor(amount, this.gameObject);
            }
            // Destroy(gameObject); commented as we want to destroy this object only if it is used (i.e., the player's health/armor is not full)
        }
    }
}
