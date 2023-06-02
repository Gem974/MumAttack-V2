using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using Spine.Unity;
using TMPro;


public class Sumo_GameManager : MonoBehaviour
{

    public int _pointsP1, _pointsP2;
    //[SerializeField] int _totalRounds;
    [SerializeField] GameObject _gameOverPanel, _gameCanvas, _scores;
    [SerializeField] Sumo_CollisionDetection _colliDetection;

    [SerializeField] Text _playerWinsTxt, _discountTxt, _pointJ1Txt, _pointJ2Txt;
    [SerializeField] Transform _player1, _player2;
    [SerializeField] Vector2 _startPos1, _startPos2;
    [SerializeField] Quaternion _playersRot;

    [SerializeField] float _value;

    [SerializeField] Volume _postProcess;
    public ChromaticAberration _chroAbe;
    public DepthOfField _dop;

    [Header("Set Spine")]
    //public Animator[] _animator;
    public SkeletonRenderer _skeletonMecanim1;
    public SkeletonRenderer _skeletonMecanim2;

    [Header("Chrono")]
    public int _timerStart;
    public TextMeshProUGUI _timerTxt;
    


    public static Sumo_GameManager instance;




    public bool _canPlay, _gameOver, _launchPlayer;


    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Il y a plus d'une instance de GameManager dans la scène");
            return;
        }

        instance = this;

        _startPos1 = _player1.position;
        _startPos2 = _player2.position;
        _playersRot = _player1.rotation;


    }
    private void Start()
    {
        StopAllCoroutines();
        _skeletonMecanim1.Skeleton.SetSkin(META.MetaGameManager.instance._player1._name);
        _skeletonMecanim1.Skeleton.SetSlotsToSetupPose();
        _skeletonMecanim2.Skeleton.SetSkin(META.MetaGameManager.instance._player2._name);
        _skeletonMecanim2.Skeleton.SetSlotsToSetupPose();



        //APL CHROMATIC ABERRATION



        UnityEngine.Time.timeScale = 1;

        _gameCanvas.SetActive(true);

        SpamBehaviour.instance._animatorP1.SetBool("isMoving", false);
        SpamBehaviour.instance._animatorP2.SetBool("isMoving", false);
        _colliDetection = FindObjectOfType<Sumo_CollisionDetection>();

        SpamBehaviour.instance._rb1.velocity = new Vector2(0f, 0f);
        SpamBehaviour.instance._rb2.velocity = new Vector2(0f, 0f);
        _colliDetection._stopColliFX = true;
        _colliDetection._inCollision = false;
        _player1.position = _startPos1;
        _player2.position = _startPos2;
        ResetRot(1);
        ResetRot(2);
        // _totalRounds = 3;



    }

 
    public void StartGameAfterDiscount()
    {
        _postProcess.profile.TryGet(out _chroAbe);
        _postProcess.profile.TryGet(out _dop);
        _launchPlayer = false;
        _gameOver = false;

        _canPlay = true;
        PauseGame.instance.CanPause();
        //LaunchTimer();
        //StartCoroutine(Chrono());
        
    }

    public void LaunchTimer()
    {
        StopCoroutine(TimerBehaviour());
        StartCoroutine(TimerBehaviour());
    }

    IEnumerator TimerBehaviour()
    {
        _timerTxt.text = _timerStart.ToString();

        while (_timerStart >= -1)
        {
            yield return new WaitForSeconds(1f);
            _timerStart--;

            if (_timerStart <= 0)
            {
                _timerTxt.text = _timerStart.ToString();
                _gameOver = true;
                StopAllCoroutines();

            }
            else
            {
                _timerTxt.text = _timerStart.ToString();
            }

        }
    }
        private void Update()
    {


        if (!_gameOver) // En jeu
        {
            _pointJ1Txt.text = _pointsP1.ToString();
            _pointJ2Txt.text = _pointsP2.ToString();
            VictoryCheckSystem();
        }
        else if (_gameOver) // Fin de partie
        {
            PauseGame.instance.CanTPause();
            _gameOverPanel.SetActive(true);

            if (_pointsP1 == 3 && _launchPlayer)
            {
                GameOverBehaviour.instance.PlayerToWin(1);
                _canPlay = false;
            }
            else if (_pointsP2 == 3 && _launchPlayer)
            {
                GameOverBehaviour.instance.PlayerToWin(2);
                _canPlay = false;
            }

            //_sound.SetActive(false);
            _discountTxt.gameObject.SetActive(false);
            //_scores.SetActive(false);

        }

    }

    public void RestartRound()
    {
        if (!_gameOver)
        {

            _canPlay = true;
            StopAllCoroutines();

            //APL CHROMATIC ABERRATION

            _chroAbe.intensity.value = 0;

            UnityEngine.Time.timeScale = 1;

            if (_pointsP2 > 0 || _pointsP1 > 0)
            {
                StartCoroutine(DiscountBeforeStart());  
            }
            SpamBehaviour.instance._animatorP1.SetBool("isMoving", false);
            SpamBehaviour.instance._animatorP2.SetBool("isMoving", false);
            
            SpamBehaviour.instance._rb1.velocity = new Vector2(0f, 0f);
            SpamBehaviour.instance._rb2.velocity = new Vector2(0f, 0f);
            _colliDetection._stopColliFX = true;
            _colliDetection._inCollision = false;
            _player1.position = _startPos1;
            _player2.position = _startPos2;
            ResetRot(1);
            ResetRot(2);
            
        }
    }

    void VictoryCheckSystem()
    {
        
        if (_pointsP1 == 3)
        {
            _gameOver = true;
            _launchPlayer = true;
            UnityEngine.Time.timeScale = 1;

            
        }
        else if (_pointsP2 == 3)
        {
            _gameOver = true;
            _launchPlayer = true;
            UnityEngine.Time.timeScale = 1;  
        }
    }




    IEnumerator DiscountBeforeStart()
    {

        _discountTxt.gameObject.SetActive(true);
        _canPlay = false;
        PresentatorVoice.instance.DiscountPresentator(3);
        _discountTxt.text = "3";
        yield return new WaitForSeconds(1f);
        PresentatorVoice.instance.DiscountPresentator(2);
        _discountTxt.text = "2";
        yield return new WaitForSeconds(1f);
        PresentatorVoice.instance.DiscountPresentator(1);
        _discountTxt.text = "1";
        yield return new WaitForSeconds(1f);
        PresentatorVoice.instance.DiscountPresentator(0);
        _discountTxt.text = "START !";
        yield return new WaitForSeconds(1f);
        _canPlay = true;
        _discountTxt.gameObject.SetActive(false);

        //APL DepthOfField
        while (_dop.focusDistance.value <= 10f)
        {
            _dop.focusDistance.value += 1f;
            yield return new WaitForSeconds(0.05f);

        }


    }

    IEnumerator PreRestart()
    {
        SpamBehaviour.instance._sfxManager.clip = SpamBehaviour.instance._clips[1];
        SpamBehaviour.instance._sfxManager.Play();
        SpamBehaviour.instance._zoomSFX.Play();

        //APL CHROMATIC ABERRATION
        

        while (_chroAbe.intensity.value <= 0.9)
        {
            _chroAbe.intensity.value += 0.1f;
    
            yield return new WaitForSeconds(0.02f);
            
        }

        UnityEngine.Time.timeScale = 0.2f;
        yield return new WaitForSeconds(0.3f);
        UnityEngine.Time.timeScale = 1;
        SpamBehaviour.instance.ResetAnim();
        
        RestartRound();



    }

    public void PreRestartVoid()
    {
        StartCoroutine(PreRestart());
    }

    public void ResetRot(int player)
    {
        if (player == 1)
        {
            _player1.rotation = _playersRot;
        }
        else if (player == 2)
        {
            _player2.rotation = _playersRot;

        }
    }

}
