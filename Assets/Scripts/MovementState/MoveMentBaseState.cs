
public abstract class MoveMentBaseState
{
    // abstract �Լ� ���ָ� ������ �׾����⿡ Ŭ���� �� abstract �߰�������Ѵ�.
    public abstract void EnterState(MovementStateManager movement);

    public abstract void UpdateState(MovementStateManager movement);

    protected private abstract void ExitState(MovementStateManager movement, MoveMentBaseState nextSate);
}
