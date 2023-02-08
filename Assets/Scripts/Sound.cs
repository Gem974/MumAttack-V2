using UnityEngine;
using System;
using System.Collections;

[Serializable]
public class Sound : MonoBehaviour
{
    public AudioClip _audio;
    AudioSource _audioSource;

    private void Start() {
        //Créer un audio source
        _audioSource =  GetComponent<AudioSource>();
        SetAudioClip();
        StartCoroutine(DestroyGameObject());
    }
    public void SetAudioClip(){
        _audioSource.PlayOneShot(_audio);
    }
    IEnumerator DestroyGameObject(){
        yield return new WaitForSeconds(_audio.length+1);
        Destroy(gameObject);
    }
}
