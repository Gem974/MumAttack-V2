using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireworks : MonoBehaviour
{
    public GameObject[] _fxs;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FireworksEffect());
    }

    IEnumerator FireworksEffect()
    {
        var id = Random.Range(0, _fxs.Length);
        _fxs[id].SetActive(true);
        yield return new WaitForSeconds(1.5f);
        _fxs[id].SetActive(false);
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(FireworksEffect());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
