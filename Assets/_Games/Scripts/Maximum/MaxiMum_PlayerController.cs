using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaxiMum_PlayerController : MonoBehaviour
{
    public float _maxMilk = 10;
    public float _currentMilk = 10;
    public KeyCode _actionKey;

    public bool _isFeeding;
    public Slider _feedingBiberon;
    public Slider _idlebiberon;
    public Animator _biberonAnimator;

    public float _feedingCD;
    public float _feedAgain;
    public bool _canFeed;

    public bool _isFed;


    void Start()
    {
        _currentMilk = _maxMilk;
        _canFeed = true;

    }

    void Update()
    {
        if (MaxiMum_GameManager._isPaused == false)
        {
            if (_canFeed)
            {
                if (Input.GetKey(_actionKey)) // If an action key is pressed
                {
                    _isFeeding = true; // changing the bool's value
                    _idlebiberon.gameObject.SetActive(false); // the idle baby bottle deactivates itself
                    _feedingBiberon.gameObject.SetActive(true);// the feeding baby bottle activates itself
                    if (_feedingBiberon) // if the feeding bool is active...
                    {
                        _currentMilk = Mathf.MoveTowards(_currentMilk, 0, Time.deltaTime); // the baby bottle's content is going down
                        _biberonAnimator.SetBool("Feeding", true);
                        _idlebiberon.value = _feedingBiberon.value; // the idle baby bottle's slider's value take the same value as the feeding one.
                    }

                }
                if (Input.GetKeyUp(_actionKey)) // si la touche d'action est relevée...
                {
                    _idlebiberon.gameObject.SetActive(true);
                    _feedingBiberon.gameObject.SetActive(false);
                    _isFeeding = false;
                    _biberonAnimator.SetBool("Feeding", false);
                    _idlebiberon.value = _feedingBiberon.value;
                }
            }


            if (_currentMilk == 0) // If the baby bottle is empty
            {
                _isFed = true; // send the info to the GameManager
            }
        }
    }
}
