using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningState : MoveMentBaseState
{
    const string RUN = "Running";
    // abstract �Լ� ���ָ� ������ �׾����⿡ Ŭ���� �� abstract �߰�������Ѵ�. --> override �� ������ �������
    public override void EnterState(MovementStateManager movement)
    {
        movement.SetAnimationState(RUN, true); // Running �ִϸ��̼� ����
    }


    public override void UpdateState(MovementStateManager movement)
    {
        if (Input.GetKeyUp(KeyCode.LeftShift))
            ExitState(movement, movement.Walk); // �ٴٰ� Ű ������  ��ũ���·� ���ư���

        else if(movement.Direction.magnitude <0.1f) // �ӵ��� 0.1���� �������� ���̵� ����.
            ExitState(movement, movement.Idle);

        movement.UpdateSpeed(this);
    }

    protected private override void ExitState(MovementStateManager movement, MoveMentBaseState nextSate)
    {
        movement.SetAnimationState(RUN, false);
        movement.SwitchState(nextSate);
    }
}
