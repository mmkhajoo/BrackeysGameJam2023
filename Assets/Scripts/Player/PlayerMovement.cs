using System;
using UnityEngine;

namespace DefaultNamespace
{
    [RequireComponent(typeof(CharacterController2D))]
    public class PlayerMovement : MonoBehaviour
    {

        [SerializeField] private Animator _animator;
        
        public event Action OnJump;
        public event Action OnLand;

        public Vector3 Direction => transform.right.normalized;

        public float VerticalMove => _verticalMove;
        public float HorizontalMove => _horizontalMove;
        public bool IsGrounded => controller.IsGrounded;


        public CharacterController2D controller;

        [SerializeField] private float runSpeed = 40f;

        private float _verticalMove = 0f;
        private float _horizontalMove = 0f;

        private bool _jump = false;
        private bool _waitForJumpButtonUp;

        private ConstantForce2D _constantForce2D;
        
        private readonly int Jump = Animator.StringToHash("Jump");
        private readonly int Run = Animator.StringToHash("Run");

        //TODO : Add Events for Play Idle and Move Animation on Player

        private void Awake()
        {
            _constantForce2D = GetComponent<ConstantForce2D>();

            controller.OnJumpAvailable += JumpAvailable;
            controller.OnLandEvent.AddListener(() =>
            {
                OnLand?.Invoke();
                _animator.SetBool(Jump, false);
            });
        }

        private void JumpAvailable()
        {
            if (_jump)
            {
                _waitForJumpButtonUp = true;
            }
        }

        private void Update()
        {
            _verticalMove = Input.GetAxisRaw("Vertical") * runSpeed;
            _horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

            // Debug.DrawLine(transform.position, transform.position + transform.right * 10, Color.yellow);
            
            if (Input.GetButton("Jump") && !_waitForJumpButtonUp)
            {
                _jump = true;
                _animator.SetBool(Jump, true);
            }
            else
            {
                _jump = false;
            }

            if (Input.GetButtonDown("Jump") && controller.IsGrounded)
            {
                   OnJump?.Invoke();

                _waitForJumpButtonUp = false;
            }


            if (Input.GetButtonUp("Jump"))
            {
                if (controller.IsGrounded)
                    _waitForJumpButtonUp = false;
                else
                    _waitForJumpButtonUp = true;
            }


            if (_horizontalMove != 0 && !_jump)
            {
                _animator.SetBool(Run, true);
            }

            if (_horizontalMove == 0)
            {
                _animator.SetBool(Run,false);
            }

        }


        private void FixedUpdate()
        {
            if (_constantForce2D.force.y != 0f)
            {
                _verticalMove = 0;
            }
            else if (_constantForce2D.force.x != 0f)
            {
                _horizontalMove = 0;
            }

            controller.Move(_verticalMove * Time.fixedDeltaTime, _horizontalMove * Time.fixedDeltaTime, false, _jump);
        }
    }
}