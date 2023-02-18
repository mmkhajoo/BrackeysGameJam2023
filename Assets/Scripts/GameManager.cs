using System.Collections;
using DefaultNamespace;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        private Player _player;
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                
                DontDestroyOnLoad(gameObject);
            }
            
        }
        
        private void Start()
        {
            _player = FindObjectOfType<Player>();
        }

        public void ChangeToWinter()
        {
            StartCoroutine(ChangeSeasonToWinter()); 
            //TODO : Change sprites
        }

        public void ResetShards()
        {
            var shards = FindObjectsOfType<IceShard>(true);

            foreach (var shard in shards)
            {
                shard.gameObject.SetActive(true);
            }

            foreach (var shard in shards)
            {
                shard.ReCreateIceShard();
            }
        }

        public void DropShards()
        {
            var shards = FindObjectsOfType<IceShard>(true);

            foreach (var shard in shards)
            {
                shard.Drop();
            }
        }

        private IEnumerator ChangeSeasonToWinter()
        {
            CameraManager.Instance.ChangeCameraToSleep();
            
            _player.Animator.Play("fox_sleep");
            _player.FButton.SetActive(false);
            
            yield return new WaitForSeconds(4f);
            
            _player.Animator.Play("Fox_Idle");
            
            ResetShards();
            
            DropShards();
            
            CameraManager.Instance.ChangeCameraToMain();

            //TODO : Change Sprites;
        }
    }
}