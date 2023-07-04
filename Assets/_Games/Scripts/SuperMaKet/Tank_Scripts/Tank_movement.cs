using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank_movement : MonoBehaviour
{
     public Game_manager _gameManager;
    public Ui _uiScript;
     public GameObject _fxFire;
    public GameObject _fxDamage;
    public AnimCam _animCam;

    [Header("TANK_DATA")]
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _currentSpeed;
    [SerializeField] private float _speedRotate;
    [SerializeField] private _player Player;
    public bool _isPlayer1;
    private KeyCode _up, _down, _left, _right;
    [SerializeField] private int _life = 3;
    private string _P1Win = "Player 1 Wins !";
    private string _P2Win = "Player 2 Wins !";
    //private string _Matchnull = "Match null";
    

    public enum _player
    {
        Player1,
        Player2
    }

    [Header("SYSTEME_TIR")]
    //système de tir
    [SerializeField] private GameObject _fireposition;
    [SerializeField] private GameObject _projectile;
    [SerializeField] private KeyCode _tir;
    [SerializeField] private float _latence;
    [SerializeField] private bool _onFire = true;
    [SerializeField] private int _speedFire = 900;
    [SerializeField] private AudioSource _soundFire;
    [SerializeField] private AudioSource[] _soundDegat;


    private void Start()
    {
        SetKeys();
    }

    private void Update()
    {
        Mouvement();
        Fire();
        Dead();
        Winner();
        life();
    }

    void SetKeys()
    {
        if (_isPlayer1)
        {
            //move ZQSD
            _up = KeyCode.Z;
            _down = KeyCode.S;
            _left = KeyCode.Q;
            _right = KeyCode.D;

        }
        else
        {
            //move directional button
            _up = KeyCode.UpArrow;
            _down = KeyCode.DownArrow;
            _left = KeyCode.LeftArrow;
            _right = KeyCode.RightArrow;
        }
    }

    void life()
    {
        if (Player == _player.Player1)
        {
            _uiScript._sliderJ1.value = _life;
        }
        if (Player == _player.Player2)
        {
            _uiScript._sliderJ2.value = _life;
        }
    }
    void Mouvement()
    {
        if (Input.GetKey(_up))
            _maxSpeed = 2;
        else if (Input.GetKey(_down))
            _maxSpeed = -2;
        else _maxSpeed = 0;

        _currentSpeed = Mathf.MoveTowards(_currentSpeed, _maxSpeed, 5* UnityEngine.Time.deltaTime);


        transform.Translate(0f, 1f * _currentSpeed * UnityEngine.Time.deltaTime, 0f);

        if (Input.GetKey(_left))
        {
            transform.Rotate(0f,0f, 45f * _speedRotate * UnityEngine.Time.deltaTime);
        }
        if (Input.GetKey(_right))
        {
            transform.Rotate(0f,0f, -45f * _speedRotate * UnityEngine.Time.deltaTime);
        }

    }


    void Fire()
    {
        if (Input.GetKeyUp(_tir))
        {
            GetComponentInChildren<LineRenderer>().enabled = false;

            if (_onFire)
            {
                StartCoroutine(Latence());
               
                
            }
            
        }
        if (Input.GetKey(_tir))
        {

            float laserLenght = Mathf.Infinity;
           

            RaycastHit2D hit = Physics2D.Raycast(_fireposition.transform.position, _fireposition.transform.up , laserLenght );

            Debug.DrawRay(_fireposition.transform.position, _fireposition.transform.up * 10, Color.red);
    
            GetComponentInChildren<LineRenderer>().enabled = true;
            GetComponentInChildren<LineRenderer>().SetPosition(0, _fireposition.transform.position);
            GetComponentInChildren<LineRenderer>().SetPosition(1, hit.point);
            

        }
    }


            



    IEnumerator Latence()
    {
        _onFire = false;
        GameObject projectile = Instantiate(_projectile, _fireposition.transform.position, _fireposition.transform.rotation); //variable local qui servira à désigner le gameobject pour ensuite faire des modification directement sur l'objet ciblé.
        _gameManager.InstanciateFx(_fxFire, _fireposition.transform.position, _fireposition.transform.rotation);
        projectile.GetComponent<Rigidbody2D>().AddForce(projectile.transform.up * _speedFire * UnityEngine.Time.deltaTime, ForceMode2D.Impulse);
        //StartCoroutine(AnimCamShoot());
        _animCam._animCamera.SetBool("Shoot_anim", true);
        _soundFire.Play();
        //_rigid2D.AddForce(transform.up * _speedProjectile * Time.deltaTime, ForceMode2D.Impulse);
        print("klaklaklakboom");
        yield return new  WaitForSeconds(_latence);

        _onFire = true;
        

    }

    IEnumerator AnimCamShoot()
    {
        _animCam._animCamera.SetBool("Shoot_anim", true);
        yield return new WaitForSeconds(.10f);
        _animCam._animCamera.SetBool("Shoot_anim", false);
    }
    IEnumerator AnimDegat()
    {
        _animCam._animCamera.SetBool("degat", true);
        yield return new WaitForSeconds(.10f);
        _animCam._animCamera.SetBool("degat", false);
    }

  
 

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.collider.gameObject.CompareTag("Projectile"))
        //{
        //    Destroy(collision.gameObject);
        //    _gameManager.InstanciateFx(_fxDamage, collision.contacts[0].point, transform.rotation);
        //    _life--;
        //    StartCoroutine(AnimDegat());
        //    _soundDegat[Random.Range(0, _soundDegat.Length)].Play();
        //    print("toucher");
            
        //}
    }

    void Dead()
    {
        if (_life <= 0)
        {
            Destroy(gameObject);
            _gameManager._panelGameOver.SetActive(true);
            _gameManager._panelInGame.SetActive(false);
            _gameManager._endGame = true;
        }
    }

    void Winner()
    {
        if (_gameManager._endGame)
        {
            if (Player == _player.Player1)
            {
                if (_life > 0)
                {
                    _gameManager._Winner.text = _P1Win;
                    _gameManager._bgMusic.Stop();
                }
            }
            else if (Player == _player.Player2)
            {
                if (_life > 0)
                {
                    _gameManager._Winner.text = _P2Win;
                    _gameManager._bgMusic.Stop();
                }
            }
            
        }
    }
}
            
        
        
            
           
          

