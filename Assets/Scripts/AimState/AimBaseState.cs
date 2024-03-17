using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AimBaseState
{
    protected float fov;
    public float FOV
    {
        get { return fov; }
        set { fov = value; }
    }
    public AimBaseState(float fov)
    {
        this.fov = fov;
    }
       
    public abstract void EnterState(AimStateManager aim);


    public abstract void UpdateState(AimStateManager aim);
}
