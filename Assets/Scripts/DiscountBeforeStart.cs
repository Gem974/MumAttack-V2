using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class DiscountBeforeStart : MonoBehaviour
{
    [SerializeField] Volume _postProcess;
    [SerializeField] DepthOfField _dop;
    [SerializeField] Text _discountTxt;
    void Start()
    {
        StartCoroutine(Discount());
        _postProcess.profile.TryGet(out _dop);
    }
    IEnumerator Discount()
    {

        _discountTxt.gameObject.SetActive(true);
        _discountTxt.text = "3";
        yield return new WaitForSeconds(1f);
        _discountTxt.text = "2";
        yield return new WaitForSeconds(1f);
        _discountTxt.text = "1";
        yield return new WaitForSeconds(1f);
        _discountTxt.text = "START !";
        yield return new WaitForSeconds(1f);
        _discountTxt.gameObject.SetActive(false);
        
        //APL DepthOfField
        while (_dop.focusDistance.value <= 10f)
        {
            _dop.focusDistance.value += 1f;
            yield return new WaitForSeconds(0.05f);

        }

        //Lance toute les fonctions de Start
        GameManager_IromMum.instance.StartGameAfterDiscount();



    }
}
