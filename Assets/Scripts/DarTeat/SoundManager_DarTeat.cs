using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager_DarTeat : MonoBehaviour
{
    public AudioClip _shotSoundEffect, _addPointSoundEffect;
    public AudioSource _soundEffectsPlayerController;

    public AudioClip _timeDecrease, _timeDecreaseWithPitch, _timeEnded; //Optional
    //Bruitages quand il ne reste plus que 15 secondes
    public AudioClip _timeDecreaseSpeed, _timeDecreaseWithPitchSpeed; //Optional
    public AudioSource _soundEffectsTimer;

    //When player win
    public AudioClip _winnerSound;
    public AudioSource _soundEffectWinner;

    //Music theme
    public AudioClip _musicTheme;
    public AudioSource _soundEffectTheme;

    //Music theme on finish
    public AudioClip _musicThemeOnFinish;
    public AudioSource _soundEffectThemeOnFinish;

    //Music theme on goldt teat
    public AudioClip _musicThemeOnGoldTeat;
    public AudioSource _soundEffectThemeOnGoldTeat;

    //Intro theme
    public AudioClip _introSound;
    public AudioSource _soundEffectIntro;

    public static SoundManager_DarTeat instance;

    private void Start()
    {
        if (instance != null)
            Debug.Log("Il y a plus d'une instance dans la scène");

        instance = this;

        //Initialization sound
        if (_musicTheme != null)
        {
            _soundEffectTheme.clip = _musicTheme;
            _soundEffectTheme.PlayDelayed(4);
        }

        //Initialization sound
        if (_introSound != null)
        {
            _soundEffectIntro.clip = _introSound;
            _soundEffectIntro.PlayDelayed(.25f);
        }

        //Initialization sound

        if (_musicThemeOnFinish != null)
        {
            _soundEffectThemeOnFinish.clip = _musicThemeOnFinish;
        }

        //Initialization sound
        if (_musicThemeOnGoldTeat != null)
        {
            _soundEffectThemeOnGoldTeat.clip = _musicThemeOnGoldTeat;
        }
    }
}
