using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Animator doorAnim; 
    public GameObject areaToSpawn;

    private GameObject playerUI;

    //boolean that defines if this door requires a key
    public bool requiresKey;
    public bool reqRed, reqBlue, reqGreen;

    private void Start()
    {
        playerUI = GameObject.FindGameObjectWithTag("PlayerUI");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (requiresKey)
            {
                print("Key required");
                if (reqRed && other.GetComponent<PlayerInventory>().hasRed)
                {
                    //open door
                    doorAnim.SetTrigger("OpenDoor");

                    //spawn enemies in area
                    areaToSpawn.SetActive(true);

                    //remove redKey from inventory
                    other.GetComponent<PlayerInventory>().hasRed = false;

                    //remove key from UI
                    playerUI.GetComponent<CanvasManager>().UpdateKeys("red");
                }

                if (reqBlue && other.GetComponent<PlayerInventory>().hasBlue)
                {
                    //open door
                    doorAnim.SetTrigger("OpenDoor");

                    //spawn enemies in area
                    areaToSpawn.SetActive(true);

                    //remove blue key from inventory
                    other.GetComponent<PlayerInventory>().hasBlue = false;

                    //remove key from UI
                    playerUI.GetComponent<CanvasManager>().UpdateKeys("blue");
                }

                if (reqGreen && other.GetComponent<PlayerInventory>().hasGreen)
                {
                    print("Green door!");
                    //open door
                    doorAnim.SetTrigger("OpenDoor");

                    // //spawn enemies in area
                    // areaToSpawn.SetActive(true);

                    //remove greenkey from inventory
                    other.GetComponent<PlayerInventory>().hasGreen = false;

                    //remove key from UI
                    // print("Updated Inventory");
                    playerUI.GetComponent<CanvasManager>().UpdateKeys("green");

                }
            }
            else
            {
                print("No key required!");
                //open door
                doorAnim.SetTrigger("OpenDoor");

                //spawn enemies in area
                areaToSpawn.SetActive(true);
            }
        }
    }
}
