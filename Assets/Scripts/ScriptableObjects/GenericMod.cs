using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class GenericMod : ScriptableObject
{
    [SerializeField] private string modName;
    [SerializeField] private string modDescription;
    [SerializeField] private Sprite _modIcon;

    [SerializeField] private float _ammoCost;
    [SerializeField] private float _cooldown;

    public string ModName { get => modName; set => modName = value; }
    public string ModDescription { get => modDescription; set => modDescription = value; }
    public Sprite ModIcon { get => _modIcon; set => _modIcon = value; }
    public float AmmoCost { get => _ammoCost; set => _ammoCost = value; }
    public float Cooldown { get => _cooldown; set => _cooldown = value; }
}
