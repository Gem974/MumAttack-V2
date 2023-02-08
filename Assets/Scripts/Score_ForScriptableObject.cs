using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Score_ForScriptableObject", menuName = "Datas/Score", order = 0)]
public class Score_ForScriptableObject : ScriptableObject
{
    public int[] _score = {0,0};
    public List<int> _highScores;
    public List<string> _highScoreNames;
    //public List<HighScoreEntry> _highScoreEntryList;
    
    //Renvoie une liste de int sous forme de tableau
    //Avec les changements
    //[x,y] où x est le player et y la place changée

    private void Awake() {
        
        
    }

    private void Start() {
        Debug.Log(PlayerPrefs.GetString("highscoreTable"));
        Debug.Log("UU");
    }
    public void ResetScore(){
        _score = new int[]{0,0};
    }

    

    void ForceSerialization(){
        #if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
        #endif
    }

    /*public int[][] PlacesModified(){
        //Création d'un tableau de 2 entrées (1 pour chaque joueur)
        int[][] valuesChanged = new int[2][];
        bool[] isHighscored = {false,false};
        
        Debug.Log("Verify HighSCore par rapport à " + _score[0] + "et" + _score[1]);


        bool _beginWithOne = true;
        //Vérifions d'abord si le socre2 est pas inférieur au score1
        if(_score[0] < _score[1]){
            _beginWithOne = false;
        }

        //Parcourir tous les highscores
        for(int _highScoreID = 0; _highScoreID<5;_highScoreID++){
            if(_beginWithOne){
                //Si le J1 a dépassé le highScore
                if(_highScores[_highScoreID] < _score[0] && !isHighscored[0]){
                    Debug.Log("La place "+(_highScoreID+1) +"A été dépassée par J1 !");
                    ChangeHighScore(_highScoreID,_score[0]);
                    valuesChanged[0] = new int[]{0,_highScoreID};
                    isHighscored[0] = true;
                    
                //Si le J2 a dépassé le score
                }else if(_highScores[_highScoreID] < _score[1] && !isHighscored[1]){
                    Debug.Log("La place "+(_highScoreID+1) +"A été dépassée par J2 !");
                    ChangeHighScore(_highScoreID,_score[1]);
                    valuesChanged[1] = new int[]{1,_highScoreID};
                    isHighscored[1] = true;
                }
            }else{
                //Si le J2 a dépassé le score
                if(_highScores[_highScoreID] < _score[1] && !isHighscored[1]){
                    Debug.Log("La place "+(_highScoreID+1) +"A été dépassée par J2 !");
                    ChangeHighScore(_highScoreID,_score[1]);
                    valuesChanged[1] = new int[]{1,_highScoreID};
                    isHighscored[1] = true;
                }

                //Si le J1 a dépassé le highScore
                else if(_highScores[_highScoreID] < _score[0] && !isHighscored[0]){
                    Debug.Log("La place "+(_highScoreID+1) +"A été dépassée par J1 !");
                    ChangeHighScore(_highScoreID,_score[0]);
                    valuesChanged[0] = new int[]{0,_highScoreID};
                    isHighscored[0] = true;
                }
            }
        }

        return valuesChanged;
    }

    //Renvoie la place qui a été modifiée
    public int VerifyOneScore(int _newScore){
        for(int _y=0;_y<5;_y++){
            if(_newScore > _highScores[_y]){
                Debug.Log("La place "+(_y+1) +"A été dépassée par le score "+ _newScore +" !");
                ChangeHighScore(_y,_newScore);
                int _place = _y;
                return _place;
            }
        }
        return -1;
    }
    /*void ChangeHighScore(int _place, int _newHighScore){
        _highScores.Insert(_place,_newHighScore);
        ForceSerialization();

        //On va supprimer tous les index au delà de 10 (car pas affichés de totue façon)
        if(_highScores.Count>10){
            Debug.Log("Attention c'est suéprieur à 10 !");
        }
    }*/
    /*public void AddNewName(int _place,string _name){
        if(_name.Length == 0 ) _highScoreNames[_place] = "Anonymus";
        _highScoreNames.Insert(_place,_name);
        //_highScoreNames[_place] = _name;
        ForceSerialization();
    }*/
    

    public void EraseScore(int _place){
        _highScoreNames.RemoveAt(_place);
        _highScores.RemoveAt(_place);
        ForceSerialization();
    }

    

}
