using System.Collections;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Events;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        private Player _player;

        private Wolf.Wolf _wolf; 

        public UnityEvent _onWinter;

        public UnityEvent _onSpring;

        private bool _isWinter = true;

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

            _wolf = FindObjectOfType<Wolf.Wolf>();
        }

        public void ChangeSeason()
        {
            if (_isWinter)
            {
                StartCoroutine(ChangeSeasonToSpring());
            }
            else
            {
                StartCoroutine(ChangeSeasonToWinter());
            }
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

            yield return new WaitForSeconds(2f);

            _onWinter?.Invoke();

            yield return new WaitForSeconds(2f);

            _player.Animator.Play("Fox_Idle");

            ResetShards();

            CameraManager.Instance.ChangeCameraToMain();

            _isWinter = true;
            
            if (_wolf.Died)
            {
                _wolf.ReplaceWithSkull();
            }

            //TODO : Change Sprites;
        }

        private IEnumerator ChangeSeasonToSpring()
        {
            CameraManager.Instance.ChangeCameraToSleep();

            _player.Animator.Play("fox_sleep");
            _player.FButton.SetActive(false);

            yield return new WaitForSeconds(2f);

            _onWinter?.Invoke();

            yield return new WaitForSeconds(2f);

            _player.Animator.Play("Fox_Idle");

            DropShards();

            CameraManager.Instance.ChangeCameraToMain();

            _isWinter = false;
            
            //TODO : Change Sprites;
        }
    }
}