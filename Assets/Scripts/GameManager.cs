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
        
        public UnityEvent _onStartChangingSeason;

        public UnityEvent _onEndChangingSeason;

        private bool _isWinter = true;

        public Transform SleepPos;
        private Vector3 prevPos;

        private bool isBranchBroken = false;
        private bool isDyingWolfShownBefore = false;
        
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
            _onStartChangingSeason.Invoke();
            ChangeFoxPosToSleepPos();
            
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

            ChangeFoxPosToNormalPos();
            _player.Animator.Play("Fox_Idle");

            ResetShards();

            CameraManager.Instance.ChangeCameraToMain();

            _isWinter = true;
            
            if (_wolf.Died)
            {
                _wolf.ReplaceWithSkull();
            }

            _onEndChangingSeason.Invoke();
            
            //TODO : Change Sprites;
        }

        private IEnumerator ChangeSeasonToSpring()
        {
            CameraManager.Instance.ChangeCameraToSleep();

            _player.Animator.Play("fox_sleep");
            _player.FButton.SetActive(false);

            yield return new WaitForSeconds(2f);

            _onSpring?.Invoke();

            yield return new WaitForSeconds(2f);

            ChangeFoxPosToNormalPos();
            _player.Animator.Play("Fox_Idle");

            DropShards();

            CameraManager.Instance.ChangeCameraToMain();

            _isWinter = false;
            
            _onEndChangingSeason.Invoke();

            if (isBranchBroken && !isDyingWolfShownBefore)
            {
                isDyingWolfShownBefore = true;
                ShowWolfDying();
            }
                
            //TODO : Change Sprites;
        }

        private void ChangeFoxPosToSleepPos()
        {
            _player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            prevPos=_player.transform.position;
            _player.transform.position=SleepPos.position;
        }
        
        private void ChangeFoxPosToNormalPos()
        {
            _player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            _player.transform.position = prevPos;
        }

        public void BranchBroken()
        {
            isBranchBroken = true;
        }

        private void ShowWolfDying()
        {
            StartCoroutine(UIManager.instance.ShowThirdText());
        }
    }
}