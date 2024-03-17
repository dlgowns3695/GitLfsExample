using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� �÷��̾��� ���¸� ��Ÿ���� ������ ������ ����, ������ ���� ����
public class MovementStateManager : MonoBehaviour
{
    [Header("�÷��̾� �̵� ���ǵ�")]
    [SerializeField] private float currentMoveSpeed;
    [SerializeField] private float walkSpeed = 3f, walkBackSpeed = 2f;
    [SerializeField] private float runSpeed = 7f, runBackSpeed = 5f;
    [SerializeField] private float crouchSpeed = 2f, crouchBackSpeed = 1f;

    private Vector3 dir;            // �÷��̾� �̵� ����
    private float hzInput, vInput;  // ����ڷκ��� �Էµ� �̵����� ��ġ
    private CharacterController controller;

    [SerializeField] private LayerMask groundLayer;
    [SerializeField][Range(-5f, -1f)] private float fallingVelocity = -2f; // ��������

    // ���� ������...
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

    private bool IsGround() // ���ٴ����� üũ
    {
        // �÷��̾��� �߽��� �� �Ÿ� ���ϱ�
        float dist = controller.bounds.size.y / 2+ 0.01f;
        return Physics.Raycast(transform.position, Vector3.down, dist, groundLayer); // �ٴڿ� ���� ���ٴ��̸�
    }

    private void Gravity() // �߷�
    {
        if(!IsGround()) //  ���߿� �� �ִٸ�
            velocity.y += Physics.gravity.y * Time.deltaTime;
        else if(velocity.y < 0)
            velocity.y = fallingVelocity; // �������� ���ǵ�

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

    public void UpdateSpeed(MoveMentBaseState state) // MoveMentBaseState ��ӹ��� idle, run, crouching, walk ��� ���¸� �ҷ��ü���
    {
        if(state is WalkingState)
            currentMoveSpeed = (vInput > 0) ? walkSpeed : walkBackSpeed;

        else if(state is RunningState)
            currentMoveSpeed = (vInput > 0) ? runSpeed : runBackSpeed;
        else if(state is CrouchingState)
            currentMoveSpeed = (vInput > 0) ? crouchSpeed : crouchBackSpeed;
    }

}
