
public abstract class MoveMentBaseState
{
    // abstract 함수 써주면 빨간줄 그어지기에 클래스 도 abstract 추가해줘야한다.
    public abstract void EnterState(MovementStateManager movement);

    public abstract void UpdateState(MovementStateManager movement);

    protected private abstract void ExitState(MovementStateManager movement, MoveMentBaseState nextSate);
}
