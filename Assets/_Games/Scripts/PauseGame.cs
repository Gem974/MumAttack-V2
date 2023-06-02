using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PauseGame : MonoBehaviour
{
    [SerializeField] GameObject _pausePanel;
    [SerializeField] Button _backBtn;
    public bool _canPause;
    public bool _pausable;


    public static PauseGame instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Il y a plus d'une instance de GameManager dans la scène");
            return;
        }

        instance = this;
    }

    void Start()
    {
        _pausePanel.SetActive(false);
        _canPause = true;
        _pausable = false;
    }


    void Update()
    {
        if (_pausable)
        {
            if (_canPause)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    _canPause = false;
                    _pausePanel.SetActive(true);
                    _backBtn.Select();
                    UnityEngine.Time.timeScale = 0;

                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Start"))
                {
                    _canPause = true;
                    _pausePanel.SetActive(false);


                    UnityEngine.Time.timeScale = 1;

                }
            } 
        }


    }

    public void CanPause()
    {
        _pausable = true;
     
    }

    public void CanTPause()
    {
        _pausable = false;
    }

    public void PauseBackToMenu()
    {
        scenesManager.instance.PauseBackToMainMenu();
    }
}