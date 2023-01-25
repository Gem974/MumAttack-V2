using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu_TextSlider : MonoBehaviour
{
    
    [SerializeField] float _speed;
    [SerializeField] Vector3 _startPos;
    [SerializeField] float _restart;

    private void Start()
    {
        _startPos = transform.position;
    }

    private void Update()
    {

        if (transform.position.x > -_restart)
        {
            transform.Translate(Vector3.left * _speed * UnityEngine.Time.deltaTime, Space.Self);
        }
        else if (transform.position.x < -_restart)
        {
            transform.position = _startPos;
        }
       
    }

  

}
