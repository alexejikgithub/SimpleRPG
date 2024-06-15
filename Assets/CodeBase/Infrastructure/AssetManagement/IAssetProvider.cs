using SimpleRPG.Infrastructure.Services;
using UnityEngine;

namespace SimpleRPG.Infrastructure.AssetManagement
{
    public interface IAssetProvider : IService
    {
        GameObject Instantiate(string path);
        GameObject Instantiate(string path, Vector3 position);
    }
}