using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.InputSystem.Users;
using Spine.Unity;


public class Player_babytoystorm : PlayerBehaviour
{
    [Header("Baby Toy Storm")]
    [SerializeField] Rigidbody _rb;
    [SerializeField] float _speed = 1;

    [Header("Spine")]
    [SerializeField] SkeletonAnimation _skeletonAnimation;
    [SerializeField] string _currentAnim;
    


    // Start is called before the first frame update
    public void Start()
    {
        base.ForceController();
        _rb = GetComponent<Rigidbody>();
        DisplayInfoCharacter();
        SetAnimation("Idle", true, 1);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Move();
    }

    //public override void ChangeActionMap()
    //{
    //    base.ChangeActionMap();
    //}



    public void Move()
    {

        if (BabyToyStorm_GameManager.instance._canPlay)
        {
            _rb.velocity = new Vector3(_moveInput.x * _speed, 0, _moveInput.y * _speed);
            if (_moveInput != Vector2.zero)
            {
                SetAnimation("Sprint", true, 1);
            }
            else
            {
                SetAnimation("Idle", true, 1);
            }

            if (_moveInput.x > 0)
            {
                _skeletonAnimation.skeleton.ScaleX = 1; //Flip a droite
            }
            else if (_moveInput.x < 0)
            {

                _skeletonAnimation.skeleton.ScaleX = -1; //Flip à gauche
            }
           
        }
    }

    void DisplayInfoCharacter()
    {
        if (_isPlayer1)
        {
            _skeletonAnimation.skeleton.SetSkin(META.MetaGameManager.instance._player1._name);
            _skeletonAnimation.Skeleton.SetSlotsToSetupPose();
          
        }
        else
        {
            _skeletonAnimation.skeleton.SetSkin(META.MetaGameManager.instance._player2._name);
            _skeletonAnimation.Skeleton.SetSlotsToSetupPose();
            
        }
    }

    void SetAnimation(string AnimName, bool isLooping, float AnimSpeed)
    {
        if (AnimName == _currentAnim) //Ne relance pas la même animation en boucle
        {
            return;
        }
        _skeletonAnimation.AnimationState.SetAnimation(0, AnimName, isLooping).TimeScale = AnimSpeed;
        _currentAnim = AnimName;
    }










}
