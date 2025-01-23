using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class GenericMod : ScriptableObject
{
    [SerializeField] private string modName;
    [SerializeField] private string modDescription;

    public string ModName { get => modName; set => modName = value; }
    public string ModDescription { get => modDescription; set => modDescription = value; }
}
