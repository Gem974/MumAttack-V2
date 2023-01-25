using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetaGameManager : MonoBehaviour
{
    public static MetaGameManager instance;

    public enum moms
    {
        Rachel, Aurelie
    }

    [Header("Set Characters")]
    public moms _moms;
    

    [Header("LeaderBoard")]
    public int[] _topScorePoints;
    public string[] _topScoreName;

    [Header("Sounds")]
    public float _volume;

    
    

    private void Awake()
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
