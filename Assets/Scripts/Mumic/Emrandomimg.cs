using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Emrandomimg : MonoBehaviour
{
    public GameObject _arrows;
    public GameObject _arrowsP;
    public GameObject _poses;
    [SerializeField] ChronoScript _chrono;
    public Mimique_AudioManager _audioManager;
    [SerializeField] AudioSource _bgMusic;


    public GameObject _arrowPanel;
    public GameObject _victoryPanel;
    public GameObject _p1Panel;
    public GameObject _p2Panel;
    public Text _scoreJ1;
    public Text _scoreJ2;


    public GameObject _posePannel;
    public GameObject _posePannel2;
    public GameObject _textGameOver;
    public GameObject _overlay;

    public Sprite[] _positions;
    public Sprite[] _positionsj2;


    public Sprite[] _directions;
    public Sprite[] _flècheJ1;
    public Sprite[] _flècheJ2;



    public List<KeyCode> _playerKeys;
    public List<KeyCode> _playerKeys2;
    public List<KeyCode> _refKeys;



    public int _keyCount = 0;
    public int _maxKeyCount = 5;

    public int _pointsJ1 = 0;
    public int _pointsJ2 = 0;
  

    private bool _isChecked;
    



    void Start()
    {
        _audioManager = FindObjectOfType<Mimique_AudioManager>();
        _chrono.ChronoStart();
        SpawnArrows();

    }

    void Update()
    {
        if (_chrono._canPlay)
        {
            if (!_isChecked)
                GetPlayerKeys();

            _scoreJ1.text = _pointsJ1.ToString();
            _scoreJ2.text = _pointsJ2.ToString(); 
        }
    }

    void SpawnArrows()
    {
        
        Debug.Log("creation de flèches");
        GameObject go = Instantiate(_arrowsP, _arrowPanel.transform);
        int aleatoire = Random.Range(0, 4);
        go.GetComponent<Image>().sprite = _directions[aleatoire];

        switch (aleatoire)
        {
            case 0:
                
                _refKeys.Add(KeyCode.UpArrow);

                break;

            case 1:

                _refKeys.Add(KeyCode.RightArrow);

                break;

            case 2:

                _refKeys.Add(KeyCode.LeftArrow);

                break;

            case 3:

                _refKeys.Add(KeyCode.DownArrow);

                break;
        }

        _keyCount++;

      

        if (_keyCount < _maxKeyCount)
        {
            SpawnArrows();
        }
       
    }
    

    void GetPlayerKeys()
    {
      

        if (_playerKeys.Count < _refKeys.Count)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {

                foreach (Transform child in _posePannel.transform)
                {
                    GameObject.Destroy(child.gameObject);
                }
                Debug.Log("1");
                _audioManager.PlaySound(1, 1);
                _playerKeys.Add(KeyCode.UpArrow);
                GameObject im = Instantiate(_arrows, _p1Panel.transform);
                im.GetComponent<Image>().sprite = _flècheJ1[0];

                GameObject pose = Instantiate(_poses, _posePannel.transform);
                pose.GetComponent<Image>().sprite = _positions[0];

                CheckPlayerKeys(_playerKeys);
                _audioManager.PlaySound(1, 1);

            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                foreach (Transform child in _posePannel.transform)
                {
                    GameObject.Destroy(child.gameObject);
                }
                Debug.Log("2");
                _audioManager.PlaySound(1, 1);
                _playerKeys.Add(KeyCode.DownArrow);
                GameObject im = Instantiate(_arrows, _p1Panel.transform);
                im.GetComponent<Image>().sprite = _flècheJ1[3];

                GameObject pose = Instantiate(_poses, _posePannel.transform);
                pose.GetComponent<Image>().sprite = _positions[3];

                CheckPlayerKeys(_playerKeys);
                _audioManager.PlaySound(1, 1);


            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                foreach (Transform child in _posePannel.transform)
                {
                    GameObject.Destroy(child.gameObject);
                }
                Debug.Log("3");
                
                _playerKeys.Add(KeyCode.LeftArrow);
                GameObject im = Instantiate(_arrows, _p1Panel.transform);
                im.GetComponent<Image>().sprite = _flècheJ1[2];

                GameObject pose = Instantiate(_poses, _posePannel.transform);
                pose.GetComponent<Image>().sprite = _positions[2];

                CheckPlayerKeys(_playerKeys);
                _audioManager.PlaySound(1, 1);


            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                foreach (Transform child in _posePannel.transform)
                {
                    GameObject.Destroy(child.gameObject);
                }
                Debug.Log("4");
                
                _playerKeys.Add(KeyCode.RightArrow);
                GameObject im = Instantiate(_arrows, _p1Panel.transform);
                im.GetComponent<Image>().sprite = _flècheJ1[1];

                GameObject pose = Instantiate(_poses, _posePannel.transform);
                pose.GetComponent<Image>().sprite = _positions[1];

                CheckPlayerKeys(_playerKeys);
                _audioManager.PlaySound(1, 1);



            }
        }

        if (_playerKeys2.Count < _refKeys.Count)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {

                foreach (Transform child in _posePannel2.transform)
                {
                    GameObject.Destroy(child.gameObject);
                }
                _audioManager.PlaySound(1, 1);
                _playerKeys2.Add(KeyCode.UpArrow);
                GameObject im = Instantiate(_arrows, _p2Panel.transform);
                im.GetComponent<Image>().sprite = _flècheJ2[0];

                GameObject pose = Instantiate(_poses, _posePannel2.transform);
                pose.GetComponent<Image>().sprite = _positionsj2[0];

                CheckPlayerKeys(_playerKeys2);
                _audioManager.PlaySound(1, 2);


            }

            if (Input.GetKeyDown(KeyCode.S))
            {

                foreach (Transform child in _posePannel2.transform)
                {
                    GameObject.Destroy(child.gameObject);
                }
                _audioManager.PlaySound(1, 1);
                _playerKeys2.Add(KeyCode.DownArrow);
                GameObject im = Instantiate(_arrows, _p2Panel.transform);
                im.GetComponent<Image>().sprite = _flècheJ2[3];

                GameObject pose = Instantiate(_poses, _posePannel2.transform);
                pose.GetComponent<Image>().sprite = _positionsj2[3];

                CheckPlayerKeys(_playerKeys2);
                _audioManager.PlaySound(1, 2);


            }

            if (Input.GetKeyDown(KeyCode.Q))
            {

                foreach (Transform child in _posePannel2.transform)
                {
                    GameObject.Destroy(child.gameObject);
                }
                _audioManager.PlaySound(1, 1);
                _playerKeys2.Add(KeyCode.LeftArrow);
                GameObject im = Instantiate(_arrows, _p2Panel.transform);
                im.GetComponent<Image>().sprite = _flècheJ2[2];

                GameObject pose = Instantiate(_poses, _posePannel2.transform);
                pose.GetComponent<Image>().sprite = _positionsj2[2];

                CheckPlayerKeys(_playerKeys2);
                _audioManager.PlaySound(1, 2);



            }

            if (Input.GetKeyDown(KeyCode.D))
            {

                foreach (Transform child in _posePannel2.transform)
                {
                    GameObject.Destroy(child.gameObject);
                }
                _audioManager.PlaySound(1, 1);
                _playerKeys2.Add(KeyCode.RightArrow);
                GameObject im = Instantiate(_arrows, _p2Panel.transform);
                im.GetComponent<Image>().sprite = _flècheJ2[1];

                GameObject pose = Instantiate(_poses, _posePannel2.transform);
                pose.GetComponent<Image>().sprite = _positionsj2[1];

                CheckPlayerKeys(_playerKeys2);
                _audioManager.PlaySound(1, 2);


            }
        }

        if (CheckPlayerKeys(_playerKeys) && _isChecked == false)
        {
            StartCoroutine(Victory());
        }

        if (CheckPlayerKeys(_playerKeys2) && !_isChecked)
        {
            StartCoroutine(Victory());
        }
      
    }

    bool CheckPlayerKeys(List<KeyCode> playerKeys)
    {
        int correctKeys = 0;

        for (int i = 0; i < playerKeys.Count; i++)
        {
            if (playerKeys[i] == _refKeys[i])
            {
                correctKeys++;
                continue;

             
            }
            else
            {
                playerKeys.Clear();
                _audioManager.PlaySound(0, 1);
                _audioManager.PlaySound(0, 2);


                if (playerKeys == _playerKeys)
                {
                    foreach (Transform child in _p1Panel.transform)
                    {


                        
                       
                        

                       // GameObject error = Instantiate(_poses, _posePannel.transform);
                    

                       // error.GetComponent<Image>().sprite = _positions[0];
                      

                        Debug.Log("Supp1");
                        Destroy(child.gameObject);

                    }
                }
                else
                {
                    foreach (Transform child in _p2Panel.transform)
                    {


                       

                        //GameObject error = Instantiate(_poses, _posePannel2.transform);

                     

                       // error.GetComponent<Image>().sprite = _positionsj2[0];


                        Debug.Log("Supp2");
                        Destroy(child.gameObject);

                    }
                }



            }

        }

        if (correctKeys == _refKeys.Count)
        {
            return true;
        }
        else return false;


    }



    IEnumerator Victory()
    {
        _isChecked = true;
        Debug.Log("victory");
        _victoryPanel.SetActive(true);

        if (CheckPlayerKeys(_playerKeys))
        {
            _victoryPanel.GetComponentInChildren<Text>().text = "Player 2 Wins";
            _pointsJ1++;
            
            
        }

        if (CheckPlayerKeys(_playerKeys2))
        {
            _victoryPanel.GetComponentInChildren<Text>().text = "Player 1 Wins";
            _pointsJ2++;
           
        }

        yield return new WaitForSeconds(3f);

        NextGame();
    }




    

    void NextGame()
    {
      

        if (_pointsJ1 < 3 && _pointsJ2 < 3)
        {
          
            _victoryPanel.SetActive(false);
            
            _isChecked = false;
            _keyCount = 0;

            if (_maxKeyCount < 7)
            {
                _maxKeyCount++;
            }
            _refKeys.Clear();
            _playerKeys.Clear();
            _playerKeys2.Clear();

            for (int i = 0; i < _arrowPanel.transform.childCount; i++)
            {
                Destroy(_arrowPanel.transform.GetChild(i).gameObject);


            }

            for (int i = 0; i < _p1Panel.transform.childCount; i++)
            {
                Destroy(_p1Panel.transform.GetChild(i).gameObject);


            }

            for (int i = 0; i < _p2Panel.transform.childCount; i++)
            {
                Destroy(_p2Panel.transform.GetChild(i).gameObject);


            }

         

            SpawnArrows();
        }
        else if (_pointsJ1 == 3 || _pointsJ2 == 3)
        {
            _textGameOver.SetActive(true);
            _overlay.SetActive(true);
            _bgMusic.Stop();
            Debug.Log("Game Over");
        }
    }
}
