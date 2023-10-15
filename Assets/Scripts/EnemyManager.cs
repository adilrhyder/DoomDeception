using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    //creating a list of enemies
    public List<Enemy> enemiesInTrigger = new List<Enemy>();

    public void AddEnemy(Enemy enemy)
    {
        enemiesInTrigger.Add(enemy);
    }
    public void RemoveEnemy(Enemy enemy)
    {
        enemiesInTrigger.Remove(enemy);
    }

}
