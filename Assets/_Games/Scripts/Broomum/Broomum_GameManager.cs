using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Broomum_GameManager : GameManagerBehaviour
{

    
   

    public static Broomum_GameManager instance;
    

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Il y a plus d'une instance de Broomum_GameManager dans la sc�ne");
            return;
        }
        instance = this;
    }
    
    void Start()
    {
        _canPlay = false;
        PauseGame.instance.CanTPause();
    }

    
    void Update()
    {
    
    }

    //Called just After the Discount
    public override void StartGameAfterDiscount()
    {
        base.StartGameAfterDiscount();
    
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




