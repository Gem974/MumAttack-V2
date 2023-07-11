using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPlaices : MonoBehaviour
{

    [SerializeField] GameObject[] _plaices;
    [SerializeField] Collider _collider;
    [SerializeField] int _spwanedPlaices;
    [SerializeField] int _maxPlaicesToSpawn = 50;
    [SerializeField] float _zoneDivide;

    public static RandomPlaices instance;

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


    public void StartRandomPlaices()
    {
        StartCoroutine(SpawnPlaices());
    }

    float RandomWait = 0;
    IEnumerator SpawnPlaices()
    {

        while (true)
        {
            if (GameManager_IromMum.instance._canPlay && _spwanedPlaices <= _maxPlaicesToSpawn)
            {
                yield return new WaitForSeconds(RandomWait);
                RandomWait = Random.Range(1, 5);

                int randomPlaices = Random.Range(0, _plaices.Length);

                //float randomZ = Random.Range(-(_collider.transform.localScale.z / 2), (_collider.transform.localScale.z / 2));
                //float randomX = Random.Range(-(_collider.transform.localScale.x / 2), (_collider.transform.localScale.x / 2));
                //Vector3 SpawnPoint = new Vector3(randomX, 0, randomZ);
                //Debug.Log("SpawnPoint: X= " + randomX + " | Y = " + randomZ);

                Vector3 SpawnPoint = RandomPointInBounds(_collider.bounds);



                float randomRot = Random.Range(-1, 1);

                var randomRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);



                Instantiate(_plaices[randomPlaices], SpawnPoint, randomRotation);
                HowMuchPlaicesToPlace(randomPlaices);

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
            -0.5f,
            Random.Range(bounds.min.z / _zoneDivide, bounds.max.z / _zoneDivide)
        );
    }
    
    public void HowMuchPlaicesToPlace(int randomPlaices)
    {
        switch (randomPlaices)
        {
            case 0:
                _spwanedPlaices += 3;
                break;
            case 1:
                _spwanedPlaices += 7;
                break;
            default:
                break;
        }
    }

    public void RemovePlaces()
    {
        _spwanedPlaices--;
    }


}
