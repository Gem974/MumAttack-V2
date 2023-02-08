using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
public class Script_HUDManager : MonoBehaviour
{
    #region Singleton
    public static Script_HUDManager instance;
    private void Awake() {
        instance = this;
    }
    #endregion

    public TextMeshProUGUI[] allUIs;

    //PointsAndCombos
    public GameObject _prefScoreTxt;
    public TextMeshProUGUI _currentTxt;
    public Color[] _playerTxtColor;

    [Header("For Scoring")]
    public Slider _sliderScore;

    [Header("Dechets")]
    [SerializeField] Image[] _garbageJ1;
    [SerializeField] Image[] _garbageJ2;
    GameObject[][] _garbagePlayer;
    
    [SerializeField] GameObject _canvas;
    [SerializeField] GameObject _dechetVisual;
    [SerializeField] GameObject[] _playerPosition;
    [SerializeField] Camera _camera;

    //Tous les déchets
    [SerializeField,Tooltip("Cot, Pizza, Boeuf, Masque, Cigarette, Canette, Poulet, Assiette, CapriSun")] Sprite[] _allGarbageImages;
    [SerializeField] Sprite _noGarbageImage;

    [SerializeField] int _garbageFill = 0;
    [SerializeField] List<GarbageType> _garbageArrayJ1 = new List<GarbageType>();
    [SerializeField] List<GarbageType> _garbageArrayJ2 = new List<GarbageType>();

    
    public void FillGarbage(int _player,GarbageType _garbage){
        int _garbageId = (int) _garbage; //Quel garbage ajouter
        int _place = 0; //Où l'ajouter

        //Avoir la position du tangue
        Transform _positionInScene = _playerPosition[_player].transform;
        Vector3 _posFinal = _camera.WorldToScreenPoint(_positionInScene.position);
        Transform _finalPos;

        //Créer un nouveau dechet visuel
        GameObject _dechetUIPre=  _dechetVisual;
        _dechetVisual.GetComponentInChildren<Image>().sprite = _allGarbageImages[_garbageId];
        CanvasGroup _dechetGroup = _dechetVisual.GetComponentInChildren<CanvasGroup>();
        _dechetGroup.alpha=1;
        
        //Instantionner
        GameObject _dechetUIMove = Instantiate(_dechetUIPre, _posFinal,Quaternion.identity);
        _dechetUIMove.transform.SetParent(_canvas.transform);
        
        StartCoroutine(StartOpacity(_dechetUIMove.GetComponentInChildren<CanvasGroup>()));
        


       
        
        //Joueur 1
        if(_player==0){
            //Sécurité pour vérifier qu'on est pas en overflow de liste
            if(_garbageArrayJ1.Count>=5) return;

            _garbageArrayJ1.Add(_garbage);
            //Modifier visuellement le déchet
            _place = _garbageArrayJ1.Count;

            //Obtenir la position finale
            _finalPos = _garbageJ1[_place-1].transform;

            //Faudra déplacer
            _dechetUIMove.transform.DOMove(_finalPos.position,1).SetEase(Ease.InOutSine) ;
            _garbageJ1[_place-1].sprite = _allGarbageImages[_garbageId]; 

            StartCoroutine(DestroySprite(_dechetUIMove));
            return;
        }

        //Joueur 2 
        if(_garbageArrayJ2.Count>=5) return;

        _garbageArrayJ2.Add(_garbage);
        _place = _garbageArrayJ2.Count;

         //Obtenir la position finale
            _finalPos = _garbageJ2[_place-1].transform;
            //Faudra déplacer
            _dechetUIMove.transform.DOMove(_finalPos.position,1).SetEase(Ease.InOutSine) ;
          

        _garbageJ2[_place-1].sprite = _allGarbageImages[_garbageId];
        StartCoroutine(DestroySprite(_dechetUIMove));

    }
    //Vider
    public void RemoveGarbage(int _player){
        int _place=0;
        if(_player == 0){
            _place = _garbageArrayJ1.Count-1;
            _garbageArrayJ1.RemoveAt(_place);
            _garbageJ1[_place].sprite = _noGarbageImage; 
            return;
        }

        _place = _garbageArrayJ2.Count-1;
        _garbageArrayJ2.RemoveAt(_place);
        _garbageJ2[_place].sprite = _noGarbageImage; 
    }

    IEnumerator DestroySprite(GameObject _objToDestroy){
        yield return new WaitForSeconds(3);
        Destroy(_objToDestroy);
    }
    IEnumerator StartOpacity(CanvasGroup _canvasGroup){
        yield return new WaitForSeconds(0.001f);
        _canvasGroup.DOFade(0,2);
    }


    private void Start() {
        _sliderScore.value=0.5f;
    }

    public void SetText(string textToRefresh,int UIId){
        allUIs[UIId].text = textToRefresh;
    }

    public void SetText(TextMeshProUGUI tmp, string textToRefresh)
    {
        tmp.text = textToRefresh;
    }

    public void SetSlider(float _value){
        _sliderScore.value = _value;
    }
    public void InstantiateScoreTxt(Vector3 V3Position, string Score,int idPlayer)
    {
        GameObject currenObject = Instantiate(_prefScoreTxt, V3Position, Quaternion.identity);
        _currentTxt = currenObject.GetComponent<ScoreText>()._myTxtMesh;
        _currentTxt.color = _playerTxtColor[idPlayer];
        SetText(_currentTxt, Score);
    }
}
