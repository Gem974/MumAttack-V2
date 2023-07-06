using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pinces : MonoBehaviour
{
    [SerializeField] Animator _animator;
    [SerializeField] Animator _animatorPince;
    [SerializeField] Rigidbody _rb;
    [SerializeField] float _power;
    [SerializeField] GameObject _plaices;


    private void Start()
    {
        int randomSpeed = Random.Range(1, 3);
        _animator.speed = randomSpeed;
    }

    public void StopAnim()
    {
        _animatorPince.enabled = false;
        
    }

    public void FlyAway(float Force, Vector3 ImpactPosition, Vector3 PlayerPos)
    {
        Destroy(_plaices);
        _rb.isKinematic = false;
        _rb.useGravity = true;
        Vector3 ImpactDir = ImpactPosition - PlayerPos;

        ImpactDir = Vector3.Normalize(ImpactDir);

        _rb.AddForce(ImpactDir * _power, ForceMode.Impulse);
        _rb.AddForce(Vector3.up * 30, ForceMode.Impulse);
        _rb.angularVelocity = ImpactDir * 20f;
        Destroy(this.gameObject, 5f);
    }


}
