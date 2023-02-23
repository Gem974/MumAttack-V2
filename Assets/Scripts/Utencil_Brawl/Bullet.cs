using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;


public class Bullet : MonoBehaviour
{

    public float _bulletSpeed = 15f;
    public float _bulletAngle = 5f;
    public float _destroyValue = 0.7f;
    public Rigidbody _Bulletbody;

    public Transform _Utencil;
   // public Player _Player;
    public PathType pathsysteme = PathType.CatmullRom;
    public Vector3[] pathLeft = new Vector3[3];
    public Vector3[] pathRight = new Vector3[3];
    public Transform _PathPoints, _PathPoints2, _PathPoints3, _PathPoints4, _PathPoints5;

    public bool _IsStraight = false;
    public bool _IsArcLeft = false;
    public bool _IsArcRight = false;



    // Start is called before the first frame update
    void Start()
    {


        //LeftArc
        pathLeft[0] = _PathPoints.position;
        pathLeft[1] = _PathPoints3.position;
        pathLeft[2] = _PathPoints5.position;


        float distLeft = Vector3.Distance(pathLeft[0], pathLeft[1]) + Vector3.Distance(pathLeft[1], pathLeft[2]);
        float durationLeft = distLeft / _bulletSpeed;



        //RightArc
        pathRight[0] = _PathPoints.position;
        pathRight[1] = _PathPoints2.position;
        pathRight[2] = _PathPoints4.position;


        float distRight = Vector3.Distance(pathRight[0], pathRight[1]) + Vector3.Distance(pathRight[1], pathRight[2]);
        float durationRight = distRight / _bulletSpeed;



        //_Utencil.transform.DOPath(pathval, 3, pathsysteme);


        if (_IsArcLeft == true )
        {

            //Debug.Log("ArcLeftShoot");
            transform.DOPath(pathLeft, durationLeft , pathsysteme);

            Destroy(transform.gameObject, _destroyValue);

            
         

        }
        else if (_IsArcRight == true )
        {

            //Debug.Log("ArcRightShoot");
            transform.DOPath(pathRight, durationRight, pathsysteme);

           

            Destroy(transform.gameObject, _destroyValue);

           

        }

    }

    // Update is called once per frame
    void Update()
    {

        if (_IsStraight == true)
        {
            //Debug.Log("StraightShoot");
            _Utencil.transform.Translate(Vector3.forward * _bulletSpeed * Time.deltaTime);

            Destroy(transform.gameObject, _destroyValue);


        }



    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Destroyed");
        Destroy(transform.gameObject);
    }


    private void OnDestroy()
    {
        //Debug.Log("destroyed");

        DOTween.Kill(_Utencil);
    }

    
}
