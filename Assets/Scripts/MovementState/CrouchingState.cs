using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchingState : MoveMentBaseState
{
    const string CROUCH = "Crouching";
    // abstract 함수 써주면 빨간줄 그어지기에 클래스 도 abstract 추가해줘야한다. --> override 로 재정의 해줘야함
    public override void EnterState(MovementStateManager movement)
    {
        movement.SetAnimationState(CROUCH, true); // 애니메이션 실행
        Debug.Log("크라우칭");
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
        movement.SetAnimationState(CROUCH, false); // 종료
        movement.SwitchState(nextSate);
    }
}
