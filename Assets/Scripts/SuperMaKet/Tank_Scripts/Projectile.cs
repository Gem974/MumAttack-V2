using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    
    private int _mur;

    public Tank_movement _tanMoov;

    [SerializeField] private AudioSource _soundSavate;

    [SerializeField] private AudioSource _soundRebond;


    private void Start()
    {
        _soundSavate.Play();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Mur"))
        {
            _mur++;
            _soundRebond.Play();
            if (_mur == 5)
            {
                Destroy(gameObject);
            }
        }
        else if (collision.transform.CompareTag("MurFragile"))
        {
            Destroy(gameObject);   
        }


        
    }


    











}
