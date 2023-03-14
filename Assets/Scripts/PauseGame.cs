using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour
{
    [SerializeField] GameObject _pausePanel;
    [SerializeField] Button _backBtn;
    bool _canPause;
    void Start()
    {

        _pausePanel.SetActive(false);
        _canPause = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (_canPause)
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Start"))
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

    public void PauseBackToMenu()
    {
        scenesManager.instance.LoadSpecificScene(0);
    }
}