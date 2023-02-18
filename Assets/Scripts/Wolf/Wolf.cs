using UnityEngine;
using UnityEngine.Events;

namespace Managers.Wolf
{
    public class Wolf : MonoBehaviour
    {
        public bool Died => _died; 
        
        [SerializeField] private UnityEvent _onWolfDie;

        [SerializeField] private Animator _animator;
        
        [SerializeField] private BarkAction _barkAction;

        [SerializeField] private BoxCollider2D _boxCollider2D;

        private bool _died;
        
        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.collider.CompareTag("Deadly"))
            {
                _onWolfDie?.Invoke();
                
                _barkAction.DisableBark();

                _boxCollider2D.enabled = false;

                _died = true;

                _animator.Play("wolf_Die");
            }
        }
    }
}