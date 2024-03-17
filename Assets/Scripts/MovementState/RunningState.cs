using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningState : MoveMentBaseState
{
    const string RUN = "Running";
    // abstract 함수 써주면 빨간줄 그어지기에 클래스 도 abstract 추가해줘야한다. --> override 로 재정의 해줘야함
    public override void EnterState(MovementStateManager movement)
    {
        movement.SetAnimationState(RUN, true); // Running 애니메이션 실행
    }


    public override void UpdateState(MovementStateManager movement)
    {
        if (Input.GetKeyUp(KeyCode.LeftShift))
            ExitState(movement, movement.Walk); // 뛰다가 키 땟으면  워크상태로 돌아가라

        else if(movement.Direction.magnitude <0.1f) // 속도가 0.1보다 낮아지면 아이들 상태.
            ExitState(movement, movement.Idle);

        movement.UpdateSpeed(this);
    }

    protected private override void ExitState(MovementStateManager movement, MoveMentBaseState nextSate)
    {
        movement.SetAnimationState(RUN, false);
        movement.SwitchState(nextSate);
    }
}
