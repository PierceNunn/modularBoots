using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private Enums.projectileBehaviors _projectileBehavior;
    [SerializeField] private float _projectileDamage;
    [SerializeField] private float _projectileSpeed;

    public void Start()
    {
        gameObject.GetComponent<Rigidbody>().AddForce(Vector3.forward * _projectileSpeed);
        print(gameObject.GetComponent<Rigidbody>().velocity);
    }
}
