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
    [SerializeField] string _methodName = "GameOver";
    public static Timer instance;

    public int _timerAtStart;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Il y a plus d'une instance de Timer dans la scène");
            return;
        }

        

        instance = this;
    }

    private void Start()
    {
        _timerAtStart = _timerStart;
    }

    public void LaunchTimer()
    {
        StopCoroutine(TimerBehaviour());
        StartCoroutine(TimerBehaviour());
    }

    //public void ResetTimer()
    //{
    //    StopCoroutine(TimerBehaviour());
    //    _timerStart = _timerAtStart;
    //    StartCoroutine(TimerBehaviour());

    //}

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
                            DiscountBeforeStart.instance._gameManagerInScene.SendMessage(_methodName);
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
                            DiscountBeforeStart.instance._gameManagerInScene.SendMessage(_methodName);
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
