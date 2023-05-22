using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;


public class ScoreText : MonoBehaviour
{
    public TextMeshProUGUI _myTxtMesh;
    public Vector3[] _pathVal;
    public Tween t;
    public Transform cam;


    private void Start()
    {
        cam = Camera.main.transform;
        transform.position = new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z); ;
       
        _pathVal[0] = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        _pathVal[1] = new Vector3(transform.position.x, 3.5f, transform.position.z);
        t = transform.DOPath(_pathVal, 0.5f, PathType.Linear);
        t.SetEase(Ease.OutCubic);

        Vector3 lookat = new Vector3(transform.position.x, cam.position.y + 20, cam.position.z - 20);
        transform.LookAt(lookat);


        Destroy(gameObject, 0.63f);
     

    }


}
