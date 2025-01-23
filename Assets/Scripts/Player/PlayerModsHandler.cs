using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModsHandler : MonoBehaviour
{
    [SerializeField] private GenericMod _testMod;
    [SerializeField] private GameObject _projectileSpawnLocation;

    public GenericMod TestMod { get => _testMod; set => _testMod = value; }

    public void FireWeapon()
    {
        if(_testMod.GetType() == typeof(ProjectileMod))
        {
            ProjectileMod testFireMod = _testMod as ProjectileMod;
            Instantiate(testFireMod.Projectiles[0], _projectileSpawnLocation.transform.position, Quaternion.identity);
        }
    }
}
