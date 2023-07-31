using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TTP_Timer : MonoBehaviour
{
    //Variables
    public int _minutes, _seconds;
    public TextMeshProUGUI _timer;

    public static TTP_Timer instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Il y a plus d'une instance de TTP_Timer dans la scene");
            return;
        }
        instance = this;
    }
    private void Start()
    {
        _timer.text = string.Format("{0} : {1}", _minutes.ToString("00"), _seconds.ToString("00"));
    }

    public void StartInGameTimer()
    {
        StartCoroutine(OneSecondLess());
    }

    IEnumerator OneSecondLess()
    {
        yield return new WaitForSeconds(1f);

        if (TTP_GameManager.instance._canPlay)
        {

            if (_seconds > 0)
            {
                _seconds--;
            }
            else
            {
                if (_minutes > 0)
                {
                    _minutes--;
                    _seconds = 59;
                }
                else
                {
                    TTP_GameManager.instance.GameOver(); 
                }
            }

            if (_minutes == 0 && _seconds <= 15)
            {
                _timer.faceColor = Color.red;
            }

            _timer.text = string.Format("{0} : {1}", _minutes.ToString("00"), _seconds.ToString("00"));
            StartCoroutine("OneSecondLess");
        }
    }
}
