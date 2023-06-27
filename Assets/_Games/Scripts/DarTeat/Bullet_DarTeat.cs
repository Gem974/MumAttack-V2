using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_DarTeat : MonoBehaviour
{
    //Variables
    public float _speed = 5f;
    private Rigidbody _rb;


    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        var i = Random.Range(10f, 40f);
        _rb.AddForce(new Vector3(0, i*1.2f, -i) * _speed);
        _rb.AddTorque(new Vector3(i, i, i));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
