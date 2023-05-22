using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxiMum_Countdown : MonoBehaviour
{
   public void ActivePause()
    {
        MaxiMum_GameManager._isPaused = false;
        this.gameObject.SetActive(false);
    }
}
