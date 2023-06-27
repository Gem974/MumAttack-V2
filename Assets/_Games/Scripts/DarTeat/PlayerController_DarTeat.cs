using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class PlayerController_DarTeat : MonoBehaviour
{
    public bool _isPlayerOne;
    public GameObject _teatPrefab;
    public float _sightSpeed = 250f;
    public Camera _mainCamera;
    public float _shootRate = 1f;
    private float _shootCountdown;
    [Range(1, 0)] float _progress;

    private Vector2 _moveInput = Vector2.zero;
    private bool _actionInput = false;
    private PlayerInput _playerInput;

    private void Start()
    {
        //Liaison avec le Player Input
        _playerInput = GetComponent<PlayerInput>();
        //Assignation manuelle du clavier (obligatoire vu que partager par tout les joueurs)
        InputUser.PerformPairingWithDevice(Keyboard.current, user: _playerInput.user);

        _progress = _shootCountdown;
        _shootCountdown -= Time.deltaTime;
    }

    //Event pour les touches de déplacement 
    public void OnMove(InputAction.CallbackContext context)
    {
        //On se contente de récupérer le Vecteur2 des touches de déplacement
        _moveInput = context.ReadValue<Vector2>();
    }

    //Event pour la touche d'action
    public void OnAction(InputAction.CallbackContext context)
    {
        _actionInput = context.action.triggered;
    }

    //Event pour l'action map Tuto (se mettre pret pour lancer le jeu)
    public void OnReady(InputAction.CallbackContext context)
    {
        Tutorials.instance.ReadyChecker(_isPlayerOne);
    }

    //Passer de l'action map Tuto (get ready) à l'action map de jeu
    public void ChangeActionMap()
    {
        if (_isPlayerOne)
        {
            _playerInput.SwitchCurrentActionMap("Player1");
        }
        else
        {
            _playerInput.SwitchCurrentActionMap("Player2");
        }
    }

    void Update()
    {
        if(!GameManager_DarTeat.instance._gameIsFinished)
        {
            MoveSight();

            if (_actionInput)
            {
                Shoot();
            }

            ClampPosition();

            //_ShootCountdown Constently dimminushing
            _shootCountdown -= Time.deltaTime;

        }
    }

    private Vector2 _move;
    void MoveSight()
    {
        if(_moveInput != Vector2.zero)
        {
            _move = new Vector2(_moveInput.x, _moveInput.y);
            transform.Translate(_move * _sightSpeed * Time.deltaTime, Space.World);
        }
    }

    void ClampPosition()
    {
        float posXPlayer =Mathf.Clamp(gameObject.transform.position.x, -3.85f, 3.85f);
        float posYPlayer = Mathf.Clamp(gameObject.transform.position.y, 0.2f, 2.2f);
        gameObject.transform.position = new Vector3(posXPlayer, posYPlayer, gameObject.transform.position.z);  
    }

    void Shoot()
    {
        if (_shootCountdown <= 0f)
        {
            if (_isPlayerOne)
            {
                HitChecker(gameObject, 1);
            }
            else
            {
                HitChecker(gameObject, 2);
            }
            _shootCountdown = 1 / _shootRate;
        }
    }

    void HitChecker(GameObject sight, int playerID)
    {
        Debug.DrawRay(_mainCamera.transform.position, sight.transform.position - _mainCamera.transform.position, Color.grey, 10f, true);
        Debug.DrawRay(sight.transform.position, sight.transform.position - _mainCamera.transform.position, Color.green, 10f, true);
        RaycastHit hit;
        if (Physics.Raycast(sight.transform.position, sight.transform.position - _mainCamera.transform.position, out hit))
        {
            if (hit.transform.CompareTag("Teat"))
            {
                ScoreController_DarTeat.instance.AddValue(hit.transform.parent.transform.parent.GetComponent<BabyBehavior_DarTeat>()._valueToAdd, playerID);
                hit.transform.parent.transform.parent.GetComponent<BabyBehavior_DarTeat>()._teat.SetActive(true);
                hit.transform.GetComponent<CapsuleCollider>().enabled = false;

                //Sound
                SoundManager_DarTeat.instance._soundEffectsPlayerController.PlayOneShot(SoundManager_DarTeat.instance._addPointSoundEffect);
                //Camera Shake
                CameraShake_DarTeat.instance.ShakeCamera();

                if (TimerBehavior_DarTeat._goldenTeat)
                    GameManager_DarTeat.instance.Victory();
            }
            else
            {
                Instantiate(_teatPrefab, hit.point, Quaternion.identity);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.grey;
        Gizmos.DrawRay(_mainCamera.transform.position, gameObject.transform.position - _mainCamera.transform.position);
        Gizmos.color = Color.green;
        Gizmos.DrawRay(gameObject.transform.position, gameObject.transform.position - _mainCamera.transform.position);
    }
}
