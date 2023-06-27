using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward_DarTeat : MonoBehaviour
{
    //Variables
    public float _speed;
    public Vector3 _dir;
    public bool _canMove = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_canMove)
           transform.Translate(_dir * _speed * Time.deltaTime);
    }


    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
