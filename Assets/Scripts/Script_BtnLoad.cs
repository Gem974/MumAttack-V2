using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Script_BtnLoad : MonoBehaviour
{
    public Image _btnContour;
    InputReceiver _IR;
    Script_GameManager _GM;
    [SerializeField] float coolDownPeriodInSeconds=2;
    [SerializeField] float _chargeSpeed = 1f;
    [SerializeField] int _sceneToLoad = 1;
    [SerializeField] bool isSceneLoad = false;


    private void Start() {
        _IR = FindObjectOfType<InputReceiver>();
        _GM = FindObjectOfType<Script_GameManager>();


    }
    private void Update() {
        //Le bouton
        if(Input.GetKeyDown(KeyCode.P)){
            _GM.LoadScene(_sceneToLoad);
        }

        if(_IR._inputsP1.x > 0.8f){
            //ça augmente la velur du slide
            _btnContour.fillAmount += _chargeSpeed * Time.deltaTime;
        }else{
            //baisse la velur du slide
            _btnContour.fillAmount -= _chargeSpeed * Time.deltaTime;
        }

        if (_btnContour.fillAmount == 1 && !isSceneLoad)
        {
            isSceneLoad = true;
            _GM.LoadScene(_sceneToLoad);
        }
    }

}
