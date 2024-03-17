using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 현재 플레이어의 상태를 나타내는 변수를 가지고 있음, 움직임 제어 까지
public class MovementStateManager : MonoBehaviour
{
    [Header("플레이어 이동 스피드")]
    [SerializeField] private float currentMoveSpeed;
    [SerializeField] private float walkSpeed = 3f, walkBackSpeed = 2f;
    [SerializeField] private float runSpeed = 7f, runBackSpeed = 5f;
    [SerializeField] private float crouchSpeed = 2f, crouchBackSpeed = 1f;

    private Vector3 dir;            // 플레이어 이동 방향
    private float hzInput, vInput;  // 사용자로부터 입력된 이동방향 수치
    private CharacterController controller;

    [SerializeField] private LayerMask groundLayer;
    [SerializeField][Range(-5f, -1f)] private float fallingVelocity = -2f; // 범위제한

    // 상태 변수들...
    private MoveMentBaseState currentState;
    [HideInInspector] public IdelState Idle = new();
    [HideInInspector] public WalkingState Walk = new ();
    [HideInInspector] public RunningState Run = new ();
    [HideInInspector] public CrouchingState Crouch = new ();

    private Animator anim;


    private Vector3 velocity;

    public Vector3 Direction
    {
        get { return dir; }
    } 

    private void Awake()
    {
        SwitchState(Idle);

        anim = GetComponent<Animator> ();
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    private void Update()
    {
        DirectionAndMove();
        Gravity();

        anim.SetFloat("hzInput", hzInput);
        anim.SetFloat("vInput", vInput);

        currentState.UpdateState(this);
    }

    private void DirectionAndMove()
    {
        hzInput = Input.GetAxis("Horizontal");
        vInput = Input.GetAxis("Vertical");

        dir = transform.forward * vInput + transform.right * hzInput;
        controller.Move(dir.normalized * currentMoveSpeed * Time.deltaTime);
    }

    private bool IsGround() // 땅바닥인지 체크
    {
        // 플레이어의 중심점 쏠 거리 구하기
        float dist = controller.bounds.size.y / 2+ 0.01f;
        return Physics.Raycast(transform.position, Vector3.down, dist, groundLayer); // 바닥에 쏴서 땅바닥이면
    }

    private void Gravity() // 중력
    {
        if(!IsGround()) //  공중에 떠 있다면
            velocity.y += Physics.gravity.y * Time.deltaTime;
        else if(velocity.y < 0)
            velocity.y = fallingVelocity; // 내려오는 스피드

        controller.Move(velocity * Time.deltaTime);
        
    }

    public void SwitchState(MoveMentBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
  
    }
    public void SetAnimationState(string animationName, bool trigger)
    {
        anim.SetBool(animationName, trigger);
    }

    public void UpdateSpeed(MoveMentBaseState state) // MoveMentBaseState 상속받은 idle, run, crouching, walk 등등 상태를 불러올수있
    {
        if(state is WalkingState)
            currentMoveSpeed = (vInput > 0) ? walkSpeed : walkBackSpeed;

        else if(state is RunningState)
            currentMoveSpeed = (vInput > 0) ? runSpeed : runBackSpeed;
        else if(state is CrouchingState)
            currentMoveSpeed = (vInput > 0) ? crouchSpeed : crouchBackSpeed;
    }

}
