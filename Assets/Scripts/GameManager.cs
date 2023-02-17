using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
        private void Awake()
        {
            if (instance == null)
                instance = this;

            DontDestroyOnLoad(gameObject);
        }

        public void ResetShards()
        {
            var shards = FindObjectsOfType<IceShard>();

            foreach (var shard in shards)
            {
                shard.ReCreateIceShard();
            }
        }
    }
}