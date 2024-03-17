using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimState : AimBaseState
{
    public AimState(float fov) : base(fov)
    {

    }

    public override void EnterState(AimStateManager aim)
    {
        aim.SetAnimationState("Aiming", true);
        aim.SetVCamFov(fov);
    }


    public override void UpdateState(AimStateManager aim)
    {
        if (Input.GetKeyUp(KeyCode.Mouse1)) // Mouse1 = ���콺 ������ ��ư,  2�� ����
            aim.SwitchState(aim.Hip);
    }
}
