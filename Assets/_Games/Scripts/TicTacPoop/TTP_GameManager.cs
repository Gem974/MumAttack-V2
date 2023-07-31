using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TTP_GameManager : GameManagerBehaviour
{
    public static TTP_GameManager instance;
    
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Il y a plus d'une instance de TTP_GameManager dans la scene");
            return;
        }
        instance = this;
    }
    
    void Start()
    {
        _canPlay = false;
        PauseGame.instance.CanTPause();

        //Attribution de la bombe en debut de partie
        var id = Random.Range(0, _players.Length);
        _players[id].GetComponent<TTP_Player>()._hasBomb = true;
    }

    //Called just After the Discount
    public override void StartGameAfterDiscount()
    {
        base.StartGameAfterDiscount();
        TTP_Timer.instance.StartInGameTimer();
    }

    // Sert a corriger les erreurs lors du spamming de touche dans le tuto. Appeler par message par le script DiscountBeforeStart 
    public override void TutoPreparationFinish()
    {
        base.TutoPreparationFinish();
    }

    public override void GameOver()
    {
        base.GameOver();

    }
}




