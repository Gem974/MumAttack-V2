using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabySpawner_DarTeat : MonoBehaviour
{
    //Object � instancier
    public GameObject _prefabBaby;

    //Rapidit� des b�b�s
    public float _babySpeed = 1f;

    //Temps minimum entre deux spawn
    public float _minTimeBetweenSpawnANewBaby = 1f;

    //Temps maximum entre deux spawn
    public float _maxTimeBetweenSpawnANewBaby = 2.5f;

    //Si besoin d'arr�ter compl�tement le spawner (fonctionne individuellement)
    public bool _stopSpawn = false;

    //D�fini le score minimum et maximum de la ligne
    public int _minScoreValue, _maxScoreValue = 5;

    public int _railID;

    //Lance une premi�re fois la coroutine
    private void Start(){ StartCoroutine(SpawnNewBaby()); }

    IEnumerator SpawnNewBaby()
    {

        yield return new WaitForSeconds(1f);

        if (GameManager_DarTeat.instance._canPlay)
        {
            var scale = 0f;
            if(_railID == 1)
            {
                scale = Random.Range(0.9f, 1.3f);

            }
            else if(_railID == 2)
            {
                scale = Random.Range(0.5f, 0.8f);
            }
            //Instancie l'objet souhait�
            GameObject baby = Instantiate(_prefabBaby, transform);
            //baby.transform.localScale = new Vector3(scale, scale, scale);
            BabyBehavior_DarTeat babyBehavior = baby.GetComponent<BabyBehavior_DarTeat>();

            //Change de fa�on dynamique la direction des b�b�s.
            if(transform.position.x > 0)
                babyBehavior._speed = -_babySpeed;
            else
                babyBehavior._speed = _babySpeed;

            //Attribue les valeurs pour le score
            babyBehavior._minScoreValue = _minScoreValue;
            babyBehavior._maxScoreValue = _maxScoreValue;
            babyBehavior.ChooseRandomScore();

            //Temps d'attente al�atoire entre les deux valeurs pr�d�fini en amont
            yield return new WaitForSeconds(Random.Range(_minTimeBetweenSpawnANewBaby, _maxTimeBetweenSpawnANewBaby));
        }

        //Relance la coroutine si le bool _stopSpawn est toujours � false
        if(!_stopSpawn)
            StartCoroutine(SpawnNewBaby());
    }    
}
