using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameMode
    {
        Emission, Libre
    }

    public GameMode _gameMode;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_gameMode == GameMode.Emission)
        {
            Emission();
        }
        else if (_gameMode == GameMode.Libre)
        {
            Libre();
        }
    }

    void Emission()
    {

    }

    void Libre()
    {

    }
}
