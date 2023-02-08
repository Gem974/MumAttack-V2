using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class Script_GameManager : MonoBehaviour
{
    #region Singleton
    public static Script_GameManager instance;

    #endregion
    //public Score_ForScriptableObject _scoreDatas;
    bool _isDebug=false;
    [SerializeField] Animator _transition;
    float transitionTime = 5f;

    public List<HighScoreEntry> _highScoreEntryList;
    public int[] _score = {0,0};
    public List<int> _highScoresList = new List<int>(); //A remplacer par _highScoreEntry list après
    private static Script_GameManager _GM;
    private void Awake() {
        //Pour le singleton
        instance = this;

        DontDestroyOnLoad(this.gameObject);
        if(_GM == null)
        {
            _GM = this;
        }
        else
        {
            Destroy(gameObject);
        }

        /*_highScoreEntryList = new List<HighScoreEntry>(){
            new HighScoreEntry{ _score = 1000, _name = "xx x x "},
            new HighScoreEntry{ _score = 500, _name = "PierQuiRoule"},
            new HighScoreEntry{ _score = 300, _name = "Anto Riz"},
            new HighScoreEntry{ _score = 200, _name = "Aude Ray"},
            new HighScoreEntry{ _score = 100, _name = "Sin Tia"}
        };*/
        
        //PlayerPrefs.DeleteKey("highscoreTable");
        string _jsonString = PlayerPrefs.GetString("highscoreTable");
        Debug.Log(_jsonString);
        
        
        //Récupérer les données en json
        

        if(_jsonString.Length == 0)
        {
            _highScoreEntryList = new List<HighScoreEntry>(){
            new HighScoreEntry{ _score = 1000, _name = "xx x x "},
            new HighScoreEntry{ _score = 500, _name = "PierQuiRoule"},
            new HighScoreEntry{ _score = 300, _name = "Anto Riz"},
            new HighScoreEntry{ _score = 200, _name = "Aude Ray"},
            new HighScoreEntry{ _score = 100, _name = "Sin Tia"} };
             //Mettre ça sur PlayerPrefs
            HighScores _highScores = new HighScores{ _highScoreEntryList = _highScoreEntryList};
            string json = JsonUtility.ToJson(_highScores);
            PlayerPrefs.SetString("highscoreTable",json);
            PlayerPrefs.Save();
            Debug.Log(PlayerPrefs.GetString("highscoreTable"));

        }
        
        else
        {
            //Décompresser les highScores de json en HighScore
            HighScores _highScores = JsonUtility.FromJson<HighScores>(_jsonString);
            _highScoreEntryList = _highScores._highScoreEntryList;

            //Mettre dans le highscore
            foreach (HighScoreEntry _entry in _highScoreEntryList)
            {
                _highScoresList.Add(_entry._score);
            }

        }


        //Trier par score

        /*
        //Mettre ça sur PlayerPrefs
        HighScores _highScores = new HighScores{ _highScoreEntryList = _highScoreEntryList};
        string json = JsonUtility.ToJson(_highScores);
        PlayerPrefs.SetString("highscoreTable",json);
        PlayerPrefs.Save();
        Debug.Log(PlayerPrefs.GetString("highscoreTable"));
        */
    }
    #region SCORING
    public void ResetScore(){
        _score = new int[]{0,0};
    }
    public void EraseScore(int _place){
        _highScoreEntryList.RemoveAt(_place);
    }

    public int[][] PlacesModified(){
        //Création d'un tableau de 2 entrées (1 pour chaque joueur)
        int[][] valuesChanged = new int[2][];
        bool[] isHighscored = {false,false};
        
        Debug.Log("Verify HighSCore par rapport à " + _score[0] + "et" + _score[1]);


        bool _beginWithOne = true;
        //Vérifions d'abord si le socre2 est pas inférieur au score1
        if(_score[0] <= _score[1]){
            _beginWithOne = false;
        }

        //Parcourir tous les highscores
        for(int _highScoreID = 0; _highScoreID<5;_highScoreID++){
            if(_beginWithOne){

                //Si le J1 a dépassé le highScore
                if(_highScoresList[_highScoreID] < _score[0] && !isHighscored[0]){
                    Debug.Log("La place "+(_highScoreID+1) +"A été dépassée par J1 !");
                    ChangeHighScore(_highScoreID,_score[0]);
                    valuesChanged[0] = new int[]{0,_highScoreID};
                    isHighscored[0] = true;
                    
                //Si le J2 a dépassé le score
                }else if(_highScoresList[_highScoreID] < _score[1] && !isHighscored[1]){
                    Debug.Log("La place "+(_highScoreID+1) +"A été dépassée par J2 !");
                    ChangeHighScore(_highScoreID,_score[1]);
                    valuesChanged[1] = new int[]{1,_highScoreID};
                    isHighscored[1] = true;
                }
                
            }else{
                //Si le J2 a dépassé le score
                if(_highScoresList[_highScoreID] < _score[1] && !isHighscored[1]){
                    Debug.Log("La place "+(_highScoreID+1) +"A été dépassée par J2 !");
                    ChangeHighScore(_highScoreID,_score[1]);
                    valuesChanged[1] = new int[]{1,_highScoreID};
                    isHighscored[1] = true;
                }

                //Si le J1 a dépassé le highScore
                else if(_highScoresList[_highScoreID] < _score[0] && !isHighscored[0]){
                    Debug.Log("La place "+(_highScoreID+1) +"A été dépassée par J1 !");
                    ChangeHighScore(_highScoreID,_score[0]);
                    valuesChanged[0] = new int[]{0,_highScoreID};
                    isHighscored[0] = true;
                }
            }
        }
        return valuesChanged;
    }

    void ChangeScoreInDatabase(){
        //Ajouter au PlayerPrefs
        HighScores _highScores = new HighScores{ _highScoreEntryList = _highScoreEntryList};
        string json = JsonUtility.ToJson(_highScores);
        PlayerPrefs.SetString("highscoreTable",json);
        PlayerPrefs.Save();
        Debug.Log(PlayerPrefs.GetString("highscoreTable"));
    }

    void ChangeHighScore(int _place, int _newHighScore){
        _highScoresList.Insert(_place,_newHighScore);
        HighScoreEntry _newHighScoreEntry = new HighScoreEntry{ _score = _newHighScore, _name = "Unknown"};
        _highScoreEntryList.Insert(_place,_newHighScoreEntry);
        ChangeScoreInDatabase();

        //On va supprimer tous les index au delà de 10 (car pas affichés de totue façon)
        if(_highScoresList.Count>10){
            Debug.Log("Attention c'est suéprieur à 10 !");
        }
    }

    
    public void AddNewName(int _place,string _name){
        if(_name.Length == 0 ) {
            _highScoreEntryList[_place]._name = "Unknown";
            ChangeScoreInDatabase();
            return;
        }
        _highScoreEntryList[_place]._name = _name;
        ChangeScoreInDatabase();

    }
    #endregion

    private void Start() {
        //ResetScore();
        SceneManager.activeSceneChanged += ChangedActiveScene;
        
        if(SceneManager.GetActiveScene().buildIndex == 0){
            //Mettre le bon highScore
            FindObjectOfType<Score_HUDScript>().ShowClassement();
        }
    }
    private void Update() {
        if( !_isDebug) return;

        //Là pouvoir reset les scores
        if(Input.GetKeyDown(KeyCode.F1)){
            Debug.Log("ERASE SCORE 1");
            EraseScore(0);
        }
        //Là pouvoir reset les scores
        if(Input.GetKeyDown(KeyCode.F2)){
            Debug.Log("ERASE SCORE 2");
            EraseScore(1);
        }
        //Là pouvoir reset les scores
        if(Input.GetKeyDown(KeyCode.F3)){
            Debug.Log("ERASE SCORE 3");
            EraseScore(2);
        }
        //Là pouvoir reset les scores
        if(Input.GetKeyDown(KeyCode.F4)){
            Debug.Log("ERASE SCORE 4");
            EraseScore(3);
        }
        //Là pouvoir reset les scores
        if(Input.GetKeyDown(KeyCode.F5)){
            Debug.Log("ERASE SCORE 5");
            EraseScore(4);
        }
        //F6 : Outline

        if(Input.GetKeyDown(KeyCode.F11)){
            LoadScene(1);
        }

        if(Input.GetKeyDown(KeyCode.F12)){
            LoadScene(0);
        }
    }

    public void LoadScene(int scene){
        StartCoroutine(LoadLevelCo(scene));
    }
    IEnumerator LoadLevelCo(int _scene){
        _transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(_scene);
    }
    private void ChangedActiveScene(Scene current, Scene next){
        //Supprimer le GameManager dans la scène si il y en a plusieurs

        _transition.SetTrigger("End");
        //Ecran de départ

        if(next.buildIndex == 2){
            _isDebug = false;
            Debug.Log("Scene de play");
            ActiveGame();
        }if(next.buildIndex == 3){
            _isDebug = true;
            Debug.Log("Scene des points");
            /*Score_HUDScript _scoreHUD = FindObjectOfType<Score_HUDScript>();

            Debug.Log(_scoreHUD);
            if(_scoreHUD==null){
                Debug.LogWarning("Pas de scoreHD trouvé");
                return;
            }*/
            //_scoreHUD.ActualizeClassement();
            //_scoreHUD.ActualizeScore();
            //_scoreHUD.ShowClassement();
            //_scoreDatas.VerifyHighScore();
        }
    }
    private void ActiveGame(){
        //FindObjectOfType<Script_Timer>().StartGame();
    }
    //Appelé grâce au timer, avant de load la scène
    public void GetScore(){
       _score = FindObjectOfType<Script_ScoreManager>()._score;
    }

    //Entrée de hishScore
    [System.Serializable]
    public class HighScoreEntry{
        public int _score;
        public string _name;
    }
    private class HighScores{
        public List<HighScoreEntry> _highScoreEntryList;
    }
}
