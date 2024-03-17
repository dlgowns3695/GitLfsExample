using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionbaseState
{

    public abstract void EnterState(ActionStateManager action);

    public abstract void UpdateState(ActionStateManager action);

}
