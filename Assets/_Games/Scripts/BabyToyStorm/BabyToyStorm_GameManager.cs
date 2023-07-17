using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyToyStorm_GameManager : MonoBehaviour
{

    [SerializeField] PlayerBehaviour[] _players;
    [SerializeField] int _scoreP1, _scoreP2;
    // Start is called before the first frame update
    public GameObject _panelGameOver;
    public bool _canPlay;

    public static BabyToyStorm_GameManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Il y a plus d'une instance de GameManager dans la scène");
            return;
        }

        instance = this;
    }


    void Start()
    {
        _canPlay = false;
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    //Called just After the Discount
    public void StartGameAfterDiscount()
    {
        SpawnRandomObject.instance.StartSpawn();
        _canPlay = true;
    }

    // Sert a corriger les erreurs lors du spamming de touche dans le tuto. Appeler par message par le script DiscountBeforeStart 
    public void TutoPreparationFinish()
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

    public void GameOver()
    {
        print("STOP");
    }
}




