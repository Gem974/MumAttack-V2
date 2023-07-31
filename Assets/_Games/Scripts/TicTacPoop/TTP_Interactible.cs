using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TTP_Interactible : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<TTP_Player>()._hasBomb)
            GetComponent<Collider>().isTrigger = false;
    }

    private void OnTriggerExit(Collider other)
    {
        GetComponent<Collider>().isTrigger = false;
    }
}
