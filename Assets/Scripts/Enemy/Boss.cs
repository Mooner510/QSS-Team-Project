using System.Collections;
using LivingEntity;
using UnityEngine;

namespace Enemy
{
    public abstract class Boss : Entity
    {
        [SerializeField] protected GameObject[] bullets;
        
        protected override void Start()
        {
            base.Start();
            var attackCount = GetAttackCount();
            for (var i = 0; i < attackCount; i++) StartCoroutine(Run(GetAttack(i)));
        }

        private IEnumerator Run(BossAttack attackRunnable)
        {
            yield return new WaitForSeconds(attackRunnable.GetAttackDelay());
            attackRunnable.Run();
            if(!IsDeath() || !GameObject.Find("Player").GetComponent<Player>().IsDeath())
                StartCoroutine(Run(attackRunnable));
        }

        private void OnBecameInvisible() => Destroy(gameObject);

        public virtual EnemyType GetEnemyType() => EnemyType.Unknown;

        protected abstract int GetAttackCount();

        protected abstract BossAttack GetAttack(int index);

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.gameObject.CompareTag("Capsule")) return;
            ScoreBoard.AddScore(2);

            Damage(GameObject.Find("Player").GetComponent<Player>().GetDamage());
            var spriteRenderer = GetComponent<SpriteRenderer>();
            var color = spriteRenderer.color;
            color.a = GetHealth() / GetMaxHealth();
            spriteRenderer.color = color;

            Destroy(col.gameObject);
        }
    }
}