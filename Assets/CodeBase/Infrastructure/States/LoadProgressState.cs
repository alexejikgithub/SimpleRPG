using SimpleRPG.Data;
using SimpleRPG.Infrastructure.Services.SaveLoad;
using SimpleRPG.Services.PersistantProgress;
using UnityEngine;

namespace SimpleRPG.Infrastructure.States
{
	public class LoadProgressState : IState
	{
		private readonly GameStateMachine _gameStateMachine;
		private readonly IPersistantProgressService _progressService;
		private readonly ISaveLoadService _saveLoadService;

		public LoadProgressState(GameStateMachine gameStateMachine, IPersistantProgressService progressService, ISaveLoadService saveLoadService)
		{
			_gameStateMachine = gameStateMachine;
			_progressService = progressService;
			_saveLoadService = saveLoadService;
		}

		public void Enter()
		{
			TryLoadProgress();
			_gameStateMachine.Enter<LoadLevelState, string>(_progressService.PlayerProgress.WorldData.PositionOnLevel.Level);
		}


		public void Exit()
		{
		}
		private void TryLoadProgress()
		{
			_progressService.PlayerProgress = _saveLoadService.LoadProgress() ?? NewProgress();
		}

		private PlayerProgress NewProgress()
		{
			var progress = new PlayerProgress(initialLevel: "Main");

			progress.HeroState.MaxHP = 50;
			progress.HeroState.ResetHP();
			progress.HeroStats.Damage = 1f;
			progress.HeroStats.DamageRadius = 1f;
			
			return progress;
		}
	}
}
