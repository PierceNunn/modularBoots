using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BasicStatModifier
{
    [SerializeField] private Enums.modifiableStats _statToModify;
    [SerializeField] private Enums.operators _operator;
    [SerializeField] private float _modifierValue;

    public Enums.modifiableStats StatToModify { get => _statToModify; set => _statToModify = value; }
    public Enums.operators Operator { get => _operator; set => _operator = value; }
    public float ModifierValue { get => _modifierValue; set => _modifierValue = value; }

    public override string ToString()
    {
        return "Modifying " + _statToModify + "; " + _operator + " by " + _modifierValue;
    }
}
