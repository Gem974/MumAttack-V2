using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MumEscape_LevelManager : MonoBehaviour
{
    //Variables
    public GameObject[] _levels;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(_levels[Random.Range(0, _levels.Length)], transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
