using UnityEngine;

namespace DefaultNamespace
{
    public class PushDetector : MonoBehaviour
    {
        private const string PushableObjectTag = "Pushable";

        private const string IdleAnimation = "Fox_Idle";
        private const string PushAnimation = "Fox_Push";

        private PlayerMovement _playerMovement;

        private bool _isPushAnimationPlaying;

        private void Start()
        {
            _playerMovement = GetComponent<PlayerMovement>();
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (!collision.gameObject.CompareTag(PushableObjectTag))
                return;

            if (_playerMovement.HorizontalMove == 0)
            {
                PlayIdleAnimation();
                return;
            }

            var bounds = collision.collider.bounds;
            var offset = bounds.size.x / 2f;

            var offsetY = bounds.size.y / 2f;

            var distance = Mathf.Abs(collision.transform.position.x - transform.position.x);
            var distanceY = Mathf.Abs(collision.transform.position.y - transform.position.y);
            
            if (distance > offset && distanceY < offsetY && !_isPushAnimationPlaying)
            {
                _playerMovement.Animator.Play(PushAnimation);
                _isPushAnimationPlaying = true;
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag(PushableObjectTag))
            {
                if (_isPushAnimationPlaying)
                {
                    PlayIdleAnimation();
                }
            }
        }

        private void PlayIdleAnimation()
        {
            _playerMovement.Animator.Play(IdleAnimation);
            _isPushAnimationPlaying = false;
        }
    }
}