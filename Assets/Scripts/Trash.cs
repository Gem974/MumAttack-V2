using UnityEngine;
using Cinemachine;
using System.Collections;
public class Trash : MonoBehaviour
{

    [Header("Trash Settings")]
    public GarbageColor _trashColor;
    public int _currentGarbagesNbrInTrash = 0;
    [SerializeField] private int _targetGroupTrashID;
    [SerializeField] private int _spawnRadius = 3;
    [SerializeField] private TrashCaracteristics[] _caracteristics;

    //AutoComplete
    private VFX_TrashCompteur _FXTrashC;
    public Animator _trashAnimator;
    private SoundManager _SoundM;
    private Script_ScoreManager _ScoreM;
    private ElementSpawner _ES;
    private CinemachineTargetGroup _CTG;
    private SphereCollider _sphereCollider;
    private int _playerCompter;
    public float _timeLeftBeforeVortex;
    public bool _canTP;

    public bool resetCompteur;

    private void Update()
    {


    }


    private void OnDrawGizmosSelected() {
        Gizmos.color=Color.blue;
        Gizmos.DrawWireSphere(transform.position,_spawnRadius);
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerCompter++;

            if (_playerCompter > 2)
            {
                _playerCompter = 2 ;
            }

            _trashAnimator.SetTrigger("OPEN");
            _trashAnimator.SetBool("CLOSE", false);

        }

        //if (other == null)
        //{
        //    print(gameObject.name);
        //    _trashAnimator.SetBool("CLOSE",true);

        //}
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerCompter--;

            if (_playerCompter < 0)
            {
                _playerCompter = 0;
            }

            if (_playerCompter == 0)
            {
                print(gameObject.name);
                _trashAnimator.SetBool("CLOSE", true);
                _trashAnimator.SetTrigger("CLOSE");

            }

        }
    }



    private void Start() 
    {
         _SoundM = SoundManager.instance;
        _ScoreM = FindObjectOfType<Script_ScoreManager>();
        _ES = FindObjectOfType<ElementSpawner>();
        _CTG = FindObjectOfType<CinemachineTargetGroup>();

        _sphereCollider = GetComponent<SphereCollider>();

        //Trash Spawn
        Instantiate(_caracteristics[(int)_trashColor]._visual, transform);

        _trashAnimator = GetComponentInChildren<Animator>();
        _FXTrashC = GetComponentInChildren<VFX_TrashCompteur>();
    }

    private void LateUpdate()
    {
        if (resetCompteur)
        {
            resetCompteur = false;
            _timeLeftBeforeVortex = 1.25f;
            _canTP = false;
        }

        //2-D    
        _timeLeftBeforeVortex = _timeLeftBeforeVortex > 0 ? _timeLeftBeforeVortex -= Time.deltaTime : 0;
        _canTP = _timeLeftBeforeVortex <= 0 ? true : false;

        if (_currentGarbagesNbrInTrash >= 5 && !_canTP)
        {
            //_trashAnimator.SetTrigger("VORTEX");
            //_trashAnimator.SetBool("VORTEXSTARTED", true);
            _CTG.m_Targets[_targetGroupTrashID].weight = 1;

            _currentGarbagesNbrInTrash = 5;
            _FXTrashC.SetCompteur(_currentGarbagesNbrInTrash + 2);

          
            //Debug.Log("Vortex");
        }
        else if (_currentGarbagesNbrInTrash >= 5 && _canTP)
        {
            //Reset
            _currentGarbagesNbrInTrash = 0;
            _FXTrashC.SetCompteur(_currentGarbagesNbrInTrash + 2);

          //  Debug.Log("Start TP");

            StartCoroutine(TrashTPSequence());         
        }



    }

    private void TrashTP()
    {
        Vector3 _newPosition = _ES.FindSpawnablePosition(_spawnRadius,true,transform.position);

        if (_newPosition == Vector3.zero)
        {
            //Can't TP
            Debug.LogWarning("On peut pas tp la poubelle, oups.");
        }
        else
        {
            //Can TP
            //PlaySound SFX_12_TrashTP
            _SoundM.StartSound(12, transform.position,1f);
            _playerCompter = 0;
            transform.position = _newPosition;
        }
    }

    public void AddPoint(int idPlayer, GarbageColor garbageThrowed)
    {            
        FillTrash();
        _ES._currentGarbageSpawned--;

        //_timeLeftBeforeVortex = 2;


        if (garbageThrowed != _trashColor)
        {
            //wrong trash;
            _ScoreM.AddPoint(idPlayer, transform.position, false);
            return;
        }


        _ScoreM.AddPoint(idPlayer, transform.position, true);


    }

    public void FillTrash()
    {
        _currentGarbagesNbrInTrash++;
        resetCompteur = true;

        /*  if (_currentGarbagesNbrInTrash < 5)
          {
              _trashAnimator.SetTrigger("SHAKE");
          }
          else
          {
              _trashAnimator.SetTrigger("VORTEX");
          }*/

        if (_currentGarbagesNbrInTrash >= 4)
        {
            _trashAnimator.SetTrigger("VORTEX");
            _trashAnimator.SetBool("VORTEXSTARTED", true);

        }
        else
        {
            _trashAnimator.SetTrigger("SHAKE");
        }

        if (_currentGarbagesNbrInTrash > 5) _currentGarbagesNbrInTrash = 5;
        _FXTrashC.SetCompteur(_currentGarbagesNbrInTrash + 2);
    }

    private IEnumerator TrashTPSequence()
    {
        _sphereCollider.enabled = false;
        _trashAnimator.SetTrigger("TPSTART");


        yield return new WaitForSeconds(1f);

        TrashTP();

        yield return new WaitForSeconds(0.3f);

        _trashAnimator.SetTrigger("TPEND");

        yield return new WaitForSeconds(1);

        _trashAnimator.SetBool("VORTEXSTARTED", false);

        _FXTrashC.TPPoof();
        _sphereCollider.enabled = true;

        yield return new WaitForSeconds(0.5f);

        _CTG.m_Targets[_targetGroupTrashID].weight = 0;
    }
}
