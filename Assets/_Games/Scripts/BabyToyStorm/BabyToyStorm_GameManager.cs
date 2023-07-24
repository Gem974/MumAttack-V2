using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyToyStorm_GameManager : GameManagerBehaviour
{

    [SerializeField] int _scoreP1, _scoreP2;
 


    public static BabyToyStorm_GameManager instance;
    

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Il y a plus d'une instance de BabyToyStorm_GameManager dans la scène");
            return;
        }
        instance = this;

       
    }


    void Start()
    {
        _canPlay = false;
        PauseGame.instance.CanTPause();
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    //Called just After the Discount
    public override void StartGameAfterDiscount()
    {
        Timer.instance.LaunchTimer();
        SpawnRandomObject.instance.StartSpawn();
        _canPlay = true;
        PauseGame.instance.CanPause();
    }

    // Sert a corriger les erreurs lors du spamming de touche dans le tuto. Appeler par message par le script DiscountBeforeStart 
    public override void TutoPreparationFinish()
    {

        foreach (var player in _players)
        {
            player.ChangeActionMap();
        }
    }

    public void AddPoint(bool isPlayer1, int howMuchPoints)
    {
        if (isPlayer1)
        {
            _scoreP1 += howMuchPoints;
        }
        else
        {
            _scoreP2 += howMuchPoints;

        }

    }

    public override void GameOver()
    {
        base.GameOver();
        if (_scoreP1 > _scoreP2)
        {
            GameOverBehaviour.instance.PlayerToWin(1);
        }
        else
        {
            GameOverBehaviour.instance.PlayerToWin(2);
        }


    }
}




