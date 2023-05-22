using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mur_fragile : MonoBehaviour
{

    public bool _OnSpawn = false;

    public GameObject _Fx;

    AnimCam _animC;

    public Game_manager _gameManager;
    private AudioSource _soundExplosionMur;

    private void Awake()
    {
        _soundExplosionMur = GetComponent<AudioSource>();
        _animC = FindObjectOfType<AnimCam>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        _soundExplosionMur.Play();
        
        _gameManager.InstanciateFx(_Fx, transform.position,transform.rotation); 

        if (collision.transform.CompareTag("Player") || collision.transform.CompareTag("Projectile"))
        {
           // _OnSpawn = true;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<Collider2D>().enabled = false;
            StartCoroutine(AnimDestroy());
            StartCoroutine("Spawn");
              
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (collision.transform.CompareTag("Player"))
        {
            //_OnSpawn = false;
            StopCoroutine("Spawn");
        }
       
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player") || collision.transform.CompareTag("Projectile"))
        {
            StartCoroutine("Spawn");
            //_OnSpawn = true;
        }
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(2f);
        
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.GetComponent<Collider2D>().enabled = true;
        //_OnSpawn = false;
    }


    public IEnumerator AnimDestroy()
    {
        _animC._animCamera.SetBool("Destroy", true);
        yield return new WaitForSeconds(.10f);
        _animC._animCamera.SetBool("Destroy", false);
    }
}
