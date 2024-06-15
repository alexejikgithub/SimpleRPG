using SimpleRPG.Data;

namespace SimpleRPG.Services.PersistantProgress
{
	public class PersistantProgressService : IPersistantProgressService
	{
		public PlayerProgress PlayerProgress { get; set; }
	}
}
