using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdminMacro : MonoBehaviour
{
    [Header ("Outline")]
    public ElementSpawner ES;
    public Color _whiteOutline;
    [SerializeField] private bool _isOutlineActive;

    private void Awake()
    {
        ES._whiteColorOutline = _whiteOutline;
    }

    private void Update()
    {
        //Outline
        if (Input.GetKeyDown(KeyCode.F6))
        {
            GameObject[] OutlineObjects = GameObject.FindGameObjectsWithTag("GarbageChild");

            if (_isOutlineActive)
            {
                _isOutlineActive = false;

                ES._SpawnWithOutline = true;

                foreach (var OutlineObject in OutlineObjects)
                {
                    OutlineObject.GetComponentInChildren<Outline>().OutlineColor = _whiteOutline;
                }
            }
            else
            {
                _isOutlineActive = true;

                ES._SpawnWithOutline = false;

                foreach (var OutlineObject in OutlineObjects)
                {
                    if (OutlineObject.TryGetComponent(out Outline outlineobj))
                    {
                        outlineobj.OutlineColor = OutlineObject.GetComponentInParent<Garbage>().GarbageOutlineColor;
                    }
                   // OutlineObject.GetComponent<Outline>().OutlineColor = OutlineObject.GetComponent<Garbage>().GarbageOutlineColor;

                }
            }
        }
    }



}
