using UnityEngine;

namespace SimpleRPG.Services.Input
{
    public class MobileInputService : InputService
    {
        public override Vector2 Axis => GetSimpleInputAxis();
    }
}