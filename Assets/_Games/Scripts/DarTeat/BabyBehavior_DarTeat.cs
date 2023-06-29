using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BabyBehavior_DarTeat : MonoBehaviour
{
    public TextMeshProUGUI _currenValue;
    public float _speed;
    public GameObject _teat, _vfxSuccess;
    public GameObject[] _tearsVFX;
    public Collider _col;
    [HideInInspector]
    public int _minScoreValue, _maxScoreValue = 5;

    //Score appliqué lorsque le joueur le touchera
    public int _valueToAdd = 10;

    public void ChooseRandomScore()
    {
        _valueToAdd = Random.Range(_minScoreValue, _maxScoreValue + 1) * 10;
        _currenValue.text = _valueToAdd + "";
    }

    void Update()
    {
        //Movement
        if(GameManager_DarTeat.instance._canPlay)
            transform.position = new Vector3(transform.position.x + _speed * Time.deltaTime, transform.position.y, transform.position.z);
        
        if (transform.position.x < -7 || transform.position.x > 7)
            Destroy(gameObject);
    }

    public void Goal(Color color)
    {
        foreach (var i in _tearsVFX)
        {
            i.SetActive(false);
        }
        _teat.SetActive(true);
        _teat.GetComponent<MeshRenderer>().material.color = color;
        _col.enabled = false;
        _vfxSuccess.SetActive(true);
    }
}
