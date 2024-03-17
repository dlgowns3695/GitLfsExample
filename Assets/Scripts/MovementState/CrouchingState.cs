using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchingState : MoveMentBaseState
{
    const string CROUCH = "Crouching";
    // abstract �Լ� ���ָ� ������ �׾����⿡ Ŭ���� �� abstract �߰�������Ѵ�. --> override �� ������ �������
    public override void EnterState(MovementStateManager movement)
    {
        movement.SetAnimationState(CROUCH, true); // �ִϸ��̼� ����
        Debug.Log("ũ���Ī");
    }


    public override void UpdateState(MovementStateManager movement)
    {
        if (Input.GetKeyUp(KeyCode.C))
            ExitState(movement, (movement.Direction.magnitude < 0.1f) ? movement.Idle : movement.Walk);
        if (Input.GetKey(KeyCode.LeftShift))
            ExitState(movement, movement.Run);

        movement.UpdateSpeed(this);
    }

    protected private override void ExitState(MovementStateManager movement, MoveMentBaseState nextSate)
    {
        movement.SetAnimationState(CROUCH, false); // ����
        movement.SwitchState(nextSate);
    }
}
