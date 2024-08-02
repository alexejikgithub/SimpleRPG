using SimpleRPG.Infrastructure.Services;
using SimpleRPG.Logic;
using SimpleRPG.StaticData.Windows;
using SimpleRPG.UI.Services.Windows;

namespace SimpleRPG.StaticData
{
    public interface IStaticDataService : IService
    {
        void LoadEnemies();
        EnemyStaticData ForEnemy(EnemyTypeId typeId);
        LevelStaticData ForLevel(string sceneKey);
        WindowConfig ForWindow(WindowId shop);
    }
}