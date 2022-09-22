using UnityEngine;

namespace Enemy.Enemies
{
    public class NormalBacteria : Enemy
    {
        [SerializeField] private float attackDelay;
        private bool _right;

        public override EnemyType GetEnemyType() => EnemyType.NormalBacteria;

        protected override float GetAttackDelay() => attackDelay;

        protected override void Attack()
        {
            var rot = transform.rotation;
            if (Random.value <= 0.05f) _right = !_right;
            rot.z += Random.Range(0, 0.05f) * (_right ? -1 : 1);
            transform.rotation = rot;
        }
    }
}