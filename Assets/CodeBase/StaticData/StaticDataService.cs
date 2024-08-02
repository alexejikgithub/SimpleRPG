using System.Collections.Generic;
using System.Linq;
using SimpleRPG.Logic;
using SimpleRPG.StaticData.Windows;
using SimpleRPG.UI.Services.Windows;
using UnityEngine;

namespace SimpleRPG.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private Dictionary<EnemyTypeId, EnemyStaticData> _enemies;
        private Dictionary<string, LevelStaticData> _levels;
        private Dictionary<WindowId, WindowConfig> _windowConfigs;
        
        private const string StaticDataEnemiesPath = "StaticData/Enemies";
        private const string StaticDataLevelsPath = "StaticData/Levels";
        private const string StaticDataWindowsPath = "StaticData/UI/WindowStaticData";

        public void LoadEnemies()
        {
            _enemies = Resources.LoadAll<EnemyStaticData>(StaticDataEnemiesPath)
                .ToDictionary(x => x.EnemyTypeId, x => x);

            _levels = Resources.LoadAll<LevelStaticData>(StaticDataLevelsPath)
                .ToDictionary(x => x.LevelKey, x => x);

            _windowConfigs = Resources.Load<WindowStaticData>(StaticDataWindowsPath).Configs
                .ToDictionary(x => x.WindowId, x => x);
        }

        public EnemyStaticData ForEnemy(EnemyTypeId typeId)
        {
            if (_enemies.TryGetValue(typeId, out EnemyStaticData staticData))
            {
                return staticData;
            }

            return null;
        }

        public LevelStaticData ForLevel(string sceneKey)
        {
            if (_levels.TryGetValue(sceneKey, out LevelStaticData staticData))
            {
                return staticData;
            }

            return null;
        }

        public WindowConfig ForWindow(WindowId windowId)
        {
            if (_windowConfigs.TryGetValue(windowId, out WindowConfig staticData))
            {
                return staticData;
            }

            return null;
        }
    }
}