using System.Collections;
using UnityEngine;

namespace Enemy.Enemies
{
    public class Cancer : Enemy
    {
        [SerializeField] private float attackDelay;
        [SerializeField] private int distanceFrame;
        [SerializeField] private float cloneSpeed;
        [SerializeField] private float cloneDistance;
        [SerializeField] private float cloneMaxCount;
        private bool _cloned;

        public override EnemyType GetEnemyType() => EnemyType.Cancer;

        protected override float GetAttackDelay() => attackDelay;

        private IEnumerator Pushing(GameObject o)
        {
            var v = new Vector2(Random.Range(-cloneSpeed, cloneSpeed), Random.Range(-cloneSpeed, cloneSpeed)) * cloneDistance;
            var push = 1f;
            for (var i = 0; i < distanceFrame; i++)
            {
                yield return null;
                if(o == null) break;
                push *= 0.98f;
                o.transform.Translate(v * push * Time.deltaTime);
            }
        }

        protected override void Attack()
        {
            if(_cloned) return;
            var count = Random.Range(1, cloneMaxCount + 1);
            for (var i = 0; i < count; i++)
            {
                var o = Instantiate(bullet, transform.position, transform.rotation);
                o.GetComponent<Cancer>()._cloned = true;
                StartCoroutine(Pushing(o));
            }
        }
    }
}