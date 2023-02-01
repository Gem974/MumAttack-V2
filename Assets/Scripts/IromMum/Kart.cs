using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Kart : MonoBehaviour
{
    public bool _isPlayer1;
    [Header("Kart Movement")]
    [SerializeField] Rigidbody _rb;
    [SerializeField] float _maxPower;
    [SerializeField] float _power;
    [SerializeField] float _chargeSpeed;
    [SerializeField] float _turnSpeed;

    [SerializeField] KeyCode _right, _left, _up, _down, _action;

    [Header("FXs")]
    [SerializeField] GameObject _fxPropulse;
    [SerializeField] Transform _fxSpawnPoint;

    [Header("Multiplier System")]
    [SerializeField] bool _canMultiply;
    [SerializeField] float _nextime;
    [SerializeField] int _multiplier = 1;


    

    private void Start()
    {
        if (GameManager_IromMum.instance._canPlay)
        {
            TouchBinding();
        }
    }

    void Update()
    {
        if (GameManager_IromMum.instance._canPlay)
        {
            ImpulseCharge();
            Move();
 
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
        if (Input.GetKey(_action))
        {
            if (_power <= _maxPower)
            {
                _power += Time.deltaTime * _chargeSpeed;

                if (_power < 2)
                {
                    //Jaune

                    
                    
                    

                }
                else if (_power > 2 && _power < 6)
                {
                    Debug.Log("PIOU");
                    
                }
                else if (_power > 6 && _power < 12)
                {
                   
                }
            }
        }
        if (Input.GetKeyUp(_action))
        {
            Vector3 Impulse = _power * transform.forward;
            _rb.AddForce(Impulse, ForceMode.Impulse);
            _power = 0;

            Instantiate(_fxPropulse, _fxSpawnPoint);
           
            //FeedBackManager.instance._propulsion?.Invoke();
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


    

    


}
