using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerBehaviour : MonoBehaviour
{
    [Header("GameManagerBehaviour")]
    public bool _canPlay;
    public GameObject _GOPanel;

    public static GameManagerBehaviour instancePrime;

    private void Awake()
    {
        if (instancePrime != null)
        {
            Debug.LogError("Il y a plus d'une instance de GameManager dans la scène");
            return;
        }

        instancePrime = this;
    }

    public virtual void GameOver()
    {
        PauseGame.instance.CanTPause();
        _canPlay = false;
        _GOPanel.SetActive(true);
       

    }
}
