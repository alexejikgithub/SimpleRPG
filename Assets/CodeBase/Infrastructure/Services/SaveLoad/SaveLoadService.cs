using SimpleRPG.Data;
using SimpleRPG.Infrastructure.Factory;
using SimpleRPG.Services.PersistantProgress;
using TMPro.EditorUtilities;
using UnityEngine;

namespace SimpleRPG.Infrastructure.Services.SaveLoad
{
	public class SaveLoadService : ISaveLoadService
	{
		private const string ProgressKey = "Progress";
		private readonly IPersistantProgressService _progressService;
		private readonly IGameFactory _gameFactory;

		public SaveLoadService(IPersistantProgressService progressService,  IGameFactory gameFactory)
		{
			_progressService = progressService;
			_gameFactory = gameFactory;
		}

		public PlayerProgress LoadProgress()
		{
			return PlayerPrefs.GetString(ProgressKey)?.ToDeserialized<PlayerProgress>();
		}

		public void SaveProgress()
		{
			foreach (ISaveProgress progressWriter in _gameFactory.ProgressWriters)
			{
				progressWriter.UpdateProgress(_progressService.PlayerProgress);
			}
			PlayerPrefs.SetString(ProgressKey, _progressService.PlayerProgress.ToJson());
		}
	}
}
