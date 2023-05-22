using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_DarTeat : MonoBehaviour
{
    public bool _gameIsFinished;
    public GameObject _panelGameIsFinished;
    public GameObject _panelStartAnimation;

    public static GameManager_DarTeat instance;

    private void Start()
    {
        if (instance != null)
            Debug.Log("Il y a plus d'une instance dans la scène");

        instance = this;

        _panelStartAnimation.SetActive(true);
    }

    public void Victory()
    {
        //Update Final Score
        ScoreController_DarTeat.instance.UpdateResultValue();
        _gameIsFinished = true;
        //Show Final Scodd
        _panelGameIsFinished.SetActive(true);
        //Sounds
        SoundManager_DarTeat.instance._soundEffectWinner.PlayOneShot(SoundManager_DarTeat.instance._winnerSound);
        SoundManager_DarTeat.instance._soundEffectTheme.Stop();
        SoundManager_DarTeat.instance._soundEffectThemeOnGoldTeat.Stop();
        SoundManager_DarTeat.instance._soundEffectThemeOnFinish.Play();
        Debug.Log("Victory !");
    }
}
