using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject _tshirt;
    public Material[] _tshirtMats;
    public GameObject[] _tshirtDeco;
    public int _tshirtID;
    public Material _plaiceMat;
    public Color[] _plaiceColors;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var i in _tshirtDeco)
        {
            i.SetActive(false);
        }
        _tshirtID = Random.Range(0, _tshirtMats.Length);
        _tshirt.GetComponent<MeshRenderer>().material = _tshirtMats[_tshirtID];
        _tshirtDeco[_tshirtID].SetActive(true);
        _plaiceMat.color = _plaiceColors[_tshirtID];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
