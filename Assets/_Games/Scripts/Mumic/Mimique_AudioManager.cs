using UnityEngine;

public class Mimique_AudioManager : MonoBehaviour
{

    [SerializeField] AudioSource _sfxPlayer1, _sfxPlayer2;
    [SerializeField] AudioClip[] _sfxList; // 0 = buzzer | 1 = Good J1 | 2 = Good J2
    [SerializeField] AudioSource source;


    public void PlaySound(int sfxindex, int player)
    {

        if (player == 1)
        {
            source = _sfxPlayer1;
            Debug.Log("J1 Reconnu");
        }
        else if (player == 2)
        {
            source = _sfxPlayer2;
            Debug.Log("J2 Reconnu");
        }

        if (sfxindex == 0)
        {
            source.clip = _sfxList[0];
            source.Play();
        }
        else if (sfxindex > 0)
        {
            Debug.Log("Play sound ?");
            int aleatoire = Random.Range(1, 3);
            source.clip = _sfxList[aleatoire];
            source.Play();
        }
    }


}
