using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MumEscape_Toys : MonoBehaviour
{
    public AudioClip[] _sfx;

    public void PlaySFX()
    {
        MumEscape_SoundManager.Instance._sfxAudioSource.PlayOneShot(_sfx[Random.Range(0, _sfx.Length)]);
    }
}
