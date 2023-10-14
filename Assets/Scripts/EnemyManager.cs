using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // A public List of Enemy objects to store enemies that are within the trigger area.
    public List<Enemy> enemiesInTrigger = new List<Enemy>();

    // Method to add an enemy to the list.
    public void AddEnemy(Enemy enemy) {
        // Add the provided 'enemy' object to the 'enemiesInTrigger' list.
        enemiesInTrigger.Add(enemy);
    }

    // Method to remove an enemy from the list.
    public void RemoveEnemy(Enemy enemy) {
        // Remove the provided 'enemy' object from the 'enemiesInTrigger' list.
        enemiesInTrigger.Remove(enemy);
    }
}
