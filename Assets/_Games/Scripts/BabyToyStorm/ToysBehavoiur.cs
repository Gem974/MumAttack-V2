using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToysBehavoiur : MonoBehaviour
{

    [SerializeField] int _toyPointValue;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "J1")
        {
            BabyToyStorm_GameManager.instance.AddPoint(true, _toyPointValue);
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.tag == "J2")
        {
            BabyToyStorm_GameManager.instance.AddPoint(false, _toyPointValue);
            Destroy(this.gameObject);
        }
    }
}
