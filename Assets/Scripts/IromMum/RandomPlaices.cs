using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPlaices : MonoBehaviour
{
    [SerializeField] Transform _ground;
    [SerializeField] GameObject[] _plaices;
    [SerializeField] Collider _collider;
    void Start()
    {
        if (GameManager_IromMum.instance._canPlay)
        {
            StartCoroutine(SpawnPlaices()); 
        }
    }

    // Update is called once per frame
    IEnumerator SpawnPlaices()
    {

        while (true)
        {
            if (GameManager_IromMum.instance._canPlay)
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
            Random.Range(bounds.min.x, bounds.max.x),
            0.5f,
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }


}
