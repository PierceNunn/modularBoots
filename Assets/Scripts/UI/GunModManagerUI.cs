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
    [SerializeField] private GameObject[] _groupingImages;
    [SerializeField] private GameObject _content;

    [SerializeField] private float _groupHeight;
    [SerializeField] private float _groupWidthPerMod;

    private List<RectTransform> groupImages = new List<RectTransform>();

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
            Time.timeScale = 1f;
        }
        else
        {
            FindObjectOfType<PlayerInput>().actions.FindActionMap("UI").Enable();
            FindObjectOfType<PlayerInput>().actions.FindActionMap("Player").Disable();
            _content.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void UpdateModDisplay()
    {
        UpdateAvailableModButtons();
        UpdateCurrentModsDisplay();
        UpdateGroups();
        FindObjectOfType<ExtraInfoWindow>().UpdateInfoDisplay(modsHandler.FireWeapon(true));
    }

    private void UpdateCurrentModsDisplay()
    {
        int lastBulletIndex = modsHandler.GetLastBulletModIndex();
        for(int i = 0; i < _currentMods.Length; i++)
        {
            if(modsHandler.ModLayout[i] == null)
            {
                _currentMods[i].gameObject.GetComponent<ModDisplayController>().UpdateDisplayInfo(null);
                _currentMods[i].color = Color.clear;
            }
            else
            {
                _currentMods[i].gameObject.GetComponent<ModDisplayController>().UpdateDisplayInfo(modsHandler.ModLayout[i]);
                _currentMods[i].color = i > lastBulletIndex ? Color.red : Color.white;
            }
        }
        modsHandler.UpdateModGroups();
        
        
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

    private void UpdateGroups()
    {
        foreach (RectTransform r in groupImages)
            Destroy(r.gameObject);
        groupImages = new List<RectTransform>();

        if (modsHandler.ModLayout[0] == null)
        {
            return;
        }

        int currentGroup = -1;
        for (int i = 0; i < modsHandler.ModGroups.Length; i++)
        {
            if (modsHandler.ModLayout[i] == null)
                return;

            if (modsHandler.ModGroups[i] == currentGroup)
            {
                groupImages[currentGroup].sizeDelta = new Vector2(groupImages[currentGroup].sizeDelta.x + _groupWidthPerMod, groupImages[currentGroup].sizeDelta.y);
            }
            else
            {
                currentGroup++;
                groupImages.Add(Instantiate(_groupingImages[0]).GetComponent<RectTransform>());
                groupImages[currentGroup].transform.SetParent(_content.transform, false);
                groupImages[currentGroup].transform.SetAsFirstSibling();
                //groupImages[currentGroup].pivot = new Vector2(0.5f, 0.5f);
                groupImages[currentGroup].anchoredPosition = _currentMods[i].GetComponent<RectTransform>().anchoredPosition - new Vector2((_currentMods[i].GetComponent<RectTransform>().sizeDelta.x / 2) + 14, 0f);
                //groupImages[currentGroup].pivot = new Vector2(0, 0.5f);
                groupImages[currentGroup].sizeDelta = new Vector2(_groupWidthPerMod, _groupHeight);
                groupImages[currentGroup].localScale = _currentMods[i].GetComponent<RectTransform>().localScale;

                groupImages[currentGroup].gameObject.GetComponent<Image>().color = currentGroup % 2 == 0 ? Color.blue : Color.green;


            }

        }
    }

    public void AddMod(int arrayPos)
    {
        modsHandler.AddModToLoadout(_availableMods[arrayPos], true);
        UpdateModDisplay();
    }

    public void RemoveMod(int arrayPos)
    {
        modsHandler.RemoveModFromLoadout(arrayPos);
        UpdateModDisplay();
    }

    public void ClearMods()
    {
        modsHandler.ClearMods();
        UpdateModDisplay();
    }
}
