using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cells : MonoBehaviour
{
    //Variables
    public bool _isGoal = false;
    public bool _isTrap = false;
    public bool _alreadyDig = false;
    public Color _baseColor, _emptyColor, _trapColor, _goalColor;
    private MeshRenderer _mesh;

    // Start is called before the first frame update
    void Start()
    {
        _mesh = GetComponent<MeshRenderer>();
        _baseColor = Color.white;
        _emptyColor = Color.grey;
        _trapColor = Color.red;
        _goalColor = Color.green;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RevealTile(bool isPlayer1, PlayerController player)
    {
        if(_alreadyDig == false)
        {
            _alreadyDig = true;
            if (_isGoal)
            {
                _mesh.material.color = _goalColor;
                GameManager_MumSweeper.instance.GameOver(isPlayer1);
            }
            if (_isTrap)
            {
                _mesh.material.color = _trapColor;
                player.GetTrapped();

            }
            if (!_isGoal && !_isTrap)
                _mesh.material.color = _emptyColor;
        }
        else
        {
            _alreadyDig = false;
            _mesh.material.color = _baseColor;
        }
    }
}
