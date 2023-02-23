using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UtencilBrawl_GameManager : MonoBehaviour
{
    public Player _J1;
    public Player _J2;
    public GameObject _panelJ1, _panelJ2;

    public bool _isGameStopped = false;
   

    // Start is called before the first frame update
    void Start()
    {
       Time.timeScale = 1;
       _isGameStopped = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isGameStopped == false && _J1._touches == 0 || _J2._touches == 0)
        {
            Victory();
        }

        
    }

    public void Victory()
    {
        if (_J1._touches == 0 )
        {
            Debug.Log("J2 Win");
            _panelJ2.SetActive(true);
            Time.timeScale = 0;
            _isGameStopped = true;


        }
        else if (_J2._touches == 0)
        {
            Debug.Log("J1 Win");
            _panelJ1.SetActive(true);
            Time.timeScale = 0;
            _isGameStopped = true;
        }

    }

    public void Niveau()
    {
        Debug.Log("reload");
        SceneManager.LoadScene(0);
    }


}
