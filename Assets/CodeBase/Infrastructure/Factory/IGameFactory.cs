using SimpleRPG.Infrastructure.Services;
using SimpleRPG.Services.PersistantProgress;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleRPG.Infrastructure.Factory
{
    public interface IGameFactory: IService
    {
		event Action HeroCreated;
		List<ISavedProgressReader> ProgressReaders { get; }
		List<ISaveProgress> ProgressWriters { get; }
		GameObject HeroGameObject { get; }

		GameObject CreateHero(GameObject initialPoint);
        GameObject CreateHud();
		void Cleanup();


	}
}