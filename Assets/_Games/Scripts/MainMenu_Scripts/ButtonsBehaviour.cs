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
    public Slider _generalSlider, _sfxSlider, _musicSlider, _voiceSlider;

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

    public void ChangeGeneralVolume(float volume)
    {
        volume = _generalSlider.value;
        _mixer.SetFloat("GeneralVolume", volume);
    }

    public void ChangeSFXVolume(float volume)
    {
        volume = _sfxSlider.value;
        _mixer.SetFloat("SFXVolume", volume);
    }

    public void ChangeVoiceVolume(float volume)
    {
        volume = _voiceSlider.value;
        _mixer.SetFloat("VoiceVolume", volume);
    }

    public void ChangeMusicVolume(float volume)
    {
        volume = _musicSlider.value;
        _mixer.SetFloat("MusicVolume", volume);
    }
}
