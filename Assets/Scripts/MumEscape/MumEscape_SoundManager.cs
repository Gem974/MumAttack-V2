using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MumEscape_SoundManager : MonoBehaviour
{
    public AudioSource _sfxAudioSource;

    public static MumEscape_SoundManager Instance;

    private void Awake()
    {
        if (Instance != null)
            Debug.Log("MumEscape_SoundManager already exist");

        Instance = this;
    }
}
