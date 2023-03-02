using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using META;

public class BroadCastVictory : MonoBehaviour
{
    [SerializeField] Image _winnerImage;
    void Start()
    {
        _winnerImage.sprite = MetaGameManager.instance._winner._winImage;
    }

    
}
