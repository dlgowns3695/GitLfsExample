using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultState : ActionbaseState
{
    public override void EnterState(ActionStateManager action)
    { 

    }

    public override void UpdateState(ActionStateManager action) 
    {
/*        action.rightHandAim.weight = Mathf.Lerp(action.rightHandAim.weight, 1, Time.deltaTime * 5);
        action.leftHandIK.weight = Mathf.Lerp(action.leftHandIK.weight, 1, Time.deltaTime * 5);*/

        if (Input.GetKeyDown(KeyCode.R) && action.ammo.CanReload())
            action.SwitchState(action.Reload);
    }
}
