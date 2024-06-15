using SimpleRPG.Data;
using SimpleRPG.Infrastructure.Services;

namespace SimpleRPG.Services.PersistantProgress
{
	public interface IPersistantProgressService: IService
	{
		PlayerProgress PlayerProgress { get; set; }
	}
}