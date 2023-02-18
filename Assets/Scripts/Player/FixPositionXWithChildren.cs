using UnityEngine;

namespace DefaultNamespace
{
    public class FixPositionXWithChildren : MonoBehaviour
    {
        [SerializeField] private Transform _children;

        private float _initialYPosition;

        private void Start()
        {
            _initialYPosition = transform.position.y;
        }

        private void FixedUpdate()
        {
            var transform1 = transform;
            var position = transform1.position;
            var position1 = _children.position;
            position = new Vector3(position1.x,_initialYPosition + position1.y,position.z);
            transform1.position = position;
        }
    }
}