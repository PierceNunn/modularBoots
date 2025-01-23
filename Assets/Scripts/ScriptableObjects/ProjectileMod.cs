using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ProjectileMod : GenericMod
{
    [SerializeField] private GameObject[] projectiles;

    public GameObject[] Projectiles { get => projectiles; set => projectiles = value; }
}
