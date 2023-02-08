using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputReceiver : MonoBehaviour
{

    public Vector2 _inputsP1, _inputsP2;
    public string[] _inputStringPayload, _inputStringP1, _inputStringP2;


    public void ReadComprateString(object data)
    {
        var text = data as string;
        Debug.Log(text);

        //Aiment
        if (text == "RESET")
        {
            FindObjectOfType<Script_GameManager>().LoadScene(0);
        }

        _inputStringPayload = text.Split('*');


        Debug.Log(_inputStringPayload[0]);
        Debug.Log(_inputStringPayload[1]);


        _inputStringP1 = _inputStringPayload[0].Split('_');
        _inputStringP2 = _inputStringPayload[1].Split('_');

      //  Debug.Log(_inputStringPayload);



        if (_inputStringP1.Length == 0 || _inputStringP2.Length == 0) { return; }

        float.TryParse(_inputStringP1[0].Replace('.',','), out _inputsP1.x);
        float.TryParse(_inputStringP1[1].Replace('.', ','), out _inputsP1.y);

        float.TryParse(_inputStringP2[0].Replace('.', ','), out _inputsP2.x);
        float.TryParse(_inputStringP2[1].Replace('.', ','), out _inputsP2.y);
    }

}
