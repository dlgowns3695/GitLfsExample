using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdelState : MoveMentBaseState
{
    // abstract �Լ� ���ָ� ������ �׾����⿡ Ŭ���� �� abstract �߰�������Ѵ�. --> override �� ������ �������
    public override void EnterState(MovementStateManager movement) { }
   

    public override void UpdateState(MovementStateManager movement)
    {
        if(movement.Direction.magnitude > 0.1f)
        {
            // MovementStateManager Walk �Ǵ� Run ���·� ����
            Debug.Log("Walk or Run");
            movement.SwitchState(Input.GetKey(KeyCode.LeftShift) ? movement.Run : movement.Walk);    
        }
        if (Input.GetKeyDown(KeyCode.C))
            movement.SwitchState(movement.Crouch);
    }

    protected private override void ExitState(MovementStateManager movement, MoveMentBaseState nextSate)
    {

    }
}
