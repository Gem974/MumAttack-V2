using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    SceneManager.LoadScene(0);
        //}
    }


    public void LoadSpecificScene(int minigameIndex)
    {
        SceneManager.LoadScene(minigameIndex);
    }

    public void SetRandomScene()
    {
        for (int i = 0; i < 3; i++)
        {
            int randomInt = Random.Range(1, 4);
            Debug.Log(randomInt);

            if (_randomScene.Contains(randomInt))
            {
                //Debug.Log("Pas Bon");
                while (_randomScene.Contains(randomInt))
                {
                    randomInt = Random.Range(1, 4);
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

    

    
}
