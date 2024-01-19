using SimpleRPG.Infrastructure.Services;
using UnityEngine;

namespace SimpleRPG.Infrastructure.Factory
{
    public interface IGameFactory: IService
    {
        GameObject CreateHero(GameObject initialPoint);
        void CreateHud();
    }
}