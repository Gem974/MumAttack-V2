using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Garbage : MonoBehaviour
{
    public GarbageType _garbageType;
    public int _playerId;
    public GarbageColor _garbageColor;
    public GarbageCharacteristics[] _caracteristics;


    public Rigidbody _myRB;
    public Vector3[] _pathVal;
    public Tween t;
    public Transform _targetTransform;
    public bool _isThrowed = false;
    public GameObject _vfxGarbageTrail;

    public Outline _myOutline;

    public Color GarbageOutlineColor;
    public Color GarbageOutlineColorYellow;
    public Color GarbageOutlineColorGris;


    private void Start()
    {
        int _garbageInt = (int)_garbageType;
        //Cr�er le gameObject avec le mesh et tout
        GameObject myPref = Instantiate(_caracteristics[_garbageInt]._visual, transform);
        _garbageColor = _caracteristics[_garbageInt]._trashColor;

        _myOutline = myPref.GetComponent<Outline>();

        if (_garbageColor == GarbageColor.Gris)
        {
            GarbageOutlineColor = GarbageOutlineColorGris;
        }
        else
        {
            GarbageOutlineColor = GarbageOutlineColorYellow;
        }

        _isThrowed = false;

        //_spawner = GetComponentInParent<ElementSpawner>();
    }

    private void Update()
    {
        if (_isThrowed)
        {
            _pathVal[0] = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            _pathVal[1] = new Vector3(_targetTransform.position.x, 4, _targetTransform.position.z);
            _pathVal[2] = new Vector3(_targetTransform.position.x, 2, _targetTransform.position.z);
        }
        
    }

    public void GetThrowed(Transform target)
    {
        _isThrowed = true;
        _targetTransform = target;
        _pathVal[0] = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        _pathVal[1] = new Vector3(target.position.x, 4, target.position.z);
        _pathVal[2] = new Vector3(target.position.x, 2, target.position.z);
        t = transform.DOPath(_pathVal, 2, PathType.CatmullRom);
        StartCoroutine(GivePoint(target.GetComponent<Trash>()));

        t.SetEase(Ease.Linear);
    }

    public void SetPlayerID(int _ID)
    {
        _playerId = _ID;
    }

    public IEnumerator GivePoint(Trash trashTarget)
    {
        _vfxGarbageTrail.SetActive(true);
        //Le son du point est dans Trash.
        yield return new WaitForSeconds(2f);
        trashTarget.AddPoint(_playerId, _garbageColor);
        yield return new WaitForFixedUpdate();
        Destroy(gameObject);
    }

    
}
