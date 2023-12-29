using UnityEngine;

namespace SimpleRPG.Services.Input
{
    public interface IInputService
    {
        Vector2 Axis { get; }
        bool IsAttackButtonUp();
    }
}