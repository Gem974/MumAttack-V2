using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace META
{
    public class MetaGameManager : MonoBehaviour
    {
        public static MetaGameManager instance;

        [Header("Set Characters")]
        public Character _player1;
        public Character _player2;


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
        }








    }
}
