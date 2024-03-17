using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingState : MoveMentBaseState
{
    const string WALK = "Walking";

    // abstract �Լ� ���ָ� ������ �׾����⿡ Ŭ���� �� abstract �߰�������Ѵ�. --> override �� ������ �������
    public override void EnterState(MovementStateManager movement) 
    {
        movement.SetAnimationState(WALK, true);
    }


    public override void UpdateState(MovementStateManager movement)
    {
        if (Input.GetKey(KeyCode.LeftShift))
            ExitState(movement, movement.Run);

        else if (Input.GetKeyDown(KeyCode.C))
            ExitState(movement, movement.Crouch);
        else if (movement.Direction.magnitude < 0.1f)
            ExitState(movement, movement.Idle);

        movement.UpdateSpeed(this);
    }

    protected private override void ExitState(MovementStateManager movement, MoveMentBaseState nextSate)
    {
        movement.SetAnimationState(WALK, false); // �ִϸ��̼� ����
        movement.SwitchState(nextSate);
    }
}
