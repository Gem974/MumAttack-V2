using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerBehaviour : MonoBehaviour
{
    [Header("GameManagerBehaviour")]
    public bool _canPlay;

    public static GameManagerBehaviour instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Il y a plus d'une instance de GameManager dans la sc�ne");
            return;
        }

        instance = this;
    }
}
