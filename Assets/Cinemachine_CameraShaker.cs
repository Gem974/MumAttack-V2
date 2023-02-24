using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System.Threading.Tasks;

public class Cinemachine_CameraShaker : MonoBehaviour
{
    [SerializeField] CinemachineBasicMultiChannelPerlin _noise;

    [SerializeField] float _amplitude;
    [SerializeField] int _timeMiliseconds;



    private void Awake()
    {
        _noise = this.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }


    public async void doCamShake()
    {
        _noise.m_AmplitudeGain = _amplitude;
        await Task.Delay(_timeMiliseconds);
        _noise.m_AmplitudeGain = 0f;

    }
}
