using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilDeco : MonoBehaviour
{
    //Variables
    public Sprite _base, _applati;
    public float _duration = 5f;
    private SpriteRenderer _renderer;
    private bool _alreadyTrigger = false;
    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _renderer.sprite = _base;
    }

    // Update is called once per frame
    void Update()
    {
        //if (_alreadyTrigger == false)
        //    transform.forward = Camera.main.transform.forward;
        //else
        //    transform.rotation = Quaternion.Euler(90, 0, 0);
    }

    public void Triggered()
    {
        if(_alreadyTrigger == false)
            StartCoroutine(Applatissement());
    }

    IEnumerator Applatissement()
    {
        _alreadyTrigger = true;
        _renderer.sprite = _applati;
        yield return new WaitForSeconds(_duration);
        _renderer.sprite = _base;
        _alreadyTrigger = false;
    }
}
