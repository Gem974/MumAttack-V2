using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GameManager_IromMum : MonoBehaviour
{
    [Header("Systems")]
    public bool _canPlay;
    public int _points1, _points2;

    [Header("UI")]
    public TextMeshProUGUI _pointsTxt1, _pointsTxt2;
    public GameObject _GOPanel;
    public Text _playerWinsText;

    public static GameManager_IromMum instance;



    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Il y a plus d'une instance de GameManager dans la scène");
            return;
        }

        instance = this;
    }

    private void Start()
    {
        _canPlay = false;

    }


    // Update is called once per frame
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
    }

    public void GameOver()
    {
        _canPlay = false;
        if (_points1 > _points2)
        {
            _GOPanel.SetActive(true);
            _playerWinsText.text = "Player 1 Wins";
            Debug.Log("P1 Wins");
        }
        else if (_points2 > _points1)
        {
            _GOPanel.SetActive(true);
            _playerWinsText.text = "Player 2 Wins";
            Debug.Log("P2 Wins");
        }   
    }

    public void StartGameAfterDiscount()
    {
        Timer.instance.LaunchTimer();
        RandomPlaices.instance.StartRandomPlaices();
        _canPlay = true;

    }
}
