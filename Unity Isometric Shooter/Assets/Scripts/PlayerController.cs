using System;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]

[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    //Actions
    public event Action OnFootstepEvent;
    public event Action OnLandedEvent;

    [Tooltip("Acceleration and deceleration")]
    public float SpeedChangeRate = 10.0f;

    // private PlayerInput _playerInput;
    private InputAction _moveAction;
    private InputAction _sprintAction;
    private Vector2 _moveAmt;
    private Animator _animator;
    private CharacterController _characterController;
    
    public float walkingSpeed = 6.0F;
    public float sprintSpeed = 10.0F;
    public float jumpSpeed = 8.0F;
    private float _animationBlend;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;

    private int _animIDSpeed;
    private int _animIDGrounded;
    private int _animIDMotionSpeed;
    private Camera _camera;

    private void Awake()
    {
        // _playerInput = GetComponent<PlayerInput>();
        _animator = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();
        _moveAction = InputSystem.actions.FindAction("Move");
        _sprintAction = InputSystem.actions.FindAction("Sprint");
    }

    private void Start()
    {
        _camera = Camera.main;
        AssignAnimationIDs();
    }

    private void Update()
    {
        _moveAmt = _moveAction.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        var movementSpeed = _sprintAction.IsPressed() ? sprintSpeed : walkingSpeed;

        if (_characterController.isGrounded)
        {
            // Get camera-relative forward and right, flattened to XZ plane
            Vector3 camForward = _camera.transform.forward;
            Vector3 camRight   = _camera.transform.right;
            camForward.y = 0f;
            camRight.y   = 0f;
            camForward.Normalize();
            camRight.Normalize();

            // Build direction from input relative to camera
            moveDirection = camForward * _moveAmt.y + camRight * _moveAmt.x;
            moveDirection *= movementSpeed;

            if (moveDirection.sqrMagnitude > 0.01f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0, moveDirection.z));
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
            }
        }

        moveDirection.y -= gravity * Time.deltaTime;
        _characterController.Move(moveDirection * Time.deltaTime);

        float inputMagnitude = _moveAction.GetControlMagnitude();
        float targetBlend    = inputMagnitude > 0.01f ? movementSpeed : 0f;

        _animationBlend = Mathf.Lerp(_animationBlend, targetBlend, Time.deltaTime * SpeedChangeRate);
        if (_animationBlend < 0.01f) _animationBlend = 0f;

        _animator.SetFloat(_animIDSpeed, _animationBlend);
        _animator.SetFloat(_animIDMotionSpeed, inputMagnitude);
    }

    private void AssignAnimationIDs()
    {
        _animIDSpeed = Animator.StringToHash("Speed");
        _animIDGrounded = Animator.StringToHash("Grounded");
        // _animIDJump = Animator.StringToHash("Jump");
        // _animIDFreeFall = Animator.StringToHash("FreeFall");
        _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
    }

    private void OnFootstep(AnimationEvent animationEvent)
    {
        if (animationEvent.animatorClipInfo.weight < 0.5f) return;
        OnFootstepEvent?.Invoke();
    }

    private void OnLand(AnimationEvent animationEvent)
    {
        if (animationEvent.animatorClipInfo.weight < 0.5f) return;
        OnLandedEvent?.Invoke();
    }
}