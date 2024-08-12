using SimpleRPG.Logic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SimpleRPG.StaticData
{
    [CreateAssetMenu(fileName = "EnemyStaticData", menuName = "StaticData/Enemy")]
    public class EnemyStaticData : ScriptableObject
    {
        public EnemyTypeId EnemyTypeId;

        [Range(1, 100)] public int Hp = 10;

        [Range(1, 30)] public float Damage = 1;
        [Range(1, 30)] public float MoveSpeed = 2;
        
        public int MaxLoot;
        public int MinLoot;

        public AssetReferenceGameObject PrefabRference;
    }
}