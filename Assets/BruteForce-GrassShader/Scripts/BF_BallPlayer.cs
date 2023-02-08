using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

public class BF_BallPlayer : MonoBehaviour
{
    public Camera cam;
    public float speed;
    public Rigidbody rb1, rb2;
    private Quaternion camRot;
    private Vector3 moveDirection_P1, moveDirection_P2;
    private Vector3 inputDirection_P1, inputDirection_P2;
    public Transform _tangueP1, _tangueP2;

    // Start is called before the first frame update
    void Start()
    {     
        if(cam == null)
        {
            cam = Camera.main;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        inputDirection_P1 = Vector3.zero;
        inputDirection_P2 = Vector3.zero;


#if ENABLE_INPUT_SYSTEM
        if (Keyboard.current.qKey.isPressed || Keyboard.current.aKey.isPressed)
        {
            inputDirection += new Vector3(0, 0, 1);
        }
        if (Keyboard.current.dKey.isPressed)
        {
            inputDirection += new Vector3(0, 0, -1);
        }
        if (Keyboard.current.wKey.isPressed || Keyboard.current.zKey.isPressed)
        {
            inputDirection += new Vector3(1, 0, 0);
        }
        if (Keyboard.current.sKey.isPressed)
        {
            inputDirection += new Vector3(-1, 0, 0);
        }
#else

        //PLAYER 1
        if (Input.GetKey(KeyCode.Q))
        {
            inputDirection_P1 += new Vector3(0, 0, 1);
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputDirection_P1 += new Vector3(0, 0, -1);
        }
        if (Input.GetKey(KeyCode.Z))
        {
            inputDirection_P1 += new Vector3(1, 0, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputDirection_P1 += new Vector3(-1, 0, 0);
        }


        //PLAYER 2
        if (Input.GetKey(KeyCode.K))
        {
            inputDirection_P2 += new Vector3(0, 0, 1);
        }
        if (Input.GetKey(KeyCode.M))
        {
            inputDirection_P2 += new Vector3(0, 0, -1);
        }
        if (Input.GetKey(KeyCode.O))
        {
            inputDirection_P2 += new Vector3(1, 0, 0);
        }
        if (Input.GetKey(KeyCode.L))
        {
            inputDirection_P2 += new Vector3(-1, 0, 0);
        }
#endif
        MoveBallS();
    }

    private void MoveBallS()
    {
        camRot = Quaternion.AngleAxis(cam.transform.rotation.eulerAngles.y, Vector3.up);
        moveDirection_P1 = camRot * new Vector3(Mathf.Clamp(inputDirection_P1.x * 5, -1, 1), 0, Mathf.Clamp(inputDirection_P1.z * 5, -1, 1));
        rb1.AddTorque(moveDirection_P1 * speed);

        camRot = Quaternion.AngleAxis(cam.transform.rotation.eulerAngles.y, Vector3.up);
        moveDirection_P2 = camRot * new Vector3(Mathf.Clamp(inputDirection_P2.x * 5, -1, 1), 0, Mathf.Clamp(inputDirection_P2.z * 5, -1, 1));
        rb2.AddTorque(moveDirection_P2 * speed);
    }




    private void Update()
    {
        _tangueP1.position = new Vector3(rb1.transform.position.x, rb1.transform.position.y, rb1.transform.position.z);
        _tangueP2.position = new Vector3(rb2.transform.position.x, rb2.transform.position.y, rb2.transform.position.z);

        _tangueP1.LookAt(rb1.transform.position + rb1.velocity);
        _tangueP2.LookAt(rb2.transform.position + rb2.velocity);

    }
}
