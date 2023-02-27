using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using META;

public class GameOverBehaviour : MonoBehaviour
{
    public static GameOverBehaviour instance;
    public GameObject _menuBtn;
    public GameObject _ContinueBtn;
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
                _ContinueBtn.SetActive(true);
                _menuBtn.SetActive(false);
                break;
            case MetaGameManager.GameMode.FreeBrawl:
                _ContinueBtn.SetActive(false);
                _menuBtn.SetActive(true);
                break;
            default:
                break;
        }


    }

    public void BackToMenu()
    {
        scenesManager.instance.LoadSpecificScene(0);
    }

    public void ContinueBroadCast()
    {
        scenesManager.instance.LoadBroadcastScene(MetaGameManager.instance._currentStep);
    }



}
