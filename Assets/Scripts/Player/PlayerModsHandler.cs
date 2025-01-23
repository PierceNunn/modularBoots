using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModsHandler : MonoBehaviour
{
    [SerializeField] private GenericMod _testMod;

    public GenericMod TestMod { get => _testMod; set => _testMod = value; }
}
