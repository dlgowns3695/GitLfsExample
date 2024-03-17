using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadState : ActionbaseState
{
    public override void EnterState(ActionStateManager action)
    {
/*        action.rightHandAim.weight = 0f;
        action.leftHandIK.weight = 0f;*/

        action.ChangeAnimation("Reload");
    }

    public override void UpdateState(ActionStateManager action)
    {

    }
}
