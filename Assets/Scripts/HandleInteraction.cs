using UnityEngine;
using UnityEngine.Events;

namespace Managers
{
    public class HandleInteraction : MonoBehaviour
    {
        [SerializeField] private UnityEvent _onInteractButtonClicked;

        private bool _isInteractActive;

        private void Update()
        {
            if(!_isInteractActive)
                return;

            if (Input.GetKeyDown(KeyCode.F))
            {
                _onInteractButtonClicked?.Invoke();
            }
        }


        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Player"))
            {
                _isInteractActive = true;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                _isInteractActive = false;
            }
        }
    }
}