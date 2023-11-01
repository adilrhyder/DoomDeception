using UnityEngine;
using UnityEngine.UI;
using TMPro;  

public class CanvasManager : MonoBehaviour
{
    public TextMeshProUGUI health; 
    public TextMeshProUGUI armor; 
    public TextMeshProUGUI ammo; 

    public Image healthIndicator; 
    public Sprite health1; // healthy
    public Sprite health2; // hurt
    public Sprite health3; // really hurt
    public Sprite health4; // dead

    public GameObject redKey; 
    public GameObject blueKey; 
    public GameObject greenKey; 


    // singleton in order to easily access from other scripts without referencing the other scripts
    private static CanvasManager _instance; 
    public static CanvasManager Instance {get {return _instance;}}

    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else 
        {
            _instance = this; 
        }
    }

    // methods to update our values

    public void UpdateHealth(int healthValue)
    {
        health.text = healthValue.ToString() + "%";
        UpdateHealthIndicator(healthValue);
    }

    public void UpdateArmor(int armorValue)
    {
        armor.text = armorValue.ToString() + "%";
    }

    public void UpdateAmmo(int ammoValue)
    {
        ammo.text = ammoValue.ToString();
   
    }

    public void UpdateHealthIndicator(int healthValue)
    {
        if (healthValue >= 66)
        {
            healthIndicator.sprite = health1;
        }   
        else if (healthValue >= 33)
        {
            healthIndicator.sprite = health2; 
        }
        else if (healthValue > 0)
        {
            healthIndicator.sprite = health3; 
        }
        else
        {
            healthIndicator.sprite = health4; 
        }
    }

    public void UpdateKeys(string keyColor)
    {
        if(keyColor == "red")
        {
            redKey.SetActive(true);
        }

        if(keyColor == "blue")
        {
            blueKey.SetActive(true);
        }

        if(keyColor == "green")
        {
            greenKey.SetActive(true);
        }
    }

    public void ClearKeys()
    {
        redKey.SetActive(false);
        greenKey.SetActive(false);
        blueKey.SetActive(false);
    }

}
