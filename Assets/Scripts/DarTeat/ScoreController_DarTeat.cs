 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreController_DarTeat : MonoBehaviour
{
    public int _currentScorePlayer1, _currentScorePlayer2 = 0;
    public int _numberOfTryingPlayer1, _numberOfTryingPlayer2;
    public int _successfulTeatsThrowingPlayer1, _successfulTeatsThrowingPlayer2;

    public TextMeshProUGUI _winner;

    public TextMeshProUGUI _currentScoresText;

    public TextMeshProUGUI _resultCurrentScoreTextPlayer1, _resultCurrentScoreTextPlayer2;
    public TextMeshProUGUI _resultNumberOfTryingPlayer1, _resultNumberOfTryingPlayer2;
    public TextMeshProUGUI _resultSuccessfulTeatsThrowingPlayer1, _resultSuccessfulTeatsThrowingPlayer2;

    public static ScoreController_DarTeat instance;

    private void Start()
    {
        if (instance != null)
            Debug.Log("Il y a déjà une instance dans la scène.");

        instance = this;
    }

    public void AddValue(int value, int playerId)
    {
        if(playerId == 1)
        {
            _currentScorePlayer1 += value;
        }
        else if(playerId == 2)
        {
            _currentScorePlayer2 += value;
        }

        _currentScoresText.text = string.Format("{0} | {1}", _currentScorePlayer1, _currentScorePlayer2);
    }

    public void UpdateResultValue()
    {
        if (_currentScorePlayer1 > _currentScorePlayer2)
            _winner.text = "Winner is Player1";
        else
            _winner.text = "Winner is Player2";

        _resultCurrentScoreTextPlayer1.text = _currentScorePlayer1 + "";
        _resultCurrentScoreTextPlayer2.text = _currentScorePlayer2 + "";

        _resultNumberOfTryingPlayer1.text = _numberOfTryingPlayer1 + "";
        _resultNumberOfTryingPlayer2.text = _numberOfTryingPlayer2 + "";

        _resultSuccessfulTeatsThrowingPlayer1.text = _successfulTeatsThrowingPlayer1 + "";
        _resultSuccessfulTeatsThrowingPlayer2.text = _successfulTeatsThrowingPlayer2 + "";
    }
}
