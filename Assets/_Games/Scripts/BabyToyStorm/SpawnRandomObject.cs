using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRandomObject : MonoBehaviour
{
    [SerializeField] GameObject[] _objects;
    [SerializeField] Collider _collider;
    [SerializeField] int _spwanedPlaices;
    [SerializeField] int _maxPlaicesToSpawn = 50;
    [SerializeField] float _zoneDivide;

    public static SpawnRandomObject instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Il y a plus d'une instance de RandomPlaices dans la scène");
            return;
        }

        instance = this;
    }
    void Start()
    {

    }


    public void StartSpawn()
    {
        StartCoroutine(SpawnObjects());
    }

    float RandomWait = 0;
    IEnumerator SpawnObjects()
    {

        while (true)
        {
            if (BabyToyStorm_GameManager.instance._canPlay && _spwanedPlaices <= _maxPlaicesToSpawn)
            {
                yield return new WaitForSeconds(RandomWait);
                RandomWait = Random.Range(1, 5);

                int randomPlaices = Random.Range(0, _objects.Length);

                //float randomZ = Random.Range(-(_collider.transform.localScale.z / 2), (_collider.transform.localScale.z / 2));
                //float randomX = Random.Range(-(_collider.transform.localScale.x / 2), (_collider.transform.localScale.x / 2));
                //Vector3 SpawnPoint = new Vector3(randomX, 0, randomZ);
                //Debug.Log("SpawnPoint: X= " + randomX + " | Y = " + randomZ);

                Vector3 SpawnPoint = RandomPointInBounds(_collider.bounds);



                float randomRot = Random.Range(-1, 1);

                var randomRotation = Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));



                Instantiate(_objects[randomPlaices], SpawnPoint, randomRotation);
                

                yield return new WaitForSeconds(0.5f);

            }
            else
            {
                yield return null;
            }
        }

    }

    public Vector3 RandomPointInBounds(Bounds bounds)
    {
        return new Vector3(
            Random.Range(bounds.min.x / _zoneDivide, bounds.max.x / _zoneDivide),
            bounds.center.y,
            Random.Range(bounds.min.z / _zoneDivide, bounds.max.z / _zoneDivide)
        );
    }

    
}
