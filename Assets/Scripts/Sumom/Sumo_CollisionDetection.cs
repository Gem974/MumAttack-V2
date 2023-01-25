using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sumo_CollisionDetection : MonoBehaviour
{

    public bool _inCollision, _falling, _stopColliFX;
    public Sumo_GameManager _gameManager;



    //[SerializeField] Rigidbody2D _rb1, _rb2;

    private void Start()
    {
        _gameManager = FindObjectOfType<Sumo_GameManager>();
    
        _inCollision = false;
        _stopColliFX = true;


    }


    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _stopColliFX = false;
        }
        
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _inCollision = true; 
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "OffLimit")
        {
            transform.Rotate(0.0f, 0.0f, 37.0f, Space.Self);
        }
        else if (collision.gameObject.tag == "OffLimit2")
        {
            transform.Rotate(0.0f, 0.0f, -37.0f, Space.Self);
        }
        else if (transform.rotation.z != 0f)
        {
            if (collision.gameObject.tag == "PreOff")
            {
                _gameManager.ResetRot(1); 
                Debug.Log("STP");
            }
            else if (collision.gameObject.tag == "PreOff2")
            {
                _gameManager.ResetRot(2);
            } 
        }


        
    }

    private void OnTriggerStay(Collider collision)
    {
        if (transform.rotation.z != 0f)
        {
            if (collision.gameObject.tag == "PreOff")
            {
                Debug.Log("REMETE AOU DROITE LMKT");
                _gameManager.ResetRot(1);
            }
            else if (collision.gameObject.tag == "PreOff2")
            {
                _gameManager.ResetRot(2);
            } 
        }
        
            
        
    }


}
