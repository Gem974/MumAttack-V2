using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.InputSystem.Users;

public class TTP_Player : PlayerBehaviour
{
    private Rigidbody _rb;
    public float _speed = 4f;
    public float _currentSpeed; 
    public bool _hasBomb = false;
    public float _actionCD = 0.5f;
    private float _nextAction;
    public Vector3 _dir;
    private RaycastHit _forward;
    public float _actionDistance = 2f;
    public LayerMask _decoInteractible;

    // Start is called before the first frame update
    public void Start()
    {
        base.ForceController();
        _rb = GetComponent<Rigidbody>();
        if (_hasBomb)
            _currentSpeed = _speed;
        else
            _currentSpeed = _speed * 0.8f;
    }

    // Update is called once per frame
    void Update()
    {
        if (_hasBomb)
            _currentSpeed = _speed;
        else
            _currentSpeed = _speed * 0.8f;

        if (Time.time >= _nextAction)
        {
            Action();
            _nextAction = Time.time + _actionCD;
        }

        
    }

    private void FixedUpdate()
    {
        transform.forward = _dir;
        Debug.DrawRay(transform.position, transform.forward * _actionDistance, Color.green);
        Movement();
    }

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

    //Methode de mouvement
    void Movement()
    {
        if(_moveInput != null)
        {
            Vector3 velocity = _rb.velocity;
            velocity.x = _moveInput.x * _currentSpeed;
            velocity.z = _moveInput.y * _currentSpeed;
            if (_moveInput.x != 0)
                _dir.x = _moveInput.x;
            if (_moveInput.y != 0)
                _dir.z = _moveInput.y;
            _rb.velocity = velocity;
            _rb.velocity = Vector3.ClampMagnitude(_rb.velocity, _currentSpeed); 
        }
    }


    // Methode d'action
    void Action()
    {
        if (_hasBomb)
        {
            if (_actionNewInput)
            {
                Physics.Raycast(transform.position, transform.forward, out _forward, _actionDistance);
                if(_forward.collider.GetComponent<TTP_Player>() != null)
                {
                    var otherPlayer = _forward.collider.GetComponent<TTP_Player>();
                    _hasBomb = false;
                    otherPlayer._hasBomb = true;
                    otherPlayer.Stun();
                    Debug.Log("Action avec bombe");
                }
                else
                {
                    Debug.Log("MISS Action avec bombe");
                }
            }
        }
        else
        {
            if (_actionNewInput)
            {
                Physics.Raycast(transform.position, transform.forward, out _forward, _actionDistance, _decoInteractible);
                if(_forward.collider != null)
                {
                    //FAIRE TRAVERSER LES ELEMENTS DE DECOR INTERACTIBLES SI IL SE TROUVE DANS LA ZONE D'ACTION
                    _forward.collider.isTrigger = true;
                    Debug.Log("Action sans bombe");
                }
                else
                {
                    Debug.Log("MISS Action sans bombe");
                }
            }
        }
    }



    public void Stun()
    {
        //FAIRE STUN QUAND ON PREND LE BEBE
    }
}




