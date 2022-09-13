using UnityEngine;

namespace Enemy.Enemies
{
    public class Virus : Enemy
    {
        [SerializeField] private float attackDelay;

        public override EnemyType GetEnemyType() => EnemyType.Virus;

        protected override float GetAttackDelay() => attackDelay;

        protected override void Attack()
        {
            for (var i = 0; i < 6; i++)
            {
                var o = Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, i * 60));
                o.GetComponent<Toxic>().SetDamage(GetDamage());
            }
        }
    }
}