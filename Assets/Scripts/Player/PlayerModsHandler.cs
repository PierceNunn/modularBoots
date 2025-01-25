using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModsHandler : MonoBehaviour
{
    [SerializeField] private GenericMod[] _modLayout;
    [SerializeField] private GameObject _projectileSpawnLocation;


    public GenericMod[] ModLayout { get => _modLayout; set => _modLayout = value; }

    public void FireWeapon()
    {
        Queue<BasicStatModifier> statModifierQueue = new Queue<BasicStatModifier>();

        for (int i = 0; i < _modLayout.Length; i++)
        {
            if (_modLayout[i] == null)
                continue;

            if (_modLayout[i].GetType() == typeof(ProjectileMod))
            {
                ProjectileMod testFireMod = _modLayout[i] as ProjectileMod;

                GameObject proj = Instantiate(testFireMod.Projectiles[0], _projectileSpawnLocation.transform.position, _projectileSpawnLocation.transform.rotation);
                proj.GetComponent<ProjectileController>().ProjectileSpawner = gameObject;
                proj.GetComponent<ProjectileController>().ApplyModifiers(statModifierQueue);
                proj.GetComponent<ProjectileController>().Fire();

                for (int j = 0; j < statModifierQueue.Count; j++)
                {
                    print(statModifierQueue.Dequeue());
                }
                statModifierQueue = new Queue<BasicStatModifier>();
            }
            else if (_modLayout[i].GetType() == typeof(StatMod))
            {
                StatMod s = _modLayout[i] as StatMod;
                foreach(BasicStatModifier bsm in s.StatModifiers)
                {
                    statModifierQueue.Enqueue(bsm);
                }
            }
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
}
