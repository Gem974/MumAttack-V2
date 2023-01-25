using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scenesManager : MonoBehaviour
{
    
    
    public int[] _randomScene;

    public static scenesManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Il y a plus d'une instance de MetaGameManager dans la scène");
            Destroy(this.gameObject);
            return;
        }

        instance = this;
    }

    private void Start()
    {
        LoadRandomScene();
    }


    public void LoadSpecificScene(int minigameIndex)
    {
        SceneManager.LoadScene(minigameIndex);
    }

    public void LoadRandomScene()
    {
        for (int i = 0; i < 5; i++)
        {
            int randomInt = Random.Range(0, 15);
            Debug.Log(randomInt);
            _randomScene.SetValue(randomInt, i);
        }

    }

    
}
