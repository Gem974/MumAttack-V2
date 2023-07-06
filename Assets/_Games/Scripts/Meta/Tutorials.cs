using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using META;

public class Tutorials : MonoBehaviour
{
    public bool _readyP1, _readyP2;
    public GameObject _readyBanner1, _readyBanner2;
    public Image _headP1, _headP2;
    public DiscountBeforeStart _discount;
    public Animator _animator;

    public AudioSource _tutoSFX;
    public AudioClip[] _clips;
    #region IndexRegister
    //0 = Validate Character
    //1 = Yeah !
    //2 = Get Ready

    #endregion

    public static Tutorials instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Il y a plus d'une instance de Tutorials dans la scène");
            return;
        }

        instance = this;
    }

    private void Start()
    {
        _headP1.sprite = MetaGameManager.instance._player1._goodHead;
        _headP2.sprite = MetaGameManager.instance._player2._goodHead;
    }


    void Update()
    {
        //ReadyChecker();
    }

    public void ReadyChecker(bool isPlayer1)
    {
        if(isPlayer1)
        {
            Debug.Log("J1 OK");
            if (!_readyP1)
            {
                _readyP1 = true;
                _tutoSFX.PlayOneShot(_clips[0]);
                _readyBanner1.SetActive(true);
            }
            else
            {
                _readyP1 = false;
                _readyBanner1.SetActive(false);
            }
        }
        else
        {
            Debug.Log("J2 OK");
            if (!_readyP2)
            {
                _readyP2 = true;
                _tutoSFX.PlayOneShot(_clips[0]);
                _readyBanner2.SetActive(true);
            }
            else
            {
                _readyP2 = false;
                _readyBanner2.SetActive(false);
            }
        }

        //if (Input.GetButtonDown("Fire_P1"))
        //{
        //    Debug.Log("J1 OK");
        //    if (!_readyP1)
        //    {
        //        _readyP1 = true;
        //        _tutoSFX.PlayOneShot(_clips[0]);
        //        _readyBanner1.SetActive(true);
        //    }
        //    else
        //    {
        //        _readyP1 = false;
        //        _readyBanner1.SetActive(false);
        //    }
        //}

        //if (Input.GetButtonDown("Fire_P2"))
        //{
        //    Debug.Log("J2 OK");
        //    if (!_readyP2)
        //    {
        //        _readyP2 = true;
        //        _tutoSFX.PlayOneShot(_clips[0]);
        //        _readyBanner2.SetActive(true);
        //    }
        //    else
        //    {
        //        _readyP2 = false;
        //        _readyBanner2.SetActive(false);
        //    }
        //}

        if (_readyP1 && _readyP2)
        {
            PrepareGame();
            //PresentatorVoice.instance.StartSpeaking(true, true);
        }
    }


    public void PrepareGame()
    {
        //Appel de la fonction de changement d'action map des joueurs (corrige les erreurs de spamming de touche dans le tuto)
        _discount.TutoPreparationFinish();
        _animator.SetTrigger("Start");
    }

    public void StartTheDiscount()
    {
        _discount.StartAfterTuto();
        Destroy(gameObject);
    }
}
