using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModsHandler : MonoBehaviour
{
    [SerializeField] private GenericMod[] _modLayout;
    [SerializeField] private GameObject _projectileSpawnLocation;

    private bool noPendingCooldown = true;
    private PlayerResources pr;


    public GenericMod[] ModLayout { get => _modLayout; set => _modLayout = value; }

    public void Start()
    {
        pr = gameObject.GetComponent<PlayerResources>();
    }

    public void FireWeapon()
    {
        if (noPendingCooldown)
        {
            Queue<BasicStatModifier> statModifierQueue = new Queue<BasicStatModifier>();
            float accumulatedCooldown = 0f;
            float accumulatedAmmoCost = 0f;

            for (int i = 0; i < _modLayout.Length; i++)
            {
                if (_modLayout[i] == null)
                    continue;

                if (_modLayout[i].GetType() == typeof(ProjectileMod))
                {
                    ProjectileMod testFireMod = _modLayout[i] as ProjectileMod;

                    accumulatedCooldown += testFireMod.Cooldown;
                    accumulatedAmmoCost += testFireMod.AmmoCost;

                    if(pr.CurrentAmmo < accumulatedAmmoCost)
                    {
                        print("cannot fire, not enough ammo");
                        return;
                    }

                    GameObject proj = Instantiate(testFireMod.Projectiles[0], _projectileSpawnLocation.transform.position, _projectileSpawnLocation.transform.rotation);
                    proj.GetComponent<ProjectileController>().ProjectileSpawner = gameObject;
                    proj.GetComponent<ProjectileController>().ApplyModifiers(statModifierQueue);
                    proj.GetComponent<ProjectileController>().Fire();

                    for (int j = 0; j < statModifierQueue.Count; j++)
                    {
                        print(statModifierQueue.Dequeue());
                    }
                    statModifierQueue = new Queue<BasicStatModifier>();

                    StartCoroutine(WeaponCooldownTimer(accumulatedCooldown));
                }
                else if (_modLayout[i].GetType() == typeof(StatMod))
                {
                    StatMod s = _modLayout[i] as StatMod;
                    foreach (BasicStatModifier bsm in s.StatModifiers)
                    {
                        statModifierQueue.Enqueue(bsm);
                        
                    }
                    accumulatedCooldown += s.Cooldown;
                    accumulatedAmmoCost += s.AmmoCost;
                }
            }
        }
        else
        {
            print("can't fire, waiting on cooldown");
        }
        
        
    }

    public bool AddModToLoadout(GenericMod mod)
    {
        for(int i = 0; i < _modLayout.Length; i++)
        {
            if(_modLayout[i] == null)
            {
                _modLayout[i] = mod;
                print("added mod in position" + i);
                return true;
            }
        }
        print("failed to add mod");
        return false;
    }

    public void ClearMods()
    {
        for (int i = 0; i < _modLayout.Length; i++)
        {
            _modLayout[i] = null;
        }
    }

    public IEnumerator WeaponCooldownTimer(float cooldownTime)
    {
        noPendingCooldown = false;
        yield return new WaitForSeconds(cooldownTime);
        noPendingCooldown = true;
    }
}
