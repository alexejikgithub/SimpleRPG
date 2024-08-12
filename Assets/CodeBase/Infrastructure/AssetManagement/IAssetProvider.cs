using System.Threading.Tasks;
using SimpleRPG.Infrastructure.Services;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SimpleRPG.Infrastructure.AssetManagement
{
    public interface IAssetProvider : IService
    {
        Task<GameObject> Instantiate(string address);
        Task<GameObject> Instantiate(string address, Vector3 position);
        Task<T> Load<T>(AssetReference assetReference) where T : class;
        void Cleanup();
        Task<T> Load<T>(string address) where T : class;
        void Initialize();
    }
}