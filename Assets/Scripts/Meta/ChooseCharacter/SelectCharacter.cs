using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace META
{
    public class SelectCharacter : MonoBehaviour
    {

        [SerializeField] KeyCode _right, _left, _up, _down, _action, _action2;
        [SerializeField] SelectCharacter _otherCharacter;

        [SerializeField] string _horizontalInput, _verticalInput, _actionInput, _altActionInput;
        [SerializeField] float _startTime, _nextTime;

        [SerializeField] int _currentPlayer;
        [SerializeField] Animator _animator;
     


        public bool _isPlayer1;
        public bool _momChoosed;

        void Start()
        {
            _nextTime = Time.time + 2;
          
            TouchBinding();
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


        // Update is called once per frame
        void Update()
        {

        



#if UNITY_EDITOR
            string[] joystickNames = Input.GetJoystickNames();
            for (int i = 0; i < joystickNames.Length; i++)
            {
                Debug.Log("Manette " + (i + 1) + " : " + joystickNames[i]);
            }
#endif


        }

        void TouchBinding()
        {
            if (_isPlayer1)
            {
                _horizontalInput = "Horizontal_P1";
                _verticalInput = "a";
                _actionInput = "Fire_P1";
                _altActionInput = "Fire_Alt_P1";
            }
            else
            {
                _horizontalInput = "Horizontal_P2";
                _verticalInput = "Vertical_P2";
                _actionInput = "Fire_P2";
                _altActionInput = "Fire_Alt_P2";
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
            
        }

        IEnumerator SelectMom()
        {
            while (true)
            {
                if (!_momChoosed)
                {
                    if (Input.GetAxis(_horizontalInput) > 0f) //NextMom
                    {
                        Debug.Log("Input:" + _horizontalInput);

                        ChangeStateMomSelection(false, false, _currentPlayer);

                        _currentPlayer++;
                        Placement();

                        ChangeStateMomSelection(true, false, _currentPlayer);

                        yield return new WaitForSeconds(0.2f);
                    }
                    else if (Input.GetAxis(_horizontalInput) < 0f) //PreviousMom
                    {
                        Debug.Log("Input:" + _horizontalInput);

                        ChangeStateMomSelection(false, false, _currentPlayer);

                        _currentPlayer--;
                        Placement();

                        ChangeStateMomSelection(true, false, _currentPlayer);
                        yield return new WaitForSeconds(0.2f);
                    }
                    else if (Input.GetButtonDown(_actionInput)) //SelectMom
                    {
                        if ((_otherCharacter._currentPlayer != _currentPlayer) || ( (_otherCharacter._currentPlayer == _currentPlayer) && !_otherCharacter._momChoosed))
                        {
                            Debug.Log("1_OK ");
                            _momChoosed = true; 
                            MomReferencer.instance.ShowButton();

                            WichMom();
                        }
                        else
                        {
                            _animator.SetTrigger("CantSelect");
                            //SFX Pas Bon
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
                    if (Input.GetButtonDown(_altActionInput)) //UnselectMom
                    {
                        _momChoosed = false;
                        _animator.SetTrigger("UnSelect");
                        WichMom();
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
                if (_momChoosed)
                {
                    if (_currentPlayer <= MomReferencer.instance._datas.Length - 1)
                    {

                        ChangeStateMomSelection(false, true, _currentPlayer);

                        MetaGameManager.instance._player1 = MomReferencer.instance._datas[_currentPlayer];
                        _animator.SetTrigger("Select");
                        Debug.Log("1_BON" + _currentPlayer + " " + MomReferencer.instance._datas.Length);
                    }
                    else
                    {
                        _animator.SetTrigger("CantSelect");
                        _momChoosed = false;
                        Debug.Log("1_NOT" + _currentPlayer + " " + MomReferencer.instance._datas.Length);
                    }
                    
                }

                
            }
            else
            {
                if (_momChoosed)
                {
                    if (_currentPlayer <= MomReferencer.instance._datas.Length - 1)
                    {
                        ChangeStateMomSelection(false, true, _currentPlayer);

                        MetaGameManager.instance._player2 = MomReferencer.instance._datas[_currentPlayer];
                        _animator.SetTrigger("Select");
                        Debug.Log("2_BON" + _currentPlayer + " " + MomReferencer.instance._datas.Length);
                    }
                    else
                    {
                        _animator.SetTrigger("CantSelect");
                        _momChoosed = false;
                        Debug.Log("2_NOT" + _currentPlayer + " " + MomReferencer.instance._datas.Length);
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
  

        

    } 

}
