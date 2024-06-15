using SimpleRPG.Infrastructure.AssetManagement;
using SimpleRPG.Infrastructure.Services;
using SimpleRPG.Services.PersistantProgress;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace SimpleRPG.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assets;

        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISaveProgress> ProgressWriters { get; } = new List<ISaveProgress>();

        public GameFactory(IAssetProvider assets)
        {
            _assets = assets;
        }

        public GameObject CreateHero(GameObject initialPoint)
		{
            return InstantiateRegistred(AssetPath.HeroPath, initialPoint.transform.position);
		}
		public void CreateHud()
		{
			InstantiateRegistred(AssetPath.UIPath);
		}

		private GameObject InstantiateRegistred(string prefabPath, Vector3 position)
		{
            GameObject gameObject = _assets.Instantiate(prefabPath, position);
			RegisterProgressWatchers(gameObject);
            return gameObject;
		}
		private GameObject InstantiateRegistred(string prefabPath)
		{
			GameObject gameObject = _assets.Instantiate(prefabPath);
			RegisterProgressWatchers(gameObject);
			return gameObject;
		}

		private void RegisterProgressWatchers(GameObject gameObject)
		{
			foreach (ISavedProgressReader progressReader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
			{
				Register(progressReader);
			}
		}

        public void Cleanup()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }
		private void Register(ISavedProgressReader progressReader)
		{
			if(progressReader is ISaveProgress progressWriter)
            {
                ProgressWriters.Add(progressWriter);
            }
            ProgressReaders.Add(progressReader);   
		}
	}
}