using SimpleRPG.Infrastructure.AssetManagement;
using SimpleRPG.Infrastructure.Services;
using UnityEngine;

namespace SimpleRPG.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assets;

        public GameFactory(IAssetProvider assets)
        {
            _assets = assets;
        }

        public GameObject CreateHero(GameObject initialPoint)
        {
            return _assets.Instantiate(AssetPath.HeroPath, initialPoint.transform.position);
        }

        public void CreateHud()
        {
            _assets.Instantiate(AssetPath.UIPath);
        }
    }
}