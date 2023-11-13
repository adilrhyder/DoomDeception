using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _interactSprite;


    //list storing enemies (we attach this variable in Unity by dragging and dropping "EnemyManager" in this field FOR EVERY ENEMY)
    public EnemyManager enemyManager;
    private Animator spriteAnim; 
    private AngleToPlayer angleToPlayer;
    private float enemyHealth = 2f; //variable for enemy health

    public GameObject gunHitEffect;
    private float instant_kill_level = 1f; 

    private const float INTERACT_DISTANCE = 5f;
    private Transform playersTransform;


    // Start is called before the first frame update
    void Start()
    {
        spriteAnim = GetComponentInChildren<Animator>();
        angleToPlayer = GetComponent<AngleToPlayer>();

        enemyManager = FindObjectOfType<EnemyManager>();  
        _interactSprite.gameObject.SetActive(false);

        playersTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {

        spriteAnim.SetFloat("spriteRot", angleToPlayer.lastIndex);
        //check if enemy health has dropped to zero
        if (enemyHealth <= 0)
        {
            //remove this object from list 
            enemyManager.RemoveEnemy(this);
            
            //destroy game object 
            Destroy(gameObject);
        }

        if ((enemyHealth <= instant_kill_level) && (!_interactSprite.gameObject.activeSelf))
        {
            _interactSprite.gameObject.SetActive(true);
        }

        if ((Input.GetKeyDown("c")) && (IsWithinInteractDistance()) && (enemyHealth <= instant_kill_level))
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
}
