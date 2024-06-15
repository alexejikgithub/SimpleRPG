using SimpleRPG.Data;

namespace SimpleRPG.Infrastructure.Services.SaveLoad
{
	public interface ISaveLoadService : IService
	{
		PlayerProgress LoadProgress();
		void SaveProgress();
	}
}
