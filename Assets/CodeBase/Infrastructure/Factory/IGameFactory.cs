using SimpleRPG.Infrastructure.Services;
using SimpleRPG.Services.PersistantProgress;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleRPG.Infrastructure.Factory
{
    public interface IGameFactory: IService
    {
		List<ISavedProgressReader> ProgressReaders { get; }
		List<ISaveProgress> ProgressWriters { get; }

		GameObject CreateHero(GameObject initialPoint);
        void CreateHud();
		void Cleanup();
	}
}