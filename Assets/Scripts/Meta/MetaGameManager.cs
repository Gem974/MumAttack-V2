using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetaGameManager : MonoBehaviour
{
    public static MetaGameManager instance;    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Il y a plus d'une instance de MetaGameManager dans la scène");
            Destroy(this.gameObject);
            return;
        }

        instance = this;
    }
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }





}
