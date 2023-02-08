using UnityEngine;

public class SoundManager : MonoBehaviour
{
    #region Singleton
    public static SoundManager instance;
    private void Awake() {
        instance = this;
    }

    #endregion

    public Transform _transformForTest;
    public GameObject _newSoundTemplate;
    public AudioClip[] _allAudio;
        public AudioClip _mainMusic;
    public AudioSource _mainAudioSource;


    private void Start()
    {
    //    sfxList.happy = _allAudio[(int)sfxList.happy.length];
    }

    public void StartSound(int _idSound, Vector3 _position, float _volume){
        //Instantiation du son
        if(_idSound>_allAudio.Length || _allAudio[_idSound] == null){
            Debug.LogWarning("Ce son n'a pas encore été implémenté ! Son:"+_idSound);
            return;
        }
        _newSoundTemplate.GetComponent<Sound>()._audio = _allAudio[_idSound];
        _newSoundTemplate.GetComponent<AudioSource>().volume = _volume;

        Instantiate(_newSoundTemplate, _position, Quaternion.identity);       
    }
    public void StartMusicGame(){
        _mainAudioSource.clip = _mainMusic;
        _mainAudioSource.Play();
    }



}
