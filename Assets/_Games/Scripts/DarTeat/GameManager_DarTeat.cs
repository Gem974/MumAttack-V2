using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager_DarTeat : GameManagerBehaviour
{
    
    private PlayerController_DarTeat[] _players;

    [Header("Systems")]
    //public bool _canPlay;
    public int _points1, _points2;

    [Header("UI")]
    public GameObject _GOPanel;
    public Text _playerWinsText;
    public TextMeshProUGUI _pointsTxt1, _pointsTxt2;
    public Animator _HUD;

    public static GameManager_DarTeat instance;

    private void Awake()
    {
        if (instance != null)
            Debug.Log("Il y a plus d'une instance dans la scène");

        instance = this;

        //_panelStartAnimation.SetActive(true);
    }
    private void Start()
    {
        _players = FindObjectsOfType<PlayerController_DarTeat>();
        
    }

    //public void Victory()
    //{
    //    //Update Final Score
    //    ScoreController_DarTeat.instance.UpdateResultValue();
    //    _gameIsFinished = true;
    //    //Show Final Scodd
    //    _panelGameIsFinished.SetActive(true);
    //    //Sounds
    //    SoundManager_DarTeat.instance._soundEffectWinner.PlayOneShot(SoundManager_DarTeat.instance._winnerSound);
    //    SoundManager_DarTeat.instance._soundEffectTheme.Stop();
    //    SoundManager_DarTeat.instance._soundEffectThemeOnGoldTeat.Stop();
    //    SoundManager_DarTeat.instance._soundEffectThemeOnFinish.Play();
    //    Debug.Log("Victory !");
    //}

    public void AddPoints(bool isPlayer1, int multiplier)
    {
        if (isPlayer1)
        {
            _points1 += 1 * multiplier;
            _pointsTxt1.text = _points1.ToString();

        }
        else
        {
            _points2 += 1 * multiplier; ;
            _pointsTxt2.text = _points2.ToString();

        }

        if (_points1 > _points2)
        {
            _HUD.SetBool("P1Stronger", true);
        }
        else if (_points2 > _points1)
        {
            _HUD.SetBool("P1Stronger", false);
        }

    }

    public override void GameOver()
    {
        base.GameOver();
        if (_points1 > _points2)
        {
            _GOPanel.SetActive(true);
            GameOverBehaviour.instance.PlayerToWin(1);
            Debug.Log("P1 Wins");
        }
        else if (_points2 > _points1)
        {
            _GOPanel.SetActive(true);
            GameOverBehaviour.instance.PlayerToWin(2);
            Debug.Log("P2 Wins");
        }
        if (_points1 == _points2)
        {
            _GOPanel.SetActive(true);
            GameOverBehaviour.instance.PlayerToWin(1);
            Debug.Log("P1 Wins");
        }
    }

    // Sert a corriger les erreurs lors du spamming de touche dans le tuto. Appeler par message par le script DiscountBeforeStart 
    public void TutoPreparationFinish()
    {
        foreach (var player in _players)
        {
            player.ChangeActionMap();
        }
    }

    public void StartGameAfterDiscount()
    {
        _canPlay = true;
        PauseGame.instance.CanPause();
        
        TimerBehavior_DarTeat.instance.StartInGameTimer();
    }
}
