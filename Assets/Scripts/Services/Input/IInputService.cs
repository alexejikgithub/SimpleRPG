using SimpleRPG.Infrastructure.Services;
using UnityEngine;

namespace SimpleRPG.Services.Input
{
    public interface IInputService : IService
    {
        Vector2 Axis { get; }
        bool IsAttackButtonUp();
    }
}