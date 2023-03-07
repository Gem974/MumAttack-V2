using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class ButtonSpine : MonoBehaviour
{
    public SkeletonGraphic _skGraphic;
    
    public void SetAnimationOneShot(string animationName)
    {
        _skGraphic.AnimationState.SetAnimation(1, animationName, false);
    }

    public void SetAnimationLooping(string animationName)
    {
        _skGraphic.AnimationState.SetAnimation(1, animationName, true);
    }

    public void SetButtonSkin(string skinName)
    {
        _skGraphic.Skeleton.SetSkin(skinName);
    }


}
