using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class AimStateManager : MonoBehaviour
{
    [SerializeField] private float mouseSensitive = 1f;  
    [SerializeField] private Transform camFollowPos;
    private float xAxis, yAxis;

    [SerializeField] private CinemachineVirtualCamera vCam;
    [SerializeField] private float aimFov = 40f;
    [SerializeField] private float hipFov = 60f;
    [SerializeField] private float fovSmoothSpeed = 10f;
    private float currentFov;

    [SerializeField] private Transform aimPos;
    [SerializeField] private float aimSoothSpeed = 20f;
    [SerializeField] private LayerMask aimMask;

    private AimBaseState currentState;
    [HideInInspector] public HipfireState Hip;
    [HideInInspector] public AimState Aim;

    private Animator anim;

    public Transform AimPos
    {
        get { return aimPos; }
    }

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;

        Hip = new(hipFov);
        Aim = new(aimFov);

        vCam.m_Lens.FieldOfView = hipFov;
        anim = GetComponent<Animator>();

      
        SwitchState(Hip);
    }



    // Update is called once per frame
    private void Update()
    {
        /* xAxis.Update(Time.deltaTime);
         yAxis.Update(Time.deltaTime);*/
        xAxis += Input.GetAxisRaw("Mouse X") * mouseSensitive ;
        yAxis -= Input.GetAxisRaw("Mouse Y") * mouseSensitive ;
        yAxis = Mathf.Clamp(yAxis, -80, 80);

        vCam.m_Lens.FieldOfView = Mathf.Lerp(vCam.m_Lens.FieldOfView, currentFov, fovSmoothSpeed * Time.deltaTime);

        currentState.UpdateState(this);

        Vector2 screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);
        if(Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, aimMask))
        { 
            aimPos.position = Vector3.Lerp(aimPos.position, hit.point,aimSoothSpeed * Time.deltaTime);
        }
    }

    private void LateUpdate()
    {
        camFollowPos.localEulerAngles = new Vector3(yAxis,
                                                    camFollowPos.localEulerAngles.y,
                                                    camFollowPos.localEulerAngles.z);

        transform.eulerAngles = new Vector3(transform.eulerAngles.x,
                                            xAxis,
                                            transform.eulerAngles.z);

    }

    public void SetAnimationState(string animationName, bool trigger)
    {
        anim.SetBool(animationName, trigger);
    }

    public void SwitchState(AimBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);


    }

    public void SetVCamFov(float fov)
    {
        currentFov = fov;
    }
}
