using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;


public class Sumo_GameManager : MonoBehaviour
{
    public int _pointsP1, _pointsP2;
    //[SerializeField] int _totalRounds;
    [SerializeField] GameObject _gameOverPanel, _gameCanvas, _scores, _sound;
    [SerializeField] SpamBehaviour _spamBehaviour;
    [SerializeField] Sumo_CollisionDetection _colliDetection;

    [SerializeField] Text _playerWinsTxt, _timer, _pointJ1Txt, _pointJ2Txt;
    [SerializeField] Transform _player1, _player2;
    [SerializeField] Vector2 _startPos1, _startPos2;
    [SerializeField] Quaternion _playersRot;

    [SerializeField] float _value;

    [SerializeField] Volume _postProcess;
    public ChromaticAberration _chroAbe;
    public DepthOfField _dop;






    public bool _canPlay, _gameOver;


    private void Awake()
    {
        

        _startPos1 = _player1.position;
        _startPos2 = _player2.position;
        _playersRot = _player1.rotation;


    }
    private void Start()
    {
        _postProcess.profile.TryGet(out _chroAbe);
        _postProcess.profile.TryGet(out _dop);
        _gameCanvas.SetActive(true);
        _colliDetection = FindObjectOfType<Sumo_CollisionDetection>();

        _gameOver = false;
        _spamBehaviour = FindObjectOfType<SpamBehaviour>();
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

            _sound.SetActive(false);
            _timer.gameObject.SetActive(false);
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

            StartCoroutine(Timer());
            _spamBehaviour._animatorP1.SetBool("isMoving", false);
            _spamBehaviour._animatorP2.SetBool("isMoving", false);
            _colliDetection._stopColliFX = true;
            _colliDetection._inCollision = false;
            _player1.position = _startPos1;
            _player2.position = _startPos2;
            ResetRot(1);
            ResetRot(2);
            _spamBehaviour._rb1.velocity = new Vector2(0f, 0f);
            _spamBehaviour._rb2.velocity = new Vector2(0f, 0f);
        }
    }

    void VictoryCheckSystem()
    {

        if (_pointsP1 == 3)
        {
            _gameOver = true;
            UnityEngine.Time.timeScale = 1;

            _playerWinsTxt.text = "Player 1 Wins";
        }
        else if (_pointsP2 == 3)
        {
            _gameOver = true;
            UnityEngine.Time.timeScale = 1;


            _playerWinsTxt.text = "Player 2 Wins";
        }
    }




    IEnumerator Timer()
    {

        _timer.gameObject.SetActive(true);
        _canPlay = false;
        _timer.text = "3";
        yield return new WaitForSeconds(1f);
        _timer.text = "2";
        yield return new WaitForSeconds(1f);
        _timer.text = "1";
        yield return new WaitForSeconds(1f);
        _timer.text = "START !";
        yield return new WaitForSeconds(1f);
        _canPlay = true;
        _timer.gameObject.SetActive(false);

        //APL DepthOfField
        while (_dop.focusDistance.value <= 10f)
        {
            _dop.focusDistance.value += 1f;
            yield return new WaitForSeconds(0.05f);

        }


    }

    IEnumerator PreRestart()
    {
        _spamBehaviour._sfxManager.clip = _spamBehaviour._clips[1];
        _spamBehaviour._sfxManager.Play();
        _spamBehaviour._zoomSFX.Play();

        //APL CHROMATIC ABERRATION
        

        while (_chroAbe.intensity.value <= 0.9)
        {
            _chroAbe.intensity.value += 0.1f;
    
            yield return new WaitForSeconds(0.02f);
            Debug.Log("Test");
        }

        UnityEngine.Time.timeScale = 0.2f;
        yield return new WaitForSeconds(0.3f);
        UnityEngine.Time.timeScale = 1;
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
