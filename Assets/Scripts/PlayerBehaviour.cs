using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [Header("Physics")]
    [SerializeField] Vector3 PlayerMovementInput;
    [SerializeField] Rigidbody _rb;
    [SerializeField] float _moveSpeed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        MovePlayer();

    }

    void MovePlayer()
    {
        Vector3 MoveVector = transform.TransformDirection(PlayerMovementInput) * _moveSpeed;
        _rb.velocity = new Vector3(MoveVector.x, _rb.velocity.y, MoveVector.z);
    }

    void Interact()
    {

    }
}
