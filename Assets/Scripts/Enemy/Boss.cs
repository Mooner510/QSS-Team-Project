using System.Collections;
using LivingEntity;
using UnityEngine;
using Yb;

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

        public override void Kill()
        {
            var scoreboard = GameObject.Find("ScoreBoard");
            var particle = scoreboard.GetComponent<ScoreBoard>().particle;
            for (var i = 0; i < 150; i++)
            {
                var o = Instantiate(particle, transform.position + new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f)), Quaternion.identity);
                o.GetComponent<SpriteRenderer>().color = new Color(Random.Range(0.6f, 1f), Random.Range(0f, 0.1f), Random.Range(0f, 0.1f));
                o.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * 3;
                o.GetComponent<Rigidbody2D>().gravityScale = Random.Range(0.1f, 0.5f);
            }
            ScoreBoard.AddScore(100);

            BossTimer.BossSpawned = false;
            base.Kill();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.gameObject.CompareTag("Capsule")) return;
            ScoreBoard.AddScore(2);

            Damage(GameObject.Find("Player").GetComponent<Player>().GetDamage());
            var spriteRenderer = GetComponent<SpriteRenderer>();
            var color = spriteRenderer.color;
            color.a = GetHealth() / GetMaxHealth() * 0.8f + 0.2f;
            spriteRenderer.color = color;
            
            var scoreboard = GameObject.Find("ScoreBoard");
            var particle = scoreboard.GetComponent<ScoreBoard>().particle;
            for (var i = 0; i < 2; i++)
            {
                var o = Instantiate(particle, col.transform.position + new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f)), Quaternion.identity);
                o.GetComponent<SpriteRenderer>().color = Color.white;
                o.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
                o.GetComponent<Rigidbody2D>().gravityScale = Random.Range(0.1f, 0.3f);
            }

            Destroy(col.gameObject);
        }
    }
}