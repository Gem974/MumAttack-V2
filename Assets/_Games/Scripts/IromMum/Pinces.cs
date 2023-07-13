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
    public GameObject _impactVFX;
    public MeshRenderer[] _mat;

    private void Start()
    {
        var col = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1f);
        foreach (var i in _mat)
        {
            i.material.color = col;
        }
        int randomSpeed = Random.Range(1, 3);
        _animator.speed = randomSpeed;
    }

    public void StopAnim()
    {
        _animatorPince.enabled = false;
        
    }

    public void FlyAway(float Force, Vector3 ImpactPosition, Vector3 PlayerPos)
    {
        Instantiate(_impactVFX, transform.position, Quaternion.identity);
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
