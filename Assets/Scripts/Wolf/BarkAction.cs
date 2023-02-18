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

        private async void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Player"))
            {
                var playerMovement = col.GetComponent<PlayerMovement>();

                var rigidBody2D = col.GetComponent<Rigidbody2D>();
                
                playerMovement.enabled = false;
                rigidBody2D.velocity = Vector2.zero;
                
                rigidBody2D.AddRelativeForce(_pushAwayForceValue);
                
                _animator.SetTrigger(Bark);

                 await Task.Delay((int)(_waitTime * 1000));

                 playerMovement.enabled = true;
                 
                 
                _onBarking?.Invoke();
            }
        }

        public void DisableBark()
        {
            _circleCollider2D.enabled = false;
        }
    }
}