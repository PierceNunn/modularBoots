using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class Enemy : ScriptableObject
{
    [SerializeField] private string enemyName;
    [SerializeField] private int health;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float aggroDistance;

    public float AggroDistance { get => aggroDistance; }

}
