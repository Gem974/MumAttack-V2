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
    }

    private void FixedUpdate()
    {
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
        base.OnAction(context);
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
                //FAIRE PASSER LA BOMBE DU JOUEUR SI IL SE TROUVE DANS LA ZONE D'ACTION
            }
        }
        else
        {
            if (_actionNewInput)
            {
                //FAIRE SAUTER LES ELEMENTS DE DECOR INTERACTIBLES SI IL SE TROUVE DANS LA ZONE D'ACTION
            }
        }
    }
}




