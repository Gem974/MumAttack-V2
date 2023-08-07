using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Sumom_GameManager : GameManagerBehaviour
{
    [Header("System")]
    
    public int _pointsP1, _pointsP2;
    public Rigidbody _rb1, _rb2;
    public Player_sumom _player1, _player2;
    

    [Header("UI")]
    [SerializeField] Text _pointsJ1Txt;
    [SerializeField] Text _pointsJ2Txt, _discountTxt;
    [SerializeField] GameObject _GameOverGO;

    [Header("Post Process")]
    [SerializeField] Volume _postProcess;
    public ChromaticAberration _chroAbe;
    public DepthOfField _dop;

    [Header("Audio")]

    public AudioSource _sfxManager;
    public AudioClip[] _clips; // 0 = CollisionSFX | 1 = Crowd | 2 = Zoom

    public enum PlayerDominant
    {
        None, P1, P2
    }

    public PlayerDominant _playerDominant;


    public static Sumom_GameManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Il y a plus d'une instance de Sign dans la scène");
            return;
        }

        instance = this;
    }
    void Start()
    {
        _postProcess.profile.TryGet(out _chroAbe);
        _canPlay = false;
        Sign.instance.StartRandomKeys();
        _playerDominant = PlayerDominant.None;
        PauseGame.instance.CanTPause();
        

        _pointsP1 = 0;
        _pointsP2 = 0;
        _pointsJ1Txt.text = _pointsP1.ToString();
        _pointsJ2Txt.text = _pointsP2.ToString();

    }

    // Sert a corriger les erreurs lors du spamming de touche dans le tuto. Appeler par message par le script DiscountBeforeStart 
    public override void TutoPreparationFinish()
    {
        _player1.ChangeActionMap();
        _player2.ChangeActionMap();
    }

    public override void StartGameAfterDiscount()
    {
        
        _canPlay = true;
        PauseGame.instance.CanPause();

        _player1.AnimCanMove();
        _player2.AnimCanMove();
    }

    void Update()
    {
        PlayerDomination();
    }

    public void PlaySFX(int WhichSound)
    {
        _sfxManager.PlayOneShot(_clips[WhichSound]);
    }

    public void PlayerDomination()
    {
        if (_rb1.velocity.x > -_rb2.velocity.x) // Si J1 > J2
        {
            _playerDominant = PlayerDominant.P1;
        }

        if (-_rb2.velocity.x > _rb1.velocity.x) // Si J2 > J1
        {
            _playerDominant = PlayerDominant.P2;

        }

        if (_rb1.velocity.x == 0 && _rb2.velocity.x == 0 || _rb1.velocity.x == -_rb2.velocity.x)
        {
            _playerDominant = PlayerDominant.None;
        }
    }

   
    public void PlayerEjected(int WhichPlayer)
    {
        PauseGame.instance.CanTPause();
        StopAllCoroutines();
        _canPlay = false;
        StartCoroutine(EjectedSystem(WhichPlayer));   
    }

    

    IEnumerator EjectedSystem(int wichPlayer)
    {
        PlaySFX(2);

        if (wichPlayer == 1)
        {
            _pointsP2++;
            _pointsJ2Txt.text = _pointsP2.ToString();
        }
        else if (wichPlayer == 2)
        {
            _pointsP1++;
            _pointsJ1Txt.text = _pointsP1.ToString();
        }

        while (_chroAbe.intensity.value <= 0.9)
        {
            _chroAbe.intensity.value += 0.1f;

            yield return new WaitForSeconds(0.02f);

        }

        UnityEngine.Time.timeScale = 0.2f;
        yield return new WaitForSeconds(0.3f);
        UnityEngine.Time.timeScale = 1;
        //if (wichPlayer == 1)
        //{
        //    if (_pointsP2 <= 2)
        //    {
        //        _pointsP2++;
        //        _pointsJ2Txt.text = _pointsP2.ToString();
        //        RestartRound();
        //    }

        //}
        //else if (wichPlayer == 2)
        //{
        //    if (_pointsP1 <= 2)
        //    {
        //        _pointsP1++;
        //        _pointsJ1Txt.text = _pointsP1.ToString();
                
        //    }

        //}
        
        if (_pointsP1 == 3 || _pointsP2 == 3)
        {
            GameOver();
        }
        else if (_pointsP2 != 3 && _pointsP1 != 3)
        {
            PauseGame.instance.CanPause();
            RestartRound();
        }

    }

    public override void GameOver()
    {
        StopAllCoroutines();
        if (_pointsP1 == 3)
        {
            print("Victoire J1");
            _canPlay = true;
            Time.timeScale = 1;
            _GameOverGO.SetActive(true);
            GameOverBehaviour.instance.PlayerToWin(1);

        }
        else if (_pointsP2 == 3)
        {
            print("Victoire J2");
            _canPlay = true;
            Time.timeScale = 1;
            _GameOverGO.SetActive(true);
            GameOverBehaviour.instance.PlayerToWin(2);

        }

    }

    public void RestartRound()
    {
        _playerDominant = PlayerDominant.None;
         
        DiscountBeforeStart.instance.RestartRoundDiscount();
        _player1.ResetPlayer();
        _player2.ResetPlayer();
    }









}
