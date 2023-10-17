using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAwareness : MonoBehaviour
{
    public Material aggroMat;
    public bool isAggro = false;    //public because other scripts are going to need to reference this 

    private void Update()
    {
        //change color if bool set
        if (isAggro)
        {
            GetComponent<MeshRenderer>().material = aggroMat;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //check if player is in vicinity (defined by trigger sphere collider that we'll assign to enemy as a component)
        if (other.transform.CompareTag("Player"))
        {
            //changes material/color of enemy in update function 
            isAggro = true;
        }
    }
}
