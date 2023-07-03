using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class PlayerController : MonoBehaviour
{
    //Variables
    public bool _isPlayer1;
    public bool _canMove = false;
    public bool _canDig = false;
    public float _stunDuration = 3f;
    public GameObject _digIcon, _stunIcon;
    private RaycastHit down;
    private bool _actionInput = false;
    private PlayerInput _playerInput;
    private int _clampMin = -1;
    private int _clampMax = 7;
     
    private void Start()
    {
        _canMove = true;
        _canDig = true;
        //Liaison avec le Player Input
        _playerInput = GetComponent<PlayerInput>();
        //Assignation manuelle du clavier (obligatoire vu que partager par tout les joueurs)
        InputUser.PerformPairingWithDevice(Keyboard.current, user: _playerInput.user);
    }

    //Event pour les touches de déplacement 
    public void OnMoveUp(InputAction.CallbackContext context)
    {
        if (GameManager_MumSweeper.instance._canPlay && _canMove)
            StartCoroutine(Move(Vector3.forward));

    }

    public void OnMoveDown(InputAction.CallbackContext context)
    {
        if (GameManager_MumSweeper.instance._canPlay && _canMove)
            StartCoroutine(Move(Vector3.back));
    }

    public void OnMoveLeft(InputAction.CallbackContext context)
    {
        if (GameManager_MumSweeper.instance._canPlay && _canMove)
            StartCoroutine(Move(Vector3.left));
    }

    public void OnMoveRight(InputAction.CallbackContext context)
    {
        if (GameManager_MumSweeper.instance._canPlay && _canMove)
            StartCoroutine(Move(Vector3.right));
    }

    IEnumerator Move(Vector3 dir)
    {
        _canMove = false;
        var newPos = transform.position + dir;
        if(newPos.x <= _clampMax && newPos.x >= _clampMin && newPos.z <= _clampMax && newPos.z >= _clampMin)
            transform.position = transform.position + dir;
        yield return new WaitForSeconds(0.2f);
        _canMove = true;
    }

    //Event pour la touche d'action
    public void OnAction(InputAction.CallbackContext context)
    {
        _actionInput = context.action.triggered;
    }

    //Event pour l'action map Tuto (se mettre pret pour lancer le jeu)
    public void OnReady(InputAction.CallbackContext context)
    {
        Tutorials.instance.ReadyChecker(_isPlayer1);
    }

    //Passer de l'action map Tuto (get ready) à l'action map de jeu
    public void ChangeActionMap()
    {
        if (_isPlayer1)
        {
            _playerInput.SwitchCurrentActionMap("GridPlayer1");
        }
        else
        {
            _playerInput.SwitchCurrentActionMap("GridPlayer2");
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (GameManager_MumSweeper.instance._canPlay)
        {
            if (_actionInput)
            {
                StartDig();
            }
        }
    }


    private void StartDig()
    {
        if (_canDig)
        {
            StartCoroutine(Dig());
        }
    }

    public void GetTrapped()
    {
        StartCoroutine(Trapped());
    }

    IEnumerator Trapped()
    {
        _stunIcon.SetActive(true);
        _canMove = false;
        _canDig = false;
        yield return new WaitForSeconds(_stunDuration);
        _stunIcon.SetActive(false);
        _canMove = true;
        _canDig = true;
    }

    IEnumerator Dig()
    {
        _digIcon.SetActive(true);
        _canMove = false;
        _canDig = false;
       
        Physics.Raycast(transform.position, Vector3.down, out down, 2f);
        //Play Anim
        yield return new WaitForSeconds(0.5f);
        _digIcon.SetActive(false);
        _canMove = true;
        _canDig = true;
        if (down.transform.GetComponent<Cells>() != null)
            down.collider.GetComponent<Cells>().RevealTile(_isPlayer1, this);
    }
}
