using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResources : MonoBehaviour
{
    [SerializeField] private float _maxAmmo;

    private float currentAmmo;

    public float MaxAmmo { get => _maxAmmo; set => _maxAmmo = value; }
    public float CurrentAmmo { get => currentAmmo; set => currentAmmo = value; }

    public void Start()
    {
        CurrentAmmo = MaxAmmo;
    }

    public bool SpendAmmo(float ammoUsed)
    {
        if(currentAmmo >= ammoUsed)
        {
            currentAmmo -= ammoUsed;
            return true;
        }
        return false;
        
    }

    public void RefillAmmo()
    {
        AudioManager.Instance.PlaySFX("Reload");
        currentAmmo = _maxAmmo;
    }
}
