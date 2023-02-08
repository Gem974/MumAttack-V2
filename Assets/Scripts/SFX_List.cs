using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SFX_List
{
   
    [Header("PlayerSFX")]
    public AudioClip SFX_BallHit;
    public AudioClip SFX_BallGrass;
    public AudioClip SFX_ItemSpawn;
    public AudioClip SFX_KiosqueProjection;

    [Header("TrashSFX")]
    public AudioClip SFX_TrashFill_01;
    public AudioClip SFX_TrashFill_02;
    public AudioClip SFX_TrashFill_03;
    public AudioClip SFX_TrashFill_04;
    public AudioClip SFX_TrashFill_05;
}
