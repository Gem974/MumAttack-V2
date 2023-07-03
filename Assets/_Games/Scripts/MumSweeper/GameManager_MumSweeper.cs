using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_MumSweeper : MonoBehaviour
{
    //Variables
    [Header("Systems")]
    public bool _canPlay;

    [Header("UI")]
    public GameObject _GOPanel;
    public Animator _HUD;

    [Header("Level Manager")]
    public List<Cells> _grid = new List<Cells>();
    public List<Cells> _emptyCells = new List<Cells>();
    public float _trapCount = 5;
        
    private PlayerController[] _players;
    public static GameManager_MumSweeper instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Il y a plus d'une instance de GameManager dans la scène");
            return;
        }

        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _players = FindObjectsOfType<PlayerController>();
        _canPlay = false;
        foreach (var i in FindObjectsOfType<Cells>())
        {
            _grid.Add(i);
        }
        GetGoal();
        PutTrap();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GetGoal()
    {
        var id = Random.Range(0, _grid.Count);
        var grid = _grid.ToArray();
        grid[id]._isGoal = true;
       
    }

    private void PutTrap()
    {
        foreach (var i in _grid)
        {
            if (i._isGoal == false)
                _emptyCells.Add(i);
        }

        for (int i = 0; i < _trapCount; i++)
        {
            var id = Random.Range(0, _emptyCells.Count);
            _emptyCells[id]._isTrap = true;
            _emptyCells.Remove(_emptyCells[id]);

        }
    }


    public void GameOver(bool isPlayer1)
    {
        PauseGame.instance.CanTPause();
        _canPlay = false;
        _GOPanel.SetActive(true);
        if (isPlayer1)
            GameOverBehaviour.instance.PlayerToWin(1);
       else
            GameOverBehaviour.instance.PlayerToWin(2);
        
    }

    public void StartGameAfterDiscount()
    {
        _canPlay = true;
        PauseGame.instance.CanPause();
        foreach (var player in _players)
        {
            player.ChangeActionMap();
        }
    }
}
