using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MumEscape_SpawnPlayer : MonoBehaviour
{
    //Variables
    public GameObject _player;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(_player, transform.position, Quaternion.identity); 
    }
}
