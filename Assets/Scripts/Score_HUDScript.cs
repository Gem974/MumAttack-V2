using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Score_HUDScript : MonoBehaviour
{
    public TextMeshProUGUI[] _currentScore;
    public TextMeshProUGUI[] _highScoreName;
    public TextMeshProUGUI[] _highScoreNumber;
    public Image[] _highScoreBarScore;

    public GameObject[] _inputName;
    
    //public Score_ForScriptableObject _scoreDatas;
    public GameObject _noClassement;
    public GameObject _yesClassement;
    public TextMeshProUGUI _winText;
    public Image _winImage;
    public Sprite[] _winImages;
    public Sprite[] _barPlayerClassement;

    [SerializeField] Script_GameManager _GM;
    private void Start() {
        //For testing
        _GM = Script_GameManager.instance;
        if(SceneManager.GetActiveScene().buildIndex == 3)
        {
            Debug.Log("On actualise classement");
            ActualizeClassement();
            ActualizeScore();
        }
        ShowClassement();
    }

    public void ActualizeAll(){
        ActualizeScore();
        ActualizeClassement();
        ShowClassement();
    }

    //Quand on a l'écran des scores
    public void ActualizeScore(){
        if(_GM == null)
        {
            _GM = Script_GameManager.instance;
        }
        for(int _i=0;_i<_GM._score.Length;_i++){
            _currentScore[_i].text = _GM._score[_i].ToString();
        }

        //Victoire de J1
        if(_GM._score[0] > _GM._score[1]){
            _winImage.sprite = _winImages[0];
            _winText.text = "J1 est vainqueur !";
        }
        //Victoire de J2
        else{
            _winImage.sprite = _winImages[1];
            _winText.text = "J2 est vainqueur !";
        }        
    }

    public void ShowClassement(){
        //Modifier l'affichage
        for(int _i=0;_i<5;_i++){
            //Modifier les noms de base
            _highScoreName[_i].text = _GM._highScoreEntryList[_i]._name;
            //_highScoreName[_i].text = _scoreDatas._highScoreNames[_i].ToString();

            //Modifier les scores de base
             _highScoreNumber[_i].text = _GM._highScoreEntryList[_i]._score.ToString();
            //_highScoreNumber[_i].text = _scoreDatas._highScores[_i].ToString();
        }
    }
    public void ActualizeClassement(){
        if (_GM == null)
        {
            _GM = Script_GameManager.instance;
        }
        //Pour les deux scores de la partie, on va vérifier si ça high score

        int[][] _placesModified = _GM.PlacesModified();

        //On récupère un array de int[]
        //Ex: [0,1],[1,3] (J1=Place 1, J2=Place 3)
        //On va décomposer la liste
        for(int _y=0;_y<2;_y++){
            //Ex: [0,1]
            if(_placesModified[_y] == null){
                Debug.Log("Aucun résultat modifié pour le J"+_y+"!");
            }else{
                int _player = _placesModified[_y][0];
                int _place = _placesModified[_y][1];

                _highScoreName[_place].gameObject.SetActive(false);
                //Activer l'input
                _inputName[_place].SetActive(true);
                //Modifier le sprite
                _highScoreBarScore[_place].sprite = _barPlayerClassement[_player];
            }
        }
        
        /*
        for(int _i=0;_i<2;_i++){
            int _player = _i;

            
            


            //Si ça a highscore
            int _placeModified = _scoreDatas.VerifyOneScore(_scoreDatas._score[_i]);
            
            //Le -1 représente ici une non modification de place car 0 peut être la place 0.
            //Et on ne peut mettre une valeur null a un int.
            //Si la place a été modifiée
            if(_placeModified != -1){
                //Désactiver le nom de base
                _highScoreName[_placeModified].gameObject.SetActive(false);
                //Activer l'input
                _inputName[_placeModified].SetActive(true);
                //Modifier le sprite
                _highScoreBarScore[_placeModified].sprite = _barPlayerClassement[_player];
            }   
        }*/
       
    }
}
