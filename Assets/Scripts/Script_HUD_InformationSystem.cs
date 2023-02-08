using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Script_HUD_InformationSystem : MonoBehaviour
{
    public int idType;
    public TextAsset infosData;
    [SerializeField] string[] datasLines;
    [SerializeField] string[] datasColumns;
    public string[]_currentDiscussionText;
    string _discussionText;
    [SerializeField,Tooltip("Prévention,Transformation,Dégradation")] GameObject[] _infoType;
    public List<int> _infosColumnOne = new List<int>();
    public List<int> _infosColumnTwo = new List<int>();
    public enum InfoMaterials {Wood,Glass,Metal,Petrole,Aluminium,Plastic};
    
    [Tooltip("Wood,Glass,Metal,Petrole,Aluminium,Plastic")]
    public Sprite[] _materialSprite;
    public Transform[] _columnsCanvas;
    private int _frequencySpawn = 3;
    private float _currenTime;

    private void Awake() {
        _currenTime = Time.time;
        ExtractDatas();
    }
    public void StartInformations() {
        for(int i =0;i<4;i++){
            CreateInformation();
        }
    }

    private void Update() {
        /*if(Input.GetKeyDown(KeyCode.I)){
            CreateInformation();
        }*/

        //Créer le générateur d'infos
        if (Time.time > _currenTime + _frequencySpawn)
        {
            CreateInformation();
            _currenTime = Time.time;
        }
    }

    [ContextMenu("Extract datas")]
    
    void ExtractDatas(){
        datasLines = infosData.text.Split(new string[] {"\n"}, StringSplitOptions.None);
            //int tableSize = data.Length / 11 -1;
            //Faut faire id + 2 pour obtenir la bonne ligne
            Debug.Log(datasLines.ToString());
            
        
                
    }
    [ContextMenu("Get Random Data")]
    int GetRandomData(){
        //0 Type / 1 Image / 2 Titre / 3 Description
        int _idData = UnityEngine.Random.Range(1,12);
        datasColumns = datasLines[_idData].ToString().Split(new string[] {";"},StringSplitOptions.None);

        _discussionText = datasColumns[3];
        
        /*if(datasColumns.Length>3){
            _currentDiscussionText = datasLines[_idData].ToString().Split(new string[] {"\""},StringSplitOptions.None);
            
            Debug.Log(_currentDiscussionText[1]);
            Debug.Break();
            _discussionText = _currentDiscussionText[1];
        }*/
        
        //Vérifier qu'il existe pas déjà
        if(_infosColumnOne.Contains(_idData) || _infosColumnTwo.Contains(_idData)){
            //Relancer le GetRandomData
            return GetRandomData();
        }

        return _idData;
    }

    [ContextMenu("Create Information")]
    void CreateInformation(){
        //Vérifier qu'on est pas au dessus du seuil
        if(_infosColumnTwo.Count>1) return;

        //Récupérer data
        int _idData =GetRandomData();
        

        //Récupérer la bonne template en fonction du type data
        int _infoTypeData = int.Parse(datasColumns[0]);
        GameObject _newInformation = _infoType[_infoTypeData];

        //Modifier ce qu'il y a besoin
        Script_HUD_Prevention _scriptInfos = _newInformation.GetComponent<Script_HUD_Prevention>();
        
        //Récupérer le sprite   
        Debug.Log(datasColumns[1]);
        int _matSprite = int.Parse(datasColumns[1]);


        Sprite _newSprite = _materialSprite[_matSprite];
        string _newTitle = datasColumns[2];
        string _newDescription = _discussionText;

        _scriptInfos.Init(_newSprite,_newTitle,_newDescription);

        //Créer l'object

        //Voir dans quelle colonne l'instantier
        int _idCanvas = 0;
        //Colonne 1
        if(_infosColumnOne.Count<2){
            _idCanvas=0;
            _infosColumnOne.Add(_idData);
            _scriptInfos.TurnToRight();
        }
        //Colonne 2
        else{
            _idCanvas = 1;
            _infosColumnTwo.Add(_idData);
            _scriptInfos.TurnToLeft();
        }
        GameObject _createdInfo = Instantiate(_newInformation,_columnsCanvas[_idCanvas]);
        

        //Lancer la coroutine pur qu'elle disparaîsse
        StartCoroutine(ETimeBeforeDisparreance(_createdInfo,_idData));
    }

    //Temps d'écran
    IEnumerator ETimeBeforeDisparreance(GameObject _objToDestroy,int _idInfo){
        int _randomTime = UnityEngine.Random.Range(8,20);
        yield return new WaitForSeconds(_randomTime);
        _objToDestroy.GetComponent<Animator>().SetTrigger("Exit");
        StartCoroutine(ETimeBeforeDestroy(_objToDestroy,_idInfo));
        
    }
    //Temps de disparition
    IEnumerator ETimeBeforeDestroy(GameObject _objToDestroy,int _idInfo){
        yield return new WaitForSeconds(2);
        if(_infosColumnOne.Contains(_idInfo)) _infosColumnOne.Remove(_idInfo);
        if(_infosColumnTwo.Contains(_idInfo)) _infosColumnTwo.Remove(_idInfo);
        //Créer une novuelle info de suite
        CreateInformation();
        Destroy(_objToDestroy);
    }
}
