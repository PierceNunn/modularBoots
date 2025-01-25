using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private Enums.projectileBehaviors _projectileBehavior;
    [SerializeField] private float _projectileDamage;
    [SerializeField] private float _projectileSpeed;

    delegate float modifierDelegate(float modifiedVar, float modValue);

    public void Start()
    {
        gameObject.GetComponent<Rigidbody>().AddForce(gameObject.transform.forward * _projectileSpeed);
        print(gameObject.GetComponent<Rigidbody>().velocity);
    }

    public void ApplyModifiers(Queue<BasicStatModifier> mods)
    {
        for(int i = 0; i < mods.Count; i++)
        {
            ApplyModifier(mods.Dequeue());
        }
    }

    public void ApplyModifier(BasicStatModifier mod)
    {
        modifierDelegate modDeg = null;

        switch(mod.Operator)
        {
            case Enums.operators.add:
                modDeg = Add;
                break;
            case Enums.operators.subtract:
                modDeg = Subtract;
                break;
            case Enums.operators.multiply:
                modDeg = Multiply;
                break;
            case Enums.operators.divide:
                modDeg = Divide;
                break;
            default:
                Debug.LogError("modifier operator is invalid!");
                break;
        }

        switch(mod.StatToModify)
        {
            case Enums.modifiableStats.speed:
                _projectileSpeed = modDeg(_projectileSpeed, mod.ModifierValue);
                break;
            case Enums.modifiableStats.damage:
                _projectileDamage = modDeg(_projectileDamage, mod.ModifierValue);
                break;
        }
    }

    public float Add(float fOne, float fTwo)
    {
        return fOne + fTwo;
    }

    public float Subtract(float fOne, float fTwo)
    {
        return fOne - fTwo;
    }

    public float Multiply(float fOne, float fTwo)
    {
        return fOne * fTwo;
    }

    public float Divide(float fOne, float fTwo)
    {
        return fOne / fTwo;
    }

}
