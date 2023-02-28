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
    private void Awake()
    {
        if (instance != null)
        {

            Destroy(this.gameObject);
            Debug.LogError("Il y avait plus d'une instance de MetaGameManager dans la sc�ne.");
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
                _ContinueBtn.gameObject.SetActive(true);
                _ContinueBtn.Select();
                
                _menuBtn.gameObject.SetActive(false);
                break;
            case MetaGameManager.GameMode.FreeBrawl:
                _ContinueBtn.gameObject.SetActive(false);
                _menuBtn.gameObject.SetActive(true);
                _menuBtn.Select();
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
