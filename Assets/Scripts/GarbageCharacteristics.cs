using UnityEngine;


[CreateAssetMenu(fileName = "New Garbage", menuName = "Element/SpawnGarbage")]
public class GarbageCharacteristics : ScriptableObject
{
    
    [SerializeField] public GarbageType _garnageType;
    public GameObject _visual;
    public GarbageColor _trashColor;
}
public enum GarbageType{ Cot, Pizza, Boeuf, Masque, Cigarette, Canette, Poulet, Assiette, CapriSun };