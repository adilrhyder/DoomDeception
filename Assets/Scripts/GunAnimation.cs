using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAnimation : MonoBehaviour
{
    // Start is called before the first frame update

    bool isWalking;
    bool isFiring;
    public Animator gunAnim;


    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        isFiring = GetComponentInParent<Gun>().isFiring;
        gunAnim.SetBool("isFiring", isFiring);
            
        if (!isFiring)
        {
            isWalking = GetComponentInParent<PlayerMove>().isWalking;
            gunAnim.SetBool("isWalking", isWalking);
        }
    }
}
