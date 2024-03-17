using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdelState : MoveMentBaseState
{
    // abstract 함수 써주면 빨간줄 그어지기에 클래스 도 abstract 추가해줘야한다. --> override 로 재정의 해줘야함
    public override void EnterState(MovementStateManager movement) { }
   

    public override void UpdateState(MovementStateManager movement)
    {
        if(movement.Direction.magnitude > 0.1f)
        {
            // MovementStateManager Walk 또는 Run 상태로 변경
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
