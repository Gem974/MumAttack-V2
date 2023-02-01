using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MumEscape_Meuble : MonoBehaviour
{
    void Update()
    {
        if(MumEscape_GameManager.Instance._light.activeSelf == true)
            GetComponent<MeshRenderer>().enabled = true;
        else
            GetComponent<MeshRenderer>().enabled = false;

    }
}
