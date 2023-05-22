using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager_DarTeat : MonoBehaviour
{
    public void LoadScene(string nameOfScene)
    {
        //Put the name of Scene
        SceneManager.LoadScene(nameOfScene);
    }
}
