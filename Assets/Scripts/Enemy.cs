using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : NPC, ITalkable
{
    public SpriteRenderer _interactSpriteKill;


    //list storing enemies (we attach this variable in Unity by dragging and dropping "EnemyManager" in this field FOR EVERY ENEMY)
    public EnemyManager enemyManager;
    private Animator spriteAnim; 

    private AngleToPlayer angleToPlayer;
    public float enemyHealth; //variable for enemy health
    public float maxEnemyHealth; //variable for enemy health


    public GameObject gunHitEffect;
    private float instant_kill_level = 1f; 

    private const float INTERACT_DISTANCE = 5f;



    // Start is called before the first frame update
    protected override void Start()
    {
        if (isTalkable)
        {
            base.Start();
        }
        spriteAnim = GetComponentInChildren<Animator>();
        angleToPlayer = GetComponent<AngleToPlayer>();

        enemyManager = FindObjectOfType<EnemyManager>();  
        _interactSpriteKill.gameObject.SetActive(false);

        playersTransform = GameObject.FindGameObjectWithTag("Player").transform;

        if (!isFinalBoss)
        {
            isDefeated = false;
        }
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (isTalkable)
        {
            base.Update();
        }

        spriteAnim.SetFloat("spriteRot", angleToPlayer.lastIndex);
        
        //check if enemy health has dropped to zero
        if ((enemyHealth <= 0) && ((!isTalkable) || (isFinalBoss)))
        {
            //remove this object from list 
            enemyManager.RemoveEnemy(this);
            
            //destroy game object 
            Destroy(gameObject);
        }

        //show dialogue prompt after enemy is defeated
        if ((enemyHealth <= 0) && (isTalkable) && (!isFinalBoss))
        {
            isDefeated = true;
        }

        
        Debug.Log("enemyHealth in Enemy: " + enemyHealth);
        Debug.Log("maxEnemyHealth in Enemy: " + maxEnemyHealth);
        Debug.Log("isDefeated: " + isDefeated);
        //to allow for us to talk to enemy before attacking it
        if ((enemyHealth < maxEnemyHealth) && (enemyHealth > 0) && isFinalBoss && (isDefeated))
        {
            // Debug.Log("Fixed isDefeated");
            isDefeated = false;
        }

        //player has seen dialogue prompt and shot again
        if ((enemyHealth <= -1) && (isDefeated))
        {
            //remove this object from list 
            enemyManager.RemoveEnemy(this);
            
            //destroy game object 
            Destroy(gameObject);
        }

        if ((enemyHealth <= instant_kill_level) && (!_interactSpriteKill.gameObject.activeSelf) && (!isTalkable))
        {
            _interactSpriteKill.gameObject.SetActive(true);
        }

        if ((Input.GetKeyDown("c")) && (IsWithinInteractDistance()) && (enemyHealth <= instant_kill_level) && (!isTalkable))
        {
            //instant kill enemy
            //remove this object from list 
            enemyManager.RemoveEnemy(this);
            
            //destroy game object 
            Destroy(gameObject);
        }

        // any animation we call will have updated index
    }

    public void TakeDamage(float damage)
    {
        //apply gun hit effect so enemies die properly
        Instantiate(gunHitEffect, transform.position, Quaternion.identity);
        
        //update enemy health
        enemyHealth -= damage;

        Debug.Log("Enemy Health: " + enemyHealth);
    }

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


    //dialogue stuff
    [SerializeField] private DialogueText dialogueText;
    [SerializeField] private DialogueController dialogueController;

    //since we're importing from NPC, we have to implement the interact function
    public override void Interact()
    {
        Talk(dialogueText);
    }

    public void Talk(DialogueText dialogueText)
    {
        //start conversation
        dialogueController.DisplayNextParagraph(dialogueText);
    }
}
