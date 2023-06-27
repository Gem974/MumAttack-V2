using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sign : MonoBehaviour
{
    [Header("System")]
    [SerializeField] Transform[] _spawnPoint; // Ou va apparaitre le panneau
    public KeyCode _pK1, _pK2, _pG1, _pG2; // pK = Keyboard & pG Gamepad
    [SerializeField] GameObject[] _arrows; // Bouton qui s'affiche
    [SerializeField] Animator _animator;
    public static Sign instance;
    public enum ButtonToSpam
    {
        South, East, North, West
    }

    public ButtonToSpam _buttonToSpam;


    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Il y a plus d'une instance de Sign dans la scène");
            return;
        }

        instance = this;
    }
    public void ChangePos() // Change la position du panneau
    {
        int randomPos = Random.Range(0, _spawnPoint.Length);
        transform.position = _spawnPoint[randomPos].position;
        _animator.SetTrigger("Appear");
    }

    public void StartRandomKeys() // Lance le choix aléatoire des flèches à spam.
    {
        StartCoroutine(RandomKeys());
    }


    public void SetRandomKeys() //Fixe la flèche à spam
    {
        ResetVelocity();


        int randomKey = Random.Range(0, 4);

        switch (randomKey)
        {

            case 0:
                ArrowsOff(); //Up
                _pK1 = KeyCode.Z;
                _pK2 = KeyCode.UpArrow;
                _pG1 = KeyCode.Joystick1Button3;
                _pG2 = KeyCode.Joystick2Button3;
                _arrows[1].SetActive(true);
                _buttonToSpam = ButtonToSpam.North;
                ChangePos();


                break;
            case 1:
                ArrowsOff(); //Right
                _pK1 = KeyCode.D;
                _pK2 = KeyCode.RightArrow;
                _pG1 = KeyCode.Joystick1Button1;
                _pG2 = KeyCode.Joystick2Button1;
                _arrows[2].SetActive(true);
                _buttonToSpam = ButtonToSpam.East;
                ChangePos();

                break;
            case 2:
                ArrowsOff(); //Left
                _pK1 = KeyCode.Q;
                _pK2 = KeyCode.LeftArrow;
                _pG1 = KeyCode.Joystick1Button2;
                _pG2 = KeyCode.Joystick2Button2;
                _arrows[3].SetActive(true);
                _buttonToSpam = ButtonToSpam.West;
                ChangePos();

                break;
            case 3:
                ArrowsOff(); //Down
                _pK1 = KeyCode.S;
                _pK2 = KeyCode.DownArrow;
                _pG1 = KeyCode.Joystick1Button0;
                _pG2 = KeyCode.Joystick2Button0;
                _arrows[0].SetActive(true);
                _buttonToSpam = ButtonToSpam.South;
                ChangePos();

                break;

        }
    }

    void ArrowsOff() //Cache toute les flèches dans la scène.
    {
        foreach (GameObject arrow in _arrows)
        {
            arrow.SetActive(false);
        }
    }

    IEnumerator RandomKeys() //Relanche le choix des flèches aléatoire toute les 2s à 4s.
    {
        while (true)
        {
            float randomTime = Random.Range(2f, 4f);
            yield return new WaitForSeconds(randomTime);
            SetRandomKeys();
        }
    }

    public void ResetVelocity()
    {
        if (Sumom_GameManager.instance._playerDominant == Sumom_GameManager.PlayerDominant.P1) //Reset Force avec J1 > J2
        {
            if (Sumom_GameManager.instance._rb1.velocity != Vector3.zero) // TEST SI LE JOUEUR 1 EST EN TRAIN DE SE DEPLACER
            {
                Sumom_GameManager.instance._rb1.velocity = new Vector3(2f, 0f, 0f); 
            }
            if (Sumom_GameManager.instance._rb2.velocity != Vector3.zero)
            {
                Sumom_GameManager.instance._rb2.velocity = new Vector3(-1f, 0f, 0f); 
            }
        }
        else if (Sumom_GameManager.instance._playerDominant == Sumom_GameManager.PlayerDominant.P2) //Reset Force avec J2 > J1
        {
            if (Sumom_GameManager.instance._rb1.velocity != Vector3.zero)
            {
                Sumom_GameManager.instance._rb1.velocity = new Vector3(1f, 0f, 0f); 
            }
            Sumom_GameManager.instance._rb2.velocity = new Vector3(-2f, 0f, 0f);
        }
        else if (Sumom_GameManager.instance._playerDominant == Sumom_GameManager.PlayerDominant.None) //Reset Force avec J1 = J2
        {
            Sumom_GameManager.instance._rb1.velocity = new Vector3(0f, 0f, 0f);
            Sumom_GameManager.instance._rb2.velocity = new Vector3(0f, 0f, 0f);
        }
    }

    
}
