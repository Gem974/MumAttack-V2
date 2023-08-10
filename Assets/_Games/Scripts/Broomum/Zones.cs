using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zones : MonoBehaviour
{
    public enum PlayerZone
    {
        Player1, Player2
    }

    public PlayerZone _playerZone;
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "ObjInterac")
        {
            if (_playerZone == PlayerZone.Player1)
            {
                Broomum_GameManager.instance.AddPoint(true, 1);
            }
            else if (_playerZone == PlayerZone.Player2)
            {

                Broomum_GameManager.instance.AddPoint(false, 1);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_playerZone == PlayerZone.Player1)
        {
            Broomum_GameManager.instance.AddPoint(true, -1);
        }
        else if (_playerZone == PlayerZone.Player2)
        {

            Broomum_GameManager.instance.AddPoint(false, -1);
        }
    }
}
