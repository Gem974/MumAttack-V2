using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementSpawner : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject _prefGarbage;   

    [Header("Spawn Settings")]
    [SerializeField] private int _maxGarbageToSpawn;
    public int _currentGarbageSpawned;

    [SerializeField] private LayerMask _ObjectDetectedLayerMask;
    [SerializeField] private int _maxSpawnAttempt = 20;

    [Header("Spawn Zone Settings")]
    [SerializeField] private Vector3 _spawnZoneCenter;
    [SerializeField] private Vector3 _spawnZoneSize;
    private Script_GarbageProjecter _GP;

    //[Header("Wave Spawning")] //V2
    private Script_Timer _ST;
    [SerializeField] private int _currentPhase = 1;
    [SerializeField] private int _currentWave = 0;
    [SerializeField] private int _frequencySpawn = 6;
    [SerializeField] private int _waveDensity = 3;
    [SerializeField] private int _maxWaveInPhase = 5;

    private float _currenTime;

    public bool _SpawnWithOutline;
    public Color _whiteColorOutline;

    public GameObject _currentSpawnedGarbage;

    public bool _canStartSpawn = false;

    private void Awake()
    {
        _ST = FindObjectOfType<Script_Timer>();
        _GP = GetComponent<Script_GarbageProjecter>();
        _currenTime = Time.time;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(_spawnZoneCenter, _spawnZoneSize);
    }

    private void Update()
    {
        if (Time.time > _currenTime + _frequencySpawn && _currentPhase < 6 && _canStartSpawn)
        {
            _currenTime = Time.time;
            _currentWave++;

            if (_currentWave > _maxWaveInPhase)
            {
                _currentWave = 0;
                _currentPhase++;

                switch (_currentPhase)
                {
                    case 2:
                        _frequencySpawn = 6;
                        _waveDensity = 4;
                        break;

                    case 3:
                        _frequencySpawn = 6;
                        _waveDensity = 5;
                        break;

                    case 4:
                        _frequencySpawn = 4;
                        _waveDensity = 6;
                        break;

                    case 5:
                        _frequencySpawn = 4;
                        _waveDensity = 25;
                        _maxWaveInPhase = 1;
                        break;
                }
            }

            StartCoroutine(SpawnGarbagesSequence(_waveDensity));

        }

        if (Input.GetKeyDown(KeyCode.Space)) SpawnAGarbage();
    }



    public Vector3 FindSpawnablePosition(float _detectionRadius, bool isTrash, Vector3 TrashPos)
    {
        //Init SpawnPoint
        Vector3 RdmPosInSpawnZone = FindPosInSpawnZone();


        //Init CheckZone around spawnpoint;
        Collider[] hitColliders = Physics.OverlapSphere(RdmPosInSpawnZone, _detectionRadius, _ObjectDetectedLayerMask);

        //Check for a spawn aera (20 attempt max to secure)
        for (int y = 0; y <= _maxSpawnAttempt; y++)
        {
            if (hitColliders.Length != 0)
            {
                if (isTrash && OnlyGarbageHere(hitColliders))
                {
                    if (Vector3.Distance(RdmPosInSpawnZone, TrashPos) >= 3f)
                    {
                        return RdmPosInSpawnZone;
                    }
                }

                RdmPosInSpawnZone = FindPosInSpawnZone();
                hitColliders = Physics.OverlapSphere(RdmPosInSpawnZone, _detectionRadius, _ObjectDetectedLayerMask);
            }
            else return RdmPosInSpawnZone;
        }

        //Nowhere to spawn an element;
        Debug.Log("Nowhere to spawn an element");
        return Vector3.zero;
    }

    public bool OnlyGarbageHere(Collider[] hitcolliders)
    {
        //If there is not onlyGarbage
        foreach (var hitcollider in hitcolliders)
        {
            if (!hitcollider.CompareTag("Garbage"))
            {
                return false;
            }
        }

        //If There is Only Garbage
        foreach (var hitcollider in hitcolliders)
        {
            Destroy(hitcollider.gameObject);
        }

        return true;
    }




    private Vector3 FindPosInSpawnZone()
    {
        //Take a Rdm Pos in Spawn Zone
        return _spawnZoneCenter + new Vector3(Random.Range(-_spawnZoneSize.x / 2, _spawnZoneSize.x / 2),
                                           0, Random.Range(-_spawnZoneSize.z / 2, _spawnZoneSize.z / 2));
    }

    public void SpawnAGarbage()
    {
        if (_currentGarbageSpawned < _maxGarbageToSpawn)
        {
            //Search Spawn Point
            Vector3 SpawnTarget = FindSpawnablePosition(0.5f,false,Vector3.zero);

            //If nowhere to spawn an element, Don't Spawn
            if (SpawnTarget == Vector3.zero) return;

            //Else spawn a garbage
            _currentGarbageSpawned++;

            int IDClosestProj = _GP.ClosestProjecter(SpawnTarget);
            Vector3 closestPos = _GP._projTransforms[IDClosestProj].position;
            _currentSpawnedGarbage = Instantiate(_prefGarbage, closestPos, Quaternion.identity);

            //Set the spawned garbage type
            int GarbageTypeEnumLength = System.Enum.GetValues(typeof(GarbageType)).Length;
            _currentSpawnedGarbage.GetComponent<Garbage>()._garbageType = (GarbageType)Random.Range(0, GarbageTypeEnumLength);





            //playSound SFX_3_ItemSpawn
            SoundManager.instance.StartSound(3, _currentSpawnedGarbage.transform.position, 0.25f);


            //Project the spawned garbage
            _GP.ProjectGarbage(_currentSpawnedGarbage.transform, IDClosestProj, SpawnTarget);
        }
        else Debug.Log("Max garbages spawn reach");
    }

    public IEnumerator SpawnGarbagesSequence(int SpawnNumber)
    {
        for (int i = 0; i < SpawnNumber; i++)
        {
            SpawnAGarbage();           

            yield return new WaitForSeconds(0.15f);


            if (!_SpawnWithOutline)
            {
                _currentSpawnedGarbage.GetComponent<Garbage>()._myOutline.OutlineColor = _currentSpawnedGarbage.GetComponent<Garbage>().GarbageOutlineColor;
            }
            else
            {
                Debug.Log(_currentSpawnedGarbage.GetComponent<Garbage>()._myOutline.OutlineColor);
                _currentSpawnedGarbage.GetComponent<Garbage>()._myOutline.OutlineColor = _whiteColorOutline;
            }
        }
    }

    public void StartSpawn()
    {
        _canStartSpawn = true;
    }

    public void StopSpawn()
    {
        _canStartSpawn = false;
    }
}
