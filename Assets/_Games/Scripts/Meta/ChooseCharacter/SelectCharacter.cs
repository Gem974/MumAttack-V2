using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

namespace META
{
    public class SelectCharacter : MonoBehaviour
    {

        //[SerializeField] KeyCode _right, _left, _up, _down, _action, _action2;
        [SerializeField] SelectCharacter _otherCharacter;

        //[SerializeField] string _horizontalInput, _verticalInput, _actionInput, _altActionInput;
        [SerializeField] float _startTime;

        [SerializeField] int _currentPlayer;
        [SerializeField] Animator _animator;

        [Header("DisplayScreen")]
        [SerializeField] SpriteRenderer _headDisplay;
        [SerializeField] TextMeshProUGUI _nameDisplay;
     


        public bool _isPlayer1;
        public bool _momChoosed;
        public bool _momValidate;

        private Vector2 _moveInput = Vector2.zero;
        private bool _actionNewInput = false;
        private bool _cancelInput = false;
        private PlayerInput _playerInput;

        void Start()
        {
            //Liaison avec le Player Input
            _playerInput = GetComponent<PlayerInput>();
            //Assignation manuelle du clavier (obligatoire vu que partager par tout les joueurs)
            ForceController();
            InputUser.PerformPairingWithDevice(Keyboard.current, user: _playerInput.user);
            _momValidate = false;


            //TouchBinding();
            if (_isPlayer1)
            {
                _currentPlayer = 0;
                ChangeStateMomSelection(true, false, _currentPlayer);
            }
            else
            {
                _currentPlayer = 1;
                ChangeStateMomSelection(true, false, _currentPlayer);
            }
            _momChoosed = false;
            Placement();
            StartCoroutine(SelectMom());
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
            _actionNewInput = context.action.triggered;
        }

        //Event pour la touche cancel
        public void OnCancel(InputAction.CallbackContext context)
        {
            _cancelInput = context.action.triggered;
        }

        public void ForceController()
        {
            _playerInput.user.UnpairDevices();
            if (_isPlayer1)
            {
                if(META.MetaGameManager.instance._device1 != null)
                    InputUser.PerformPairingWithDevice(META.MetaGameManager.instance._device1, user: _playerInput.user);
            }
            else
            {
                if (META.MetaGameManager.instance._device2 != null)
                    InputUser.PerformPairingWithDevice(META.MetaGameManager.instance._device2, user: _playerInput.user);
            }
        }

        // Update is called once per frame
        //        void Update()
        //        {
        //#if UNITY_EDITOR
        //            string[] joystickNames = Input.GetJoystickNames();
        //            for (int i = 0; i < joystickNames.Length; i++)
        //            {
        //                Debug.Log("Manette " + (i + 1) + " : " + joystickNames[i]);
        //            }
        //#endif


        //        }

        //void TouchBinding()
        //{
        //    if (_isPlayer1)
        //    {
        //        _horizontalInput = "Horizontal_P1";
        //        _verticalInput = "a";
        //        _actionInput = "Fire_P1";
        //        _altActionInput = "Fire_Alt_P1";
        //    }
        //    else
        //    {
        //        _horizontalInput = "Horizontal_P2";
        //        _verticalInput = "Vertical_P2";
        //        _actionInput = "Fire_P2";
        //        _altActionInput = "Fire_Alt_P2";
        //    }


        //}

        void Placement()
        {
            if (_currentPlayer > MomReferencer.instance._moms.Length - 1)
            {
                _currentPlayer = 0;
            }
            else if (_currentPlayer < 0)
            {
                _currentPlayer = MomReferencer.instance._moms.Length - 1;
            }

            transform.position = MomReferencer.instance._moms[_currentPlayer].position;
            transform.rotation = MomReferencer.instance._moms[_currentPlayer].rotation;

            
        }

      

        IEnumerator SelectMom()
        {
            while (true)
            {
                if (!_momChoosed)
                {
                    if (/*Input.GetAxis(_horizontalInput) > 0f*/ _moveInput.x > 0f) //NextMom
                    {
                        //Debug.Log("Input:" + _horizontalInput);

                        ChangeStateMomSelection(false, false, _currentPlayer);

                        _currentPlayer++;
                        Placement();

                        ChangeStateMomSelection(true, false, _currentPlayer);

                        yield return new WaitForSeconds(0.2f);
                    }
                    else if (/*Input.GetAxis(_horizontalInput) < 0f*/ _moveInput.x < 0f) //PreviousMom
                    {
                        //Debug.Log("Input:" + _horizontalInput);

                        ChangeStateMomSelection(false, false, _currentPlayer);

                        _currentPlayer--;
                        Placement();

                        ChangeStateMomSelection(true, false, _currentPlayer);
                        yield return new WaitForSeconds(0.2f);
                    }
                    else if (/*Input.GetButtonDown(_actionInput)*/ _actionNewInput) //SelectMom
                    {
                        if ((_otherCharacter._currentPlayer != _currentPlayer) || ( (_otherCharacter._currentPlayer == _currentPlayer) && !_otherCharacter._momChoosed))
                        {
                            Debug.Log("1_OK ");
                            _momChoosed = true; 

                            WichMom();
                            MomReferencer.instance.ShowButton();
                            //DisplayScreenInfo();
                        }
                        else
                        {
                            //_animator.SetTrigger("CantSelect");
                            //_nameDisplay.text = "Select a Mom";
                            //_headDisplay.gameObject.SetActive(false);
                            ////SFX Pas Bon
                        }
                        yield return null;
                    }
                    else
                    {
                        yield return null;
                    }

                }
                else
                {
                    if (/*Input.GetButtonDown(_altActionInput)*/ _cancelInput) //UnselectMom
                    {
                        _momChoosed = false;
                        _animator.SetTrigger("UnSelect");
                        WichMom();
                        _nameDisplay.text = "Select a Mom";
                        _headDisplay.gameObject.SetActive(false);
                        MomReferencer.instance.ShowButton();
                        

                    }
                    yield return null;

                } 
                    
            }
        }

        void WichMom()
        {
            if (_isPlayer1)
            {
                if (_momChoosed) //J1
                {
                    if (_currentPlayer <= MomReferencer.instance._datas.Length - 1)
                    {
                        DisplayScreenInfo();
                        ChangeStateMomSelection(false, true, _currentPlayer);

                        MetaGameManager.instance._player1 = MomReferencer.instance._datas[_currentPlayer];
                        _animator.SetTrigger("Select");
                        Debug.Log("1_BON" + _currentPlayer + " " + MomReferencer.instance._datas.Length);
                        _momValidate = true;
                    }
                    else
                    {
                        ChangeStateMomSelection(false, false, _currentPlayer);
                        _animator.SetTrigger("CantSelect");
                        
                        _momChoosed = false;
                        _momValidate = false;
                        Debug.Log("1_NOT" + _currentPlayer + " " + MomReferencer.instance._datas.Length);

                        _nameDisplay.text = "Select a Mom";
                        _headDisplay.gameObject.SetActive(false);
                    }
                    
                }

                
            }
            else
            {
                if (_momChoosed) //J2
                {
                    if (_currentPlayer <= MomReferencer.instance._datas.Length - 1)
                    {
                        DisplayScreenInfo();
                        ChangeStateMomSelection(false, true, _currentPlayer);

                        MetaGameManager.instance._player2 = MomReferencer.instance._datas[_currentPlayer];
                        _animator.SetTrigger("Select");

                        _momValidate = true;
                        Debug.Log("2_BON" + _currentPlayer + " " + MomReferencer.instance._datas.Length);
                    }
                    else
                    {
                        ChangeStateMomSelection(false, false, _currentPlayer);
                        _animator.SetTrigger("CantSelect");
                        _momChoosed = false;
                        _momValidate = false;
                        Debug.Log("2_NOT" + _currentPlayer + " " + MomReferencer.instance._datas.Length);

                        _nameDisplay.text = "Select a Mom";
                        _headDisplay.gameObject.SetActive(false);
                        
                    }
                }

                
            }

            
        }

        public void ChangeStateMomSelection(bool show, bool selected, int currentSelection)
        {

            MomSelecter _momSelecter = MomReferencer.instance._moms[currentSelection].GetComponent<MomSelecter>();


            if (selected)
            {
                _momSelecter.OnChangeState(MomSelecter.MomSelecterState.Selected);
                return;
            }


            if (show)
            {
                _momSelecter.OnChangeState(MomSelecter.MomSelecterState.Hover);

            }
            else if (!show && currentSelection != _otherCharacter._currentPlayer)
            {
                _momSelecter.OnChangeState(MomSelecter.MomSelecterState.Default);
            }



        }

        public void DisplayScreenInfo()
        {
                Debug.Log("Jsuis rentré zbi");
                _nameDisplay.text = MomReferencer.instance._datas[_currentPlayer]._name;
                _headDisplay.gameObject.SetActive(true);
                _headDisplay.sprite = MomReferencer.instance._datas[_currentPlayer]._goodHead;
            //if (_currentPlayer <= MomReferencer.instance._datas.Length - 1)
            //{
            //}
            
            
        }
  

        

    } 

}
