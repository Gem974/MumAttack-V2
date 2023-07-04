using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_MumSweeper : MonoBehaviour
{
    //Variables
    [Header("Systems")]
    public bool _canPlay;
    private PlayerController[] _players;

    [Header("UI")]
    public GameObject _GOPanel;
    public Animator _HUD;

    [Header("Level Manager")]
    public float _trapCount = 5;
    private List<Cells> _grid = new List<Cells>();
        

    //Singleton
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

        //List des cellules du terrain
        foreach (var i in FindObjectsOfType<Cells>())
        {
            _grid.Add(i);
        }

        //Choisir la cellule à trouver
        GetGoal();

        //Mise en place des pieges
        PutTrap();
    }

    //Fonction pour assigner une cellule objectif de la partie
    private void GetGoal()
    {
        var id = Random.Range(0, _grid.Count);
        _grid[id]._isGoal = true;

        //On retire la cellule objectif de la list de cellule
        _grid.Remove(_grid[id]);
    }

    //Fonction qui choisi une cellule et la transforme en piege
    private void PutTrap()
    {
        for (int i = 0; i < _trapCount; i++)
        {
            var id = Random.Range(0, _grid.Count);
            _grid[id]._isTrap = true;
            _grid.Remove(_grid[id]);
        }
    }

    //Fonction déclenchée par l'effet de la cellule objectif
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
