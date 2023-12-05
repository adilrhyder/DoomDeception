using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false; 

    public GameObject pauseMenuUI; 

    private GameObject player;
    private GameObject playerUI;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerUI = GameObject.FindGameObjectWithTag("PlayerUI");

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("q") && isPaused)
        {
            // Quit Game
            Debug.Log("Quitting Game");
            QuitGame();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPaused)
            {
                Resume();
            }
            else
            {    
                Pause();
            }
        }   
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; 
        isPaused = false;

        playerUI.SetActive(true);
        player.transform.GetChild(1).gameObject.SetActive(true);
        player.transform.GetChild(2).gameObject.SetActive(true);
        player.GetComponent<MouseMovement>().isPaused = false;

    }

    void Pause()
    {   
        playerUI.SetActive(false);
        player.transform.GetChild(1).gameObject.SetActive(false);
        player.transform.GetChild(2).gameObject.SetActive(false);
        player.GetComponent<MouseMovement>().isPaused = true;

        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; 
        isPaused = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Application.Quit();    
    }
}
