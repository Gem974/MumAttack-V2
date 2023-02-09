using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPlaices : MonoBehaviour
{
    [SerializeField] Transform _ground;
    [SerializeField] GameObject[] _plaices;
    [SerializeField] Collider _collider;
    [SerializeField] int _spwanedPlaices;
    [SerializeField] int _maxPlaicesToSpawn = 50;

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
   
    IEnumerator SpawnPlaices()
    {

        while (true)
        {
            if (GameManager_IromMum.instance._canPlay && _spwanedPlaices <= _maxPlaicesToSpawn)
            {
                int RandomWait = Random.Range(1, 5);
                yield return new WaitForSeconds(RandomWait);

                int randomPlaices = Random.Range(0, _plaices.Length);

                //float randomZ = Random.Range(-(_ground.transform.localScale.z / 2), (_ground.transform.localScale.z / 2));
                //float randomX = Random.Range(-(_ground.transform.localScale.x / 2), (_ground.transform.localScale.x / 2));
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
            Random.Range(bounds.min.x / 1.8f, bounds.max.x / 1.8f),
            0.5f,
            Random.Range(bounds.min.z / 1.8f, bounds.max.z / 1.8f)
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
