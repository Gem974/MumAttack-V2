using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI    ;

namespace META
{
    public class MomReferencer : MonoBehaviour
    {

        public Transform[] _moms;
        public Character[] _datas;
        public Button _playBtn;
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

            _moms = new Transform[transform.childCount-1];
            for (int i = 0; i < transform.childCount - 1; i++)
            {
                _moms[i] = transform.GetChild(i);
            }
        }


        private void Start()
        {
            _playBtn.onClick.AddListener(StartButton);
            _playBtn.gameObject.SetActive(false);
            Debug.Log(MetaGameManager.instance._device1.name);
        }

        private void StartButton()
        {
            scenesManager.instance.StartButton();
        }

        //private void Update()
        //{
        //    if (Input.GetButtonDown("Cancel"))
        //    {
        //        scenesManager.instance.LoadSpecificScene(0);
        //    }
        //}

        public void ShowButton()
        {
            if (_playersSelector[0]._momValidate && _playersSelector[1]._momValidate)
            {
                _playBtn.gameObject.SetActive(true);
                _playBtn.Select();
            }
            else
            {
                _playBtn.gameObject.SetActive(false);
            }
        }
    } 
}
