using SimpleRPG.Data;

namespace SimpleRPG.Services.PersistantProgress
{
	public interface ISavedProgress
	{
		void UpdateProgress(PlayerProgress progress);
	}
}