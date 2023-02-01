using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightNoise_DarTeat : MonoBehaviour
{
    public float _speed;
    public float _timeBetweenChangeTarget = .5f;
    public Vector3 _target;


    private void Start()
    {
        StartCoroutine("Newtarget");
    }
    void Update()
    {
        if(!GameManager_DarTeat.instance._gameIsFinished)
            transform.Translate(_target * Time.deltaTime, Space.Self);
    }

    IEnumerator Newtarget()
    {
        for(int i = 1; i > 0; i++)
        {
            _target = Random.insideUnitCircle * _speed;
            yield return new WaitForSeconds(_timeBetweenChangeTarget);
        }
    }
}
