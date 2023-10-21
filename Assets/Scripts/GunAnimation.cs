using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAnimation : MonoBehaviour
{
    // Start is called before the first frame update

    bool isWalking;
    public Animator gunAnim;


    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        isWalking = GetComponentInParent<PlayerMove>().isWalking;
        gunAnim.SetBool("isWalking", isWalking);
    }
}
