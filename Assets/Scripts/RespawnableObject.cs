using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Managers
{
    public class RespawnableObject : MonoBehaviour
    {
        [SerializeField] private UnityEvent _onRespawn;
        [SerializeField] private UnityEvent _onDrown;

        [SerializeField] private float _respawnDelay;

        private Vector3 _initialPosition;

        private void Start()
        {
            _initialPosition = transform.position;
        }

        private async void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Water"))
            {
                gameObject.SetActive(false);

                _onDrown?.Invoke();
                
                await Respawn();
                
                _onRespawn?.Invoke();
            }
        }

        private async Task Respawn()
        {
            await Task.Delay((int)(_respawnDelay*1000));

            transform.position = _initialPosition;
            gameObject.SetActive(true);
            
        }
    }
}