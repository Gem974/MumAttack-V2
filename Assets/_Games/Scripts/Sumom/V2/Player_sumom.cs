using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using Spine.Unity;

public class Player_sumom : MonoBehaviour
{
    [Header("Core System")]
    public bool _isPlayer1;
    [SerializeField] float _impulseForce;
    [SerializeField] Rigidbody _rb;
    [SerializeField] Animator _animator;
    public bool _inCollision, _falling;
    [SerializeField] Transform _player;
    [SerializeField] Vector3 _startPos;
    [SerializeField] PlayerInput _playerInput;


    [Header("Controls")]
    [SerializeField] KeyCode _pK; //Touches clavier
    [SerializeField] KeyCode _pG; //Touches Gamepad
    [SerializeField] bool _buttonSouth, _buttonNorth, _buttonWest, _buttonEast;


    [Header("Spine")]
    public SkeletonMecanim _skeletonMecanim;

    [Header("VFX Part")]
    [SerializeField] GameObject[] _VFX;
    [SerializeField] Transform[] _FXOrigins;

    [Header("Audio")]
    [SerializeField] AudioSource _sfxManager;
    public AudioClip[] _clips; // 0 = CollisionSFX | 1 = Crowd | 2 = funny run | 3 = Démarrage

    private void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        InputUser.PerformPairingWithDevice(Keyboard.current, user: _playerInput.user);
        DisplayInfoCharacter();
        _inCollision = false;
        _animator.SetBool("CanMove", false);
        _startPos = transform.position;

    }

    // Update is called once per frame
    void Update()
    {

        if (Sumom_GameManager.instance._canPlay)
        {
            AnimTransitor();
        }

    }

    private void FixedUpdate()
    {
        if (Sumom_GameManager.instance._canPlay)
        {
            //if (Input.GetKeyDown(_pK) || Input.GetKeyDown(_pG))
            //{
            //    SpamBehaviour();
            //} 
        }
    }


    void DisplayInfoCharacter()
    {
        if (_isPlayer1)
        {
            _skeletonMecanim.skeleton.SetSkin(META.MetaGameManager.instance._player1._name);
            _skeletonMecanim.Skeleton.SetSlotsToSetupPose();
            _animator.runtimeAnimatorController = META.MetaGameManager.instance._player1.sumom_animatorController;
        }
        else
        {
            _skeletonMecanim.skeleton.SetSkin(META.MetaGameManager.instance._player2._name);
            _skeletonMecanim.Skeleton.SetSlotsToSetupPose();
            _animator.runtimeAnimatorController = META.MetaGameManager.instance._player2.sumom_animatorController;
        }
    }


    void SpamBehaviour()
    {
        InstantiateFXs(2);
        _rb.AddForce(transform.right * (_impulseForce/3), ForceMode.Impulse);
        InstantiateFXs(1);
        
    }

    void InstantiateFXs(int whichFXs)
    {
        // 0 = CollideFX, 1 = RunFX, 2 = SweatFX, 3 = Struggle
        Instantiate(_VFX[whichFXs], _FXOrigins[whichFXs].position, _FXOrigins[whichFXs].rotation);
    }




    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _inCollision = true;
            InstantiateFXs(0);
            if (_isPlayer1)
            {
                Sumom_GameManager.instance.PlaySFX(0); 
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "OffLimit" && _isPlayer1)
        {

            
            _animator.SetTrigger("Fall");
            PresentatorVoice.instance.StartSpeaking(false, false);
            Sumom_GameManager.instance.PlayerEjected(1);
            


        }
        else if (collision.gameObject.tag == "OffLimit2" && !_isPlayer1)
        {
          
            _animator.SetTrigger("Fall");
            PresentatorVoice.instance.StartSpeaking(false, false);
            Sumom_GameManager.instance.PlayerEjected(2);
        }
      
    }

    

    

    //private void OnTriggerExit(Collider other)                        POUR FAIRE REVENIR LE JOUEUR DANS LA COURSE QUAND IL EST AU BORD
    //{
    //    if (other.gameObject.tag == "OffLimit")
    //    {

    //        _animator.SetTrigger("Push");

    //    }
    //}


    void AnimTransitor()
    {
        if (!_inCollision) // Test si le joueur N'EST PAS en collision avec l'autre joueur
        {
            if (_animator.GetBool("CanMove"))
            {
                if (_rb.velocity.magnitude > 0.01) // Si le joueur se déplace
                {
                    _animator.SetTrigger("Push");
                    print("PUSH");
                } 
            }
            //else
            //{
            //    _animator.SetTrigger("StartPos");
                
            //    print("ANIM GO BACK LA MERDE");
            //}
            
        }
        else
        {
            
            if (_animator.GetBool("CanMove"))
            {
                if (Sumom_GameManager.instance._playerDominant == Sumom_GameManager.PlayerDominant.P1)
                {
                    if (_isPlayer1)
                    {
                        _animator.SetTrigger("Push");
                    }
                    else
                    {
                        _animator.SetTrigger("Resist");
                    }
                }

                if (Sumom_GameManager.instance._playerDominant == Sumom_GameManager.PlayerDominant.P2)
                {
                    if (!_isPlayer1)
                    {
                        _animator.SetTrigger("Push");
                    }
                    else
                    {
                        _animator.SetTrigger("Resist");
                    }
                } 
            }
        }
    }

    public void ResetPlayer()
    {
        _animator.SetBool("CanMove", false);
        _rb.velocity = Vector3.zero;
        _player.position = _startPos;
        _inCollision = false;
        _animator.SetTrigger("Idle");

    }

    public void AnimCanMove()
    {
        _animator.SetBool("CanMove", true);

    }

    public void ChangeActionMap()
    {
        if (_isPlayer1)
        {
            _playerInput.SwitchCurrentActionMap("SumomP1");
        }
        else
        {
            _playerInput.SwitchCurrentActionMap("SumomP2");
        }
    }

    public void OnActionSud(InputAction.CallbackContext context)
    {
        print("SUD");
        if (Sumom_GameManager.instance._canPlay)
        {
            if (Sign.instance._buttonToSpam == Sign.ButtonToSpam.South)
            {
                SpamBehaviour();
            }
        }
    }
    public void OnActionNord(InputAction.CallbackContext context)
    {
        print("NORD");
        if (Sumom_GameManager.instance._canPlay)
        {
            if (Sign.instance._buttonToSpam == Sign.ButtonToSpam.North)
            {
                SpamBehaviour();
            }
        }
    }
    public void OnActionOuest(InputAction.CallbackContext context)
    {
        print("OUEST");
        if (Sumom_GameManager.instance._canPlay)
        {
            if (Sign.instance._buttonToSpam == Sign.ButtonToSpam.West)
            {
                SpamBehaviour();
            }
        }

    }
    public void OnActionEst(InputAction.CallbackContext context)
    {
        print("EST");
        if (Sumom_GameManager.instance._canPlay)
        {
            if (Sign.instance._buttonToSpam == Sign.ButtonToSpam.East)
            {
                SpamBehaviour();
            }
        }

    }

    public void OnReady(InputAction.CallbackContext context)
    {
        Tutorials.instance.ReadyChecker(_isPlayer1);
    }










}
