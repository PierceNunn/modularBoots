using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DebugManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _debugText;

    private PlayerResources pr;

    void Start()
    {
        pr = FindObjectOfType<PlayerResources>();

    }

    // Update is called once per frame
    void Update()
    {
        _debugText.text = "Current Ammo: " + pr.CurrentAmmo + "/" + pr.MaxAmmo;
    }
}
