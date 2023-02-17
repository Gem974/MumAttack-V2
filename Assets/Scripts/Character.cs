using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Rendering;
using Spine;

[CreateAssetMenu(fileName = "Create New Character", menuName = "CHARACTERS")]
public class Character : ScriptableObject

{
    public string _name;
    public Color32 _color;
    [Header("IRONMUM")]
    public Sprite _ironMum_sprite;


    [Header("SUMOM")]
    public RuntimeAnimatorController _animatorController;
 
    

}
