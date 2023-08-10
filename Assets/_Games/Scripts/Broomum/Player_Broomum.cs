using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.InputSystem.Users;

public class Player_Broomum : PlayerBehaviour
{

    [Header("Player Broomum")]
    [SerializeField] Rigidbody _rb;
    [SerializeField] float _speed;

    public void Start()
    {
        base.ForceController();
    }

    private void FixedUpdate()
    {
        Move();
    }
    void Move()
    {
        _rb.velocity = new Vector3(_moveInput.x * _speed, 0, _moveInput.y * _speed);
    }

    public override void OnMove(InputAction.CallbackContext context)
    {
        base.OnMove(context);
        
    }

    //Event pour la touche d'action
    public override void OnAction(InputAction.CallbackContext context)
    {
        base.OnAction(context);
    }


    public override void OnPause(InputAction.CallbackContext context)
    {
        
        base.OnPause(context);
       
    }
}




