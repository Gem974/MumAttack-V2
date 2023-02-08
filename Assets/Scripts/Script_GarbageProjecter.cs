using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Script_GarbageProjecter : MonoBehaviour
{
    public Transform[] _projTransforms;
    public Tween t;

    public Vector3[] _pathVal;


    public int ClosestProjecter(Vector3 Trashposition)
    {
        float[] CloserProjecters = new float[_projTransforms.Length];
        int IDClosestProjecteur = 0;
        float currentClosestDistance = 100;

        for (int i = 0; i < _projTransforms.Length; i++)
        {

            CloserProjecters[i] = Vector3.Distance(_projTransforms[i].position, Trashposition);

            if (currentClosestDistance > CloserProjecters[i])
            {
                IDClosestProjecteur = i;
                currentClosestDistance = Vector3.Distance(_projTransforms[i].position, Trashposition);
            }
        }

        return IDClosestProjecteur;
    }


    public void ProjectGarbage(Transform GarbageTransform, int IDProjecter, Vector3 target)
    {

        Vector3 startPos = _projTransforms[IDProjecter].position;
        Vector3 endPos = target;

        StartCoroutine(SpawnSequence(GarbageTransform, startPos, endPos));
      
    }

   // IEnumerator Wait
    private IEnumerator SpawnSequence(Transform GarbageToSpawn, Vector3 start, Vector3 end)
    {

        GarbageToSpawn.GetComponent<BoxCollider>().enabled = false;
        GarbageToSpawn.GetComponent<SphereCollider>().enabled = false;
        GarbageToSpawn.GetComponent<Rigidbody>().isKinematic = true;
        GarbageToSpawn.GetComponent<Rigidbody>().useGravity = false;
        GarbageToSpawn.GetComponent<Garbage>()._vfxGarbageTrail.SetActive(true);


        yield return new WaitForEndOfFrame();

        _pathVal[0] = start;
        _pathVal[1] = start + ((end-start)/2) + Vector3.up*3;
        _pathVal[2] = new Vector3(end.x, 0, end.z);
        t = GarbageToSpawn.DOPath(_pathVal, 2, PathType.CatmullRom,PathMode.Full3D, 10, Color.blue);

        t.SetEase(Ease.OutQuad);

        yield return new WaitForSeconds(2);

        GarbageToSpawn.GetComponent<BoxCollider>().enabled = true;
        GarbageToSpawn.GetComponent<SphereCollider>().enabled = true;
        GarbageToSpawn.GetComponent<Rigidbody>().isKinematic = false;
        GarbageToSpawn.GetComponent<Rigidbody>().useGravity = true;
        GarbageToSpawn.GetComponent<Garbage>()._vfxGarbageTrail.SetActive(false);

    }
}
