using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace META
{
    public class SelectCharacter : MonoBehaviour
    {

        [SerializeField] KeyCode _right, _left, _up, _down, _action;
        [SerializeField] SelectCharacter _otherCharacter;

        [SerializeField] int _currentPlayer;
        [SerializeField] Animator _animator;
     


        public bool _isPlayer1;
        public bool _momChoosed;

        void Start()
        {
            
            TouchBinding();
            if (_isPlayer1)
            {
                _currentPlayer = 0;
            }
            else
            {
                _currentPlayer = 1;
            }
            _momChoosed = false;
            Placement();


        }


        // Update is called once per frame
        void Update()
        {
            MomSelection();

            
            
        }

        void TouchBinding()
        {
            if (_isPlayer1)
            {
                _right = KeyCode.D;
                _left = KeyCode.Q;
                _up = KeyCode.Z;
                _down = KeyCode.S;
                _action = KeyCode.A;
            }
            else
            {
                _right = KeyCode.RightArrow;
                _left = KeyCode.LeftArrow;
                _up = KeyCode.UpArrow;
                _down = KeyCode.DownArrow;
                _action = KeyCode.RightControl;
            }
        }

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

        void MomSelection()
        {
            if (!_momChoosed)
            {
                if (Input.GetKeyDown(_right)) //NextMom
                {
                    _currentPlayer++;
                    Placement();
                }
                else if (Input.GetKeyDown(_left)) //PreviousMom
                {
                    _currentPlayer--;
                    Placement();
                }
                else if (Input.GetKeyDown(_action)) //SelectMom
                {
                    _momChoosed = true;
                    MomReferencer.instance.ShowButton();
                    
                    WichMom();
                }
            }
            else
            {
                if (Input.GetKeyDown(_action)) //UnselectMom
                {
                    _momChoosed = false;
                    _animator.SetTrigger("UnSelect");
                    WichMom();
                    MomReferencer.instance.ShowButton();
                }
            }
        }
        void WichMom()
        {
            if (_isPlayer1)
            {
                if (_momChoosed)
                {
                    switch (_currentPlayer)
                    {
                        case 0: // Aurelie
                            MetaGameManager.instance._player1 = MomReferencer.instance._datas[_currentPlayer];
                            CheckSelection();
                            break;
                        case 1: // Rachel
                            MetaGameManager.instance._player1 = MomReferencer.instance._datas[_currentPlayer];
                            CheckSelection();
                            break;
                        case 2: // WIP Uncomment all lines below when the new mom is implemented
                            //MetaGameManager.instance._player1 = MomReferencer.instance._datas[_currentPlayer];
                            //CheckSelection();
                            _animator.SetTrigger("CantSelect"); // Delete this line when the new mom is implemented 
                            _momChoosed = false; // Delete this line when the new mom is implemented 
                            break;
                        case 3: // WIP Uncomment all lines below when the new mom is implemented
                            //MetaGameManager.instance._player1 = MomReferencer.instance._datas[_currentPlayer];
                            //CheckSelection();
                            _animator.SetTrigger("CantSelect"); // Delete this line when the new mom is implemented 
                            _momChoosed = false; // Delete this line when the new mom is implemented 

                            break;

                        default:
                            break;
                    } 
                }

                if (MetaGameManager.instance._player1 == MetaGameManager.instance._player2 && _momChoosed == true)
                {
                    _momChoosed = false;
                    MetaGameManager.instance._player1 = null;
                    MomReferencer.instance._playBtn.SetActive(false);
                    //SFX Pas Bon
                    
                    Debug.Log("Deselect J1");

                }
            }
            else
            {
                if (_momChoosed)
                {
                    switch (_currentPlayer)
                    {
                        case 0: // Aurelie
                            MetaGameManager.instance._player2 = MomReferencer.instance._datas[_currentPlayer];
                            CheckSelection();
                            break;
                        case 1: // Rachel
                            MetaGameManager.instance._player2 = MomReferencer.instance._datas[_currentPlayer];
                            CheckSelection();
                            break;
                        case 2: // WIP Uncomment all lines below when the new mom is implemented
                            //MetaGameManager.instance._player2 = MomReferencer.instance._datas[_currentPlayer];
                            //CheckSelection();
                            _animator.SetTrigger("CantSelect"); // Delete this line when the new mom is implemented 
                            _momChoosed = false; // Delete this line when the new mom is implemented 
                            break;
                        case 3: // WIP Uncomment all lines below when the new mom is implemented
                            //MetaGameManager.instance._player2 = MomReferencer.instance._datas[_currentPlayer];
                            //CheckSelection();
                            _animator.SetTrigger("CantSelect"); // Delete this line when the new mom is implemented 
                            _momChoosed = false; // Delete this line when the new mom is implemented 
                            break;
                        
                        default:
                            break;
                    } 
                }

                if (MetaGameManager.instance._player1 == MetaGameManager.instance._player2 && _momChoosed == true)
                {
                    _momChoosed = false;
                    MetaGameManager.instance._player2 = null;
                    MomReferencer.instance._playBtn.SetActive(false);
                    _animator.SetTrigger("UnSelect");
                    //SFX Pas Bon
                    Debug.Log("Deselect J2");
                }
            }

            
        }

        void CheckSelection()
        {
            if (_otherCharacter._currentPlayer == _currentPlayer && _otherCharacter._momChoosed)
            {
                _animator.SetTrigger("CantSelect");
                //SFX Pas Bon
            }
            else if (_otherCharacter._currentPlayer != _currentPlayer)
            {
                _animator.SetTrigger("Select");
            }
        }

        

    } 

}
