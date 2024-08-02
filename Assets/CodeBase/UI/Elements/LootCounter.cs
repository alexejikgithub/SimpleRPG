using SimpleRPG.Data;
using SimpleRPG.Services.PersistantProgress;
using TMPro;
using UnityEngine;

namespace SimpleRPG.UI.Elements
{
    public class LootCounter : MonoBehaviour,ISavedProgress, ISavedProgressReader
    {
        [SerializeField] private TextMeshProUGUI _counter;

        private WorldData _worldData;

        private void Start()
        {
            UpdateCounter();
        }

        public void Construct(WorldData worldData)
        {
            _worldData = worldData;
            _worldData.LootData.Changed += UpdateCounter;
        }

        private void UpdateCounter()
        {
            
            _counter.text = $"{_worldData.LootData.CollectedAmount}";
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.WorldData.LootData = _worldData.LootData;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _worldData.LootData = progress.WorldData.LootData;
        }
    }
}