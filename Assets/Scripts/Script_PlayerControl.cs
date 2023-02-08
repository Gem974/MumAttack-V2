using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_PlayerControl : MonoBehaviour
{
    [Header ("Controller")]
    [SerializeField] private bool _isP1WithBoard;
    [SerializeField] private bool _isP2WithBoard;
    [SerializeField] private bool _useAll;

    [Header("References")]
    [SerializeField] private InputReceiver _IR;
    [SerializeField] private Rigidbody rb1;
    [SerializeField] private Rigidbody rb2;

    [Header("Models")]
    [SerializeField] private Transform _tangueModelP1;
    [SerializeField] private Transform _tangueModelP2;
    [SerializeField] private Animator _tangueAnimatorP1;
    [SerializeField] private Animator _tangueAnimatorP2;


    [Header("Speeds")]
    [SerializeField] private float _boardSpeed;
    [SerializeField] private float _keyboardSpeed;

    [Header("FX")]
    [SerializeField] private GameObject _grassFX_P1;
    [SerializeField] private GameObject _grassFX_P2;
    [SerializeField] private AudioSource _SFX_P1;
    [SerializeField] private AudioSource _SFX_P2;


    private Camera cam;
    private Quaternion camRot;
    private Vector2 _inputVectorP1 , _inputVectorP2;
    private Vector3 moveDirection_P1, moveDirection_P2;
    private Vector3 inputDirection_P1, inputDirection_P2;

    public bool canMove = false;



    private void Start()
    {
        _IR = FindObjectOfType<InputReceiver>();
        if(_IR == null){
            Debug.LogWarning("IR NOT FOUND");
        }
        cam = Camera.main;

        if (!_isP1WithBoard && !_isP1WithBoard && !_useAll)
        {
            _useAll = true;
        }
    }


    void FixedUpdate()
    {

        if (canMove)
        {

            //BoardMove
            if (_isP1WithBoard && _isP2WithBoard || _useAll)
            {
                GetBoardInput();
                BoardMove();
            }

            //KeyboardMove
            if (!_isP1WithBoard && !_isP2WithBoard || _useAll)
            {
                GetKeyboardInput();
                KeyboardMove();
            }

            //Tangue Position & Rotation
            TangueModelRotPos();

        }

        if (!canMove)
        {
            rb1.velocity = Vector3.zero;
            rb2.velocity = Vector3.zero;
        }
    }

    //KEYBOARD
    private void TangueModelRotPos()
    {
        _tangueModelP1.position = new Vector3(rb1.transform.position.x, rb1.transform.position.y, rb1.transform.position.z);
        _tangueModelP2.position = new Vector3(rb2.transform.position.x, rb2.transform.position.y, rb2.transform.position.z);
        _tangueModelP1.LookAt(rb1.transform.position + rb1.velocity);
        _tangueModelP2.LookAt(rb2.transform.position + rb2.velocity);
    }

    private void GetKeyboardInput()
    {
        inputDirection_P1 = Vector3.zero;
        inputDirection_P2 = Vector3.zero;

        //PLAYER 1
        if (Input.GetKey(KeyCode.Q)) inputDirection_P1 += new Vector3(0, 0, 1);
        if (Input.GetKey(KeyCode.D)) inputDirection_P1 += new Vector3(0, 0, -1);
        if (Input.GetKey(KeyCode.Z)) inputDirection_P1 += new Vector3(1, 0, 0);
        if (Input.GetKey(KeyCode.S)) inputDirection_P1 += new Vector3(-1, 0, 0);

        //PLAYER 2
        if (Input.GetKey(KeyCode.K)) inputDirection_P2 += new Vector3(0, 0, 1);
        if (Input.GetKey(KeyCode.M)) inputDirection_P2 += new Vector3(0, 0, -1);
        if (Input.GetKey(KeyCode.O)) inputDirection_P2 += new Vector3(1, 0, 0);
        if (Input.GetKey(KeyCode.L)) inputDirection_P2 += new Vector3(-1, 0, 0);

    }

    private void KeyboardMove()
    {

        if (!_isP1WithBoard || _useAll)
        {
            camRot = Quaternion.AngleAxis(cam.transform.rotation.eulerAngles.y, Vector3.up);
            moveDirection_P1 = camRot * new Vector3(Mathf.Clamp(inputDirection_P1.x * 2, -1, 1), 0, Mathf.Clamp(inputDirection_P1.z * 2, -1, 1));
            rb1.AddTorque(moveDirection_P1.normalized * _keyboardSpeed);

            if (rb1.velocity.magnitude < 1.5f)//Walk
            {
                _grassFX_P1.SetActive(false);
                _tangueAnimatorP1.SetBool("RUN", false);
                // rb1.drag = 10;
            }
            else//____________________________//Run
            {
                _grassFX_P1.SetActive(true);
                _tangueAnimatorP1.SetBool("RUN", true);

                //rb1.drag = 0.1f;
            }

            //PaySound SFX_1_GrassSFX
            _SFX_P1.volume = rb1.velocity.magnitude/7;
        }

        if (!_isP2WithBoard || _useAll)
        {
            camRot = Quaternion.AngleAxis(cam.transform.rotation.eulerAngles.y, Vector3.up);
            moveDirection_P2 = camRot * new Vector3(Mathf.Clamp(inputDirection_P2.x * 2, -1, 1), 0, Mathf.Clamp(inputDirection_P2.z * 2, -1, 1));
            rb2.AddTorque(moveDirection_P2.normalized * _keyboardSpeed);

            if (rb2.velocity.magnitude < 1.5f)//Walk
            {
                _grassFX_P2.SetActive(false);
                _tangueAnimatorP2.SetBool("RUN", false);
                //rb2.drag = 10;
            }
            else//____________________________//Run
            {
                _grassFX_P2.SetActive(true);
                _tangueAnimatorP2.SetBool("RUN", true);
                //rb2.drag = 0.1f;
            }

            //PaySound SFX_1_GrassSFX
            _SFX_P2.volume = rb2.velocity.magnitude/7;
        }
    }


    //BOARD
    public void GetBoardInput()
    {
        _inputVectorP1.x = _IR._inputsP1.x;
        _inputVectorP1.y = -_IR._inputsP1.y;

        _inputVectorP2.x = _IR._inputsP2.x;
        _inputVectorP2.y = -_IR._inputsP2.y;
    }

    public void BoardMove()
    {
        //Player 1 BoardMove
        if (_isP1WithBoard || _useAll)
        {
            Vector3 vectorDirection = new Vector3(_inputVectorP1.x, 0, _inputVectorP1.y);
            rb1.AddTorque(vectorDirection.normalized * _boardSpeed);

            if (rb1.velocity.magnitude < 1.5f)//Walk
            {
                _grassFX_P1.SetActive(false);
                _tangueAnimatorP1.SetBool("RUN", false);
                // rb1.drag = 10;
            }
            else//____________________________//Run
            {
                _grassFX_P1.SetActive(true);
                _tangueAnimatorP1.SetBool("RUN", true);

                //rb1.drag = 0.1f;
            }

            //PaySound SFX_1_GrassSFX
            _SFX_P1.volume = rb1.velocity.magnitude / 7;
        }

        //Player 2 BoardMove
        if (_isP2WithBoard || _useAll)
        {
            Vector3 vectorDirection = new Vector3(_inputVectorP2.x, 0, _inputVectorP2.y);
            rb2.AddTorque(vectorDirection.normalized * _boardSpeed);

            if (rb2.velocity.magnitude < 1.5f)//Walk
            {
                _grassFX_P2.SetActive(false);
                _tangueAnimatorP2.SetBool("RUN", false);
                // rb2.drag = 10;
            }
            else//____________________________//Run
            {
                _grassFX_P2.SetActive(true);
                _tangueAnimatorP2.SetBool("RUN", true);

                //rb2.drag = 0.1f;
            }

            //PaySound SFX_1_GrassSFX
            _SFX_P2.volume = rb2.velocity.magnitude / 7;
        }
    }

    public void ActiveCanMove()
    {
        canMove = true;
    }

    public void DesactiveCanMove()
    {
        canMove = false;
    }

}
