using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using Spine.Unity;


public class Sumo_GameManager : MonoBehaviour
{

    public int _pointsP1, _pointsP2;
    //[SerializeField] int _totalRounds;
    [SerializeField] GameObject _gameOverPanel, _gameCanvas, _scores, _sound;
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
    public Animator[] _animator;
    public SkeletonMecanim _skeletonMecanim1;
    public SkeletonMecanim _skeletonMecanim2;






    public bool _canPlay, _gameOver, _launchPlayer;


    private void Awake()
    {
        

        _startPos1 = _player1.position;
        _startPos2 = _player2.position;
        _playersRot = _player1.rotation;


    }
    private void Start()
    {
        _skeletonMecanim1.skeleton.SetSkin(META.MetaGameManager.instance._player1._name);
        _skeletonMecanim2.skeleton.SetSkin(META.MetaGameManager.instance._player2._name);
        _postProcess.profile.TryGet(out _chroAbe);
        _postProcess.profile.TryGet(out _dop);
        _gameCanvas.SetActive(true);
        _colliDetection = FindObjectOfType<Sumo_CollisionDetection>();
        _launchPlayer = false;
        _gameOver = false;
       
        RestartRound();
        // _totalRounds = 3;



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

            _sound.SetActive(false);
            _discountTxt.gameObject.SetActive(false);
            //_scores.SetActive(false);

        }

    }

    public void RestartRound()
    {
        if (!_gameOver)
        {

            
            StopAllCoroutines();

            //APL CHROMATIC ABERRATION

            _chroAbe.intensity.value = 0;

            UnityEngine.Time.timeScale = 1;

            StartCoroutine(DiscountBeforeStart());
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
        _discountTxt.text = "3";
        yield return new WaitForSeconds(1f);
        _discountTxt.text = "2";
        yield return new WaitForSeconds(1f);
        _discountTxt.text = "1";
        yield return new WaitForSeconds(1f);
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
