using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scenesManager : MonoBehaviour
{
    public int[] _randomScene;

    private void Start()
    {
        LoadRandomScene();
    }
    public void LoadSpecificScene(string minigameIndex)
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
