using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    #region Variables

   // public static CameraShake _instance;
    private float _shakeTimeRemaining, _shakePower, _shakeFadeTime;
    private Vector3 _initPos;

	#endregion

	#region Unity Methods
    // Start is called before the first frame update
    void Start()
    {
        _initPos = transform.position;
     //   _instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            StartShake(5f, .3f);
        }

    }

    private void LateUpdate()
    {
        if( _shakeTimeRemaining > 0)
        {
            _shakeTimeRemaining -= Time.deltaTime;

            float xAmount = Random.Range(-1f, 1f)* _shakePower;
            float yAmount = Random.Range(-1f, 1f)* _shakePower;

            transform.position += new Vector3(xAmount, yAmount, 0f);

            _shakePower = Mathf.MoveTowards(_shakePower, 0f, _shakeFadeTime * Time.deltaTime);
        }else
        {
            transform.position = _initPos;
        }
        
        
    }

    #endregion

    #region Custom Methods
    public void StartShake(float length, float power)
    {
        _shakeTimeRemaining = length;
        _shakePower = power;

        _shakeFadeTime = power / length;
    }

	#endregion
}
