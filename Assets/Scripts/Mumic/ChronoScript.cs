using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChronoScript : MonoBehaviour
{
    [SerializeField] GameObject _gameOverPanel;
    //[SerializeField] Emrandomimg _gameManager;
    [SerializeField] Text _chronoText;
    public bool _canPlay;

    public void ChronoStart()
    {
        StartCoroutine(Chrono());
    }
    public IEnumerator Chrono()
    {
        _gameOverPanel.SetActive(true);
        _canPlay = false;
        _chronoText.text = "3";
        yield return new WaitForSeconds(1f);
        _chronoText.text = "2";
        yield return new WaitForSeconds(1f);
        _chronoText.text = "1";
        yield return new WaitForSeconds(1f);
        _chronoText.text = "START !";
        yield return new WaitForSeconds(1f);
        _canPlay = true;
        _gameOverPanel.SetActive(false);

    }
}
