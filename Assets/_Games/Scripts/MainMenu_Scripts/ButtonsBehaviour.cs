using META;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Audio;

public class ButtonsBehaviour : MonoBehaviour
{
    [SerializeField] GameObject _mainMenuPanel, _modePanel, _freebrawlPanel, _optionsPanel;
    public AudioMixer _mixer;
    public Slider _volumeSlider;

    private void Start()
    {
        OffPanels();
        _mainMenuPanel.SetActive(true);
    }


    private void Update()
    {
        
    }

    public void OffPanels()
    {
        _mainMenuPanel.SetActive(false); 
        _modePanel.SetActive(false); 
        _freebrawlPanel.SetActive(false); 
        _optionsPanel.SetActive(false);
    }

    public void ShowPanel(GameObject panelToShow)
    {
        OffPanels();
        panelToShow.SetActive(true);
    }

    public void PauseBackToMenu()
    {
        
        scenesManager sm = FindObjectOfType<scenesManager>();
        sm.LoadSpecificScene(0);
        
    }

    public void SetUIFocus(RectTransform ui)
    {
        EventSystem.current.SetSelectedGameObject(ui.gameObject);
       
    }

    public void SetMiniGameID(int id)
    {
        MetaGameManager.instance._gameID = id;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void DebugButton()
    {
        Debug.Log("Working");
    }

    public void ChangeLangage(int langageID)
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[langageID];
    }

    public void ChangeVolume(float volume)
    {
        volume = _volumeSlider.value;
        _mixer.SetFloat("Volume", volume);
    }
}
