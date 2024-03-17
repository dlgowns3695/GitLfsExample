using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class ActionStateManager : MonoBehaviour
{
    private ActionbaseState currentState;

    public ReloadState Reload = new();
    public DefaultState Default = new();

    public GameObject currentWeapon;
    [HideInInspector] public WeaponAmmo ammo;

    private Animator anim;

    public MultiAimConstraint rightHandAim;
    public TwoBoneIKConstraint leftHandIK;


    public ActionbaseState CurrentState
    {
        get { return currentState; }
    }

    private void Awake()
    {
        SwitchState(Default);
        ammo = currentWeapon.GetComponent<WeaponAmmo>();
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);

    }

    public void SwitchState(ActionbaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    public void ReloadWeapon()
    {
        ammo.Reload();
        SwitchState(Default);
    }

    public void ChangeAnimation(string animationName)
    {
        anim.SetTrigger(animationName);
    }

    public void MagOut()
    {
        ammo.MagOut(); 
    }
    public void MagIn()
    {
        ammo.MagIn();
    }
    public void ReleaseSlide()
    {
        ammo.ReleaseSlide();
    }

}
