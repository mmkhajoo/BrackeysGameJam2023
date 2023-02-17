using MoreMountains.Feedbacks;
using UnityEngine;

namespace Managers.Wolf
{
    public class Wolf : MonoBehaviour
    {
        public bool Died => _died; 
        
        [SerializeField] private MMFeedbacks _onWolfDie;

        [SerializeField] private Animator _animator;

        private bool _died;
        
        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.collider.CompareTag("Deadly"))
            {
                _onWolfDie.PlayFeedbacks();

                _died = true;
                
                //TODO : Play the Die Animation;
            }
        }
    }
}