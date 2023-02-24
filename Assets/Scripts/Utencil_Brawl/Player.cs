using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    [Header("Clamp Displacement")]
    [SerializeField] float _clamping = 4.4f;

    public bool _isPlayerOne;

    public float _moveSpeed = 5f;
    private Vector3 _move;

    public int _touches = 5;

    public float _shootRate = 1f;
    private float _shootCountdown;

    public GameObject _projectilePrefab;
    public Transform _firePoint;

    public Transform _Lanceur;

    public Image _actualtouche;
    public Image _touchepoints;
    public Sprite[] _sprites;

    public Image _circleFill;
    [Range(1, 0)] float _progress;

  
    void Start()
    {
        //Debug.Log(_Touches);
        _progress = _shootCountdown;
        _shootCountdown -= Time.deltaTime;
        LifeChecker();
    }


    void Update()
    {
        _circleFill.fillAmount = _shootCountdown;

        if (Input.GetButtonDown("Fire_P1") && _isPlayerOne)
        {
            Shoot();
        }
        if (Input.GetButtonDown("Fire_P2") && !_isPlayerOne)
        {
            Shoot();
        }
    }


    private void FixedUpdate()
    {       
        //Movement
        movePlayer();      

        //_ShootCountdown Constently dimminushing
        _shootCountdown -= Time.deltaTime;

    }

  
    public void movePlayer()
    {
        if (_isPlayerOne)
        {
            //Vector2 Axis created in OldInputSystem
            _move = new Vector3(0f, 0f, Input.GetAxis("Vertical_P1")); 

            transform.Translate(_move * _moveSpeed * Time.deltaTime, Space.World);

            ClampDisplacement();


        }
        else if (!_isPlayerOne)
        {
            //Vector2 Axis created in OldInputSystem
            _move = new Vector3(0f, 0f, Input.GetAxis("Vertical_P2"));

            transform.Translate(_move * _moveSpeed * Time.deltaTime, Space.World);

            ClampDisplacement();
        }
    }


    public void Shoot()
    {
        if (_shootCountdown <= 0f)
        {
            GameObject _bulletLifted = Instantiate(_projectilePrefab, _firePoint.position, _Lanceur.rotation);

            if (_move.z >= 0.1f) _bulletLifted.GetComponent<Bullet>()._IsArcLeft = true;         //SPECIAL LEFT
            else if (_move.z <= -0.1f) _bulletLifted.GetComponent<Bullet>()._IsArcRight = true;  //SPECIAL RIGHT
            else                                                                               //DEFAULT SHOT
            {
                Destroy(_bulletLifted);
                GameObject _bulletStraight = Instantiate(_projectilePrefab, _firePoint.position, _firePoint.rotation);
                _bulletStraight.GetComponent<Bullet>()._IsStraight = true; 
            }

            _shootCountdown = 1 / _shootRate;
        }

        #region //DO THE SAME THING BUT NOT OPTI
          /*//DEFAULT SHOT J1_J2
            if (_shootCountdown <= 0f && _move.z == 0f)
            {
            //Debug.Log("I've Shot");
            GameObject _bullet = Instantiate(_projectilePrefab, _firePoint.position, _firePoint.rotation);
            _bullet.GetComponent<Bullet>()._IsStraight = true;
                _shootCountdown = 1 / _shootRate;
            }

            //J1
            //SPECIAL LEFT
            else if (_shootCountdown <= 0f && _move.z <= -1f && _isPlayerOne)
            {
                 GameObject _bullet = Instantiate(_projectilePrefab, _firePoint.position, _Lanceur.rotation);
                _bullet.GetComponent<Bullet>()._IsArcLeft = true;
                _shootCountdown = 1 / _shootRate;
            }
            //SPECIAL RIGHT
            else if (_shootCountdown <= 0f && _move.z >= 1f && _isPlayerOne)
            {

                 GameObject _bullet = Instantiate(_projectilePrefab, _firePoint.position, _Lanceur.rotation);
                _bullet.GetComponent<Bullet>()._IsArcRight = true;
                _shootCountdown = 1 / _shootRate;
            }

            //J2
            //SPECIAL LEFT
            else if (_shootCountdown <= 0f && -_move.z <= -1f && !_isPlayerOne)
            {

                 GameObject _bullet = Instantiate(_projectilePrefab, _firePoint.position, _Lanceur.rotation);
                _bullet.GetComponent<Bullet>()._IsArcLeft = true;
                _shootCountdown = 1 / _shootRate;
            }
            //SPECIAL RIGHT
            else if (_shootCountdown <= 0f && -_move.z >= 1f && !_isPlayerOne)
            {

                 GameObject _bullet = Instantiate(_projectilePrefab, _firePoint.position, _Lanceur.rotation);
                _bullet.GetComponent<Bullet>()._IsArcRight = true;
                _shootCountdown = 1 / _shootRate;
            }
          */
        
        #endregion
    }


    public void LifeChecker()
    {
        _touchepoints.sprite = _sprites[_touches];

        #region //DO THE SAME THING BUT NOT OPTI

        /*if (_touches == 5)
        {
            _touchepoints.sprite = _sprites[5];
        }
        else if (_touches == 4)
        {
            _touchepoints.sprite = _sprites[4];
        }
        else if (_touches == 3)
        {
            _touchepoints.sprite = _sprites[3];
        }
        else if (_touches == 2)
        {
            _touchepoints.sprite = _sprites[2];
        }
        else if (_touches == 1)
        {
            _touchepoints.sprite = _sprites[1];
        }
        else if (_touches == 0)
        {
            _touchepoints.sprite = _sprites[0];
        }*/
        #endregion
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("AIE PUTAIN");
        _touches--;
        LifeChecker();
    }

    void ClampDisplacement()
    {
        if (transform.position.z > _clamping)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, _clamping);
        }
        else if (transform.position.z < -_clamping)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -_clamping);
        }
    }

}
