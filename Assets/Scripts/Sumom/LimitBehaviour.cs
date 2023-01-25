using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitBehaviour : MonoBehaviour
{
    public GameObject _p1, _p2;
    public Sumo_GameManager _gameManager;
    
    private void Start()
    {
        _gameManager = FindObjectOfType<Sumo_GameManager>();
    }

    private void OnTriggerEnter(Collider collision) 
    {
        if (_gameManager._canPlay)
        {
            if (collision.gameObject == _p1)
            {
                //Debug.Log("Joueur 1 Wins");
                _gameManager._pointsP2++;
                _gameManager._canPlay = false;
                _gameManager.PreRestartVoid();
                
            }
            else if (collision.gameObject == _p2)
            {
                //Debug.Log("Joueur 2 Wins");
                _gameManager._pointsP1++;
                _gameManager._canPlay = false;
                _gameManager.PreRestartVoid();

            } 
        }
    }
}
