using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerBehaviour : MonoBehaviour
{
    [Header("GameManagerBehaviour")]
    public bool _canPlay;
    public GameObject _GOPanel;
    public PlayerBehaviour[] _players;

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

    public virtual void TutoPreparationFinish()
    {

        foreach (var player in _players)
        {
            player.ChangeActionMap();
        }
    }

    public virtual void StartGameAfterDiscount()
    {
        _canPlay = true;
        PauseGame.instance.CanPause();

    }

    public virtual void GameOver()
    {
        PauseGame.instance.CanTPause();
        _canPlay = false;
        _GOPanel.SetActive(true);
       

    }
}
