using UnityEngine;

namespace SimpleRPG.Enemy
{
    [RequireComponent(typeof(Attack))]
    public class CheckAttackRange : MonoBehaviour
    {
        [SerializeField] private Attack _attack;
        [SerializeField] private TriggerObserver _triggerObserver;

        private void Start()
        {
            _triggerObserver.TriggerEnter += TriggerEnter;
            _triggerObserver.TriggerExit += TriggerExit;
            _attack.Disable();
        }


        private void TriggerEnter(Collider obj)
        {
            _attack.Enable();
        }

        private void TriggerExit(Collider obj)
        {
            _attack.Disable();
        }
    }
}