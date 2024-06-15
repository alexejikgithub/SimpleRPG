using SimpleRPG.Data;

namespace SimpleRPG.Services.PersistantProgress
{
	public interface ISavedProgressReader
	{
		void LoadProgress(PlayerProgress progress);
	}

}