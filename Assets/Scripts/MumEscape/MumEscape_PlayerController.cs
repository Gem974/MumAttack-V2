using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MumEscape_PlayerController : MonoBehaviour
{
    //Variables
    public string _name;
    public bool _isPlayer1;
    [SerializeField] KeyCode _Kright, _Kleft, _Kup, _Kdown, _Kaction;
    public RaycastHit _up, _down, _left, _right;
    public float _rayDistance = 2;
    public Transform _mesh;
    private int _clampX, _clampZ;

    private void Start()
    {
        TouchBinding();
        var level = FindObjectOfType<MumEscape_Level>();
        _clampX = level._large;
        _clampZ = level._lenght;
    }

    // Update is called once per frame
    void Update()
    {
        //RayCast
        Physics.Raycast(transform.position, Vector3.forward, out _up, _rayDistance);
        Physics.Raycast(transform.position, Vector3.back, out _down, _rayDistance);
        Physics.Raycast(transform.position, Vector3.right, out _right, _rayDistance);
        Physics.Raycast(transform.position, Vector3.left, out _left, _rayDistance);

        //Debug RayCast
        Debug.DrawRay(transform.position, Vector3.forward * _rayDistance, Color.red);
        Debug.DrawRay(transform.position, Vector3.back * _rayDistance, Color.blue);
        Debug.DrawRay(transform.position, Vector3.right * _rayDistance, Color.green);
        Debug.DrawRay(transform.position, Vector3.left * _rayDistance, Color.yellow);

        //PLayer can't move if the light is open
        if (!MumEscape_GameManager.Instance._isLightOpen && !MumEscape_GameManager.Instance._isGameOver)
        {
            //Movement
            if (Input.GetKeyDown(_Kup))
            {

                if (transform.position.z < _clampZ -1) 
                {
                    if(_up.collider != null)
                    {
                        if(_up.collider.CompareTag("Obstacle") == false)
                        {

                            Orientation(_name,  "up");
                            transform.position = transform.position + new Vector3(0, 0, 1);
                            MumEscape_GameManager.Instance.AddCounter(5, gameObject.name);
                        }
                    }
                    else
                    {
                        Orientation(_name, "up");
                        transform.position = transform.position + new Vector3(0, 0, 1);
                        MumEscape_GameManager.Instance.AddCounter(5, gameObject.name);
                    }
                } 
                
            }
            else if(Input.GetKeyDown(_Kdown))
            {
                if (transform.position.z > 0)
                {
                    if (_down.collider != null)
                    {
                        if (_down.collider.CompareTag("Obstacle") == false)
                        {
                            Orientation(_name, "down");
                            transform.position = transform.position + new Vector3(0, 0, -1);
                            MumEscape_GameManager.Instance.AddCounter(5, gameObject.name);
                        }
                    }
                    else
                    {
                        Orientation(_name, "down");
                        transform.position = transform.position + new Vector3(0, 0, -1);
                        MumEscape_GameManager.Instance.AddCounter(5, gameObject.name);
                    }
                }
            }
            else if (Input.GetKeyDown(_Kright))
            {
                if (transform.position.x < _clampX -1)
                {
                    if (_right.collider != null)
                    {
                        if (_right.collider.CompareTag("Obstacle") == false)
                        {
                            Orientation(_name, "right");
                            transform.position = transform.position + new Vector3(1, 0, 0);
                            MumEscape_GameManager.Instance.AddCounter(5, gameObject.name);
                        }
                    }
                    else
                    {
                        Orientation(_name, "right");
                        transform.position = transform.position + new Vector3(1, 0, 0);
                        MumEscape_GameManager.Instance.AddCounter(5, gameObject.name);
                    }
                }
            }
            else if (Input.GetKeyDown(_Kleft))
            {
                if (transform.position.x > 0)
                {
                    if (_left.collider != null)
                    {
                        if (_left.collider.CompareTag("Obstacle") == false)
                        {
                            Orientation(_name, "left");
                            transform.position = transform.position + new Vector3(-1, 0, 0);
                            MumEscape_GameManager.Instance.AddCounter(5, gameObject.name);
                        }
                    }
                    else
                    {
                        Orientation(_name, "left");
                        transform.position = transform.position + new Vector3(-1, 0, 0);
                        MumEscape_GameManager.Instance.AddCounter(5, gameObject.name);
                    }
                }
            }

            //Action button : Open the light and take 1 annoyed counter
            if(Input.GetKeyDown(_Kaction))
            {
                MumEscape_GameManager.Instance.ActiveLight(1.5f);
                MumEscape_GameManager.Instance.AddCounter(33, gameObject.name);
            }
        }
    }

    public void Orientation(string name, string orientation)
    {
        if (name.Contains("1"))
        {
            if (orientation.Contains("up"))
            {
                _mesh.eulerAngles = new Vector3(0f, 90f, 0f);
            }
            else if(orientation.Contains("down"))
            {
                _mesh.eulerAngles = new Vector3(0f, -90f, 0f);
            }
            else if (orientation.Contains("right"))
            {
                _mesh.eulerAngles = new Vector3(0f, 180f, 0f);
            }
            else if (orientation.Contains("left"))
            {
                _mesh.eulerAngles = new Vector3(0f, 0f, 0f);
            }
        }
        else
        {
            if (orientation.Contains("up"))
            {
                _mesh.eulerAngles = new Vector3(0f, -90f, 0f);
            }
            else if (orientation.Contains("down"))
            {
                _mesh.eulerAngles = new Vector3(0f, 90f, 0f);
            }
            else if (orientation.Contains("right"))
            {
                _mesh.eulerAngles = new Vector3(0f, 0f, 0f);
            }
            else if (orientation.Contains("left"))
            {
                _mesh.eulerAngles = new Vector3(0f, 180f, 0f);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Toy"))
        {
            other.gameObject.GetComponent<MumEscape_Toys>().PlaySFX();
            MumEscape_CameraShake.Instance.Shake(0.75f, 0.4f, 50f);
            MumEscape_GameManager.Instance.AddCounter(33, gameObject.name);
        }

        if (other.gameObject.CompareTag("Goal"))
        {
            if(gameObject.name.Contains("1"))
                MumEscape_GameManager.Instance.GameOver("Player 1");
            else
                MumEscape_GameManager.Instance.GameOver("Player 2");
        }
    }

    void TouchBinding()
    {
        if (_isPlayer1)
        {
            _Kright = KeyCode.D;
            _Kleft = KeyCode.Q;
            _Kup = KeyCode.Z;
            _Kdown = KeyCode.S;
            _Kaction = KeyCode.A;
        }
        else
        {
            _Kright = KeyCode.RightArrow;
            _Kleft = KeyCode.LeftArrow;
            _Kup = KeyCode.UpArrow;
            _Kdown = KeyCode.DownArrow;
            _Kaction = KeyCode.RightControl;
        }
    }
}
