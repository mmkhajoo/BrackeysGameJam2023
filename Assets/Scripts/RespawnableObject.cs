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

        void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Water"))
            {
                _onDrown?.Invoke();
                
                StartCoroutine(Respawn());
                
                _onRespawn?.Invoke();
                
                //gameObject.SetActive(false);

            }
        }

        IEnumerator Respawn()
        {
            yield return new WaitForSeconds(_respawnDelay);
            transform.position = _initialPosition;
            gameObject.SetActive(true);
            
        }
    }
}