using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("q"))
        {
            // Quit Game
            Debug.Log("Quitting Game");
            Application.Quit();
        }
    }

}
