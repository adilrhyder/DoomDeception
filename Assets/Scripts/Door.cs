using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Animator doorAnim; 
    public GameObject areaToSpawn;

    //boolean that defines if this door requires a key
    public bool requiresKey;
    public bool reqRed, reqBlue, reqGreen;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (requiresKey)
            {
                if (reqRed && other.GetComponent<PlayerInventory>().hasRed)
                {
                    //open door
                    doorAnim.SetTrigger("OpenDoor");

                    //spawn enemies in area
                    areaToSpawn.SetActive(true);

                    //remove redKey from inventory
                    other.GetComponent<PlayerInventory>().hasRed = false;
                }

                if (reqBlue && other.GetComponent<PlayerInventory>().hasBlue)
                {
                    //open door
                    doorAnim.SetTrigger("OpenDoor");

                    //spawn enemies in area
                    areaToSpawn.SetActive(true);

                    //remove blue key from inventory
                    other.GetComponent<PlayerInventory>().hasBlue = false;
                }

                if (reqGreen && other.GetComponent<PlayerInventory>().hasGreen)
                {
                    //open door
                    doorAnim.SetTrigger("OpenDoor");

                    //spawn enemies in area
                    areaToSpawn.SetActive(true);

                    //remove greenkey from inventory
                    other.GetComponent<PlayerInventory>().hasGreen = false;
                }
            }
            else
            {
                //open door
                doorAnim.SetTrigger("OpenDoor");

                //spawn enemies in area
                areaToSpawn.SetActive(true);
            }
        }
    }
}
