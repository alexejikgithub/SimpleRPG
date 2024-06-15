using SimpleRPG.Data;

namespace SimpleRPG.Services.PersistantProgress
{
	public interface ISaveProgress
	{
		void UpdateProgress(PlayerProgress progress);
	}
}