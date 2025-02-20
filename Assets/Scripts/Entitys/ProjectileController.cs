using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private Enums.projectileBehaviors _projectileBehavior;
    [SerializeField] private float _projectileDamage;
    [SerializeField] private float _projectileSpeed;
    [SerializeField] private bool _destroyedOnCollision = true;
    [SerializeField] private GameObject _impactParticles;

    private GameObject _projectileSpawner;

    private Rigidbody parentRB;

    public GameObject ProjectileSpawner { get => _projectileSpawner; set => _projectileSpawner = value; }
    public Enums.projectileBehaviors ProjectileBehavior { get => _projectileBehavior; set => _projectileBehavior = value; }
    public float ProjectileDamage { get => _projectileDamage; set => _projectileDamage = value; }
    public float ProjectileSpeed { get => _projectileSpeed; set => _projectileSpeed = value; }

    delegate float modifierDelegate(float modifiedVar, float modValue);

    public void Start()
    {
        
    }

    public void Fire()
    {
        parentRB = ProjectileSpawner.GetComponent<Rigidbody>();
        gameObject.GetComponent<Rigidbody>().AddForce(gameObject.transform.forward * _projectileSpeed);
        if(parentRB.velocity.y < 0)
        {
            //falling
            parentRB.velocity = new Vector3(parentRB.velocity.x, 0, parentRB.velocity.z);
            parentRB.AddForce(-gameObject.transform.forward * _projectileSpeed);
        }
        else
        {
            parentRB.AddForce(-gameObject.transform.forward * _projectileSpeed);
        }
    }

    public float ApplyModifiers(Queue<BasicStatModifier> mods, float cooldown = -1)
    {
        var localMods = mods.ToArray();
        float output = cooldown;
        for(int i = 0; i < mods.Count; i++)
        {
            output = ApplyModifier(localMods[i], output);
        }
        return output;
    }

    public float ApplyModifier(BasicStatModifier mod, float cooldown = -1)
    {
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        modifierDelegate modDeg = null;
        float output = cooldown;
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
                ProjectileSpeed = modDeg(ProjectileSpeed, mod.ModifierValue);
                break;
            case Enums.modifiableStats.damage:
                ProjectileDamage = modDeg(ProjectileDamage, mod.ModifierValue);
                break;
            case Enums.modifiableStats.cooldown:
                if (output == -1)
                    FindObjectOfType<PlayerModsHandler>().RemainingCooldown = modDeg(FindObjectOfType<PlayerModsHandler>().RemainingCooldown, mod.ModifierValue);
                else
                    output = modDeg(output, mod.ModifierValue);
                break;
            case Enums.modifiableStats.size:
                gameObject.transform.localScale = new Vector3(modDeg(gameObject.transform.localScale.x, mod.ModifierValue), modDeg(gameObject.transform.localScale.y, mod.ModifierValue), modDeg(gameObject.transform.localScale.z, mod.ModifierValue));
                //testing this out for bouncy bullet
                if(rb.angularDrag != 0)//identifying bouncy bullet based off it's rb
                {
                    rb.mass = modDeg(rb.mass, mod.ModifierValue);
                }
                break;
        }

        return output;
    }

    [System.Obsolete]
    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject == _projectileSpawner)
        {
            
        }
        else if(collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy"))
        {
            if(collision.gameObject.CompareTag("Player"))
            {
                TakeDamage.Instance.PlayHurtVignette();
                AudioManager.Instance.PlaySFX("Take Damage");
            }
            collision.gameObject.GetComponent<HealthSystem>().TakeDamage(_projectileDamage);
        }
        
        if(_destroyedOnCollision && !collision.gameObject.CompareTag("Projectile"))
        {
            try
            {
                Instantiate(_impactParticles, transform.position, transform.rotation).GetComponent<ParticleSystem>().startSpeed = ProjectileSpeed / 5;
            }
            catch
            {
                Debug.LogWarning("No particles associated with projectile!");
            }
            Destroy(gameObject);
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
