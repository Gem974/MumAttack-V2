using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Events;


public class Kart : MonoBehaviour
{
    public bool _isPlayer1;
    public enum PlayerState
    {
       Standby, Charging, Propulse
    }

    [Header("Character Display")]
    [SerializeField] SpriteRenderer _sprite;
    [SerializeField] Renderer _kartMat;

    [Header("Kart Movement")]
    [SerializeField] PlayerState _playerState;
    [SerializeField] Rigidbody _rb;
    [SerializeField] float _maxPower;
    [SerializeField] Vector3 _moveForce;
    [SerializeField] float _power;
    [SerializeField] float _chargeSpeed;
    [SerializeField] float _turnSpeed;
    [SerializeField] float _driftPower = 1;
    [SerializeField] Animator _animator;
    [SerializeField] bool _canPropulse;
    [SerializeField] float _timeBeforeSpam;

    [SerializeField] string _horizontalInput, _verticalInput, _actionInput;

    [Header("Multiplier System")]
    [SerializeField] bool _canMultiply;
    [SerializeField] float _nextime;
    [SerializeField] int _multiplier = 1;

    [Header("FXs")]
    [SerializeField] GameObject[] _fxPropulse;
    [SerializeField] Transform _fxSpawnPoint;
    [SerializeField] GameObject _fxCollision;
    [SerializeField] UnityEvent _yellowSteam, _orangeSteam, _violetSteam, _noSteam;
    [SerializeField] Bubble _bubble;

    [Header("Sounds")]
    [SerializeField] AudioSource _source;
    [SerializeField] AudioClip _plaicesIroned;





    private void Start()
    {
        DisplayInfoCharacter();
        TouchBinding();
        _playerState = PlayerState.Standby;
        _canPropulse = true;
    }

    void Update()
    {

        if (GameManager_IromMum.instance._canPlay)
        {
            switch (_playerState)
            {
                case PlayerState.Standby:
                    PowerCharge();
                    break;
                case PlayerState.Charging:
                    PowerCharge();

                    break;
                case PlayerState.Propulse:
                    PowerCharge();

                    break;
                default:
                    break;
            } 
        }
    }

    private void FixedUpdate()
    {
        if (GameManager_IromMum.instance._canPlay)
        {

            Move();
            PowerImpulse();
            switch (_playerState)
            {
                case PlayerState.Standby:
                    
                    break;
                case PlayerState.Charging:
                    break;
                case PlayerState.Propulse:
                    _driftPower -= 0.02f;
                    if (_driftPower <= 0)
                    {
                       
                        _power = Mathf.Lerp(_power, 0f, 2f * Time.fixedDeltaTime);
                        if (_power <= 0.1f)
                        {
                            _playerState = PlayerState.Standby;
                            _power = 0;
                            _driftPower = 0;
                        }
                    }
                    _rb.AddForce(transform.forward * 0.6f * (_power / 10F), ForceMode.VelocityChange);
                    
                    break;
                default:
                    break;
            }

            if (_rb.velocity.sqrMagnitude > 324f)
            {
                _rb.velocity = _rb.velocity.normalized * 18f;
                //Debug.Log(_rb.velocity.sqrMagnitude); 
            }

        }

       
    }

    void TouchBinding()
    {
        if (_isPlayer1)
        {
            _horizontalInput = "Horizontal_P1";
            _verticalInput = "Vertical_P1";
            _actionInput = "Fire_P1";
        }
        else
        {
            _horizontalInput = "Horizontal_P2";
            _verticalInput = "Vertical_P2";
            _actionInput = "Fire_P2";
        }
    }

    void Move()
    {
        if (Input.GetAxis(_horizontalInput) > 0)
        {
            Quaternion deltaRotation = Quaternion.Euler(new Vector3(0, _turnSpeed, 0) * Time.fixedDeltaTime);

           
            _rb.MoveRotation(_rb.rotation * deltaRotation);
        }
        if (Input.GetAxis(_horizontalInput) < 0)
        {
            Quaternion deltaRotation = Quaternion.Euler(new Vector3(0, -_turnSpeed, 0) * Time.fixedDeltaTime);
            _rb.MoveRotation(_rb.rotation * deltaRotation);
        }
    }

    void CantSpam()
    {
        StartCoroutine(CantSpamCoroutine());
    }

    IEnumerator CantSpamCoroutine()
    {
        yield return new WaitForSeconds(_timeBeforeSpam);
        _canPropulse = true;
    }

    void PowerCharge()
    {
        if (Input.GetButton(_actionInput))
        {
            if (_canPropulse)
            {
                _canPropulse = false;
                CantSpam();
            }
            _animator.SetBool("Charge", true);
            _animator.speed = 1f;
            _playerState = PlayerState.Charging;
            if (_power <= _maxPower)
            {
                _power += Time.fixedDeltaTime * _chargeSpeed;

                if (_power < 4)
                {
                    _animator.speed = 1.2f;
                    _yellowSteam?.Invoke();
                }
                else if (_power < 8)
                {
                    _animator.speed = 1.4f;
                    _orangeSteam?.Invoke();

                }
                else if (_power < 12)
                {
                    _animator.speed = 10f;
                    _violetSteam?.Invoke();
                }
            }
        }
        if (Input.GetButtonUp(_actionInput))
        {
            _animator.SetBool("Charge", false);
            _noSteam?.Invoke();

            _driftPower = 1;

            //Vector3 Impulse = _power * transform.forward;
            //_rb.AddForce(Impulse, ForceMode.Impulse);
            _playerState = PlayerState.Propulse;



            if (_power < 4)
            {

                Instantiate(_fxPropulse[0], _fxSpawnPoint);

            }
            else if (_power > 4 && _power < 8)
            {

                Instantiate(_fxPropulse[1], _fxSpawnPoint);

            }
            else if (_power > 8 && _power < 12)
            {
                Instantiate(_fxPropulse[2], _fxSpawnPoint);
            }



        }

        if (Input.GetButtonDown(_actionInput) && !_canPropulse)
        {
            _power = 0;
        }

    }

    void PowerImpulse()
    {
        if (_playerState != PlayerState.Propulse)
        {

            
            if (Input.GetButtonUp(_actionInput))
            {

                Vector3 Impulse = _power * transform.forward;
                _rb.AddForce(Impulse, ForceMode.Impulse);
                _playerState = PlayerState.Propulse;




            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Plaices")
        {
            _canMultiply = true;
            StopAllCoroutines();
            StartCoroutine(Multiply());
            GameManager_IromMum.instance.AddPoints(_isPlayer1, _multiplier);
            RandomPlaices.instance.RemovePlaces();
            _source.PlayOneShot(_plaicesIroned);
            Destroy(other.gameObject);
        }
    }
    
    IEnumerator Multiply()
    {
        if (_multiplier <= 3)
        {
            _multiplier++;

        }
        else if (_multiplier >= 3)
        {
            PresentatorVoice.instance.StartSpeaking(true, true); // When the player get a lot of points  
        }
        yield return new WaitForSeconds(1f);
        _multiplier = 1;
        _canMultiply = false;

    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "OffLimit")
        {
            foreach (ContactPoint contact in collision.contacts)
            {
                _bubble._angryEvent?.Invoke();
                Instantiate(_fxCollision, contact.point, Quaternion.identity);
                PresentatorVoice.instance.StartSpeaking(true, false);
            }

        }
    }

    void DisplayInfoCharacter()
    {
        if (_isPlayer1)
        {
            _kartMat.materials[0].SetColor("_BaseColor", META.MetaGameManager.instance._player1._color);
            //_sprite.sprite = META.MetaGameManager.instance._player1._ironMum_sprite;
        }
        else if (!_isPlayer1)
        {
            _kartMat.materials[0].SetColor("_BaseColor", META.MetaGameManager.instance._player2._color);
            //_sprite.sprite = META.MetaGameManager.instance._player2._ironMum_sprite;
        }
        
    }







}
