using UnityEngine;
using UnityEngine.Playables;
using System.Collections;
using TMPro;
using System.Collections.Generic;


public class Script_Timer : MonoBehaviour
{
    public float timeRestant; //Besoin récupérer le temps qui reste ?
    public bool isFinished=false; //Besoin de vérifier si le jeu est terminé ?

    public int secondsToWait=120;
    bool isGameStarted = false;
    float startTime; //Au cas où on a besoin de delay le start
    private Script_HUDManager hudManager;
    private Script_GameManager _GM;
    //public PlayableDirector _director;
    
    
    //Pour le Spawn
    public int _phase;
    
    private void Start() {
        hudManager = Script_HUDManager.instance;
        _GM = Script_GameManager.instance;
    }
    private void Update() {
        if(isGameStarted){
            //Si terminé
            if(timeRestant<=0 && !isFinished){
                isFinished=true;               
                Finished();

            }else if(!isFinished){
                timeRestant = secondsToWait- (Time.time-startTime);
                string minutes = "0"+((int) timeRestant/60).ToString();
                string seconds = (timeRestant%60).ToString("f0");
                string finalText = minutes+":"+seconds;
                
                hudManager.SetText(finalText,0);
            }
        }       
    }

    public void StartGame(){
        //Lancer le chrono
        isGameStarted=true;
        //Lancer le startTime
        startTime = Time.time;
        timeRestant = secondsToWait;
    }

    public void Finished(){
        hudManager.SetText("00:00",0);
        Debug.Log("Le chrono est terminé ! On arrête le jeu !");
        _GM.GetScore();

        //_director.Play();
        StartCoroutine(WaitBeforeLoadScene());
        
    }

    IEnumerator WaitBeforeLoadScene(){
        yield return new WaitForSeconds(3);
        FindObjectOfType<Script_GameManager>().GetScore();
        Debug.Log("Load la scene");
        FindObjectOfType<Script_GameManager>().LoadScene(3);
    }

}
