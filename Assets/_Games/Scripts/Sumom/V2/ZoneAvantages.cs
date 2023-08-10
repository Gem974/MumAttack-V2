using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneAvantages : MonoBehaviour
{
    //[SerializeField] bool _isZoneP1;
    public int _playerIn = 0;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _playerIn++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _playerIn--;
        }
    }
}
