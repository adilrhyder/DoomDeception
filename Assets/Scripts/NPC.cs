using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
// using UnityEngine.InputSystem;

//this is a base for any dialogue-enabled NPC in the game
//abstract classes are blank methods without any implementation (meant to be inherited)
//when we import an interface, we have to implement the methods contained therein
public abstract class NPC : MonoBehaviour, IInteractable
{
    [SerializeField] protected SpriteRenderer _interactSprite;

    protected Transform playersTransform;

    //protected variable for talkable enemy to access (when enemy is defeated, only THEN should you be able to talk to it)
    public bool isDefeated = true;

    //variable for storing whether this enemy is talkable
    public bool isTalkable;

    //flag to recognize final boss (should be talkable before attack)
    public bool isFinalBoss;

    //flag to recognize intercomms
    public bool isIntercomm;

    private const float INTERACT_DISTANCE = 5f;

    virtual protected void Start()
    {
        playersTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    virtual protected void Update()
    {
        // Debug.Log("Value of isDefeated: " + isDefeated);
        // if (Keyboard.current.eKey.wasPressedThisFrame)
        // {
        //    //interact method called 
        // }
        if ((Input.GetKeyDown("e")) && (IsWithinInteractDistance()) && (isDefeated || isIntercomm))
        {
            Interact();
            //call interact method
        }

        // Debug.Log("")
        if ((_interactSprite.gameObject.activeSelf) && (!IsWithinInteractDistance()) && (!isDefeated))
        {
            // Debug.Log("Turning off");
            //turn off interact sprite if not in range of NPC
            _interactSprite.gameObject.SetActive(false);
        }
        else if ((!_interactSprite.gameObject.activeSelf) && ((IsWithinInteractDistance() && isIntercomm) || (isDefeated)))
        {
            // Debug.Log("Turning on");
            //turn on interact sprite if in range of NPC
            _interactSprite.gameObject.SetActive(true);
        }
    }

    //cannot add abstract methods to class unless class itself is abstract
    public abstract void Interact();

    private bool IsWithinInteractDistance()
    {
        var dist = Vector3.Distance(transform.position, playersTransform.position);

        if (dist < INTERACT_DISTANCE)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}