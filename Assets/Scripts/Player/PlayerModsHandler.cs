using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModsHandler : MonoBehaviour
{
    [SerializeField] private GenericMod[] _modLayout;
    [SerializeField] private GameObject _projectileSpawnLocation;

    private bool noPendingCooldown = true;
    private float remainingCooldown = 0f;
    private int[] modGroups;
    private PlayerResources pr;


    public GenericMod[] ModLayout { get => _modLayout; set => _modLayout = value; }
    public bool NoPendingCooldown { get => noPendingCooldown; set => noPendingCooldown = value; }
    public float RemainingCooldown { get => remainingCooldown; set => remainingCooldown = value; }
    public int[] ModGroups { get => modGroups; set => modGroups = value; }

    public void Start()
    {
        pr = gameObject.GetComponent<PlayerResources>();
    }

    public void UpdateModGroups()
    {
        ModGroups = new int[_modLayout.Length];
        int currentGroup = 0;
        for(int i = 0; i < ModGroups.Length; i++)
        {
            ModGroups[i] = currentGroup;
            if (_modLayout[i] != null && _modLayout[i].GetType() == typeof(ProjectileMod))
                currentGroup++;
        }
    }

    public string FireWeapon(bool testFire = false)
    {
        string output = "";
        int projectileCount = 1;
        if (NoPendingCooldown)
        {
            Queue<BasicStatModifier> statModifierQueue = new Queue<BasicStatModifier>();
            float accumulatedCooldown = 0f;
            float accumulatedAmmoCost = 0f;
            float shownCooldown = 0f;

            for (int i = 0; i < _modLayout.Length; i++)
            {
                if (_modLayout[i] == null)
                    continue;

                if (_modLayout[i].GetType() == typeof(ProjectileMod))
                {
                    ProjectileMod testFireMod = _modLayout[i] as ProjectileMod;

                    accumulatedCooldown += testFireMod.Cooldown;
                    accumulatedAmmoCost += testFireMod.AmmoCost;

                    (string, float) g = testFireMod.DetailedPostModInfoString(statModifierQueue, accumulatedCooldown);

                    output = output + "\nProjectile " + projectileCount + ":\n" + g.Item1;
                    projectileCount++;
                    shownCooldown += g.Item2;

                    

                    if(!testFire)
                    {
                        if (!pr.SpendAmmo(accumulatedAmmoCost))
                        {
                            print("cannot fire, not enough ammo");
                            return output;
                        }
                        accumulatedAmmoCost = 0;

                        StopAllCoroutines();
                        StartCoroutine(WeaponCooldownTimer(accumulatedCooldown));
                        accumulatedCooldown = 0;

                        GameObject proj = Instantiate(testFireMod.Projectiles[0], _projectileSpawnLocation.transform.position, _projectileSpawnLocation.transform.rotation);
                        proj.GetComponent<ProjectileController>().ProjectileSpawner = gameObject;
                        proj.GetComponent<ProjectileController>().ApplyModifiers(statModifierQueue);
                        proj.GetComponent<ProjectileController>().Fire();
                    }
                    

                    for (int j = 0; j < statModifierQueue.Count; j++)
                    {
                        print(statModifierQueue.Dequeue());
                    }
                    statModifierQueue = new Queue<BasicStatModifier>();

                    
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
            if (!testFire)
                AudioManager.Instance.PlaySFX("Gun Shot");
            return output + "\nTotal Cooldown: " + shownCooldown + "\nTotal Ammo Cost: " + accumulatedAmmoCost;
        }
        else
        {
            return "can't fire, waiting on cooldown";
        }

        
    }

    public bool AddModToLoadout(GenericMod mod, bool addInReverse = false)
    {
        if(!addInReverse)
        {
            //this was the original code for adding a mod, leaving in just in case
            for (int i = 0; i < _modLayout.Length; i++)
            {
                if (_modLayout[i] == null)
                {
                    _modLayout[i] = mod;
                    print("added mod in position" + i);
                    return true;
                }
            }
            print("failed to add mod");
            return false;
        }
        else
        {
            if(_modLayout[_modLayout.Length - 1] != null)
            {
                Debug.LogWarning("Can't add mod, layout is full");
                return false;
            }
            
            GenericMod[] temp = new GenericMod[_modLayout.Length];

            temp[0] = mod;
            for(int i = 1; i < temp.Length; i++)
            {
                temp[i] = _modLayout[i - 1];
            }
            _modLayout = temp;
            return true;
        }
        
    }

    public bool RemoveModFromLoadout(int index)
    {
        GenericMod[] temp = new GenericMod[_modLayout.Length];

        if (index >= _modLayout.Length || _modLayout[index] == null)
            return false;

        for(int i = 0; i < _modLayout.Length - 1; i++)
        {
            if (i >= index)
                temp[i] = _modLayout[i + 1];
            else
                temp[i] = _modLayout[i];
        }
        _modLayout = temp;
        return true;
    }

    public void ClearMods()
    {
        for (int i = 0; i < _modLayout.Length; i++)
        {
            _modLayout[i] = null;
        }
    }

    public int GetLastBulletModIndex()
    {
        for(int i = _modLayout.Length - 1; i >= 0; i--)
        {
            if (_modLayout[i] != null &&  _modLayout[i].GetType() == typeof(ProjectileMod))
                return i;
        }

        return -1;
    }

    public IEnumerator WeaponCooldownTimer(float cooldownTime)
    {
        NoPendingCooldown = false;
        remainingCooldown += cooldownTime;
        while(remainingCooldown > 0f)
        {
            RemainingCooldown -= Time.deltaTime;
            yield return null;
        }
        RemainingCooldown = 0f;
        NoPendingCooldown = true;
    }
}
