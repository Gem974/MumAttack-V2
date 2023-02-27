using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using META;

public class DisplayCharacter : MonoBehaviour
{
    public Character _player1Character;
    public Character _player2Character;

    public static DisplayCharacter instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Il y a plus d'une instance de DisplayCharacter dans la scène");
            return;
        }

        instance = this;
        _player1Character = MetaGameManager.instance._player1;
        _player2Character = MetaGameManager.instance._player2;
    }

}
