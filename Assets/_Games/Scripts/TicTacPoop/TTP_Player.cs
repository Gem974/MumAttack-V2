using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.InputSystem.Users;

public class TTP_Player : PlayerBehaviour
{
    [Header("Stats")]
    public float _speed = 4f;
    public float _currentSpeed; 
    public bool _hasBomb = false;
    public float _stunDuration;

    [Header("Action")]
    public float _actionCD = 0.5f;
    private float _nextAction;
    public float _actionDistance = 2f;
    public LayerMask _decoInteractible;

    [Header("Composantes")]
    private Rigidbody _rb;
    private RaycastHit _forward;
    public bool _hasControl = true;

    [Header("Debug")]
    public GameObject _iconIndicator;
    public Sprite _bombSprite, _stunSprite;
    public GameObject _mesh;
    public Sprite _goodHead, _badHead;

    [Header("Juice")]
    public GameObject _teleportVFX;
    public GameObject _explodeVFX;
    public GameObject _hitPlayer;
    private Vector3 _offset = new Vector3(0, 1f, 0);

    //Event pour la touche Move
    public override void OnMove(InputAction.CallbackContext context)
    {
        base.OnMove(context);
    }

    //Event pour la touche d'action
    public override void OnAction(InputAction.CallbackContext context)
    {
        _actionNewInput = context.action.triggered;
    }

    //Event pour la touche pause
    public override void OnPause(InputAction.CallbackContext context)
    {
        base.OnPause(context);
    }

    // Start is called before the first frame update
    public void Start()
    {
        base.ForceController();
        if (_isPlayer1)
        {
            _goodHead = META.MetaGameManager.instance._player1._goodHead;
            _badHead = META.MetaGameManager.instance._player1._badHead;

        }
        else
        {
            _goodHead = META.MetaGameManager.instance._player2._goodHead;
            _badHead = META.MetaGameManager.instance._player2._badHead;
        }
        _rb = GetComponent<Rigidbody>();
        if (_hasBomb) 
        {
            _currentSpeed = _speed;
            _mesh.GetComponent<SpriteRenderer>().sprite = _badHead;
            _iconIndicator.SetActive(true);
            _iconIndicator.GetComponent<SpriteRenderer>().sprite = _bombSprite;
        }
        else
        {
            _currentSpeed = _speed * 0.8f;
            _mesh.GetComponent<SpriteRenderer>().sprite = _goodHead;
            _iconIndicator.SetActive(false);
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        //Orientation du mesh pour qu'il fasse face à la camera
        //_mesh.transform.forward = Camera.main.transform.forward;

        if (TTP_GameManager.instance._canPlay)
        {
            //Modification de la vitesse en fonction de la bombe
            if (_hasBomb)
                _currentSpeed = _speed;
            else
                _currentSpeed = _speed * 0.8f;

            //CD sur l'action
            if (Time.time >= _nextAction && _hasControl)
            {
                Action();
                _nextAction = Time.time + _actionCD;
            }
        }
    }

    private void FixedUpdate()
    {
        if (TTP_GameManager.instance._canPlay)
        {
            Debug.DrawRay(transform.position, transform.forward * _actionDistance, Color.green);
            if (_hasControl)
                Movement();
        }
    }

    //Methode de mouvement
    void Movement()
    {
        if(_moveInput != null)
        {
            Vector3 movement = new Vector3(_moveInput.x, 0f, _moveInput.y).normalized;

            //Orientation de Sprite selon la direction (gauche / droite)
            //if(_moveInput.x > 0f)
            //{
            //    _mesh.GetComponent<SpriteRenderer>().flipX = false;
            //}
            //else if(_moveInput.x < 0f)
            //{
            //    _mesh.GetComponent<SpriteRenderer>().flipX = true;
            //}


            //Gere l'orientation du perso (important pour le raycast de l'action)
            if (movement != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(movement, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
            }

            _rb.velocity = movement * _currentSpeed;

        }
    }

    // Methode d'action
    void Action()
    {
        // Avec la bombe
        if (_hasBomb)
        {
            if (_actionNewInput)
            {
                //On tire le Raycast
                if(Physics.Raycast(transform.position, transform.forward, out _forward, _actionDistance))
                {
                    //Si on touche l'autre joueur
                    if(_forward.collider.GetComponent<TTP_Player>() != null)
                    {
                        Instantiate(_hitPlayer, _forward.point, Quaternion.identity);
                        var otherPlayer = _forward.collider.GetComponent<TTP_Player>();
                        //On passe la bombe et on le stun
                        _iconIndicator.SetActive(false);
                        _mesh.GetComponent<SpriteRenderer>().sprite = _goodHead;
                        _hasBomb = false;
                        otherPlayer.GetCatch();
                        Debug.Log("Action avec bombe");
                    }
                }
            }
        }
        else // Sans la bombe
        {
            if (_actionNewInput)
            {
                //On tire le raycast et on verifie qu'il touche un decor interactible (via le layer spécifique)
                if (Physics.Raycast(transform.position, transform.forward, out _forward, _actionDistance, _decoInteractible))
                {
                    Instantiate(_teleportVFX, transform.position + _offset, Quaternion.identity);
                    // Récupération du collider de l'objet en collision
                    Collider objectCollider = _forward.collider;

                    // Calcul de la nouvelle position du personnage qui traverse l'objet touché
                    // Calcul : position actuel du joueur + la surface à traverser (collider) à partir de la normal de la face touchée * -1 pour aller dans le sens inverse de la normal
                    Vector3 newPosition = transform.position + _forward.normal * objectCollider.bounds.size.magnitude * -1f;
                    newPosition.y = 1f;
                    // Affectation la position calculée au personnage
                    transform.position = newPosition;
                    Instantiate(_teleportVFX, newPosition + _offset, Quaternion.identity);
                }
            }
        }
    }

    //Methode declenchant le Stun
    public void GetCatch()
    {
        StartCoroutine(Stun());
    }

    //Desactive le controle du player pour la durée du stun et lui donne la bombe
    public IEnumerator Stun()
    {
        _mesh.GetComponent<SpriteRenderer>().sprite = _badHead;
        _iconIndicator.SetActive(true);
        _iconIndicator.GetComponent<SpriteRenderer>().sprite = _stunSprite;
        _hasBomb = true;
        _hasControl = false;
        yield return new WaitForSeconds(_stunDuration);
        _hasControl = true;
        _iconIndicator.GetComponent<SpriteRenderer>().sprite = _bombSprite;
    }

    public void Explode()
    {
        Instantiate(_explodeVFX, transform.position + _offset, Quaternion.identity);
    }
}




