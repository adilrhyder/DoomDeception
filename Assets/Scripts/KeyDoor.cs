using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoor : MonoBehaviour
{
    public Animator doorAnim; 
    public GameObject areaToSpawn;
    public GameObject areaToSpawn_alternate;

    public GameObject areaToSpawnOnReturn;
    public GameObject areaToSpawnOnReturn_alternate;

    public GameObject activeRoom;

    public bool checksDeath;
    public bool enemyKilled;

    public bool checksRedKey;
    public bool checksBlueKey;
    public bool checksGreenKey;

    public bool isOpen = false;

    public bool deactivatesRoom;

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
            //deactivate intro room
            if (isOpen && deactivatesRoom)
            {
                activeRoom.SetActive(false);
            }

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

                    if (checksDeath)
                    {
                        if (other.GetComponent<PlayerInventory>().hasKilled)
                        {
                            areaToSpawn.SetActive(false);
                            areaToSpawn_alternate.SetActive(true);
                        }
                        else
                        {
                            areaToSpawn_alternate.SetActive(false);
                            areaToSpawn.SetActive(true);
                        }
                    }
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

                    if (checksDeath)
                    {
                        if (other.GetComponent<PlayerInventory>().hasKilled)
                        {
                            areaToSpawn.SetActive(false);
                            areaToSpawn_alternate.SetActive(true);
                        }
                        else
                        {
                            areaToSpawn_alternate.SetActive(false);
                            areaToSpawn.SetActive(true);
                        }
                    }
                }
                

                if (reqGreen && other.GetComponent<PlayerInventory>().hasGreen)
                {
                    print("Green door!");
                    //open door
                    doorAnim.SetTrigger("OpenDoor");

                    // //spawn enemies in area
                    areaToSpawn.SetActive(true);

                    //remove greenkey from inventory
                    other.GetComponent<PlayerInventory>().hasGreen = false;

                    //remove key from UI
                    // print("Updated Inventory");
                    playerUI.GetComponent<CanvasManager>().UpdateKeys("green");

                    if (checksDeath)
                    {
                        if (other.GetComponent<PlayerInventory>().hasKilled)
                        {
                            areaToSpawn.SetActive(false);
                            areaToSpawn_alternate.SetActive(true);
                        }
                        else
                        {
                            areaToSpawn_alternate.SetActive(false);
                            areaToSpawn.SetActive(true);
                        }
                    }
                }

                requiresKey = false;
            }
            
            else
            {
                print("No key required!");
                //open door
                // doorAnim.SetTrigger("OpenDoor");

                //spawn enemies in area
                if (isOpen)
                {

                    if (checksDeath)
                    {
                        if (other.GetComponent<PlayerInventory>().hasKilled)
                        {
                            areaToSpawn.SetActive(false);
                            areaToSpawn_alternate.SetActive(true);
                        }
                        else
                        {
                            areaToSpawn_alternate.SetActive(false);
                            areaToSpawn.SetActive(true);
                        }
                    }
                    else if (checksBlueKey)
                    {
                        if (other.GetComponent<PlayerInventory>().hasBlue)
                        {
                            Debug.Log("Turning off");
                            areaToSpawnOnReturn_alternate.SetActive(false);
                            areaToSpawnOnReturn.SetActive(true);
                        }
                        else
                        {
                            Debug.Log("Turning on");
                            areaToSpawnOnReturn.SetActive(false);
                            areaToSpawnOnReturn_alternate.SetActive(true);
                        }
                    }
                    else if (checksRedKey)
                    {
                        if (other.GetComponent<PlayerInventory>().hasRed)
                        {
                            Debug.Log("Turning off");
                            areaToSpawn_alternate.SetActive(false);
                            areaToSpawn.SetActive(true);
                        }
                        else
                        {
                            Debug.Log("Turning on");
                            areaToSpawn.SetActive(false);
                            areaToSpawn_alternate.SetActive(true);
                        }
                    }
                    else if (checksGreenKey)
                    {
                        if (other.GetComponent<PlayerInventory>().hasGreen)
                        {
                            Debug.Log("Turning off");
                            areaToSpawn_alternate.SetActive(false);
                            areaToSpawn.SetActive(true);
                        }
                        else
                        {
                            Debug.Log("Turning on");
                            areaToSpawn.SetActive(false);
                            areaToSpawn_alternate.SetActive(true);
                        }
                    }
                    else
                    {
                        doorAnim.SetTrigger("OpenDoor");
                        areaToSpawn.SetActive(true);
                    }
                }
                else
                {
                    doorAnim.SetTrigger("OpenDoor");
                    areaToSpawn.SetActive(true);
                    isOpen = true;
                }
            }
        }
    }
}