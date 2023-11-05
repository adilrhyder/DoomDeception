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
    [SerializeField] private SpriteRenderer _interactSprite;

    private Transform playersTransform;

    private const float INTERACT_DISTANCE = 5f;

    private void Start()
    {
        playersTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        // if (Keyboard.current.eKey.wasPressedThisFrame)
        // {
        //    //interact method called 
        // }
        if ((Input.GetKeyDown("e")) && (IsWithinInteractDistance()))
        {
            Interact();
            //call interact method
        }

        if ((_interactSprite.gameObject.activeSelf) && (!IsWithinInteractDistance()))
        {
            //turn off interact sprite if not in range of NPC
            _interactSprite.gameObject.SetActive(false);
        }
        else if ((!_interactSprite.gameObject.activeSelf) && (IsWithinInteractDistance()))
        {
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
