using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashHolder : MonoBehaviour
{
    public Transform _playerTransform;
    
    public float _rotationSpeed;

    private void Update()
    {
        transform.position = new Vector3(_playerTransform.position.x , transform.position.y, _playerTransform.position.z);
        transform.Rotate(0, _rotationSpeed, 0);
    }
}
