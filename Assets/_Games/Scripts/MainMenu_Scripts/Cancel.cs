using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cancel : MonoBehaviour
{

    public GameObject _playPanel, _modePanel;
    public Button _playBtn;
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            Back();
        }
    }

    public void Back()
    {
        _playPanel.SetActive(true);
        _playBtn.Select();
        _modePanel.SetActive(false);
    }
}
