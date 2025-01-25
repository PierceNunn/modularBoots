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
        UpdateModDisplay();
    }

    public void UpdateModDisplay()
    {
        for(int i = 0; i < _modButtons.Length; i++)
        {
            if(i > _availableMods.Length)
            {
                _modButtons[i].gameObject.SetActive(false);
            }
            else
            {
                _modButtons[i].gameObject.SetActive(true);
                _modButtons[i].gameObject.GetComponent<Image>().sprite = _availableMods[i].ModIcon;
                _modButtons[i].gameObject.GetComponentInChildren<TextMeshProUGUI>().text = _availableMods[i].ModName;
            }
        }
    }

    public void AddMod(int arrayPos)
    {
        modsHandler.AddModToLoadout(_availableMods[arrayPos]);
    }
}
