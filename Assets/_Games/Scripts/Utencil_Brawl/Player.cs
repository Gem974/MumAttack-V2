using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


namespace Utencil_Brawl
{
    public class Player : MonoBehaviour
    {

        [Header("Clamp Displacement")]
        [SerializeField] float _clamping = 4.4f;

        [SerializeField] Animator _animator;

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

        public UnityEvent _onHit, _onShoot, _Startrun, _Stoprun;


        void Start()
        {
            //Debug.Log(_Touches);
            _progress = _shootCountdown;
            _shootCountdown -= Time.deltaTime;
            if (_isPlayerOne)
            {
                _animator.runtimeAnimatorController = META.MetaGameManager.instance._player1.utencil_animatorController;
            }
            else
            {
                _animator.runtimeAnimatorController = META.MetaGameManager.instance._player2.utencil_animatorController;
            }
            LifeChecker();
        }


        void Update()
        {
            if (UtencilBrawl_GameManager.instance._canPlay)
            {
                _circleFill.fillAmount = _shootCountdown;

                if (PauseGame.instance._canPause)
                {
                    if (Input.GetButtonDown("Fire_P1") && _isPlayerOne)
                    {
                        Shoot();
                    }
                    if (Input.GetButtonDown("Fire_P2") && !_isPlayerOne)
                    {
                        Shoot();
                    } 
                } 
            }
        }


        private void FixedUpdate()
        {
            if (UtencilBrawl_GameManager.instance._canPlay)
            {
                if (PauseGame.instance._canPause)
                {
                    //Movement
                    movePlayer();

                    //_ShootCountdown Constently dimminushing
                    _shootCountdown -= Time.deltaTime;  
                }
            }

        }


        public void movePlayer()
        {
            if (_isPlayerOne)
            {
                if (Input.GetAxis("Vertical_P1") != 0)
                {
                    //Vector2 Axis created in OldInputSystem
                    _move = new Vector3(0f, 0f, Input.GetAxis("Vertical_P1"));
                    //SFX Run on Loop
                    _Startrun?.Invoke();

                    transform.Translate(_move * _moveSpeed * Time.deltaTime, Space.World);
                    _animator.SetTrigger("Run");
                    ClampDisplacement();
                }
                else
                {
                    //SFX Run Stop
                    _Stoprun?.Invoke();
                    _move = Vector3.zero;
                    _animator.SetTrigger("Static");
                }


            }
            else if (!_isPlayerOne)
            {
                //Vector2 Axis created in OldInputSystem
                if (Input.GetAxis("Vertical_P2") != 0)
                {
                    _move = new Vector3(0f, 0f, Input.GetAxis("Vertical_P2"));

                    transform.Translate(_move * _moveSpeed * Time.deltaTime, Space.World);
                    _animator.SetTrigger("Run");

                    ClampDisplacement();
                }
                else
                {
                    _move = Vector3.zero;
                    _animator.SetTrigger("Static");
                }
            }

            
        }


        public void Shoot()
        {
            if (_shootCountdown <= 0f)
            {
                GameObject _bulletLifted = Instantiate(_projectilePrefab, _firePoint.position, _Lanceur.rotation);
                

                if (_move.z >= 0.1f)
                {
                    _bulletLifted.GetComponent<Bullet>()._IsArcLeft = _isPlayerOne ? false : true;
                    _animator.SetTrigger("ShootArc");
                }
                else if (_move.z <= -0.1f)
                {
                    _bulletLifted.GetComponent<Bullet>()._IsArcLeft = _isPlayerOne ? true : false;
                    _animator.SetTrigger("ShootArc");
                }
                else
                {
                    Destroy(_bulletLifted);
                    GameObject _bulletStraight = Instantiate(_projectilePrefab, _firePoint.position, _firePoint.rotation);
                    _bulletStraight.GetComponent<Bullet>()._IsStraight = true;
                    _animator.SetTrigger("ShootStraight");
                }

                _onShoot?.Invoke();
                _shootCountdown = 1 / _shootRate;
                //SFX Shoot
                //Debug.Log("MoveZ " + _move.z);
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
            _onHit?.Invoke();
            //SFX Hit
            LifeChecker();
            PresentatorVoice.instance.StartSpeaking(true, false);
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
}
