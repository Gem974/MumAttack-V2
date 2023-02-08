using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Script_HUD_Prevention : MonoBehaviour
{
    public Image _icon;
    public TextMeshProUGUI _title;
    public TextMeshProUGUI _description;
    public RectTransform _bg;

    public void Init(Sprite _newSprite, string _newTitle, string _newDescription){
        _title.text = _newTitle;
        _description.text = _newDescription;
        _icon.sprite = _newSprite;
    }
    public void TurnToLeft(){
        _bg.localScale = new Vector3(-1f,1f,1f);
    }
    public void TurnToRight(){
        _bg.localScale = new Vector3(1f,1f,1f);
    }
}
