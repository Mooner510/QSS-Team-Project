using UnityEngine;

namespace Enemy.Enemies
{
    public class Cancer : Enemy
    {
        [SerializeField] private float attackDelay;
        private float _pushing = 1f;

        public override EnemyType GetEnemyType() => EnemyType.Cancer;

        protected override float GetAttackDelay() => attackDelay;

        private void Push()
        {
            InvokeRepeating(nameof(Pushing), 0, 0.05f);
            Invoke(nameof(StopPushing), 2);
        }

        private void StopPushing()
        {
            CancelInvoke(nameof(Pushing));
        }

        private void Pushing()
        {
            _pushing *= 0.95f;
            transform.Translate(Vector3.forward * _pushing * 0.05f);
        }

        protected override void Attack()
        {
            var o = Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
            o.GetComponent<Cancer>().Push();
        }
    }
}