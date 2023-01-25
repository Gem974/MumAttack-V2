using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonsBehaviour : MonoBehaviour
{
    [SerializeField] GameObject _mainMenuPanel, _miniGamesPanel, _optionsPanel;
    [SerializeField] Outline[] _outlines;
    [SerializeField] int _currentGame; // 0 = MainMenu | 1 = Sumo | 2 = Tank | 3 = Mimique
    
    


    private void Start()
    {
        
        //_outlines[0].enabled = false;
        //_outlines[1].enabled = false;
        //_outlines[2].enabled = false;

        if (_miniGamesPanel)
        {
            _miniGamesPanel.SetActive(false);
            _mainMenuPanel.SetActive(true);
            _optionsPanel.SetActive(false); 
        }
    }


    public void PlayButton()
    {
        
        _miniGamesPanel.SetActive(true);
        _mainMenuPanel.SetActive(false);
        _optionsPanel.SetActive(false);
    }

    public void OptionsButton()
    {   
        _miniGamesPanel.SetActive(false);
        _mainMenuPanel.SetActive(false);
        _optionsPanel.SetActive(true);
    }

    public void QuitButton()
    {
        Debug.Log("App Quit");
        Application.Quit();
    }

    public void BackButton()
    {
        _miniGamesPanel.SetActive(false);
        _mainMenuPanel.SetActive(true);
        _optionsPanel.SetActive(false);
        _outlines[0].enabled = false;
        _outlines[1].enabled = false;
        _outlines[2].enabled = false;
    }

    public void RandomButton()
    {
        Debug.Log("Random");
        StartCoroutine(RandomSystem());
    }

    IEnumerator RandomSystem()
    {
        int tour = 0;
        while (tour < 6)
        {
            yield return new WaitForSeconds(0.1f);
            _outlines[0].enabled = true;
            _outlines[1].enabled = false;
            _outlines[2].enabled = false;
            yield return new WaitForSeconds(0.1f);
            _outlines[0].enabled = false;
            _outlines[1].enabled = true;
            _outlines[2].enabled = false;
            yield return new WaitForSeconds(0.1f);
            _outlines[0].enabled = false;
            _outlines[1].enabled = false;
            _outlines[2].enabled = true;
            tour++;
        }


        int selectedGame = Random.Range(0, 3);
        Debug.Log(selectedGame);
        switch (selectedGame)
        {
            case 0: //Sumo
                _outlines[0].enabled = true;
                _outlines[1].enabled = false;
                _outlines[2].enabled = false;
                yield return new WaitForSeconds(2f);
                _currentGame = 1;
                Debug.Log("Lance Sumo");
                StartGame(1);



                break;

            case 1: // Tank
                _outlines[0].enabled = false;
                _outlines[1].enabled = true;
                _outlines[2].enabled = false;
                yield return new WaitForSeconds(2f);
                Debug.Log("Lance Tank");
                _currentGame = 2;
                StartGame(2);



                break;

            case 2: //Mimique
                _outlines[0].enabled = false;
                _outlines[1].enabled = false;
                _outlines[2].enabled = true;
                yield return new WaitForSeconds(2f);
                Debug.Log("Lance Mimique");
                _currentGame = 3;
                StartGame(3);



                break;

        }



    }

    public void StartGame(int GameSelected)
    {
        GameSelected = _currentGame;

        if (GameSelected == 0)
        {
           
            SceneManager.LoadScene(0); // Main Menu
        }
        else if (GameSelected == 1)
        {
           

            SceneManager.LoadScene(1); // Sumo

        }
        else if (GameSelected == 2)
        {
            

            SceneManager.LoadScene(2); // Tank

        }
        else if (GameSelected == 3)
        {
            

            SceneManager.LoadScene(3); // Mimique

        }

    }

    public void StartSumo()
    {
        SceneManager.LoadScene(1);

    }

    public void StartTank()
    {
        SceneManager.LoadScene(2);

    }

    public void StartMimique()
    {
        SceneManager.LoadScene(3);

    }
    public void StartMainMenu()
    {
        UnityEngine.Time.timeScale = 1;
        SceneManager.LoadScene(0);

    }

    

    


}
