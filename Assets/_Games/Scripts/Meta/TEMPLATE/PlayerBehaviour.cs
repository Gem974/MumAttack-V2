using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.InputSystem.Users;

[RequireComponent(typeof(PlayerInput))]
public class PlayerBehaviour : MonoBehaviour
{
    [Header("Player Behaviour")]
    [SerializeField] protected PlayerInput _playerInput;
    [SerializeField] protected string _actionMapP1Name = "Player1", _actionMapP2Name = "Player2";
    [SerializeField] protected bool _isPlayer1;
    private Vector2 _moveInput = Vector2.zero;
    private bool _actionNewInput = false;

    public virtual void PairingPlayerInput()
    {
        //Liaison avec le Player Input
        _playerInput = GetComponent<PlayerInput>();
        //Assignation manuelle du clavier (obligatoire vu que partager par tout les joueurs)
        InputUser.PerformPairingWithDevice(Keyboard.current, user: _playerInput.user);
    }
    public virtual void Kabo()
    {
        print("OUI");
    }

    //Event pour les touches de déplacement 
    public virtual void OnMove(InputAction.CallbackContext context)
    {
        //On se contente de récupérer le Vecteur2 des touches de déplacement
        if (GameManager_IromMum.instance._canPlay)
        {
            _moveInput = context.ReadValue<Vector2>();
        }
    }

    //Event pour la touche d'action
    public virtual void OnAction(InputAction.CallbackContext context)
    {
        if (GameManager_IromMum.instance._canPlay)
        {
            //Equivaut a un GetKeyDown (appuie sur le bouton)
            context.action.started += context =>
            {
                _actionNewInput = true;
            };

            //Equivaut a un GetKeyUp (quand on relache le bouton)
            context.action.canceled += context =>
            {
                
                _actionNewInput = false;
            };
        }
    }

    //Event pour l'action map Tuto (se mettre pret pour lancer le jeu)
    public virtual void OnReady(InputAction.CallbackContext context)
    {
        Tutorials.instance.ReadyChecker(_isPlayer1);
    }

    //Passer de l'action map Tuto (get ready) à l'action map de jeu
    public virtual void ChangeActionMap()
    {
        if (_isPlayer1)
        {
            _playerInput.SwitchCurrentActionMap("_actionMapP1");
        }
        else
        {
            _playerInput.SwitchCurrentActionMap("_actionMapP2");
        }
    }
}
