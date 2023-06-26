using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using META;

public class DiscountBeforeStart : MonoBehaviour
{
    [SerializeField] GameObject _gameManagerInScene;
    [SerializeField] GameObject _timerGO, _soundStart;
    [SerializeField] Volume _postProcess;
    [SerializeField] DepthOfField _dop;
    [SerializeField] Text _discountTxt;


    public static DiscountBeforeStart instance;



    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Il y a plus d'une instance de GameManager dans la sc�ne");
            return;
        }

        instance = this;
    }

    void Start()
    {
        
    }

    public void StartAfterTuto()
    {
        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            GameManager_IromMum.instance._bubbleJ1.SetActive(true);
            GameManager_IromMum.instance._bubbleJ2.SetActive(true); 
        }
        _soundStart.SetActive(true);
        StartCoroutine(Discount());
        _postProcess.profile.TryGet(out _dop);
    }

    public void RestartDiscount()
    {
        StartCoroutine(Discount());
        PauseGame.instance.CanPause();
        

    }




    IEnumerator Discount()
    {
        PauseGame.instance.CanTPause();
        _discountTxt.gameObject.SetActive(true);
        PresentatorVoice.instance.DiscountPresentator(3);
        _discountTxt.text = "3";
        yield return new WaitForSeconds(1f);
        PresentatorVoice.instance.DiscountPresentator(2);
        _discountTxt.text = "2";
        yield return new WaitForSeconds(1f);
        PresentatorVoice.instance.DiscountPresentator(1);
        _discountTxt.text = "1";
        yield return new WaitForSeconds(1f);
        PresentatorVoice.instance.DiscountPresentator(0);
        _discountTxt.text = "START !";
        yield return new WaitForSeconds(1f);
        _discountTxt.gameObject.SetActive(false);
        PauseGame.instance.CanPause();
        
        //APL DepthOfField
        while (_dop.focusDistance.value <= 10f)
        {
            _dop.focusDistance.value += 1f;
            yield return new WaitForSeconds(0.05f);

        }

        //Lance toute les fonctions de Start
        _gameManagerInScene.SendMessage("StartGameAfterDiscount");



    }
}
