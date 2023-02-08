using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class VFX_TrashCompteur : MonoBehaviour
{

    public ParticleSystem[] _fxCompteur;
    public ParticleSystem _fxTPPoof;

    private void Start()
    {
        SetCompteur(2);
    }
    public void SetCompteur(int garbageNumber)
    {
        foreach (ParticleSystem _fxCompt in _fxCompteur)
        {

            var fx = _fxCompt.main;
            fx.startSize = garbageNumber;

        }

        StartCoroutine(FXADD());

    }

    public IEnumerator FXADD()
    {

        SetFxActive(false);


        yield return new WaitForEndOfFrame();

        SetFxActive(true);


        yield return new WaitForSeconds(0.4f);

        _fxCompteur[2].gameObject.SetActive(false);
        _fxCompteur[3].gameObject.SetActive(false);

    }

    public void SetFxActive(bool state)
    {
        foreach (ParticleSystem _fxCompt in _fxCompteur)
        {
            _fxCompt.gameObject.SetActive(state);
        }

    }

    public void TPPoof()
    {
        _fxTPPoof.Play();
    }




}
