using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SpamBehaviour : MonoBehaviour
{
    Sumo_GameManager _gameManager;
    [SerializeField] KeyCode _pK1, _pK2, _pG1, _pG2;
    [SerializeField] GameObject[] _arrows; // 0 = DOWN | 1 = UP | 2 = RIGHT | 3 = LEFT
    public Rigidbody _rb1, _rb2;
    [SerializeField] float _impulseForce;

    [SerializeField] Sumo_CollisionDetection _colliDetection;

    public Animator _animatorP1;
    public Animator _animatorP2;

    [Header("VFX Part")]
    [SerializeField] GameObject[] _VFX;
    [SerializeField] Transform[] _FXOrigins;
    public enum PlayerDominant
    {
        None, Player1, Player2
    }
    public PlayerDominant _playerDominant;


    [Header("SFX Part")]
    public AudioSource _sfxManager; 
    public AudioSource _sfxRun1;
    public AudioSource _sfxRun2;
    public AudioSource _zoomSFX;
    public AudioClip[] _clips; // 0 = CollisionSFX | 1 = Crowd | 2 = funny run | 3 = Démarrage


    public static SpamBehaviour instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Il y a plus d'une instance de SpamBehaviour dans la scène");
            return;
        }

        instance = this;
    }


    private void Start()
    {
        _colliDetection = FindObjectOfType<Sumo_CollisionDetection>();
        _gameManager = FindObjectOfType<Sumo_GameManager>();
        SetRandomKey();
        StartCoroutine(RandomKeys());
        ResetAnim();
        //_animatorP1.enabled = false;
        //_animatorP2.enabled = false;


    }

    void Update()
    {
        if (_gameManager._canPlay)
        {
            FXSystem();
            SpamBehave(_impulseForce);
            //_arrows[4].SetActive(true);
            
        }
        else
        {
            //_arrows[4].SetActive(false); //Désactive les flèches
        }

        if (_animatorP2.GetBool("isMoving"))    
        {
            _sfxRun2.gameObject.SetActive(true);
        }
        else
        {
            _sfxRun2.gameObject.SetActive(false);
        }

        if (_animatorP1.GetBool("isMoving"))
        {
            
            _sfxRun1.gameObject.SetActive(true);
        }
        else
        {
            
            _sfxRun1.gameObject.SetActive(false);
        }

    }


    IEnumerator RandomKeys()
    {
        while (true)
        {
            float randomTime = Random.Range(2f, 4f);
            yield return new WaitForSeconds(randomTime);
            SetRandomKey(); 
        }
        
        
    }

    void SetRandomKey()
    {
        if (_gameManager._canPlay)
        {
            if (_rb1.velocity.x > -_rb2.velocity.x) //Reset Force avec J1 > J2
            {
                SpamBehaviour.instance._rb1.velocity = new Vector3(2f, 0f, 0f); //Reset Force
                SpamBehaviour.instance._rb2.velocity = new Vector3(-1f, 0f, 0f);
            }
            else if (-_rb2.velocity.x > _rb1.velocity.x)
            {
                SpamBehaviour.instance._rb1.velocity = new Vector3(1f, 0f, 0f); //Reset Force
                SpamBehaviour.instance._rb2.velocity = new Vector3(-2f, 0f, 0f);
            }
        }

        int randomKey = Random.Range(0, 4);

        switch (randomKey)
        {

            case 0:
                ArrowsOff(); //Up
                _pK1 = KeyCode.Z;
                _pK2 = KeyCode.UpArrow;
                _pG1 = KeyCode.Joystick1Button3;
                _pG2 = KeyCode.Joystick2Button3;
                _arrows[1].SetActive(true);
                Sign.instance.ChangePos();


                break;
            case 1:
                ArrowsOff(); //Right
                _pK1 = KeyCode.D;
                _pK2 = KeyCode.RightArrow;
                _pG1 = KeyCode.Joystick1Button1;
                _pG2 = KeyCode.Joystick2Button1;
                _arrows[2].SetActive(true);
                Sign.instance.ChangePos();

                break;
            case 2:
                ArrowsOff(); //Left
                _pK1 = KeyCode.Q;
                _pK2 = KeyCode.LeftArrow;
                _pG1 = KeyCode.Joystick1Button2;
                _pG2 = KeyCode.Joystick2Button2;
                _arrows[3].SetActive(true);
                Sign.instance.ChangePos();

                break;
            case 3:
                ArrowsOff(); //Down
                _pK1 = KeyCode.S;
                _pK2 = KeyCode.DownArrow;
                _pG1 = KeyCode.Joystick1Button0;
                _pG2 = KeyCode.Joystick2Button0;
                _arrows[0].SetActive(true);
                Sign.instance.ChangePos();

                break;

        }
    }

    public void SpamBehave(float impulseForce)
    {
        impulseForce = _impulseForce;

        if (Input.GetKeyDown(_pK1) || Input.GetKeyDown(_pG1))
        {
            Instantiate(_VFX[3], _FXOrigins[3].position, _FXOrigins[3].rotation);
            _rb1.AddForce(transform.right * impulseForce, ForceMode.Impulse);
            InstantiateFX(1); //FX Course J1
        }
        



        if (Input.GetKeyDown(_pK2) || Input.GetKeyDown(_pG2))
        {
            Instantiate(_VFX[3], _FXOrigins[4].position, _FXOrigins[4].rotation);
            _rb2.AddForce(-transform.right * impulseForce, ForceMode.Impulse);
            InstantiateFX(2); //FX Course J2

        }
        

    }

    void ArrowsOff()
    {
        _arrows[0].SetActive(false);
        _arrows[1].SetActive(false);
        _arrows[2].SetActive(false);
        _arrows[3].SetActive(false);
    }

    void FXSystem()
    {
        if (!_colliDetection._inCollision) //Test si les personnage NE sont PAS en collision
        {
            if (_rb1.velocity.x > 0)
            {
                Debug.Log("FX J1 Avance sans collision");
                _animatorP1.SetBool("isMoving", true);
            }
            
            if (_rb2.velocity.x < 0)
            {
                Debug.Log("FX J2 Avance sans collision");
                _animatorP2.SetBool("isMoving", true);
            }
        }
        else if (_colliDetection._inCollision)  //Test si les personnage sont en collision
        {
            //PresentatorVoice.instance.StartSpeaking(true, true);
            if (!_animatorP1.GetBool("FirstCollision") && !_animatorP2.GetBool("FirstCollision"))
            {
                PresentatorVoice.instance.StartSpeaking(true, true);   
            }
            _animatorP2.SetBool("FirstCollision", true);
            _animatorP1.SetBool("FirstCollision", true);
            
            Debug.Log("Lance FX Collision");
            if (_colliDetection._inCollision && _colliDetection._stopColliFX)
            {
                _animatorP2.SetBool("Collision", true);
                _animatorP1.SetBool("Collision", true);
                if (_FXOrigins[0].childCount == 0)
                {
                    //_colliDetection._stopColliFX = false;
                    InstantiateFX(0);
                    _sfxManager.clip = _clips[0];
                    _sfxManager.Play();
                } 
            }

            if (_rb1.velocity.x > -_rb2.velocity.x)
            {
                if (_playerDominant == PlayerDominant.Player2)
                {
                    PresentatorVoice.instance.StartSpeaking(true, true);
                    _playerDominant = PlayerDominant.Player1;
                }
                else if (_playerDominant == PlayerDominant.None)
                {
                    _playerDominant = PlayerDominant.Player1;
                }

                Debug.Log("FX Avantage J1");
                _animatorP1.SetBool("isMoving", true);
                if (_FXOrigins[2].childCount == 0)
                {
                    Instantiate(_VFX[1], _FXOrigins[2]); 
                   
                }
                _animatorP2.SetBool("isMoving", false);
            }
            else if (-_rb2.velocity.x > _rb1.velocity.x)
            {
                if (_playerDominant == PlayerDominant.Player1)
                {
                    PresentatorVoice.instance.StartSpeaking(true, true);
                    _playerDominant = PlayerDominant.Player2;
                    
                }
                else if (_playerDominant == PlayerDominant.None)
                {
                    _playerDominant = PlayerDominant.Player2;
                }
            
                Debug.Log("FX Avantage J2");
                _animatorP1.SetBool("isMoving", false);
                if (_FXOrigins[1].childCount == 0)
                {
                    Instantiate(_VFX[2], _FXOrigins[1]);
                    
                }                
                _animatorP2.SetBool("isMoving", true);

            }
        }
    }

    public void ResetAnim()
    {
        _animatorP1.SetTrigger("StartPos");
        _animatorP2.SetTrigger("StartPos");
        _animatorP1.SetBool("Collision", false);
        _animatorP2.SetBool("Collision", false);
        _animatorP1.SetBool("isMoving", false);
        _animatorP2.SetBool("isMoving", false);
        _animatorP1.SetBool("FirstCollision", false);
        _animatorP2.SetBool("FirstCollision", false);
        
    }

    void InstantiateFX(int FXindex)
    {
        Instantiate(_VFX[FXindex], _FXOrigins[FXindex]);

        //0 = FX Collision
        //1= RunJ1
        //2= RunJ2
    }






}
