using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using META;

public class GameOverBehaviour : MonoBehaviour
{
    public static GameOverBehaviour instance;
    public Button _menuBtn;
    public Button _ContinueBtn;
    public Text _playerWinTxt;
    
    public Image[] _playersHead;
    private void Awake()
    {
        if (instance != null)
        {

            Destroy(this.gameObject);
            Debug.LogError("Il y avait plus d'une instance de MetaGameManager dans la scène.");
            return;
        }

        instance = this;
    }

    private void Start()
    {
        switch (MetaGameManager.instance._gameMode)
        {
            case MetaGameManager.GameMode.BroadCast:
                MetaGameManager.instance.BroadCastNextStep();        
                _menuBtn.gameObject.SetActive(false);
                WaitForButtons(_ContinueBtn);

                break;
            case MetaGameManager.GameMode.FreeBrawl:
                _ContinueBtn.gameObject.SetActive(false);
                WaitForButtons(_menuBtn);

                break;
            default:
                break;
        }


    }

    public void BackToMenu()
    {
        scenesManager.instance.LoadSpecificScene(0);
    }

    public void PlayerToWin(int player)
    {
        
        if (player == 1)
        {
            _playerWinTxt.text = META.MetaGameManager.instance._player1._name + " Wins !";
            MetaGameManager.instance._P1Wins++;
            _playersHead[0].sprite = META.MetaGameManager.instance._player1._goodHead;
            _playersHead[1].rectTransform.localScale = new Vector3(3f, 3f, 3f);
            _playersHead[1].sprite = META.MetaGameManager.instance._player2._badHead;
            _playersHead[1].color = new Color(166, 155, 155);
        }
        else if (player == 2)
        {
            _playerWinTxt.text = META.MetaGameManager.instance._player2._name + " Wins !";
            MetaGameManager.instance._P2Wins++;
            _playersHead[1].sprite = META.MetaGameManager.instance._player2._goodHead;
            _playersHead[0].rectTransform.localScale = new Vector3(3f, 3f, 3f);
            _playersHead[0].sprite = META.MetaGameManager.instance._player1._badHead;
            _playersHead[0].color = new Color(166, 155, 155) ;
        }
    }

    public void ContinueBroadCast()
    {
        scenesManager.instance.LoadBroadcastScene(MetaGameManager.instance._currentStep);
    }

    public void WaitForButtons(Button buttonToWait)
    {
        StartCoroutine(ButtonWait(buttonToWait));
    }

    IEnumerator ButtonWait(Button buttonToWait)
    {
        yield return new WaitForSeconds(1.5f);
        buttonToWait.gameObject.SetActive(true);
        buttonToWait.Select();
    }



}
