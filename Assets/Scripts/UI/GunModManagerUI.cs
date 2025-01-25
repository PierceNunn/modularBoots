using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GunModManagerUI : MonoBehaviour
{
    [SerializeField] private GenericMod[] _availableMods;
    [SerializeField] private Button[] _modButtons;

    private PlayerModsHandler modsHandler;

    public void Start()
    {
        modsHandler = FindObjectOfType<PlayerModsHandler>();
    }
}
