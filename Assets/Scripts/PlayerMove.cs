using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //variables to help us control the player
    public float playerSpeed = 10f;
    public float momentumDamping = 5f;  //to add momentum to character movement (classic doom)

    //variable to access character controller component
    private CharacterController myCC;

    //variable to access head bob animation (public variables can be assigned values in the inspector)
    public Animator camAnim;

    //boolean to keep track of whether character is walking
    private bool isWalking;

    //use vector3 variables to store player movement in x and y (and will store variable for gravity separately)
    private Vector3 inputVector;        //for passing to GetInput
    private Vector3 movementVector;     //for passing to MovePlayer

    private float myGravity = -10f; //for adjusting z

    // Start is called before the first frame update
    void Start()
    {
        myCC = GetComponent<CharacterController>();   
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();             //function gets input
        MovePlayer();           //function moves player
        
        camAnim.SetBool("isWalking", isWalking);    //function to use animator
    }

    void GetInput()
    {
        //creating input vector (this is what happens when we hold the keys)
        if (Input.GetKey(KeyCode.W) || 
            Input.GetKey(KeyCode.A) ||
            Input.GetKey(KeyCode.S) ||
            Input.GetKey(KeyCode.D))
        {
            //if we're holding down wasd, we'll get (-1,0,1)
            inputVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")); //getting coordinates of player model that are all between 0 and 1 
            inputVector.Normalize();                                                                     //normalize to return unit changes of more than 1 to within 1 and 0 (keeps speed constant)
            inputVector = transform.TransformDirection(inputVector);                                     //to make the player move in the direction the player is facing
        
            isWalking = true;   //for headbob check
        }
        else
        {
            //when we release the keys, we want some momentum to carry over to the character
            // if we're not pressing the buttons give us whatever inputVector was at when it was last checked and lerp it towards zero 
            inputVector = Vector3.Lerp(inputVector, Vector3.zero, momentumDamping * Time.deltaTime);
        
            isWalking = false;  //for headbob check
        }

        movementVector = (inputVector * playerSpeed) + (Vector3.up * myGravity);                     //Vector3.up is unity's y-axis; movementVector tells us how fast and in what direction a player should move
    }

    void MovePlayer()
    {
        myCC.Move(movementVector * Time.deltaTime);     //moving character controller by calculated value, multiplied with change in time to make transition smooth
    }

}
