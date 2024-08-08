using UnityEngine;

namespace SimpleRPG.Infrastructure
{
    public class GameRunner : MonoBehaviour
    {
        [SerializeField] GameBootstrapper _bootstrapperPrefab;

        private void Awake()
        {
            var gameBootstrapper = FindObjectOfType<GameBootstrapper>();
            if (gameBootstrapper == null)
            {
                Instantiate(_bootstrapperPrefab);
            }
        }
    }
}