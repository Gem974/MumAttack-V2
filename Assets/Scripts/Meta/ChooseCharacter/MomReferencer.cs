using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace META
{
    public class MomReferencer : MonoBehaviour
    {

        public Transform[] _moms;
        public Character[] _datas;
        public GameObject _playBtn;
        public SelectCharacter[] _playersSelector;


        public static MomReferencer instance;
        private void Awake()
        {
            if (instance != null)
            {
                Debug.LogError("Il y a plus d'une instance de MomReferencer dans la scène");
                return;
            }

            instance = this;

            _moms = new Transform[transform.childCount];
            for (int i = 0; i < transform.childCount; i++)
            {
                _moms[i] = transform.GetChild(i);
            }
        }

        private void Start()
        {
            _playBtn.SetActive(false);
        }

        public void ShowButton()
        {
            if (_playersSelector[0]._momChoosed && _playersSelector[1]._momChoosed)
            {
                _playBtn.SetActive(true);
            }
            else
            {
                _playBtn.SetActive(false);
            }
        }
    } 
}
