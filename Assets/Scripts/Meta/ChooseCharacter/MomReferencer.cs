using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MomReferencer : MonoBehaviour
{
    
    public Transform[] _moms;
    public Character[] _datas;

    public static MomReferencer instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Il y a plus d'une instance de MomReferencer dans la scène");
            return;
        }

        instance = this;
    }

    private void Start()
    {
        _moms = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
     {
            _moms[i] = transform.GetChild(i);
        }
    }
}
