using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private EnemyAwareness enemyAwareness;
    private Transform playersTransform;
    private NavMeshAgent enemyNavMeshAgent;

    private void Start()
    {
        enemyAwareness = GetComponent<EnemyAwareness>();                //enemy awareness set
        playersTransform = FindObjectOfType<PlayerMove>().transform;    //PlayerMove can only be found on the player so use that
        enemyNavMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (enemyAwareness.isAggro)                                         //if enemy is aware of player
        {
            enemyNavMeshAgent.SetDestination(playersTransform.position);    //move enemy towards players transform
        }
        else
        {
            enemyNavMeshAgent.SetDestination(transform.position);           //stay in default position otherwise
        }
    }
}
