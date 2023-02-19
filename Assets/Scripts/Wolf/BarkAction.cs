using System.Collections;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Events;
using Task = System.Threading.Tasks.Task;

namespace Managers.Wolf
{
    public class BarkAction : MonoBehaviour
    {
        [SerializeField] private UnityEvent _onBarking;
        
        [SerializeField] private Vector3 _pushAwayForceValue;

        [SerializeField] private float _waitTime = 2f;

        [SerializeField] private Animator _animator;

        [SerializeField] private CircleCollider2D _circleCollider2D;
        
        private static readonly int Bark = Animator.StringToHash("Bark");

        void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Player"))
            {
                var playerMovement = col.GetComponent<PlayerMovement>();

                var rigidBody2D = col.GetComponent<Rigidbody2D>();
                
                playerMovement.enabled = false;
                rigidBody2D.velocity = Vector2.zero;
                
                rigidBody2D.AddRelativeForce(_pushAwayForceValue);
                
                _animator.SetTrigger(Bark);

                StartCoroutine(DoMyDelay(playerMovement));
            }
        }

        public void DisableBark()
        {
            _circleCollider2D.enabled = false;
        }

        IEnumerator DoMyDelay(PlayerMovement playerMovement)
        {
            yield return new WaitForSeconds(_waitTime);

            playerMovement.enabled = true;
            
            _onBarking?.Invoke();
        }
    }
}