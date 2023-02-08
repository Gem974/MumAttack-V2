using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GarbageColor { Gris, Jaune, Vert };


public class GarbageManager : MonoBehaviour
{
    public int _currentSlotNumber = 0;
    public Transform[] _slotHolders;

    public GarbageColor _playerCarbageColor;
    public bool _isNearTrash = false;
    public bool _canThrowAgain = true;
    public float _coolDown = 0.5f;

    public int _playerID;
    //Script_PlayerControl _PC;
    public Transform _currentObjectToThrow;    
    SoundManager _SoundM;

    Script_HUDManager _HUDM;

    public SlotShowerManager _SSM;

    private void Start()
    {
        _HUDM = Script_HUDManager.instance;
        _SoundM = SoundManager.instance;
   //     _PC.GetComponent<Script_PlayerControl>();

    }


    private void OnTriggerEnter(Collider other)
    {
        GameObject _currentObjectDetected = other.gameObject;

        if (CanPickUp(_currentObjectDetected))
        {
            PickUp(_currentObjectDetected);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        GameObject _TrashDetected = other.gameObject;

        if (canThrowToTrash(_TrashDetected))
        {
            ThowToTrash(_TrashDetected);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.transform.CompareTag("Terrain") && !collision.transform.CompareTag("Garbage"))
        {
            //PlaySound SFX_02_BallHit
            SoundManager.instance.StartSound(2, transform.position, 0.25f);
        }
    }

    public void PickUp(GameObject otherObject)
    {
        //UpdateUI
        _SSM.AddSlotShower();

        //PlaySound SFX_10_PickUp
        _SoundM.StartSound(10,transform.position,1);

        //Stop Collide and Add to holder, increment slot
        otherObject.GetComponent<BoxCollider>().enabled = false;
        otherObject.GetComponent<SphereCollider>().enabled = false;
        otherObject.GetComponent<Rigidbody>().isKinematic = true;
        otherObject.GetComponent<Rigidbody>().useGravity = false;
        otherObject.GetComponentInChildren<Outline>().enabled = false;
        otherObject.GetComponentInChildren<MeshRenderer>().enabled = false;

        otherObject.transform.SetParent(_slotHolders[_currentSlotNumber]);
        //Debug.Log("PickUp");

        _currentObjectToThrow = otherObject.transform;
        _currentObjectToThrow.transform.localPosition = Vector3.zero;
        _currentObjectToThrow.GetComponent<Garbage>().SetPlayerID(_playerID);
        GarbageType _garbageType =  _currentObjectToThrow.GetComponent<Garbage>()._garbageType;
        _HUDM.FillGarbage(_playerID,_garbageType);


        _currentSlotNumber += 1;


        ShowCurrentObject();


    }

    public bool CanPickUp(GameObject otherObject)
    {

        if (!otherObject.CompareTag("Garbage"))
        {
            return false;
        }

        //IsSlotEmpty : ChangePlayerGarbageColor
        if (_currentSlotNumber == 0) 
        {
            SetPlayerTrashColor(otherObject);

            return true;
        }

        //Check if there is a slot empty left
        if (_currentSlotNumber < _slotHolders.Length) 
        {
        }
        else //SlotIsFull
        {
            return false;
        }

        return true;
    }

    public void SetPlayerTrashColor(GameObject otherObject)
    {
        _playerCarbageColor = otherObject.GetComponent<Garbage>()._garbageColor;
    }

    public bool canThrowToTrash(GameObject TrashObject)
    {
        //Check IF its a trash
        if (!TrashObject.CompareTag("Trash"))
        {
            return false;
        }
        _isNearTrash = true;

        //Check IF Player have a garbage left
        if (_currentSlotNumber < 1)
        {
            return false;
        }


        if (!_canThrowAgain)
        {
            return false;
        }

        return true;
    }

    private void OnTriggerExit(Collider other)
    {
        //Check IF its a trash
        if (other.CompareTag("Trash"))
        {
            StopAllCoroutines();
            _isNearTrash = false;
            StartCoroutine(ThrowingToTrash());
        }
    }

    public void ThowToTrash(GameObject TrashDetected)
    {
        //UpdateUI
        _SSM.SubSlotShower();


        //PlaySound SFX_11_ThrowToTrash
        _SoundM.StartSound(11,transform.position,1);

        TrashDetected.GetComponent<Trash>().resetCompteur = true;


        //DotThrowToTrash        
        _currentObjectToThrow = _slotHolders[_currentSlotNumber - 1].GetChild(0);
        _currentObjectToThrow.GetComponentInChildren<MeshRenderer>().enabled = true;
        _currentObjectToThrow.SetParent(TrashDetected.transform);
        _currentObjectToThrow.GetComponent<Garbage>().GetThrowed(TrashDetected.transform);

   //   TrashDetected.GetComponent<Trash>()._noObjectSinceTimeDroped = false;

        StartCoroutine(ThrowingToTrash());

        _currentSlotNumber -= 1;
        
        _HUDM.RemoveGarbage(_playerID);

        if (_currentSlotNumber > 0)
        {
            ShowCurrentObject();
        }


        //PutCurrentObjectOnHead(_currentObjectToThrow.GetComponent<GarbageCharacteristics>());
    }

    public IEnumerator ThrowingToTrash()
    {
        _canThrowAgain = false;
        yield return new WaitForSeconds(_coolDown);
        _canThrowAgain = true;
    }

    public void ShowCurrentObject()
    {
        for (int i = 0; i < _currentSlotNumber - 1; i++)
        {
            _slotHolders[i].GetComponentInChildren<MeshRenderer>().enabled = false;
        }

        _slotHolders[_currentSlotNumber - 1].GetComponentInChildren<MeshRenderer>().enabled = true;

    }

}
