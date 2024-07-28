using SimpleRPG.Infrastructure.Services;
using SimpleRPG.Logic;

namespace SimpleRPG.StaticData
{
    public interface IStaticDataService : IService
    {
        void LoadEnemies();
        EnemyStaticData ForEnemy(EnemyTypeId typeId);
    }
}