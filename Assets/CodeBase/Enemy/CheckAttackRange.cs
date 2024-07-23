using UnityEngine;

namespace SimpleRPG.Enemy
{
    [RequireComponent(typeof(EnemyAttack))]
    public class CheckAttackRange : MonoBehaviour
    {
        [SerializeField] private EnemyAttack enemyAttack;
        [SerializeField] private TriggerObserver _triggerObserver;

        private void Start()
        {
            _triggerObserver.TriggerEnter += TriggerEnter;
            _triggerObserver.TriggerExit += TriggerExit;
            enemyAttack.Disable();
        }


        private void TriggerEnter(Collider obj)
        {
            enemyAttack.Enable();
        }

        private void TriggerExit(Collider obj)
        {
            enemyAttack.Disable();
        }
    }
}