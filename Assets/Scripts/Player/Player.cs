using System;
using Managers.Audio_Manager;
using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace
{
    public class Player : MonoBehaviour, IPlayer
    {
        public Transform Transform => transform;

        public GameObject FButton => _fButton;

        public Animator Animator => _animator;

        #region Fields


        private PlayerStateType _currentPlayerStateType = PlayerStateType.None;

        #endregion


        #region EnterButton

        [SerializeField] private GameObject _fButton;

        [SerializeField] private Animator _animator;

        #endregion

        #region Controllers

        private PlayerMovement _playerMovement;
        private CharacterController2D _characterController2D;
        private GravityController _gravityController;
        private ConstantForce2D _constantForce2D;
        private BoxCollider2D _boxCollider2D;
        private CircleCollider2D _circleCollider2D;
        private Rigidbody2D _rigidbody2D;

        #endregion

        #region Events

        [Header("Events")] [SerializeField] private PlayerStateEvent _onPlayerStateChanged;
        [SerializeField] private UnityEvent OnPlayerLand;
        [SerializeField] private UnityEvent OnPlayerJumped;

        [Header("Audio Source")] [SerializeField]
        private AudioSource _audioSource;

        [SerializeField] private AudioSource _transitionAudioSource;
        [SerializeField] private AudioSource _walkAudioSource;
        #endregion

        #region Private Properties

        private bool isPlayerMoving => _playerMovement.VerticalMove != 0f || _playerMovement.HorizontalMove != 0f;

        #endregion

        private bool _collisionTriggered;

        private void Awake()
        {
            _playerMovement = GetComponent<PlayerMovement>();
            _characterController2D = GetComponent<CharacterController2D>();
            _gravityController = GetComponent<GravityController>();
            _constantForce2D = GetComponent<ConstantForce2D>();
            _boxCollider2D = GetComponent<BoxCollider2D>();
            _circleCollider2D = GetComponent<CircleCollider2D>();
            _rigidbody2D = GetComponent<Rigidbody2D>();

            _playerMovement.OnJump += () =>
            {
                OnPlayerJumped?.Invoke();
                AudioManager.instance.PlaySoundEffect(_audioSource, AudioTypes.Jump);
            };
            _playerMovement.OnLand += () =>
            {
                OnPlayerLand?.Invoke();
                AudioManager.instance.PlaySoundEffect(_audioSource, AudioTypes.Land);
            };
            _onPlayerStateChanged.AddListener(state =>
            {
                if (state == PlayerStateType.Walking)
                {
                    AudioManager.instance.PlaySoundEffect(_walkAudioSource, AudioTypes.Walk);
                }
                else
                {
                    AudioManager.instance.StopSoundEffect(_walkAudioSource);
                }
            });
        }

        private void Update()
        {
            if (isPlayerMoving)
            {
                SetPlayerState(PlayerStateType.Walking);
            }

            if (!isPlayerMoving && _playerMovement.IsGrounded && _currentPlayerStateType != PlayerStateType.Idle)
            {
                SetPlayerState(PlayerStateType.Idle);
            }
        }

        private void SetPlayerState(PlayerStateType playerStateType)
        {
            if (playerStateType == _currentPlayerStateType)
                return;

            _currentPlayerStateType = playerStateType;
            _onPlayerStateChanged?.Invoke(_currentPlayerStateType);
        }

        public void Enable()
        {
            _playerMovement.enabled = true;
            _characterController2D.enabled = true;
            _boxCollider2D.isTrigger = false;
            _circleCollider2D.isTrigger = false;
            _constantForce2D.enabled = true;
        }

        public void Disable()
        {
            _rigidbody2D.velocity = Vector2.zero;

            _playerMovement.enabled = false;
            _characterController2D.enabled = false;
            _boxCollider2D.isTrigger = true;
            _circleCollider2D.isTrigger = true;
            _constantForce2D.enabled = false;
        }

        public void Die()
        {
            Disable();
            
            SetPlayerState(PlayerStateType.Die);
            
            AudioManager.instance.PlaySoundEffect(_audioSource,AudioTypes.Die);
            //TODO : Play Die Animation;
        }
        
        void OnCollisionEnter2D(Collision2D collision)
        {
            if (_collisionTriggered)
                return;
            
            if (collision.collider.CompareTag("Win"))
            {
                Disable();
                
                _collisionTriggered = true;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Win"))
            {
                Disable();
                
                _collisionTriggered = true;
            }

            if (collision.CompareTag("Interactable"))
            {
                ShowInteractButton();
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Interactable"))
            {
                HideInteractButton();
            }
        }

        private void ShowInteractButton()
        {
            _fButton.SetActive(true);
        }

        private void HideInteractButton()
        {
            _fButton.SetActive(false);
        }
    }

    [Serializable]
    public class PlayerStateEvent : UnityEvent<PlayerStateType>
    {
    }
}