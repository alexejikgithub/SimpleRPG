using System;
using System.Collections.Generic;
using SimpleRPG.Enemy;

namespace SimpleRPG.Data
{
    [Serializable]
    public class KillData
    {
        public List<string> ClearedSpawners = new List<string>();
        public List<LootPieceData> NonPickedLoot = new List<LootPieceData>();
    }
    
}