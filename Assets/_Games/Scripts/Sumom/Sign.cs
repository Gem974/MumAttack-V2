using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sign : MonoBehaviour
{
    [SerializeField] Transform[] _spawnPoint;
    [SerializeField] Animator _animator;
    public static Sign instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Il y a plus d'une instance de Sign dans la scène");
            return;
        }

        instance = this;
    }
    public void ChangePos()
    {
        int randomPos = Random.Range(0, _spawnPoint.Length);
        transform.position = _spawnPoint[randomPos].position;
        _animator.SetTrigger("Appear");

    }
}
