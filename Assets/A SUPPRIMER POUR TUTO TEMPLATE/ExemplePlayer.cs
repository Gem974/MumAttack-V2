using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.InputSystem.Users;

public class ExemplePlayer : PlayerBehaviour
{

  
   
    // Start is called before the first frame update
    public void Start()
    {
        base.ForceController();
   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnMove(InputAction.CallbackContext context)
    {
        base.OnMove(context);
        
    }

    //Event pour la touche d'action
    public override void OnAction(InputAction.CallbackContext context)
    {
        base.OnAction(context);
    }


    public override void OnPause(InputAction.CallbackContext context)
    {
        
        base.OnPause(context);
       
    }
}




