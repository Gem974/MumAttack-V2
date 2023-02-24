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
        MetaGameManager.instance._gameMode = MetaGameManager.GameMode.BroadCast;

        if (step == 1)
        {
            SceneManager.LoadScene(scenesManager.instance._randomScene[1]);
        }
        else if (step == 2)
        {

            SceneManager.LoadScene(scenesManager.instance._randomScene[2]);
        }
        else if (step == 3)
        {
            SceneManager.LoadScene(scenesManager.instance._randomScene[3]);

        }
    }

    

    
}
