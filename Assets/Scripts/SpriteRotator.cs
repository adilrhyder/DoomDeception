using UnityEngine;

public class SpriteRotator : MonoBehaviour
{
    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        //target is player's position
        target = FindObjectOfType<PlayerMove>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        //move sprite billboard to look at player
        transform.LookAt(target);
    }
}
