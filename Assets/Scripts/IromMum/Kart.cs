using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Kart : MonoBehaviour
{
    public bool _isPlayer1;
    public enum PlayerState
    {
       Standby, Charging, Propulse
    }

    [Header("Kart Movement")]
    [SerializeField] PlayerState _playerState;
    [SerializeField] Rigidbody _rb;
    [SerializeField] float _maxPower;
    [SerializeField] Vector3 _moveForce;
    [SerializeField] float _power;
    [SerializeField] float _chargeSpeed;
    [SerializeField] float _turnSpeed;
    [SerializeField] float _driftPower = 1;

    [SerializeField] KeyCode _right, _left, _up, _down, _action;

    [Header("Multiplier System")]
    [SerializeField] bool _canMultiply;
    [SerializeField] float _nextime;
    [SerializeField] int _multiplier = 1;

    [Header("FXs")]
    [SerializeField] GameObject[] _fxPropulse;
    [SerializeField] Transform _fxSpawnPoint;
    [SerializeField] GameObject _fxCollision;
    [SerializeField] UnityEvent _yellowSteam, _orangeSteam, _violetSteam, _noSteam;



    

    private void Start()
    {
        TouchBinding();
        _playerState = PlayerState.Standby;
    }

    void Update()
    {
        if (GameManager_IromMum.instance._canPlay)
        {
            
            ImpulseCharge();
            Move();
 
        }

    }

    private void FixedUpdate()
    {

        if (_playerState == PlayerState.Propulse)
        {
            _driftPower -= 0.02f;
            _rb.AddForce(transform.forward * 0.3f * (_power / 10), ForceMode.VelocityChange);
            if (_driftPower <= 0)
            {
                _driftPower = 0f;
                _playerState = PlayerState.Standby;
                _power = 0;
            }
        }
    }

    void TouchBinding()
    {
        if (_isPlayer1)
        {
            _right = KeyCode.D;
            _left = KeyCode.Q;
            _up = KeyCode.Z;
            _down = KeyCode.S;
            _action = KeyCode.A;
        }
        else
        {
            _right = KeyCode.RightArrow;
            _left = KeyCode.LeftArrow;
            _up = KeyCode.UpArrow;
            _down = KeyCode.DownArrow;
            _action = KeyCode.RightControl;
        }
    }
    void Move()
    {
        if (Input.GetKey(_right))
        {
            Quaternion deltaRotation = Quaternion.Euler(new Vector3(0, _turnSpeed, 0) * Time.deltaTime);
            _rb.MoveRotation(_rb.rotation * deltaRotation);
        }
        if (Input.GetKey(_left))
        {
            Quaternion deltaRotation = Quaternion.Euler(new Vector3(0, -_turnSpeed, 0) * Time.deltaTime);
            _rb.MoveRotation(_rb.rotation * deltaRotation);
        }
    }

    void ImpulseCharge()
    {
        if (_playerState != PlayerState.Propulse)
        {

            if (Input.GetKey(_action))
            {
                _playerState = PlayerState.Charging;
                if (_power <= _maxPower)
                {
                    _power += Time.deltaTime * _chargeSpeed;

                    if (_power < 4)
                    {
                       
                        _yellowSteam?.Invoke();
                    }
                    else if (_power > 4 && _power < 8)
                    {
                     
                        _orangeSteam?.Invoke();

                    }
                    else if (_power > 8 && _power < 12)
                    {
                     
                        _violetSteam?.Invoke();
                    }
                }
            }
            if (Input.GetKeyUp(_action))
            {

                _noSteam?.Invoke();

                _driftPower = 1;
                
                Vector3 Impulse = _power * transform.forward;
                _rb.AddForce(Impulse, ForceMode.Impulse);
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

            Destroy(other.gameObject);
        }
    }
    
    IEnumerator Multiply()
    {
        _multiplier++;
        yield return new WaitForSeconds(1f);
        _multiplier = 1;
        _canMultiply = false;

    }


    private void OnCollisionEnter(Collision collision)
    {
        if (gameObject.tag == "Player")
        {
            foreach (ContactPoint contact in collision.contacts)
            {
                Instantiate(_fxCollision, contact.point, Quaternion.identity);
            }

        }
    }







}
