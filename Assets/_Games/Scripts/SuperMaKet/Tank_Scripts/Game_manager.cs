using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Game_manager : Ui
{
    

    public GameObject[] _tank;
    public AudioSource _bgMusic;
    public GameObject[] _Map;

    [Header("Timer")]
    public int _timer;

    public bool _endGame = false;
    public int _startTimer;
    public bool _start = false;
    public GameObject _StartPanelTank;

    private void Start()
    {
        StartCoroutine(Starttimer());

       
        


        _endGame = false;

        int i = Random.Range(0, _Map.Length);
        _Map[i].SetActive(true);
    }

    private void Update()
    {
        _start_timer.text = _startTimer.ToString();


        if (_start)
        {
            StopCoroutine(Starttimer());
            _StartPanelTank.SetActive(false);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            _panelLogo.SetActive(false);
        }

        if (_timer == 0)
        {
            StopAllCoroutines();
            _panelInGame.SetActive(false);
            _panelMatchNull.SetActive(true);
            _endGame = true;
            for (int i = 0; i < _tank.Length; i++)
            {
                _tank[i].GetComponent<Tank_movement>().enabled = false;
            }

            
        }
    }

    

    IEnumerator Starttimer()
    {
        yield return new WaitForSeconds(1);
        _startTimer--;

        if (_startTimer > 0)
        {
            StartCoroutine(Starttimer());
        }
        
        if (_startTimer == 0)
        {
            _start = true;

            StartCoroutine(Time());

            for (int i = 0; i < _tank.Length; i++)
            {
                _tank[i].GetComponent<Tank_movement>().enabled = true;
            }
           
        }

    }
    public void PlayBouton()
    {
       
        SceneManager.LoadScene(1);

    }

    public void RestartButton()
    {
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }
    public void Menu()
    {
        SceneManager.LoadScene(0);
    }

    /*void Boucle()
    {
        for (int i = 0; i < _tank.Length; i++)
        {
            _tank[i].SetActive(true);
        }
    }*/


    IEnumerator Time()
    {
        _timer--;
        _timerText.text = _timer.ToString();
        yield return new WaitForSeconds(1f);

        StartCoroutine(Time());
        

    }
    

   public void InstanciateFx(GameObject gm, Vector3 position, Quaternion rotate)
    {
        Instantiate(gm, position, rotate);
    }

    
}
        

