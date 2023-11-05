using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAwareness : MonoBehaviour
{
    // public Material aggroMat;
    public bool isAggro = false;    //public because other scripts are going to need to reference this 
    private Transform playersTransform;
    public float awarenessRadius = 8f;   //radius at which player is detected

    private void Start()
    {
        playersTransform = FindObjectOfType<PlayerMove>().transform;
    }

    private void Update()
    {
        //calculating distance between player and enemy
        var dist = Vector3.Distance(transform.position, playersTransform.position);

        //if distance is within radius of enemy's awareness, set bool
        if (dist < awarenessRadius)
        {
            isAggro = true;
        }

        //change color if bool set
        if (isAggro)
        {
            // GetComponent<MeshRenderer>().material = aggroMat;
        }
    }
}
