using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    public float sensitivity = 7.5f;
    public float smoothing = 1.5f;

    private float xMousePos;
    private float smoothedMousePos;

    private float currentLookingPos;

    // Start is called before the first frame update
    private void Start()
    {
       //lock and hide the cursor so player can move around
       Cursor.lockState = CursorLockMode.Locked;
       Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();         //function to get input
        ModifyInput();      //modifying input
        MovePlayer();       //moving player
    }

    void GetInput()
    {
        xMousePos = Input.GetAxisRaw("Mouse X");    //getting x-coordinate of mouse
    }

    void ModifyInput()
    {
        xMousePos *= sensitivity * smoothing;       //calculating new xMouse coordinate factoring in sensitivity and smoothing
        smoothedMousePos = Mathf.Lerp(smoothedMousePos, xMousePos, 1f/smoothing);   //Lerp function eases transitions of values for smooth camera movement
    }

    void MovePlayer()
    {
        currentLookingPos += smoothedMousePos;      //updating current looking position with smoothed Mouse Pos
        transform.localRotation = Quaternion.AngleAxis(currentLookingPos, transform.up);    //transform axial view of function
    }
}
