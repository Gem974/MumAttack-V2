using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace META
{
    public class SelectCharacter : MonoBehaviour
    {

        [SerializeField] KeyCode _right, _left, _up, _down, _action;

        [SerializeField] int _currentPlayer;
     


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
            Placement();
            _momChoosed = false;


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
                    WichMom();
                }
            }
            else
            {
                if (Input.GetKeyDown(_action)) //UnselectMom
                {
                    _momChoosed = false;
                    WichMom();
                }
            }
        }
        void WichMom()
        {
            if (_isPlayer1)
            {
                switch (_currentPlayer)
                {
                    case 1: // Aurelie
                        MetaGameManager.instance._player1 = MomReferencer.instance._datas[0];
                        break;
                    case 2: // Rachel
                        MetaGameManager.instance._player1 = MomReferencer.instance._datas[1];
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (_currentPlayer)
                {
                    case 1: // Aurelie
                        MetaGameManager.instance._player2 = MomReferencer.instance._datas[0];
                        break;
                    case 2: // Rachel
                        MetaGameManager.instance._player2    = MomReferencer.instance._datas[1];
                        break;
                    default:
                        break;
                }
            }

            if (MetaGameManager.instance._player1 = MetaGameManager.instance._player2)
            {

            }
        }

    } 

}
