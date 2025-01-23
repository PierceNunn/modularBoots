using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerModsHandler modsHandler;

    public void Start()
    {
        modsHandler = gameObject.GetComponent<PlayerModsHandler>();
    }
    public void OnFire()
    {
        modsHandler.FireWeapon();
    }
}
