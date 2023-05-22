using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimCam : MonoBehaviour
{
    public Animator _animCamera;
    // Start is called before the first frame update
    void Start()
    {
        _animCamera = GetComponent<Animator>();  
    }

   
}
