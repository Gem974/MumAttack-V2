using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sumo_CollisionDetection : MonoBehaviour
{

    [SerializeField] bool _isPlayer1;
    [SerializeField] Animator _animator;
    
    public bool _inCollision, _falling, _stopColliFX;
    public Sumo_GameManager _gameManager;
    



    private void Start()
    {
        DisplayInfoCharacter();
        _gameManager = FindObjectOfType<Sumo_GameManager>();
        //_animator = GetComponent<Animator>();
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
            //transform.Rotate(0.0f, 0.0f, 37.0f, Space.Self);
            _animator.SetTrigger("Fall");
            PresentatorVoice.instance.StartSpeaking(false, false);

        }
        else if (collision.gameObject.tag == "OffLimit2")
        {
            //transform.Rotate(0.0f, 0.0f, -37.0f, Space.Self);
            _animator.SetTrigger("Fall");
            PresentatorVoice.instance.StartSpeaking(false, false);
            
            
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "OffLimit")
        {
            //_gameManager.ResetRot(1);
            _animator.SetTrigger("ComeBack");
            Debug.Log("STP");
        }
        else if (other.gameObject.tag == "OffLimit2")
        {
            _animator.SetTrigger("ComeBack");
            //_gameManager.ResetRot(2);
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        //if (transform.rotation.z != 0f)
        //{
        //    if (collision.gameObject.tag == "PreOff")
        //    {
        //        Debug.Log("REMETE AOU DROITE LMKT");
        //        _gameManager.ResetRot(1);
        //    }
        //    else if (collision.gameObject.tag == "PreOff2")
        //    {
        //        _gameManager.ResetRot(2);
        //    } 
        //}
        
            
        
    }

    void DisplayInfoCharacter()
    {
        if (_isPlayer1)
        {
            _animator.runtimeAnimatorController = META.MetaGameManager.instance._player1.sumom_animatorController;
        }
        else
        {
            _animator.runtimeAnimatorController = META.MetaGameManager.instance._player2.sumom_animatorController;
        }
    }

   


}
