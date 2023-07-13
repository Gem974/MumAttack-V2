using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PresentatorVoice : MonoBehaviour
{
    public bool _canTalk;
    [SerializeField] AudioClip[] _goodThingsClips;
    [SerializeField] AudioClip[] _badThingsClips;
    [SerializeField] AudioClip[] _discounts;
    [SerializeField] AudioSource _audioSource;
    public GameObject[] _prompts;
    public Sprite[] _sprites; // 0 : Stress / 1 : Sad / 2 : happy / 3 : Angry 

    public static PresentatorVoice instance;

    private void Awake()
    {
        _canTalk = true;

        if (instance != null)
        {
            Debug.LogError("Il y a plus d'une instance de SpamBehaviour dans la sc�ne");
            return;
        }

        instance = this;
    }

    public void ShowPrompt(float duration, bool isGood)
    {
        foreach (var i in _prompts)
        {
            i.SetActive(false);
        }
        StartCoroutine(Prompt(duration, isGood));
    }

    IEnumerator Prompt(float duration, bool isGood)
    {
        var id = Random.Range(0, _prompts.Length);
        if (isGood)
            _prompts[id].GetComponent<Image>().sprite = _sprites[2];
        else
            _prompts[id].GetComponent<Image>().sprite = _sprites[3];
        _prompts[id].SetActive(true);
        yield return new WaitForSeconds(duration);
        _prompts[id].SetActive(false);
    }

    public void StartSpeaking(bool isOneShot, bool _isGoodThings)
    {
        if (_canTalk)
        {
            StartCoroutine(Speak(isOneShot, _isGoodThings));
            ShowPrompt(0.5f, _isGoodThings);
            _canTalk = false;
        }
    }

    public void DiscountPresentator(int whatNumber)
    {
        _audioSource.PlayOneShot(_discounts[whatNumber]);
    }


    IEnumerator Speak(bool isOneShot, bool _isGoodThings)
    {
        if (isOneShot) //If it's a song play in OneShot
        {
            if (_isGoodThings) //If it's a GOOD things that the presentator says
            {
                int randomClip = Random.Range(0, _goodThingsClips.Length);
                _audioSource.PlayOneShot(_goodThingsClips[randomClip]);
                yield return new WaitForSeconds(_goodThingsClips[randomClip].length);
                _canTalk = true;

            }
            else //If it's a BAD things that the presentator says
            {
                int randomClip = Random.Range(0, _badThingsClips.Length);
                _audioSource.PlayOneShot(_badThingsClips[randomClip]);
                yield return new WaitForSeconds(_badThingsClips[randomClip].length);
                _canTalk = true;
            }
        }
        else //If it's NOT a song play in OneShot
        {
            if (_isGoodThings) //If it's a GOOD things that the presentator says
            {
                int randomClip = Random.Range(0, _goodThingsClips.Length);
                _audioSource.clip = _goodThingsClips[randomClip];
                _audioSource.Play();
                yield return new WaitForSeconds(_goodThingsClips[randomClip].length);
                _canTalk = true;
            }
            else //If it's a BAD things that the presentator says
            {
                int randomClip = Random.Range(0, _badThingsClips.Length);
                _audioSource.clip = _badThingsClips[randomClip];
                _audioSource.Play();
                yield return new WaitForSeconds(_badThingsClips[randomClip].length);
                _canTalk = true;
            }
        }
    }


}
