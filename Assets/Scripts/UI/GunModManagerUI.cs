using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class GunModManagerUI : MonoBehaviour
{
    [SerializeField] private GenericMod[] _availableMods;
    [SerializeField] private Button[] _modButtons;
    [SerializeField] private Image[] _currentMods;
    [SerializeField] private GameObject _content;

    private PlayerModsHandler modsHandler;

    public void Start()
    {
        modsHandler = FindObjectOfType<PlayerModsHandler>();
        UpdateModDisplay();
        _content.SetActive(false);
    }

    public void ToggleModMenu()
    {
        if (_content.activeSelf)
        {
            FindObjectOfType<PlayerInput>().actions.FindActionMap("UI").Disable();
            FindObjectOfType<PlayerInput>().actions.FindActionMap("Player").Enable();
            _content.SetActive(false);
        }
        else
        {
            FindObjectOfType<PlayerInput>().actions.FindActionMap("UI").Enable();
            FindObjectOfType<PlayerInput>().actions.FindActionMap("Player").Disable();
            _content.SetActive(true);
        }
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
                _modButtons[i].gameObject.GetComponent<ModDisplayController>().UpdateDisplayInfo(_availableMods[i]);
            }
        }
    }

    public void AddMod(int arrayPos)
    {
        modsHandler.AddModToLoadout(_availableMods[arrayPos]);
        UpdateModDisplay();
    }

    public void ClearMods()
    {
        modsHandler.ClearMods();
        UpdateModDisplay();
    }
}
