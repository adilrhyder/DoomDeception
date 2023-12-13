using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public bool hasRed;
    public bool hasGreen;
    public bool hasBlue;

    public bool hasKilled;

    public bool hasEncountered;

    // Start is called before the first frame update
    void Start()
    {
        CanvasManager.Instance.ClearKeys();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
