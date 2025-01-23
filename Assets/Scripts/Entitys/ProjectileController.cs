using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private Enums.projectileBehaviors _projectileBehavior;
    [SerializeField] private float _projectileDamage;
    [SerializeField] private float _projectileSpeed;

    public void Update()
    {
        gameObject.GetComponent<Rigidbody>().velocity = (Vector3.forward * _projectileSpeed * 10000);
        print("ahhh!");
    }
}
