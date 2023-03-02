using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Utencil_Brawl;


public class UtencilBrawl_GameManager : MonoBehaviour
{
    public Player _J1;
    public Player _J2;
    public GameObject _GOPanel;

    public bool _isGameStopped = false;

    public bool _canPlay;

    public static UtencilBrawl_GameManager instance;



    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Il y a plus d'une instance de GameManager dans la scène");
            return;
        }

        instance = this;
    }

    void Start()
    {
       
        _canPlay = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isGameStopped == false && _J1._touches == 0 || _J2._touches == 0 && _isGameStopped == false)
        {
            Victory();
        }

        
    }

    public void Victory()
    {
        if (_J1._touches <= 0 )
        {
            Debug.Log("J2 Win");
            _isGameStopped = true;
            _GOPanel.SetActive(true);
            GameOverBehaviour.instance.PlayerToWin(2);
       

        }
        else if (_J2._touches <= 0)
        {
            Debug.Log("J1 Win");
            _isGameStopped = true;
            _GOPanel.SetActive(true);
            GameOverBehaviour.instance.PlayerToWin(1);
      
        }

    }

    public void Niveau()
    {
        Debug.Log("reload");
        SceneManager.LoadScene(0);
    }

    public void StartGameAfterDiscount()
    {
        Time.timeScale = 1;
        _isGameStopped = false;
        _canPlay = true;

    }




}
