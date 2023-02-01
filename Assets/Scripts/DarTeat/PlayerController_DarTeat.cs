using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_DarTeat : MonoBehaviour
{
    public GameObject _sightPlayer1, _sightPlayer2;
    public float _sightSpeed = 250f;
    public Camera _mainCamera;
    public GameObject _player1Hand;
    public GameObject _player2Hand;

    public static PlayerController_DarTeat instance;

    private void Start()
    {
        if (instance != null)
            Debug.Log("Il y a déjà une instance dans la scène.");

        instance = this;
    }

    void Update()
    {
        if(!GameManager_DarTeat.instance._gameIsFinished)
        {
            MoveViseurForPlayer1();
            MoveViseurForPlayer2();

            ShootForPlayer1();
            ShootForPlayer2();

            ClampPosition();

            //if (BulletBehavior_DarTeat.instance.transform.position.z < 0)
            //    BulletBehavior_DarTeat.instance.Hit(_sightPlayer1.transform.position);
            //else
            //    BulletBehavior_DarTeat.instance.Hit(_sightPlayer1.transform.position - _mainCamera.transform.position);
        }
    }

    void MoveViseurForPlayer1()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            _sightPlayer1.transform.position = new Vector2(_sightPlayer1.transform.position.x, _sightPlayer1.transform.position.y + _sightSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S))
        {
            _sightPlayer1.transform.position = new Vector2(_sightPlayer1.transform.position.x, _sightPlayer1.transform.position.y -_sightSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.Q))
        {
            _sightPlayer1.transform.position = new Vector2(_sightPlayer1.transform.position.x - _sightSpeed * Time.deltaTime, _sightPlayer1.transform.position.y);
        }

        if (Input.GetKey(KeyCode.D))
        {
            _sightPlayer1.transform.position = new Vector2(_sightPlayer1.transform.position.x + _sightSpeed * Time.deltaTime, _sightPlayer1.transform.position.y);
        }
    }

    void MoveViseurForPlayer2()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            _sightPlayer2.transform.position = new Vector2(_sightPlayer2.transform.position.x, _sightPlayer2.transform.position.y + _sightSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            _sightPlayer2.transform.position = new Vector2(_sightPlayer2.transform.position.x, _sightPlayer2.transform.position.y - _sightSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            _sightPlayer2.transform.position = new Vector2(_sightPlayer2.transform.position.x - _sightSpeed * Time.deltaTime, _sightPlayer2.transform.position.y);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            _sightPlayer2.transform.position = new Vector2(_sightPlayer2.transform.position.x + _sightSpeed * Time.deltaTime, _sightPlayer2.transform.position.y);
        }
    }

    void ClampPosition()
    {
        float posXPlayer1 =Mathf.Clamp(_sightPlayer1.transform.position.x, -3.85f, 3.85f);
        float posYPlayer1 = Mathf.Clamp(_sightPlayer1.transform.position.y, 0.2f, 2.2f);
        _sightPlayer1.transform.position = new Vector3(posXPlayer1, posYPlayer1, _sightPlayer1.transform.position.z);

        float posXPlayer2 = Mathf.Clamp(_sightPlayer2.transform.position.x, -3.85f, 3.85f);
        float posYPlayer2 = Mathf.Clamp(_sightPlayer2.transform.position.y, 0.2f, 2.2f);
        _sightPlayer2.transform.position = new Vector3(posXPlayer2, posYPlayer2, _sightPlayer2.transform.position.z);
    }

    void ShootForPlayer1()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.DrawRay(_mainCamera.transform.position, _sightPlayer1.transform.position - _mainCamera.transform.position, Color.grey, 10f, true);
            Debug.DrawRay(_sightPlayer1.transform.position, _sightPlayer1.transform.position - _mainCamera.transform.position, Color.green, 10f, true);

            RaycastHit hit;


            if (Physics.Raycast(_sightPlayer1.transform.position, _sightPlayer1.transform.position - _mainCamera.transform.position , out hit))
            {
                if(hit.transform.CompareTag("Teat"))
                {
                    ScoreController_DarTeat.instance.AddValue(hit.transform.parent.transform.parent.GetComponent<BabyBehavior_DarTeat>()._valueToAdd, 1);
                    hit.transform.parent.transform.parent.GetComponent<BabyBehavior_DarTeat>()._teat.SetActive(true);
                    hit.transform.GetComponent<CapsuleCollider>().enabled = false;

                    //Update Final Score
                    ScoreController_DarTeat.instance._successfulTeatsThrowingPlayer1++;
                    //Sound
                    SoundManager_DarTeat.instance._soundEffectsPlayerController.PlayOneShot(SoundManager_DarTeat.instance._addPointSoundEffect);
                    //Camera Shake
                    CameraShake_DarTeat.instance.ShakeCamera();

                    if (TimerBehavior_DarTeat._goldenTeat)
                        GameManager_DarTeat.instance.Victory();
                }
            }
            //Sound
            SoundManager_DarTeat.instance._soundEffectsPlayerController.PlayOneShot(SoundManager_DarTeat.instance._shotSoundEffect);
            //Update Final Score
            ScoreController_DarTeat.instance._numberOfTryingPlayer1++;
        }
    }

    void ShootForPlayer2()
    {
        if (Input.GetKeyDown(KeyCode.RightControl))
        {
            Debug.DrawRay(_mainCamera.transform.position, _sightPlayer2.transform.position - _mainCamera.transform.position, Color.green, 10f, true);
            Debug.DrawRay(_sightPlayer2.transform.position, _sightPlayer2.transform.position - _mainCamera.transform.position, Color.green, 10f, true);

            RaycastHit hit;

            if (Physics.Raycast(_sightPlayer2.transform.position, _sightPlayer2.transform.position - _mainCamera.transform.position, out hit))
            {
                if (hit.transform.CompareTag("Teat"))
                {
                    ScoreController_DarTeat.instance.AddValue(hit.transform.parent.transform.parent.GetComponent<BabyBehavior_DarTeat>()._valueToAdd, 2);
                    hit.transform.parent.transform.parent.GetComponent<BabyBehavior_DarTeat>()._teat.SetActive(true);
                    hit.transform.GetComponent<CapsuleCollider>().enabled = false;

                    //Update Final Score
                    ScoreController_DarTeat.instance._successfulTeatsThrowingPlayer2++;
                    //Sound
                    SoundManager_DarTeat.instance._soundEffectsPlayerController.PlayOneShot(SoundManager_DarTeat.instance._addPointSoundEffect);
                    //Camera Shake
                    CameraShake_DarTeat.instance.ShakeCamera();

                    if (TimerBehavior_DarTeat._goldenTeat)
                        GameManager_DarTeat.instance.Victory();
                }
            }
            //Sound
            SoundManager_DarTeat.instance._soundEffectsPlayerController.PlayOneShot(SoundManager_DarTeat.instance._shotSoundEffect);
            //Update Final Score
            ScoreController_DarTeat.instance._numberOfTryingPlayer2++;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.grey;
        Gizmos.DrawRay(_mainCamera.transform.position, _sightPlayer1.transform.position - _mainCamera.transform.position);
        Gizmos.color = Color.green;
        Gizmos.DrawRay(_sightPlayer1.transform.position, _sightPlayer1.transform.position - _mainCamera.transform.position);

        Gizmos.color = Color.grey;
        Gizmos.DrawRay(_mainCamera.transform.position, _sightPlayer2.transform.position - _mainCamera.transform.position);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(_sightPlayer2.transform.position, _sightPlayer2.transform.position - _mainCamera.transform.position);
    }
}
