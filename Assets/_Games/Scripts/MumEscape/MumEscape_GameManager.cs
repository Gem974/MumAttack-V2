using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MumEscape_GameManager : MonoBehaviour
{
    //Variables
    public float _annoyedCounter;
    public GameObject _gameoverPanel;
    public GameObject _light;
    public float _lightOpenTime;
    public bool _isLightOpen, _isGameOver;
    public Slider _annoyedSlider;
    public float _calmingSpeed;
    public Text _winnerText;
    public AudioClip _cryingBabySFX;

    //Singleton
    public static MumEscape_GameManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("One GameManager already exist");
            return;
        }

        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _isGameOver = false;
        _isLightOpen = true;
        _annoyedCounter = 0;
        _annoyedSlider.value = _annoyedCounter;
        ActiveLight(_lightOpenTime);
    }

    private void Update()
    {
        //Decrease annoyed slider over the time  
        if(!_isGameOver)
            _annoyedCounter = Mathf.MoveTowards(_annoyedCounter, 0, Time.deltaTime * _calmingSpeed);

        _annoyedSlider.value = _annoyedCounter;
    }

    //Increase annoyed value
    public void AddCounter(int amount, string name)
    {
        _annoyedCounter += amount;

        //Game Over when the annoyed counter = 100
        if (_annoyedCounter >= 100)
        {
            MumEscape_SoundManager.Instance._sfxAudioSource.PlayOneShot(_cryingBabySFX);
            MumEscape_CameraShake.Instance.Shake(1f, 0.6f, 80f);
            _annoyedSlider.value = 100;

            if(name.Contains("1"))
            {
                GameOver("Player 2");
            }
            else
            {
                GameOver("Player 1");
            }
        }
        else
        {
            _annoyedSlider.value = _annoyedCounter;

        }
    }

    //Game Over Method
    public void GameOver(string winner)
    {
        _isGameOver = true;
        _winnerText.text = winner + " win the game !";
        _gameoverPanel.SetActive(true);
    }

    //Method : Open the light with time parameter
    public void ActiveLight(float lightTime)
    {
        StartCoroutine(ShowLight(lightTime));
    }

    private IEnumerator ShowLight(float lightTime)
    {
        _isLightOpen = true;
        _light.SetActive(true);
        yield return new WaitForSeconds(lightTime);
        _isLightOpen = false;
        _light.SetActive(false);
    }
}
