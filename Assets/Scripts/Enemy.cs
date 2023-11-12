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
    private float enemyHealth = 3f; //variable for enemy health

    public GameObject gunHitEffect;

    // Start is called before the first frame update
    void Start()
    {
        spriteAnim = GetComponentInChildren<Animator>();
        angleToPlayer = GetComponent<AngleToPlayer>();

        enemyManager = FindObjectOfType<EnemyManager>();  
        _interactSprite.gameObject.SetActive(false);

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

        if ((enemyHealth <= 2f) && (!_interactSprite.gameObject.activeSelf))
        {
            _interactSprite.gameObject.SetActive(true);
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
}
