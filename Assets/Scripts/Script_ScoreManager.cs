using UnityEngine;

public class Script_ScoreManager : MonoBehaviour
{
    public int[] _score = {0,0};
    int _nbPointWinByGarbage=100;
    Script_HUDManager _HM;
    
    public float[] _currentCombotFadeTimers;
    public float _InitCombotCDTimer=2f;
    public int[] _comboSlots;
    public int _oldPlayerCombo;
    public bool[] _isComboFinished;

    public int _wrongCompter;
    public int _rightCompter;
    
    private void Update() {

        for (int i = 0; i < 2; i++)
        {
            if (_currentCombotFadeTimers[i] > 0)
            {
                _currentCombotFadeTimers[i] -= Time.deltaTime;
            }
            else if (_currentCombotFadeTimers[i] < 0)
            {
                _currentCombotFadeTimers[i] = 0;
                _comboSlots[i] = 0;
            }

            _isComboFinished[i] = _comboSlots[i] == 0 ? true : false;
        }


        if (_wrongCompter >= 5)
        {
            _wrongCompter = 0;
            _rightCompter = 0;

            int _rdmSFX = Random.Range(16, 19);

            //playSound SFX_16_17_18_WrongTrashVoiceSong
            SoundManager.instance.StartSound(_rdmSFX, Camera.main.transform.position, 1);
        }

        if (_rightCompter >= 5)
        {
            _rightCompter = 0;
            _wrongCompter = 0;

            int _rdmSFX = Random.Range(13, 16);

            //playSound SFX_13_14_15_RightTrashVoiceSong
            SoundManager.instance.StartSound(_rdmSFX, Camera.main.transform.position, 1);
        }

    }

    private void Start() {
        _HM = FindObjectOfType<Script_HUDManager>();
    }

    public void AddPoint(int idPlayer , Vector3 trashPosition, bool rightTrash){

        _currentCombotFadeTimers[idPlayer] = _InitCombotCDTimer;

        if (!rightTrash)
        {
            _HM.InstantiateScoreTxt(trashPosition, "+ 0", idPlayer);

            //playSound SFX_9_WrongTrash
            SoundManager.instance.StartSound(9, trashPosition, 1f);
            _comboSlots[idPlayer] = 0;
            _wrongCompter++;

            return;
        }
        else
        {
            _rightCompter++;
        }


        //Vérifier si ya pas de combo
        //Lancer le combo
        if (_currentCombotFadeTimers[idPlayer] <= 0){
            _comboSlots[idPlayer]++;

            //playSound SFX_4_Score_Up
            SoundManager.instance.StartSound(4, trashPosition, 1f);

        }
        else if(_currentCombotFadeTimers[idPlayer] > 0){
            
            _comboSlots[idPlayer]++;

            if (_comboSlots[idPlayer] >= 5)
            {
                //playSound SFX_8 _Score_Up
                SoundManager.instance.StartSound(8, trashPosition, 1f);
            }
            else
            {
                //playSound SFX_5 SFX_6 SFX_7 _Score_Up
                SoundManager.instance.StartSound(_comboSlots[idPlayer] + 3, trashPosition, 1f);
            }
        }

        //Mettre à jour le score du player
        _score[idPlayer]+=CalculatePoint(idPlayer);
        _HM.InstantiateScoreTxt(trashPosition, "+ " + CalculatePoint(idPlayer).ToString(), idPlayer);

        UpdateGUI(idPlayer);
    }

    private float NormalizeSlider(float rawValue,float max){
        return rawValue /max;
    }

    int CalculatePoint(int idPlayer){
        int finalScore = _comboSlots[idPlayer] *_nbPointWinByGarbage;
        return finalScore;
    }
    private void UpdateGUI(int IDPlayer){
        //MaJ score player
        _HM.SetText(_score[IDPlayer].ToString(), IDPlayer + 1);
        
        //Mettre à jour le total
        int _totalScore = _score[0] + _score[1];
        _HM.SetText(_totalScore.ToString(),3);

        //Modifier le slider
        //Get the less value
        
        _HM.SetSlider(NormalizeSlider(_score[0],_totalScore));
    }

    

    

}
