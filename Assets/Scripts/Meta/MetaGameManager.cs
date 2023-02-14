using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace META
{
    public class MetaGameManager : MonoBehaviour
    {
        public static MetaGameManager instance;

        public enum moms
        {
            None, Rachel, Aurelie
        }

        [Header("Set Characters")]
        public moms _moms;


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
