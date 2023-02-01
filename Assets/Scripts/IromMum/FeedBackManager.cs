using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FeedBackManager : MonoBehaviour
{
    public UnityEvent _propulsion;

    public static FeedBackManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Il y a plus d'une instance de FeedbackManager dans la scène");
            return;
        }

        instance = this;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
