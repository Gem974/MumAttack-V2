using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MaxiMum_GameManager : MonoBehaviour
{
    #region Variables

    [Header("Sliders")]
    [SerializeField] public float _maxTolerance = 20;
    [SerializeField] public float _currentToleranceP1, _currentToleranceP2;
    public Slider _toleranceSliderP1, _toleranceSliderP2;
    public Slider _biberonP1, _biberonP2;

    [Header("Objects")]
    public MaxiMum_PlayerController _player1, _player2;
    public float _stunTime = 1f;
    public GameObject _countDown;

    [Header("Multiplicateurs")]
    [SerializeField] public float _drainingSpeed = 5f;
    [SerializeField] public float _fillinfSpeed = 10f;

    [Header("Animation")]
    public Animator _baby1;
    public Animator _baby2;

    [Header("UIManager")]
    public Text _winnerText;
    public GameObject _victoryScreen;
    public static bool _isPaused;
    public bool _paused;

    #endregion

    #region Unity Methods

    void Start()
    {
        _isPaused = true;
        _currentToleranceP1 = _toleranceSliderP1.value;
        _currentToleranceP2 = _toleranceSliderP2.value;
        _countDown.gameObject.SetActive(true);
    }

    void Update() 
    {
        _paused = _isPaused;
        _biberonP1.value = _player1._currentMilk;
        _biberonP2.value = _player2._currentMilk;

        if (!_player1._isFeeding) // si le joueur 1 nourris le bébé...
            _toleranceSliderP1.value = Mathf.MoveTowards(_toleranceSliderP1.value, 0, Time.deltaTime * _drainingSpeed); // ..alors on augmente la valeur du slider
        else  // si le joueur 1 n'est pas en train de nourrir le bébé on la fait descendre automatiquement
        {
            _toleranceSliderP1.value = Mathf.MoveTowards(_toleranceSliderP1.value, 20, Time.deltaTime * _fillinfSpeed);
        }

        if (!_player2._isFeeding) // si le joueur 2 nourris le bébé alors on augmente la valeur du slider
        {
            _toleranceSliderP2.value = Mathf.MoveTowards(_toleranceSliderP2.value, 0, Time.deltaTime * _drainingSpeed); // ..alors on augmente la valeur du slider
        }
        else // si le joueur 2 n'est pas en train de nourrir le bébé on la fait descendre automatiquement
        {
            _toleranceSliderP2.value = Mathf.MoveTowards(_toleranceSliderP2.value, 20, Time.deltaTime * _fillinfSpeed);
        }

        if (_biberonP1.value == 0) // si  la valeur du biberon du joueur 1 est égale à 0...
        {
            SetWinner("Player1"); // ..le joueur 1 gagne la partie
        }
        else if(_biberonP2.value == 0) // si  la valeur du biberon du joueur 2 est égale à 0...
        {
            SetWinner("Player2"); // ..le joueur 1 gagne la partie
        }

        if (_currentToleranceP1 == _maxTolerance)
        {
            StartCoroutine(Reject(_player1));
            _player1.GetComponent<CameraShake>().StartShake(1.5f, 10f);
            //CameraShake._instance.StartShake(3f , 1f);
        }
        if (_currentToleranceP2 == _maxTolerance)
        {
            StartCoroutine(Reject(_player2));
            _player2.GetComponent<CameraShake>().StartShake(1.5f, 10f);
            //CameraShake._instance.StartShake(3f, 1f);
        }

        _currentToleranceP1 = _toleranceSliderP1.value;
        _currentToleranceP2 = _toleranceSliderP2.value;
    }

    #endregion

    #region Custom Region
    

    public void SetWinner(string winnerplayer)
    {
        _isPaused = true;
        StopAllCoroutines();
        _winnerText.text = string.Format("{0} won", winnerplayer);
        _victoryScreen.SetActive(true);
        //_uiAnimator.SetTrigger("Opening");
        _player1.enabled = false;
        _player2.enabled = false;
    }

    public void Rematch()
    {

        //_uiAnimator.SetTrigger("Opening");
    }

    public void NoButton()
    {
      
        Invoke("UIClosingAnimation", 0.1f); // Invoking the "closing" methods for the question box
        //StartCoroutine(UIClosingAnimation());
    }

    public void UIClosingAnimation()
    {
       // yield return new WaitForSeconds(0.1f);

    }

    #endregion

    #region Coroutine
    public IEnumerator Reject(MaxiMum_PlayerController player)
    {
        player.GetComponent<AudioSource>().Play();
        player._isFeeding = false;
        player._feedingBiberon.gameObject.SetActive(false);
        player._idlebiberon.gameObject.SetActive(true);
        player._canFeed = false;
        yield return new WaitForSeconds(_stunTime);
        player._canFeed = true;

    }

    #endregion
}
