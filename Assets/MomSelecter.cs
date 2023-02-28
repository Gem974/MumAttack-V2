using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MomSelecter : MonoBehaviour
{
    public UnityEvent OnDefault, OnHover, OnSelected; 

    public  enum MomSelecterState
    {
        Default,
        Hover,
        Selected
    }

    [SerializeField] MomSelecterState momSelecterState;

    public void OnChangeState(MomSelecterState newStateChange)
    {
        switch (newStateChange)
        {
            case MomSelecterState.Default:
                OnDefault?.Invoke();
                momSelecterState = MomSelecterState.Default;
                break;
            
            case MomSelecterState.Hover:
                OnHover?.Invoke();
                momSelecterState = MomSelecterState.Hover;
                break;
            
            case MomSelecterState.Selected:
                OnSelected?.Invoke();
                momSelecterState = MomSelecterState.Selected;
                break;
            
            default:
                break;
        }
    }
}
