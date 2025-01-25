using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GunModManagerUI : MonoBehaviour
{
    [SerializeField] private GenericMod[] _availableMods;
    [SerializeField] private Button[] _modButtons;
    [SerializeField] private Image[] _currentMods;

    private PlayerModsHandler modsHandler;

    public void Start()
    {
        modsHandler = FindObjectOfType<PlayerModsHandler>();
        UpdateModDisplay();
    }

    public void UpdateModDisplay()
    {
        UpdateAvailableModButtons();
        UpdateCurrentModsDisplay();
    }

    private void UpdateCurrentModsDisplay()
    {
        for(int i = 0; i < _currentMods.Length; i++)
        {
            if(modsHandler.ModLayout[i] == null)
            {
                _currentMods[i].sprite = null;
            }
            else
            {
                _currentMods[i].sprite = modsHandler.ModLayout[i].ModIcon;
            }
        }
    }

    private void UpdateAvailableModButtons()
    {
        for (int i = 0; i < _modButtons.Length; i++)
        {
            if (i > _availableMods.Length)
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
        UpdateModDisplay();
    }
}
