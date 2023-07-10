using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSelectionScroll : MonoBehaviour
{
    //Variables
    public ScrollRect _scrollRect;
    public float _offset;
    public float _speed;
    public List<FreeBrawlButtonData> _games = new List<FreeBrawlButtonData>();
    public List<FreeBrawlButtonData> _ptrGames = new List<FreeBrawlButtonData>();

    float _leftPos;
    float _rightPos;
    RectTransform _content;
    RectTransform _viewPort;
    Coroutine _coroutine;

    // Start is called before the first frame update
    void Start()
    {
        _content = _scrollRect.content;
        _viewPort = _scrollRect.viewport;

        foreach (var i in _content.GetComponentsInChildren<FreeBrawlButtonData>())
        {
            if(i._isPTRGame == false)
                _games.Add(i);
            else
            {
                _ptrGames.Add(i);
                i.gameObject.SetActive(false);
            }
        }
    }
    private bool _ptrActive = false;
    public void ActivePTRGames()
    {
        if(_ptrActive == false)
        {
            foreach (var i in _ptrGames)
            {
                i.gameObject.SetActive(true);
                
            }
            Navigation nav1 = _games[0].GetComponent<Button>().navigation;
            nav1.selectOnLeft = _ptrGames[_ptrGames.Count - 1].GetComponent<Selectable>();
            _games[0].GetComponent<Button>().navigation = nav1;
            Navigation nav2 = _games[_games.Count - 1].GetComponent<Button>().navigation;
            nav2.selectOnRight = _ptrGames[0].GetComponent<Selectable>();
            _games[_games.Count - 1].GetComponent<Button>().navigation = nav2;
            _ptrActive = true;
        }
        else
        {
            foreach (var i in _ptrGames)
            {
                i.gameObject.SetActive(false);
            }
            Navigation nav1 = _games[0].GetComponent<Button>().navigation;
            nav1.selectOnLeft = _games[_games.Count - 1].GetComponent<Selectable>();
            _games[0].GetComponent<Button>().navigation = nav1;
            Navigation nav2 = _games[_games.Count - 1].GetComponent<Button>().navigation;
            nav2.selectOnRight = _games[0].GetComponent<Selectable>();
            _games[_games.Count - 1].GetComponent<Button>().navigation = nav2;
            _ptrActive = false;
        }
    }

    public void SelectItem(RectTransform go)
    {
        bool inView = RectTransformUtility.RectangleContainsScreenPoint(_viewPort, go.pivot);
        if(_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
        if (!inView)
        {
            _leftPos = -go.anchoredPosition.x + _offset - go.sizeDelta.x;
            _rightPos = _viewPort.rect.width - go.anchoredPosition.x - _offset + go.sizeDelta.x;
            _coroutine = StartCoroutine(MovePosition());
        }
    }

    IEnumerator MovePosition()
    {
        float Timer = 0;
        Vector2 vec = _content.anchoredPosition;
        while(Timer < 1)
        {
            if(_leftPos > _content.anchoredPosition.x)
            {
                _content.anchoredPosition = Vector2.Lerp(vec, new Vector2(_leftPos, 0), Timer);
            }
            else
            {
                _content.anchoredPosition = Vector2.Lerp(vec, new Vector2(_rightPos, 0), Timer);
            }
            Timer += Time.deltaTime;
            yield return null;
        }
    }

}
