using UnityEngine;
using TMPro;

public class Script_InputSlot : MonoBehaviour
{
    [SerializeField] int _place;
    [SerializeField] TMP_InputField _input;
    Script_GameManager _GM;
    
     void Start () 
    {
        _GM = Script_GameManager.instance;
        //Add a listener function here
        //Note: The function has to be of the type with parameter string
        _input.onEndEdit.AddListener(SendNameToScriptableObject);
    }
    public void SendNameToScriptableObject(string _name){
        _GM.AddNewName(_place, _name);
    }
}
