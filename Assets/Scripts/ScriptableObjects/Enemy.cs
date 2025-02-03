using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class Enemy : ScriptableObject
{
    [SerializeField] public string enemyName;
    [SerializeField] public int health;
    [SerializeField] public float moveSpeed;
    [SerializeField] public float aggroDistance;
    [SerializeField] public float attackInterval;

}
