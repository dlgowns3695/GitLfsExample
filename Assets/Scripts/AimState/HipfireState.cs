using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HipfireState : AimBaseState
{
    public HipfireState(float fov) : base(fov)
    {

    }

    public override void EnterState(AimStateManager aim)
    {
        aim.SetAnimationState("Aiming", false);
        aim.SetVCamFov(fov);
    }


    public override void UpdateState(AimStateManager aim)
    {
        if (Input.GetKey(KeyCode.Mouse1)) // Mouse1 = 마우스 오른쪽 버튼,  2는 왼쪽
            aim.SwitchState(aim.Aim);
    }
}
