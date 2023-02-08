using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Script_HUD_PlayTransitions : MonoBehaviour
{
    [SerializeField] VideoPlayer[] _allTransitions;
    
    public void ActivateTransition(int _transitionNumber){
        _allTransitions[_transitionNumber].Play();
    }
}
