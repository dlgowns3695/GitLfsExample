using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingState : MoveMentBaseState
{
    const string WALK = "Walking";

    // abstract 함수 써주면 빨간줄 그어지기에 클래스 도 abstract 추가해줘야한다. --> override 로 재정의 해줘야함
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
        movement.SetAnimationState(WALK, false); // 애니메이션 중지
        movement.SwitchState(nextSate);
    }
}
