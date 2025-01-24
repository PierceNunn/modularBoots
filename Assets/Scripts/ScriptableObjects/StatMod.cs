using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class StatMod : GenericMod
{
    [SerializeField] private BasicStatModifier[] _statModifiers;

    public BasicStatModifier[] StatModifiers { get => _statModifiers; set => _statModifiers = value; }
}
