using UnityEngine;


[CreateAssetMenu(fileName = "New Trash", menuName = "Element/Spawn")]
public class TrashCaracteristics : ScriptableObject
{
    public GameObject _visual;
    public GarbageColor _trashColor;
}
