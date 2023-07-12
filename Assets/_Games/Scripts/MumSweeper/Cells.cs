using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cells : MonoBehaviour
{
    //Script des cellules du damier

    //Variables
    public bool _isGoal = false; // La cellule a trouvé
    public bool _isTrap = false; // La cellule negative
    public bool _alreadyDig = false; // Decouverte ou non
    public Color _baseColor, _emptyColor, _trapColor, _goalColor; //Debug pour le moment : a changer par ce que tu veux
    private MeshRenderer _mesh;

    // Start is called before the first frame update
    void Start()
    {
        //DEBUG
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

    //Fonction principale des cellules declanché par l'action du joueur : Revele et applique les effets ou cache la cellule
    public void RevealTile(bool isPlayer1, PlayerController player)
    {
        if(_alreadyDig == false)
        {
            _alreadyDig = true;
            //Effet de la cellule à trouver
            if (_isGoal)
            {
                PresentatorVoice.instance.StartSpeaking(true, true);
                _mesh.material.color = _goalColor;
                GameManager_MumSweeper.instance.GameOver(isPlayer1);
            }

            //Effet de la cellule piege
            if (_isTrap)
            {
                PresentatorVoice.instance.StartSpeaking(true, false);
                _mesh.material.color = _trapColor;
                player.GetTrapped();

            }

            //Effet de la cellule qui n'est pas celle a trouver ni un piege
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
