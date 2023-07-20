using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.InputSystem.Users;

public class Player_babytoystorm : PlayerBehaviour
{
    [Header("Baby Toy Storm")]
    [SerializeField] Rigidbody _rb;
    [SerializeField] float _speed = 1;

    
    // Start is called before the first frame update
    public void Start()
    {
        base.ForceController();
        _rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    //public override void ChangeActionMap()
    //{
    //    base.ChangeActionMap();
    //}

    public override void OnMove(InputAction.CallbackContext context)
    {
        base.OnMove(context);
        

    }
    

    public void Move()
    {
        _rb.velocity = new Vector3(_moveInput.x * _speed, 0, _moveInput.y * _speed);
    }










}
