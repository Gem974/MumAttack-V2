using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

namespace META
{
    public class MetaGameManager : MonoBehaviour
    {
        public static MetaGameManager instance;

        public enum GameMode
        {
            None,BroadCast, FreeBrawl
        }
        public GameMode _gameMode;

        [Header("BroadCast Mode")]
        public int _maxStep;
        public int _currentStep;
        public int _P1Wins, _P2Wins;

        [Header("FreeBrawl Mode")]
        public int _gameID;

        [Header("Set Characters")]
        public Character _player1;
        public Character _player2;
        public Character _winner;
        public PlayerInput _p1, _p2;
        public InputUser _user1, _user2;
        public InputDevice _device1, _device2;

        [Header("LeaderBoard")]

        public int[] _topScorePoints;
        public string[] _topScoreName;

        [Header("Sounds")]
        public float _volume;




        private void Awake()
        {
            if (instance != null)
            {

                Destroy(this.gameObject);
                Debug.LogError("Il y avait plus d'une instance de MetaGameManager dans la scène.");
                return;
            }
            instance = this;
        }
        void Start()
        {
            DontDestroyOnLoad(this.gameObject);
            _currentStep = 1;
            _gameMode = GameMode.None;
            _user1 = _p1.user;
            _user2 = _p2.user;
            _device1 = _user1.pairedDevices[0];
            _device2 = _user2.pairedDevices[0];
        }

        public void BroadCastNextStep()
        {
            if (_currentStep <= _maxStep)
            {
                _currentStep++;
            }
            else
            {
                _currentStep = 0;
            }

            if (_currentStep >= _maxStep)
            {
                if (_P1Wins > _P2Wins)
                {
                    _winner = _player1;
                }
                else
                {
                    _winner = _player2;
                }
            }
        }

        public void ResetAll()
        {
            _player1 = null;
            _player2 = null;
            _currentStep = 1;
            _gameID = 0;
            _gameMode = GameMode.None;
        }








    }
}
