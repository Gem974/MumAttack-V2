using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.InputSystem.Users;
using Spine.Unity;
using META;

[RequireComponent(typeof(PlayerInput))]
public class PlayerBehaviour : MonoBehaviour
{
    [Header("Player Behaviour")]
    [SerializeField] protected PlayerInput _playerInput;
    [SerializeField] protected string _actionMapP1Name = "Player1", _actionMapP2Name = "Player2";
    public bool _isPlayer1;
    [SerializeField] protected Vector2 _moveInput = Vector2.zero;
    public bool _actionNewInput = false;

   
   

    //Event pour les touches de déplacement 
    public virtual void OnMove(InputAction.CallbackContext context)
    {
        //On se contente de récupérer le Vecteur2 des touches de déplacement
        _moveInput = context.ReadValue<Vector2>();
        
    }

    //Event pour la touche d'action
    public virtual void OnAction(InputAction.CallbackContext context)
    {
        if (GameManagerBehaviour.instancePrime._canPlay)
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

    public virtual void OnPause(InputAction.CallbackContext context)
    {
        
        if (PauseGame.instance._pausable)
        {
            if (PauseGame.instance._canPause)
            {
                PauseGame.instance.ShowPause(true);
            }
            else
            {
                PauseGame.instance.ShowPause(false);
            }

        }
       
    }

    //Passer de l'action map Tuto (get ready) à l'action map de jeu
    public virtual void ChangeActionMap()
    {
        if (_isPlayer1)
        {
            _playerInput.SwitchCurrentActionMap(_actionMapP1Name);
        }
        else
        {
            _playerInput.SwitchCurrentActionMap(_actionMapP2Name);
        }
    }

    public virtual void ForceController()
    {
        _playerInput.user.UnpairDevices();
        if (_isPlayer1)
        {
            if (META.MetaGameManager.instance._device1 != null)
                InputUser.PerformPairingWithDevice(META.MetaGameManager.instance._device1, user: _playerInput.user);
        }
        else
        {
            if (META.MetaGameManager.instance._device2 != null)
                InputUser.PerformPairingWithDevice(META.MetaGameManager.instance._device2, user: _playerInput.user);
        }
        //Assignation manuelle du clavier (obligatoire vu que partager par tout les joueurs)
        InputUser.PerformPairingWithDevice(Keyboard.current, user: _playerInput.user);
    }
}
