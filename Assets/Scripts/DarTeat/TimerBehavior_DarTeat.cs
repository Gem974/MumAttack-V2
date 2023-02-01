using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerBehavior_DarTeat : MonoBehaviour
{
    public int _minutes, _seconds;
    public TextMeshProUGUI _timer;
    //public int _moreSecondsIfEquality = 15;
    public GameObject _equalityAnimation;
    public GameObject _goldenTeatText;
    public static bool _goldenTeat = false;
    bool _soundPitch;

    public static TimerBehavior_DarTeat instance;

    bool _canSpendTime = true;

    private void Start()
    {
        if (instance != null)
            Debug.Log("Il y a plus d'une instance dans la scène");

        instance = this;

        _goldenTeat = false;

        _timer.text = string.Format("{0} : {1}", _minutes.ToString("00"), _seconds.ToString("00"));
        StartCoroutine("OneSecondLess");
    }

    IEnumerator OneSecondLess()
    {
        yield return new WaitForSeconds(1f);

        if(!GameManager_DarTeat.instance._gameIsFinished)
        {

            if(_seconds > 0)
            {
                _seconds--;
            }
            else
            {
                if(_minutes > 0)
                {
                    _minutes--;
                    _seconds = 59;
                }
                else
                {
                    if (ScoreController_DarTeat.instance._currentScorePlayer1 == ScoreController_DarTeat.instance._currentScorePlayer2)
                    {
                        _equalityAnimation.SetActive(true);
                        _goldenTeat = true;
                        _canSpendTime = false;
                        GameManager_DarTeat.instance._gameIsFinished = true;
                        yield return new WaitForSeconds(3.5f);
                        _goldenTeatText.SetActive(true);
                        _goldenTeatText.transform.parent.GetComponent<TextMeshProUGUI>().enabled = false;
                        GameManager_DarTeat.instance._gameIsFinished = false;
                        SoundManager_DarTeat.instance._soundEffectTheme.Stop();
                        SoundManager_DarTeat.instance._soundEffectThemeOnGoldTeat.Play();

                    }
                    else
                    {
                        //Victoire
                        Victory();
                    }
                }
            }

            if(_soundPitch)
            {
                if(_minutes >= 0 && _seconds <= 15)
                    SoundManager_DarTeat.instance._soundEffectsTimer.PlayOneShot(SoundManager_DarTeat.instance._timeDecrease);
                else
                    SoundManager_DarTeat.instance._soundEffectsTimer.PlayOneShot(SoundManager_DarTeat.instance._timeDecreaseSpeed);
                _soundPitch = false;
            }
            else if (!_soundPitch)
            {
                if (_minutes >= 0 && _seconds <= 15)                   
                    SoundManager_DarTeat.instance._soundEffectsTimer.PlayOneShot(SoundManager_DarTeat.instance._timeDecreaseWithPitch);
                else
                    SoundManager_DarTeat.instance._soundEffectsTimer.PlayOneShot(SoundManager_DarTeat.instance._timeDecreaseWithPitchSpeed);

                _soundPitch = true;
            }

            if (_minutes == 0 && _seconds <= 15)
            {
                _timer.faceColor = Color.red;
            }
        }

        if(!_goldenTeat)
            _timer.text = string.Format("{0} : {1}", _minutes.ToString("00"), _seconds.ToString("00"));

        //Timer can continue ?
        if (_canSpendTime)
            StartCoroutine("OneSecondLess");
    }

    public void Victory()
    {
        GameManager_DarTeat.instance.Victory();
        //Timer can continue ?
        _canSpendTime = false;
        //Sounds
        SoundManager_DarTeat.instance._soundEffectsTimer.PlayOneShot(SoundManager_DarTeat.instance._timeEnded);
    }
}
