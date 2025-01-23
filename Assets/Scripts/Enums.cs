using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enums : MonoBehaviour
{
    public enum projectileBehaviors
    {
        straight,
        curved
    }

    public enum operators
    {
        add,
        subtract,
        multiply,
        divide
    }

    public enum modifiableStats
    {
        speed,
        damage
    }
}
