using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Save : MonoBehaviour
{
    public Slider _sliderRef;
    public Slider _sliderInGame;




    void Start()
    {
        
        

        if (_sliderRef)
        {
            _sliderRef.value = 1f;
            PlayerPrefs.SetFloat("listenerValue", _sliderRef.value);
        }
        else
        {
            _sliderInGame.value = PlayerPrefs.GetFloat("listenerValue");
        }

        Debug.Log("Volume: " + PlayerPrefs.GetFloat("listenerValue"));


    }

}
