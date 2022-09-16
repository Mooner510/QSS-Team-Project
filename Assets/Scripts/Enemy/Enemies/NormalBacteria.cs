using UnityEngine;

namespace Enemy.Enemies
{
    public class NormalBacteria : Enemy
    {
        [SerializeField] private float attackDelay;

        public override EnemyType GetEnemyType() => EnemyType.Bacteria;

        protected override float GetAttackDelay() => attackDelay;

        protected override void Attack()
        {
            var rot = transform.rotation;
            rot.z *= 1 - Random.Range(-0.05f, 0.05f);
            transform.rotation = rot;
            transform.Translate(transform.forward);
        }
    }
}