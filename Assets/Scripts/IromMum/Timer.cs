using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public enum TIMEBEHAVIOUR{ CHRONO, COUNTDOWN }

    public TIMEBEHAVIOUR _timeBehaviour = TIMEBEHAVIOUR.CHRONO;

    public int _timerStart;
    public TextMeshProUGUI _timerTxt;
    public int _chronoMax = 10;

    public static Timer instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Il y a plus d'une instance de Timer dans la scène");
            return;
        }

        instance = this;
    }

    public void LaunchTimer()
    {
        StopCoroutine(TimerBehaviour());
        StartCoroutine(TimerBehaviour());
    }

    IEnumerator TimerBehaviour()
    {
        _timerTxt.text = _timerStart.ToString();

        while (true)
        {
            switch (_timeBehaviour)
            {
                case TIMEBEHAVIOUR.CHRONO:
                    while (_timerStart < _chronoMax)
                    {
                        yield return new WaitForSeconds(1f);
                        _timerStart++;
                        if (_timerStart >= _chronoMax)
                        {
                            GameManager_IromMum.instance.GameOver();
                        }
                        else
                        {
                            _timerTxt.text = _timerStart.ToString();
                        }

                    }
                    break;
                case TIMEBEHAVIOUR.COUNTDOWN:
                    while (_timerStart >= -1)
                    {
                        yield return new WaitForSeconds(1f);
                        _timerStart--;
           
                        if (_timerStart <= 0)
                        {
                            _timerTxt.text = _timerStart.ToString();
                            GameManager_IromMum.instance.GameOver();
                            StopAllCoroutines();
                           
                        }
                        else
                        {
                            _timerTxt.text = _timerStart.ToString();
                        }

                    }
                    break;
                default:
                    yield return null;
                    break;
            } 
        }

        
    }
}
