using System;
using System.Collections;
using UnityEngine;

namespace Managers
{
    public class BreakTree : MonoBehaviour
    {
        [SerializeField] private int _timesToBreak;

        [SerializeField] private Animator _animator;

        [SerializeField] private Rigidbody2D _rigidbody2D;

        [SerializeField] private float _timeToActiveNextTouch;

        private bool _isTouchActive;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();

            _isTouchActive = true;
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if(!_isTouchActive)
                return;

            if (col.collider.CompareTag("Player"))
            {
                _timesToBreak--;

                if (_timesToBreak == 0)
                {
                    _animator.enabled = false;
                    _rigidbody2D.isKinematic = false;
                }
                else if(_timesToBreak > 0)
                {
                    _animator.Play("woodMove",-1,0);
                    
                    _isTouchActive = false;

                    StartCoroutine(ActiveTouch());
                }
            }
        }

        private IEnumerator ActiveTouch()
        {
            yield return new WaitForSeconds(_timeToActiveNextTouch);

            _isTouchActive = true;
        }
    }
}