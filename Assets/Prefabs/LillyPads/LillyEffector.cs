using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LillyEffector : MonoBehaviour
{
    private Rigidbody playerRB;
    [SerializeField] private Collider lillyCollider;

    private void Start()
    {
        lillyCollider = GetComponent<Collider>();
        playerRB = FindAnyObjectByType<PlayerController>().GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (playerRB.velocity.y > 0)
            {
                //going up
                lillyCollider.isTrigger = true;
            }
            else
            {
                lillyCollider.isTrigger = false;
            }
        }
    }


    private void OnCollisionExit(Collision Collision)
    {
        if (Collision.gameObject.CompareTag("Player"))
        {
            lillyCollider.isTrigger = true;
        }
    }
}
