using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }
}
