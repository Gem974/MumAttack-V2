using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using META;

public class scenesManager : MonoBehaviour
{

    public bool _isNotDestroyable;
    
    public List<int> _randomScene = new List<int>();

    public static scenesManager instance;

    

    private void Awake()
    {
        if (_isNotDestroyable)
        {
            if (instance != null)
            {
                return;
            }

            instance = this; 
        }
    }

    private void Start()
    {
        if (_isNotDestroyable)
        {
            SetRandomScene(); 

        }
    }
    public void LoadSpecificScene(int minigameIndex)
    {
        SceneManager.LoadScene(minigameIndex);
    }

    public void SetRandomScene()
    {
        for (int i = 0; i < 3; i++)
        {
            int randomInt = Random.Range(2, 5);
            Debug.Log(randomInt);

            if (_randomScene.Contains(randomInt))
            {
                //Debug.Log("Pas Bon");
                while (_randomScene.Contains(randomInt))
                {
                    randomInt = Random.Range(2, 5);
                }
                _randomScene.Add(randomInt);
            }
            else if (!_randomScene.Contains(randomInt))
            {
                Debug.Log("Bon");
                _randomScene.Add(randomInt);

            }

        }



    }

    public void LoadBroadcastScene(int step)
    {
        
        if (MetaGameManager.instance._currentStep < MetaGameManager.instance._maxStep) //En cours de BroadCast
        {
            Debug.Log("Step= " + step);
            if (MetaGameManager.instance._gameMode == MetaGameManager.GameMode.None)
            {
                MetaGameManager.instance.BroadCastNextStep();
                MetaGameManager.instance._gameMode = MetaGameManager.GameMode.BroadCast;
            }

            SceneManager.LoadScene(scenesManager.instance._randomScene[step - 1]);

        }
        else if (MetaGameManager.instance._currentStep >= MetaGameManager.instance._maxStep) //Fin du BroadCast
        {
            _randomScene.Clear();
            SetRandomScene();
            MetaGameManager.instance.ResetAll();
            LoadSpecificScene(10);
        }
    }


    public void PauseBackToMainMenu()
    {
        _randomScene.Clear();
        SetRandomScene();
        MetaGameManager.instance.ResetAll();
        LoadSpecificScene(0);
    }


    public void SetToFreeBrawl()
    {
        META.MetaGameManager.instance._gameMode = MetaGameManager.GameMode.FreeBrawl;
    }

    public void SetToBroadCast()
    {
        META.MetaGameManager.instance._gameMode = MetaGameManager.GameMode.BroadCast;
    }

    public void Quit()
    {
        Application.Quit();
    }

    

    
}
