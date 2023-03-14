using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bubble : MonoBehaviour
{
    [SerializeField] Kart _player;
    [SerializeField] SpriteRenderer _bubble, _head;
    [SerializeField] Sprite[] _bubbles; // 0 = Normal, 1 = Angry
    public UnityEvent _angryEvent;



 
    void Start()
    {
        if (_player._isPlayer1)
        {
            _head.sprite = META.MetaGameManager.instance._player1._goodHead;
        }
        else
        {
            _head.sprite = META.MetaGameManager.instance._player2._goodHead;

        }

        StartDeactivate(3f);
        
        
    }

    private void Update()
    {
        transform.LookAt(Camera.main.transform, Vector3.up);
    }

    public void StartDeactivate(float seconds)
    {
        StartCoroutine(Deactivate(seconds));
    }
    

    public void ShowBubble(int bubbleToShow)
    {
        _bubble.sprite = _bubbles[bubbleToShow];
    }

    public void ShowHead(bool showGoodHead)
    {
        if (showGoodHead)
        {
            if (_player._isPlayer1)
            {
                _head.sprite = META.MetaGameManager.instance._player1._goodHead;
            }
            else
            {
                _head.sprite = META.MetaGameManager.instance._player2._goodHead;

            }
        }
        else
        {
            if (_player._isPlayer1)
            {
                _head.sprite = META.MetaGameManager.instance._player1._badHead;
            }
            else
            {
                _head.sprite = META.MetaGameManager.instance._player2._badHead;

            }
        }
    }

    IEnumerator Deactivate(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        _bubble.gameObject.SetActive(false);
    }
}
