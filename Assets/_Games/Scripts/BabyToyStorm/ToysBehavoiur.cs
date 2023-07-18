using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToysBehavoiur : MonoBehaviour
{

    [SerializeField] int _toyPointValue;
    [SerializeField] Rigidbody _rb;

    private void Start()
    {
        _rb.angularVelocity = new Vector3(Random.Range(0, 40), Random.Range(0, 40), Random.Range(0, 40));
    }
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

        if (collision.gameObject.tag != "J1" || collision.gameObject.tag != "J2")
        {
            this.gameObject.GetComponent<Collider>().enabled = false;
            Destroy(this.gameObject, 1.5f);
        }
    }
}
