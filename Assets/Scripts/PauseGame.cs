using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    [SerializeField] GameObject _pausePanel;
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
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                _canPause = false;
                _pausePanel.SetActive(true);

                UnityEngine.Time.timeScale = 0;
                
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                _canPause = true;
                _pausePanel.SetActive(false);

                UnityEngine.Time.timeScale = 1;

            }
        }
    }
}
