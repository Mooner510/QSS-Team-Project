using UnityEngine;

namespace Enemy.Enemies
{
    public class Bacteria : Enemy
    {
        [SerializeField] private float attackDelay;

        public override EnemyType GetEnemyType() => EnemyType.Bacteria;

        protected override float GetAttackDelay() => attackDelay;

        protected override void Attack()
        {
            var o = Instantiate(toxic, transform.position, Quaternion.Euler(0, 0, 180));
            o.GetComponent<Toxic>().SetDamage(GetDamage());
        }
    }
}